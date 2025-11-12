// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System;
using System.IO;
using System.Linq;
using System.Text;
using SixLabors.ImageSharp.Textures.Tests.TestUtilities;
using Xunit;

namespace SixLabors.ImageSharp.Textures.Tests.Formats.Astc
{
//     [Trait("Format", "Astc")]
//     public class AstcAppliedPixelsComparisonTests
//     {
//         private static string AstInputDir => Path.Combine(TestEnvironment.InputImagesDirectoryFullPath, "Astc");
//         private static string BaselineDir => Path.Combine(TestEnvironment.BaselineDirectoryFullPath, "AstcApplied");
//         private static string ActualDir => Path.Combine(TestEnvironment.ActualOutputDirectoryFullPath, "AstcApplied");

//         [Theory]
//         [InlineData("atlas_small_8x8.astc", 8, 8)]
//         [InlineData("footprint_10x5.astc", 10, 5)]
//         [InlineData("footprint_12x10.astc", 12, 10)]
//         [InlineData("footprint_10x10.astc", 10, 10)]
//         public void AppliedPixels_MatchBaseline(string fileName, int blockW, int blockH)
//         {
//             Directory.CreateDirectory(ActualDir);

//             string path = Path.Combine(AstInputDir, fileName);
//             Assert.True(File.Exists(path), "ASTC input file not found: " + path);

//             byte[] data = File.ReadAllBytes(path);
//             var pixels = SixLabors.ImageSharp.Textures.TextureFormats.Decoding.AstcDecoder.ExtractAppliedColorPixelsForTests(data.AsSpan(0, 16), blockW, blockH);
//             Assert.NotNull(pixels);

//             string baseName = Path.GetFileNameWithoutExtension(fileName);
//             string baselinePath = Path.Combine(BaselineDir, baseName + ".raw");
//             string actualPath = Path.Combine(ActualDir, baseName + ".raw");

//             // Write actual for debugging purposes
//             File.WriteAllBytes(actualPath, pixels);

//             if (File.Exists(baselinePath))
//             {
//                 byte[] expected = File.ReadAllBytes(baselinePath);
//                 if (!expected.SequenceEqual(pixels))
//                 {
//                     // Build a helpful diagnostic message with mismatches and ranges
//                     int expectedLen = expected.Length;
//                     int actualLen = pixels.Length;
//                     int maxLen = Math.Max(expectedLen, actualLen);

//                     var mismatchIndices = new System.Collections.Generic.List<int>();
//                     for (int i = 0; i < maxLen; i++)
//                     {
//                         byte e = i < expectedLen ? expected[i] : (byte)0xFF;
//                         byte a = i < actualLen ? pixels[i] : (byte)0xFF;
//                         if (i >= expectedLen || i >= actualLen || e != a)
//                         {
//                             mismatchIndices.Add(i);
//                         }
//                     }

//                     // Coalesce contiguous indices into ranges
//                     var ranges = new System.Collections.Generic.List<(int start, int end)>();
//                     foreach (var idx in mismatchIndices)
//                     {
//                         if (ranges.Count == 0 || idx != ranges.Last().end + 1)
//                         {
//                             ranges.Add((idx, idx));
//                         }
//                         else
//                         {
//                             var last = ranges.Last();
//                             ranges[ranges.Count - 1] = (last.start, idx);
//                         }
//                     }

//                     var sb = new StringBuilder();
//                     sb.AppendLine($"Applied pixel mismatch for {fileName}.");
//                     sb.AppendLine($"Baseline: {baselinePath}");
//                     sb.AppendLine($"Actual:   {actualPath}");
//                     sb.AppendLine($"Baseline length: {expectedLen} bytes");
//                     sb.AppendLine($"Actual length:   {actualLen} bytes");
//                     sb.AppendLine($"Total mismatching byte positions: {mismatchIndices.Count}");

//                     if (ranges.Count > 0)
//                     {
//                         sb.AppendLine("Mismatch ranges (start..end):");
//                         sb.AppendLine(string.Join(", ", ranges.Select(r => r.start == r.end ? r.start.ToString() : r.start + ".." + r.end)));
//                     }

//                     // Show up to 10 sample mismatches with expected vs actual hex
//                     int samples = Math.Min(10, mismatchIndices.Count);
//                     if (samples > 0)
//                     {
//                         sb.AppendLine("Sample mismatches (index: expected -> actual):");
//                         for (int i = 0; i < samples; i++)
//                         {
//                             int idx = mismatchIndices[i];
//                             string eStr = idx < expectedLen ? expected[idx].ToString("X2") : "--";
//                             string aStr = idx < actualLen ? pixels[idx].ToString("X2") : "--";
//                             sb.AppendLine($"  {idx}: {eStr} -> {aStr}");
//                         }
//                     }

//                     // Provide a small hex context around the first mismatch
//                     if (mismatchIndices.Count > 0)
//                     {
//                         int ctxIdx = mismatchIndices[0];
//                         int ctxStart = Math.Max(0, ctxIdx - 8);
//                         int ctxEnd = Math.Min(maxLen - 1, ctxIdx + 8);
//                         sb.AppendLine($"Hex context around first mismatch (offset {ctxIdx}):");
//                         var ctxLines = new System.Collections.Generic.List<string>();
//                         for (int i = ctxStart; i <= ctxEnd; i++)
//                         {
//                             string eStr = i < expectedLen ? expected[i].ToString("X2") : "--";
//                             string aStr = i < actualLen ? pixels[i].ToString("X2") : "--";
//                             string marker = i == ctxIdx ? "*" : " ";
//                             ctxLines.Add($"{marker}{i:D4}: {eStr} | {aStr}");
//                         }
//                         sb.AppendLine(string.Join(Environment.NewLine, ctxLines));
//                     }

//                     throw new Exception(sb.ToString());
//                 }
//             }
//             else
//             {
//                 // Create baseline directory and populate baseline for future runs
//                 Directory.CreateDirectory(Path.GetDirectoryName(baselinePath)!);
//                 File.Copy(actualPath, baselinePath);
//             }
//         }
//     }
}
