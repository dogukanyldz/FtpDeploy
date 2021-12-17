using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Data
{
    public class DatabaseCollection : System.Collections.CollectionBase
    {
        public void Add(Database Database)
        {
            List.Add(Database);
        }

        public void Remove(int index)
        {
            if (!(index > Count - 1 || index < 0))
            {
                List.RemoveAt(index);
            }
        }

        public Database Item(int Index)
        {
            return (Database)List[Index];
        }

        public Database GetByName(string ItemName)
        {
            int Index = 0;
            foreach (Database i in this.List)
            {
                if (i.DbName == ItemName) return (Database)List[Index];
                Index += 1;
            }
            return null;
        }

        public Database GetByDescription(string ItemDescription)
        {
            int Index = 0;
            foreach (Database i in this.List)
            {
                if (i.Description == ItemDescription) return (Database)List[Index];
                Index += 1;
            }
            return null;
        }
    }
}
