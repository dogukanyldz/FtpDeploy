using System;
using System.Data;
using System.Configuration;
using DevExpress.XtraGrid.Localization;

public class GridLocalizer_TR : DevExpress.XtraGrid.Localization.GridLocalizer
{
    private string _MenuFooterAverageFormat = "ORT={0}";

    public string MenuFooterAverageFormat
    {
        get { return _MenuFooterAverageFormat; }
        set { _MenuFooterAverageFormat = value; }
    }

    private string _MenuFooterCountFormat = "SAYI={0}";

    public string MenuFooterCountFormat
    {
        get { return _MenuFooterCountFormat; }
        set { _MenuFooterCountFormat = value; }
    }

    private string _MenuFooterCountGroupFormat = "{0}";

    public string MenuFooterCountGroupFormat
    {
        get { return _MenuFooterCountGroupFormat; }
        set { _MenuFooterCountGroupFormat = value; }
    }

    private string _MenuFooterCustomFormat = "{0}";

    public string MenuFooterCustomFormat
    {
        get { return _MenuFooterCustomFormat; }
        set { _MenuFooterCustomFormat = value; }
    }

    private string _MenuFooterMaxFormat = "MAX={0}";

    public string MenuFooterMaxFormat
    {
        get { return _MenuFooterMaxFormat; }
        set { _MenuFooterMaxFormat = value; }
    }

    private string _MenuFooterMinFormat = "MIN={0}";

    public string MenuFooterMinFormat
    {
        get { return _MenuFooterMinFormat; }
        set { _MenuFooterMinFormat = value; }
    }

    private string _MenuFooterSumFormat = "TOP={0}";

    public string MenuFooterSumFormat
    {
        get { return _MenuFooterSumFormat; }
        set { _MenuFooterSumFormat = value; }
    }

    public GridLocalizer_TR() { }

    public override string GetLocalizedString(GridStringId id)
    {
        switch (id)
        {
            //case GridStringId.CardViewCaptionFormat: return "Yeni Kart"; //	New Card
            case GridStringId.CardViewNewCard: return "Yeni Kart"; //	New Card
            case GridStringId.CardViewQuickCustomizationButton: return "Kiþiselleþtir"; //	Customize
            case GridStringId.CardViewQuickCustomizationButtonFilter: return "Filtre"; //	Filter
            case GridStringId.CardViewQuickCustomizationButtonSort: return "Sýrala"; //	Sort:
            case GridStringId.ColumnViewExceptionMessage: return "Deðeri Düzeltmek Ýstiyormusunuz?"; //	Do you want to correct the value ?
            case GridStringId.CustomFilterDialog2FieldCheck: return "Alan"; //	Field
            case GridStringId.CustomFilterDialogCancelButton: return "Ýptal"; //	&Cancel
            case GridStringId.CustomFilterDialogCaption: return "Satýrlarýn Yerini Göster"; //	Show rows where:
            case GridStringId.CustomFilterDialogClearFilter: return "Filtreyi Temizle"; //	C&lear Filter
            case GridStringId.CustomFilterDialogFormCaption: return "Kiþisel Otomatik Filtrele"; //	Custom AutoFilter
            case GridStringId.CustomFilterDialogOkButton: return "Tamam"; //	&OK
            case GridStringId.CustomFilterDialogRadioAnd: return "Ve"; //	&And
            case GridStringId.CustomFilterDialogRadioOr: return "Veya"; //	O&r
            case GridStringId.CustomizationBands: return "Bantlar"; //	Bands
            case GridStringId.CustomizationCaption: return "Kiþiselleþtirme"; //	Customization
            case GridStringId.CustomizationColumns: return "Sütunlar"; //	Columns
            case GridStringId.CustomizationFormBandHint: return "Kiþiselleþtirmek için Bantlarý Buraya Sürükle- Býrak"; //	Drag and drop bands here to customize layout
            case GridStringId.CustomizationFormColumnHint: return "Kiþiselleþtirmek için Sütunlarý buraya Sürükle Býrak"; //	Drag and drop columns here to customize layout
            case GridStringId.FileIsNotFoundError: return "{0} Dosya Bulunamadý"; //	File {0} is not found
            case GridStringId.FilterBuilderApplyButton: return "Uygula"; //	&Apply
            case GridStringId.FilterBuilderCancelButton: return "Ýptal"; //	&Cancel
            case GridStringId.FilterBuilderCaption: return "Filtre Oluþturucu"; //	Filter Builder
            case GridStringId.FilterBuilderOkButton: return "Tamam"; //	&OK
            case GridStringId.FilterPanelCustomizeButton: return "Filtreyi Düzenle"; //	Edit Filter
            case GridStringId.GridGroupPanelText: return "Bu sütunu gruplayabilmek için sütun baþlýðýný buraya sürükleyin"; //	Drag a column header here to group by that column
            case GridStringId.GridNewRowText: return "Yeni bir Kayýt eklemek için buraya týklayýn"; //	Click here to add a new row
            case GridStringId.GridOutlookIntervals: return "Daha Önce;Geçen Ay;Bu Aydan Önce;Üç Hafta Önce;Ýki Hafta Önce;Geçen Hafta;;;;;;;;Dün;Bugün;Yarýn;;;;;;;;Bir Sonraki Hafta;Ýki Hafta Sonra;Üç Hafta Sonra;Bu Aydan Sonra;Bir Sonraki Ay;Bir Sonraki Aydan Sonra;"; //	Older;Last Month;Earlier this Month;Three Weeks Ago;Two Weeks Ago;Last Week;;;;;;;;Yesterday;Today;Tomorrow;;;;;;;;Next Week;Two Weeks Away;Three Weeks Away;Later this Month;Next Month;Beyond Next Month;
            case GridStringId.LayoutModifiedWarning: return "Düzen  Deðiþtirildi. Deðiþiklikleri Kaydetmek Ýstiyormusunuz ?"; //	The layout has been modified. Do you want to save the changes?
            case GridStringId.LayoutViewButtonApply: return "Uygula"; //	&Apply
            case GridStringId.LayoutViewButtonCancel: return "Ýptal"; //	&Cancel
            case GridStringId.LayoutViewButtonCustomizeHide: return "Kiþiselleþtirmeyi Sakla"; //	Hide Customi&zation
            case GridStringId.LayoutViewButtonCustomizeShow: return "Kiþiselleþtirmeyi Göster"; //	&Show Customization
            case GridStringId.LayoutViewButtonLoadLayout: return "Yerleþimi Yükle"; //	&Load Layout...
            case GridStringId.LayoutViewButtonOk: return "Tamam"; //	&Ok
            case GridStringId.LayoutViewButtonPreview: return "Daha Fazla Kart Göster"; //	Show &More Cards
            case GridStringId.LayoutViewButtonReset: return "Þablon Kartýný Sýfýrla"; //	&Reset Template Card
            case GridStringId.LayoutViewButtonSaveLayout: return "Düzeni Kaydet..."; //	Sa&ve Layout...
            case GridStringId.LayoutViewButtonShrinkToMinimum: return "Þablon Kartýný Küçült"; //	&Shrink Template Card
            case GridStringId.LayoutViewCarouselModeBtnHint: return "Carousel Modu"; //	Carousel Mode
            case GridStringId.LayoutViewCloseZoomBtnHintClose: return "Görünümü Yeniden Yükle"; //	Restore View
            case GridStringId.LayoutViewCloseZoomBtnHintZoom: return "Detaylarý Büyült"; //	Maximize Detail
            case GridStringId.LayoutViewColumnModeBtnHint: return "Bir Sütun"; //	One Column
            case GridStringId.LayoutViewCustomizationFormCaption: return "Düzen Görünümü Kiþiselleþtirme"; //	LayoutView Customization
            case GridStringId.LayoutViewCustomizationFormDescription: return "Kiþiselleþtirme Menüsü ve Sürükle-Býrak kullanarak  kart görünümünü kiþiselleþtir ve veriyi Görünüm Düzeni Sayfasýnda Önizle"; //	Customize the card layout using drag-and-drop and customization menu, and preview data in the View Layout page.
            case GridStringId.LayoutViewCustomizeBtnHint: return "Kiþiselleþtirme"; //	Customization
            case GridStringId.LayoutViewGroupCaptions: return "Baþlýklar"; //	Captions
            case GridStringId.LayoutViewGroupCards: return "Kartlar"; //	Cards
            case GridStringId.LayoutViewGroupCustomization: return "Kiþiselleþtirme"; //	Customization
            case GridStringId.LayoutViewGroupFields: return "Alanlar"; //	Fields
            case GridStringId.LayoutViewGroupHiddenItems: return "Saklý Öðeler"; //	Hidden Items
            case GridStringId.LayoutViewGroupIndents: return "Girintiler"; //	Indents
            case GridStringId.LayoutViewGroupIntervals: return "Aralýklar"; //	Intervals
            case GridStringId.LayoutViewGroupLayout: return "Düzen"; //	Layout
            case GridStringId.LayoutViewGroupPropertyGrid: return "Özellikler Tablosu"; //	Property Grid
            case GridStringId.LayoutViewGroupTreeStructure: return "Düzen Aðacý Görünümü"; //	Layout Tree View
            case GridStringId.LayoutViewGroupView: return "Görünüm"; //	View
            case GridStringId.LayoutViewLabelAllowFieldHotTracking: return "Hot-Tracking e izin ver"; //	Allow Hot-Tracking
            case GridStringId.LayoutViewLabelCaptionLocation: return "Alan Baþlýðý Konumu"; //	Field Caption Location:
            case GridStringId.LayoutViewLabelCardArrangeRule: return "Düzenleme Kuralý"; //	Arrange Rule:
            case GridStringId.LayoutViewLabelCardEdgeAlignment: return "Kart Kenar Hizalama"; //	Card Edge Alignment:
            case GridStringId.LayoutViewLabelGroupCaptionLocation: return "Grup Baþlýk Konumu"; //	Group Caption Location:
            case GridStringId.LayoutViewLabelHorizontal: return "Yatay Aralýk"; //	Horizontal Interval
            case GridStringId.LayoutViewLabelPadding: return "Doldurma"; //	Padding
            case GridStringId.LayoutViewLabelScrollVisibility: return "Kaydýrma Görünülürlüðü"; //	Scroll Visibility:
            case GridStringId.LayoutViewLabelShowCardBorder: return "Kenarlýðý Göster"; //	Show Border
            case GridStringId.LayoutViewLabelShowCardCaption: return "Baþlýðý Göster"; //	Show Caption
            case GridStringId.LayoutViewLabelShowCardExpandButton: return "Geniþ Butonu Göster"; //	Show Expand Button
            case GridStringId.LayoutViewLabelShowFieldBorder: return "Kenarlýðý Göster"; //	Show Border
            case GridStringId.LayoutViewLabelShowFieldHint: return "Ýpucu Göster"; //	Show Hint
            case GridStringId.LayoutViewLabelShowFilterPanel: return "Filtre Panelini Göster"; //	Show Filter Panel:
            case GridStringId.LayoutViewLabelShowHeaderPanel: return "Baþlýk Panelini Göster"; //	Show Header Panel
            case GridStringId.LayoutViewLabelShowLines: return "Satýrlarý Göster"; //	Show Lines
            case GridStringId.LayoutViewLabelSpacing: return "Aralýklandýrma"; //	Spacing
            case GridStringId.LayoutViewLabelTextAlignment: return "Alan Baþlýðý Metin Hizalama"; //	Field Caption Text Alignment:
            case GridStringId.LayoutViewLabelTextIndent: return "Metin Girintileri"; //	Text Indents
            case GridStringId.LayoutViewLabelVertical: return "Dikey Aralýk"; //	Vertical Interval
            case GridStringId.LayoutViewLabelViewMode: return "Dikey Mod"; //	View Mode:
            case GridStringId.LayoutViewMultiColumnModeBtnHint: return "Çoklu Sütunlar"; //	Multiple Columns
            case GridStringId.LayoutViewMultiRowModeBtnHint: return "Çoklu Kayýtlar"; //	Multiple Rows
            case GridStringId.LayoutViewPageTemplateCard: return "Þablon Kartý"; //	Template Card
            case GridStringId.LayoutViewPageViewLayout: return "Düzeni Görüntüle"; //	View Layout
            case GridStringId.LayoutViewPanBtnHint: return "Gezdirme"; //	Panning
            case GridStringId.LayoutViewRowModeBtnHint: return "Bir Kayýt"; //	One Row
            case GridStringId.LayoutViewSingleModeBtnHint: return "Bir Kart"; //	One Card

            case GridStringId.MenuColumnBestFit: return "En Uygun"; //	Best Fit
            case GridStringId.MenuColumnBestFitAllColumns: return "En Uygun (Tüm Sütunlar)"; //	Best Fit (all columns)
            case GridStringId.MenuColumnClearFilter: return "Filtreyi Temizle"; //	Clear Filter
            case GridStringId.MenuColumnClearSorting: return "Sýralamayý Temizle"; //	Clear Sorting
            case GridStringId.MenuColumnColumnCustomization: return "Sütun Seçimi"; //	Column Chooser
            case GridStringId.MenuColumnFilter: return "Filtrelenebilir mi?"; //	Can Filter
            case GridStringId.MenuColumnFilterEditor: return "Filtre Editörü"; //	Filter Editor
            case GridStringId.MenuColumnGroup: return "Bu sütuna göre grupla"; //	Group By This Column
            case GridStringId.MenuColumnGroupBox: return "Birçok sütuna göre grupla"; //	Group By Box
            case GridStringId.MenuColumnSortAscending: return "Artan Yönde Sýrala"; //	Sort Ascending
            case GridStringId.MenuColumnSortDescending: return "Azalan Yönde Sýrala"; //	Sort Descending
            case GridStringId.MenuColumnUnGroup: return "Gruplamayý Kaldýr"; //	UnGroup

            case GridStringId.MenuColumnAutoFilterRowHide: return "Otomatik Filtreleme Satýrýný Gizle";
            case GridStringId.MenuColumnAutoFilterRowShow: return "Otomatik Filtreleme Satýrýný Göster";
            //case GridStringId.MenuColumnAverageSummaryTypeDescription: return;
            //case GridStringId.MenuColumnCountSummaryTypeDescription: return;
            case GridStringId.MenuColumnExpressionEditor: return "Ýfade Editorü...";
            //case GridStringId.MenuColumnFilterMode: return;
            //case GridStringId.MenuColumnFilterModeDisplayText: return;
            //case GridStringId.MenuColumnFilterModeValue: return;
            case GridStringId.MenuColumnFindFilterHide: return "Arama Kutusunu Gizle";
            case GridStringId.MenuColumnFindFilterShow: return "Arama Kutusunu Göster";
            case GridStringId.MenuColumnGroupIntervalDay: return "Gün";
            case GridStringId.MenuColumnGroupIntervalMenu: return "Grup Aralýðý";
            case GridStringId.MenuColumnGroupIntervalMonth: return "Ay";
            case GridStringId.MenuColumnGroupIntervalNone: return "Hiçbiri";
            case GridStringId.MenuColumnGroupIntervalSmart: return "Akýllý";
            case GridStringId.MenuColumnGroupIntervalYear: return "Yýl";
            case GridStringId.MenuColumnGroupSummaryEditor: return "Grup Toplam Editörü";
            //case GridStringId.MenuColumnGroupSummarySortFormat: return;
            //case GridStringId.MenuColumnMaxSummaryTypeDescription: return;
            //case GridStringId.MenuColumnMinSummaryTypeDescription: return;
            case GridStringId.MenuColumnRemoveColumn: return "Sütunu Gizle";
            //case GridStringId.MenuColumnResetGroupSummarySort: return;
            case GridStringId.MenuColumnShowColumn: return "Sütunu Göster";
            //case GridStringId.MenuColumnSortGroupBySummaryMenu: return;
            //case GridStringId.MenuColumnSumSummaryTypeDescription: return;


            case GridStringId.MenuFooterAverage: return "Ortalama"; //	Average
            case GridStringId.MenuFooterAverageFormat: return _MenuFooterAverageFormat; //	AVG={0:#.##}
            case GridStringId.MenuFooterCount: return "Say"; //	Count
            case GridStringId.MenuFooterCountFormat: return _MenuFooterCountFormat; //	{0}
            case GridStringId.MenuFooterCountGroupFormat: return _MenuFooterCountGroupFormat; //	Count={0}
            case GridStringId.MenuFooterCustomFormat: return _MenuFooterCustomFormat; //	Custom={0}
            case GridStringId.MenuFooterMax: return "Maksimum"; //	Max
            case GridStringId.MenuFooterMaxFormat: return _MenuFooterMaxFormat; //	MAX={0}
            case GridStringId.MenuFooterMin: return "Minimum"; //	Min
            case GridStringId.MenuFooterMinFormat: return _MenuFooterMinFormat; //	MIN={0}
            case GridStringId.MenuFooterNone: return "Hiçbiri"; //	None
            case GridStringId.MenuFooterSum: return "Toplam"; //	Sum
            case GridStringId.MenuFooterSumFormat: return _MenuFooterSumFormat; //	SUM={0:#.##}

            case GridStringId.MenuGroupPanelClearGrouping: return "Gruplamayý Temizle"; //	Clear Grouping
            case GridStringId.MenuGroupPanelFullCollapse: return "Hepsini Kapat"; //	Full Collapse
            case GridStringId.MenuGroupPanelFullExpand: return "Hepsini Aç"; //	Full Expand

            case GridStringId.PopupFilterAll: return "(Tümü)"; //	(All)
            case GridStringId.PopupFilterBlanks: return "(Boþluklar)"; //	(Blanks)
            case GridStringId.PopupFilterCustom: return "(Kiþisel)"; //	(Custom)
            case GridStringId.PopupFilterNonBlanks: return "(Aralýksýz)"; //	(Non blanks)

            case GridStringId.PrintDesignerBandedView: return "Ayarlarý Yazdýr (Bant Görünümü)"; //	Print Settings (Banded View)
            case GridStringId.PrintDesignerBandHeader: return "Bant Baþlýðý"; //	BandHeader
            case GridStringId.PrintDesignerCardView: return "Ayarlarý Yazdýr (Kart Görünümü)"; //	Print Settings (Card View)
            case GridStringId.PrintDesignerDescription: return "Þu anki Görünüm için Yazdýrma Ayarlarýný Kur"; //	Set up various printing options for the current view.
            case GridStringId.PrintDesignerGridView: return "Yazdýrma Ayarlarý (Grid Görünümü)"; //	Print Settings (Grid View)
            case GridStringId.PrintDesignerLayoutView: return "Yazdýrma Ayarlarý (Düzen Görünümü)"; //	Print Settings (Layout View)

            case GridStringId.GroupSummaryEditorFormCancelButton: return "Ýptal";
            case GridStringId.GroupSummaryEditorFormCaption: return "Grup Alt Toplamlar";
            case GridStringId.GroupSummaryEditorFormItemsTabCaption: return "Alanlar";
            case GridStringId.GroupSummaryEditorFormOkButton: return "Tamam";
            case GridStringId.GroupSummaryEditorFormOrderTabCaption: return "Sýralama";
            case GridStringId.GroupSummaryEditorSummaryAverage: return "Averaj";
            case GridStringId.GroupSummaryEditorSummaryCount: return "Adet";
            case GridStringId.GroupSummaryEditorSummaryMax: return "Maksimum";
            case GridStringId.GroupSummaryEditorSummaryMin: return "Minimum";
            case GridStringId.GroupSummaryEditorSummarySum: return "Toplam";

        }
        return base.GetLocalizedString(id);
    }

}
