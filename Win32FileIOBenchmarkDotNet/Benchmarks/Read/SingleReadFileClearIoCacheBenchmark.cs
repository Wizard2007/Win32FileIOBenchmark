using BenchmarkDotNet.Attributes;
using System.IO;
using Win32FileIOBenchmarkDotNet.Configs.Read;
using Win32FileIOBenchmarkDotNet.Helpers;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Read
{
    /// <inheritdoc />
    /// <summary>
    /// Implement benchmark to test single file write with IO cache cean up
    /// </summary>
    [Config(typeof(SingleFileReadClearIoCacheConfig))]
    public class SingleReadFileClearIoCacheBenchmark : CommonReadFileBenchmark
    {
        #region Constructor

        public SingleReadFileClearIoCacheBenchmark()
            => ClassName = typeof(SingleReadFileClearIoCacheBenchmark).Name;

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
