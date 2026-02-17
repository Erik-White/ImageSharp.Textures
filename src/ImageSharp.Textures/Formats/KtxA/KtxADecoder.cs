// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Formats.KtxA;

/// <summary>
/// Image decoder for Apple KTX textures.
/// </summary>
public sealed class KtxADecoder : ITextureDecoder, IKtxADecoderOptions, ITextureInfoDetector
{
    /// <inheritdoc/>
    public Texture DecodeTexture(Configuration configuration, Stream stream)
    {
        Guard.NotNull(stream);

        return new KtxADecoderCore(configuration, this).DecodeTexture(stream);
    }

    /// <inheritdoc/>
    public ITextureInfo Identify(Configuration configuration, Stream stream)
    {
        Guard.NotNull(stream);

        return new KtxADecoderCore(configuration, this).Identify(stream);
    }
}
