using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid.Xml
{
    public class Parameter
    {
        private string _KeyField;
        public string KeyField
        {
            get { return _KeyField; }
            set { _KeyField = value; }
        }

        private System.Data.CommandType _CommandType;
        public System.Data.CommandType CommandType
        {
            get { return _CommandType; }
            set { _CommandType = value; }
        }

        private string _ReportName;
        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private bool _ReplaceableTemplate;
        public bool ReplaceableTemplate
        {
            get { return _ReplaceableTemplate; }
            set { _ReplaceableTemplate = value; }
        }

        private bool _CreatableTemplate;
        public bool CreatableTemplate
        {
            get { return _CreatableTemplate; }
            set { _CreatableTemplate = value; }
        }

        private bool _DeletableTemplate;
        public bool DeletableTemplate
        {
            get { return _DeletableTemplate; }
            set { _DeletableTemplate = value; }
        }

        public ColumnCollection Columns;
        public Rs Rs;
        public int IntegratedSecurity;

        public Parameter()
        {
            Columns = new ColumnCollection();
            Rs = new Rs();
        }
    }
}
