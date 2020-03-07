using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Win32FileIOBenchmarkDotNet.Benchmarks.Read;
using Win32FileIOBenchmarkDotNet.Configs.Common;

namespace Win32FileIOBenchmarkDotNet.Configs.Read
{
    /// <summary>
    /// Implement class with benchmark settings to test <see cref="AverageReadFileBenchmark"/>
    /// </summary>
    public class AverageFileReadConfig : CommonConfig
    {
        #region Constructor

        public AverageFileReadConfig()
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
