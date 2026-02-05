// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Formats.Ktx;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;
using SixLabors.ImageSharp.Textures.TextureFormats;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Ktx;

/// <summary>
/// Tests for HDR (High Dynamic Range) formats in KTX files.
/// These formats include floating-point textures (R16F, RG16F, RGB16F, RGBA16F, R32F, RG32F, RGB32F, RGBA32F).
/// </summary>
[Trait("Format", "Ktx")]
[Trait("Format", "Hdr")]
public class KtxHdrDecoderTests
{
    private static readonly KtxDecoder KtxDecoder = new();

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.R16)]
    public void KtxDecoder_CanDecode_R16F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.R32)]
    public void KtxDecoder_CanDecode_R32F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rg16)]
    public void KtxDecoder_CanDecode_RG16F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rg32)]
    public void KtxDecoder_CanDecode_RG32F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rgb16)]
    public void KtxDecoder_CanDecode_RGB16F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rgb32)]
    public void KtxDecoder_CanDecode_RGB32F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rgba16)]
    public void KtxDecoder_CanDecode_RGBA16F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx, TestTextureType.Flat, TestTextureTool.PvrTexToolCli, TestImages.Ktx.Hdr.Rgba32)]
    public void KtxDecoder_CanDecode_RGBA32F(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxDecoder);
        provider.SaveTextures(texture);
        var flatTexture = texture as FlatTexture;

        Assert.NotNull(flatTexture?.MipMaps);
        Assert.True(flatTexture.MipMaps.Count > 0);

        Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        Assert.NotNull(firstMipMap);
    }
}
