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
            case GridStringId.CardViewQuickCustomizationButton: return "Ki�iselle�tir"; //	Customize
            case GridStringId.CardViewQuickCustomizationButtonFilter: return "Filtre"; //	Filter
            case GridStringId.CardViewQuickCustomizationButtonSort: return "S�rala"; //	Sort:
            case GridStringId.ColumnViewExceptionMessage: return "De�eri D�zeltmek �stiyormusunuz?"; //	Do you want to correct the value ?
            case GridStringId.CustomFilterDialog2FieldCheck: return "Alan"; //	Field
            case GridStringId.CustomFilterDialogCancelButton: return "�ptal"; //	&Cancel
            case GridStringId.CustomFilterDialogCaption: return "Sat�rlar�n Yerini G�ster"; //	Show rows where:
            case GridStringId.CustomFilterDialogClearFilter: return "Filtreyi Temizle"; //	C&lear Filter
            case GridStringId.CustomFilterDialogFormCaption: return "Ki�isel Otomatik Filtrele"; //	Custom AutoFilter
            case GridStringId.CustomFilterDialogOkButton: return "Tamam"; //	&OK
            case GridStringId.CustomFilterDialogRadioAnd: return "Ve"; //	&And
            case GridStringId.CustomFilterDialogRadioOr: return "Veya"; //	O&r
            case GridStringId.CustomizationBands: return "Bantlar"; //	Bands
            case GridStringId.CustomizationCaption: return "Ki�iselle�tirme"; //	Customization
            case GridStringId.CustomizationColumns: return "S�tunlar"; //	Columns
            case GridStringId.CustomizationFormBandHint: return "Ki�iselle�tirmek i�in Bantlar� Buraya S�r�kle- B�rak"; //	Drag and drop bands here to customize layout
            case GridStringId.CustomizationFormColumnHint: return "Ki�iselle�tirmek i�in S�tunlar� buraya S�r�kle B�rak"; //	Drag and drop columns here to customize layout
            case GridStringId.FileIsNotFoundError: return "{0} Dosya Bulunamad�"; //	File {0} is not found
            case GridStringId.FilterBuilderApplyButton: return "Uygula"; //	&Apply
            case GridStringId.FilterBuilderCancelButton: return "�ptal"; //	&Cancel
            case GridStringId.FilterBuilderCaption: return "Filtre Olu�turucu"; //	Filter Builder
            case GridStringId.FilterBuilderOkButton: return "Tamam"; //	&OK
            case GridStringId.FilterPanelCustomizeButton: return "Filtreyi D�zenle"; //	Edit Filter
            case GridStringId.GridGroupPanelText: return "Bu s�tunu gruplayabilmek i�in s�tun ba�l���n� buraya s�r�kleyin"; //	Drag a column header here to group by that column
            case GridStringId.GridNewRowText: return "Yeni bir Kay�t eklemek i�in buraya t�klay�n"; //	Click here to add a new row
            case GridStringId.GridOutlookIntervals: return "Daha �nce;Ge�en Ay;Bu Aydan �nce;�� Hafta �nce;�ki Hafta �nce;Ge�en Hafta;;;;;;;;D�n;Bug�n;Yar�n;;;;;;;;Bir Sonraki Hafta;�ki Hafta Sonra;�� Hafta Sonra;Bu Aydan Sonra;Bir Sonraki Ay;Bir Sonraki Aydan Sonra;"; //	Older;Last Month;Earlier this Month;Three Weeks Ago;Two Weeks Ago;Last Week;;;;;;;;Yesterday;Today;Tomorrow;;;;;;;;Next Week;Two Weeks Away;Three Weeks Away;Later this Month;Next Month;Beyond Next Month;
            case GridStringId.LayoutModifiedWarning: return "D�zen  De�i�tirildi. De�i�iklikleri Kaydetmek �stiyormusunuz ?"; //	The layout has been modified. Do you want to save the changes?
            case GridStringId.LayoutViewButtonApply: return "Uygula"; //	&Apply
            case GridStringId.LayoutViewButtonCancel: return "�ptal"; //	&Cancel
            case GridStringId.LayoutViewButtonCustomizeHide: return "Ki�iselle�tirmeyi Sakla"; //	Hide Customi&zation
            case GridStringId.LayoutViewButtonCustomizeShow: return "Ki�iselle�tirmeyi G�ster"; //	&Show Customization
            case GridStringId.LayoutViewButtonLoadLayout: return "Yerle�imi Y�kle"; //	&Load Layout...
            case GridStringId.LayoutViewButtonOk: return "Tamam"; //	&Ok
            case GridStringId.LayoutViewButtonPreview: return "Daha Fazla Kart G�ster"; //	Show &More Cards
            case GridStringId.LayoutViewButtonReset: return "�ablon Kart�n� S�f�rla"; //	&Reset Template Card
            case GridStringId.LayoutViewButtonSaveLayout: return "D�zeni Kaydet..."; //	Sa&ve Layout...
            case GridStringId.LayoutViewButtonShrinkToMinimum: return "�ablon Kart�n� K���lt"; //	&Shrink Template Card
            case GridStringId.LayoutViewCarouselModeBtnHint: return "Carousel Modu"; //	Carousel Mode
            case GridStringId.LayoutViewCloseZoomBtnHintClose: return "G�r�n�m� Yeniden Y�kle"; //	Restore View
            case GridStringId.LayoutViewCloseZoomBtnHintZoom: return "Detaylar� B�y�lt"; //	Maximize Detail
            case GridStringId.LayoutViewColumnModeBtnHint: return "Bir S�tun"; //	One Column
            case GridStringId.LayoutViewCustomizationFormCaption: return "D�zen G�r�n�m� Ki�iselle�tirme"; //	LayoutView Customization
            case GridStringId.LayoutViewCustomizationFormDescription: return "Ki�iselle�tirme Men�s� ve S�r�kle-B�rak kullanarak  kart g�r�n�m�n� ki�iselle�tir ve veriyi G�r�n�m D�zeni Sayfas�nda �nizle"; //	Customize the card layout using drag-and-drop and customization menu, and preview data in the View Layout page.
            case GridStringId.LayoutViewCustomizeBtnHint: return "Ki�iselle�tirme"; //	Customization
            case GridStringId.LayoutViewGroupCaptions: return "Ba�l�klar"; //	Captions
            case GridStringId.LayoutViewGroupCards: return "Kartlar"; //	Cards
            case GridStringId.LayoutViewGroupCustomization: return "Ki�iselle�tirme"; //	Customization
            case GridStringId.LayoutViewGroupFields: return "Alanlar"; //	Fields
            case GridStringId.LayoutViewGroupHiddenItems: return "Sakl� ��eler"; //	Hidden Items
            case GridStringId.LayoutViewGroupIndents: return "Girintiler"; //	Indents
            case GridStringId.LayoutViewGroupIntervals: return "Aral�klar"; //	Intervals
            case GridStringId.LayoutViewGroupLayout: return "D�zen"; //	Layout
            case GridStringId.LayoutViewGroupPropertyGrid: return "�zellikler Tablosu"; //	Property Grid
            case GridStringId.LayoutViewGroupTreeStructure: return "D�zen A�ac� G�r�n�m�"; //	Layout Tree View
            case GridStringId.LayoutViewGroupView: return "G�r�n�m"; //	View
            case GridStringId.LayoutViewLabelAllowFieldHotTracking: return "Hot-Tracking e izin ver"; //	Allow Hot-Tracking
            case GridStringId.LayoutViewLabelCaptionLocation: return "Alan Ba�l��� Konumu"; //	Field Caption Location:
            case GridStringId.LayoutViewLabelCardArrangeRule: return "D�zenleme Kural�"; //	Arrange Rule:
            case GridStringId.LayoutViewLabelCardEdgeAlignment: return "Kart Kenar Hizalama"; //	Card Edge Alignment:
            case GridStringId.LayoutViewLabelGroupCaptionLocation: return "Grup Ba�l�k Konumu"; //	Group Caption Location:
            case GridStringId.LayoutViewLabelHorizontal: return "Yatay Aral�k"; //	Horizontal Interval
            case GridStringId.LayoutViewLabelPadding: return "Doldurma"; //	Padding
            case GridStringId.LayoutViewLabelScrollVisibility: return "Kayd�rma G�r�n�l�rl���"; //	Scroll Visibility:
            case GridStringId.LayoutViewLabelShowCardBorder: return "Kenarl��� G�ster"; //	Show Border
            case GridStringId.LayoutViewLabelShowCardCaption: return "Ba�l��� G�ster"; //	Show Caption
            case GridStringId.LayoutViewLabelShowCardExpandButton: return "Geni� Butonu G�ster"; //	Show Expand Button
            case GridStringId.LayoutViewLabelShowFieldBorder: return "Kenarl��� G�ster"; //	Show Border
            case GridStringId.LayoutViewLabelShowFieldHint: return "�pucu G�ster"; //	Show Hint
            case GridStringId.LayoutViewLabelShowFilterPanel: return "Filtre Panelini G�ster"; //	Show Filter Panel:
            case GridStringId.LayoutViewLabelShowHeaderPanel: return "Ba�l�k Panelini G�ster"; //	Show Header Panel
            case GridStringId.LayoutViewLabelShowLines: return "Sat�rlar� G�ster"; //	Show Lines
            case GridStringId.LayoutViewLabelSpacing: return "Aral�kland�rma"; //	Spacing
            case GridStringId.LayoutViewLabelTextAlignment: return "Alan Ba�l��� Metin Hizalama"; //	Field Caption Text Alignment:
            case GridStringId.LayoutViewLabelTextIndent: return "Metin Girintileri"; //	Text Indents
            case GridStringId.LayoutViewLabelVertical: return "Dikey Aral�k"; //	Vertical Interval
            case GridStringId.LayoutViewLabelViewMode: return "Dikey Mod"; //	View Mode:
            case GridStringId.LayoutViewMultiColumnModeBtnHint: return "�oklu S�tunlar"; //	Multiple Columns
            case GridStringId.LayoutViewMultiRowModeBtnHint: return "�oklu Kay�tlar"; //	Multiple Rows
            case GridStringId.LayoutViewPageTemplateCard: return "�ablon Kart�"; //	Template Card
            case GridStringId.LayoutViewPageViewLayout: return "D�zeni G�r�nt�le"; //	View Layout
            case GridStringId.LayoutViewPanBtnHint: return "Gezdirme"; //	Panning
            case GridStringId.LayoutViewRowModeBtnHint: return "Bir Kay�t"; //	One Row
            case GridStringId.LayoutViewSingleModeBtnHint: return "Bir Kart"; //	One Card

            case GridStringId.MenuColumnBestFit: return "En Uygun"; //	Best Fit
            case GridStringId.MenuColumnBestFitAllColumns: return "En Uygun (T�m S�tunlar)"; //	Best Fit (all columns)
            case GridStringId.MenuColumnClearFilter: return "Filtreyi Temizle"; //	Clear Filter
            case GridStringId.MenuColumnClearSorting: return "S�ralamay� Temizle"; //	Clear Sorting
            case GridStringId.MenuColumnColumnCustomization: return "S�tun Se�imi"; //	Column Chooser
            case GridStringId.MenuColumnFilter: return "Filtrelenebilir mi?"; //	Can Filter
            case GridStringId.MenuColumnFilterEditor: return "Filtre Edit�r�"; //	Filter Editor
            case GridStringId.MenuColumnGroup: return "Bu s�tuna g�re grupla"; //	Group By This Column
            case GridStringId.MenuColumnGroupBox: return "Bir�ok s�tuna g�re grupla"; //	Group By Box
            case GridStringId.MenuColumnSortAscending: return "Artan Y�nde S�rala"; //	Sort Ascending
            case GridStringId.MenuColumnSortDescending: return "Azalan Y�nde S�rala"; //	Sort Descending
            case GridStringId.MenuColumnUnGroup: return "Gruplamay� Kald�r"; //	UnGroup

            case GridStringId.MenuColumnAutoFilterRowHide: return "Otomatik Filtreleme Sat�r�n� Gizle";
            case GridStringId.MenuColumnAutoFilterRowShow: return "Otomatik Filtreleme Sat�r�n� G�ster";
            //case GridStringId.MenuColumnAverageSummaryTypeDescription: return;
            //case GridStringId.MenuColumnCountSummaryTypeDescription: return;
            case GridStringId.MenuColumnExpressionEditor: return "�fade Editor�...";
            //case GridStringId.MenuColumnFilterMode: return;
            //case GridStringId.MenuColumnFilterModeDisplayText: return;
            //case GridStringId.MenuColumnFilterModeValue: return;
            case GridStringId.MenuColumnFindFilterHide: return "Arama Kutusunu Gizle";
            case GridStringId.MenuColumnFindFilterShow: return "Arama Kutusunu G�ster";
            case GridStringId.MenuColumnGroupIntervalDay: return "G�n";
            case GridStringId.MenuColumnGroupIntervalMenu: return "Grup Aral���";
            case GridStringId.MenuColumnGroupIntervalMonth: return "Ay";
            case GridStringId.MenuColumnGroupIntervalNone: return "Hi�biri";
            case GridStringId.MenuColumnGroupIntervalSmart: return "Ak�ll�";
            case GridStringId.MenuColumnGroupIntervalYear: return "Y�l";
            case GridStringId.MenuColumnGroupSummaryEditor: return "Grup Toplam Edit�r�";
            //case GridStringId.MenuColumnGroupSummarySortFormat: return;
            //case GridStringId.MenuColumnMaxSummaryTypeDescription: return;
            //case GridStringId.MenuColumnMinSummaryTypeDescription: return;
            case GridStringId.MenuColumnRemoveColumn: return "S�tunu Gizle";
            //case GridStringId.MenuColumnResetGroupSummarySort: return;
            case GridStringId.MenuColumnShowColumn: return "S�tunu G�ster";
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
            case GridStringId.MenuFooterNone: return "Hi�biri"; //	None
            case GridStringId.MenuFooterSum: return "Toplam"; //	Sum
            case GridStringId.MenuFooterSumFormat: return _MenuFooterSumFormat; //	SUM={0:#.##}

            case GridStringId.MenuGroupPanelClearGrouping: return "Gruplamay� Temizle"; //	Clear Grouping
            case GridStringId.MenuGroupPanelFullCollapse: return "Hepsini Kapat"; //	Full Collapse
            case GridStringId.MenuGroupPanelFullExpand: return "Hepsini A�"; //	Full Expand

            case GridStringId.PopupFilterAll: return "(T�m�)"; //	(All)
            case GridStringId.PopupFilterBlanks: return "(Bo�luklar)"; //	(Blanks)
            case GridStringId.PopupFilterCustom: return "(Ki�isel)"; //	(Custom)
            case GridStringId.PopupFilterNonBlanks: return "(Aral�ks�z)"; //	(Non blanks)

            case GridStringId.PrintDesignerBandedView: return "Ayarlar� Yazd�r (Bant G�r�n�m�)"; //	Print Settings (Banded View)
            case GridStringId.PrintDesignerBandHeader: return "Bant Ba�l���"; //	BandHeader
            case GridStringId.PrintDesignerCardView: return "Ayarlar� Yazd�r (Kart G�r�n�m�)"; //	Print Settings (Card View)
            case GridStringId.PrintDesignerDescription: return "�u anki G�r�n�m i�in Yazd�rma Ayarlar�n� Kur"; //	Set up various printing options for the current view.
            case GridStringId.PrintDesignerGridView: return "Yazd�rma Ayarlar� (Grid G�r�n�m�)"; //	Print Settings (Grid View)
            case GridStringId.PrintDesignerLayoutView: return "Yazd�rma Ayarlar� (D�zen G�r�n�m�)"; //	Print Settings (Layout View)

            case GridStringId.GroupSummaryEditorFormCancelButton: return "�ptal";
            case GridStringId.GroupSummaryEditorFormCaption: return "Grup Alt Toplamlar";
            case GridStringId.GroupSummaryEditorFormItemsTabCaption: return "Alanlar";
            case GridStringId.GroupSummaryEditorFormOkButton: return "Tamam";
            case GridStringId.GroupSummaryEditorFormOrderTabCaption: return "S�ralama";
            case GridStringId.GroupSummaryEditorSummaryAverage: return "Averaj";
            case GridStringId.GroupSummaryEditorSummaryCount: return "Adet";
            case GridStringId.GroupSummaryEditorSummaryMax: return "Maksimum";
            case GridStringId.GroupSummaryEditorSummaryMin: return "Minimum";
            case GridStringId.GroupSummaryEditorSummarySum: return "Toplam";

        }
        return base.GetLocalizedString(id);
    }

}
