// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Astc.Core;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc.HDR;

public class RgbaHdrColorTests
{
    [Fact]
    public void Constructor_WithValidValues_ShouldInitializeCorrectly()
    {
        RgbaHdrColor color = new(1000, 2000, 3000, 4000);

        Assert.Equal(1000, color.R);
        Assert.Equal(2000, color.G);
        Assert.Equal(3000, color.B);
        Assert.Equal(4000, color.A);
    }

    [Fact]
    public void Indexer_WithValidIndices_ShouldReturnCorrectChannels()
    {
        RgbaHdrColor color = new(1000, 2000, 3000, 4000);

        Assert.Equal(1000, color[0]);
        Assert.Equal(2000, color[1]);
        Assert.Equal(3000, color[2]);
        Assert.Equal(4000, color[3]);
    }

    [Fact]
    public void Indexer_WithInvalidIndex_ShouldThrowException()
    {
        RgbaHdrColor color = new(1000, 2000, 3000, 4000);

        Action act = () => _ = color[4];

        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void FromLdr_WithMinMaxValues_ShouldScaleCorrectly()
    {
        RgbaColor ldrColor = new(0, 127, 255, 200);

        RgbaHdrColor hdrColor = RgbaHdrColor.FromRgba(ldrColor);

        Assert.Equal(0, hdrColor.R);        // 0 * 257 = 0
        Assert.Equal(32639, hdrColor.G);    // 127 * 257 = 32639
        Assert.Equal(65535, hdrColor.B);    // 255 * 257 = 65535
        Assert.Equal(51400, hdrColor.A);    // 200 * 257 = 51400
    }

    [Fact]
    public void ToLdr_WithHdrValues_ShouldDownscaleCorrectly()
    {
        RgbaHdrColor hdrColor = new(0, 32639, 65535, 51400);

        RgbaColor ldrColor = hdrColor.ToLowDynamicRange();

        Assert.Equal(0, ldrColor.R);     // 0 >> 8 = 0
        Assert.Equal(127, ldrColor.G);   // 32639 >> 8 = 127
        Assert.Equal(255, ldrColor.B);   // 65535 >> 8 = 255
        Assert.Equal(200, ldrColor.A);   // 51400 >> 8 = 200
    }

    [Fact]
    public void FromLdr_ToLdr_RoundTrip_ShouldPreserveValues()
    {
        RgbaColor original = new(50, 100, 150, 200);

        RgbaHdrColor hdrColor = RgbaHdrColor.FromRgba(original);
        RgbaColor result = hdrColor.ToLowDynamicRange();

        Assert.Equal(original.R, result.R);
        Assert.Equal(original.G, result.G);
        Assert.Equal(original.B, result.B);
        Assert.Equal(original.A, result.A);
    }

    [Fact]
    public void IsCloseTo_WithSimilarColors_ShouldReturnTrue()
    {
        RgbaHdrColor color1 = new(1000, 2000, 3000, 4000);
        RgbaHdrColor color2 = new(1005, 1995, 3002, 3998);

        bool result = color1.IsCloseTo(color2, 10);

        Assert.True(result);
    }

    [Fact]
    public void IsCloseTo_WithDifferentColors_ShouldReturnFalse()
    {
        RgbaHdrColor color1 = new(1000, 2000, 3000, 4000);
        RgbaHdrColor color2 = new(1020, 2000, 3000, 4000);

        bool result = color1.IsCloseTo(color2, 10);

        Assert.False(result);
    }

    [Fact]
    public void Empty_ShouldReturnBlackTransparent()
    {
        RgbaHdrColor empty = RgbaHdrColor.Empty;

        Assert.Equal(0, empty.R);
        Assert.Equal(0, empty.G);
        Assert.Equal(0, empty.B);
        Assert.Equal(0, empty.A);
    }
}
