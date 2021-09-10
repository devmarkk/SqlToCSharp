using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SqlToCSharp.Extensions;
using SqlToCSharp.Model;
using SqlToCSharp.Services;

namespace SqlToCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await Run())
                .GetAwaiter()
                .GetResult();
        }

        static async Task Run()
        {
            try
            {
                string connectionString = "Server=localhost;Database=Mofix-dev;User ID=marco;password=12345";
                string tableName = "Usuario";

                HandlerService handlerService = new(path: AppContext.BaseDirectory, tableName: tableName);

                IEnumerable<Column> columns = await handlerService.TryFetchColumns(connectionString);

                if (columns is not null)
                {
                    handlerService.SetColumns(columns);

                    await handlerService.GenerateFile();
                }
                else
                    foreach (var item in handlerService.ErrorMessages)
                    {
                        Console.WriteLine(item);
                    }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
