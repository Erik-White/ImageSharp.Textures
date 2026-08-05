// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Compression.Astc.Core;

#pragma warning disable SA1201 // Readability: keep each expansion strategy adjacent to the interface it implements.
#pragma warning disable SA1649 // Multiple small, tightly-related expansion strategies share one file.

/// <summary>
/// Linear LDR expansion (ASTC spec §C.2.19). Used for every channel in linear decode mode, for the
/// alpha channel in every mode, and for LDR channels borrowed by the HDR output path.
/// </summary>
internal readonly struct LinearExpand
{
    /// <summary>
    /// Expands an 8-bit component <paramref name="c"/> to its 16-bit form.
    /// </summary>
    /// <param name="c">The 8-bit component value.</param>
    /// <returns>The 16-bit expanded value.</returns>
    public static int Expand(int c) => (c << 8) | c;
}

/// <summary>
/// sRGB LDR expansion (ASTC spec §C.2.19). Used for the R, G, and B channels in sRGB decode mode.
/// </summary>
internal readonly struct SrgbExpand
{
    /// <summary>
    /// Expands an 8-bit component <paramref name="c"/> to its 16-bit form.
    /// </summary>
    /// <param name="c">The 8-bit component value.</param>
    /// <returns>The 16-bit expanded value.</returns>
    public static int Expand(int c) => (c << 8) | 0x80;
}

/// <summary>
/// An LDR decode mode's per-channel endpoint expansion (ASTC spec §C.2.19).
/// </summary>
internal interface ILdrColorMode
{
    /// <summary>
    /// Expands an 8-bit R, G, or B endpoint component to 16 bits.
    /// </summary>
    /// <param name="c">The 8-bit colour component value.</param>
    /// <returns>The 16-bit expanded value.</returns>
    public static abstract int ExpandColor(int c);

    /// <summary>
    /// Expands an 8-bit alpha endpoint component to 16 bits.
    /// </summary>
    /// <param name="c">The 8-bit alpha component value.</param>
    /// <returns>The 16-bit expanded value.</returns>
    public static abstract int ExpandAlpha(int c);
}

/// <summary>
/// Linear LDR decode mode
/// </summary>
internal readonly struct LinearMode : ILdrColorMode
{
    /// <inheritdoc />
    public static int ExpandColor(int c) => LinearExpand.Expand(c);

    /// <inheritdoc />
    public static int ExpandAlpha(int c) => LinearExpand.Expand(c);
}

/// <summary>
/// sRGB LDR decode mode: R, G, B use <see cref="SrgbExpand"/>; alpha stays <see cref="LinearExpand"/>.
/// ASTC spec §C.2.19, only the colour channels take the sRGB low byte.
/// </summary>
internal readonly struct SrgbMode : ILdrColorMode
{
    /// <inheritdoc />
    public static int ExpandColor(int c) => SrgbExpand.Expand(c);

    /// <inheritdoc />
    public static int ExpandAlpha(int c) => LinearExpand.Expand(c);
}

#pragma warning restore SA1201
#pragma warning restore SA1649
