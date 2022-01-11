namespace iTopsMain
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblPWD = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            resources.ApplyResources(this.BtnOk, "BtnOk");
            this.BtnOk.BackColor = System.Drawing.Color.DimGray;
            this.BtnOk.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.BtnOk.FlatAppearance.BorderSize = 0;
            this.BtnOk.ForeColor = System.Drawing.Color.White;
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.UseVisualStyleBackColor = false;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Controls.Add(this.txtID);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.Enter += new System.EventHandler(this.txtID_Enter);
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            this.txtID.Leave += new System.EventHandler(this.txtID_Leave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblPWD);
            this.panel2.Controls.Add(this.txtPWD);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // txtPWD
            // 
            this.txtPWD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtPWD, "txtPWD");
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.UseSystemPasswordChar = true;
            this.txtPWD.Enter += new System.EventHandler(this.txtPWD_Enter);
            this.txtPWD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPWD_KeyDown);
            this.txtPWD.Leave += new System.EventHandler(this.txtPWD_Leave);
            // 
            // lblPWD
            // 
            this.lblPWD.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblPWD, "lblPWD");
            this.lblPWD.ForeColor = System.Drawing.Color.Gray;
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Click += new System.EventHandler(this.txtPWD_Enter);
            // 
            // lblID
            // 
            this.lblID.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lblID, "lblID");
            this.lblID.ForeColor = System.Drawing.Color.Gray;
            this.lblID.Name = "lblID";
            this.lblID.Click += new System.EventHandler(this.txtID_Enter);
            // 
            // FrmLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.Opacity = 0.97D;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLogin_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblPWD;
    }
}