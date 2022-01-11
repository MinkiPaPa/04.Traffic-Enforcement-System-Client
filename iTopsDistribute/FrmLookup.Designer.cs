namespace iTopsDistribute
{
    partial class FrmLookup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdbtnAll = new System.Windows.Forms.RadioButton();
            this.rdbtnDistribution = new System.Windows.Forms.RadioButton();
            this.rdbtnNoDstribution = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtInspectorId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CbbInspector = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChkbxUseDate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.dsInspector = new System.Data.DataSet();
            this.Inspector = new System.Data.DataTable();
            this.user_id = new System.Data.DataColumn();
            this.user_nm = new System.Data.DataColumn();
            this.dsCode = new System.Data.DataSet();
            this.Code = new System.Data.DataTable();
            this.cd = new System.Data.DataColumn();
            this.cd_nm = new System.Data.DataColumn();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 76);
            this.panel1.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.rdbtnAll);
            this.panel6.Controls.Add(this.rdbtnDistribution);
            this.panel6.Controls.Add(this.rdbtnNoDstribution);
            this.panel6.Location = new System.Drawing.Point(14, 11);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(386, 50);
            this.panel6.TabIndex = 2;
            // 
            // rdbtnAll
            // 
            this.rdbtnAll.AutoSize = true;
            this.rdbtnAll.Location = new System.Drawing.Point(279, 15);
            this.rdbtnAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnAll.Name = "rdbtnAll";
            this.rdbtnAll.Size = new System.Drawing.Size(41, 21);
            this.rdbtnAll.TabIndex = 5;
            this.rdbtnAll.Tag = "2";
            this.rdbtnAll.Text = "All";
            this.rdbtnAll.UseVisualStyleBackColor = true;
            // 
            // rdbtnDistribution
            // 
            this.rdbtnDistribution.AutoSize = true;
            this.rdbtnDistribution.Location = new System.Drawing.Point(160, 15);
            this.rdbtnDistribution.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnDistribution.Name = "rdbtnDistribution";
            this.rdbtnDistribution.Size = new System.Drawing.Size(96, 21);
            this.rdbtnDistribution.TabIndex = 4;
            this.rdbtnDistribution.Tag = "1";
            this.rdbtnDistribution.Text = "Distributed";
            this.rdbtnDistribution.UseVisualStyleBackColor = true;
            // 
            // rdbtnNoDstribution
            // 
            this.rdbtnNoDstribution.AutoSize = true;
            this.rdbtnNoDstribution.Checked = true;
            this.rdbtnNoDstribution.Location = new System.Drawing.Point(19, 15);
            this.rdbtnNoDstribution.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnNoDstribution.Name = "rdbtnNoDstribution";
            this.rdbtnNoDstribution.Size = new System.Drawing.Size(111, 21);
            this.rdbtnNoDstribution.TabIndex = 3;
            this.rdbtnNoDstribution.TabStop = true;
            this.rdbtnNoDstribution.Tag = "0";
            this.rdbtnNoDstribution.Text = "Undistributed";
            this.rdbtnNoDstribution.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 76);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 288);
            this.panel2.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtInspectorId);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.CbbInspector);
            this.panel5.Location = new System.Drawing.Point(15, 137);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(386, 67);
            this.panel5.TabIndex = 5;
            // 
            // txtInspectorId
            // 
            this.txtInspectorId.Location = new System.Drawing.Point(220, 21);
            this.txtInspectorId.Name = "txtInspectorId";
            this.txtInspectorId.ReadOnly = true;
            this.txtInspectorId.Size = new System.Drawing.Size(142, 25);
            this.txtInspectorId.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Inspector";
            // 
            // CbbInspector
            // 
            this.CbbInspector.FormattingEnabled = true;
            this.CbbInspector.Location = new System.Drawing.Point(99, 21);
            this.CbbInspector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CbbInspector.Name = "CbbInspector";
            this.CbbInspector.Size = new System.Drawing.Size(119, 25);
            this.CbbInspector.TabIndex = 2;
            this.CbbInspector.SelectedIndexChanged += new System.EventHandler(this.CbbInspector_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.ChkbxUseDate);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.dtpEnd);
            this.panel4.Controls.Add(this.dtpStart);
            this.panel4.Location = new System.Drawing.Point(15, 18);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 110);
            this.panel4.TabIndex = 4;
            // 
            // ChkbxUseDate
            // 
            this.ChkbxUseDate.AutoSize = true;
            this.ChkbxUseDate.Location = new System.Drawing.Point(19, 21);
            this.ChkbxUseDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChkbxUseDate.Name = "ChkbxUseDate";
            this.ChkbxUseDate.Size = new System.Drawing.Size(131, 21);
            this.ChkbxUseDate.TabIndex = 4;
            this.ChkbxUseDate.Text = "Regulation Date";
            this.ChkbxUseDate.UseVisualStyleBackColor = true;
            this.ChkbxUseDate.CheckedChanged += new System.EventHandler(this.ChkbxUseDate_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "~";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Enabled = false;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(217, 58);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(145, 25);
            this.dtpEnd.TabIndex = 6;
            // 
            // dtpStart
            // 
            this.dtpStart.Enabled = false;
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(19, 58);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(145, 25);
            this.dtpStart.TabIndex = 5;
            this.dtpStart.Value = new System.DateTime(2019, 6, 3, 9, 29, 12, 0);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BtnClose);
            this.panel3.Controls.Add(this.BtnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 296);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(415, 68);
            this.panel3.TabIndex = 3;
            // 
            // BtnClose
            // 
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(314, 19);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(86, 32);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Cancel";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Location = new System.Drawing.Point(222, 19);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(86, 32);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "Look Up";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // dsInspector
            // 
            this.dsInspector.DataSetName = "NewDataSet";
            this.dsInspector.Tables.AddRange(new System.Data.DataTable[] {
            this.Inspector});
            // 
            // Inspector
            // 
            this.Inspector.Columns.AddRange(new System.Data.DataColumn[] {
            this.user_id,
            this.user_nm});
            this.Inspector.TableName = "Inspector";
            // 
            // user_id
            // 
            this.user_id.ColumnName = "user_id";
            // 
            // user_nm
            // 
            this.user_nm.Caption = "Desc";
            this.user_nm.ColumnName = "user_nm";
            // 
            // dsCode
            // 
            this.dsCode.DataSetName = "NewDataSet";
            this.dsCode.Tables.AddRange(new System.Data.DataTable[] {
            this.Code});
            // 
            // Code
            // 
            this.Code.Columns.AddRange(new System.Data.DataColumn[] {
            this.cd,
            this.cd_nm});
            this.Code.TableName = "Code";
            // 
            // cd
            // 
            this.cd.ColumnName = "cd";
            // 
            // cd_nm
            // 
            this.cd_nm.ColumnName = "cd_nm";
            // 
            // FrmLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 364);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lookup";
            this.Deactivate += new System.EventHandler(this.FrmLookup_Deactivate);
            this.Load += new System.EventHandler(this.FrmLookup_Load);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbbInspector;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ChkbxUseDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.RadioButton rdbtnAll;
        private System.Windows.Forms.RadioButton rdbtnDistribution;
        private System.Windows.Forms.RadioButton rdbtnNoDstribution;
        private System.Data.DataSet dsInspector;
        private System.Data.DataTable Inspector;
        private System.Data.DataColumn user_id;
        private System.Data.DataColumn user_nm;
        private System.Data.DataSet dsCode;
        private System.Data.DataTable Code;
        private System.Data.DataColumn cd;
        private System.Data.DataColumn cd_nm;
        private System.Windows.Forms.TextBox txtInspectorId;
    }
}