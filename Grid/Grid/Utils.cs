using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using Microsoft.Win32;
using System.IO;
using System.Data.Common;
using Dapper;

namespace Grid.Grid
{
    public class Utils
    {
        static Data.Database _oDatabase;

        public Utils(Data.Database oDatabase)
        {
            _oDatabase = oDatabase;            
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(@"Data Source = " + _oDatabase.ServerName + "; Initial Catalog = " + _oDatabase.DbName + "; Connection Timeout = 30; Connection Lifetime = 1000; Persist Security Info = True; User ID = " + _oDatabase.DbUserName + "; Password = " + _oDatabase.DbPassword);

            }      
        }
        private static int GroupIndex = 0, VisibleIndex = 0;


   
        public static GridColumn AddColumn(DevExpress.XtraGrid.Views.Grid.GridView oGridView, Grid.Xml.Column column, GridLocalizer_TR oGridLocalizer)
        {
            GridColumn col = new GridColumn();
            col.Name = column.Name;
            col.FieldName = column.Name;
            col.Caption = String.IsNullOrEmpty(column.Caption) ? column.Name : column.Caption;
            col.Visible = column.Visible;
            if (col.Visible) col.VisibleIndex = VisibleIndex++;

            switch (column.ColumnType)
            {
                case Grid.Xml.DataType.DateTime:
                    col.DisplayFormat.FormatType = FormatType.DateTime;
                    break;
                case Grid.Xml.DataType.Numeric:
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    break;
                default:
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    break;
            }

            string DecimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
            string GroupSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator;
            int DecimalLength = 0;
            if (!String.IsNullOrEmpty(column.FormatString) && (column.FormatString.Contains("#")))
            {
                int LastIndexOfGroupSeparator = column.FormatString.LastIndexOf(GroupSeparator);
                int LastIndexOfDecimalSeparator = column.FormatString.LastIndexOf(DecimalSeparator);
                if ((LastIndexOfGroupSeparator > 0) && (LastIndexOfDecimalSeparator > 0))
                {
                    if (LastIndexOfGroupSeparator > LastIndexOfDecimalSeparator)
                        DecimalLength = column.FormatString.Length - (LastIndexOfGroupSeparator + 1);
                    else
                        DecimalLength = column.FormatString.Length - (LastIndexOfDecimalSeparator + 1);
                }
                col.DisplayFormat.FormatString = "{0:n" + DecimalLength.ToString() + "}";// + column.FormatString + "}";#.0,000
            }
            else
            {
                col.DisplayFormat.FormatString = col.DisplayFormat.FormatType == FormatType.DateTime ? "{0:d}" : "{0}";
            }

            if (col.DisplayFormat.FormatType == FormatType.Numeric)
            {
                oGridLocalizer.MenuFooterAverageFormat = "ORT=" + col.DisplayFormat.FormatString;
                oGridLocalizer.MenuFooterMaxFormat = "MAX=" + col.DisplayFormat.FormatString;
                oGridLocalizer.MenuFooterMinFormat = "MIN=" + col.DisplayFormat.FormatString;
                oGridLocalizer.MenuFooterSumFormat = "TOP=" + col.DisplayFormat.FormatString;
            }

            if (column.Group == true)
            {
                col.GroupIndex = GroupIndex++;
                //col.GroupFormat;
                //col.GroupInterval;
            }

            if (column.GroupTotal == true)
            {
                oGridView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded;
                GridGroupSummaryItem item = new GridGroupSummaryItem();
                item.FieldName = col.FieldName;
                item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item.DisplayFormat = col.DisplayFormat.FormatString;
                item.Tag = 1;
                item.ShowInGroupColumnFooter = col;
                oGridView.GroupSummary.Add(item);
            }

            if (column.GrandTotal == true)
            {
                col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                col.SummaryItem.DisplayFormat = col.DisplayFormat.FormatString;
                col.SummaryItem.Tag = 1;
                oGridView.OptionsView.ShowFooter = true;
            }

            col.OptionsColumn.AllowEdit = false;
            col.OptionsColumn.AllowFocus = true;
            col.OptionsColumn.AllowGroup = DefaultBoolean.True;
            col.OptionsColumn.AllowIncrementalSearch = true;
            col.OptionsColumn.AllowMerge = DefaultBoolean.False;
            col.OptionsColumn.AllowMove = true;
            col.OptionsColumn.AllowSize = true;
            col.OptionsColumn.AllowSort = DefaultBoolean.True;
            col.OptionsColumn.FixedWidth = false;
            col.OptionsColumn.ReadOnly = true;
            col.OptionsColumn.ShowCaption = true;
            col.OptionsColumn.ShowInCustomizationForm = true;
            col.OptionsFilter.AllowAutoFilter = true;

            if (column.ColumnType == Grid.Xml.DataType.Numeric)
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Equals;
            else
                col.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

            col.OptionsFilter.AllowFilter = true;
            col.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList; // checkbox haline getirdigimizde boþ seçenekler gelmiyor..
            //col.OptionsFilter.ImmediateUpdateAutoFilter = true;

            oGridView.Columns.Add(col);
            if (column.Group)
            {
                oGridView.GroupCount = GroupIndex > 0 ? GroupIndex : 0;
                oGridView.SortInfo.Add(new GridColumnSortInfo(col, DevExpress.Data.ColumnSortOrder.Ascending));
                oGridView.Columns[col.FieldName].Group();
            }
            //col.SortMode = ColumnSortMode.DisplayText;
            return col;
        }

        public static void AddColumns(DevExpress.XtraGrid.Views.Grid.GridView oGridView, Grid.Xml.Parameter oParameter, GridLocalizer_TR oGridLocalizer)
        {
            GroupIndex = 0;
            VisibleIndex = 0;
            foreach (Grid.Xml.Column column in oParameter.Columns)
            {
                GridColumn col = AddColumn(oGridView, column, oGridLocalizer);
            }
        }

        public static void BindDataSource(DataTable oDataTable, Grid.Xml.Parameter oParameter, Data.Database oDatabase)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                   
                        oDataTable.Load(connection.ExecuteReader(oParameter.Rs.SqlQuery, commandTimeout: 300));
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Baðlantý veya sorgulama hatasý oluþtu!", "HATA");
                MessageBox.Show(ex.Message, "HATA");
            }
            finally
            {
                //conn.Close();
            }
        }

        public static bool GetTemplateList(Grid.ReportTemplateCollection oTemplates, Data.Database oDatabase, Grid.Xml.Parameter oParameter)
        {
            bool bReturn = false;

            if (String.IsNullOrEmpty(oDatabase.DbCid))
            {
                MessageBox.Show("Þirket bilgisine ulaþýlamadý!", "HATA");
                return false;
            }

            try
            {
                using (IDbConnection connection = Connection)

                {
                    if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                    {
                        string sql2 = "DECLARE @BinVar binary(128);" +
                          "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                          "SET context_info @BinVar";
                        connection.Execute(sql2);
                    }

                    StringBuilder sql = new StringBuilder("SELECT * FROM Usersdatabases..utl_devx_grid_templates ");
                    sql.Append("WHERE [cid]=@cid and [report_id]=@report_id and [pivotgrid]=0 ");
                    sql.Append("ORDER BY [default] DESC,[all_user] DESC,[template_name] ASC");
                    IDataReader rdr = connection.ExecuteReader(sql.ToString(),param: new { cid = oDatabase.DbCid, user_name=oParameter.UserName,report_id=oParameter.ReportName});
                    while (rdr.Read())
                    {
                        Grid.ReportTemplate template = new Grid.ReportTemplate();
                        template.RSayac = (int)rdr["r_sayac"];
                        template.Guid = (Guid)rdr["guid"];
                        template.UserName = (string)rdr["user_name"];
                        template.ReportId = (string)rdr["report_id"];
                        template.TemplateName = (string)rdr["template_name"];
                        template.Default = (bool)rdr["default"];
                        template.AllUser = (bool)rdr["all_user"];
                        template.PivotGrid = (bool)rdr["pivotgrid"];
                        oTemplates.Add(template);
                    }
                    rdr.Close();

                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("CommandText property has not been initialized"))
                    MessageBox.Show(ex.Message, "HATA"); ;
                bReturn = false;
            }
            return bReturn;
        }

        public static bool SaveLayout(Grid.ReportTemplate oTemplate, Data.Database oDatabase, Grid.Xml.Parameter oParameter)
        {
           string user_name= getTemplateUserName(oTemplate.Guid.ToString());
            bool bReturn = false;
            StringBuilder sb = null;
      
            if (String.IsNullOrEmpty(oDatabase.DbCid))
            {
                MessageBox.Show("Þirket bilgisine ulaþýlamadý!", "HATA");
                return false;
            }

            if (String.IsNullOrEmpty(oTemplate.ReportId))
            {
                MessageBox.Show("Rapor adý boþ olamaz!", "HATA");
                return false;
            }

            if (String.IsNullOrEmpty(oTemplate.TemplateName))
            {
                MessageBox.Show("Þablon adý boþ olamaz!", "HATA");
                return false;
            }

            if(user_name != oParameter.UserName)
            {
                MessageBox.Show("Þablon sadece onu oluþturan kullanýcý tarfýndan deðiþtirilebilir.", "HATA");
                return false;
            }

            try
            {
                byte[] bytes = oTemplate.Layout.ToArray();
                string file = Encoding.UTF8.GetString(bytes, 0, bytes.Length);


                using (IDbConnection connection = Connection)
                {

                    if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                    {
                        string sql = "DECLARE @BinVar binary(128);" +
                          "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                          "SET context_info @BinVar";
                        connection.Execute(sql);
                    }

                    if (oTemplate.RSayac > 0)
                    {
                        sb = new StringBuilder("UPDATE Usersdatabases..utl_devx_grid_templates ");
                        sb.Append("SET [cid]=@cid,[user_name]=@user_name,[report_id]=@report_id,[template_name]=@template_name,[default]=@default_,[all_user]=@all_user,[pivotgrid]=0,[template_file]=@template_file ");
                        sb.Append("WHERE guid=@guid");

                        connection.Execute(sb.ToString(), param: new
                        {
                            cid = oDatabase.DbCid,
                            guid = oTemplate.Guid,
                            user_name = oTemplate.UserName,
                            report_id = oTemplate.ReportId,
                            template_name = oTemplate.TemplateName,
                            default_ = oTemplate.Default,
                            all_user = oTemplate.AllUser,
                            template_file = file
                        });
                    }
                    else
                    {
                        sb = new StringBuilder("INSERT INTO Usersdatabases..utl_devx_grid_templates([cid],[guid],[user_name],[report_id],[template_name],[default],[all_user],[pivotgrid],[template_file]) ");
                        sb.Append("VALUES(@cid,NewId(),@user_name,@report_id,@template_name,@default_,@all_user,0,@template_file)");
                        connection.Execute(sb.ToString(), param: new
                        {
                            cid = oDatabase.DbCid,
                            user_name = oTemplate.UserName,
                            report_id = oTemplate.ReportId,
                            template_name = oTemplate.TemplateName,
                            default_ = oTemplate.Default,
                            all_user = oTemplate.AllUser,
                            template_file = file
                        });
                    }
              
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY")) MessageBox.Show("Ayný þablon adý daha önce tanýmlanmýþ!", "UYARI");
                else MessageBox.Show(ex.Message, "HATA");
                bReturn = false;
            }
           
            return bReturn;
        }

        public static bool LoadLayout(Grid.ReportTemplate oTemplate, Data.Database oDatabase)
        {
            bool bReturn = false;
            try
            {
               using(IDbConnection connection = Connection)
                {
                    if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                    {
                        string sql = "DECLARE @BinVar binary(128);" +
                          "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                          "SET context_info @BinVar";
                        connection.Execute(sql);
                    }

                    string query = "SELECT template_file FROM Usersdatabases..utl_devx_grid_templates WHERE [guid]=@guid";
                    IDataReader rdr = connection.ExecuteReader(query, param : new {guid=oTemplate.Guid});
                    if (rdr.Read())
                    {
                        string file = (string)rdr["template_file"];
                        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                        byte[] bytes = encoding.GetBytes(file);
                        System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes);
                        oTemplate.Layout = stream;
                    }
                    rdr.Close();

                    bReturn = true;
                }

               
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("CommandText property has not been initialized"))
                    MessageBox.Show(ex.Message, "HATA");
                bReturn = false;
            }        
            return bReturn;
        }

        public static string getTemplateUserName(string guid)
        {
            string username = "";

            try
            {
                using (IDbConnection connection = Connection)
                {
                    string sql = $@"select user_name FROM Usersdatabases..utl_devx_grid_templates WHERE guid='{guid}'";

                    username = connection.ExecuteScalar(sql).ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Template UserName'i Getirilirken Hata Alýndý!");
            }
            

            return username;


        }
        public static bool DeleteLayout(Grid.ReportTemplate oTemplate, Data.Database oDatabase, Grid.Xml.Parameter oParameter)
        {
            bool bReturn = false;
            try
            {
                #region ""sablon_silebilir" yetkisi"

                if (oParameter.DeletableTemplate == false && oTemplate.UserName.ToLower() != oParameter.UserName.ToLower() && oParameter.UserName.ToLower() != "admin")
                {
                    MessageBox.Show("Rapor þablonunu silme yetkiniz yoktur! \n\n Yetki Nesne Adý : Basýlý Form Rapor Þablonu Silme Yetkisi  \n Nesne id : utl_dev_sablon_silebilme", "UYARI");
                    bReturn = false;
                }
                else
                {
                    try
                    {
                        using(IDbConnection connection = Connection)
                        {
                            if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                            {
                                string sql = "DECLARE @BinVar binary(128);" +
                                  "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                                  "SET context_info @BinVar";
                                connection.Execute(sql);
                            }

                           string query = "DELETE FROM Usersdatabases..utl_devx_grid_templates WHERE [guid]=@guid";
                            connection.Execute(query, param: new { guid = oTemplate.Guid });
                            bReturn = true;

                        }
                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "HATA");
                        bReturn = false;
                    }               
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA");
                bReturn = false;
            }

            return bReturn;
        }

        public static bool SaveAsDefault(Grid.ReportTemplate oTemplate, Data.Database oDatabase)
        {
            bool bReturn = false;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                    {
                        string sql = "DECLARE @BinVar binary(128);" +
                          "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                          "SET context_info @BinVar";
                        connection.Execute(sql);
                    }

                    string query = "UPDATE Usersdatabases..utl_devx_grid_templates SET [default]=@default_ WHERE [guid]=@guid";
                    connection.Execute(query,param : new {guid=oTemplate.Guid,default_=true });
                }
                bReturn = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA");
                bReturn = false;
            }
            return bReturn;
        }

        public static string GetFirmaUnvani(Data.Database oDatabase)
        {
            string bReturn = "";
  
            try
            { 
                using(IDbConnection connection = Connection)
                {

                    if (!string.IsNullOrEmpty(oDatabase.ContextInfo))
                    {
                        string sql = "DECLARE @BinVar binary(128);" +
                          "SET @BinVar = cast(" + oDatabase.ContextInfo + " as binary(128));" +
                          "SET context_info @BinVar";
                        connection.Execute(sql);
                    }
                    bReturn = connection.ExecuteScalar("SELECT max(firmaunvani) FROM tnm_firma").ToString();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA");
                bReturn = "";
            }
           
            return bReturn;
        }

        public static string GetOfficeVersion()
        {
            string strEVersionSubKey = "\\Excel.Application\\CurVer"; //HKEY_CLASSES_ROOT/Excel.Application/Curver

            string strValue = null; //Value Present In Above Key
            string strVersion = null; //Determines Excel Version

            try
            {
                RegistryKey rkVersion = null; //Registry Key To Determine Excel Version

                rkVersion = Registry.ClassesRoot.OpenSubKey(strEVersionSubKey, false); //Open Registry Key

                if ((rkVersion != null)) //If Key Exists
                {
                    strValue = (string)rkVersion.GetValue(string.Empty); //Get Value

                    strValue = strValue.Substring(strValue.LastIndexOf(".") + 1); //Store Value

                    switch (strValue) //Determine Version
                    {
                        case "7":
                            strVersion = "95";
                            break;
                        case "8":
                            strVersion = "97";
                            break;
                        case "9":
                            strVersion = "2000";
                            break;
                        case "10":
                            strVersion = "2002";
                            break;
                        case "11":
                            strVersion = "2003";
                            break;
                        case "12":
                            strVersion = "2007";
                            break;
                        case "14":
                            strVersion = "2010";
                            break;
                        case "15":
                            strVersion = "2013";
                            break;
                        case "16":
                            strVersion = "2017";
                            break;
                        case "17":
                            strVersion = "2018";
                            break;
                        case "18":
                            strVersion = "2018";
                            break;
                        case "19":
                            strVersion = "2019";
                            break;
                        case "20":
                            strVersion = "2020";
                            break;
                        case "21":
                            strVersion = "2021";
                            break;
                        case "22":
                            strVersion = "2022";
                            break;

                    }

                    //MessageBox.Show("Excel " + strVersion + " Installed!"); //Display Result
                }
            }
            catch { }

            return strVersion;
        }


        public static void WriteLog(object Message)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\");
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\temp\\Grid.log";

                FileStream file = new FileStream(filePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(DateTime.Now.ToString() + " " + Message);
                sw.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(DateTime.Now.ToString() + " " + ex.Message);
            }
        }

        public static byte[] GetStringBytes(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            char[] chars = str.ToCharArray();

            for (int i = 0; i < bytes.Length; ++i)
            {
                switch (chars[i])
                {
                    case 'ý': bytes[i] = 253; break;
                    case 'ð': bytes[i] = 240; break;
                    case 'Ð': bytes[i] = 208; break;
                    case 'ü': bytes[i] = 252; break;
                    case 'Ü': bytes[i] = 220; break;
                    case 'þ': bytes[i] = 254; break;
                    case 'Þ': bytes[i] = 222; break;
                    case 'Ý': bytes[i] = 221; break;
                    case 'Ö': bytes[i] = 214; break;
                    case 'ö': bytes[i] = 246; break;
                    case 'ç': bytes[i] = 231; break;
                    case 'Ç': bytes[i] = 199; break;
                }
            }
            return bytes;
        }
    }
}
