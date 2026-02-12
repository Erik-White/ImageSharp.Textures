// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Text.RegularExpressions;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Textures.Formats.Ktx2;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.ImageComparison;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;
using SixLabors.ImageSharp.Textures.TextureFormats;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Ktx2;

[Trait("Format", "Ktx2")]
[Trait("Format", "Astc")]
public partial class Ktx2AstcDecoderTests
{
    private static readonly Ktx2Decoder KtxDecoder = new();

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_4x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_5x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_5x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_6x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_6x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_8x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_8x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_8x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_10x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_10x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_10x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_10x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_12x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_12x12)]
    public void Ktx2AstcDecoder_CanDecode_Rgba32_Blocksizes(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        FlatTexture flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.Single(flatTexture.MipMaps);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.Equal(256, firstMipMap.Width);
        Assert.Equal(256, firstMipMap.Height);
        Assert.Equal(32, firstMipMap.PixelType.BitsPerPixel);

        Image<Rgba32> firstMipMapImage = firstMipMap as Image<Rgba32>;

        // Note that the comparer is given a high threshold to allow for the lossy compression of ASTC,
        // especially at larger block sizes, but the output is still expected to be very similar to the reference image.
        firstMipMapImage.CompareToReferenceOutput(ImageComparer.TolerantPercentage(2.0f), provider, appendPixelTypeToFileName: false);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_4x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_5x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_5x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_6x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_6x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_8x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_8x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_8x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_10x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_10x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_10x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_10x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_12x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_Unorm_12x12)]
    public void Ktx2AstcDecoder_CanDecode_Rgba32_Unorm(TestTextureProvider provider)
    {
        string blockSize = GetBlockSizeFromFileName(provider.InputFile);
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        FlatTexture flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.Single(flatTexture.MipMaps);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.Equal(16, firstMipMap.Width);
        Assert.Equal(16, firstMipMap.Height);
        Assert.Equal(32, firstMipMap.PixelType.BitsPerPixel);

        (firstMipMap as Image<Rgba32>).CompareToReferenceOutput(ImageComparer.TolerantPercentage(0.05f), provider, testOutputDetails: $"{blockSize}");
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_4x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_5x4)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_5x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_6x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_6x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_8x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_8x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_8x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_10x5)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_10x6)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_10x8)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_10x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_12x10)]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Rgb32_sRgb_12x12)]
    public void Ktx2AstcDecoder_CanDecode_Rgba32_Srgb(TestTextureProvider provider)
    {
        string blockSize = GetBlockSizeFromFileName(provider.InputFile);
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        FlatTexture flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.Single(flatTexture.MipMaps);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.Equal(16, firstMipMap.Width);
        Assert.Equal(16, firstMipMap.Height);
        Assert.Equal(32, firstMipMap.PixelType.BitsPerPixel);

        (firstMipMap as Image<Rgba32>).CompareToReferenceOutput(ImageComparer.TolerantPercentage(0.05f), provider, testOutputDetails: $"{blockSize}");
    }

    private static string GetBlockSizeFromFileName(string fileName)
    {
        Match match = GetBlockSizeFromFileName().Match(fileName);

        return match.Success ? match.Value : string.Empty;
    }

    [GeneratedRegex(@"(\d+x\d+)")]
    private static partial Regex GetBlockSizeFromFileName();
}
