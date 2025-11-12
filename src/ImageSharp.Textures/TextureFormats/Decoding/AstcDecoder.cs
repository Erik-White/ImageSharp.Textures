// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.TextureFormats.Decoding;

/// <summary>
/// An ASTC decoder for various block footprints.
/// Based on the Google ASTC codec implementation.
/// </summary>
/// <remarks>
/// <see href="https://github.com/google/astc-codec/tree/master/src/decoder"/>
/// </remarks>
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
    public static void DecodeBlock(ReadOnlySpan<byte> blockData, int blockWidth, int blockHeight, Span<byte> decodedPixels, bool isSrgb = false)
    {
        AstcSharp.Footprint? footprint = AstcSharp.Footprint.FromDimensions(blockWidth, blockHeight);
        if (footprint is null)
        {
            throw new ArgumentOutOfRangeException(nameof(blockWidth), "Block dimensions must be between 4 and 12.");
        }
        AstcSharp.Codec.DecompressBlock(blockData, footprint.Value, decodedPixels);
    }
}
