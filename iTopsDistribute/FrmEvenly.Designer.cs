namespace iTopsDistribute
{
    partial class FrmEvenly
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
            this.dsInspector = new System.Data.DataSet();
            this.Inspector = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnDistribute = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblIns = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChkbxAll = new System.Windows.Forms.CheckBox();
            this.LvInspector = new System.Windows.Forms.ListView();
            this.chSel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUserNm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUserId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.BtnDistribute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 262);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 72);
            this.panel1.TabIndex = 13;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(160, 17);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(86, 42);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnDistribute
            // 
            this.BtnDistribute.Location = new System.Drawing.Point(68, 17);
            this.BtnDistribute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnDistribute.Name = "BtnDistribute";
            this.BtnDistribute.Size = new System.Drawing.Size(86, 42);
            this.BtnDistribute.TabIndex = 11;
            this.BtnDistribute.Text = "Distribute";
            this.BtnDistribute.UseVisualStyleBackColor = true;
            this.BtnDistribute.Click += new System.EventHandler(this.BtnDistribute_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 262);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblIns);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(258, 28);
            this.panel3.TabIndex = 14;
            // 
            // lblIns
            // 
            this.lblIns.AutoSize = true;
            this.lblIns.Location = new System.Drawing.Point(5, 5);
            this.lblIns.Name = "lblIns";
            this.lblIns.Size = new System.Drawing.Size(68, 17);
            this.lblIns.TabIndex = 0;
            this.lblIns.Text = "Inspector";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ChkbxAll);
            this.panel4.Controls.Add(this.LvInspector);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(258, 262);
            this.panel4.TabIndex = 15;
            // 
            // ChkbxAll
            // 
            this.ChkbxAll.AutoSize = true;
            this.ChkbxAll.Location = new System.Drawing.Point(9, 39);
            this.ChkbxAll.Name = "ChkbxAll";
            this.ChkbxAll.Size = new System.Drawing.Size(15, 14);
            this.ChkbxAll.TabIndex = 16;
            this.ChkbxAll.UseVisualStyleBackColor = true;
            this.ChkbxAll.CheckedChanged += new System.EventHandler(this.ChkbxAll_CheckedChanged);
            this.ChkbxAll.Click += new System.EventHandler(this.ChkbxAll_Click);
            // 
            // LvInspector
            // 
            this.LvInspector.CheckBoxes = true;
            this.LvInspector.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSel,
            this.chUserNm,
            this.chUserId});
            this.LvInspector.FullRowSelect = true;
            this.LvInspector.GridLines = true;
            this.LvInspector.Location = new System.Drawing.Point(3, 29);
            this.LvInspector.Margin = new System.Windows.Forms.Padding(0);
            this.LvInspector.Name = "LvInspector";
            this.LvInspector.Size = new System.Drawing.Size(253, 230);
            this.LvInspector.TabIndex = 15;
            this.LvInspector.UseCompatibleStateImageBehavior = false;
            this.LvInspector.View = System.Windows.Forms.View.Details;
            this.LvInspector.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.LvInspector_DrawColumnHeader);
            this.LvInspector.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.LvInspector_DrawItem);
            this.LvInspector.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.LvInspector_DrawSubItem);
            // 
            // chSel
            // 
            this.chSel.Text = "";
            this.chSel.Width = 22;
            // 
            // chUserNm
            // 
            this.chUserNm.Text = "Name";
            this.chUserNm.Width = 210;
            // 
            // chUserId
            // 
            this.chUserId.Text = "ID";
            this.chUserId.Width = 1;
            // 
            // FrmEvenly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 334);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEvenly";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distribute Evenly";
            this.Deactivate += new System.EventHandler(this.FrmEvenly_Deactivate);
            this.Load += new System.EventHandler(this.FrmEvenly_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataSet dsInspector;
        private System.Data.DataTable Inspector;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnDistribute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblIns;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView LvInspector;
        private System.Windows.Forms.ColumnHeader chSel;
        private System.Windows.Forms.ColumnHeader chUserNm;
        private System.Windows.Forms.ColumnHeader chUserId;
        private System.Windows.Forms.CheckBox ChkbxAll;
    }
}