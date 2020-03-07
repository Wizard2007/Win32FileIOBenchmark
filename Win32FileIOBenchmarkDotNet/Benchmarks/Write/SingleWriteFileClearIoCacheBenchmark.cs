using BenchmarkDotNet.Attributes;
using System.IO;
using Win32FileIOBenchmarkDotNet.Configs.Write;
using Win32FileIOBenchmarkDotNet.Helpers;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Write
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test single file read with IO cache cean up
    /// </summary>
    [Config(typeof(SingleFileWriteClearIoCacheConfig))]
    public class SingleWriteFileClearIoCacheBenchmark : CommonWriteFileBenchmark
    {
        #region Constructor

        public SingleWriteFileClearIoCacheBenchmark()
            => ClassName = typeof(SingleWriteFileClearIoCacheBenchmark).Name;

        #endregion

        #region Iteration setup

        [IterationSetup]
        public void ClearFilesCache()
        {
            using (var fileIoHelper = new FileIOHelper())
            {
                foreach (var fileFullName in Directory.GetFiles($@"{DataPath}\{ClassName}", "*.txt", SearchOption.AllDirectories))
                {
                    fileIoHelper.FlushIOCacheW(fileFullName);
                }
            }
        }

        #endregion
    }
}
