using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Configs.Read;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Read
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test single file read without IO cache cean up
    /// </summary>
    [Config(typeof(SingleFileReadConfig))]
    public class SingleReadFileBenchmark : CommonReadFileBenchmark
    {
        #region Constructor

        public SingleReadFileBenchmark()
            => ClassName = typeof(SingleReadFileBenchmark).Name;

        #endregion
    }
}
