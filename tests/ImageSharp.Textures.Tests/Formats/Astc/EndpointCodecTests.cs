// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;
using SixLabors.ImageSharp.Textures.Astc.ColorEncoding;
using SixLabors.ImageSharp.Textures.Astc.Core;
using SixLabors.ImageSharp.Textures.Astc.TexelBlock;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc;

public class EndpointCodecTests
{
    [Theory]
    [InlineData(EndpointEncodingMode.DirectLuma)]
    [InlineData(EndpointEncodingMode.DirectLumaAlpha)]
    [InlineData(EndpointEncodingMode.BaseScaleRgb)]
    [InlineData(EndpointEncodingMode.BaseScaleRgba)]
    [InlineData(EndpointEncodingMode.DirectRbg)]
    [InlineData(EndpointEncodingMode.DirectRgba)]
    internal void EncodeColorsForMode_WithVariousRanges_ShouldProduceValidQuantizedValues(EndpointEncodingMode mode)
    {
        RgbaColor low = new(0, 0, 0, 0);
        RgbaColor high = new(255, 255, 255, 255);

        for (int quantRange = 5; quantRange < 256; quantRange++)
        {
            List<int> values = [];
            EndpointEncoder.EncodeColorsForMode(low, high, quantRange, mode, out ColorEndpointMode _, values);

            // Assert value count matches expected
            Assert.Equal(mode.GetValuesCount(), values.Count);

            // Assert all values are within quantization range
            Assert.All(values, v => Assert.InRange(v, 0, quantRange));
        }
    }

    [Theory]
    [InlineData(EndpointEncodingMode.DirectLuma)]
    [InlineData(EndpointEncodingMode.DirectLumaAlpha)]
    [InlineData(EndpointEncodingMode.BaseScaleRgb)]
    [InlineData(EndpointEncodingMode.BaseScaleRgba)]
    [InlineData(EndpointEncodingMode.DirectRbg)]
    [InlineData(EndpointEncodingMode.DirectRgba)]
    internal void EncodeDecodeColors_WithBlackAndWhite_ShouldPreserveColors(EndpointEncodingMode mode)
    {
        RgbaColor white = new(255, 255, 255, 255);
        RgbaColor black = new(0, 0, 0, 255);

        for (int quantRange = 5; quantRange < 256; ++quantRange)
        {
            (RgbaColor low, RgbaColor high) = EncodeAndDecodeColors(white, black, quantRange, mode);

            Assert.True(low == white);
            Assert.True(high == black);
        }
    }

    [Fact]
    public void UsesBlueContract_WithDirectModes_ShouldDetectCorrectly()
    {
        List<int> values = [132, 127, 116, 112, 183, 180, 31, 22];

        Assert.True(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbDirect, values));
        Assert.True(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbaDirect, values));
    }

    [Fact]
    public void UsesBlueContract_WithOffsetModes_ShouldDetectBasedOnBitFlags()
    {
        List<int> baseValues = [132, 127, 116, 112, 183, 180, 31, 22];

        List<int> valuesClearedBit6 = [.. baseValues];
        valuesClearedBit6[1] &= 0xBF;
        valuesClearedBit6[3] &= 0xBF;
        valuesClearedBit6[5] &= 0xBF;
        valuesClearedBit6[7] &= 0xBF;

        Assert.False(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbBaseOffset, valuesClearedBit6));
        Assert.False(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbaBaseOffset, valuesClearedBit6));

        List<int> valuesSetBit6 = [.. baseValues];
        valuesSetBit6[1] |= 0x40;
        valuesSetBit6[3] |= 0x40;
        valuesSetBit6[5] |= 0x40;
        valuesSetBit6[7] |= 0x40;

        Assert.True(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbBaseOffset, valuesSetBit6));
        Assert.True(EndpointEncoder.UsesBlueContract(255, ColorEndpointMode.LdrRgbaBaseOffset, valuesSetBit6));
    }

    [Fact]
    public void EncodeColorsForMode_WithRgbDirectAndSpecificPairs_ShouldUseBlueContract()
    {
        (RgbaColor, RgbaColor)[] pairs =
        [
            (new RgbaColor(22, 18, 30, 59), new RgbaColor(162, 148, 155, 59)),
            (new RgbaColor(22, 30, 27, 36), new RgbaColor(228, 221, 207, 36)),
            (new RgbaColor(54, 60, 55, 255), new RgbaColor(23, 30, 27, 255))
        ];

        const int endpointRange = 31;

        foreach ((RgbaColor low, RgbaColor high) in pairs)
        {
            List<int> values = [];
            EndpointEncoder.EncodeColorsForMode(low, high, endpointRange, EndpointEncodingMode.DirectRbg, out ColorEndpointMode astcMode, values);

            Assert.True(EndpointEncoder.UsesBlueContract(endpointRange, astcMode, values));
        }
    }

    [Fact]
    public void EncodeDecodeColors_WithLumaDirect_ShouldProduceLumaValues()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.DirectLuma;

        (RgbaColor low, RgbaColor high) = EncodeAndDecodeColors(
            new RgbaColor(247, 248, 246, 255),
            new RgbaColor(2, 3, 1, 255),
            255,
            mode);

        Assert.True(low == new RgbaColor(247, 247, 247, 255));
        Assert.True(high == new RgbaColor(2, 2, 2, 255));

        (RgbaColor low2, RgbaColor high2) = EncodeAndDecodeColors(
            new RgbaColor(80, 80, 50, 255),
            new RgbaColor(99, 255, 6, 255),
            255,
            mode);

        Assert.True(low2 == new RgbaColor(70, 70, 70, 255));
        Assert.True(high2 == new RgbaColor(120, 120, 120, 255));

        (RgbaColor low3, RgbaColor high3) = EncodeAndDecodeColors(
            new RgbaColor(247, 248, 246, 255),
            new RgbaColor(2, 3, 1, 255),
            15,
            mode);

        Assert.True(low3 == new RgbaColor(255, 255, 255, 255));
        Assert.True(high3 == new RgbaColor(0, 0, 0, 255));

        (RgbaColor low4, RgbaColor high4) = EncodeAndDecodeColors(
            new RgbaColor(64, 127, 192, 255),
            new RgbaColor(0, 0, 0, 255),
            63,
            mode);

        Assert.True(low4 == new RgbaColor(130, 130, 130, 255));
        Assert.True(high4 == new RgbaColor(0, 0, 0, 255));
    }

    [Fact]
    public void EncodeDecodeColors_WithLumaAlphaDirect_ShouldPreserveLumaAndAlpha()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.DirectLumaAlpha;

        // Grey with varying alpha
        (RgbaColor low, RgbaColor high) = EncodeAndDecodeColors(
            new RgbaColor(64, 127, 192, 127),
            new RgbaColor(0, 0, 0, 20),
            63,
            mode);

        Assert.True((low == new RgbaColor(130, 130, 130, 125)) ||
            low.IsCloseTo(new RgbaColor(130, 130, 130, 125), 1));
        Assert.True((high == new RgbaColor(0, 0, 0, 20)) ||
            high.IsCloseTo(new RgbaColor(0, 0, 0, 20), 1));

        // Different alpha values
        (RgbaColor low2, RgbaColor high2) = EncodeAndDecodeColors(
            new RgbaColor(247, 248, 246, 250),
            new RgbaColor(2, 3, 1, 172),
            255,
            mode);

        Assert.True(low2 == new RgbaColor(247, 247, 247, 250));
        Assert.True(high2 == new RgbaColor(2, 2, 2, 172));
    }

    [Fact]
    public void EncodeDecodeColors_WithRgbDirectAndRandomColors_ShouldPreserveColors()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.DirectRbg;
        Random random = new(unchecked((int)0xdeadbeef));

        for (int i = 0; i < 100; ++i)
        {
            RgbaColor low = new(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256), 255);
            RgbaColor high = new(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256), 255);
            (RgbaColor low1, RgbaColor high1) = EncodeAndDecodeColors(low, high, 255, mode);

            Assert.True(low1 == low);
            Assert.True(high1 == high);
        }
    }

    [Fact]
    public void EncodeDecodeColors_WithRgbDirectAndSpecificColors_ShouldMatchExpected()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.DirectRbg;

        (RgbaColor low, RgbaColor high) = EncodeAndDecodeColors(
            new RgbaColor(64, 127, 192, 255),
            new RgbaColor(0, 0, 0, 255),
            63,
            mode);

        Assert.True(low == new RgbaColor(65, 125, 190, 255));
        Assert.True(high == new RgbaColor(0, 0, 0, 255));

        (RgbaColor low2, RgbaColor high2) = EncodeAndDecodeColors(
            new RgbaColor(0, 0, 0, 255),
            new RgbaColor(64, 127, 192, 255),
            63,
            mode);

        Assert.True(low2 == new RgbaColor(0, 0, 0, 255));
        Assert.True(high2 == new RgbaColor(65, 125, 190, 255));
    }

    [Fact]
    public void EncodeDecodeColors_WithRgbBaseScaleAndIdenticalColors_ShouldBeCloseToOriginal()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.BaseScaleRgb;
        Random random = new(unchecked((int)0xdeadbeef));

        for (int i = 0; i < 100; ++i)
        {
            RgbaColor color = new(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256), 255);
            (RgbaColor low, RgbaColor high) = EncodeAndDecodeColors(color, color, 255, mode);

            Assert.True(low.IsCloseTo(color, 1));
            Assert.True(high.IsCloseTo(color, 1));
        }
    }

    [Fact]
    public void EncodeDecodeColors_WithRgbBaseScaleAndDifferentColors_ShouldMatchExpected()
    {
        EndpointEncodingMode mode = EndpointEncodingMode.BaseScaleRgb;
        RgbaColor low = new(20, 4, 40, 255);
        RgbaColor high = new(80, 16, 160, 255);

        (RgbaColor decodedLow, RgbaColor decodedHigh) = EncodeAndDecodeColors(low, high, 255, mode);
        Assert.True(decodedLow.IsCloseTo(low, 0));
        Assert.True(decodedHigh.IsCloseTo(high, 0));

        (RgbaColor low2, RgbaColor high2) = EncodeAndDecodeColors(low, high, 127, mode);
        Assert.True(low2.IsCloseTo(low, 1));
        Assert.True(high2.IsCloseTo(high, 1));
    }

    internal static TheoryData<RgbaColor, RgbaColor> RgbBaseOffsetColorPairs() => new()
    {
        { new RgbaColor(80, 16, 112, 255), new RgbaColor(87, 18, 132, 255) },
        { new RgbaColor(80, 74, 82, 255), new RgbaColor(90, 92, 110, 255) },
        { new RgbaColor(0, 0, 0, 255), new RgbaColor(2, 2, 2, 255) },
    };

    [Theory]
#pragma warning disable xUnit1016 // MemberData is internal because RgbaColor is internal
    [MemberData(nameof(RgbBaseOffsetColorPairs))]
#pragma warning restore xUnit1016
    internal void DecodeColorsForMode_WithRgbBaseOffset_AndSpecificColorPairs_ShouldDecodeCorrectly(
        RgbaColor expectedLow, RgbaColor expectedHigh)
    {
        int[] values = EncodeRgbBaseOffset(expectedLow, expectedHigh);
        (RgbaColor decLow, RgbaColor decHigh) = EndpointCodec.DecodeColorsForMode(values, 255, ColorEndpointMode.LdrRgbBaseOffset);

        Assert.True(decLow == expectedLow);
        Assert.True(decHigh == expectedHigh);
    }

    [Fact]
    public void DecodeColorsForMode_WithRgbBaseOffset_AndIdenticalColors_ShouldDecodeCorrectly()
    {
        Random random = new(unchecked((int)0xdeadbeef));

        for (int i = 0; i < 100; ++i)
        {
            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);

            // Ensure even channels (reference test skips odd)
            if (((r | g | b) & 1) != 0)
            {
                continue;
            }

            RgbaColor color = new(r, g, b, 255);
            int[] values = EncodeRgbBaseOffset(color, color);
            (RgbaColor decLow, RgbaColor decHigh) = EndpointCodec.DecodeColorsForMode(values, 255, ColorEndpointMode.LdrRgbBaseOffset);

            Assert.True(decLow == color);
            Assert.True(decHigh == color);
        }
    }

    private static int[] EncodeRgbBaseOffset(RgbaColor low, RgbaColor high)
    {
        List<int> values = [];
        for (int i = 0; i < 3; ++i)
        {
            bool isLarge = low[i] >= 128;
            values.Add((low[i] * 2) & 0xFF);
            int diff = (high[i] - low[i]) * 2;
            if (isLarge)
            {
                diff |= 0x80;
            }

            values.Add(diff);
        }

        return [.. values];
    }

    [Fact]
    public void DecodeCheckerboard_ShouldDecodeToGrayscaleEndpoints()
    {
        string astcFilePath = TestFile.GetInputFileFullPath(Path.Combine(TestImages.Astc.InputFolder, "checkerboard.astc"));
        byte[] astcData = File.ReadAllBytes(astcFilePath);

        int blocksDecoded = 0;

        for (int i = 0; i < astcData.Length; i += PhysicalBlock.SizeInBytes)
        {
            // Read block bytes
            UInt128 blockData = BinaryPrimitives.ReadUInt128LittleEndian(astcData.AsSpan(i, PhysicalBlock.SizeInBytes));
            PhysicalBlock physicalBlock = PhysicalBlock.Create(blockData);

            // Unpack to intermediate block
            IntermediateBlock.IntermediateBlockData? intermediateBlock = IntermediateBlock.UnpackIntermediateBlock(physicalBlock);
            Assert.NotNull(intermediateBlock);
            IntermediateBlock.IntermediateBlockData ib = intermediateBlock!.Value;

            // Verify endpoints exist
            Assert.True(ib.EndpointCount > 0, "block should have endpoints");

            int colorRange = IntermediateBlock.EndpointRangeForBlock(ib);
            Assert.True(colorRange > 0, "color range should be valid");

            // Check all endpoint pairs decode successfully to grayscale colors
            for (int ep = 0; ep < ib.EndpointCount; ep++)
            {
                IntermediateBlock.IntermediateEndpointData endpoints = ib.Endpoints[ep];
                ReadOnlySpan<int> colorSpan = ((ReadOnlySpan<int>)endpoints.Colors)[..endpoints.ColorCount];
                (RgbaColor low, RgbaColor high) = EndpointCodec.DecodeColorsForMode(
                    colorSpan,
                    colorRange,
                    endpoints.Mode);

                // Assert - Checkerboard should produce grayscale colors (R == G == B)
                Assert.True(low.R == low.G, $"block {i} low endpoint should be grayscale");
                Assert.True(low.G == low.B, $"block {i} low endpoint should be grayscale");
                Assert.True(high.R == high.G, $"block {i} high endpoint should be grayscale");
                Assert.True(high.G == high.B, $"block {i} high endpoint should be grayscale");
            }

            blocksDecoded++;
        }

        // Verify we decoded a reasonable number of blocks
        Assert.True(blocksDecoded > 0);
    }

    private static (RgbaColor Low, RgbaColor High) EncodeAndDecodeColors(
        RgbaColor low,
        RgbaColor high,
        int quantRange,
        EndpointEncodingMode mode)
    {
        List<int> values = [];
        bool needsSwap = EndpointEncoder.EncodeColorsForMode(low, high, quantRange, mode, out ColorEndpointMode astcMode, values);
        (RgbaColor decLow, RgbaColor decHigh) = EndpointCodec.DecodeColorsForMode(values.ToArray(), quantRange, astcMode);

        return needsSwap ? (decHigh, decLow) : (decLow, decHigh);
    }
}
