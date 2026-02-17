// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Common.Exceptions;
using SixLabors.ImageSharp.Textures.Formats;
using SixLabors.ImageSharp.Textures.Formats.KtxA;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.KtxA;

[Trait("Format", "KtxA")]
public partial class KtxADecoderTests
{
    private static readonly KtxADecoder KtxADecoder = new();

    [Fact]
    public void KtxADecoder_InvalidMagicBytes_ThrowsUnknownTextureFormatException()
    {
        byte[] invalidData = new byte[100];
        using MemoryStream stream = new(invalidData);

        Assert.Throws<UnknownTextureFormatException>(() => KtxADecoder.DecodeTexture(Configuration.Default, stream));
    }

    [Fact]
    public void KtxADecoder_MissingHeadChunk_ThrowsTextureFormatException()
    {
        byte[] data = new byte[100];

        // Add valid magic bytes
        data[0] = 0x41; // A
        data[1] = 0x41; // A
        data[2] = 0x50; // P
        data[3] = 0x4C; // L
        data[4] = 0x0D; // \r
        data[5] = 0x0A; // \n
        data[6] = 0x1A;
        data[7] = 0x0A; // \n

        using MemoryStream stream = new(data);

        Assert.Throws<TextureFormatException>(() => KtxADecoder.DecodeTexture(Configuration.Default, stream));
    }

    [Theory]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4)]
    public void KtxADecoder_CanDetectFormat(TestTextureProvider provider)
    {
        KtxAImageFormatDetector detector = new();
        using FileStream stream = File.OpenRead(provider.InputFile);
        byte[] header = new byte[detector.HeaderSize];
        stream.Read(header, 0, header.Length);

        ITextureFormat format = detector.DetectFormat(header);

        Assert.NotNull(format);
        Assert.Equal("Apple KTX", format.Name);
    }

    [Theory]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4)]
    public void KtxADecoder_CanIdentify(TestTextureProvider provider)
    {
        using FileStream stream = File.OpenRead(provider.InputFile);

        ITextureInfo info = KtxADecoder.Identify(Configuration.Default, stream);

        Assert.NotNull(info);
        Assert.Equal(1705, info.Width);
        Assert.Equal(787, info.Height);
    }
}
