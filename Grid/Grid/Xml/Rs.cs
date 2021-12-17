using System;
using System.Collections.Generic;
using System.Text;

namespace Grid.Grid.Xml
{
    public class Rs
    {
        private string _SqlQuery;
        public string SqlQuery
        {
            get { return _SqlQuery; }
            set { _SqlQuery = value; }
        }

        private string _ServerName;
        public string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }

        private string _OdbcDriver;
        public string OdbcDriver
        {
            get { return _OdbcDriver; }
            set { _OdbcDriver = value; }
        }

        private string _DbName;
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        private string _DbUserName;
        public string DbUserName
        {
            get { return _DbUserName; }
            set { _DbUserName = value; }
        }

        private string _DbPassword;
        public string DbPassword
        {
            get { return _DbPassword; }
            set { _DbPassword = value; }
        }

        private string _UsersDatabases;
        public string UsersDatabases
        {
            get { return _UsersDatabases; }
            set { _UsersDatabases = value; }
        }

        private string _ContextInfo;
        public string ContextInfo
        {
            get { return _ContextInfo; }
            set { _ContextInfo = value; }
        }

        public Rs()
        {
            UsersDatabases = "UsersDatabases";
        }
    }
}
