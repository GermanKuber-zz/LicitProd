using System;

namespace LicitProd.Data
{
    public class Column
    {
        public string Name { get; }

        public Column(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
