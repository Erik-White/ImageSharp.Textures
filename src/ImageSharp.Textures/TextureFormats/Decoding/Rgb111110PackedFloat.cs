// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Textures.TextureFormats.Decoding;

/// <summary>
/// Texture format for the VK_FORMAT_B10G11R11_UFLOAT_PACK32 packed pixel type.
/// Each pixel is a 32-bit unsigned integer containing three unsigned float channels:
/// R (11-bit, bits 0-10), G (11-bit, bits 11-21), B (10-bit, bits 22-31).
/// </summary>
internal readonly struct Rgb111110PackedFloat : IBlock<Rgb111110PackedFloat>
{
    /// <inheritdoc/>
    public int BitsPerPixel => 32;

    /// <inheritdoc/>
    public byte PixelDepthBytes => 4;

    /// <inheritdoc/>
    public byte DivSize => 1;

    /// <inheritdoc/>
    public byte CompressedBytesPerBlock => 4;

    /// <inheritdoc/>
    public bool Compressed => false;

    /// <inheritdoc/>
    public Image GetImage(byte[] blockData, int width, int height)
    {
        byte[] decompressedData = this.Decompress(blockData, width, height);
        return Image.LoadPixelData<Textures.PixelFormats.Rgba128Float>(decompressedData, width, height);
    }

    /// <inheritdoc/>
    public byte[] Decompress(byte[] blockData, int width, int height)
    {
        int pixelCount = width * height;
        byte[] output = new byte[pixelCount * 16];
        Span<byte> inputSpan = blockData.AsSpan();
        Span<byte> outputSpan = output.AsSpan();

        for (int i = 0; i < pixelCount; i++)
        {
            uint packed = BinaryPrimitives.ReadUInt32LittleEndian(inputSpan[(i * 4)..]);

            // Decode from Vulkan B10G11R11 format (exponent bias 15).
            float r = UnpackFloat11(packed & 0x7FFu);
            float g = UnpackFloat11((packed >> 11) & 0x7FFu);
            float b = UnpackFloat10((packed >> 22) & 0x3FFu);

            int outOffset = i * 16;
            BinaryPrimitives.WriteSingleLittleEndian(outputSpan[outOffset..], r);
            BinaryPrimitives.WriteSingleLittleEndian(outputSpan[(outOffset + 4)..], g);
            BinaryPrimitives.WriteSingleLittleEndian(outputSpan[(outOffset + 8)..], b);
            BinaryPrimitives.WriteSingleLittleEndian(outputSpan[(outOffset + 12)..], 1.0f);
        }

        return output;
    }

    /// <summary>
    /// Unpacks an 11-bit unsigned float (5-bit exponent bias 15, 6-bit mantissa) to IEEE float32.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float UnpackFloat11(uint value)
    {
        uint e = (value >> 6) & 0x1Fu;
        uint m = value & 0x3Fu;

        if (e == 0)
        {
            if (m == 0)
            {
                return 0f;
            }

            // Denormalized: m * 2^(1 - 15 - 6) = m * 2^(-20)
            return m * BitConverter.UInt32BitsToSingle(107u << 23);
        }

        if (e == 31)
        {
            uint ieee = (0xFFu << 23) | (m << 17);
            return BitConverter.UInt32BitsToSingle(ieee);
        }

        // Normalized: 2^(e-15) * (1 + m/64), bias delta = 127 - 15 = 112
        return BitConverter.UInt32BitsToSingle(((e + 112u) << 23) | (m << 17));
    }

    /// <summary>
    /// Unpacks a 10-bit unsigned float (5-bit exponent bias 15, 5-bit mantissa) to IEEE float32.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float UnpackFloat10(uint value)
    {
        uint e = (value >> 5) & 0x1Fu;
        uint m = value & 0x1Fu;

        if (e == 0)
        {
            if (m == 0)
            {
                return 0f;
            }

            // Denormalized: m * 2^(1 - 15 - 5) = m * 2^(-19)
            return m * BitConverter.UInt32BitsToSingle(108u << 23);
        }

        if (e == 31)
        {
            uint ieee = (0xFFu << 23) | (m << 18);
            return BitConverter.UInt32BitsToSingle(ieee);
        }

        // Normalized: 2^(e-15) * (1 + m/32), bias delta = 127 - 15 = 112
        return BitConverter.UInt32BitsToSingle(((e + 112u) << 23) | (m << 18));
    }
}
