using SqlToCSharp.Model;
using System.Collections.Generic;
using System.Linq;

namespace SqlToCSharp.Abstractions
{
    public abstract class ConverterBase
    {
        private ConverterBase()
        {
            _columns = new List<Column>();
            _errorMessages = new List<string>();
        }

        protected ConverterBase(string path, string className) : this()
        {
            Path = path;
            TableName = className;
        }

        public string Path { get; set; }
        public string TableName { get; set; }

        private IList<string> _errorMessages;
        public IReadOnlyCollection<string> ErrorMessages { get => _errorMessages.ToArray(); }

        private IEnumerable<Column> _columns;
        public IReadOnlyCollection<Column> Columns { get => _columns.ToArray(); }

        public void SetColumns(IEnumerable<Column> columns)
        {
            _columns = columns;
        }

        protected void AddErrorMessage(string errorMessage)
        {
            _errorMessages.Add(errorMessage);
        }
    }
}
