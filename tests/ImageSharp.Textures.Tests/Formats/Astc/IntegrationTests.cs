// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Astc;
using SixLabors.ImageSharp.Textures.Astc.IO;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc;

public class IntegrationTests
{
    [Theory]
    [InlineData(TestImages.Astc.Atlas_Small_4x4)]
    [InlineData(TestImages.Astc.Atlas_Small_5x5)]
    [InlineData(TestImages.Astc.Atlas_Small_6x6)]
    [InlineData(TestImages.Astc.Atlas_Small_8x8)]
    [InlineData(TestImages.Astc.Checkerboard)]
    [InlineData(TestImages.Astc.Checkered_4)]
    [InlineData(TestImages.Astc.Checkered_5)]
    [InlineData(TestImages.Astc.Checkered_6)]
    [InlineData(TestImages.Astc.Checkered_7)]
    [InlineData(TestImages.Astc.Checkered_8)]
    [InlineData(TestImages.Astc.Checkered_9)]
    [InlineData(TestImages.Astc.Checkered_10)]
    [InlineData(TestImages.Astc.Checkered_11)]
    [InlineData(TestImages.Astc.Checkered_12)]
    [InlineData(TestImages.Astc.Footprint_4x4)]
    [InlineData(TestImages.Astc.Footprint_5x4)]
    [InlineData(TestImages.Astc.Footprint_5x5)]
    [InlineData(TestImages.Astc.Footprint_6x5)]
    [InlineData(TestImages.Astc.Footprint_6x6)]
    [InlineData(TestImages.Astc.Footprint_8x5)]
    [InlineData(TestImages.Astc.Footprint_8x6)]
    [InlineData(TestImages.Astc.Footprint_8x8)]
    [InlineData(TestImages.Astc.Footprint_10x5)]
    [InlineData(TestImages.Astc.Footprint_10x6)]
    [InlineData(TestImages.Astc.Footprint_10x8)]
    [InlineData(TestImages.Astc.Footprint_10x10)]
    [InlineData(TestImages.Astc.Footprint_12x10)]
    [InlineData(TestImages.Astc.Footprint_12x12)]
    [InlineData(TestImages.Astc.Rgb_4x4)]
    [InlineData(TestImages.Astc.Rgb_5x4)]
    [InlineData(TestImages.Astc.Rgb_6x6)]
    [InlineData(TestImages.Astc.Rgb_8x8)]
    [InlineData(TestImages.Astc.Rgb_12x12)]
    public void DecompressToImage_WithTestdataFile_ShouldDecodeSuccessfully(string inputFile)
    {
        string filePath = TestFile.GetInputFileFullPath(inputFile);
        byte[] bytes = File.ReadAllBytes(filePath);
        AstcFile astc = AstcFile.FromMemory(bytes);

        Span<byte> result = AstcDecoder.DecompressImage(astc);

        Assert.True(result.Length > 0, $"decoding should succeed for {inputFile}");
    }
}
