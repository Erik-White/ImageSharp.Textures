// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Compression.Astc;

/// <summary>
/// Selects how the LDR decode path expands and outputs color (ASTC spec §C.2.19, §C.2.5).
/// </summary>
public enum LdrDecodeMode
{
    /// <summary>
    /// Linear decode. Each 8-bit endpoint component is bit-replicated to 16 bits before interpolation.
    /// </summary>
    Linear,

    /// <summary>
    /// sRGB decode. The R, G, and B endpoint components are expanded, alpha is unchanged.
    /// </summary>
    Srgb,
}
