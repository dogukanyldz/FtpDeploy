using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid
{
    public class ReportTemplate
    {
        private int _RSayac;
        public int RSayac
        {
            get { return _RSayac; }
            set { _RSayac = value; }
        }
        private Guid _Guid;
        public Guid Guid
        {
            get { return _Guid; }
            set { _Guid = value; }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _ReportId;
        public string ReportId
        {
            get { return _ReportId; }
            set { _ReportId = value; }
        }
        private string _TemplateName;
        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        private bool _Default;
        public bool Default
        {
            get { return _Default; }
            set { _Default = value; }
        }
        private bool _AllUser;
        public bool AllUser
        {
            get { return _AllUser; }
            set { _AllUser = value; }
        }
        private bool _PivotGrid;
        public bool PivotGrid
        {
            get { return _PivotGrid; }
            set { _PivotGrid = value; }
        }
        private System.IO.MemoryStream _Layout;
        public System.IO.MemoryStream Layout
        {
            get { return _Layout; }
            set { _Layout = value; }
        }
        private bool _SaveAs;
        public bool SaveAs
        {
            get { return _SaveAs; }
            set { _SaveAs = value; }
        }
    }
}
