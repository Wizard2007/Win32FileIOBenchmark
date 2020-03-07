using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Configs.Read;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Read
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test average file read without clean up IO cache
    /// </summary>
    [Config(typeof(AverageFileReadConfig))]
    public class AverageReadFileBenchmark : CommonReadFileBenchmark
    {
        #region Constructor

        public AverageReadFileBenchmark()
            => ClassName = typeof(AverageReadFileBenchmark).Name;

        #endregion
    }
}