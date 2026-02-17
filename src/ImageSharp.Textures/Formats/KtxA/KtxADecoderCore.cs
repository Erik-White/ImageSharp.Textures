// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Textures.Common.Exceptions;
using SixLabors.ImageSharp.Textures.Formats.Ktx;

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Performs the Apple KTX decoding operation.
/// </summary>
internal sealed class KtxADecoderCore
{
    /// <summary>
    /// The global configuration.
    /// </summary>
    private readonly Configuration configuration;

    /// <summary>
    /// Used for allocating memory during processing operations.
    /// </summary>
    private readonly MemoryAllocator memoryAllocator;

    /// <summary>
    /// The file header containing general information about the texture.
    /// </summary>
    private KtxAHeader header;

    /// <summary>
    /// The texture decoder options.
    /// </summary>
    private readonly IKtxADecoderOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="KtxADecoderCore"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="options">The options.</param>
    public KtxADecoderCore(Configuration configuration, IKtxADecoderOptions options)
    {
        this.configuration = configuration;
        this.memoryAllocator = configuration.MemoryAllocator;
        this.options = options;
    }

    /// <summary>
    /// Decodes the texture from the specified stream.
    /// </summary>
    /// <param name="stream">The stream where the texture should be decoded from.</param>
    /// <returns>The decoded texture.</returns>
    public Texture DecodeTexture(Stream stream)
    {
        // Validate magic bytes
        ThrowIfInvalidMagic(stream);

        // Parse chunked structure
        KtxAChunk? headChunk = null;
        KtxAChunk? lzfsChunk = null;

        while (stream.Position < stream.Length)
        {
            byte[] chunkHeaderBuffer = new byte[KtxAConstants.ChunkHeaderSize];
            int bytesRead = stream.Read(chunkHeaderBuffer, 0, KtxAConstants.ChunkHeaderSize);

            if (bytesRead < KtxAConstants.ChunkHeaderSize)
            {
                break;
            }

            KtxAChunk chunk = KtxAChunk.Parse(chunkHeaderBuffer, stream.Position);

            if (chunk.HasIdentifier(KtxAConstants.HeadChunkIdentifier))
            {
                headChunk = chunk;
            }
            else if (chunk.HasIdentifier(KtxAConstants.LzfsChunkIdentifier))
            {
                lzfsChunk = chunk;
            }

            // Skip over chunk data
            stream.Position += chunk.Size;
        }

        if (!headChunk.HasValue)
        {
            throw new TextureFormatException("Apple KTX file is missing required HEAD chunk");
        }

        if (!lzfsChunk.HasValue)
        {
            throw new TextureFormatException("Apple KTX file is missing required LZFS chunk");
        }

        // Read and parse HEAD chunk
        stream.Position = headChunk.Value.DataOffset;
        byte[] headData = new byte[headChunk.Value.Size];
        stream.Read(headData, 0, headData.Length);
        this.header = KtxAHeader.Parse(headData);

        if (this.header.Width == 0)
        {
            throw new UnknownTextureFormatException("Width cannot be 0");
        }

        int width = (int)this.header.Width;
        int height = (int)this.header.Height;

        // Read and decompress LZFS chunk
        // Skip 4 bytes after the chunk header
        stream.Position = lzfsChunk.Value.DataOffset + 4;
        byte[] compressedData = new byte[lzfsChunk.Value.Size - 4];
        stream.Read(compressedData, 0, compressedData.Length);

        // Calculate expected decompressed size based on ASTC format
        // ASTC blocks are always 128 bits (16 bytes) regardless of block dimensions
        // We need to determine block size from the format
        (int blockWidth, int blockHeight) = GetAstcBlockSize(this.header.InternalFormat);
        int blocksX = (width + blockWidth - 1) / blockWidth;
        int blocksY = (height + blockHeight - 1) / blockHeight;
        int expectedDecompressedSize = blocksX * blocksY * 16; // 16 bytes per ASTC block

        byte[] decompressedData;
        try
        {
            // Allocate buffer with some extra space in case the calculation is slightly off
            decompressedData = new byte[expectedDecompressedSize + 1024];
            int actualSize = LzfseSharp.LzfseDecoder.Decompress(decompressedData, compressedData);

            // Trim to actual size
            if (actualSize != decompressedData.Length)
            {
                Array.Resize(ref decompressedData, actualSize);
            }
        }
        catch (Exception ex)
        {
            throw new TextureFormatException("Failed to decompress LZFSE data in Apple KTX file", ex);
        }

        // Process the decompressed ASTC data
        return new KtxAProcessor(this.header).ProcessTexture(decompressedData, width, height);
    }

    /// <summary>
    /// Reads the raw texture information from the specified stream.
    /// </summary>
    /// <param name="currentStream">The stream containing texture data.</param>
    /// <returns>Texture information.</returns>
    public ITextureInfo Identify(Stream currentStream)
    {
        ThrowIfInvalidMagic(currentStream);

        // Find and parse HEAD chunk
        while (currentStream.Position < currentStream.Length)
        {
            byte[] chunkHeaderBuffer = new byte[KtxAConstants.ChunkHeaderSize];
            int bytesRead = currentStream.Read(chunkHeaderBuffer, 0, KtxAConstants.ChunkHeaderSize);

            if (bytesRead < KtxAConstants.ChunkHeaderSize)
            {
                break;
            }

            KtxAChunk chunk = KtxAChunk.Parse(chunkHeaderBuffer, currentStream.Position);

            if (chunk.HasIdentifier(KtxAConstants.HeadChunkIdentifier))
            {
                byte[] headData = new byte[chunk.Size];
                currentStream.Read(headData, 0, headData.Length);
                this.header = KtxAHeader.Parse(headData);

                TextureInfo textureInfo = new(
                    new TextureTypeInfo((int)this.header.Depth),
                    (int)this.header.Width,
                    (int)this.header.Height);

                return textureInfo;
            }

            // Skip over chunk data
            currentStream.Position += chunk.Size;
        }

        throw new TextureFormatException("Apple KTX file is missing required HEAD chunk");
    }

    /// <summary>
    /// Validates the Apple KTX magic bytes.
    /// </summary>
    /// <param name="stream">The stream to validate.</param>
    private static void ThrowIfInvalidMagic(Stream stream)
    {
        byte[] magicBytes = new byte[KtxAConstants.MagicBytes.Length];
        int bytesRead = stream.Read(magicBytes, 0, magicBytes.Length);

        if (bytesRead < magicBytes.Length)
        {
            throw new UnknownTextureFormatException("Stream is too short to be an Apple KTX file");
        }

        if (!magicBytes.AsSpan().SequenceEqual(KtxAConstants.MagicBytes))
        {
            throw new UnknownTextureFormatException("Invalid Apple KTX magic bytes");
        }
    }

    private static (int Width, int Height) GetAstcBlockSize(GlInternalPixelFormat format) => format switch
    {
        GlInternalPixelFormat.CompressedRgbaAstc4x4Khr => (4, 4),
        GlInternalPixelFormat.CompressedRgbaAstc5x4Khr => (5, 4),
        GlInternalPixelFormat.CompressedRgbaAstc5x5Khr => (5, 5),
        GlInternalPixelFormat.CompressedRgbaAstc6x5Khr => (6, 5),
        GlInternalPixelFormat.CompressedRgbaAstc6x6Khr => (6, 6),
        GlInternalPixelFormat.CompressedRgbaAstc8x5Khr => (8, 5),
        GlInternalPixelFormat.CompressedRgbaAstc8x6Khr => (8, 6),
        GlInternalPixelFormat.CompressedRgbaAstc8x8Khr => (8, 8),
        GlInternalPixelFormat.CompressedRgbaAstc10x5Khr => (10, 5),
        GlInternalPixelFormat.CompressedRgbaAstc10x6Khr => (10, 6),
        GlInternalPixelFormat.CompressedRgbaAstc10x8Khr => (10, 8),
        GlInternalPixelFormat.CompressedRgbaAstc10x10Khr => (10, 10),
        GlInternalPixelFormat.CompressedRgbaAstc12x10Khr => (12, 10),
        GlInternalPixelFormat.CompressedRgbaAstc12x12Khr => (12, 12),
        _ => throw new ArgumentException("Unsupported ASTC format in Apple KTX file: " + format)
    };
}
