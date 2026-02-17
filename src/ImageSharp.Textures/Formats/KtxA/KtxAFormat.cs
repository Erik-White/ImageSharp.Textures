// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Registers the texture decoders and mime type detectors for the Apple KTX format.
/// </summary>
public sealed class KtxAFormat : ITextureFormat
{
    /// <summary>
    /// Prevents a default instance of the <see cref="KtxAFormat"/> class from being created.
    /// </summary>
    private KtxAFormat()
    {
    }

    /// <summary>
    /// Gets the current instance.
    /// </summary>
    public static KtxAFormat Instance { get; } = new KtxAFormat();

    /// <inheritdoc/>
    public string Name => "Apple KTX";

    /// <inheritdoc/>
    public string DefaultMimeType => "image/ktx";

    /// <inheritdoc/>
    public IEnumerable<string> MimeTypes => KtxAConstants.FileExtensions;

    /// <inheritdoc/>
    public IEnumerable<string> FileExtensions => KtxAConstants.FileExtensions;
}
