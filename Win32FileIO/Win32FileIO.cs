using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Win32FileIO
{
    public unsafe class WinFileIO : IDisposable
    {
        // This class provides the capability to utilize the ReadFile and Writefile windows IO functions.  These functions
        // are the most efficient way to perform file I/O from C# or even C++.  The constructor with the buffer and buffer
        // size should usually be called to init this class.  PinBuffer is provided as an alternative.  The reason for this
        // is because a pointer needs to be obtained before the ReadFile or WriteFile functions are called.
        //
        // Error handling - In each public function of this class where an error can occur, an ApplicationException is
        // thrown with the Win32Exception message info if an error is detected.  If no exception is thrown, then a normal
        // return is considered success.
        // 
        // This code is not thread safe.  Thread control primitives need to be added if running this in a multi-threaded
        // environment.
        //
        // The recommended and fastest function for reading from a file is to call the ReadBlocks method.
        // The recommended and fastest function for writing to a file is to call the WriteBlocks method.
        //
        // License and disclaimer:
        // This software is free to use by any individual or entity for any endeavor for profit or not.
        // Even though this code has been tested and automated unit tests are provided, there is no gaurantee that
        // it will run correctly with your system or environment.  I am not responsible for any failure and you agree
        // that you accept any and all risk for using this software.
        //
        //
        // Written by Robert G. Bryan in Feb, 2011.
        //
        // Constants required to handle file I/O:

        #region Private fields

        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint OPEN_EXISTING = 3;
        private const uint CREATE_ALWAYS = 2;
        private const int BlockSize = 65536;


        /// <summary>
        /// Handle to GCHandle object used to pin the I/O buffer in memory.
        /// </summary>
        private GCHandle _gchBuf;

        /// <summary>
        /// Handle to the file to be read from or written to.
        /// </summary>
        private IntPtr _pHandle;

        /// <summary>
        /// Pointer to the buffer used to perform I/O.
        /// </summary>
        private void* _pBuffer;

        #endregion

        #region Dll imports

        // Define the Windows system functions that are called by this class via COM Interop:
        [DllImport("kernel32", SetLastError = true)]
        static extern unsafe IntPtr CreateFile
        (
            string FileName,          // file name
            uint DesiredAccess,       // access mode
            uint ShareMode,           // share mode
            uint SecurityAttributes,  // Security Attributes
            uint CreationDisposition, // how to create
            uint FlagsAndAttributes,  // file attributes
            int hTemplateFile         // handle to template file
        );

        [DllImport("kernel32", SetLastError = true)]
        static extern unsafe bool ReadFile
        (
            IntPtr hFile,      // handle to file
            void* pBuffer,            // data buffer
            int NumberOfBytesToRead,  // number of bytes to read
            int* pNumberOfBytesRead,  // number of bytes read
            int Overlapped            // overlapped buffer which is used for async I/O.  Not used here.
        );

        [DllImport("kernel32", SetLastError = true)]
        static extern unsafe bool WriteFile
        (
            IntPtr handle,                     // handle to file
            void* pBuffer,             // data buffer
            int NumberOfBytesToWrite,    // Number of bytes to write.
            int* pNumberOfBytesWritten,// Number of bytes that were written..
            int Overlapped                     // Overlapped buffer which is used for async I/O.  Not used here.
        );

        [DllImport("kernel32", SetLastError = true)]
        static extern unsafe bool CloseHandle
        (
            IntPtr hObject     // handle to object
        );

        #endregion

        #region Constructors

        public WinFileIO()
            => _pHandle = IntPtr.Zero;

        /// <summary>
        /// This constructor is provided so that the buffer can be pinned in memory.
        /// Cleanup must be called in order to unpin the buffer.
        /// </summary>
        /// <param name="buffer">Buffer used to perform IO operations.</param>
        public WinFileIO(Array buffer)
        {
            PinBuffer(buffer);
            _pHandle = IntPtr.Zero;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// This function must be called to pin the buffer in memory before any file I/O is done.
        /// This shows how to pin a buffer in memory for an extended period of time without using
        /// the "Fixed" statement.  Pinning a buffer in memory can take some cycles, so this technique
        /// is helpful when doing quite a bit of file I/O.
        /// </summary>
        /// <param name="buffer">Buffer to pin.</param>
        public void PinBuffer(Array buffer)
        {
            // Make sure we don't leak memory if this function was called before and the UnPinBuffer was not called.
            UnpinBuffer();
            _gchBuf = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var pAddr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            // pBuffer is the pointer used for all of the I/O functions in this class.
            _pBuffer = pAddr.ToPointer();
        }

        /// <summary>
        /// This function unpins the buffer and needs to be called before a new buffer is pinned or
        /// when disposing of this object.  It does not need to be called directly since the code in Dispose
        /// or PinBuffer will automatically call this function.
        /// </summary>
        public void UnpinBuffer()
        {
            if (_gchBuf.IsAllocated)
                _gchBuf.Free();
        }

        /// <summary>
        /// This function uses the Windows API CreateFile function to open an existing file.
        /// A return value of true indicates success.
        /// </summary>
        /// <param name="fileName">Full path to file.</param>
        public void OpenForReading(string fileName)
        {
            Close();
            _pHandle = CreateFile(fileName, GENERIC_READ, 0, 0, OPEN_EXISTING, 0, 0);

            if (_pHandle == IntPtr.Zero)
            {
                throw new ApplicationException($"WinFileIO:OpenForReading - Could not open file {fileName} - {new Win32Exception().Message}");
            }
        }

        /// <summary>
        /// This function uses the Windows API CreateFile function to open an existing file.
        /// If the file exists, it will be overwritten.
        /// </summary>
        /// <param name="fileName">Full path to file.</param>
        public void OpenForWriting(string fileName)
        {
            Close();
            _pHandle = CreateFile(fileName, GENERIC_WRITE, 0, 0, CREATE_ALWAYS, 0, 0);

            if (_pHandle == IntPtr.Zero)
            {
                throw new ApplicationException($"WinFileIO:OpenForWriting - Could not open file {fileName} - {new Win32Exception().Message}");
            }
        }

        /// <summary>
        /// This function reads in a file up to BytesToRead using the Windows API function ReadFile.  The return value
        /// is the number of bytes read.
        /// </summary>
        /// <param name="bytesToRead"></param>
        /// <returns>Number of bytes read.</returns>
        public int Read(int bytesToRead)
        {
            var bytesRead = 0;

            if (!ReadFile(_pHandle, _pBuffer, bytesToRead, &bytesRead, 0))
            {
                throw new ApplicationException($"WinFileIO:Read - Error occurred reading a file. - {new Win32Exception().Message}");
            }

            return bytesRead;
        }

        /// <summary>
        /// This function reads in chunks at a time instead of the entire file.  Make sure the file is <= 2GB.
        /// Also, if the buffer is not large enough to read the file, then an ApplicationException will be thrown.
        /// No check is made to see if the buffer is large enough to hold the file.  If this is needed, then
        /// use the ReadBlocks function below.
        /// </summary>
        /// <returns></returns>
        public int ReadUntilEOF()
        {
            int bytesReadInBlock = 0, bytesRead = 0;
            var pBuf = (byte*)_pBuffer;
            // Do until there are no more bytes to read or the buffer is full.
            for (; ; )
            {
                if (!ReadFile(_pHandle, pBuf, BlockSize, &bytesReadInBlock, 0))
                {
                    // This is an error condition.  The error msg can be obtained by creating a Win32Exception and
                    // using the Message property to obtain a description of the error that was encountered.
                    throw new ApplicationException($"WinFileIO:ReadUntilEOF - Error occurred reading a file. - {new Win32Exception().Message}");
                }

                if (bytesReadInBlock == 0)
                    break;

                bytesRead += bytesReadInBlock;
                pBuf += bytesReadInBlock;
            }

            return bytesRead;
        }

        /// <summary>
        /// This function reads a total of BytesToRead at a time.  There is a limit of 2gb per call.
        /// </summary>
        /// <param name="bytesToRead">Block size to read.</param>
        /// <returns>Total readed bytes.</returns>
        public int ReadBlocks(int bytesToRead)
        {
            int bytesReadInBlock = 0, bytesRead = 0;
            var pBuf = (byte*)_pBuffer;
            // Do until there are no more bytes to read or the buffer is full.
            do
            {
                var blockByteSize = Math.Min(BlockSize, bytesToRead - bytesRead);

                if (!ReadFile(_pHandle, pBuf, blockByteSize, &bytesReadInBlock, 0))
                {
                    throw new ApplicationException($"WinFileIO:ReadBytes - Error occurred reading a file. - {new Win32Exception().Message}");
                }

                if (bytesReadInBlock == 0)
                    break;

                bytesRead += bytesReadInBlock;
                pBuf += bytesReadInBlock;
            } while (bytesRead < bytesToRead);

            return bytesRead;
        }

        /// <summary>
        /// Writes out the file in one swoop using the Windows WriteFile function.
        /// </summary>
        /// <param name="bytesToWrite">Number bytes to write.</param>
        /// <returns>Total writed bytes.</returns>
        public int Write(int bytesToWrite)
        {
            int numberOfBytesWritten;

            if (!WriteFile(_pHandle, _pBuffer, bytesToWrite, &numberOfBytesWritten, 0))
            {
                throw new ApplicationException($"WinFileIO:Write - Error occurred writing a file. - {new Win32Exception().Message}");
            }

            return numberOfBytesWritten;
        }

        /// <summary>
        /// This function writes out chunks at a time instead of the entire file.  This is the fastest write function,
        /// perhaps because the block size is an even multiple of the sector size.
        /// </summary>
        /// <param name="numBytesToWrite">Block size to wtite.</param>
        /// <returns>Total writed bytes.</returns>
        public int WriteBlocks(int numBytesToWrite)
        {
            int bytesWritten = 0, bytesOutput = 0;
            var pBuf = (byte*)_pBuffer;
            var remainingBytes = numBytesToWrite;
            // Do until there are no more bytes to write.
            do
            {
                var bytesToWrite = Math.Min(remainingBytes, BlockSize);

                if (!WriteFile(_pHandle, pBuf, bytesToWrite, &bytesWritten, 0))
                {
                    // This is an error condition.  The error msg can be obtained by creating a Win32Exception and
                    // using the Message property to obtain a description of the error that was encountered.
                    throw new ApplicationException($"WinFileIO:WriteBlocks - Error occurred writing a file. - {new Win32Exception().Message}");
                }

                pBuf += bytesToWrite;
                bytesOutput += bytesToWrite;
                remainingBytes -= bytesToWrite;
            } while (remainingBytes > 0);

            return bytesOutput;
        }

        /// <summary>
        /// This function closes the file handle.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            var success = true;

            if (_pHandle != IntPtr.Zero)
            {
                success = CloseHandle(_pHandle);
                _pHandle = IntPtr.Zero;
            }

            return success;
        }

        #endregion

        #region Disposal pattern

        protected void Dispose(bool disposing)
        {
            // This function frees up the unmanaged resources of this class.
            Close();
            UnpinBuffer();
        }

        public void Dispose()
        {
            // This method should be called to clean everything up.
            Dispose(true);
            // Tell the GC not to finalize since clean up has already been done.
            GC.SuppressFinalize(this);
        }

        ~WinFileIO()
        {
            // Finalizer gets called by the garbage collector if the user did not call Dispose.
            Dispose(false);
        }

        #endregion
    }
}