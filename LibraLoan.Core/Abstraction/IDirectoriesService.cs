using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLoan.Core.Abstraction
{
    public interface IDirectoriesService
    {
        string AppDataPath { get; }
        string DataPath { get; }
        string DatabasePath { get; }

        void CreateDatabase();
        void CreateDirectories();
    }
}
