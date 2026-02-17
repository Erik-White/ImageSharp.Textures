// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Represents a chunk in an Apple KTX file.
/// </summary>
internal readonly struct KtxAChunk
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KtxAChunk"/> struct.
    /// </summary>
    /// <param name="size">The size of the chunk data in bytes.</param>
    /// <param name="identifier">The 4-byte chunk identifier.</param>
    /// <param name="dataOffset">The offset in the stream where the chunk data begins.</param>
    public KtxAChunk(uint size, byte[] identifier, long dataOffset)
    {
        this.Size = size;
        this.Identifier = identifier;
        this.DataOffset = dataOffset;
    }

    /// <summary>
    /// Gets the size of the chunk data in bytes.
    /// </summary>
    public uint Size { get; }

    /// <summary>
    /// Gets the 4-byte chunk identifier (e.g., "HEAD", "LZFS", "FILL").
    /// </summary>
    public byte[] Identifier { get; }

    /// <summary>
    /// Gets the offset in the stream where the chunk data begins.
    /// </summary>
    public long DataOffset { get; }

    /// <summary>
    /// Parses a chunk header from the provided data.
    /// </summary>
    /// <param name="data">The chunk header data (8 bytes).</param>
    /// <param name="currentStreamPosition">The current position in the stream after reading the header.</param>
    /// <returns>A parsed chunk.</returns>
    public static KtxAChunk Parse(ReadOnlySpan<byte> data, long currentStreamPosition)
    {
        if (data.Length < KtxAConstants.ChunkHeaderSize)
        {
            throw new ArgumentException(
                $"Chunk header must be {KtxAConstants.ChunkHeaderSize} bytes. Was {data.Length} bytes.",
                nameof(data));
        }

        uint size = BinaryPrimitives.ReadUInt32LittleEndian(data[..4]);
        byte[] identifier = data.Slice(4, 4).ToArray();

        return new KtxAChunk(size, identifier, currentStreamPosition);
    }

    /// <summary>
    /// Checks if this chunk has the specified identifier.
    /// </summary>
    /// <param name="expectedIdentifier">The expected identifier.</param>
    /// <returns>True if the identifier matches, false otherwise.</returns>
    public bool HasIdentifier(ReadOnlySpan<byte> expectedIdentifier)
        => this.Identifier.AsSpan().SequenceEqual(expectedIdentifier);
}
