// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;
using SixLabors.ImageSharp.Textures.Common.Exceptions;
using SixLabors.ImageSharp.Textures.Formats.Ktx;

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Describes an Apple KTX HEAD chunk.
/// </summary>
internal readonly struct KtxAHeader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KtxAHeader"/> struct.
    /// </summary>
    /// <param name="internalFormat">The internal format.</param>
    /// <param name="width">The pixel width.</param>
    /// <param name="height">The pixel height.</param>
    /// <param name="depth">The pixel depth.</param>
    /// <param name="numberOfArrayElements">The number of array elements.</param>
    /// <param name="numberOfFaces">The number of faces.</param>
    /// <param name="numberOfMipmapLevels">The number of mipmap levels.</param>
    public KtxAHeader(
        GlInternalPixelFormat internalFormat,
        uint width,
        uint height,
        uint depth,
        uint numberOfArrayElements,
        uint numberOfFaces,
        uint numberOfMipmapLevels)
    {
        this.InternalFormat = internalFormat;
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        this.NumberOfArrayElements = numberOfArrayElements;
        this.NumberOfFaces = numberOfFaces;
        this.NumberOfMipmapLevels = numberOfMipmapLevels;
    }

    /// <summary>
    /// Gets the internal format.
    /// Apple uses internal format codes from OpenGL/Vulkan.
    /// For ASTC formats, this will be in the 0x93B0-0x93BD range.
    /// </summary>
    public GlInternalPixelFormat InternalFormat { get; }

    /// <summary>
    /// Gets the width in pixels of the texture at level 0.
    /// </summary>
    public uint Width { get; }

    /// <summary>
    /// Gets the height in pixels of the texture at level 0.
    /// </summary>
    public uint Height { get; }

    /// <summary>
    /// Gets the pixel depth.
    /// For 2D textures, this is typically 0.
    /// </summary>
    public uint Depth { get; }

    /// <summary>
    /// Gets the number of array elements.
    /// If the texture is not an array texture, this should be 0.
    /// </summary>
    public uint NumberOfArrayElements { get; }

    /// <summary>
    /// Gets the number of faces.
    /// For cubemaps this should be 6, for regular textures this should be 1.
    /// </summary>
    public uint NumberOfFaces { get; }

    /// <summary>
    /// Gets the number of mipmap levels.
    /// Must equal 1 for non-mipmapped textures.
    /// </summary>
    public uint NumberOfMipmapLevels { get; }

    /// <summary>
    /// Parses the HEAD chunk data.
    /// </summary>
    /// <param name="data">The HEAD chunk data (84 bytes).</param>
    /// <returns>A parsed header.</returns>
    public static KtxAHeader Parse(ReadOnlySpan<byte> data)
    {
        if (data.Length < KtxAConstants.HeadChunkDataSize)
        {
            throw new ArgumentException(
                $"HEAD chunk data must be at least {KtxAConstants.HeadChunkDataSize} bytes. Was {data.Length} bytes.",
                nameof(data));
        }

        // Skip first 16 bytes, then:
        // Offset 16-19: Internal format (0x93B0 = ASTC 4x4)
        // Offset 20-23: Base internal format
        // Offset 24-27: Pixel width
        // Offset 28-31: Pixel height
        // Offset 32-35: Pixel depth
        // Offset 36-39: Number of array elements
        // Offset 40-43: Number of faces
        // Offset 44-47: Number of mipmap levels
        var internalFormat = (GlInternalPixelFormat)BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(16, 4));
        uint width = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(24, 4));
        uint height = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(28, 4));
        uint depth = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(32, 4));
        uint numberOfArrayElements = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(36, 4));
        uint numberOfFaces = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(40, 4));
        uint numberOfMipmapLevels = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(44, 4));

        return new KtxAHeader(
            internalFormat,
            width,
            height,
            depth,
            numberOfArrayElements,
            numberOfFaces,
            numberOfMipmapLevels);
    }
}
