using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid.Xml
{
    public enum DataType { None, Custom, Numeric, DateTime }

    public class Column
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }

        private DataType _ColumnType;
        public DataType ColumnType
        {
            get { return _ColumnType; }
            set { _ColumnType = value; }
        }

        private string _FormatString;
        public string FormatString
        {
            get { return _FormatString; }
            set { _FormatString = value; }
        }

        private bool _Visible;
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private bool _Group;
        public bool Group
        {
            get { return _Group; }
            set { _Group = value; }
        }

        private bool _GroupTotal;
        public bool GroupTotal
        {
            get { return _GroupTotal; }
            set { _GroupTotal = value; }
        }

        private bool _GrandTotal;
        public bool GrandTotal
        {
            get { return _GrandTotal; }
            set { _GrandTotal = value; }
        }
    }
}
