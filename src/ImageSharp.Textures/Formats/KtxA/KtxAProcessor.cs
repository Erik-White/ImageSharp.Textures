// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Textures.Common.Exceptions;
using SixLabors.ImageSharp.Textures.Formats.Ktx;
using SixLabors.ImageSharp.Textures.TextureFormats;
using SixLabors.ImageSharp.Textures.TextureFormats.Decoding;

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Processes decompressed Apple KTX texture data.
/// </summary>
internal class KtxAProcessor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KtxAProcessor"/> class.
    /// </summary>
    /// <param name="header">The Apple KTX header.</param>
    public KtxAProcessor(KtxAHeader header) => this.Header = header;

    /// <summary>
    /// Gets the Apple KTX header.
    /// </summary>
    public KtxAHeader Header { get; }

    /// <summary>
    /// Processes the decompressed ASTC texture data and creates a texture.
    /// </summary>
    /// <param name="decompressedData">The decompressed texture data (raw ASTC blocks).</param>
    /// <param name="width">The texture width.</param>
    /// <param name="height">The texture height.</param>
    /// <returns>A decoded texture.</returns>
    public Texture ProcessTexture(byte[] decompressedData, int width, int height)
    {
        // For now, we support single-face, single-mipmap textures
        // TODO: Add support for cubemaps and mipmaps if needed
        if (this.Header.NumberOfFaces == 6)
        {
            throw new NotSupportedException("Apple KTX cubemaps are not yet supported");
        }

        // TODO: Add support for multiple mipmap levels if needed.
        // This will require calculating the dimensions of each mipmap level and extracting the corresponding data from the decompressedData array.
        if (this.Header.NumberOfMipmapLevels > 1)
        {
            throw new NotSupportedException("Apple KTX mipmaps are not yet supported");
        }

        FlatTexture texture = new();

        MipMap mipMap = CreateMipMap(this.Header.InternalFormat, decompressedData, width, height);
        texture.MipMaps.Add(mipMap);

        return texture;
    }

    private static MipMap CreateMipMap(GlInternalPixelFormat format, byte[] data, int width, int height) => format switch
    {
        GlInternalPixelFormat.CompressedRgbaAstc4x4Khr => new MipMap<RgbaAstc4X4>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc5x4Khr => new MipMap<RgbaAstc5X4>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc5x5Khr => new MipMap<RgbaAstc5X5>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc6x5Khr => new MipMap<RgbaAstc6X5>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc6x6Khr => new MipMap<RgbaAstc6X6>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc8x5Khr => new MipMap<RgbaAstc8X5>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc8x6Khr => new MipMap<RgbaAstc8X6>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc8x8Khr => new MipMap<RgbaAstc8X8>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc10x5Khr => new MipMap<RgbaAstc10X5>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc10x6Khr => new MipMap<RgbaAstc10X6>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc10x8Khr => new MipMap<RgbaAstc10X8>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc10x10Khr => new MipMap<RgbaAstc10X10>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc12x10Khr => new MipMap<RgbaAstc12X10>(default, data, width, height),
        GlInternalPixelFormat.CompressedRgbaAstc12x12Khr => new MipMap<RgbaAstc12X12>(default, data, width, height),
        _ => throw new TextureFormatException($"Unsupported ASTC format: {format}")
    };
}
