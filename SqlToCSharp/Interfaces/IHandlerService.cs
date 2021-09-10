using SqlToCSharp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToCSharp.Interfaces
{
    public interface IHandlerService
    {
        Task GenerateFile();

        Task<IEnumerable<Column>> TryFetchColumns(string connectionString);

        StringBuilder BuildFile();
    }
}
