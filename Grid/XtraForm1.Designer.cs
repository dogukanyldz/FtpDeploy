namespace Grid
{
    partial class XtraForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm1));
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItemGridExportTo = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemGridExportToText = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridExportToHtml = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridExportToPdf = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridExportToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridSaveLayout = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridRefreshData = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridGroupPanelFullExpand = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridGroupPanelFullCollapse = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridGroupPanelClearGrouping = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridShowCustomizationWindow = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridShowFilterBuilderWindow = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGridDeleteLayout = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrintScreen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCopySelected = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHesaplananAlanEkle = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ýmageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItemGridSatirAltToplam = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItemGridSatirGenelToplam = new DevExpress.XtraBars.BarCheckItem();
            this.popupMenuGrid = new DevExpress.XtraBars.PopupMenu(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            this.cmbTemplates = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDeleteTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.lblTemplates = new DevExpress.XtraEditors.LabelControl();
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ýmageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplates.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(2, 2);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1144, 462);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.grid_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.ýmageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItemGridExportTo,
            this.barButtonItemGridShowCustomizationWindow,
            this.barButtonItemGridRefreshData,
            this.barButtonItemGridExportToText,
            this.barButtonItemGridExportToHtml,
            this.barButtonItemGridExportToPdf,
            this.barButtonItemGridExportToExcel,
            this.barButtonItemGridSaveLayout,
            this.barButtonItemGridGroupPanelFullExpand,
            this.barButtonItemGridGroupPanelFullCollapse,
            this.barButtonItemGridGroupPanelClearGrouping,
            this.barButtonItemGridDeleteLayout,
            this.barButtonItemPrintScreen,
            this.barButtonItemGridShowFilterBuilderWindow,
            this.barButtonItemCopy,
            this.barButtonItemCopySelected,
            this.barCheckItemGridSatirAltToplam,
            this.barCheckItemGridSatirGenelToplam,
            this.barButtonItem1,
            this.barButtonItemHesaplananAlanEkle});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 27;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemGridExportTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridSaveLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridRefreshData),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridGroupPanelFullExpand),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridGroupPanelFullCollapse),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridGroupPanelClearGrouping),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridShowCustomizationWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridShowFilterBuilderWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridDeleteLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPrintScreen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCopySelected),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemHesaplananAlanEkle)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItemGridExportTo
            // 
            this.barSubItemGridExportTo.Caption = "Dosyaya Yaz";
            this.barSubItemGridExportTo.Id = 7;
            this.barSubItemGridExportTo.ImageIndex = 5;
            this.barSubItemGridExportTo.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridExportToText),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridExportToHtml),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridExportToPdf),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGridExportToExcel)});
            this.barSubItemGridExportTo.Name = "barSubItemGridExportTo";
            this.barSubItemGridExportTo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItemGridExportToText
            // 
            this.barButtonItemGridExportToText.Caption = "Text";
            this.barButtonItemGridExportToText.Id = 10;
            this.barButtonItemGridExportToText.ImageIndex = 0;
            this.barButtonItemGridExportToText.Name = "barButtonItemGridExportToText";
            this.barButtonItemGridExportToText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridExportToText_ItemClick);
            // 
            // barButtonItemGridExportToHtml
            // 
            this.barButtonItemGridExportToHtml.Caption = "Html";
            this.barButtonItemGridExportToHtml.Id = 11;
            this.barButtonItemGridExportToHtml.ImageIndex = 1;
            this.barButtonItemGridExportToHtml.Name = "barButtonItemGridExportToHtml";
            this.barButtonItemGridExportToHtml.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridExportToText_ItemClick);
            // 
            // barButtonItemGridExportToPdf
            // 
            this.barButtonItemGridExportToPdf.Caption = "Pdf";
            this.barButtonItemGridExportToPdf.Id = 12;
            this.barButtonItemGridExportToPdf.ImageIndex = 2;
            this.barButtonItemGridExportToPdf.Name = "barButtonItemGridExportToPdf";
            this.barButtonItemGridExportToPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridExportToText_ItemClick);
            // 
            // barButtonItemGridExportToExcel
            // 
            this.barButtonItemGridExportToExcel.Caption = "Excel";
            this.barButtonItemGridExportToExcel.Id = 13;
            this.barButtonItemGridExportToExcel.ImageIndex = 3;
            this.barButtonItemGridExportToExcel.Name = "barButtonItemGridExportToExcel";
            this.barButtonItemGridExportToExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridExportToText_ItemClick);
            // 
            // barButtonItemGridSaveLayout
            // 
            this.barButtonItemGridSaveLayout.Caption = "Ayarlarý Kaydet";
            this.barButtonItemGridSaveLayout.Id = 14;
            this.barButtonItemGridSaveLayout.ImageIndex = 4;
            this.barButtonItemGridSaveLayout.Name = "barButtonItemGridSaveLayout";
            this.barButtonItemGridSaveLayout.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridSaveLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridRefreshData
            // 
            this.barButtonItemGridRefreshData.Caption = "Veriyi Güncelle";
            this.barButtonItemGridRefreshData.Id = 9;
            this.barButtonItemGridRefreshData.ImageIndex = 7;
            this.barButtonItemGridRefreshData.Name = "barButtonItemGridRefreshData";
            this.barButtonItemGridRefreshData.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridRefreshData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridGroupPanelFullExpand
            // 
            this.barButtonItemGridGroupPanelFullExpand.Caption = "Hepsini Aç";
            this.barButtonItemGridGroupPanelFullExpand.Id = 15;
            this.barButtonItemGridGroupPanelFullExpand.ImageIndex = 8;
            this.barButtonItemGridGroupPanelFullExpand.Name = "barButtonItemGridGroupPanelFullExpand";
            this.barButtonItemGridGroupPanelFullExpand.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridGroupPanelFullExpand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridGroupPanelFullCollapse
            // 
            this.barButtonItemGridGroupPanelFullCollapse.Caption = "Hepsini Kapat";
            this.barButtonItemGridGroupPanelFullCollapse.Id = 16;
            this.barButtonItemGridGroupPanelFullCollapse.ImageIndex = 9;
            this.barButtonItemGridGroupPanelFullCollapse.Name = "barButtonItemGridGroupPanelFullCollapse";
            this.barButtonItemGridGroupPanelFullCollapse.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridGroupPanelFullCollapse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridGroupPanelClearGrouping
            // 
            this.barButtonItemGridGroupPanelClearGrouping.Caption = "Gruplamayý Temizle";
            this.barButtonItemGridGroupPanelClearGrouping.Id = 17;
            this.barButtonItemGridGroupPanelClearGrouping.ImageIndex = 10;
            this.barButtonItemGridGroupPanelClearGrouping.Name = "barButtonItemGridGroupPanelClearGrouping";
            this.barButtonItemGridGroupPanelClearGrouping.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridGroupPanelClearGrouping.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridShowCustomizationWindow
            // 
            this.barButtonItemGridShowCustomizationWindow.Caption = "Kolon Penceresi";
            this.barButtonItemGridShowCustomizationWindow.Id = 8;
            this.barButtonItemGridShowCustomizationWindow.ImageIndex = 6;
            this.barButtonItemGridShowCustomizationWindow.Name = "barButtonItemGridShowCustomizationWindow";
            this.barButtonItemGridShowCustomizationWindow.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridShowCustomizationWindow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridShowFilterBuilderWindow
            // 
            this.barButtonItemGridShowFilterBuilderWindow.Caption = "Süzme Penceresi";
            this.barButtonItemGridShowFilterBuilderWindow.Id = 19;
            this.barButtonItemGridShowFilterBuilderWindow.ImageIndex = 12;
            this.barButtonItemGridShowFilterBuilderWindow.Name = "barButtonItemGridShowFilterBuilderWindow";
            this.barButtonItemGridShowFilterBuilderWindow.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridShowFilterBuilderWindow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemGridDeleteLayout
            // 
            this.barButtonItemGridDeleteLayout.Caption = "Ayarlarý Sil";
            this.barButtonItemGridDeleteLayout.Id = 18;
            this.barButtonItemGridDeleteLayout.ImageIndex = 11;
            this.barButtonItemGridDeleteLayout.Name = "barButtonItemGridDeleteLayout";
            this.barButtonItemGridDeleteLayout.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemGridDeleteLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemPrintScreen
            // 
            this.barButtonItemPrintScreen.Caption = "Ekraný Yaz";
            this.barButtonItemPrintScreen.Id = 19;
            this.barButtonItemPrintScreen.ImageIndex = 0;
            this.barButtonItemPrintScreen.Name = "barButtonItemPrintScreen";
            this.barButtonItemPrintScreen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemPrintScreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItemCopySelected
            // 
            this.barButtonItemCopySelected.Caption = "Seçili Alaný Kopyala";
            this.barButtonItemCopySelected.Id = 21;
            this.barButtonItemCopySelected.ImageIndex = 13;
            this.barButtonItemCopySelected.Name = "barButtonItemCopySelected";
            this.barButtonItemCopySelected.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItemCopySelected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGridShowCustomizationWindow_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 25;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItemHesaplananAlanEkle
            // 
            this.barButtonItemHesaplananAlanEkle.Caption = "Hesaplanan Alan Ekle";
            this.barButtonItemHesaplananAlanEkle.Id = 26;
            this.barButtonItemHesaplananAlanEkle.Name = "barButtonItemHesaplananAlanEkle";
            this.barButtonItemHesaplananAlanEkle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHesaplananAlanEkle_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1148, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 552);
            this.barDockControlBottom.Size = new System.Drawing.Size(1148, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 52);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 500);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1148, 52);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 500);
            // 
            // ýmageCollection1
            // 
            this.ýmageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ýmageCollection1.ImageStream")));
            // 
            // barButtonItemCopy
            // 
            this.barButtonItemCopy.Caption = "Kopyala";
            this.barButtonItemCopy.Id = 20;
            this.barButtonItemCopy.Name = "barButtonItemCopy";
            this.barButtonItemCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCopy_ItemClick);
            // 
            // barCheckItemGridSatirAltToplam
            // 
            this.barCheckItemGridSatirAltToplam.Caption = "Satýr Alt Toplam";
            this.barCheckItemGridSatirAltToplam.Id = 22;
            this.barCheckItemGridSatirAltToplam.Name = "barCheckItemGridSatirAltToplam";
            this.barCheckItemGridSatirAltToplam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemGridSatirAltToplam_ItemClick);
            // 
            // barCheckItemGridSatirGenelToplam
            // 
            this.barCheckItemGridSatirGenelToplam.Caption = "Satýr Genel Toplam";
            this.barCheckItemGridSatirGenelToplam.Id = 23;
            this.barCheckItemGridSatirGenelToplam.Name = "barCheckItemGridSatirGenelToplam";
            this.barCheckItemGridSatirGenelToplam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemGridSatirGenelToplam_ItemClick);
            // 
            // popupMenuGrid
            // 
            this.popupMenuGrid.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemGridSatirAltToplam, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemGridSatirGenelToplam)});
            this.popupMenuGrid.Manager = this.barManager1;
            this.popupMenuGrid.Name = "popupMenuGrid";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Black";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.cmbTemplates);
            this.panelTop.Controls.Add(this.btnDeleteTemplate);
            this.panelTop.Controls.Add(this.btnSaveTemplate);
            this.panelTop.Controls.Add(this.lblTemplates);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 52);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1148, 34);
            this.panelTop.TabIndex = 4;
            // 
            // cmbTemplates
            // 
            this.cmbTemplates.Location = new System.Drawing.Point(43, 6);
            this.cmbTemplates.Name = "cmbTemplates";
            // 
            // 
            // 
            this.cmbTemplates.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTemplates.Properties.View = this.gridLookUpEdit1View;
            this.cmbTemplates.Size = new System.Drawing.Size(288, 20);
            this.cmbTemplates.TabIndex = 20;
            this.cmbTemplates.EditValueChanged += new System.EventHandler(this.cmbTemplates_EditValueChanged);
            this.cmbTemplates.TextChanged += new System.EventHandler(this.cmbTemplates_TextChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btnDeleteTemplate
            // 
            this.btnDeleteTemplate.Location = new System.Drawing.Point(462, 3);
            this.btnDeleteTemplate.Name = "btnDeleteTemplate";
            this.btnDeleteTemplate.Size = new System.Drawing.Size(106, 23);
            this.btnDeleteTemplate.TabIndex = 18;
            this.btnDeleteTemplate.Text = "Þablonu Sil";
            this.btnDeleteTemplate.Click += new System.EventHandler(this.btnDeleteTemplate_Click);
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Location = new System.Drawing.Point(349, 3);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(106, 23);
            this.btnSaveTemplate.TabIndex = 17;
            this.btnSaveTemplate.Text = "Þablonu Kaydet";
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // lblTemplates
            // 
            this.lblTemplates.Location = new System.Drawing.Point(5, 9);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new System.Drawing.Size(32, 13);
            this.lblTemplates.TabIndex = 16;
            this.lblTemplates.Text = "Þablon";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.grid);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 86);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1148, 466);
            this.panelContent.TabIndex = 5;
            // 
            // printingSystem1
            // 
            this.printingSystem1.ExportOptions.Xlsx.ShowGridLines = true;
            // 
            // printableComponentLink1
            // 
            this.printableComponentLink1.Component = this.grid;
            this.printableComponentLink1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.printableComponentLink1.PrintingSystemBase = this.printingSystem1;
            this.printableComponentLink1.CreateReportHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink1_CreateReportHeaderArea);
            this.printableComponentLink1.CreateReportFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.printableComponentLink1_CreateReportFooterArea);
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 552);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XtraForm1";
            this.Text = "Grid";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ýmageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplates.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenuGrid;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.Utils.ImageCollection ýmageCollection1;
        private DevExpress.XtraBars.BarSubItem barSubItemGridExportTo;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridShowCustomizationWindow;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridRefreshData;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridExportToText;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridExportToHtml;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridExportToPdf;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridExportToExcel;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridSaveLayout;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridGroupPanelFullExpand;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridGroupPanelFullCollapse;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridGroupPanelClearGrouping;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridDeleteLayout;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrintScreen;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGridShowFilterBuilderWindow;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopySelected;
        private DevExpress.XtraEditors.PanelControl panelTop;
        private DevExpress.XtraEditors.GridLookUpEdit cmbTemplates;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btnDeleteTemplate;
        private DevExpress.XtraEditors.SimpleButton btnSaveTemplate;
        private DevExpress.XtraEditors.LabelControl lblTemplates;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
        private DevExpress.XtraBars.BarCheckItem barCheckItemGridSatirAltToplam;
        private DevExpress.XtraBars.BarCheckItem barCheckItemGridSatirGenelToplam;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHesaplananAlanEkle;

    }
}