namespace Grid
{
    partial class SaveAsForm
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
            this.chkDefault = new DevExpress.XtraEditors.CheckEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtReportName = new DevExpress.XtraEditors.TextEdit();
            this.lblReportName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkAllUser = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkDefault
            // 
            this.chkDefault.Location = new System.Drawing.Point(59, 31);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Properties.Caption = "Varsayýlan olarak iþaretle";
            this.chkDefault.Size = new System.Drawing.Size(145, 19);
            this.chkDefault.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(285, 65);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(59, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Tamam";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Ýptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(61, 5);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(345, 20);
            this.txtReportName.TabIndex = 3;
            // 
            // lblReportName
            // 
            this.lblReportName.Location = new System.Drawing.Point(8, 8);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(50, 13);
            this.lblReportName.TabIndex = 4;
            this.lblReportName.Text = "Þablon Adý";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkAllUser);
            this.panelControl1.Controls.Add(this.txtReportName);
            this.panelControl1.Controls.Add(this.lblReportName);
            this.panelControl1.Controls.Add(this.chkDefault);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(418, 56);
            this.panelControl1.TabIndex = 5;
            // 
            // chkAllUser
            // 
            this.chkAllUser.Location = new System.Drawing.Point(231, 30);
            this.chkAllUser.Name = "chkAllUser";
            this.chkAllUser.Properties.Caption = "Tüm Kullanýcýlar";
            this.chkAllUser.Size = new System.Drawing.Size(110, 19);
            this.chkAllUser.TabIndex = 5;
            // 
            // SaveAsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 91);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "SaveAsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farklý Kaydet...";
            this.Load += new System.EventHandler(this.SaveAsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllUser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkDefault;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtReportName;
        private DevExpress.XtraEditors.LabelControl lblReportName;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chkAllUser;
    }
}