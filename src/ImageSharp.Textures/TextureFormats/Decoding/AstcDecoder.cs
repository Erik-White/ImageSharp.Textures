// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using AstcSharp.Core;

namespace SixLabors.ImageSharp.Textures.TextureFormats.Decoding;

/// <summary>
/// An ASTC decoder for various block footprints.
/// Based on the Google ASTC codec implementation.
/// </summary>
/// <remarks>
internal static class AstcDecoder
{
    /// <summary>
    /// Decodes an ASTC block into RGBA pixels.
    /// </summary>
    /// <param name="blockData">The 16-byte ASTC block data.</param>
    /// <param name="blockWidth">The width of the block footprint (4-12).</param>
    /// <param name="blockHeight">The height of the block footprint (4-12).</param>
    /// <param name="decodedPixels">The output span for decoded RGBA pixels.</param>
    /// <param name="isSrgb">Optional flag to indicate if the output should be in sRGB color space.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the block dimensions are invalid.</exception>
    public static void DecodeBlock(ReadOnlySpan<byte> blockData, int blockWidth, int blockHeight, Span<byte> decodedPixels, bool isSrgb = false)
    {
        Footprint footprint = Footprint.FromFootprintType(FootprintFromDimensions(blockWidth, blockHeight));

        AstcSharp.AstcDecoder.DecompressBlock(blockData, footprint, decodedPixels);
    }

    private static FootprintType FootprintFromDimensions(int width, int height)
        => (width, height) switch
        {
            (4, 4) => FootprintType.Footprint4x4,
            (5, 4) => FootprintType.Footprint5x4,
            (5, 5) => FootprintType.Footprint5x5,
            (6, 5) => FootprintType.Footprint6x5,
            (6, 6) => FootprintType.Footprint6x6,
            (8, 5) => FootprintType.Footprint8x5,
            (8, 6) => FootprintType.Footprint8x6,
            (8, 8) => FootprintType.Footprint8x8,
            (10, 5) => FootprintType.Footprint10x5,
            (10, 6) => FootprintType.Footprint10x6,
            (10, 8) => FootprintType.Footprint10x8,
            (10, 10) => FootprintType.Footprint10x10,
            (12, 10) => FootprintType.Footprint12x10,
            (12, 12) => FootprintType.Footprint12x12,
            _ => throw new ArgumentOutOfRangeException(nameof(width), "Invalid footprint type."),
        };
}
