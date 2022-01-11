namespace iTopsInspection
{
    partial class FrmReject
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
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnReject = new System.Windows.Forms.Button();
            this.dsInspector = new System.Data.DataSet();
            this.Inspector = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RdbtnALL = new System.Windows.Forms.RadioButton();
            this.RdbtnOne = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCommentary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbInspector = new System.Windows.Forms.GroupBox();
            this.CbbInspector = new System.Windows.Forms.ComboBox();
            this.txtInspectorId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbInspector.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(374, 382);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(86, 42);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnReject
            // 
            this.BtnReject.Location = new System.Drawing.Point(281, 382);
            this.BtnReject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnReject.Name = "BtnReject";
            this.BtnReject.Size = new System.Drawing.Size(86, 42);
            this.BtnReject.TabIndex = 9;
            this.BtnReject.Text = "Reject";
            this.BtnReject.UseVisualStyleBackColor = true;
            this.BtnReject.Click += new System.EventHandler(this.BtnReject_Click);
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
            this.dataColumn1,
            this.dataColumn2});
            this.Inspector.TableName = "Inspector";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "user_id";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "user_nm";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RdbtnALL);
            this.groupBox2.Controls.Add(this.RdbtnOne);
            this.groupBox2.Location = new System.Drawing.Point(14, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(446, 96);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Reject Kind";
            // 
            // RdbtnALL
            // 
            this.RdbtnALL.AutoSize = true;
            this.RdbtnALL.Checked = true;
            this.RdbtnALL.Location = new System.Drawing.Point(110, 55);
            this.RdbtnALL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RdbtnALL.Name = "RdbtnALL";
            this.RdbtnALL.Size = new System.Drawing.Size(176, 21);
            this.RdbtnALL.TabIndex = 1;
            this.RdbtnALL.TabStop = true;
            this.RdbtnALL.Tag = "ALL";
            this.RdbtnALL.Text = "Everyone\'s Inspections";
            this.RdbtnALL.UseVisualStyleBackColor = true;
            this.RdbtnALL.Click += new System.EventHandler(this.RdbtnOne_Click);
            // 
            // RdbtnOne
            // 
            this.RdbtnOne.AutoSize = true;
            this.RdbtnOne.Location = new System.Drawing.Point(110, 26);
            this.RdbtnOne.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RdbtnOne.Name = "RdbtnOne";
            this.RdbtnOne.Size = new System.Drawing.Size(174, 21);
            this.RdbtnOne.TabIndex = 0;
            this.RdbtnOne.Tag = "ONE";
            this.RdbtnOne.Text = "A Person\'s inspections";
            this.RdbtnOne.UseVisualStyleBackColor = true;
            this.RdbtnOne.Click += new System.EventHandler(this.RdbtnOne_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCommentary);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(14, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 171);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // txtCommentary
            // 
            this.txtCommentary.Location = new System.Drawing.Point(110, 24);
            this.txtCommentary.Multiline = true;
            this.txtCommentary.Name = "txtCommentary";
            this.txtCommentary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCommentary.Size = new System.Drawing.Size(318, 131);
            this.txtCommentary.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Commentary";
            // 
            // gbInspector
            // 
            this.gbInspector.Controls.Add(this.CbbInspector);
            this.gbInspector.Controls.Add(this.txtInspectorId);
            this.gbInspector.Controls.Add(this.label1);
            this.gbInspector.Location = new System.Drawing.Point(14, 112);
            this.gbInspector.Name = "gbInspector";
            this.gbInspector.Size = new System.Drawing.Size(446, 72);
            this.gbInspector.TabIndex = 15;
            this.gbInspector.TabStop = false;
            // 
            // CbbInspector
            // 
            this.CbbInspector.FormattingEnabled = true;
            this.CbbInspector.Location = new System.Drawing.Point(110, 28);
            this.CbbInspector.Name = "CbbInspector";
            this.CbbInspector.Size = new System.Drawing.Size(172, 25);
            this.CbbInspector.TabIndex = 18;
            this.CbbInspector.SelectedIndexChanged += new System.EventHandler(this.CbbInspector_SelectedIndexChanged);
            // 
            // txtInspectorId
            // 
            this.txtInspectorId.Location = new System.Drawing.Point(288, 28);
            this.txtInspectorId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInspectorId.Name = "txtInspectorId";
            this.txtInspectorId.ReadOnly = true;
            this.txtInspectorId.Size = new System.Drawing.Size(140, 25);
            this.txtInspectorId.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Inspector";
            // 
            // FrmReject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 443);
            this.Controls.Add(this.gbInspector);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnReject);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmReject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reject";
            this.Deactivate += new System.EventHandler(this.FrmReject_Deactivate);
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbInspector.ResumeLayout(false);
            this.gbInspector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnReject;
        private System.Data.DataSet dsInspector;
        private System.Data.DataTable Inspector;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton RdbtnALL;
        private System.Windows.Forms.RadioButton RdbtnOne;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCommentary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbInspector;
        private System.Windows.Forms.TextBox txtInspectorId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbbInspector;
    }
}