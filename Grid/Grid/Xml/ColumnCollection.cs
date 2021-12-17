using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid.Xml
{
    public class ColumnCollection : System.Collections.CollectionBase
    {
        public void Add(Column Column)
        {
            List.Add(Column);
        }

        public void Remove(int index)
        {
            if (!(index > Count - 1 || index < 0))
            {
                List.RemoveAt(index);
            }
        }

        public Column Item(int Index)
        {
            return (Column)List[Index];
        }

        public Column GetByName(string ItemName)
        {
            int Index = 0;
            foreach (Column i in this.List)
            {
                if (i.Name == ItemName) return (Column)List[Index];
                Index += 1;
            }
            return null;
        }
    }
}
