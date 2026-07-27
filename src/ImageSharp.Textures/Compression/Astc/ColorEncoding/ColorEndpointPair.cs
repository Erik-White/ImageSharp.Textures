// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.InteropServices;
using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Textures.Compression.Astc.ColorEncoding;

/// <summary>
/// A value-type discriminated union representing either an LDR or HDR color endpoint pair.
/// </summary>
[StructLayout(LayoutKind.Auto)]
internal readonly struct ColorEndpointPair
{
    public bool IsHdr { get; private init; }

    // LDR fields (used when IsHdr == false)
    public Rgba32 LdrLow { get; private init; }

    public Rgba32 LdrHigh { get; private init; }

    // HDR fields (used when IsHdr == true)
    public Rgba64 HdrLow { get; private init; }

    public Rgba64 HdrHigh { get; private init; }

    public bool AlphaIsLdr { get; private init; }

    public bool ValuesAreLns { get; private init; }

    public static ColorEndpointPair Ldr(Rgba32 low, Rgba32 high)
        => new() { IsHdr = false, LdrLow = low, LdrHigh = high };

    public static ColorEndpointPair Hdr(Rgba64 low, Rgba64 high, bool alphaIsLdr = false, bool valuesAreLns = true)
        => new() { IsHdr = true, HdrLow = low, HdrHigh = high, AlphaIsLdr = alphaIsLdr, ValuesAreLns = valuesAreLns };
}
