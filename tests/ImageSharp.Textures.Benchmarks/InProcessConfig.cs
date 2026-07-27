// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace SixLabors.ImageSharp.Textures.Benchmarks;

internal class InProcessConfig : ManualConfig
{
    public InProcessConfig() => this.AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.Instance));
}
