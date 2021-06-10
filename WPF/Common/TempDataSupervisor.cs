using System;
using System.IO;

namespace WPF.Common
{
    internal class TempDataSupervisor : IDisposable
    {
        private readonly string _tempDirectoryName = "temp";
        private readonly string _tempDirectoryPath;

        public TempDataSupervisor()
        {
            _tempDirectoryPath = Path.Combine(Environment.CurrentDirectory, _tempDirectoryName);
        }

        public void Dispose()
        {
            DeleteDirectory(_tempDirectoryPath);
        }

        public void Start()
        {
            Directory.CreateDirectory(_tempDirectoryPath);
        }

        private static void DeleteDirectory(string directory)
        {
            var files = Directory.GetFiles(directory);
            var dirs = Directory.GetDirectories(directory);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directory, false);
        }
    }
}