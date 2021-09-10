using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SqlToCSharp.Extensions
{
    public static class Helpers
    {
        public static string ToPascalCase(this string @string)
        {
            //clear string
            @string = Regex.Replace(@string, "[^0-9A-Za-z ,]", string.Empty);

            // Find word parts using the following rules:
            // 1. all lowercase starting at the beginning is a word
            // 2. all caps is a word.
            // 3. first letter caps, followed by all lowercase is a word
            // 4. the entire string must decompose into words according to 1,2,3.
            // Note that 2&3 together ensure MPSUser is parsed as "MPS" + "User".

            Match match = Regex.Match(@string, "^(?<word>^[a-z]+|[A-Z]+|[A-Z][a-z]+)+$");
            Group group = match.Groups["word"];

            // Take each word and convert individually to TitleCase
            // to generate the final output.  Note the use of ToLower
            // before ToTitleCase because all caps is treated as an abbreviation.

            TextInfo thread = Thread
                .CurrentThread
                .CurrentCulture
                .TextInfo;

            StringBuilder stringBuilder = new();

            foreach (Capture capture in group.Captures.Cast<Capture>())
                stringBuilder.Append(thread.ToTitleCase(capture.Value.ToLower()));

            return stringBuilder.ToString();
        }

        public static string ToCSharpType(this string @string)
        {
            switch (@string)
            {
                case "varbinary":
                case "binary":
                case "filestream":
                case "image":
                case "rowversion":
                case "timestamp"://?
                    return "byte[]";
                case "tinyint":
                    return "byte";
                case "varchar":
                case "nvarchar":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                    return "string";
                case "char":
                    return "char";
                case "bigint":
                    return "long";
                case "bit":
                    return "bool";
                case "smalldatetime":
                case "datetime":
                case "date":
                case "datetime2":
                    return "DateTime";
                case "datetimeoffset":
                    return "DateTimeOffset";
                case "@decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return "decimal";
                case "@float":
                    return "double";
                case "@int":
                    return "int";
                case "real":
                    return "Single";
                case "smallint":
                    return "short";
                case "uniqueidentifier":
                    return "Guid";
                case "sql_variant":
                    return "object";
                case "time":
                    return "TimeSpan";
                default:
                    return "string";
            }
        }
    }
}
