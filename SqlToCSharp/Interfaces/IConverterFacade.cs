using SqlToCSharp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToCSharp.Interfaces
{
    public interface IConverterFacade
    {
        Task GenerateFile();

        Task<IEnumerable<Column>> FetchColumns(string connectionString);

        StringBuilder BuildFile();
    }
}
