using System.Collections.Generic;
using System.Linq;

namespace LicitProd.Data
{
    public class Columns
    {
        private  List<Column> columns = new List<Column>();

        protected Columns(List<Column> columns)
        {
            this.columns = columns;
        }
        public Columns()
        {

        }
        public Columns Add(string column)
        {
            columns.Add(new Column(column));
            return new Columns(columns); ;
        }
        public List<string> Send() => columns.Select(x => x.Name).ToList();
    }
}
