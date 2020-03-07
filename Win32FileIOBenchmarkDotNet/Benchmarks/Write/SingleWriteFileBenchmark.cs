using BenchmarkDotNet.Attributes;
using Win32FileIOBenchmarkDotNet.Configs.Write;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Write
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test single file write without IO cache cean up
    /// </summary>
    [Config(typeof(SingleFileWriteConfig))]
    public class SingleWriteFileBenchmark : CommonWriteFileBenchmark
    {
        #region Constructor

        public SingleWriteFileBenchmark()
            => ClassName = typeof(SingleWriteFileBenchmark).Name;

        #endregion
    }
}
