using LibraLoan.Core.Abstraction;
using System;

namespace LibraLoan.Data
{
    internal class ConnectionStringFactory : IConnectionStringFactory
    {
        public string GetConnectionString()
        {
            return @$"Data Source = {_directoriesService.DatabasePath};Password=fEr4uLNHktXtHcx;";
        }

        private readonly IDirectoriesService _directoriesService;

        public ConnectionStringFactory(IDirectoriesService directoriesService)
        {
            _directoriesService = directoriesService;
        }
    }
}
