using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Grid
{
    public partial class SaveAsForm : DevExpress.XtraEditors.XtraForm
    {
        private Grid.ReportTemplate _template;
        private Grid.Xml.Parameter _parameter;

        public SaveAsForm(Grid.ReportTemplate template, Grid.Xml.Parameter parameter)
        {
            _template = template;
            _parameter = parameter;
            InitializeComponent();
        }

        private void SaveAsForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtReportName.Text = _template.TemplateName;
                //this.txtReportName.Text = "";
                this.chkDefault.Checked = _template.Default;
                this.chkAllUser.Checked = _template.AllUser;
                //if (_parameter.UserName.ToLower() != "admin") this.chkAllUser.Visible = false;
                this.txtReportName.Focus();
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("Bilgileri kontrol ediniz.");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                #region "sablon_olusturabilir" yetkisi"
                if (_template.RSayac == 0)
                {
                    if (_parameter.CreatableTemplate == false && _parameter.UserName.ToLower() != "admin")
                    {
                        MessageBox.Show("Yeni rapor þablonu oluþturma yetkiniz yoktur! \n\n Yetki Nesne Adý : Basýlý Form Rapor Þablonu Oluþturma Yetkisi  \n Nesne id : utl_dev_sablon_olusturma", "UYARI");
                        _template.SaveAs = false;
                        return;
                    }
                }
                #endregion

                #region "read xml - eðer xml dosyasýnda "sablon_degistirebilir" yetkisi"
                if (_parameter.ReplaceableTemplate == false && _parameter.UserName.ToLower() != "admin")
                {
                    if (_template.UserName.ToLower() != _parameter.UserName.ToLower() && _parameter.UserName.ToLower() != "admin")
                    {
                        MessageBox.Show("Rapor þablonunda deðiþiklik yapma yetkisi yoktur! \n\n Yetki Nesne Adý : Basýlý Form Rapor Þablonu Deðiþtirme Yetkisi  \n Nesne id : utl_dev_sablon_degistirme", "UYARI");
                        _template.SaveAs = false;
                        return;
                    }
                    #region eski
                    //if ((_template.UserName.ToLower() != _parameter.UserName.ToLower()) && (_template.TemplateName == this.txtReportName.Text))
                    //{
                    //    MessageBox.Show("Farklý kullanýcý tarafýndan hazýrlanan þablonu deðiþtiremezsiniz!", "UYARI");
                    //    _template.SaveAs = false;
                    //    return;
                    //}
                    #endregion
                }
                #endregion

                this.txtReportName.Text = (this.txtReportName.Text).Replace('/', '-');

                if ((_template.RSayac > 0) && (_template.TemplateName != this.txtReportName.Text)) _template.RSayac = 0;
                _template.TemplateName = this.txtReportName.Text;
                _template.Default = chkDefault.Checked;
                //_template.AllUser = _template.UserName.ToLower() == "admin" ? this.chkAllUser.Checked : false;
                _template.AllUser = chkAllUser.Checked;
                _template.SaveAs = true;
                _template.UserName = _parameter.UserName; // 13.07.2016 da eklendi
                this.Close();
            }
            catch (System.Exception ex)
            {
                Grid.Utils.WriteLog(ex);
                //MessageBox.Show("Bilgileri kontrol ediniz.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}