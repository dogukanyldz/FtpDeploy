using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Collections;
using System.IO;
using System.Xml;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using Microsoft.Win32;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using DevExpress.Data.Filtering;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Grid
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        const int WM_COPYDATA = 0x4A;

        private void GetDatabaseSettings()
        {
            Process[] ps = Process.GetProcessesByName("ana32");
            if (ps == null || ps.Length == 0)
            {
                Trace.WriteLine("Ana32 is not running");
                return;
            }

            int sid = Process.GetCurrentProcess().SessionId;

            Process p = null;// ps[0]; // Only one instance of ana32 can run

            foreach (Process pr in ps)
            {
                if (pr.SessionId == sid)
                {
                    p = pr;
                    break;
                }
            }

            if (p == null)
            {
                Trace.WriteLine("Ana32 is not running 2");
                return;
            }

            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cds));
            Marshal.StructureToPtr(cds, ptr, true);

            SendMessage(p.MainWindowHandle, WM_COPYDATA, this.Handle, ptr);

            Marshal.FreeCoTaskMem(ptr);

        }

        protected override void WndProc(ref Message msg)
        {

            switch (msg.Msg)
            {
                // Database connection settings are sent with this message
                case WM_COPYDATA:
                    COPYDATASTRUCT cds = new COPYDATASTRUCT();
                    cds = (COPYDATASTRUCT)Marshal.PtrToStructure(msg.LParam, typeof(COPYDATASTRUCT));
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    string conn = Marshal.PtrToStringAnsi(cds.lpData);
                    //Trace.WriteLine("CONN=" + conn);
                    char[] seps = { ';' };
                    string[] toks = conn.Split(seps);
                    //Trace.WriteLine("Server: " + toks[0]);
                    //Trace.WriteLine("Database: " + toks[1]);
                    //Trace.WriteLine("User: " + toks[2]);
                    //Trace.WriteLine("Password: " + toks[3]);

                    oGridParameter.Rs.ServerName = toks[0];
                    oGridParameter.Rs.DbName = toks[1];
                    oGridParameter.Rs.DbUserName = toks[2];
                    oGridParameter.Rs.DbPassword = toks[3];

                    if (String.IsNullOrEmpty(oGridParameter.Rs.UsersDatabases))
                        oGridParameter.Rs.UsersDatabases = oGridParameter.Rs.DbName;

                    msg.Result = (IntPtr)1; // return TRUE
                    return;
            }
            base.WndProc(ref msg); // {msg=0x14 (WM_ERASEBKGND) hwnd=0x3510d8 wparam=0x30140cc lparam=0x0 result=0x0}
        }

        #region "definition"
        private Grid.ReportTemplateCollection oTemplates = null;
        private Grid.ReportTemplate oTemplate = null;
        private DataTable DTTemplates = null;
        private Data.Database oActiveDatabase = null;
        private Grid.Xml.Parameter oGridParameter = null;
        private string GridLayoutXmlPath = null;
        private GridView oGridView = null;
        private GridLocalizer_TR oGridLocalizer = null;
        private DataTable DTSource = null;
        private string AlwaysDirectory = null;
        private int CheckedComboBox_Popup_top_count = 0;
        //ArrayList colorArray = new ArrayList();
        //const int DRAW_OFFSET = 4;
        //public static string __message;
        #endregion

        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            this.barButtonItemHesaplananAlanEkle.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //barButtonItemHesaplananAlanEkle.Enabled = false;
            //this.barButtonItemHesaplananAlanEkle.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            this.barButtonItemGridSaveLayout.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.WindowState = FormWindowState.Maximized;
            oGridLocalizer = new GridLocalizer_TR();
            oGridParameter = new Grid.Xml.Parameter();
            oGridView = (GridView)this.grid.Views[0];

            DTSource = new DataTable();
            DTTemplates = new DataTable();
            InitDTTemplates(DTTemplates);
            InitGridTemplates();

            string XmlPath = @"C:\always\gridsettings\GridColumns_E5C63964-8BF4-46D4-9532-A844BFD574AD.xml";

            bool excel = false;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                XmlPath = args[1]; // Manual Baslatm için açýklama satýrýna alman gerekir bu satýrý
                if (args.Length > 2)
                {
                    excel = args[2].Equals("excel");
                }
            }
            if (!String.IsNullOrEmpty(XmlPath))
            {
                //MessageBox.Show("XmlPath: " + XmlPath);
                //if (!File.Exists(XmlPath))
                //{
                //    // MessageBox.Show("Baðlantý bilgilerine ulaþýlamadý. Lütfen sistem yöneticisine baþvurunuz!", "HATA");
                //}
                //else
                //{
                //AlwaysDirectory = XmlPath.Substring(0, XmlPath.LastIndexOf("\\")).ToLower().Replace("\\gridsettings", "");
                if (XmlPath.ToLower().StartsWith("gridsettings\\"))
                {
                    AlwaysDirectory = Environment.CurrentDirectory;
                    XmlPath = AlwaysDirectory + "\\" + XmlPath;
                }
                else
                {
                    AlwaysDirectory = XmlPath.ToLower().Substring(0, XmlPath.LastIndexOf("\\")).Replace("\\gridsettings", "");
                }
                //MessageBox.Show("Xml Path : " + XmlPath + "\r\nAlways Path : " + always_path);
                Grid.Xml.Reader xr = new Grid.Xml.Reader(oGridParameter, XmlPath);

                #region GetDatabaseSettings
                //oGridParameter.Rs.ServerName = "";
                //oGridParameter.Rs.DbName = "";
                //oGridParameter.Rs.DbUserName = "";
                if (String.IsNullOrEmpty(oGridParameter.Rs.ServerName)
                    || String.IsNullOrEmpty(oGridParameter.Rs.DbName)
                    || String.IsNullOrEmpty(oGridParameter.Rs.DbUserName))
                {
                    GetDatabaseSettings();
                }
                if (String.IsNullOrEmpty(oGridParameter.Rs.ServerName)
                    || String.IsNullOrEmpty(oGridParameter.Rs.DbName)
                    || String.IsNullOrEmpty(oGridParameter.Rs.DbUserName))
                {
                    MySplashForm.CloseSplash();
                    MessageBox.Show("Veritabaný baðlantý bilgilerine ulaþýlamadý!");
                    return;
                }
                #endregion

                oActiveDatabase = new Data.Database();
                oActiveDatabase.ServerName = oGridParameter.Rs.ServerName;
                oActiveDatabase.DbName = oGridParameter.Rs.DbName;
                oActiveDatabase.DbUserName = oGridParameter.Rs.DbUserName;
                oActiveDatabase.DbPassword = oGridParameter.Rs.DbPassword;
                oActiveDatabase.UsersDatabases = oGridParameter.Rs.UsersDatabases;
                oActiveDatabase.ContextInfo = oGridParameter.Rs.ContextInfo;
                oActiveDatabase.DbCid = "HVC";
                Grid.Utils utils = new Grid.Utils(oActiveDatabase);

                RegistryKey _key = Registry.CurrentUser.OpenSubKey("Software\\Model\\Always\\CurrentCompany");
                if (_key != null)
                {
                    object _value = _key.GetValue("Cid");
                    if (_value != null) oActiveDatabase.DbCid = _value.ToString();
                }

                if (excel)
                {
                    Excel.FillExcelWithData(oGridParameter, oActiveDatabase, true);
                }
                else
                {
                    // Aslinda burasi normal isleyis
                    //
                    DevExpress.XtraGrid.Localization.GridLocalizer.Active = oGridLocalizer;
                    Grid.Utils.BindDataSource(DTSource, oGridParameter, oActiveDatabase);

                    oTemplate = new Grid.ReportTemplate();
                    oTemplate.RSayac = 0;
                    oTemplate.UserName = oGridParameter.UserName;
                    oTemplate.ReportId = oGridParameter.ReportName;
                    oTemplate.TemplateName = "";
                    oTemplate.Default = false;
                    FillTemplateList();

                    if ((this.cmbTemplates.Text == "") || (this.cmbTemplates.Text == null))
                    {
                        LayoutInit();
                        GridInit();
                    }
                }
            }

            MySplashForm.CloseSplash();
            this.Activate();
            //MessageBox.Show("Data satýrlarý: " + oGridView.Columns.Count.ToString(), "HATA");
        }

        private void LayoutInit()
        {
            GridLayoutXmlPath = AlwaysDirectory + "\\GridSettings\\GridLayout_" + oGridParameter.ReportName + ".xml";
        }

        private void GridSaveLayoutToXml()
        {
            if (!System.IO.Directory.Exists(AlwaysDirectory + "\\GridSettings\\"))
                Directory.CreateDirectory(AlwaysDirectory + "\\GridSettings\\");

            if (System.IO.File.Exists(GridLayoutXmlPath))
                this.grid.Views[0].SaveLayoutToXml(GridLayoutXmlPath, DevExpress.Utils.OptionsLayoutBase.FullLayout);

            MessageBox.Show("Rapor ayarlarýnýz kaydedildi!");
        }

        private void GridDeleteLayoutXml()
        {
            if (MessageBox.Show("Rapor ayarlarýnýz silinecek! Devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!System.IO.File.Exists(GridLayoutXmlPath)) System.IO.File.Delete(GridLayoutXmlPath);
            }
        }

        private void GridRestoreLayoutFromXml()
        {
                 
            if ((System.IO.File.Exists(GridLayoutXmlPath)) || (oTemplate.Layout != null))
            {
                XmlDocument xml = new XmlDocument();
                if (oTemplate.Layout != null)
                {
                    xml.Load(oTemplate.Layout);
                }
                else
                {
                    xml.Load(GridLayoutXmlPath);
                }
                xml.Save(GridLayoutXmlPath);                                                                                // xml'e sql'den gelen veriler dýþarýya kaydedildi.

                oGridView.RestoreLayoutFromXml(GridLayoutXmlPath, DevExpress.Utils.OptionsLayoutBase.FullLayout);          // dýþarýya kaydedilen xml'den gridview'a ilgili kolonlar ve propertyleri atanýyor.
                                                                                                                           
                                                                                                                            // kýsaca iþleyiþ þöyle: 1- sql'den gelen layoutu xml'e yükle 2- xml'i dýþarýya kaydet 3- kaydedilen xml'den ilgili kolon ve property bilgileri al.

                 
                foreach (Grid.Xml.Column column in oGridParameter.Columns)
                {
        
                    int i = 0;
                    if (oGridView.Columns.ColumnByName(column.Name) == null)
                    {
                        GridColumn col = Grid.Utils.AddColumn(oGridView, column, oGridLocalizer);
                        col.Visible = false;
                    }
                    else
                    {
                        if (oGridView.Columns.ColumnByName(column.Name).Caption != column.Caption)
                        {
                            oGridView.Columns.ColumnByName(column.Name).Caption = column.Caption;
                        }

                        if (column.ColumnType == Grid.Xml.DataType.Numeric)
                        {

                            //string format_type = oGridParameter.Columns.GetByName(column.Name).FormatString;
                            //format_type = format_type.Replace(";", "");
                            //format_type = format_type.Replace("m", "n");
                            string format_type = "{0:" + column.FormatString + "}";                                                       // Erp tarafýndan bize yollanan layout'da, numeric deðerler için virgülden sonra formatlarý ayarlama iþlemi.
                            format_type = format_type.Replace(";", "");                                                                   //örnk-> {0:n2} formatý 100,00 ve {0:n3} formatý da 100,000 olarak çýktý verir.         
                            oGridView.Columns.ColumnByName(column.Name).OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
                            oGridView.Columns.ColumnByName(column.Name).DisplayFormat.FormatString = format_type;
                        }
                    }
                }
            }
        }

        private void GridInit()
        {
            this.Text = oGridParameter.ReportName;
            this.grid.DataSource = null;
            this.grid.DataSource = DTSource;

            oGridView.Columns.Clear();
            oGridView.SortInfo.Clear();
            oGridView.GroupSummary.Clear();

            if (oTemplate.Layout != null)
            {
                GridRestoreLayoutFromXml();
            }
            else
            {
                Grid.Utils.AddColumns(oGridView, oGridParameter, oGridLocalizer);
            }

            oGridView.OptionsBehavior.Editable = false;

            #region OptionsCustomization
            oGridView.OptionsCustomization.AllowColumnMoving = true;
            oGridView.OptionsCustomization.AllowColumnResizing = true;
            oGridView.OptionsCustomization.AllowFilter = true;
            oGridView.OptionsCustomization.AllowGroup = true;
            oGridView.OptionsCustomization.AllowRowSizing = true;
            oGridView.OptionsCustomization.AllowSort = true;
            #endregion

            #region OptionsDetail
            oGridView.OptionsDetail.AllowExpandEmptyDetails = true;
            oGridView.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            oGridView.OptionsDetail.AllowZoomDetail = true;
            oGridView.OptionsDetail.AutoZoomDetail = true;
            oGridView.OptionsDetail.EnableDetailToolTip = true;
            oGridView.OptionsDetail.EnableMasterViewMode = true;
            oGridView.OptionsDetail.ShowDetailTabs = true;
            oGridView.OptionsDetail.SmartDetailExpand = true;
            #endregion

            #region OptionsFilter
            oGridView.OptionsFilter.AllowColumnMRUFilterList = true;
            oGridView.OptionsFilter.AllowFilterEditor = true;
            oGridView.OptionsFilter.AllowMRUFilterList = true;
            oGridView.OptionsFilter.ColumnFilterPopupMaxRecordsCount = 1000000; // unutma 03.04.2012
            oGridView.OptionsFilter.ColumnFilterPopupRowCount = 1000000;
            oGridView.OptionsFilter.MaxCheckedListItemCount = 1000000;
            oGridView.OptionsFilter.MRUColumnFilterListCount = 100000;
            oGridView.OptionsFilter.MRUFilterListCount = 100000;
            oGridView.OptionsFilter.MRUFilterListPopupCount = 100000;
            oGridView.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            oGridView.OptionsFilter.UseNewCustomFilterDialog = true; //  unutma 03.04.2012
            oGridView.OptionsFilter.DefaultFilterEditorView = FilterEditorViewMode.Visual; // unutma 03.04.2012

            #endregion

            oGridView.OptionsHint.ShowCellHints = true;
            oGridView.OptionsHint.ShowColumnHeaderHints = true;

            #region OptionsLayout
            oGridView.OptionsLayout.StoreAllOptions = true;
            oGridView.OptionsLayout.StoreAppearance = true;
            oGridView.OptionsLayout.StoreDataSettings = true;
            oGridView.OptionsLayout.StoreVisualOptions = true;
            //oGridView.OptionsLayout.Columns.StoreAllOptions = true;
            //oGridView.OptionsLayout.Columns.StoreAppearance = true;
            //oGridView.OptionsLayout.Columns.StoreLayout = true;
            #endregion

            #region OptionsMenu
          
            oGridView.OptionsMenu.EnableColumnMenu = true;
            oGridView.OptionsMenu.EnableFooterMenu = true;
            oGridView.OptionsMenu.EnableGroupPanelMenu = true;
            oGridView.OptionsMenu.ShowAutoFilterRowItem = true;
            oGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = true;
            oGridView.OptionsMenu.ShowGroupSortSummaryItems = true;
            oGridView.OptionsMenu.ShowGroupSummaryEditorItem = true;
            #endregion

            #region OptionsNavigation
            oGridView.OptionsNavigation.AutoFocusNewRow = true;
            oGridView.OptionsNavigation.AutoMoveRowFocus = true;
            oGridView.OptionsNavigation.EnterMoveNextColumn = true;
            oGridView.OptionsNavigation.UseTabKey = true;
            #endregion

            #region OptionsPrint
            oGridView.OptionsPrint.AutoWidth = false;
            oGridView.OptionsPrint.EnableAppearanceEvenRow = true;
            oGridView.OptionsPrint.EnableAppearanceOddRow = true;
            oGridView.OptionsPrint.ExpandAllDetails = true;
            oGridView.OptionsPrint.ExpandAllGroups = true;
            oGridView.OptionsPrint.PrintDetails = true;
            oGridView.OptionsPrint.PrintFilterInfo = false;
            oGridView.OptionsPrint.PrintFooter = true;
            oGridView.OptionsPrint.PrintGroupFooter = true;
            oGridView.OptionsPrint.PrintHeader = true;
            oGridView.OptionsPrint.PrintHorzLines = true;
            oGridView.OptionsPrint.PrintPreview = false;
            oGridView.OptionsPrint.PrintVertLines = true;
            oGridView.OptionsPrint.SplitCellPreviewAcrossPages = false;
            oGridView.OptionsPrint.UsePrintStyles = true;

            oGridView.AppearancePrint.HeaderPanel.BackColor = Color.FromArgb(141, 180, 227);
            oGridView.AppearancePrint.HeaderPanel.Font = new Font(oGridView.AppearancePrint.HeaderPanel.Font.FontFamily, 8, FontStyle.Bold);

            oGridView.AppearancePrint.GroupRow.BackColor = Color.FromArgb(197, 217, 241);
            oGridView.AppearancePrint.GroupRow.Font = new Font(oGridView.AppearancePrint.GroupRow.Font.FontFamily, 8, FontStyle.Bold);

            oGridView.AppearancePrint.GroupFooter.BackColor = Color.FromArgb(197, 217, 241);
            oGridView.AppearancePrint.GroupFooter.Font = new Font(oGridView.AppearancePrint.GroupFooter.Font.FontFamily, 8, FontStyle.Bold);

            oGridView.AppearancePrint.FooterPanel.BackColor = Color.FromArgb(197, 217, 241);
            oGridView.AppearancePrint.FooterPanel.Font = new Font(oGridView.AppearancePrint.FooterPanel.Font.FontFamily, 8, FontStyle.Bold);

            oGridView.AppearancePrint.EvenRow.BackColor = Color.White;
            oGridView.AppearancePrint.OddRow.BackColor = Color.White;

            oGridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            oGridView.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //oGridView.AppearancePrint.Lines.Options.UseTextOptions = true;
            //oGridView.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

            //oGridView.AppearancePrint.Row.Options.UseTextOptions = true;
            //oGridView.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

            oGridView.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            oGridView.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

            oGridView.AppearancePrint.OddRow.Options.UseTextOptions = true;
            oGridView.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            #endregion

            #region OptionsSelection
            oGridView.OptionsSelection.EnableAppearanceFocusedCell = true;
            oGridView.OptionsSelection.EnableAppearanceFocusedRow = true;
            oGridView.OptionsSelection.EnableAppearanceHideSelection = true;
            oGridView.OptionsSelection.InvertSelection = true;
            oGridView.OptionsSelection.MultiSelect = true;
            oGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            oGridView.OptionsSelection.UseIndicatorForSelection = true;
            #endregion

            #region OptionsView
            oGridView.OptionsView.AllowCellMerge = true;
            oGridView.OptionsView.AllowHtmlDrawHeaders = true;
            oGridView.OptionsView.AutoCalcPreviewLineCount = true;
            oGridView.OptionsView.ColumnAutoWidth = false;
            oGridView.OptionsView.EnableAppearanceEvenRow = true;
            oGridView.OptionsView.EnableAppearanceOddRow = true;
            oGridView.OptionsView.RowAutoHeight = true;
            oGridView.OptionsView.ShowAutoFilterRow = true;
            oGridView.OptionsView.ShowAutoFilterRow = true;
            oGridView.OptionsView.ShowChildrenInGroupPanel = true;
            oGridView.OptionsView.ShowColumnHeaders = true;
            oGridView.OptionsView.ShowDetailButtons = true;
            oGridView.OptionsView.ShowFooter = true;
            oGridView.OptionsView.ShowGroupedColumns = true;
            oGridView.OptionsView.ShowGroupPanel = true;
            oGridView.OptionsView.ShowHorzLines = true;
            oGridView.OptionsView.ShowIndicator = true;
            //oGridView.OptionsView.ShowPreview = true;
            oGridView.OptionsView.ShowPreviewLines = true;
            oGridView.OptionsView.ShowVertLines = true;
            #endregion

            #region GroupSummary
            if (oGridView.GroupSummary.Count > 0)
            {
                foreach (GridGroupSummaryItem item in oGridView.GroupSummary)
                {
                    if (!String.IsNullOrEmpty(item.FieldName))
                    {
                        if (oGridView.Columns[item.FieldName] != null)
                        {
                            item.DisplayFormat = oGridView.Columns[item.FieldName].DisplayFormat.FormatString;
                            item.ShowInGroupColumnFooter = oGridView.Columns[item.FieldName];
                        }
                    }
                }

                oGridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded;
                this.barCheckItemGridSatirAltToplam.Checked = true;
            }
            else
            {
                oGridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                this.barCheckItemGridSatirAltToplam.Checked = false;
            }
            #endregion

            this.barCheckItemGridSatirGenelToplam.Checked = oGridView.OptionsView.ShowFooter;

            oGridView.GroupSummary.CollectionChanged += new CollectionChangeEventHandler(GroupSummary_CollectionChanged);

            oGridView.ExpandAllGroups();
            oGridView.ColumnsCustomization();

            AdvCheckedFilter newFilter = new AdvCheckedFilter(gridView1); //unutma 03.04.2012
            foreach (GridColumn col in gridView1.Columns)
            {
                newFilter.AdvColumns.Add(col);
            }
        }

        protected void GroupSummary_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (sender is GridGroupSummaryItemCollection)
            {
                GridGroupSummaryItemCollection items = sender as GridGroupSummaryItemCollection;

                if ((items != null) && (items.Count > 0))
                {
                    foreach (GridGroupSummaryItem item in items)
                    {
                        if (!String.IsNullOrEmpty(item.FieldName))
                        {
                            if (oGridView.Columns[item.FieldName] != null)
                            {
                                item.DisplayFormat = oGridView.Columns[item.FieldName].DisplayFormat.FormatString;
                                item.ShowInGroupColumnFooter = oGridView.Columns[item.FieldName];
                            }
                        }
                    }
                    oGridView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded;
                    this.barCheckItemGridSatirAltToplam.Checked = true;
                }
                else
                {
                    oGridView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
                    this.barCheckItemGridSatirAltToplam.Checked = false;
                }
            }
        }

        private void barButtonItemGridShowCustomizationWindow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "barButtonItemGridShowCustomizationWindow":
                    oGridView.ColumnsCustomization();
                    break;
                case "barButtonItemGridRefreshData":
                    DTSource.Rows.Clear();
                    Grid.Utils.BindDataSource(DTSource, oGridParameter, oActiveDatabase);
                    oGridView.RefreshData();
                    break;
                case "barButtonItemGridSaveLayout":
                    GridSaveLayoutToXml();
                    break;
                case "barButtonItemGridDeleteLayout":
                    GridDeleteLayoutXml();
                    break;
                case "barButtonItemGridGroupPanelFullExpand":
                    oGridView.ExpandAllGroups();
                    break;
                case "barButtonItemGridGroupPanelFullCollapse":
                    oGridView.CollapseAllGroups();
                    break;
                case "barButtonItemGridGroupPanelClearGrouping":
                    oGridView.ClearGrouping();
                    break;
                case "barButtonItemGridShowFilterBuilderWindow":
                    oGridView.ShowFilterEditor(oGridView.Columns[0]);
                    break;
                case "barButtonItemCopySelected":
                    oGridView.CopyToClipboard();
                    break;
                case "barButtonItemPrintScreen":

                    oGridView.ShowPrintPreview();
                    break;
            }

            //FilterControl control1 = new FilterControl();
            //string ab = control1.FilterString.ToString();
            //string ab = control1.FilterViewInfo.ToString(); // boþ
            //string ab = control1.FilterColumns.GetValueScreenText();
            //string ab = control1.
            //MessageBox.Show(ab);
            //MessageBox.Show(gridView1.RowCount.ToString());
        }


        private void InitDTTemplates(DataTable dt)
        {
            dt.Columns.Add("r_sayac", typeof(int));
            dt.Columns.Add("guid", typeof(Guid));
            dt.Columns.Add("user_name", typeof(string));
            dt.Columns.Add("report_id", typeof(string));
            dt.Columns.Add("template_name", typeof(string));
            dt.Columns.Add("default", typeof(bool));
            dt.Columns.Add("all_user", typeof(bool));
            DataColumn[] pricols = new DataColumn[1];
            pricols[0] = dt.Columns["r_sayac"];
            dt.PrimaryKey = pricols;
        }

        private void InitGridTemplates()
        {
            cmbTemplates.Properties.View.Columns.Clear();
            cmbTemplates.Properties.DataSource = DTTemplates;
            cmbTemplates.Properties.ValueMember = "guid";
            cmbTemplates.Properties.DisplayMember = "template_name";
            cmbTemplates.Properties.PopulateViewColumns();
            cmbTemplates.Properties.View.Columns["r_sayac"].Visible = false;
            cmbTemplates.Properties.View.Columns["guid"].Visible = false;
            cmbTemplates.Properties.View.Columns["user_name"].Visible = true;
            cmbTemplates.Properties.View.Columns["user_name"].Caption = "Kullanýcý";
            cmbTemplates.Properties.View.Columns["user_name"].Width = 75;
            cmbTemplates.Properties.View.Columns["user_name"].VisibleIndex = 3;
            cmbTemplates.Properties.View.Columns["report_id"].Visible = false;
            cmbTemplates.Properties.View.Columns["template_name"].Visible = true;
            cmbTemplates.Properties.View.Columns["template_name"].Caption = "Þablon Adý";
            cmbTemplates.Properties.View.Columns["template_name"].Width = 150;
            cmbTemplates.Properties.View.Columns["template_name"].VisibleIndex = 0;
            cmbTemplates.Properties.View.Columns["default"].Visible = true;
            cmbTemplates.Properties.View.Columns["default"].Caption = "Varsayýlan";
            cmbTemplates.Properties.View.Columns["default"].Width = 75;
            cmbTemplates.Properties.View.Columns["default"].VisibleIndex = 1;
            cmbTemplates.Properties.View.Columns["all_user"].Visible = true;
            cmbTemplates.Properties.View.Columns["all_user"].Caption = "Tüm Kullanýcýlar";
            cmbTemplates.Properties.View.Columns["all_user"].Width = 100;
            cmbTemplates.Properties.View.Columns["all_user"].VisibleIndex = 2;
            //cmbTemplates.Properties.View.BestFitColumns();
            cmbTemplates.Properties.PopupFormWidth = 400;
            cmbTemplates.EditValue = Guid.Empty;
            cmbTemplates.Text = "";
        }

        private void FillTemplateList()
        {
            #region dttemplates
            oTemplates = new Grid.ReportTemplateCollection();
            Grid.Utils.GetTemplateList(oTemplates, oActiveDatabase, oGridParameter);
            DTTemplates.Rows.Clear();
            int selindex = -1;
            Guid guid = Guid.Empty;
            string template_name = "";
            for (int i = 0; i < oTemplates.Count; i++)
            {
                if ((oTemplates.Item(i).Default) && (selindex == -1))
                {
                    selindex = i;
                    guid = oTemplates.Item(i).Guid;
                    template_name = oTemplates.Item(i).TemplateName;
                }
                DataRow row = DTTemplates.NewRow();
                row["r_sayac"] = oTemplates.Item(i).RSayac;
                row["guid"] = oTemplates.Item(i).Guid;
                row["user_name"] = oTemplates.Item(i).UserName;
                row["report_id"] = oTemplates.Item(i).ReportId;
                row["template_name"] = oTemplates.Item(i).TemplateName;
                row["default"] = oTemplates.Item(i).Default;
                row["all_user"] = oTemplates.Item(i).AllUser;
                DTTemplates.Rows.Add(row);
            }
            #endregion

            if (guid != Guid.Empty)
            {
                cmbTemplates.EditValue = guid;
                //cmbTemplates.Text = String.IsNullOrEmpty(template_name) ? "<þablon adý belirtilmemiþ>" : template_name;
            }
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsForm dialog = new SaveAsForm(oTemplate, oGridParameter);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (oTemplate.SaveAs)
                    {
                        MemoryStream stream = new MemoryStream();
                        oGridView.SaveLayoutToStream(stream, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                        oTemplate.Layout = stream;
                        bool bInserted = Grid.Utils.SaveLayout(oTemplate, oActiveDatabase,oGridParameter);
                        if (bInserted)
                        {
                            FillTemplateList();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("Bilgileri kontrol ediniz.");
            }
        }

        private void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show((this.cmbTemplates.Text + " þablonunu silmek istediðinize emin misiniz?"), "UYARI", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    oTemplate = oTemplates.GetByName(this.cmbTemplates.Text);
                    if (oTemplate != null)
                    {
                        bool deleted = Grid.Utils.DeleteLayout(oTemplate, oActiveDatabase, oGridParameter);
                        if (deleted)
                        {
                            FillTemplateList();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("Bilgileri kontrol ediniz.");
            }
        }

        private void cmbTemplates_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (oTemplates != null)
                {
                    oTemplate = oTemplates.GetByName(this.cmbTemplates.Text);
                    Grid.Utils.LoadLayout(oTemplate, oActiveDatabase);
                    if (oTemplate != null)
                    {
                        if (oTemplate.Layout != null)
                        {
                            LayoutInit();
                            GridInit();
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("Bilgileri kontrol ediniz.");
            }
        }

        private void cmbTemplates_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void gridView1_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
                {
                    DevExpress.XtraGrid.Menu.GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                    //Erasing the default menu items
                    //menu.Items.Clear();
                    if (menu.Column != null)
                    {
                        //Adding new items
                        DXMenuCheckItem item = CreateCheckItem("Sabitleme", menu.Column, FixedStyle.None, null);
                        item.BeginGroup = true;
                        menu.Items.Add(item);
                        menu.Items.Add(CreateCheckItem("Sola Sabitle", menu.Column, FixedStyle.Left, null));
                        menu.Items.Add(CreateCheckItem("Saða Sabitle", menu.Column, FixedStyle.Right, null));

                        item = CreateCheckItem2("Birleþtirme", menu.Column, DevExpress.Utils.DefaultBoolean.False, null);
                        item.BeginGroup = true;
                        menu.Items.Add(item);
                        menu.Items.Add(CreateCheckItem2("Birleþtir", menu.Column, DevExpress.Utils.DefaultBoolean.True, null));
                    }
                }
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("");
            }
        }

        //Create a menu item
        DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, FixedStyle style, Image image)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, column.Fixed == style, image, new EventHandler(OnFixedClick));
            item.Tag = new MenuInfo(column, style);
            return item;
        }

        //Create a menu item
        DXMenuCheckItem CreateCheckItem2(string caption, GridColumn column, DevExpress.Utils.DefaultBoolean merge, Image image)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, column.OptionsColumn.AllowMerge == merge, image, new EventHandler(OnMergeClick));
            item.Tag = new MenuInfo(column, merge);
            return item;
        }

        //Menu item click handler
        void OnFixedClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;
            info.Column.Fixed = info.Style;
        }

        void OnMergeClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;
            info.Column.OptionsColumn.AllowMerge = info.AllowMerge;
        }

        private void barButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grid.Views[0].CopyToClipboard();
        }

        private void printableComponentLink1_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            StringBuilder reportHeader = new StringBuilder();
            reportHeader.Append("Firma         : " + Grid.Utils.GetFirmaUnvani(oActiveDatabase) + "\n\r");
            reportHeader.Append("Rapor Tarihi  : " + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\r");
            //reportHeader.Append("Rapor Adý \t: " + oGridParameter.ReportName);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            int visible = 0;
            int width = 0;
            foreach (GridColumn col in oGridView.Columns.View.VisibleColumns)
            {
                if (col.Visible)
                {
                    width += col.Width;
                    //break;
                    visible++;
                }
            }

            //e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
            //e.Graph.Font = new Font("Tahoma", 8, FontStyle.Regular);
            RectangleF rec = new RectangleF(0, 0, width, 35);
            //RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35);
            //RectangleF rec = new RectangleF(0, 0, 300, 35);
            //RectangleF rec = new RectangleF(0, 0, (100 * visible), 35);
            //RectangleF rec = new RectangleF(0, 0, 1, 35);
            e.Graph.DrawString(reportHeader.ToString(), rec);
         
            //e.Graph.DrawString(reportHeader.ToString(), Color.Black, rec, DevExpress.XtraPrinting.BorderSide.None); // BorderSide.None);
        }



        private void printableComponentLink1_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void grid_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (e.Button != MouseButtons.Right) return;
            if (oGridView.CalcHitInfo(pt).HitTest != GridHitTest.RowCell) return;
            // Shows the context menu. 
            popupMenuGrid.ShowPopup(barManager1, pt);
            //gridView1.ApplyColumnsFilter();
        }

        private void barCheckItemGridSatirAltToplam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarCheckItem item = e.Item as DevExpress.XtraBars.BarCheckItem;
            oGridView.GroupFooterShowMode = item.Checked ? GroupFooterShowMode.VisibleIfExpanded : GroupFooterShowMode.Hidden;
        }

        private void barCheckItemGridSatirGenelToplam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarCheckItem item = e.Item as DevExpress.XtraBars.BarCheckItem;
            oGridView.OptionsView.ShowFooter = item.Checked;
            //oGridView.ShowUnboundExpressionEditor(oGridView.Columns[0]);
        }

        private void gridView1_ShowFilterPopupCheckedListBox(object sender, FilterPopupCheckedListBoxEventArgs e)
        {
            try
            {
                //object[] obj = gridView1.DataController.FilterHelper.GetUniqueColumnValues(e.Column.ColumnHandle, -1, false, true, null);

                CheckedComboBox_Popup_top_count = 0;
                e.CheckedComboBox.AllowNullInput = DefaultBoolean.True;
                e.CheckedComboBox.SelectAllItemCaption = "Hepsi";

                #region "list - rows"
                ViewFilter my_vf = new ViewFilter();

                #region aktif filter bu kolona ait filtre kýsmýný kaldýrýlýyor
                ViewFilter vf = gridView1.ActiveFilter;
                IEnumerator ef = vf.GetEnumerator();
                while (ef.MoveNext())
                {
                    ViewColumnFilterInfo vcfi = ef.Current as ViewColumnFilterInfo;
                    if (vcfi.Column.FieldName != e.Column.FieldName)
                        my_vf.Add(vcfi);
                }
                #endregion

                #region "filtre - panel - columns"
                GridView gv = new GridView();
                ef = my_vf.GetEnumerator();
                while (ef.MoveNext())
                {
                    ViewColumnFilterInfo vcfi = ef.Current as ViewColumnFilterInfo;
                    gv.ActiveFilter.Add(vcfi);
                }
                string filter = gv.FilterPanelText;
                //string filter = my_vf.DisplayText;//gridView1.FilterPanelText;
                #endregion

                //if (filter != "")
                //{
                foreach (GridColumn clmn in gridView1.Columns)
                {
                    string a = clmn.OptionsFilter.ToString();
                    filter = filter.Replace(("[" + clmn.Caption + "]"), ("[" + clmn.FieldName + "]"));
                }

                filter = filter.Replace("m,", ",");
                filter = filter.Replace("m)", ")");

                //filtrelenmiþ rowlar -- Missing operand after 'm' operator. hatasý veriyordu.
                DataRow[] rows = DTSource.Select(filter);

                GridColumn col = gridView1.Columns[e.Column.FieldName];

                #region "filtre seçeneklerine boþ kolonu da eklemek için"
                if (gridView1.Columns.Count > 0)
                {
                    //foreach (GridColumn clmn in gridView1.Columns)
                    //{
                    //    foreach (DataRow row in rows)
                    //    {
                    //        switch (col.DisplayFormat.FormatType)
                    //        {
                    //            case FormatType.DateTime:
                    //                if (row[clmn.FieldName] == null || row[clmn.FieldName].ToString() == DBNull.Value.ToString())
                    //                {
                    //                    row[clmn.FieldName] = DBNull.Value;
                    //                }
                    //                //MessageBox.Show("ok");
                    //                break;
                    //            default:
                    //                if (row[clmn.FieldName] == null || row[clmn.FieldName].ToString() == DBNull.Value.ToString())
                    //                {
                    //                    row[clmn.FieldName] = "";
                    //                }
                    //                break;
                    //        }
                    //    }
                    //}

                    //if (row[e.Column.FieldName] == null || row[e.Column.FieldName].ToString() == DBNull.Value.ToString())
                    //{
                    //    row[e.Column.FieldName] = "";
                    //}
                }
                #endregion

                //popupfilter tipi checkedboxlist olduðunda metind dýþýndaki kolonlar için boþ seçeneði gelmiyor
                //o yüzden bu kolonlar için listeye manual ekleyeceðiz

                bool bos_ekle = false;

                #region "filtrelenmiþ rowlar içindeki kolon deðer listesi"
                List<object> objects_top = new List<object>();
                foreach (DataRow row in rows)
                {
                    if ((row[e.Column.FieldName] == null) || (String.IsNullOrEmpty(row[e.Column.FieldName].ToString())))
                    {
                        bos_ekle = true;
                        //if (!objects_top.Contains(null)) // || !objects_top.Contains(string.Empty))
                        //     objects_top.Add(null);
                    }
                    else
                    {
                        switch (col.DisplayFormat.FormatType)
                        {
                            case FormatType.Numeric:
                                if (!objects_top.Contains(Convert.ToDouble(row[e.Column.FieldName])))
                                    objects_top.Add(Convert.ToDouble(row[e.Column.FieldName]));
                                break;
                            case FormatType.DateTime:
                                if (!objects_top.Contains(Convert.ToDateTime(row[e.Column.FieldName])))
                                    objects_top.Add(Convert.ToDateTime(row[e.Column.FieldName]));
                                break;
                            case FormatType.Custom:
                                if (col.DisplayFormat.FormatString.Contains("{0:n"))
                                {
                                    if (!objects_top.Contains(Convert.ToDouble(row[e.Column.FieldName])))
                                        objects_top.Add(Convert.ToDouble(row[e.Column.FieldName]));
                                }
                                else
                                {
                                    if (!objects_top.Contains(row[e.Column.FieldName].ToString()))
                                        objects_top.Add(row[e.Column.FieldName].ToString());
                                }
                                break;
                            default:
                                if (!objects_top.Contains(row[e.Column.FieldName].ToString()))
                                    objects_top.Add(row[e.Column.FieldName].ToString());
                                break;
                        }
                    }
                }
                #endregion

                List<int> list_top = new List<int>();
                List<int> list_bottom = new List<int>();

                #region index sýralamasý için
                for (int i = 0; i < e.CheckedComboBox.Items.Count; i++) // count ' a boþ olan rowlar kolon olarak düþmüyor.
                {
                    CheckedListBoxItem item = e.CheckedComboBox.Items[i];

                    //if (item.Value == null)
                    //{
                    //    list_bottom.Add(i);
                    //else
                    //    list_top.Add(i);
                    //}
                    //else
                    //{
                    switch (col.DisplayFormat.FormatType)
                    {
                        case FormatType.Numeric:
                            if (!objects_top.Contains(Convert.ToDouble(item.Value.ToString())))
                                list_bottom.Add(i);
                            else
                                list_top.Add(i);
                            break;
                        case FormatType.DateTime:
                            if (!objects_top.Contains(Convert.ToDateTime(item.Value.ToString())))
                                list_bottom.Add(i);
                            else
                                list_top.Add(i);
                            break;
                        case FormatType.Custom:
                            if (col.DisplayFormat.FormatString.Contains("{0:n"))
                            {
                                if (!objects_top.Contains(Convert.ToDouble(item.Value.ToString())))
                                    list_bottom.Add(i);
                                else
                                    list_top.Add(i);
                            }
                            else
                            {
                                if (!objects_top.Contains(item.Value.ToString()))
                                    list_bottom.Add(i);
                                else
                                    list_top.Add(i);
                            }
                            break;
                        default:
                            if (!objects_top.Contains(item.Value.ToString()))
                                list_bottom.Add(i);
                            else
                                list_top.Add(i);
                            break;
                    }
                    //}
                }
                #endregion

                //selected.Reverse();

                #region listenin kaynaðý þekillendiriliyor
                CheckedListBoxItemCollection items = new CheckedListBoxItemCollection();

                foreach (int index in list_top)
                {
                    items.Add(e.CheckedComboBox.Items[index]);
                }
                foreach (int index in list_bottom)
                {
                    items.Add(e.CheckedComboBox.Items[index]);
                }

                e.CheckedComboBox.Items.Clear();
                if (bos_ekle)
                {
                    FilterItem fi = new FilterItem("(Boþluklar)", new ColumnFilterInfo(new OperandProperty(e.Column.FieldName).IsNull()));
                    CheckedListBoxItem item = new CheckedListBoxItem(fi);
                    e.CheckedComboBox.Items.Add(item);
                }
                foreach (CheckedListBoxItem item in items)
                {
                    e.CheckedComboBox.Items.Add(item);
                }
                #endregion

                #endregion

                if (!String.IsNullOrEmpty(filter))
                    CheckedComboBox_Popup_top_count = list_top.Count;
                //}

                e.CheckedComboBox.Popup += CheckedComboBox_Popup; //açmayý //unutma 03.04.2012

            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                MessageBox.Show("sorguyu kontrol ediniz.", ex.Message);
                //throw exception ;
            }
        }

        void CheckedComboBox_Popup(object sender, EventArgs e) //unutma 03.04.2012
        {
            CheckedComboBoxEdit edit = sender as CheckedComboBoxEdit;
            PopupContainerForm popup = (edit as IPopupControl).PopupWindow as PopupContainerForm;
            CheckedListBoxControl listBox = popup.Controls[2].Controls[0] as CheckedListBoxControl;
            listBox.DrawItem += listBox_DrawItem;
            popup.OkButton.Click += new EventHandler(OkButton_Click);
        }

        void OkButton_Click(object sender, EventArgs e)
        {
            gridView1.ApplyColumnsFilter(); //unutma 03.04.2012
        }

        void listBox_DrawItem(object sender, ListBoxDrawItemEventArgs e)   //unutma 03.04.2012
        {
            #region MyRegion
            CheckedListBoxControl control = sender as CheckedListBoxControl;
            if (CheckedComboBox_Popup_top_count > 0)
            {
                if (e.Index <= CheckedComboBox_Popup_top_count)
                {
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    e.Appearance.ForeColor = Color.DarkGray;
                }
            }
            #endregion
        }

        private void barButtonItemGridExportToText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool oldexcel = false;
            SaveFileDialog DialogSave = new SaveFileDialog();
            DialogSave.AddExtension = true;
            DialogSave.RestoreDirectory = true;
            //DialogSave.Title = "Where do you want to save the file?";
            DialogSave.InitialDirectory = @"C:/";

            #region "file type"
            switch (e.Item.Name)
            {
                case "barButtonItemGridExportToText":
                    DialogSave.DefaultExt = "txt";
                    DialogSave.Filter = "Text File (*.txt)|*.txt";
                    break;
                case "barButtonItemGridExportToHtml":
                    DialogSave.DefaultExt = "html";
                    DialogSave.Filter = "HTML File (*.html)|*.html";
                    break;
                case "barButtonItemGridExportToPdf":
                    DialogSave.DefaultExt = "pdf";
                    DialogSave.Filter = "PDF File (*.pdf)|*.pdf";
                    break;
                case "barButtonItemGridExportToExcel":
                    string office_version = Grid.Utils.GetOfficeVersion();
                    oldexcel = false;
                    if (!string.IsNullOrEmpty(office_version) && 
                        Convert.ToInt32(office_version) >= 2007) // < 2007
                    {
                        DialogSave.DefaultExt = "xlsx";
                        DialogSave.Filter = "EXCEL File (*.xlsx)|*.xlsx";
                    }
                    else
                    {
                        DialogSave.DefaultExt = "xlsx";
                        DialogSave.Filter = "EXCEL File (*.xls)|*.xls";
                        oldexcel = true;
                    }
                    break;
            }
            #endregion

            string filePath = null;
            if (DialogSave.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            filePath = DialogSave.FileName;
            DialogSave.Dispose();
            DialogSave = null;

            try
            {
                //grid.ExportToXlsx(filePath);

                //printableComponentLink1.ShowPreviewDialog();
                printableComponentLink1.CreateDocument();  // Exception of type 'System.OutOfMemoryException' was thrown.
                //printableComponentLink1.ShowPreview();
                //printableComponentLink1.PrintingSystem.ExportToPdf(@"c:\gridcontrol.pdf");
            }
            catch (Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                MessageBox.Show(ex.Message);
                return;
            }
            switch (e.Item.Name)
            {
                case "barButtonItemGridExportToText":
                    #region "eski hali"
                    //oGridView.ExportToText(filePath);
                    //printableComponentLink1.PrintingSystem.ExportToText(filePath);
                    #endregion

                    #region "kaydetmeye çalýþýlan text dosyasý açýk ise tekrar dosyayý açýyor, hataya düþmüyor."
                    try
                    {
                        printableComponentLink1.PrintingSystem.ExportToText(filePath);
                    }
                    catch (IOException ex)
                    {
                        Grid.Utils.WriteLog(ex);
                    }
                    catch (Exception ex2)
                    {
                        Grid.Utils.WriteLog(ex2);
                        MessageBox.Show("Kaydetmeye çalýþýlan '" + Path.GetFileName(filePath) + "' dosyasý kullanýmdadýr, kaydedilemez.", "UYARI");
                        return;
                    }
                    #endregion
                    break;
                case "barButtonItemGridExportToHtml":
                    #region "eski hali"
                    //oGridView.ExportToHtml(filePath);
                    //printableComponentLink1.PrintingSystem.ExportToHtml(filePath);
                    #endregion

                    #region "kaydetmeye çalýþýlan html dosyasý açýk ise tekrar dosyayý açýyor, hataya düþmüyor."
                    try
                    {
                        printableComponentLink1.PrintingSystem.ExportToHtml(filePath);
                    }
                    catch (IOException ex)
                    {
                        Grid.Utils.WriteLog(ex);
                    }
                    catch (Exception ex2)
                    {
                        Grid.Utils.WriteLog(ex2);
                        MessageBox.Show("Kaydetmeye çalýþýlan '" + Path.GetFileName(filePath) + "' dosyasý kullanýmdadýr, kaydedilemez.", "UYARI");
                        return;
                    }
                    #endregion
                    break;
                case "barButtonItemGridExportToPdf":
                    #region "eski hali"
                    //oGridView.ExportToPdf(filePath);
                    //printableComponentLink1.PrintingSystem.ExportToPdf(filePath);
                    #endregion

                    #region "kaydetmeye çalýþýlan pdf dosyasý açýk ise hata kontrolüne düþer"
                    try
                    {
                        printableComponentLink1.PrintingSystem.ExportToPdf(filePath);
                    }
                    catch (Exception ex)
                    {
                        Grid.Utils.WriteLog(ex);
                        if (ex.Message.Contains("The output file is too big"))
                        {
                            MessageBox.Show("Kaydetmeye çalýþtýðýnýz dosya boyutu çok büyük. Satýr sayýsýný azaltmayý deneyiniz veya birkaç sayfaya bölünüz!");
                        }
                        else
                        {
                            MessageBox.Show(ex.Message);
                        }
                        return;
                    }
                    #endregion
                    break;
                
                case "barButtonItemGridExportToExcel":
                    //this.grid.ExportToXlsx(filePath);
                    //break;

                    // Orijinal hali

                    //printableComponentLink1.PrintingSystem.ExportOptions.Xls.SheetName = "Sheet1";
                    try
                    {

                        if (!oldexcel)
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            DevExpress.XtraPrinting.XlsxExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsxExportOptions();
                            oGridView.ExportToXlsx(filePath, xlsxOptions);
                            oGridView.ExportToXlsx(filePath);
                            printableComponentLink1.PrintingSystem.ExportToXlsx(filePath);
                        }
                        else
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            DevExpress.XtraPrinting.XlsExportOptions xlsOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                            oGridView.ExportToXls(filePath, xlsOptions);
                            oGridView.ExportToXls(filePath);
                            printableComponentLink1.PrintingSystem.ExportToXls(filePath);
                        }
                       

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Lütfen Excel Versiyonunuzu Kontrol Ediniz. ");
                    }
                    break;
                    /*
                    #region "kaydetmeye çalýþýlan excel dosyasý açýk ise hata kontrolüne düþer"
                    try
                    {
                        if (filePath.EndsWith(".xlsx"))
                            printableComponentLink1.PrintingSystem.ExportToXlsx(filePath);
                        else
                            printableComponentLink1.PrintingSystem.ExportToXls(filePath);
                    }
                    catch (IOException ex)
                    {
                        Grid.Utils.WriteLog(ex);
                        if (ex.Message.Contains("The output file is too big"))
                        {
                            MessageBox.Show("Kaydetmeye çalýþtýðýnýz dosya boyutu çok büyük. Satýr sayýsýný azaltmayý deneyiniz veya birkaç sayfaya bölünüz!");
                            return;
                        }
                    }
                    catch (Exception ex2)
                    {
                        Grid.Utils.WriteLog(ex2);
                        if (ex2.Message.Contains("The output file is too big"))
                        {
                            MessageBox.Show("Kaydetmeye çalýþtýðýnýz dosya boyutu çok büyük. Satýr sayýsýný azaltmayý deneyiniz veya birkaç sayfaya bölünüz!");
                        }
                        else
                        {
                            MessageBox.Show("Kaydetmeye çalýþýlan '" + dosya_adi + "' dosyasý kullanýmdadýr, kaydedilemez.", "UYARI");
                        }
                        return;
                    }
                    #endregion
                    break;*/
            }

            if (DialogResult.Yes == MessageBox.Show("Dosyanýn görüntülenmesini istiyor musunuz?", "Dosya Aç", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = filePath;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                catch (System.ComponentModel.Win32Exception ex) 
                {
                    Grid.Utils.WriteLog(ex);
                }
            }
        }

        //The class that stores menu specific information
        class MenuInfo
        {
            public GridColumn Column;
            public FixedStyle Style;
            public DevExpress.Utils.DefaultBoolean AllowMerge;

            public MenuInfo(GridColumn column, FixedStyle style)
            {
                this.Column = column;
                this.Style = style;
            }

            public MenuInfo(GridColumn column, DevExpress.Utils.DefaultBoolean merge)
            {
                this.Column = column;
                this.AllowMerge = merge;
            }
        }

        private void barButtonItemHesaplananAlanEkle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //MessageBox.Show("OnNewUnboundFieldClick");

            //foreach (Grid.Xml.Column pgf in Grid) // foreach (PivotGridField pgf in pivotGridContent.Fields)
            //{
            //    if (pgf.UnboundFieldName.StartsWith("unbound_field")) unbound_field_count++;
            //}
            //unbound_field_count++;

            //AddUnboundFieldForm dialog = new AddUnboundFieldForm(pivotGridContent.Fields);
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    //TEST Unbound Field
            //    PivotGridField unbound_field = new PivotGridField();
            //    unbound_field.Area = PivotArea.DataArea;
            //    unbound_field.Caption = dialog.Caption;
            //    unbound_field.Name = ("unbound_field_name" + unbound_field_count.ToString());
            //    unbound_field.UnboundFieldName = ("unbound_field" + unbound_field_count.ToString());
            //    unbound_field.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            //    unbound_field.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    unbound_field.CellFormat.FormatString = dialog.FormatString;//"n0";
            //    unbound_field.Options.ShowUnboundExpressionMenu = true;
            //    unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
            //    switch (dialog.SummaryType)
            //    {
            //        case "Ortalama"://"Average":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Average;
            //            break;
            //        case "Sayý"://"Count":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            //            break;
            //        case "Kiþisel"://"Custom":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
            //            break;
            //        case "Maksimum"://"Max":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            //            break;
            //        case "Minimum"://"Min":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            //            break;
            //        case "Standart Sapma"://"StdDev":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.StdDev;
            //            break;
            //        case "Standart Sapma P"://"StdDevp":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.StdDevp;
            //            break;
            //        case "Toplam"://"Sum":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
            //            break;
            //        case "Deðiþken"://"Var":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Var;
            //            break;
            //        case "Deðiþken P"://"Varp":
            //            unbound_field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Varp;
            //            break;
            //    }
            //    unbound_field.Tag = dialog.SummaryFormula;
            //    pivotGridContent.Fields.Add(unbound_field);
            //    HashFieldNameCaption[unbound_field.UnboundFieldName] = unbound_field.Caption;
            //}
        }

        //public static void SaveToExcel(string location, DataSet ds)
        public static void SaveToExcel(string location, DataTable table)
        {
            //Creae an Excel application instance
            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            var excelWorkBook = excelApp.Workbooks.Add();

            excelWorkBook.Application.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual;
            //foreach (DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                var excelWorkSheet = excelWorkBook.Sheets.Add();
                //excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    if (j % 100 == 0) Trace.WriteLine("ROWS " + j);

                    var row = excelWorkSheet.Rows[j + 2];
                    var itarr = table.Rows[j].ItemArray;
                    int count = table.Columns.Count;
                    count = 10;
                    for (int k = 0; k < count; k++)
                    {
                        //excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                        row.Cells[k + 1] = itarr[k].ToString();
                    }
                }
            }

            //excelWorkBook.SaveAs(location);
            excelWorkBook.Application.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic;
            //excelWorkBook.Close();
            excelApp.Visible = true;
            //excelApp.Quit();

        }
 
 

    }
}