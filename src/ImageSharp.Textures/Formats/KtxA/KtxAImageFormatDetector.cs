// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Detects Apple KTX file headers.
/// </summary>
public sealed class KtxAImageFormatDetector : ITextureFormatDetector
{
    /// <inheritdoc/>
    public int HeaderSize => 8;

    /// <inheritdoc/>
    public ITextureFormat? DetectFormat(ReadOnlySpan<byte> header) => this.IsSupportedFileFormat(header) ? KtxAFormat.Instance : null;

    private bool IsSupportedFileFormat(ReadOnlySpan<byte> header)
    {
        if (header.Length >= this.HeaderSize)
        {
            ReadOnlySpan<byte> magicBytes = header[..8];
            return magicBytes.SequenceEqual(KtxAConstants.MagicBytes);
        }

        return false;
    }
}
