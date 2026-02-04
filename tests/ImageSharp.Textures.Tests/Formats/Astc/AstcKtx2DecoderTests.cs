// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Textures.Formats.Ktx2;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;
using SixLabors.ImageSharp.Textures.TextureFormats;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc;

[Trait("Format", "Ktx2")]
[Trait("Compression", "Astc")]
public class AstcKtx2DecoderTests
{
    private static readonly Ktx2Decoder Ktx2Decoder = new();

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_4x4_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_4x4_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_5x4_IronBars)]
    public void Ktx2Decoder_CanDecode_Astc_5x4_IronBars(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x5_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_6x5_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x6_IronBars)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_IronBars(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x6_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Posx(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_8x6_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_8x6_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_8x8_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_8x8_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_10x5_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_10x5_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_12x10_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_12x10_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_12x12_FlightHelmet)]
    public void Ktx2Decoder_CanDecode_Astc_12x12_FlightHelmet(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_4x4_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_4x4_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x5_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_6x5_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Posy)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Posy_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Posz)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Posz_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_8x6_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_8x6_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_8x8_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_8x8_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_10x5_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_10x5_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_12x10_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_12x10_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_12x12_Posx)]
    public void Ktx2Decoder_CanDecode_Astc_12x12_Posx_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Fastest)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Fastest_Quality_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Fast)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Fast_Quality_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_6x6_Medium)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Medium_Quality_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Volume, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x6_3dTex)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_VolumeTexture(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x6_ArrayTex)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_ArrayTexture(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_6x6_ArrayTex_Mipmap)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_ArrayTexture_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Cubemap, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Ldr_Cubemap_6x6)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Cubemap(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }

    [Theory]
    [WithFile(TestTextureFormat.Ktx2, TestTextureType.Cubemap, TestTextureTool.ToKtx, TestImages.Ktx2.Astc.Mipmap_Ldr_Cubemap_6x6)]
    public void Ktx2Decoder_CanDecode_Astc_6x6_Cubemap_WithMipmaps(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(Ktx2Decoder);
        provider.SaveTextures(texture);
    }
}
