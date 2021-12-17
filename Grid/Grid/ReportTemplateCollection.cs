using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid
{
    public class ReportTemplateCollection : System.Collections.CollectionBase
    {
        public void Add(ReportTemplate ReportTemplate)
        {
            List.Add(ReportTemplate);
        }

        public void Remove(int index)
        {
            if (!(index > Count - 1 || index < 0))
            {
                List.RemoveAt(index);
            }
        }

        public ReportTemplate Item(int Index)
        {
            return (ReportTemplate)List[Index];
        }

        public ReportTemplate GetByName(string ItemName)
        {
            int Index = 0;
            foreach (ReportTemplate i in this.List)
            {
                if (i.TemplateName == ItemName) return (ReportTemplate)List[Index];
                Index += 1;
            }
            return null;
        }

        public ReportTemplate GetByRSayac(int RSayac)
        {
            int Index = 0;
            foreach (ReportTemplate i in this.List)
            {
                if (i.RSayac == RSayac) return (ReportTemplate)List[Index];
                Index += 1;
            }
            return null;
        }

        public ReportTemplate GetDefault()
        {
            int Index = 0;
            foreach (ReportTemplate i in this.List)
            {
                if (i.Default) return (ReportTemplate)List[Index];
                Index += 1;
            }
            return null;
        }
    }
}
