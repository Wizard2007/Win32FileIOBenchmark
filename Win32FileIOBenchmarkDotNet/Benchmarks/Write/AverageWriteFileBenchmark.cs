using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Configs.Write;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Write
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test average file write without clean up IO cache
    /// </summary>
    [Config(typeof(AverageFileWriteConfig))]
    public class AverageWriteFileBenchmark : CommonWriteFileBenchmark
    {
        #region Constructor

        public AverageWriteFileBenchmark()
            => ClassName = typeof(AverageWriteFileBenchmark).Name;

        #endregion
    }
}