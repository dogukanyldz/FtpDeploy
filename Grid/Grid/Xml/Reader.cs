using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Grid.Grid.Xml
{
    public class Reader
    {
        public Reader(Parameter Parameters, string XmlPath)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(XmlPath);

                Parameters.KeyField = Read("sKeyField", xml);
                Parameters.ReportName = Read("sReportName", xml);
                Parameters.UserName = Read("sUserName", xml);
                Parameters.ReplaceableTemplate = Read("sablon_degistirebilir", xml) == "1" ? true : false;
                Parameters.CreatableTemplate = Read("sablon_olusturabilir", xml) == "1" ? true : false;
                Parameters.DeletableTemplate = Read("sablon_silebilir", xml) == "1" ? true : false;                

                #region nExecuteProp
                if (xml.GetElementsByTagName("nExecuteProp").Count > 0)
                {
                    switch (xml.GetElementsByTagName("nExecuteProp").Item(0).InnerText)
                    {
                        case "1": Parameters.CommandType = System.Data.CommandType.StoredProcedure;
                            break;
                        case "0": Parameters.CommandType = System.Data.CommandType.Text;
                            break;
                        default: Parameters.CommandType = System.Data.CommandType.Text;
                            break;
                    }
                }
                else
                {
                    Parameters.CommandType = System.Data.CommandType.Text;
                }
                #endregion

                #region IntegratedSecurity
                Parameters.IntegratedSecurity = 0;
                Debug.WriteLine("Parameters.IntegratedSecurity" + Parameters.IntegratedSecurity);
                try
                {
                    Parameters.IntegratedSecurity = Convert.ToInt32(Read("IntegratedSecurity", xml));
                    Debug.WriteLine("TRY Parameters.IntegratedSecurity" + Parameters.IntegratedSecurity);
                }
                catch
                {
                    Debug.WriteLine("CATCH Parameters.IntegratedSecurity" + Parameters.IntegratedSecurity);
                    Parameters.IntegratedSecurity = 0;
                }
                #endregion

                #region Columns
                XmlNodeList ColumnList = xml.GetElementsByTagName("oCol");
                foreach (XmlNode Col in ColumnList)
                {
                    Column column = new Column();
                    column.Name = Read("kolon_adi", Col);
                    column.Caption = Read("kolon_basligi", Col);

                    column.ColumnType = DataType.Custom;
                    if (Col.InnerXml.IndexOf("kolon_veri_tipi") > 0)
                    {
                        var kolvertip = Col.SelectSingleNode("kolon_veri_tipi").InnerText;
                        if (kolvertip.Length > 0)
                        {
                            switch (kolvertip[0])
                            {
                                case 'd':
                                    column.ColumnType = DataType.DateTime;
                                    break;
                                case 'n':
                                    column.ColumnType = DataType.Numeric;
                                    break;
                                default:
                                    column.ColumnType = DataType.Custom;
                                    break;
                            }
                            column.FormatString = kolvertip;
                        }
                    }
                    else
                    {
                        column.ColumnType = DataType.Custom;
                    }

                    // Artik bunlara ihtiyac kalmadi
                    //column.FormatString = Read("Excel_Format", Col);
                    //if (column.FormatString == "@") column.FormatString = "";

                    column.Visible = false;
                    if (Col.InnerXml.IndexOf("kolonu_goster") > 0)
                    {
                        if (Col.SelectSingleNode("kolonu_goster").InnerText == "1") column.Visible = true;
                    }

                    column.Group = false;
                    if (Col.InnerXml.IndexOf("kirilima_tabi") > 0)
                    {
                        if (Col.SelectSingleNode("kirilima_tabi").InnerText == "1")
                        {
                            bool group = false;
                            if (Col.InnerXml.IndexOf("kirilim_toplam_yaz") > 0)
                                if (Col.SelectSingleNode("kirilim_toplam_yaz").InnerText == "1")
                                    group = true;
                            if(!group)
                                if (Col.InnerXml.IndexOf("kirilim_adedi_yaz") > 0)
                                    if (Col.SelectSingleNode("kirilim_adedi_yaz").InnerText == "1")
                                        group = true;
                            if (group)
                            {
                                column.Group = true;
                                column.Visible = true;
                            }
                        }
                    }

                    column.GroupTotal = false;
                    if (Col.InnerXml.IndexOf("kirilimda_toplam_al") > 0)
                    {
                        if (Col.SelectSingleNode("kirilimda_toplam_al").InnerText == "1") column.GroupTotal = true;
                    }

                    column.GrandTotal = false;
                    if (Col.InnerXml.IndexOf("genel_toplam_al") > 0)
                    {
                        if (Col.SelectSingleNode("genel_toplam_al").InnerText == "1") column.GrandTotal = true;
                    }

                    Parameters.Columns.Add(column);
                }
                #endregion

                #region Rs
                int Counter = 1;
                while (xml.GetElementsByTagName("sQuery" + Counter).Count > 0)
                {
                    Parameters.Rs.SqlQuery += xml.GetElementsByTagName("sQuery" + Counter).Item(0).InnerText;
                    Counter += 1;
                }

                Parameters.Rs.ServerName = Read("sServerName", xml);
                Parameters.Rs.DbName = Read("sDbName", xml);
                Parameters.Rs.DbUserName = Read("sDbUserName", xml);
                Parameters.Rs.DbPassword = Read("sDbUserPass", xml);
                Parameters.Rs.ContextInfo = Read("ContextInfo", xml);

                string usersDatabases = Read("UsersDatabases", xml);

                if (!string.IsNullOrEmpty(usersDatabases))
                {
                    if (usersDatabases == "~ayni_db~")
                        Parameters.Rs.UsersDatabases = Parameters.Rs.DbName;
                    else
                        Parameters.Rs.UsersDatabases = usersDatabases;
                }
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }
        }

        private string Read(string KeyName, XmlDocument Document)
        {
            string result = "";
            if (Document.GetElementsByTagName(KeyName).Count > 0)
                result = Document.GetElementsByTagName(KeyName).Item(0).InnerText;
            return result;
        }

        private string Read(string KeyName, XmlNode Node)
        {
            string result = "";
            if (Node.InnerXml.IndexOf(KeyName) > 0)
                result = Node.SelectSingleNode(KeyName).InnerText;
            return result;
        }
    }
}
