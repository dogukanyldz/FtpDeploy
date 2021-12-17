using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.Utils.Win;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;

namespace Grid
{
    class AdvCheckedFilter
    {
        #region ""tanýmlar
        bool checkLock;
        GridColumn focusedColumn;
        int CheckedComboBox_Popup_top_count;
        DataTable dtSource;

        GridView _view;
        BindingList<GridColumn> _advColumns;
        #endregion

        public GridView View
        {
            get
            {
                return _view;
            }
            set
            {
                if (_view == value)
                    return;
                _view = value;
            }
        }

        public BindingList<GridColumn> AdvColumns
        {
            get
            {
                return _advColumns;
            }
            set
            {
                if (_advColumns == value)
                    return;
                _advColumns = value;
            }
        }

        public AdvCheckedFilter(GridView View)
        {
            AdvColumns = new BindingList<GridColumn>();
            this.View = View;
            this.dtSource = (View.DataSource as DataView).Table;
            View.ShowFilterPopupCheckedListBox += new FilterPopupCheckedListBoxEventHandler(this.View_ShowFilterPopupCheckedListBox); //unutma 03.04.2012
        }

        private void View_ShowFilterPopupCheckedListBox(object sender, DevExpress.XtraGrid.Views.Grid.FilterPopupCheckedListBoxEventArgs e)
        {
            if (!_advColumns.Contains(e.Column))
                return;

            focusedColumn = e.Column;
            CheckedComboBox_Popup_top_count = 3;

            e.CheckedComboBox.SelectAllItemVisible = true;
            e.CheckedComboBox.SelectAllItemCaption = "Hepsini Seç";

            //#region "list - rows"
            ViewFilter my_vf = new ViewFilter();

            #region aktif filter bu kolona ait filtre kýsmýný kaldýrýlýyor
            ViewFilter vf = View.ActiveFilter;

            IEnumerator ef = vf.GetEnumerator();
            while (ef.MoveNext())
            {
                ViewColumnFilterInfo vcfi = ef.Current as ViewColumnFilterInfo;
                if (vcfi.Column.FieldName != e.Column.FieldName) // && vcfi.Column.FieldName != "(Hepsi)")
                    my_vf.Add(vcfi);
            }
            #endregion

            #region "filtre - panel - columns"
            GridView gv = new GridView();
            ef = my_vf.GetEnumerator();
            while (ef.MoveNext())
            {
                ViewColumnFilterInfo vcfi = ef.Current as ViewColumnFilterInfo;
                if (vcfi.Filter.FilterString.ToLower().Contains("contains"))                                                    // burada ve aþaðýda yapýlan iþlemde ana veri kaynaðýný filtrelemek için devexpress tarafýndan oluþan contains ve ve isnuloremtpy filtrelerinin,                                                                                                                            
                {                                                                                                               //ado.net'de datatable'a select yaparken kullanýlan filtrede hata vermesinden dolayý replace edilerek sql formatýna uygun hale getirildi.
                    GridColumn colCategory = View.Columns[e.Column.FieldName];
                    string findPO = string.Format("[{0}] LIKE '%{1}%'", vcfi.Column.FieldName, vcfi.Filter.Value);                          

                    ColumnFilterInfo filter_ = new ColumnFilterInfo(findPO, "");
                    ViewColumnFilterInfo filter2_ = new ViewColumnFilterInfo(colCategory, filter_);
                    gv.ActiveFilter.Add(filter2_);


                }
                else if (vcfi.Filter.FilterString.Contains("IsNullOrEmpty"))
                {
                    GridColumn colCategory = View.Columns[e.Column.FieldName];
                    string findPO = string.Format("[{0}] is null or [{0}]='' ", vcfi.Column.FieldName);
                    ColumnFilterInfo filter_ = new ColumnFilterInfo(findPO, "");
                    ViewColumnFilterInfo filter2_ = new ViewColumnFilterInfo(colCategory, filter_);
                    gv.ActiveFilter.Add(filter2_);

                }
              
                else
                {
                    gv.ActiveFilter.Add(vcfi);

                }

            }

            string filter = gv.ActiveFilterString;// FilterPanelText;
            #endregion

            //if (filter != "")
            //{
            foreach (GridColumn clmn in View.Columns)
            {
                filter = filter.Replace(("[" + clmn.Caption + "]"), ("[" + clmn.FieldName + "]"));

               
            }
            
            filter = filter.Replace("'(Hepsi)',", "");
            filter = filter.Replace("m,", ",");
            filter = filter.Replace("m)", ")");                          // burada yapýlan tüm iþkenceler, bazý kolonlarda (boy,uzunluk gibi) verinin sonunda 'm' ifadesi gelmesiydi
                                                                         // bu yüzden filtreleme yapabilmek için daha uygun bir formata çevrildi.
            Regex rgx = new Regex(@"(\dm)(\s|\W|$)");
            filter = rgx.Replace(filter, " ");
            try
            {
                DataRow[] rows;

                    rows = dtSource.Select(filter);                       // ana veri kaynaðýna oluþturduðumuz filtreleri ekliyoruz. yukarýda belirttiðimiz contains ve is null or empty burada sorun çýkarýyordu.
                GridColumn col = View.Columns[e.Column.FieldName];
                bool bos_ekle = false;
                List<object> objects_top = new List<object>();
                string format = col.DisplayFormat.FormatString;
                foreach (DataRow row in rows)                            
                {
                    if ((row[e.Column.FieldName] == null) || (String.IsNullOrEmpty(row[e.Column.FieldName].ToString())))
                    {
                        bos_ekle = true;
                    }
                    else
                    {
                        switch (col.DisplayFormat.FormatType)                        // burada yapýlan iþlem filtrelenen data içindeki row deðerlerini checkboxa doldurmak.
                        {
                            case FormatType.Numeric:
                                if (!objects_top.Contains(Convert.ToDouble(row[e.Column.FieldName])))
                                    if (col.DisplayFormat.FormatString.Contains("{0:n"))
                                    {

                                        if (format == "{0:n}")
                                        {                                                                                               //0:n formatýnýn default deðeri 2 sýfýr. bu sebepten 2 sýfýr eklendi.
                                            if (!objects_top.Contains(Convert.ToDouble(row[e.Column.FieldName])))
                                                objects_top.Add(Math.Round(Convert.ToDouble(row[e.Column.FieldName]),2));
                                        }
                                        else
                                        {
                                            char index = format[format.Length - 2];
                                            string _index = index.ToString();
                                            if (!objects_top.Contains(Convert.ToDouble(row[e.Column.FieldName])))                       //eðer n deðil ise diðer olasýklar olan n:3,n:4 gibi durumlarda buradaki int degiskeni kadar 0 eklendi.
                                                objects_top.Add(Math.Round(Convert.ToDouble(row[e.Column.FieldName]), Convert.ToInt32(_index)));
                                        }
                                        
                                    }
                                    else
                                    {
                                        if (!objects_top.Contains(row[e.Column.FieldName].ToString()))
                                            objects_top.Add(row[e.Column.FieldName].ToString());
                                    }
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
                                {    //DateTime date = DateTime.Parse(row[e.Column.FieldName].ToString());
                                    //string dat1 = String.Format("{0:MM/dd/yy 00:00:00}", date);
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
                List<int> list_top = new List<int>();
                List<int> list_bottom = new List<int>();

            
                for (int i = 0; i < e.CheckedComboBox.Items.Count; i++)         // Burada list top dizisine objects_top içerisindeki indexler atanýyor. Eðer içeriyorsa list_top içine atanýyor.
                                                                                   //burada hardcoded datetime eklenmesinin sebebi, örneðin 12.12 03.03.03 olan saati devexpress 12.12 00.00 olarak çeviriyor 
                                                                                    // ve bu sebepten dolayý hiçbir zaman combobaxlara ekleme yapamýyordu.Tarih içeren kolonlar bu yüzden muaf tutuldu.
                {
                    CheckedListBoxItem item = e.CheckedComboBox.Items[i];

                    switch (col.DisplayFormat.FormatType)
                    {
                        
                        case FormatType.Numeric:
                            if (item.Value.ToString() != "" && item.Value.ToString()!="(Boþluklar)")
                            {
                                if (objects_top.Contains(Convert.ToDouble(item.Value.ToString())) || e.Column.ColumnType.Name.ToLower().Contains("datetime"))
                                    list_top.Add(i);
                                else
                                    list_bottom.Add(i);
                            }                          

                            break;
                        case FormatType.DateTime:
                            if (item.Value.ToString() != "" && item.Value.ToString() != "(Boþluklar)")
                            {
                                if (objects_top.Contains(Convert.ToDateTime(item.Value.ToString())) || e.Column.ColumnType.Name.ToLower().Contains("datetime"))
                                    list_top.Add(i);
                                else
                                    list_bottom.Add(i);
                            }
                            break;
                        case FormatType.Custom:
                            if (item.Value.ToString() != "" && item.Value.ToString() != "(Boþluklar)")
                            {
                                if (col.DisplayFormat.FormatString.Contains("{0:n"))
                                {
                                    if (objects_top.Contains(Convert.ToDouble(item.Value.ToString())))
                                        list_top.Add(i);
                                    else
                                        list_bottom.Add(i);
                                }
                                else
                                {

                                    if (objects_top.Contains(item.Value.ToString()) || e.Column.ColumnType.Name.ToLower().Contains("datetime"))
                                        list_top.Add(i);
                                    else
                                        list_bottom.Add(i);
                                }
                            }
                            break;
                        default:
                            if (item.Value.ToString() != "" && item.Value.ToString() != "(Boþluklar)")
                            {
                                if (objects_top.Contains(item.Value.ToString()) || e.Column.ColumnType.Name.ToLower().Contains("datetime"))
                                    list_top.Add(i);
                                else
                                    list_bottom.Add(i);
                            }
                            break;
                    }
                }


              

                CheckedListBoxItemCollection items = new CheckedListBoxItemCollection();    //


                if (bos_ekle && e.CheckedComboBox.Items.Count >= 1)                                                // burada kullanýlan bottom_list listesi, boþluk girilen satýrlar için kullanýlan boþluklar checkboxi için kullanýlmýþtýr.
                {

                    items.Add(e.CheckedComboBox.Items[0]);                               //Sadece bir seferlik boþ girilen kayýtlarý comboboxý eklemek için yapýldý.

                }
                foreach (int index in list_top)                           // list_top'da elde edilen indexler ile de combobaxlarýn indexleri seçilip collectiona atama yapýlýyor.
                {
                    items.Add(e.CheckedComboBox.Items[index]);
                }

                
                e.CheckedComboBox.Items.Clear();
                FilterItem fi = new FilterItem("(Boþ)", "(Boþ)");
                CheckedListBoxItem item2 = new CheckedListBoxItem(fi);
                e.CheckedComboBox.Items.Insert(0, item2);
                fi = new FilterItem("(Boþ Olmayan)", "(Boþ Olmayan)");      // burada her halukarda gözükmesi için boþ ve boþ olmayan comboboxlarý ekleniyor.
                item2 = new CheckedListBoxItem(fi);
                e.CheckedComboBox.Items.Insert(1, item2);
                foreach (CheckedListBoxItem item in items)
                {
                    e.CheckedComboBox.Items.Add(item);
                }

                if (!String.IsNullOrEmpty(filter))
                    CheckedComboBox_Popup_top_count = list_top.Count + 3;

            }
            catch (Exception ex)
            {
                MessageBox.Show(("Filtreye girilen deðer ile kolonun formatý uyuþmamaktadýr.\n" + ex.Message), "HATA");
                Grid.Utils.WriteLog(ex);
            }
            e.CheckedComboBox.Popup += new EventHandler(CheckedComboBox_Popup); // unutma 03.04.2012

            e.CheckedComboBox.CloseUp += new CloseUpEventHandler(CheckedComboBox_CloseUp); // unutma 03.04.2012

        }

        void OkButton_Click(object sender, EventArgs e)
        {
      
            View.ApplyColumnsFilter(); // unutma 03.04.2012
        }

        void control_DrawItem(object sender, ListBoxDrawItemEventArgs e) // unutma 03.04.2012
        {
            CheckedListBoxControl control = sender as CheckedListBoxControl;
            if (CheckedComboBox_Popup_top_count > 0 && !String.IsNullOrEmpty(View.FilterPanelText))
            {
                if (e.Index < CheckedComboBox_Popup_top_count)
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
        }

        CheckedListBoxControl FindListBoxControl(Control container)
        {
            if (container is CheckedListBoxControl)
                return container as CheckedListBoxControl;
            foreach (Control control in container.Controls)
            {
                CheckedListBoxControl listBox = FindListBoxControl(control);
                if (listBox != null)
                    return listBox as CheckedListBoxControl;
            }
            return null;
        }

        void CheckItems(List<object> values, CheckedListBoxControl control)
        {
            foreach (object value in values)
            {
                foreach (CheckedListBoxItem item in control.Items)
                {
                    //if ((item.Value as FilterItem).Value == value)
                    //    item.CheckState = CheckState.Checked;
                    FilterItem fi = item.Value as FilterItem;

                    if (fi != null)
                    {
                        if ((item.Value as FilterItem).Value != null)
                        {
                            if ((item.Value as FilterItem).Value.ToString() == "(Hepsi)"
                           || (item.Value as FilterItem).Value.ToString() == "(Boþ)"
                           || (item.Value as FilterItem).Value.ToString() == "(Boþ Olmayan)"
                           || value.ToString() == "(Hepsi)"
                           || value.ToString() == "(Boþ)"
                           || value.ToString() == "(Boþ Olmayan)"
                           )
                            {
                                if ((item.Value as FilterItem).Value == value)
                                    item.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                switch (focusedColumn.DisplayFormat.FormatType)
                                {
                                    case FormatType.Numeric:
                                        if (Convert.ToDouble((item.Value as FilterItem).Value) == Convert.ToDouble(value))
                                            item.CheckState = CheckState.Checked;
                                        break;
                                    case FormatType.DateTime:
                                        if (Convert.ToDateTime((item.Value as FilterItem).Value) == Convert.ToDateTime(value))
                                            item.CheckState = CheckState.Checked;
                                        break;
                                    case FormatType.Custom:
                                        if (focusedColumn.DisplayFormat.FormatString.Contains("{0:n"))
                                        {
                                            if (Convert.ToDouble((item.Value as FilterItem).Value) == Convert.ToDouble(value))
                                                item.CheckState = CheckState.Checked;
                                        }
                                        else
                                        {
                                            if ((item.Value as FilterItem).Value.Equals(value))// == value)
                                                item.CheckState = CheckState.Checked;
                                        }
                                        break;
                                    default:
                                        if ((item.Value as FilterItem).Value.Equals(value))// == value)
                                            item.CheckState = CheckState.Checked;
                                        break;
                                }
                            }
                        }
                   

                    }


                }
            }
        }

        List<object> GetOperandsString(CriteriaOperator crOperator)
        {
            List<object> operandList = new List<object>();
            //List<string> operandList = new List<string>();
            if (crOperator == null)
                return operandList;
            if (crOperator is OperandValue)
            {
                //  operandList.Add((string)(crOperator as OperandValue).Value);

                if (/*(crOperator as OperandValue).Value.ToString() == "(Hepsi)"
                    ||*/ (crOperator as OperandValue).Value.ToString() == "(Boþ)"
                    || (crOperator as OperandValue).Value.ToString() == "(Boþ Olmayan)")
                {
                    operandList.Add((string)(crOperator as OperandValue).Value);
                }
                else
                {

                    switch (focusedColumn.DisplayFormat.FormatType)
                    {
                        case FormatType.Numeric:
                            try
                            {
                                operandList.Add(Convert.ToDouble((crOperator as OperandValue).Value));
                            }
                            catch (Exception ex)
                            {
                                Grid.Utils.WriteLog(ex);
                                //MessageBox.Show("Filtreye girilen deðer ile kolonun formatý uyuþmamaktadýr.", "HATA");
                            }
                            break;
                        case FormatType.DateTime:
                            operandList.Add(Convert.ToDateTime((crOperator as OperandValue).Value));
                            break;
                        case FormatType.Custom:
                            if (focusedColumn.DisplayFormat.FormatString.Contains("{0:n"))
                            {
                                operandList.Add(Convert.ToDouble((crOperator as OperandValue).Value));
                            }
                            else
                            {
                                operandList.Add(Convert.ToString((crOperator as OperandValue).Value));
                            }
                            break;
                        default:
                            operandList.Add((string)(crOperator as OperandValue).Value);
                            break;
                    }
                }
            }
            if (crOperator is InOperator)
            {
                InOperator group = crOperator as InOperator;
                foreach (CriteriaOperator operand in group.Operands)
                    operandList.AddRange(GetOperandsString(operand));
            }
            if (crOperator is UnaryOperator)
            {
                UnaryOperator unary = crOperator as UnaryOperator;
                if (unary.OperatorType == UnaryOperatorType.IsNull)
                    operandList.Add("(Boþ)");
                if (unary.OperatorType == UnaryOperatorType.Not)
                    operandList.Add("(Boþ Olmayan)");
            }
            if (crOperator is GroupOperator)
            {
                GroupOperator group = crOperator as GroupOperator;
                foreach (CriteriaOperator operand in group.Operands)
                    operandList.AddRange(GetOperandsString(operand));
            }
            if (crOperator is BinaryOperator)
            {
                BinaryOperator binOperator = crOperator as BinaryOperator;
                operandList.AddRange(GetOperandsString(binOperator.RightOperand));
                operandList.AddRange(GetOperandsString(binOperator.LeftOperand));
            }
            return operandList;
        }

        void Form_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (checkLock)
                return;
            try
            {
                CheckedListBoxControl listBox = (sender as CheckedListBoxControl);
                checkLock = true;
                switch (e.Index)
                {
                    case 0: SelectAllChecked(listBox, e);
                        CheckState cs0 = listBox.Items[0].CheckState;
                        CheckState cs1 = listBox.Items[1].CheckState;
                        CheckState cs2 = listBox.Items[2].CheckState;
                        //listBox.ItemCheck -= new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(Form_ItemCheck);
                        //for (int i = 3; i < listBox.Items.Count; i++)
                        //    listBox.Items[i].CheckState = e.State;
                        if (e.State == CheckState.Checked) listBox.CheckAll();
                        else listBox.UnCheckAll();

                        listBox.Items[0].CheckState = cs0;
                        listBox.Items[1].CheckState = cs1;
                        listBox.Items[2].CheckState = cs2;

                        //listBox.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(Form_ItemCheck);
                        break;
                    case 1:
                    case 2: BlankNonBlankChecked(listBox, e); break;
                    default: CheckAllItemsChecked(listBox); break;
                }
            }
            finally
            {
                checkLock = false;
            }
        }

        void SelectAllChecked(CheckedListBoxControl listBox, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked)
            {
                listBox.Items[1].CheckState = e.State;
                listBox.Items[2].CheckState = e.State == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            }
            else
            {
                listBox.Items[1].CheckState = CheckState.Unchecked;
                listBox.Items[2].CheckState = CheckState.Unchecked;
            }
            //for (int i = 3; i < listBox.Items.Count; i++)
            //    listBox.Items[i].CheckState = e.State;
        }

        void BlankNonBlankChecked(CheckedListBoxControl listBox, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked)
            {
                if (e.Index == 1)
                    listBox.Items[2].CheckState = CheckState.Unchecked;
                else
                    listBox.Items[1].CheckState = CheckState.Unchecked;
            }
        }

        void CheckAllItemsChecked(CheckedListBoxControl listBox)
        {
            int checkedCount = 0;
            listBox.Items[0].CheckState = CheckState.Unchecked;
            //for (int i = 3; i < listBox.Items.Count; i++)
            //    if (listBox.Items[i].CheckState == CheckState.Checked)
            //        checkedCount++;
            checkedCount = listBox.Items.GetCheckedValues().Count;
            if (listBox.Items[1].CheckState == CheckState.Checked) checkedCount--;
            //if (listBox.Items[2].CheckState == CheckState.Checked) checkedCount--;
            //
            if (checkedCount == 0)
                listBox.Items[0].CheckState = CheckState.Unchecked;
            if (checkedCount == listBox.Items.Count - 3)
                listBox.Items[0].CheckState = CheckState.Checked;
        }

        void CheckedComboBox_CloseUp(object sender, CloseUpEventArgs e)
        {
            GridColumn column = focusedColumn;// View.FocusedColumn;
            PopupContainerForm form = (sender as IPopupControl).PopupWindow as PopupContainerForm;
            CheckedListBoxControl control = form.Controls[3].Controls[0] as CheckedListBoxControl;
            List<Object> checkedValues = control.Items.GetCheckedValues();      //burada yapýlan ilgili sütuna ait checkleri getirmek.
            ViewFilter vf = View.ActiveFilter;

            if (checkedValues.Count == 0)
            {
                View.ActiveFilter.Add(column, new ColumnFilterInfo());
                return;
            }
            if (control != null)
            {
                ColumnFilterInfo blanksFilter = GetBlanksFilter(column, e.Value, checkedValues);
                ColumnFilterInfo valuesFilter = GetValuesFilter(column, e.Value, checkedValues);
                if (valuesFilter != null && blanksFilter != null)
                {
                    this.View.ActiveFilter.Add(column, new ColumnFilterInfo(valuesFilter.FilterCriteria | blanksFilter.FilterCriteria));
                    e.AcceptValue = false;
                    return;
                }
                if (valuesFilter != null)
                {
                    this.View.ActiveFilter.Add(column, valuesFilter);
                    e.AcceptValue = true;
                    return;
                }
                if (blanksFilter != null)
                {
                    this.View.ActiveFilter.Add(column, blanksFilter);
                    e.AcceptValue = false;

                }
            }
        }
        ColumnFilterInfo GetValuesFilter(GridColumn column, object stringValues, List<object> checkedValues)
        {
            InOperator oper = new InOperator(new OperandProperty(column.FieldName));
            ColumnFilterInfo filterInfo = null;
            foreach (object item in checkedValues)
            {
                FilterItem fi = item as FilterItem;
                //if ((fi != null) && !(fi.Value is string))
                if (fi != null)
                {
                    if (fi.Value != null)
                    {
                        if ((string)fi.Value.ToString() != "(Boþ)" && (string)fi.Value.ToString() != "(Boþ Olmayan)")
                            oper.Operands.Add(new OperandValue(fi.Value));
                        //if (column.ColumnType.Name.ToLower() == "datetime")
                        //{
                        //    oper.Operands.Add(new OperandValue(fi.Value));
                        //}
                            
                    }
                }
            
            }
            switch (oper.Operands.Count)
            {
                case 0:
                    break;
                case 1:
                    filterInfo = new ColumnFilterInfo(oper.LeftOperand == ((CriteriaOperator)oper.Operands[0]));
                    break;
                case 2:
                    filterInfo = new ColumnFilterInfo(
                        oper.LeftOperand == ((CriteriaOperator)oper.Operands[0])
                        |
                        oper.LeftOperand == ((CriteriaOperator)oper.Operands[1])
                        );

                    break;
                default:
                    filterInfo = new ColumnFilterInfo(oper);
                    break;
            }
            return filterInfo;
        }
        private void CheckedComboBox_Popup(object sender, EventArgs e) // unutma 03.04.2012
        {
            IPopupControl popupControl = sender as IPopupControl;
            if (popupControl == null) return;
             GridColumn column = focusedColumn;// View.FocusedColumn;
            PopupContainerForm form = popupControl.PopupWindow as PopupContainerForm;
            if (form == null) return;
            CheckedListBoxControl control = FindListBoxControl(form);
            if (control != null)
            {

                //control.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(Form_ItemCheck);
                CheckAllItemsChecked(control);

                List<object> operands = GetOperandsString(focusedColumn.FilterInfo.FilterCriteria);
                CheckItems(operands, control);

                //control.DrawItem += new ListBoxDrawItemEventHandler(control_DrawItem);
            }
            form.OkButton.Click += new EventHandler(OkButton_Click);
        }
        ColumnFilterInfo GetBlanksFilter(GridColumn column, object stringValues, List<object> checkedValues)
        {
            ColumnFilterInfo cfiBlanks = null;
            foreach (object item in checkedValues)
            {
                FilterItem fi = item as FilterItem;
                if (fi != null)
                {
                    if (fi.Value is string)
                    {
                        if ((string)fi.Value == "(Boþ)")
                        {
                            cfiBlanks = new ColumnFilterInfo(new OperandProperty(column.FieldName).IsNull());
                            break;
                        }
                        if ((string)fi.Value == "(Boþ Olmayan)")
                        {
                            cfiBlanks = new ColumnFilterInfo(new OperandProperty(column.FieldName).IsNotNull());
                            break;
                        }
                    }
                }
                
              
            }
            return cfiBlanks;
        }
    }
}


