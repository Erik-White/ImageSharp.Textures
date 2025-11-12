// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using AstcSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities.ImageComparison;
using PixelRgba = SixLabors.ImageSharp.PixelFormats.Rgba32;
using TestUtilEnv = SixLabors.ImageSharp.Textures.Tests.TestUtilities.TestEnvironment;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc;

[Trait("Format", "Astc")]
public class AstcImageComparisonTests
{
    public static IEnumerable<object[]> AstcTestFiles()
    {
        string inputDir = Path.Combine(TestUtilEnv.InputImagesDirectoryFullPath, "Astc");
        string expectedDir = Path.Combine(TestUtilEnv.SolutionDirectoryFullPath, "tests", "Images", "Expected", "ASTC");

        if (!Directory.Exists(inputDir) || !Directory.Exists(expectedDir))
        {
            yield break;
        }

        foreach (string astc in Directory.GetFiles(inputDir, "*.astc"))
        {
            string baseName = Path.GetFileNameWithoutExtension(astc);
            string expected = Path.Combine(expectedDir, baseName + ".bmp");
            if (File.Exists(expected))
            {
                yield return new object[] { astc, expected };
            }
        }
    }

#nullable enable
    [Theory]
    [MemberData(nameof(AstcTestFiles))]
    public void DecodeAstcFiles_ProducesExpectedImages(string inputPath, string expectedPath)
    {
        using var expected = Image.Load<PixelRgba>(expectedPath);
        byte[] inputData = File.ReadAllBytes(inputPath);
        var inputFile = AstcFile.LoadFromMemory(inputData, out string? errorMessage);
        int stride = expected.Width * 4;
        byte[] decodedBuffer = new byte[stride * expected.Height];

        Assert.NotNull(inputFile);
        Assert.Null(errorMessage);

        Codec.DecompressToImage(inputFile, decodedBuffer, decodedBuffer.Length, stride);
        using var actual = Image.LoadPixelData<PixelRgba>(decodedBuffer, expected.Width, expected.Height);
        // Reading the buffer manually will result in x,y order instead of row,column
        actual.Mutate(x => x.RotateFlip(RotateMode.Rotate180, FlipMode.Horizontal));

        // Compare exact
        // Use a tighter tolerant comparer threshold to enforce more accurate decoding.
        var comparer = ImageComparer.TolerantPercentage(0.1f);
        ImageSimilarityReport<PixelRgba, PixelRgba> report = comparer.CompareImages(expected, actual);

        if (!report.IsEmpty)
        {
            // Save actual image for debugging
            string outDir = TestUtilEnv.CreateOutputDirectory("ASTC");
            string outFile = Path.Combine(outDir, Path.GetFileName(inputPath) + ".actual.png");
            actual.SaveAsPng(outFile);

            // Also save a small text report
            string reportFile = Path.ChangeExtension(outFile, ".diff.txt");
            File.WriteAllText(reportFile, report.ToString());
        }

        Assert.True(report.IsEmpty, $"Image difference too large for {Path.GetFileName(inputPath)} -> {report.DifferencePercentageString}");
    }
}
