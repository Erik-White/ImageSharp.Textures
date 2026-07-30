// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using AstcEncoder;
using BenchmarkDotNet.Attributes;
using SixLabors.ImageSharp.Textures.Compression.Astc;
using SixLabors.ImageSharp.Textures.Compression.Astc.IO;
using SixLabors.ImageSharp.Textures.Tests.Formats.Astc.Reference;

namespace SixLabors.ImageSharp.Textures.Benchmarks;

/// <summary>
/// Head-to-head steady-state decode of this library against the ARM reference codec (astcenc)
/// over a range of footprints and content. The ARM context is allocated once in
/// <see cref="Setup"/> and reset between decodes so the per-invocation cost is just the decode,
/// matching the streaming API's reused-stream framing.
/// </summary>
[MemoryDiagnoser]
[Config(typeof(InProcessConfig))]
public class AstcReferenceDecoderBenchmark
{
    private AstcFile? astcFile;

    private AstcencContext armLdrContext;
    private AstcencContext armHdrContext;
    private byte[]? armLdrOutput;
    private byte[]? armHdrOutput;
    private byte[]? armBlocksCopy;

    // Reused streams so this library's benchmarks measure decode work, not allocation.
    private MemoryStream source = null!;
    private MemoryStream sink = null!;

    [Params("rgba-4x4", "rgba-8x8", "footprint-4x4", "footprint-12x12")]
    public string FileName { get; set; } = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        string path = Path.Combine(TestEnvironment.InputImagesDirectoryFullPath, "Astc", this.FileName + ".astc");
        this.astcFile = AstcFile.FromMemory(File.ReadAllBytes(path));

        (int blockX, int blockY) = AstcReferenceDecoder.ToBlockDimensions(this.astcFile.Footprint.Type);
        int pixelCount = this.astcFile.Width * this.astcFile.Height;

        this.armLdrOutput = new byte[pixelCount * 4];
        this.armHdrOutput = new byte[pixelCount * 4 * sizeof(ushort)]; // FP16 = 2 bytes per channel
        this.armBlocksCopy = this.astcFile.Blocks.ToArray();
        this.source = new MemoryStream(this.armBlocksCopy);
        this.sink = new MemoryStream(pixelCount * 4 * sizeof(float));

        this.armLdrContext = AstcReferenceDecoder.AllocDecodeContext(AstcencProfile.AstcencPrfLdr, blockX, blockY);
        this.armHdrContext = AstcReferenceDecoder.AllocDecodeContext(AstcencProfile.AstcencPrfHdr, blockX, blockY);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        AstcReferenceDecoder.FreeContext(this.armLdrContext);
        AstcReferenceDecoder.FreeContext(this.armHdrContext);
    }

    [Benchmark]
    public long ImageSharp_DecompressLdr()
    {
        AstcFile file = this.astcFile!;
        this.source.Position = 0;
        this.sink.SetLength(0);
        AstcDecoder.DecompressImage(this.source, this.sink, file.Width, file.Height, file.Footprint);
        return this.sink.Length;
    }

    [Benchmark]
    public long ImageSharp_DecompressHdr()
    {
        AstcFile file = this.astcFile!;
        this.source.Position = 0;
        this.sink.SetLength(0);
        AstcDecoder.DecompressHdrImage(this.source, this.sink, file.Width, file.Height, file.Footprint);
        return this.sink.Length;
    }

    [Benchmark]
    public byte[] ArmReference_DecompressLdr()
    {
        AstcFile file = this.astcFile!;
        AstcReferenceDecoder.DecompressLdrInto(this.armLdrContext, this.armBlocksCopy!, file.Width, file.Height, this.armLdrOutput!);
        AstcReferenceDecoder.ResetDecode(this.armLdrContext);
        return this.armLdrOutput!;
    }

    [Benchmark]
    public byte[] ArmReference_DecompressHdr()
    {
        AstcFile file = this.astcFile!;
        AstcReferenceDecoder.DecompressHdrInto(this.armHdrContext, this.armBlocksCopy!, file.Width, file.Height, this.armHdrOutput!);
        AstcReferenceDecoder.ResetDecode(this.armHdrContext);
        return this.armHdrOutput!;
    }
}
