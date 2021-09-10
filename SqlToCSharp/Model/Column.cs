using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToCSharp.Model
{
    public class Column
    {
        public Column(
            string name,
            string dataType)
        {
            Name = name;
            DataType = dataType;
        }

        public string Name { get; private set; }
        public string DataType { get; private set; }
    }
}
