using LibraLoan.Core.Abstraction;
using System.IO;
using System;

namespace LibraLoan.Services
{
    internal class DirectoriesService : IDirectoriesService
    {
        public string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LibraLoan");

        public string DataPath => Path.Combine(AppDataPath, "Data");

        public string DatabasePath
        {
            get
            {
#if DEBUG
                return Path.Combine(DataPath, "LibraLoan.dev.db");
#else
                return Path.Combine(DataPath, "LibraLoan.db");
#endif
            }
        }

        public void CreateDirectories()
        {
            Directory.CreateDirectory(AppDataPath);
            Directory.CreateDirectory(DataPath);
        }

        public void CreateDatabase()
        {
            if (!File.Exists(DatabasePath))
            {
                string sourceDatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", "LibraLoan.db");
                File.Copy(sourceDatabasePath, DatabasePath);
            }
        }
    }
}
