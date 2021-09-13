using Dapper;
using SqlToCSharp.Abstractions;
using SqlToCSharp.Extensions;
using SqlToCSharp.Interfaces;
using SqlToCSharp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SystemPath = System.IO.Path;

namespace SqlToCSharp.Services
{
    public class ConverterFacade : ConverterBase, IConverterFacade
    {
        public ConverterFacade(
            string path,
            string tableName)
            : base(path, tableName)
        {
        }

        public async Task GenerateFile()
        {
            StringBuilder builder = BuildFile();

            await File.WriteAllTextAsync(
                path: GetFilePath(),
                contents: builder.ToString());
        }

        public async Task<IEnumerable<Column>> FetchColumns(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT 
                                          COLUMN_NAME AS Name,
                                          DATA_TYPE AS DataType
	                                 FROM INFORMATION_SCHEMA.COLUMNS
	                                 WHERE TABLE_NAME = @tableName
	                                 ORDER BY ORDINAL_POSITION";

                    object parameters = new
                    {
                        TableName
                    };

                    return await connection.QueryAsync<Column>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);

                return null;
            }
        }

        private string GetFilePath()
        {
            string fileName = string.Concat(TableName, ".cs");

            return SystemPath.Combine(Path, fileName);
        }

        public StringBuilder BuildFile()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("public class " + TableName)
                   .AppendLine("{");

            foreach (var item in Columns)
            {
                builder.AppendLine(BuildProperty(item.Name, item.DataType));
            }

            builder.AppendLine("}");

            return builder;
        }

        private static string BuildProperty(string name, string sqlType)
        {
            return $"   public {sqlType.ToCSharpType()} {name.ToPascalCase()}  {{ get; set; }}";
        }
    }
}
