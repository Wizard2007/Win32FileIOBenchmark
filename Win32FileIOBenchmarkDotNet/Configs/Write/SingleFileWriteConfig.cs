using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Win32FileIOBenchmarkDotNet.Benchmarks.Write;
using Win32FileIOBenchmarkDotNet.Configs.Common;

namespace Win32FileIOBenchmarkDotNet.Configs.Write
{
    /// <summary>
    /// Implement class with benchmark settings to test <see cref="SingleWriteFileBenchmark"/>
    /// </summary>
    public class SingleFileWriteConfig : CommonConfig
    {
        #region Constructor

        public SingleFileWriteConfig()
            => Add(
                Job.Default
                    .With(RunStrategy.ColdStart)
                    .With(Jit.RyuJit)
                    .With(Platform.X64)
                    .With(ClrRuntime.Net461)
                    .WithLaunchCount(10)
                    .WithIterationCount(1)
                    .WithWarmupCount(0)
            );

        #endregion
    }
}
