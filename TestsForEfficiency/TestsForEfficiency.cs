using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Win32FileIO;

namespace TestsForEfficiency
{
    /// <summary>
    /// This class is used to benchmark different I/O methods in order to determine which one is the
    /// most efficient.
    ///
    /// Written by Robert G. Bryan in Feb, 2011.
    /// </summary>
    public class FileEfficientTests : IDisposable
    {

        #region Private fields

        /// <summary>
        /// Buffer used to read in unicode data from files in the file I/O tests.
        /// </summary>
        private char[] _charBuf;

        /// <summary>
        /// Buffer used to read in byte data from files in the file I/O tests.
        /// </summary>
        private static byte[] _byteBuf;

        /// <summary>
        /// Buffer used to verify that the TestFileStreamRead7 ran correctly.
        /// </summary>
        private static byte[] _byteBufVer;

        /// <summary>
        /// Size of file I/O buffers.
        /// </summary>
        private const int BufSize = 60000000;

        /// <summary>
        /// The max amount of data read in at any one time.
        /// </summary>
        private const int BufSizeM1M = BufSize - 1000000;

        /// <summary>
        /// The block size used for the asynch tests.
        /// </summary>
        private const int AsyncBufSize = 131072;

        /// <summary>
        /// Size of block used to read/write files.
        /// </summary>
        private const int BlockSize = 65536;

        /// <summary>
        /// The file stream used for the asynch tests.
        /// </summary>
        private static FileStream _asyncFs;

        /// <summary>
        /// Event flag used to notify when an asynch test has completed.
        /// </summary>
        private static ManualResetEvent _waitReadAsync;

        /// <summary>
        /// Event flag used to notify when an asynch test has completed.
        /// </summary>
        private static ManualResetEvent _waitSignal;

        /// <summary>
        /// Used in the async tests to calculate where to place the next block of data.
        /// </summary>
        private static int _bufIndex;

        /// <summary>
        ///  Counts the bytes read in TestFileStreamRead7.
        /// </summary>
        private static int _bufCount;

        /// <summary>
        /// Object is required to convert between byte[] & String.
        /// </summary>
        private static UTF8Encoding _utf8;

        /// <summary>
        /// End of line marker, used to parse a text file.
        /// </summary>
        private const byte Cr = 13;

        /// <summary>
        /// The object that implements the windows readfile and writefile system functions.
        /// </summary>
        private readonly WinFileIO _wfio;

        #endregion

        #region Constructor

        public FileEfficientTests()
        {
            _charBuf = new char[BufSizeM1M];
            _byteBuf = new byte[BufSizeM1M];
            _byteBufVer = new byte[BufSizeM1M];
            _wfio = new WinFileIO(_byteBuf);
            _waitReadAsync = new ManualResetEvent(false);
            _waitSignal = new ManualResetEvent(true);
            _utf8 = new UTF8Encoding();
            StringsContents = new Dictionary<string, string[]>();
            StringContents = new Dictionary<string, string>();
            BytesInFiles = new Dictionary<string, int>();
        }

        #endregion

        #region Disposal pattern

        protected void Dispose(bool disposing)
        {
            // This function should be called in order to clean everything up.  Usually the Close method should be
            // called to close the file and the 
            if (disposing)
            {
                _wfio.Dispose();
            }
        }

        public void Dispose()
        {
            // This method should be called to clean everything up.
            Dispose(true);
            // Tell the GC not to finalize since clean up has already been done.
            GC.SuppressFinalize(this);
        }

        ~FileEfficientTests()
            // Destructor gets called by the garbage collector if the user did not call Dispose.
            => Dispose(false);

        #endregion

        #region Public properties

        public Dictionary<string, string[]> StringsContents { get; set; }

        public Dictionary<string, string> StringContents { get; set; }

        /// <summary>
        /// Holds the number of bytes that are in each file.
        /// </summary>
        public Dictionary<string, int> BytesInFiles { get; set; }

        #endregion

        #region Read file functions

        /// <summary>
        /// This function tests out how quickly the File.ReadAllLines method returns all the lines in a file
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadAllLines(string pathToFile)
            => File.ReadAllLines(pathToFile, Encoding.UTF8);

        /// <summary>
        /// This function tests out how quickly the File.ReadAllText method returns all text in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadAllText(string pathToFile)
            => File.ReadAllText(pathToFile, Encoding.UTF8);

        /// <summary>
        /// using the ReadAllBytes function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadAllBytes(string pathToFile)
            => File.ReadAllBytes(pathToFile);

        /// <summary>
        /// This function tests reading data from a file with the BinaryReader class.  This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestBinaryReader1(string pathToFile)
        {
            using (var brFile = new BinaryReader(File.Open(pathToFile, FileMode.Open)))
            {
                brFile.Read(_charBuf, 0, BufSizeM1M);
                brFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader1(string pathToFile)
        {
            using (var srFile = new StreamReader(pathToFile))
            {
                srFile.Read(_charBuf, 0, BufSizeM1M);
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader2(string pathToFile)
        {
            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                srFile.Read(_charBuf, 0, BufSizeM1M);
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The ReadBlock function is tested here.
        /// Get how fast it takes to read in the .683MB, 10MB, and 50MB files into memory.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader3(string pathToFile)
        {
            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                srFile.ReadBlock(_charBuf, 0, BufSizeM1M);
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The ReadToEnd function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader4(string pathToFile)
        {
            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                srFile.ReadToEnd();
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here
        /// using a smaller buffer size and reading into memory just BlockSize bytes each time until the entire file is
        /// read into memory.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader5(string pathToFile)
        {
            int blockLoop = 0, numChars = 0;

            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                while (srFile.Peek() >= 0)
                {
                    // Documentation says to use less than the buffer size for best performance, but not how much less.
                    numChars += srFile.Read(_charBuf, 0, 64000);
                    blockLoop++;
                }

                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The ReadLine function is tested here
        /// using the ReadLine function.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader6(string pathToFile)
        {
            string line;
            var lineCount = 0;

            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                while (srFile.Peek() >= 0)
                {
                    // Documentation says to use less than the buffer size for best performance, but not how much less.
                    line = srFile.ReadLine();
                    lineCount++;
                }
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.  The Read function is tested here.
        /// The difference beween this function and TestStreamReader2 which uses the exact same code for reading the file in
        /// is that this function parses the bufffer into individual lines so that a comparision can be made between
        /// this method and TestStreamReader6, which does the parsing for us to determine which method is the most
        /// efficient.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader7(string pathToFile)
        {
            string line;
            int nextIndex, lastIndex, lineCount, len;

            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                srFile.Read(_charBuf, 0, BufSizeM1M);
                srFile.Close();
            }

            lastIndex = lineCount = 0;
            // Parse each line in the buffer by looking for the <CR> char at the end of each line.
            for (; ; )
            {
                nextIndex = Array.IndexOf(_charBuf, '\r', lastIndex);
                if (nextIndex == -1)
                    break;
                len = nextIndex - lastIndex;
                line = new string(_charBuf, lastIndex, len);
                lastIndex = nextIndex + 2;
                lineCount++;
            }
        }

        /// <summary>
        // This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        // This first test reads in the entire file using the Sequential Scan option in the constructor.  No parsing of
        // lines is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead1(string pathToFile)
        {
            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, BlockSize, FileOptions.SequentialScan))
            {
                fsFile.Read(_byteBuf, 0, BufSizeM1M);
                fsFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the Sequential Scan option in the constructor.  The lines are
        /// parsed into strings.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead2(string pathToFile)
        {
            string line;
            int nextIndex, lastIndex, lineCount, len;

            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, BlockSize, FileOptions.SequentialScan))
            {
                fsFile.Read(_byteBuf, 0, BufSizeM1M);
                fsFile.Close();
            }

            lastIndex = lineCount = 0;
            // Parse each line in the buffer by looking for the <CR> char at the end of each line.
            for (; ; )
            {
                nextIndex = Array.IndexOf(_byteBuf, Cr, lastIndex);

                if (nextIndex == -1)
                    break;

                len = nextIndex - lastIndex;
                line = _utf8.GetString(_byteBuf, lastIndex, len);
                lastIndex = nextIndex + 2;
                lineCount++;
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the file using the same buffer size as TestFileStreamRead5 so that a true comparison can be
        /// made when parsing the file into strings using multiple reads.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead2A(string pathToFile)
        {
            string line;
            int numChars, nextIndex, lastIndex, lineCount, len;

            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, AsyncBufSize, FileOptions.SequentialScan))
            {
                numChars = 0;

                for (; ; )
                {
                    len = fsFile.Read(_byteBuf, numChars, AsyncBufSize);
                    numChars += len;

                    if (len < AsyncBufSize)
                        break;
                }

                fsFile.Close();
            }

            lastIndex = lineCount = 0;
            // Parse each line in the buffer by looking for the <CR> char at the end of each line.
            for (; ; )
            {
                nextIndex = Array.IndexOf(_byteBuf, Cr, lastIndex);

                if (nextIndex == -1)
                    break;

                len = nextIndex - lastIndex;
                line = _utf8.GetString(_byteBuf, lastIndex, len);
                lastIndex = nextIndex + 2;
                lineCount++;
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The Read function is tested here.
        /// This test reads in the entire file using the RandomAccess option in the constructor.  No parsing is done here.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead3(string pathToFile)
        {
            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, BlockSize, FileOptions.RandomAccess))
            {
                fsFile.Read(_byteBuf, 0, BufSizeM1M);
                fsFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The async BeginRead function is tested here.
        /// This test reads in each file asynchronously reading AsyncBufSize bytes at a time.  No parsing is done.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead4(string pathToFile)
        {
            _waitReadAsync.Reset();
            _asyncFs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, AsyncBufSize, FileOptions.Asynchronous);
            _bufIndex = 0;

            var aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, EndReadCallback4, 0);

            // The code should wait until the rest of the file is read in via EndReadCallback4.  a 15 sec timeout is used
            // in case there is a problem.
            _waitReadAsync.WaitOne(15000, false);
        }

        public static void EndReadCallback4(IAsyncResult asyncResult)
        {
            // This is the async call back function used by BeginRead.  The data is returned via the EndRead function.
            // This function implements a lock that prevents more than 1 read from being processed at the same time.
            // The idea is to execute the next read and then process the data while reading in the next block.
            // In this test, there is no processing of data in order to compare with other tests that do the same thing.
            int numChars;
            IAsyncResult aResult;
            var callBack = new AsyncCallback(EndReadCallback4);

            lock (_waitReadAsync)
            {
                numChars = _asyncFs.EndRead(asyncResult);

                if (numChars > 0)
                {
                    if (numChars == AsyncBufSize)
                    {
                        _bufIndex += numChars;
                        aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, callBack, 0);
                    }
                }

                if (numChars < AsyncBufSize)
                {
                    _asyncFs.Close();
                    _asyncFs.Dispose();
                    _waitReadAsync.Set();
                }
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.  The async BeginRead function is tested
        /// here. This test reads in each file asynchronously reading AsyncBufSize bytes at a time.  The file is parsed while
        /// the next block is read.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead5(string pathToFile)
        {
            _byteBuf[AsyncBufSize] = Cr;
            _waitReadAsync.Reset();
            _asyncFs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, AsyncBufSize, FileOptions.Asynchronous);
            _bufIndex = 0;

            var aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, new AsyncCallback(EndReadCallback5), 0);

            _waitReadAsync.WaitOne(30000, false);
        }

        public static void EndReadCallback5(IAsyncResult asyncResult)
        {
            // This is the async call back function used by BeginRead.  The data is returned via the EndRead function.
            // This function implements a lock that prevents more than 1 read from being processed at the same time.
            // The idea is to execute the next read and then process the data while reading in the next block.
            // In this test, there is parsing of data in order to compare with other tests that do the same thing.
            int numChars, nextIndex, len, lastIndex, lineCount;
            String line;
            IAsyncResult aResult;
            var callBack = new AsyncCallback(EndReadCallback5);

            lock (_waitReadAsync)
            {
                numChars = _asyncFs.EndRead(asyncResult);

                if (numChars > 0)
                {
                    if (numChars == AsyncBufSize)
                    {
                        lastIndex = _bufIndex;
                        _bufIndex += numChars;
                        aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, callBack, 0);
                        lineCount = 0;
                        // Parse each line in the buffer by looking for the <CR> char at the end of each line.
                        for (; ; )
                        {
                            nextIndex = Array.IndexOf(_byteBuf, Cr, lastIndex);
                            if ((nextIndex == -1) || (nextIndex >= _bufIndex))
                                break;
                            len = nextIndex - lastIndex;
                            line = _utf8.GetString(_byteBuf, lastIndex, len);
                            lastIndex = nextIndex + 2;
                            lineCount++;
                        }
                    }
                }

                if (numChars < AsyncBufSize)
                {
                    _asyncFs.Close();
                    _asyncFs.Dispose();
                    _waitReadAsync.Set();
                }
            }
        }

        /// <summary>
        /// This test is the same as TestFileStreamRead5, with the only difference being that the callback function uses
        /// a ManualResetEvent object for controlling thread access instead of a lock statement.
        /// The async BeginRead function is tested here. This test reads in each file asynchronously reading
        /// AsyncBufSize bytes at a time.  The file is parsed while the next block is read.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead6(string pathToFile)
        {
            _waitReadAsync.Reset();
            _asyncFs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, AsyncBufSize, FileOptions.Asynchronous);
            _bufIndex = 0;

            var aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, EndReadCallback6, 0);

            _waitReadAsync.WaitOne(300000, false);
        }

        public static void EndReadCallback6(IAsyncResult asyncResult)
        {
            // This is the async call back function used by BeginRead.  The data is returned via the EndRead function.
            // This function implements a lock that prevents more than 1 read from being processed at the same time.
            // The idea is to execute the next read and then process the data while reading in the next block.
            // In this test, there is parsing of data in order to compare with other tests that do the same thing.
            int numChars, nextIndex, len, lastIndex, lineCount;
            String line;
            IAsyncResult aResult;
            var callBack = new AsyncCallback(EndReadCallback6);

            numChars = _asyncFs.EndRead(asyncResult);
            _waitSignal.WaitOne();
            _waitSignal.Reset();

            if (numChars > 0)
            {
                if (numChars == AsyncBufSize)
                {
                    lastIndex = _bufIndex;
                    _bufIndex += numChars;
                    aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, callBack, 0);
                    lineCount = 0;
                    // Parse each line in the buffer by looking for the <CR> char at the end of each line.
                    for (; ; )
                    {
                        nextIndex = Array.IndexOf(_byteBuf, Cr, lastIndex);
                        if ((nextIndex == -1) || (nextIndex >= _bufIndex))
                            break;
                        len = nextIndex - lastIndex;
                        line = _utf8.GetString(_byteBuf, lastIndex, len);
                        lastIndex = nextIndex + 2;
                        lineCount++;
                    }
                }
            }

            if (numChars < AsyncBufSize)
            {
                _asyncFs.Close();
                _asyncFs.Dispose();
                _waitReadAsync.Set();
            }
            // Allow the next thread to go through.
            _waitSignal.Set();
        }

        /// <summary>
        /// This test removes the lock at the top of the callback function, which means that multiple threads will
        /// execute simultaneously which should in theory yield better performance.  The last argument of BeginRead
        /// specifies the index to the buffer where data should be processed.
        /// The async BeginRead function is tested here. This test reads in each file asynchronously reading
        /// AsyncBufSize bytes at a time.  No parsing is done in this test.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead7(string pathToFile)
        {
            // Read in the same file using the sync method of FileStream to determine if this test ran correctly.
            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, BlockSize, FileOptions.RandomAccess))
            {
                fsFile.Read(_byteBufVer, 0, BufSizeM1M);
                fsFile.Close();
            }

            _waitReadAsync.Reset();
            _asyncFs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, AsyncBufSize, FileOptions.Asynchronous);
            _bufIndex = _bufCount = 0;

            var aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, EndReadCallback7, _bufIndex);

            _waitReadAsync.WaitOne(300000, false);
        }

        public static void EndReadCallback7(IAsyncResult asyncResult)
        {
            // This is the async call back function used by BeginRead.  The data is returned via the EndRead function.
            // Unlike the other tests, this callback function allows multiple threads to be processed simultaneously.
            // 
            // The idea is to execute the next read and then process the data while reading in the next block.
            // In this test, there is parsing of data in order to compare with other tests that do the same thing.
            int numChars, index, nextIndex, fileByteCount;
            // int Loop;
            IAsyncResult aResult;
            var callBack = new AsyncCallback(EndReadCallback7);

            numChars = _asyncFs.EndRead(asyncResult);
            fileByteCount = Interlocked.Add(ref _bufCount, numChars);
            index = (int)asyncResult.AsyncState;

            if (numChars > 0)
            {
                if (numChars == AsyncBufSize)
                {
                    nextIndex = index + AsyncBufSize;
                    aResult = _asyncFs.BeginRead(_byteBuf, nextIndex, AsyncBufSize, callBack, nextIndex);
                }
            }
            // By using a stack variable instead of a class variable (that is used by all the threads)
            // a lock statement does not have to be used to single thread here.
            if (fileByteCount >= _asyncFs.Length)
            {
                /* This code is currently commented out since it would interfere with the timing of this test.
                // Its primary purpose is to show that locks are not required to correctly read a file into memory
                // using an asynch method that uses multiple threads.
                //
                // Verify that the file read in matches the verification buffer.
                if (FileByteCount != AsyncFS.Length)
                  BufCount = 0; // Set a breakpoint on this line to detect an error.
                for (Loop = 0; Loop < FileByteCount; Loop++)
                {
                  if (ByteBuf[Loop] != ByteBufVer[Loop])
                    BufCount = 0; // Set a breakpoint on this line to detect an error.
                }
                */
                _asyncFs.Close();
                _asyncFs.Dispose();
                _waitReadAsync.Set();
            }
        }

        /// <summary>
        /// This test removes the lock at the top of the callback function, which means that multiple threads will
        /// execute simultaneously which should in theory yield better performance.  The last argument of BeginRead
        /// specifies the index to the buffer where data should be placed.  When EndRead is called in the callback
        /// function, it obtains this value in order to calculate the next index where data will be placed.
        /// The async BeginRead function is tested here. This test reads in each file asynchronously reading
        /// AsyncBufSize bytes at a time.  The file is parsed while the next block is read.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead8(string pathToFile)
        {
            _waitReadAsync.Reset();
            _asyncFs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read,
                FileShare.None, AsyncBufSize, FileOptions.Asynchronous);
            _bufIndex = _bufCount = 0;
            var aResult = _asyncFs.BeginRead(_byteBuf, _bufIndex, AsyncBufSize, EndReadCallback8, _bufIndex);
            _waitReadAsync.WaitOne(300000, false);
        }

        public static void EndReadCallback8(IAsyncResult asyncResult)
        {
            // This is the async call back function used by BeginRead.  The idea is to execute the next read and then
            // process the data while reading in the next block.  In this test, there is parsing of data in order to
            // compare with other tests that do the same thing.  The data is returned via the EndRead function.
            // Unlike the other tests, this callback function allows multiple threads to be processed simultaneously.
            // The reason it can be done like this without any problems is because the index to the buffer is passed by
            // BeginRead in the last argument.
            // The only place where a lock is required is when calculating BufCount.  The Interlocked.Add method is used
            // to obtain the index to place data at on the next read.
            // To verify that this test works as intended, uncomment the code before the AsyncFS.Close() statement and
            // set a breakpoint on each error condition test.
            int numChars, index, nextIndex, len, lastIndex, lineCount, fileByteCount;
            // int Loop;
            String line;
            IAsyncResult aResult;
            var callBack = new AsyncCallback(EndReadCallback8);

            numChars = _asyncFs.EndRead(asyncResult);
            fileByteCount = Interlocked.Add(ref _bufCount, numChars);
            index = (int)asyncResult.AsyncState;

            if (numChars > 0)
            {
                if (numChars == AsyncBufSize)
                {
                    nextIndex = index + AsyncBufSize;
                    aResult = _asyncFs.BeginRead(_byteBuf, nextIndex, AsyncBufSize, callBack, nextIndex);
                }

                lineCount = 0;
                lastIndex = index;
                // Parse each line in the buffer by looking for the <CR> char at the end of each line.
                for (; ; )
                {
                    nextIndex = Array.IndexOf(_byteBuf, Cr, lastIndex);
                    if ((nextIndex == -1) || (nextIndex >= index + AsyncBufSize))
                        break;
                    len = nextIndex - lastIndex;
                    line = _utf8.GetString(_byteBuf, lastIndex, len);
                    lastIndex = nextIndex + 2;
                    lineCount++;
                }
            }
            // By using a stack variable instead of a class variable (that is used by all the threads)
            // a lock statement does not have to be used to single thread here.
            if (fileByteCount >= _asyncFs.Length)
            {
                /* This code is currently commented out since it would interfere with the timing of this test.
                // Its primary purpose is to show that locks are not required to correctly read a file into memory
                // using an asynch method that uses multiple threads.
                //
                // Verify that the file read in matches the verification buffer.
                if (FileByteCount != AsyncFS.Length)
                  BufCount = 0; // Set a breakpoint on this line to detect an error.
                for (Loop = 0; Loop < FileByteCount; Loop++)
                {
                  if (ByteBuf[Loop] != ByteBufVer[Loop])
                    BufCount = 0; // Set a breakpoint on this line to detect an error.
                }
                */
                _asyncFs.Close();
                _asyncFs.Dispose();
                _waitReadAsync.Set();
            }
        }

        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This function tests the ReadFile function
        /// by reading in the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadFileWinApi1(string pathToFile)
        {
            _wfio.OpenForReading(pathToFile);
            _wfio.Read(BufSizeM1M);
            _wfio.Close();
        }

        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadFileWinApi2(string pathToFile)
        {
            _wfio.OpenForReading(pathToFile);
            _wfio.ReadUntilEOF();
            _wfio.Close();
        }

        /// <summary>
        /// This function tests out the Windows API function ReadFile.  This test tests out the ReadUntilEOF method
        /// in the WinFileIO class.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadFileWinApi3(string pathToFile)
        {
            _wfio.OpenForReading(pathToFile);
            _wfio.ReadBlocks(BufSizeM1M);
            _wfio.Close();
        }

        /// <summary>
        /// This function tests reading data from a file with the BinaryReader class.
        /// This function is exactly the same as TestBinaryReader1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestBinaryReader1NoOpenClose(string pathToFile)
        {
            using (var brFile = new BinaryReader(File.Open(pathToFile, FileMode.Open)))
            {
                brFile.Read(_charBuf, 0, BufSizeM1M);
                brFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the StreamReader class.
        /// This function is exactly the same as TestStreamReader2, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamReader2NoOpenClose(string pathToFile)
        {
            using (var srFile = new StreamReader(pathToFile, Encoding.UTF8, false, BlockSize))
            {
                srFile.Read(_charBuf, 0, BufSizeM1M);
                srFile.Close();
            }
        }

        /// <summary>
        /// This function tests reading data from a file with the FileStream class.
        /// This function is exactly the same as TestFileStreamRead1, except it measures the time taken after
        /// opening the file and before closing it.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamRead1NoOpenClose(string pathToFile)
        {
            using (var fsFile = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.None, BlockSize, FileOptions.SequentialScan))
            {
                fsFile.Read(_byteBuf, 0, BufSizeM1M);
                fsFile.Close();
            }
        }

        /// <summary>
        /// This function is similar to TestReadFileWinAPI, except that it times only the reading of the file.
        /// </summary>
        /// <param name="pathToFile">Path to file</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestReadFileWinApiNoOpenClose(string pathToFile)
        {
            _wfio.OpenForReading(pathToFile);
            _wfio.Read(BufSizeM1M);
            _wfio.Close();
        }

        #endregion

        #region Write file functions

        /// <summary>
        /// This function tests out how quickly the File.WriteAllLines method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestWriteAllLines(string pathToFile, string[] content)
            => File.WriteAllLines(pathToFile, content, Encoding.UTF8);

        /// <summary>
        /// This function tests out how quickly the File.WriteAllText method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="content">Content to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestWriteAllText(string pathToFile, string content)
            => File.WriteAllText(pathToFile, content, Encoding.UTF8);

        /// <summary>
        /// This function tests out how quickly the File.WriteAllBytes method returns all the lines in a file.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestWriteAllBytes(string pathToFile)
            => File.WriteAllBytes(pathToFile, _byteBuf);

        /// <summary>
        /// This function tests writing data from a file with the BinaryReader class.
        /// This function uses a char buf.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestBinaryWriter1(string pathToFile, int bytesInFile)
        {
            using (var bwFile = new BinaryWriter(File.Open(pathToFile, FileMode.Create)))
            {
                bwFile.Write(_byteBuf, 0, bytesInFile);
                bwFile.Close();
            }
        }

        /// <summary>
        /// This function tests writing data to a file with the StreamReader class.
        /// The write function is tested here.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestStreamWriter1(string pathToFile, int bytesInFile)
        {
            using (var swFile = new StreamWriter(pathToFile))
            {
                swFile.Write(_charBuf, 0, bytesInFile);
                swFile.Close();
            }
        }

        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This first test writes out the file with the FileOptions set to none.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamWrite1(string pathToFile, int bytesInFile)
        {
            using (var fsFile = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None, BlockSize, FileOptions.None))
            {
                fsFile.Write(_byteBuf, 0, bytesInFile);
                fsFile.Close();
            }
        }

        /// <summary>
        /// This function tests writing data to a file with the FileStream class.
        /// The write function is tested here.
        /// This test writes out the file with the FileOptions set to WriteThrough.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamWrite2(string pathToFile, int bytesInFile)
        {
            using (var fsFile = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None, BlockSize, FileOptions.WriteThrough))
            {
                fsFile.Write(_byteBuf, 0, bytesInFile);
                fsFile.Close();
            }
        }

        /// <summary>
        /// This function tests writing data to a file with the FileStream class.  The write function is tested here.
        /// This test involves writing out the file in 65536 byte chunks .vs. writing it out all at once.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestFileStreamWrite3(string pathToFile, int bytesInFile)
        {
            int bytesToWrite, bufIndex;

            using (var fsFile = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None, BlockSize, FileOptions.WriteThrough))
            {
                bufIndex = 0;

                do
                {
                    bytesToWrite = Math.Min(BlockSize, bytesInFile - bufIndex);
                    fsFile.Write(_byteBuf, bufIndex, bytesToWrite);
                    bufIndex += bytesToWrite;
                } while (bufIndex < bytesInFile);

                fsFile.Close();
            }
        }

        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the entire file with one call.
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestWriteFileWinApi1(string pathToFile, int bytesInFile)
        {
            _wfio.OpenForWriting(pathToFile);
            _wfio.Write(bytesInFile);
            _wfio.Close();
        }

        /// <summary>
        /// This function tests out the Windows API function WriteFile.  This function tests the WriteFile function
        /// by writing out the file in blocks. 
        /// </summary>
        /// <param name="pathToFile">Path to file.</param>
        /// <param name="bytesInFile">Bytes to write.</param>>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestWriteFileWinApi2(string pathToFile, int bytesInFile)
        {
            _wfio.OpenForWriting(pathToFile);
            _wfio.WriteBlocks(bytesInFile);
            _wfio.Close();
        }

        #endregion
    }
}
