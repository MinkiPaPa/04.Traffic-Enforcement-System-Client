namespace iTopsDistribute
{
    partial class FrmAssign
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
            this.label1 = new System.Windows.Forms.Label();
            this.CbbInspector = new System.Windows.Forms.ComboBox();
            this.txtInspectorId = new System.Windows.Forms.TextBox();
            this.BtnAssign = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.dsInspector = new System.Data.DataSet();
            this.Inspector = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inspector";
            // 
            // CbbInspector
            // 
            this.CbbInspector.FormattingEnabled = true;
            this.CbbInspector.Location = new System.Drawing.Point(86, 24);
            this.CbbInspector.Name = "CbbInspector";
            this.CbbInspector.Size = new System.Drawing.Size(150, 25);
            this.CbbInspector.TabIndex = 2;
            this.CbbInspector.SelectedIndexChanged += new System.EventHandler(this.CbbInspector_SelectedIndexChanged);
            // 
            // txtInspectorId
            // 
            this.txtInspectorId.Location = new System.Drawing.Point(242, 24);
            this.txtInspectorId.Name = "txtInspectorId";
            this.txtInspectorId.Size = new System.Drawing.Size(123, 25);
            this.txtInspectorId.TabIndex = 3;
            // 
            // BtnAssign
            // 
            this.BtnAssign.Location = new System.Drawing.Point(209, 65);
            this.BtnAssign.Name = "BtnAssign";
            this.BtnAssign.Size = new System.Drawing.Size(75, 30);
            this.BtnAssign.TabIndex = 4;
            this.BtnAssign.Text = "Assign";
            this.BtnAssign.UseVisualStyleBackColor = true;
            this.BtnAssign.Click += new System.EventHandler(this.BtnAssign_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(290, 65);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 30);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
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
            // FrmAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(383, 107);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnAssign);
            this.Controls.Add(this.txtInspectorId);
            this.Controls.Add(this.CbbInspector);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAssign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assign Inspector";
            this.Deactivate += new System.EventHandler(this.FrmAssign_Deactivate);
            this.Load += new System.EventHandler(this.FrmAssign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inspector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbbInspector;
        private System.Windows.Forms.TextBox txtInspectorId;
        private System.Windows.Forms.Button BtnAssign;
        private System.Windows.Forms.Button BtnCancel;
        private System.Data.DataSet dsInspector;
        private System.Data.DataTable Inspector;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
    }
}