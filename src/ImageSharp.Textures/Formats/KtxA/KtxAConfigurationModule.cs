// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Registers the image encoders, decoders and mime type detectors for the Apple KTX format.
/// </summary>
public class KtxAConfigurationModule : IConfigurationModule
{
    /// <inheritdoc/>
    public void Configure(Configuration configuration)
    {
        configuration.ImageFormatsManager.SetDecoder(KtxAFormat.Instance, new KtxADecoder());
        configuration.ImageFormatsManager.AddImageFormatDetector(new KtxAImageFormatDetector());
    }
}
