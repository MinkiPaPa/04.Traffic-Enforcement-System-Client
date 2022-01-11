namespace iTopsInspection
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
            this.pnlKind = new System.Windows.Forms.Panel();
            this.lblSupervisor = new System.Windows.Forms.Label();
            this.pnlSupervisor = new System.Windows.Forms.Panel();
            this.rdbtnAll_SV = new System.Windows.Forms.RadioButton();
            this.rdbtnChecked_SV = new System.Windows.Forms.RadioButton();
            this.rdbtnUnChecked_SV = new System.Windows.Forms.RadioButton();
            this.pnlInspection_kind = new System.Windows.Forms.Panel();
            this.rdbtnAll = new System.Windows.Forms.RadioButton();
            this.rdbtnChecked = new System.Windows.Forms.RadioButton();
            this.rdbtnUnChecked = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlInspector = new System.Windows.Forms.Panel();
            this.txtInspectorId = new System.Windows.Forms.TextBox();
            this.lblInspector = new System.Windows.Forms.Label();
            this.CbbInspector = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtOfficerId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CbbOfficer = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChkbxUseDate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.dsOfficer = new System.Data.DataSet();
            this.Officer = new System.Data.DataTable();
            this.user_id = new System.Data.DataColumn();
            this.user_nm = new System.Data.DataColumn();
            this.dsCode = new System.Data.DataSet();
            this.Code = new System.Data.DataTable();
            this.cd = new System.Data.DataColumn();
            this.cd_nm = new System.Data.DataColumn();
            this.dsInspector = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.pnlKind.SuspendLayout();
            this.pnlSupervisor.SuspendLayout();
            this.pnlInspection_kind.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlInspector.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsOfficer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Officer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlKind
            // 
            this.pnlKind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKind.Controls.Add(this.pnlInspection_kind);
            this.pnlKind.Controls.Add(this.lblSupervisor);
            this.pnlKind.Controls.Add(this.pnlSupervisor);
            this.pnlKind.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKind.Location = new System.Drawing.Point(0, 0);
            this.pnlKind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlKind.Name = "pnlKind";
            this.pnlKind.Size = new System.Drawing.Size(415, 80);
            this.pnlKind.TabIndex = 1;
            // 
            // lblSupervisor
            // 
            this.lblSupervisor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSupervisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupervisor.Location = new System.Drawing.Point(15, -1);
            this.lblSupervisor.Name = "lblSupervisor";
            this.lblSupervisor.Size = new System.Drawing.Size(100, 50);
            this.lblSupervisor.TabIndex = 5;
            this.lblSupervisor.Text = "Supervisor";
            this.lblSupervisor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSupervisor.Visible = false;
            // 
            // pnlSupervisor
            // 
            this.pnlSupervisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSupervisor.Controls.Add(this.rdbtnAll_SV);
            this.pnlSupervisor.Controls.Add(this.rdbtnChecked_SV);
            this.pnlSupervisor.Controls.Add(this.rdbtnUnChecked_SV);
            this.pnlSupervisor.Location = new System.Drawing.Point(116, -1);
            this.pnlSupervisor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSupervisor.Name = "pnlSupervisor";
            this.pnlSupervisor.Size = new System.Drawing.Size(285, 50);
            this.pnlSupervisor.TabIndex = 3;
            this.pnlSupervisor.Visible = false;
            // 
            // rdbtnAll_SV
            // 
            this.rdbtnAll_SV.AutoSize = true;
            this.rdbtnAll_SV.Location = new System.Drawing.Point(227, 15);
            this.rdbtnAll_SV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnAll_SV.Name = "rdbtnAll_SV";
            this.rdbtnAll_SV.Size = new System.Drawing.Size(41, 21);
            this.rdbtnAll_SV.TabIndex = 5;
            this.rdbtnAll_SV.Tag = "13";
            this.rdbtnAll_SV.Text = "All";
            this.rdbtnAll_SV.UseVisualStyleBackColor = true;
            this.rdbtnAll_SV.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // rdbtnChecked_SV
            // 
            this.rdbtnChecked_SV.AutoSize = true;
            this.rdbtnChecked_SV.Location = new System.Drawing.Point(127, 15);
            this.rdbtnChecked_SV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnChecked_SV.Name = "rdbtnChecked_SV";
            this.rdbtnChecked_SV.Size = new System.Drawing.Size(84, 21);
            this.rdbtnChecked_SV.TabIndex = 4;
            this.rdbtnChecked_SV.Tag = "12";
            this.rdbtnChecked_SV.Text = "Checked";
            this.rdbtnChecked_SV.UseVisualStyleBackColor = true;
            this.rdbtnChecked_SV.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // rdbtnUnChecked_SV
            // 
            this.rdbtnUnChecked_SV.AutoSize = true;
            this.rdbtnUnChecked_SV.Checked = true;
            this.rdbtnUnChecked_SV.Location = new System.Drawing.Point(10, 15);
            this.rdbtnUnChecked_SV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnUnChecked_SV.Name = "rdbtnUnChecked_SV";
            this.rdbtnUnChecked_SV.Size = new System.Drawing.Size(102, 21);
            this.rdbtnUnChecked_SV.TabIndex = 3;
            this.rdbtnUnChecked_SV.TabStop = true;
            this.rdbtnUnChecked_SV.Tag = "11";
            this.rdbtnUnChecked_SV.Text = "UnChecked";
            this.rdbtnUnChecked_SV.UseVisualStyleBackColor = true;
            this.rdbtnUnChecked_SV.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // pnlInspection_kind
            // 
            this.pnlInspection_kind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInspection_kind.Controls.Add(this.rdbtnAll);
            this.pnlInspection_kind.Controls.Add(this.rdbtnChecked);
            this.pnlInspection_kind.Controls.Add(this.rdbtnUnChecked);
            this.pnlInspection_kind.Location = new System.Drawing.Point(15, 15);
            this.pnlInspection_kind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlInspection_kind.Name = "pnlInspection_kind";
            this.pnlInspection_kind.Size = new System.Drawing.Size(386, 50);
            this.pnlInspection_kind.TabIndex = 2;
            // 
            // rdbtnAll
            // 
            this.rdbtnAll.AutoSize = true;
            this.rdbtnAll.Location = new System.Drawing.Point(279, 15);
            this.rdbtnAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnAll.Name = "rdbtnAll";
            this.rdbtnAll.Size = new System.Drawing.Size(41, 21);
            this.rdbtnAll.TabIndex = 5;
            this.rdbtnAll.Tag = "3";
            this.rdbtnAll.Text = "All";
            this.rdbtnAll.UseVisualStyleBackColor = true;
            this.rdbtnAll.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // rdbtnChecked
            // 
            this.rdbtnChecked.AutoSize = true;
            this.rdbtnChecked.Location = new System.Drawing.Point(160, 15);
            this.rdbtnChecked.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnChecked.Name = "rdbtnChecked";
            this.rdbtnChecked.Size = new System.Drawing.Size(84, 21);
            this.rdbtnChecked.TabIndex = 4;
            this.rdbtnChecked.Tag = "2";
            this.rdbtnChecked.Text = "Checked";
            this.rdbtnChecked.UseVisualStyleBackColor = true;
            this.rdbtnChecked.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // rdbtnUnChecked
            // 
            this.rdbtnUnChecked.AutoSize = true;
            this.rdbtnUnChecked.Checked = true;
            this.rdbtnUnChecked.Location = new System.Drawing.Point(19, 15);
            this.rdbtnUnChecked.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbtnUnChecked.Name = "rdbtnUnChecked";
            this.rdbtnUnChecked.Size = new System.Drawing.Size(102, 21);
            this.rdbtnUnChecked.TabIndex = 3;
            this.rdbtnUnChecked.TabStop = true;
            this.rdbtnUnChecked.Tag = "1";
            this.rdbtnUnChecked.Text = "UnChecked";
            this.rdbtnUnChecked.UseVisualStyleBackColor = true;
            this.rdbtnUnChecked.Click += new System.EventHandler(this.Fn_KindClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlInspector);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 353);
            this.panel2.TabIndex = 2;
            // 
            // pnlInspector
            // 
            this.pnlInspector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInspector.Controls.Add(this.txtInspectorId);
            this.pnlInspector.Controls.Add(this.lblInspector);
            this.pnlInspector.Controls.Add(this.CbbInspector);
            this.pnlInspector.Location = new System.Drawing.Point(15, 204);
            this.pnlInspector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlInspector.Name = "pnlInspector";
            this.pnlInspector.Size = new System.Drawing.Size(386, 67);
            this.pnlInspector.TabIndex = 6;
            // 
            // txtInspectorId
            // 
            this.txtInspectorId.Location = new System.Drawing.Point(220, 21);
            this.txtInspectorId.Name = "txtInspectorId";
            this.txtInspectorId.ReadOnly = true;
            this.txtInspectorId.Size = new System.Drawing.Size(142, 25);
            this.txtInspectorId.TabIndex = 4;
            // 
            // lblInspector
            // 
            this.lblInspector.AutoSize = true;
            this.lblInspector.Location = new System.Drawing.Point(25, 24);
            this.lblInspector.Name = "lblInspector";
            this.lblInspector.Size = new System.Drawing.Size(68, 17);
            this.lblInspector.TabIndex = 3;
            this.lblInspector.Text = "Inspector";
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
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtOfficerId);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.CbbOfficer);
            this.panel5.Location = new System.Drawing.Point(15, 128);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(386, 67);
            this.panel5.TabIndex = 5;
            // 
            // txtOfficerId
            // 
            this.txtOfficerId.Location = new System.Drawing.Point(220, 21);
            this.txtOfficerId.Name = "txtOfficerId";
            this.txtOfficerId.ReadOnly = true;
            this.txtOfficerId.Size = new System.Drawing.Size(142, 25);
            this.txtOfficerId.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Officer";
            // 
            // CbbOfficer
            // 
            this.CbbOfficer.FormattingEnabled = true;
            this.CbbOfficer.Location = new System.Drawing.Point(99, 21);
            this.CbbOfficer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CbbOfficer.Name = "CbbOfficer";
            this.CbbOfficer.Size = new System.Drawing.Size(119, 25);
            this.CbbOfficer.TabIndex = 2;
            this.CbbOfficer.SelectedIndexChanged += new System.EventHandler(this.CbbOfficer_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.ChkbxUseDate);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.dtpEnd);
            this.panel4.Controls.Add(this.dtpStart);
            this.panel4.Location = new System.Drawing.Point(15, 9);
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
            this.panel3.Location = new System.Drawing.Point(0, 365);
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
            // dsOfficer
            // 
            this.dsOfficer.DataSetName = "NewDataSet";
            this.dsOfficer.Tables.AddRange(new System.Data.DataTable[] {
            this.Officer});
            // 
            // Officer
            // 
            this.Officer.Columns.AddRange(new System.Data.DataColumn[] {
            this.user_id,
            this.user_nm});
            this.Officer.TableName = "Officer";
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
            // dsInspector
            // 
            this.dsInspector.DataSetName = "NewDataSet";
            this.dsInspector.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.dataTable1.TableName = "Officer";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "user_id";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "Desc";
            this.dataColumn2.ColumnName = "user_nm";
            // 
            // FrmLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 433);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlKind);
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
            this.Shown += new System.EventHandler(this.FrmLookup_Shown);
            this.pnlKind.ResumeLayout(false);
            this.pnlSupervisor.ResumeLayout(false);
            this.pnlSupervisor.PerformLayout();
            this.pnlInspection_kind.ResumeLayout(false);
            this.pnlInspection_kind.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlInspector.ResumeLayout(false);
            this.pnlInspector.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsOfficer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Officer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlKind;
        private System.Windows.Forms.Panel pnlInspection_kind;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbbOfficer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ChkbxUseDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.RadioButton rdbtnAll;
        private System.Windows.Forms.RadioButton rdbtnChecked;
        private System.Windows.Forms.RadioButton rdbtnUnChecked;
        private System.Data.DataSet dsOfficer;
        private System.Data.DataTable Officer;
        private System.Data.DataColumn user_id;
        private System.Data.DataColumn user_nm;
        private System.Data.DataSet dsCode;
        private System.Data.DataTable Code;
        private System.Data.DataColumn cd;
        private System.Data.DataColumn cd_nm;
        private System.Windows.Forms.TextBox txtOfficerId;
        private System.Windows.Forms.Panel pnlInspector;
        private System.Windows.Forms.TextBox txtInspectorId;
        private System.Windows.Forms.Label lblInspector;
        private System.Windows.Forms.ComboBox CbbInspector;
        private System.Data.DataSet dsInspector;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.Panel pnlSupervisor;
        private System.Windows.Forms.RadioButton rdbtnAll_SV;
        private System.Windows.Forms.RadioButton rdbtnChecked_SV;
        private System.Windows.Forms.RadioButton rdbtnUnChecked_SV;
        private System.Windows.Forms.Label lblSupervisor;
    }
}