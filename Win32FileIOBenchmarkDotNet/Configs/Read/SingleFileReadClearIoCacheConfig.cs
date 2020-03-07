using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Win32FileIOBenchmarkDotNet.Benchmarks.Read;
using Win32FileIOBenchmarkDotNet.Configs.Common;

namespace Win32FileIOBenchmarkDotNet.Configs.Read
{
    /// <summary>
    /// Implement class with benchmark settings to test <see cref="SingleReadFileClearIoCacheBenchmark"/>
    /// </summary>
    public class SingleFileReadClearIoCacheConfig : CommonConfig
    {
        #region Constructor

        public SingleFileReadClearIoCacheConfig()
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
