using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Win32FileIOBenchmarkDotNet.Helpers
{
    public class FileIOHelper: IDisposable
    {
        #region Private fields

        private IntPtr _pHandle;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;

        private const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        private const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;

        private const uint OPEN_EXISTING = 3;
        private const uint CREATE_ALWAYS = 2;

        private const uint FILE_SHARE_READ = 0x00000001;
        private const uint FILE_SHARE_WRITE = 0x00000002;
        private const uint FILE_SHARE_DELETE = 0x00000004;

        #endregion

        #region Public methods

        public void FlushIOCacheW(string fileName)
        {
            OpenForWriting(fileName);
            Close();
        }

        #endregion

        #region Private methods 

        private void OpenForWriting(string fileName)
        {
            // This function uses the Windows API CreateFile function to open an existing file.
            // If the file exists, it will be overwritten.
            Close();
            _pHandle = CreateFile(fileName, GENERIC_WRITE, 0, 0, CREATE_ALWAYS | OPEN_EXISTING, FILE_FLAG_NO_BUFFERING, 0);

            if (_pHandle == IntPtr.Zero)
            {
                throw new ApplicationException($"WinFileIO:OpenForWriting - Could not open file {fileName} - {new Win32Exception().Message}");
            }
        }

        private bool Close()
        {
            // This function closes the file handle.
            var  success = true;

            if (_pHandle != IntPtr.Zero)
            {
                success = CloseHandle(_pHandle);
                _pHandle = IntPtr.Zero;
            }

            return success;
        }

        #endregion

        #region import from DLLs

        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr CreateFile
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
        static extern bool CloseHandle
        (
             IntPtr hObject     // handle to object
        );

        #endregion

        #region Disposal pattern

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Close();
                }

                _disposedValue = true;
            }
        }

        ~FileIOHelper() 
            => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
