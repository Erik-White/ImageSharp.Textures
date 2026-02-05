// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Formats.Ktx2;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;
using SixLabors.ImageSharp.Textures.TextureFormats;
using Xunit;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Ktx2
{
    /// <summary>
    /// Tests for HDR (High Dynamic Range) formats in KTX2 files.
    /// These formats include:
    /// - Floating-point textures: R16_SFLOAT, RG16_SFLOAT, RGB16_SFLOAT, RGBA16_SFLOAT,
    ///   R32_SFLOAT, RG32_SFLOAT, RGB32_SFLOAT, RGBA32_SFLOAT
    /// - Packed HDR formats: E5B9G9R9_UFLOAT_PACK32 (RGB9E5), B10G11R11_UFLOAT_PACK32
    /// - Compressed HDR: BC6H_UFLOAT_BLOCK, BC6H_SFLOAT_BLOCK
    /// - ASTC HDR: Various ASTC_*_SFLOAT_BLOCK formats
    ///
    /// Note: Test files for KTX2 HDR formats are not yet available in the test suite.
    /// This class serves as a placeholder for future HDR KTX2 test implementation.
    /// When test files become available, add them to tests/Images/Input/Ktx2/Hdr/
    /// and uncomment the tests below.
    /// </summary>
    [Trait("Format", "Ktx2")]
    [Trait("Format", "Hdr")]
    public class Ktx2HdrDecoderTests
    {
        private static readonly Ktx2Decoder Ktx2Decoder = new Ktx2Decoder();

        // TODO: Uncomment and add test files when KTX2 HDR test files become available
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.R16F)]
        // public void Ktx2Decoder_CanDecode_R16_SFLOAT(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.RGBA16F)]
        // public void Ktx2Decoder_CanDecode_RGBA16_SFLOAT(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.R32F)]
        // public void Ktx2Decoder_CanDecode_R32_SFLOAT(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.RGBA32F)]
        // public void Ktx2Decoder_CanDecode_RGBA32_SFLOAT(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.RGB9E5)]
        // public void Ktx2Decoder_CanDecode_E5B9G9R9_UFLOAT_PACK32(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.R11G11B10F)]
        // public void Ktx2Decoder_CanDecode_B10G11R11_UFLOAT_PACK32(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.BC6H_UFloat)]
        // public void Ktx2Decoder_CanDecode_BC6H_UFLOAT_BLOCK(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }
        //
        // [Theory]
        // [WithFile(TestTextureFormat.Ktx2, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.Ktx2.Hdr.BC6H_SFloat)]
        // public void Ktx2Decoder_CanDecode_BC6H_SFLOAT_BLOCK(TestTextureProvider provider)
        // {
        //     using Texture texture = provider.GetTexture(Ktx2Decoder);
        //     provider.SaveTextures(texture);
        //     var flatTexture = texture as FlatTexture;
        //
        //     Assert.NotNull(flatTexture?.MipMaps);
        //     Assert.True(flatTexture.MipMaps.Count > 0);
        //
        //     Image firstMipMap = flatTexture.MipMaps[0].GetImage();
        //     Assert.NotNull(firstMipMap);
        // }

        // Note: To create KTX2 HDR test files, you can use the toktx tool from KTX-Software:
        // https://github.com/KhronosGroup/KTX-Software
        //
        // Example commands:
        // toktx --t2 --format R16_SFLOAT hdr_r16f.ktx2 input.hdr
        // toktx --t2 --format R32G32B32A32_SFLOAT hdr_rgba32f.ktx2 input.hdr
        // toktx --t2 --format E5B9G9R9_UFLOAT_PACK32 hdr_rgb9e5.ktx2 input.hdr
        // toktx --t2 --bcmp --format BC6H hdr_bc6h.ktx2 input.hdr
    }
}
