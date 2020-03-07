using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Win32FileIOBenchmarkDotNet.Benchmarks.Write;
using Win32FileIOBenchmarkDotNet.Configs.Common;

namespace Win32FileIOBenchmarkDotNet.Configs.Write
{
    /// <summary>
    /// Implement class with benchmark settings to test <see cref="AverageWriteFileBenchmark"/>
    /// </summary>
    public class AverageFileWriteConfig : CommonConfig
    {
        #region Constructor

        public AverageFileWriteConfig()
            => Add(
                Job.Default
                    .With(RunStrategy.Monitoring)
                    .With(Jit.RyuJit)
                    .With(Platform.X64)
                    .With(ClrRuntime.Net461)
                    .WithIterationCount(10)
                    .WithLaunchCount(1)
                    .WithWarmupCount(1)
            );

        #endregion
    }
}
