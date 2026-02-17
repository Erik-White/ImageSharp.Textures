// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Text.RegularExpressions;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Textures.Formats.KtxA;
using SixLabors.ImageSharp.Textures.Tests.Enums;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.Attributes;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.TextureProviders;
using SixLabors.ImageSharp.Textures.TextureFormats;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.KtxA;

[Trait("Format", "KtxA")]
public partial class KtxADecoderFlatTests
{
    private static readonly KtxADecoder KtxADecoder = new();

    [Theory]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4)]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4_Small)]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4_Medium)]
    [WithFile(TestTextureFormat.KtxA, TestTextureType.Flat, TestTextureTool.ToKtx, TestImages.KtxA.Rgba32_4x4_Large)]
    public void KtxADecoder_CanDecode_Rgba_4x4(TestTextureProvider provider)
    {
        using Texture texture = provider.GetTexture(KtxADecoder);
        provider.SaveTextures(texture);
        FlatTexture flatTexture = texture as FlatTexture;

        using Image firstMipMap = flatTexture.MipMaps[0].GetImage();

        (firstMipMap as Image<Rgba32>).CompareToReferenceOutput(provider, testOutputDetails: GetSourceFileSuffix(provider.InputFile));
    }

    private static string? GetSourceFileSuffix(string filePath)
    {
        Match match = GetSourceFileSuffix().Match(filePath);

        return match.Success ? match.Groups[1].Value : null;
    }

    [GeneratedRegex(@"-([a-z]+)\.ktx$")]
    private static partial Regex GetSourceFileSuffix();
}
