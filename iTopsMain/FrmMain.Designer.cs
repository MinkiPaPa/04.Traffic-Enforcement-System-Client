namespace iTopsMain
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.MainMn = new System.Windows.Forms.MenuStrip();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnIt_Login = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnIt_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.codeMagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.offenceCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnIt_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MnIt_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TabClientArea = new System.Windows.Forms.TabControl();
            this.timerCloseTab = new System.Windows.Forms.Timer(this.components);
            this.picBack = new System.Windows.Forms.PictureBox();
            this.MainMn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMn
            // 
            this.MainMn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.MainMn, "MainMn");
            this.MainMn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.codeMagementToolStripMenuItem,
            this.MnIt_File});
            this.MainMn.Name = "MainMn";
            this.MainMn.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainMn_ItemClicked);
            this.MainMn.Click += new System.EventHandler(this.MainMn_Click);
            this.MainMn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMn_MouseMove);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnIt_Login,
            this.toolStripSeparator3,
            this.MnIt_Exit});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            resources.ApplyResources(this.connectToolStripMenuItem, "connectToolStripMenuItem");
            this.connectToolStripMenuItem.Tag = "";
            // 
            // MnIt_Login
            // 
            this.MnIt_Login.Name = "MnIt_Login";
            resources.ApplyResources(this.MnIt_Login, "MnIt_Login");
            this.MnIt_Login.Tag = "";
            this.MnIt_Login.Click += new System.EventHandler(this.MnIt_Login_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Tag = "0;0";
            // 
            // MnIt_Exit
            // 
            this.MnIt_Exit.Name = "MnIt_Exit";
            resources.ApplyResources(this.MnIt_Exit, "MnIt_Exit");
            this.MnIt_Exit.Tag = "";
            this.MnIt_Exit.Click += new System.EventHandler(this.MnIt_Exit_Click);
            // 
            // codeMagementToolStripMenuItem
            // 
            this.codeMagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersToolStripMenuItem,
            this.masterCodeToolStripMenuItem,
            this.toolStripSeparator4,
            this.offenceCodeToolStripMenuItem,
            this.locationCodeToolStripMenuItem,
            this.locationMappingToolStripMenuItem});
            this.codeMagementToolStripMenuItem.Name = "codeMagementToolStripMenuItem";
            resources.ApplyResources(this.codeMagementToolStripMenuItem, "codeMagementToolStripMenuItem");
            this.codeMagementToolStripMenuItem.Tag = "3,3";
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            resources.ApplyResources(this.usersToolStripMenuItem, "usersToolStripMenuItem");
            this.usersToolStripMenuItem.Tag = "3,4";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // masterCodeToolStripMenuItem
            // 
            this.masterCodeToolStripMenuItem.Name = "masterCodeToolStripMenuItem";
            resources.ApplyResources(this.masterCodeToolStripMenuItem, "masterCodeToolStripMenuItem");
            this.masterCodeToolStripMenuItem.Tag = "3,10";
            this.masterCodeToolStripMenuItem.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Tag = "3,3";
            // 
            // offenceCodeToolStripMenuItem
            // 
            this.offenceCodeToolStripMenuItem.Name = "offenceCodeToolStripMenuItem";
            resources.ApplyResources(this.offenceCodeToolStripMenuItem, "offenceCodeToolStripMenuItem");
            this.offenceCodeToolStripMenuItem.Tag = "3,4";
            this.offenceCodeToolStripMenuItem.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // locationCodeToolStripMenuItem
            // 
            this.locationCodeToolStripMenuItem.Name = "locationCodeToolStripMenuItem";
            resources.ApplyResources(this.locationCodeToolStripMenuItem, "locationCodeToolStripMenuItem");
            this.locationCodeToolStripMenuItem.Tag = "3,10";
            this.locationCodeToolStripMenuItem.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // locationMappingToolStripMenuItem
            // 
            this.locationMappingToolStripMenuItem.Name = "locationMappingToolStripMenuItem";
            resources.ApplyResources(this.locationMappingToolStripMenuItem, "locationMappingToolStripMenuItem");
            this.locationMappingToolStripMenuItem.Tag = "3,4";
            this.locationMappingToolStripMenuItem.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // MnIt_File
            // 
            this.MnIt_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnIt_Import,
            this.toolStripSeparator1});
            resources.ApplyResources(this.MnIt_File, "MnIt_File");
            this.MnIt_File.Name = "MnIt_File";
            this.MnIt_File.Tag = "1,2";
            // 
            // MnIt_Import
            // 
            this.MnIt_Import.Name = "MnIt_Import";
            resources.ApplyResources(this.MnIt_Import, "MnIt_Import");
            this.MnIt_Import.Tag = "1,4";
            this.MnIt_Import.Click += new System.EventHandler(this.MnIt_ExecChild_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Tag = "2,4";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // TabClientArea
            // 
            resources.ApplyResources(this.TabClientArea, "TabClientArea");
            this.TabClientArea.Name = "TabClientArea";
            this.TabClientArea.SelectedIndex = 0;
            this.TabClientArea.TabStop = false;
            this.TabClientArea.SelectedIndexChanged += new System.EventHandler(this.TabClientArea_SelectedIndexChanged);
            this.TabClientArea.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.TabClientArea_ControlAdded);
            // 
            // timerCloseTab
            // 
            this.timerCloseTab.Tick += new System.EventHandler(this.timerCloseTab_Tick);
            // 
            // picBack
            // 
            this.picBack.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.picBack, "picBack");
            this.picBack.Name = "picBack";
            this.picBack.TabStop = false;
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picBack);
            this.Controls.Add(this.TabClientArea);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMn);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMn;
            this.Name = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.FrmMain_DpiChanged);
            this.MainMn.ResumeLayout(false);
            this.MainMn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMn;
        private System.Windows.Forms.ToolStripMenuItem MnIt_File;
        private System.Windows.Forms.ToolStripMenuItem MnIt_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnIt_Login;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MnIt_Exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl TabClientArea;
        private System.Windows.Forms.Timer timerCloseTab;
        private System.Windows.Forms.ToolStripMenuItem codeMagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem locationCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offenceCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationMappingToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBack;
    }
}

