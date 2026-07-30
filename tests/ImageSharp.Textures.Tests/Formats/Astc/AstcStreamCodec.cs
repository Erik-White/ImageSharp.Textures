// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers.Binary;
using SixLabors.ImageSharp.Textures.Compression.Astc;
using SixLabors.ImageSharp.Textures.Compression.Astc.Core;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc;

/// <summary>
/// Test adapter that bridges the streaming <see cref="AstcDecoder"/> API to materialized arrays
/// </summary>
internal static class AstcStreamCodec
{
    public static byte[] DecodeLdr(ReadOnlySpan<byte> astcData, int width, int height, Footprint footprint)
    {
        using MemoryStream source = new(astcData.ToArray());
        using MemoryStream destination = new();
        AstcDecoder.DecompressImage(source, destination, width, height, footprint);

        return destination.ToArray();
    }

    public static byte[] DecodeLdr(ReadOnlySpan<byte> astcData, int width, int height, FootprintType footprint)
        => DecodeLdr(astcData, width, height, Footprint.FromFootprintType(footprint));

    public static float[] DecodeHdr(ReadOnlySpan<byte> astcData, int width, int height, Footprint footprint)
    {
        using MemoryStream source = new(astcData.ToArray());
        using MemoryStream destination = new();
        AstcDecoder.DecompressHdrImage(source, destination, width, height, footprint);

        return ToFloats(destination.GetBuffer(), (int)destination.Length);
    }

    public static float[] DecodeHdr(ReadOnlySpan<byte> astcData, int width, int height, FootprintType footprint)
        => DecodeHdr(astcData, width, height, Footprint.FromFootprintType(footprint));

    private static float[] ToFloats(byte[] bytes, int byteCount)
    {
        float[] values = new float[byteCount / sizeof(float)];
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = BinaryPrimitives.ReadSingleLittleEndian(bytes.AsSpan(i * sizeof(float), sizeof(float)));
        }

        return values;
    }
}
