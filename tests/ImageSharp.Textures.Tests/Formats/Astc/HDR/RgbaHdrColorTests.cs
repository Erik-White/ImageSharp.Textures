// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Textures.Compression.Astc.Core;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc.HDR;

public class Rgba64ExtensionsTests
{
    [Fact]
    public void Constructor_WithValidValues_ShouldInitializeCorrectly()
    {
        Rgba64 color = new(1000, 2000, 3000, 4000);

        Assert.Equal(1000, color.R);
        Assert.Equal(2000, color.G);
        Assert.Equal(3000, color.B);
        Assert.Equal(4000, color.A);
    }

    [Fact]
    public void GetChannel_WithValidIndices_ShouldReturnCorrectChannels()
    {
        Rgba64 color = new(1000, 2000, 3000, 4000);

        Assert.Equal(1000, color.GetChannel(0));
        Assert.Equal(2000, color.GetChannel(1));
        Assert.Equal(3000, color.GetChannel(2));
        Assert.Equal(4000, color.GetChannel(3));
    }

    [Fact]
    public void GetChannel_WithInvalidIndex_ShouldThrowException()
    {
        Rgba64 color = new(1000, 2000, 3000, 4000);

        Action act = () => _ = color.GetChannel(4);

        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void FromLdr_WithMinMaxValues_ShouldScaleCorrectly()
    {
        Rgba32 ldrColor = new(0, 127, 255, 200);

        Rgba64 hdrColor = ldrColor.ToHdr();

        Assert.Equal(0, hdrColor.R);        // 0 * 257 = 0
        Assert.Equal(32639, hdrColor.G);    // 127 * 257 = 32639
        Assert.Equal(65535, hdrColor.B);    // 255 * 257 = 65535
        Assert.Equal(51400, hdrColor.A);    // 200 * 257 = 51400
    }

    [Fact]
    public void ToLdr_WithHdrValues_ShouldDownscaleCorrectly()
    {
        Rgba64 hdrColor = new(0, 32639, 65535, 51400);

        Rgba32 ldrColor = hdrColor.ToLdr();

        Assert.Equal(0, ldrColor.R);     // 0 >> 8 = 0
        Assert.Equal(127, ldrColor.G);   // 32639 >> 8 = 127
        Assert.Equal(255, ldrColor.B);   // 65535 >> 8 = 255
        Assert.Equal(200, ldrColor.A);   // 51400 >> 8 = 200
    }

    [Fact]
    public void FromLdr_ToLdr_RoundTrip_ShouldPreserveValues()
    {
        Rgba32 original = new(50, 100, 150, 200);

        Rgba64 hdrColor = original.ToHdr();
        Rgba32 result = hdrColor.ToLdr();

        Assert.Equal(original.R, result.R);
        Assert.Equal(original.G, result.G);
        Assert.Equal(original.B, result.B);
        Assert.Equal(original.A, result.A);
    }

    [Fact]
    public void IsCloseTo_WithSimilarColors_ShouldReturnTrue()
    {
        Rgba64 color1 = new(1000, 2000, 3000, 4000);
        Rgba64 color2 = new(1005, 1995, 3002, 3998);

        bool result = color1.IsCloseTo(color2, 10);

        Assert.True(result);
    }

    [Fact]
    public void IsCloseTo_WithDifferentColors_ShouldReturnFalse()
    {
        Rgba64 color1 = new(1000, 2000, 3000, 4000);
        Rgba64 color2 = new(1020, 2000, 3000, 4000);

        bool result = color1.IsCloseTo(color2, 10);

        Assert.False(result);
    }

    [Fact]
    public void Empty_ShouldReturnBlackTransparent()
    {
        Rgba64 empty = default(Rgba64);

        Assert.Equal(0, empty.R);
        Assert.Equal(0, empty.G);
        Assert.Equal(0, empty.B);
        Assert.Equal(0, empty.A);
    }
}
