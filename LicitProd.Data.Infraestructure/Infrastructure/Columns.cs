using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Data.Infrastructure.Infrastructure
{
    public class Columns
    {
        private readonly List<Column> _columns = new List<Column>();

        protected Columns(List<Column> columns)
        {
            _columns = columns;
        }
        public Columns()
        {

        }
        public Columns Add(string column)
        {
            _columns.Add(new Column(column));
            return new Columns(_columns); ;
        }
        public List<string> Send() => _columns.Select(x => x.Name).ToList();
    }
}
