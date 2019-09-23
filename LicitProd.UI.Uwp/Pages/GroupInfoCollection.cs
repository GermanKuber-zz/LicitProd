using System;
using System.Collections.Generic;

namespace LicitProd.UI.Uwp.Pages
{
    public class GroupInfoCollection<TEntity>
    {
        public List<TEntity> List { get; set; } = new List<TEntity>();
        public string Key { get; internal set; }

        internal void Add(TEntity item) =>
            List.Add(item);
    }
}