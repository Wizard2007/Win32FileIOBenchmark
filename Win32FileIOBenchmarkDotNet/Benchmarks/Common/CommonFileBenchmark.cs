using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using TestsForEfficiency;

namespace Win32FileIOBenchmarkDotNet.Benchmarks.Common
{
    /// <summary>
    /// Implement common functionality for IO benchmarks.
    /// </summary>
    public abstract class CommonFileBenchmark : IDisposable
    {
        #region Private fields

        /// <summary>
        /// Lock object to syncronize global setup action
        /// </summary>
        private readonly object _syncGlobalSetUp = new object();

        private string _dataPath;

        #endregion

        #region Constructor

        protected CommonFileBenchmark()
        {
            FileEfficientTests = new FileEfficientTests();
            DataPath = Assembly.GetExecutingAssembly().Location;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Root path to data source directory.
        /// </summary>
        public string DataPath
        {
            get => _dataPath;
            set
            {
                var n = value.LastIndexOf(@"bin\", StringComparison.Ordinal);
                _dataPath = value.Substring(0, n) + "data";
            }
        }

        /// <summary>
        /// Class name need to obtain correct nested class name in global setup actions. 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Short file name used in run benchmark. Used as a parameter in benchmarck
        /// </summary>
        public IEnumerable<string> ShortFileNames { get; set; }
        //=> new[] { "01 MB.txt", "10 MB.txt", "50 MB.txt" };

        /// <summary>
        /// Instance of class implemented all needed IO functions
        /// </summary>
        public FileEfficientTests FileEfficientTests { get; }

        #endregion

        #region Protected methods

        /// <summary>
        /// Produce full file name for each tested IO method
        /// </summary>
        /// <param name="shortFileName">Short file name.</param>
        /// <param name="methodName">Method name.</param>
        /// <returns>$@"{DataPath}\{ClassName}\{methodName}\{shortFileName}" as a full file name.</returns>
        protected string GetFullFileName(string shortFileName, [CallerMemberName] string methodName = "MyMethod")
            => $@"{DataPath}\{ClassName}\{methodName}\{shortFileName}";

        /// <summary>
        /// Copy files for each benchmark from common source directory to benchmark destination directory.
        /// Used in global setup actions.
        /// </summary>
        /// <param name="className">Benchmark class name.</param>
        /// <param name="fileSourceDirectory">Source directory.</param>
        protected void CopyFilesForBenchMarks(string className, string fileSourceDirectory)
        {
            lock (_syncGlobalSetUp)
            {
                var methods = GetType()
                    .GetMethods()
                    .Where(methodInfo => methodInfo.GetCustomAttributes(typeof(BenchmarkAttribute), true).Length > 0);

                foreach (var method in methods)
                {
                    foreach (var shortFileName in ShortFileNames)
                    {
                        var fileDirectory = $@"{DataPath}\{className}\{method.Name}";
                        var fullFileName = $@"{fileDirectory}\{shortFileName}";

                        if (!File.Exists(fullFileName))
                        {
                            if (!Directory.Exists(fileDirectory))
                            {
                                Directory.CreateDirectory(fileDirectory);
                            }

                            var sourceFileName = $@"{fileSourceDirectory}\{shortFileName}";

                            Console.WriteLine($"Copy '{sourceFileName}' to '{fullFileName}' ");
                            File.Copy(sourceFileName, fullFileName);
                        }
                    }
                }
            }
        }

        #endregion

        #region Benchmark Setup

        [GlobalSetup]
        public virtual void GlobalSetUp() => CopyFilesForBenchMarksGlobal();

        /// <summary>
        /// Prepare files to run benchmark. Copy to destination directory.
        /// </summary>
        protected abstract void CopyFilesForBenchMarksGlobal();

        #endregion

        #region Disposal pattern

        public void Dispose()
            => FileEfficientTests?.Dispose();

        #endregion
    }
}
