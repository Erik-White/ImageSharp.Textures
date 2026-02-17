// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

internal static class KtxAConstants
{
    /// <summary>
    /// The minimum size of a KtxA file in bytes (8-byte magic + 8-byte first chunk header).
    /// </summary>
    public const int MinimumFileSize = 16;

    /// <summary>
    /// The size of the chunk header in bytes (4-byte size + 4-byte identifier).
    /// </summary>
    public const int ChunkHeaderSize = 8;

    /// <summary>
    /// The size of the HEAD chunk data in bytes.
    /// </summary>
    public const int HeadChunkDataSize = 84;

    /// <summary>
    /// The list of file extensions that equate to an Apple KTX file.
    /// </summary>
    public static readonly IEnumerable<string> FileExtensions = new[] { "ktx" };

    /// <summary>
    /// Gets the magic bytes identifying an Apple KTX texture.
    /// </summary>
    public static ReadOnlySpan<byte> MagicBytes =>
    [
        0x41, // A
        0x41, // A
        0x50, // P
        0x4C, // L
        0x0D, // \r
        0x0A, // \n
        0x1A,
        0x0A, // \n
    ];

    /// <summary>
    /// Gets the HEAD chunk identifier.
    /// </summary>
    public static ReadOnlySpan<byte> HeadChunkIdentifier => "HEAD"u8;

    /// <summary>
    /// Gets the LZFS chunk identifier.
    /// </summary>
    public static ReadOnlySpan<byte> LzfsChunkIdentifier => "LZFS"u8;

    /// <summary>
    /// Gets the FILL chunk identifier.
    /// </summary>
    public static ReadOnlySpan<byte> FillChunkIdentifier => "FILL"u8;
    ///// <summary>
    ///// Gets the HEAD chunk identifier.
    ///// </summary>
    //public static ReadOnlySpan<byte> HeadChunkIdentifier => new byte[]
    //{
    //    0x48, // H
    //    0x45, // E
    //    0x41, // A
    //    0x44, // D
    //};

    ///// <summary>
    ///// Gets the LZFS chunk identifier.
    ///// </summary>
    //public static ReadOnlySpan<byte> LzfsChunkIdentifier => new byte[]
    //{
    //    0x4C, // L
    //    0x5A, // Z
    //    0x46, // F
    //    0x53, // S
    //};

    ///// <summary>
    ///// Gets the FILL chunk identifier.
    ///// </summary>
    //public static ReadOnlySpan<byte> FillChunkIdentifier => new byte[]
    //{
    //    0x46, // F
    //    0x49, // I
    //    0x4C, // L
    //    0x4C, // L
    //};
}
