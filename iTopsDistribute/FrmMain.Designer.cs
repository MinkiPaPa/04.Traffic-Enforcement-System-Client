namespace iTopsDistribute
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnEvenly = new System.Windows.Forms.Button();
            this.BtnAssign = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnLookup = new System.Windows.Forms.Button();
            this.lblLookupCondition = new System.Windows.Forms.Label();
            this.lblLookupConditionTit = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.DBGrid_ORG = new System.Windows.Forms.DataGridView();
            this.cntMenu = new System.Windows.Forms.ContextMenuStrip();
            this.TsmnSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmnRelease = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsmnAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.distributeEvenlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChkbxAll = new System.Windows.Forms.CheckBox();
            this.DBGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dsDistribution = new System.Data.DataSet();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid_ORG)).BeginInit();
            this.cntMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDistribution)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.pnlTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1900, 120);
            this.panel1.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.BtnEvenly);
            this.panel4.Controls.Add(this.BtnAssign);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1900, 60);
            this.panel4.TabIndex = 1;
            // 
            // BtnEvenly
            // 
            this.BtnEvenly.Location = new System.Drawing.Point(92, 11);
            this.BtnEvenly.Name = "BtnEvenly";
            this.BtnEvenly.Size = new System.Drawing.Size(141, 35);
            this.BtnEvenly.TabIndex = 7;
            this.BtnEvenly.Text = "Distribute Evenly";
            this.BtnEvenly.UseVisualStyleBackColor = true;
            this.BtnEvenly.Click += new System.EventHandler(this.BtnEvenly_Click);
            // 
            // BtnAssign
            // 
            this.BtnAssign.Location = new System.Drawing.Point(11, 11);
            this.BtnAssign.Name = "BtnAssign";
            this.BtnAssign.Size = new System.Drawing.Size(75, 35);
            this.BtnAssign.TabIndex = 6;
            this.BtnAssign.Text = "Assign";
            this.BtnAssign.UseVisualStyleBackColor = true;
            this.BtnAssign.Click += new System.EventHandler(this.TsmnAssign_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Gray;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.panel5);
            this.pnlTop.Controls.Add(this.lblLookupCondition);
            this.pnlTop.Controls.Add(this.lblLookupConditionTit);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1900, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.BtnClose);
            this.panel5.Controls.Add(this.BtnSave);
            this.panel5.Controls.Add(this.BtnLookup);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1597, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(301, 58);
            this.panel5.TabIndex = 7;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(200, 11);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(86, 35);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(107, 11);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(86, 35);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnLookup
            // 
            this.BtnLookup.Location = new System.Drawing.Point(15, 11);
            this.BtnLookup.Name = "BtnLookup";
            this.BtnLookup.Size = new System.Drawing.Size(86, 35);
            this.BtnLookup.TabIndex = 4;
            this.BtnLookup.Text = "Lookup";
            this.BtnLookup.UseVisualStyleBackColor = true;
            this.BtnLookup.Click += new System.EventHandler(this.BtnLookup_Click);
            // 
            // lblLookupCondition
            // 
            this.lblLookupCondition.AutoSize = true;
            this.lblLookupCondition.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLookupCondition.ForeColor = System.Drawing.Color.Blue;
            this.lblLookupCondition.Location = new System.Drawing.Point(134, 38);
            this.lblLookupCondition.Name = "lblLookupCondition";
            this.lblLookupCondition.Size = new System.Drawing.Size(20, 17);
            this.lblLookupCondition.TabIndex = 6;
            this.lblLookupCondition.Text = "...";
            // 
            // lblLookupConditionTit
            // 
            this.lblLookupConditionTit.AutoSize = true;
            this.lblLookupConditionTit.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLookupConditionTit.Location = new System.Drawing.Point(6, 38);
            this.lblLookupConditionTit.Name = "lblLookupConditionTit";
            this.lblLookupConditionTit.Size = new System.Drawing.Size(136, 17);
            this.lblLookupConditionTit.TabIndex = 5;
            this.lblLookupConditionTit.Text = "Look up Condition : ";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrid.Controls.Add(this.DBGrid_ORG);
            this.pnlGrid.Controls.Add(this.ChkbxAll);
            this.pnlGrid.Controls.Add(this.DBGrid);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 120);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1900, 904);
            this.pnlGrid.TabIndex = 3;
            // 
            // DBGrid_ORG
            // 
            this.DBGrid_ORG.AllowUserToAddRows = false;
            this.DBGrid_ORG.AllowUserToDeleteRows = false;
            this.DBGrid_ORG.AllowUserToResizeRows = false;
            this.DBGrid_ORG.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid_ORG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGrid_ORG.ContextMenuStrip = this.cntMenu;
            this.DBGrid_ORG.Location = new System.Drawing.Point(5000, 5000);
            this.DBGrid_ORG.Name = "DBGrid_ORG";
            this.DBGrid_ORG.RowHeadersWidth = 20;
            this.DBGrid_ORG.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DBGrid_ORG.RowTemplate.Height = 23;
            this.DBGrid_ORG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DBGrid_ORG.Size = new System.Drawing.Size(518, 743);
            this.DBGrid_ORG.TabIndex = 4;
            this.DBGrid_ORG.Visible = false;
            this.DBGrid_ORG.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DbGrid_Scroll);
            // 
            // cntMenu
            // 
            this.cntMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmnSelect,
            this.TsmnRelease,
            this.toolStripSeparator1,
            this.TsmnAssign,
            this.toolStripSeparator2,
            this.distributeEvenlyToolStripMenuItem});
            this.cntMenu.Name = "cntMenu";
            this.cntMenu.Size = new System.Drawing.Size(165, 104);
            // 
            // TsmnSelect
            // 
            this.TsmnSelect.Name = "TsmnSelect";
            this.TsmnSelect.Size = new System.Drawing.Size(164, 22);
            this.TsmnSelect.Text = "Select";
            this.TsmnSelect.Click += new System.EventHandler(this.TsmnSelect_Click);
            // 
            // TsmnRelease
            // 
            this.TsmnRelease.Name = "TsmnRelease";
            this.TsmnRelease.Size = new System.Drawing.Size(164, 22);
            this.TsmnRelease.Text = "Release";
            this.TsmnRelease.Click += new System.EventHandler(this.TsmnRelease_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // TsmnAssign
            // 
            this.TsmnAssign.Name = "TsmnAssign";
            this.TsmnAssign.Size = new System.Drawing.Size(164, 22);
            this.TsmnAssign.Text = "Assign Inspector";
            this.TsmnAssign.Click += new System.EventHandler(this.TsmnAssign_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // distributeEvenlyToolStripMenuItem
            // 
            this.distributeEvenlyToolStripMenuItem.Name = "distributeEvenlyToolStripMenuItem";
            this.distributeEvenlyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.distributeEvenlyToolStripMenuItem.Text = "Distribute Evenly";
            this.distributeEvenlyToolStripMenuItem.Click += new System.EventHandler(this.BtnEvenly_Click);
            // 
            // ChkbxAll
            // 
            this.ChkbxAll.AutoSize = true;
            this.ChkbxAll.Location = new System.Drawing.Point(10, 15);
            this.ChkbxAll.Name = "ChkbxAll";
            this.ChkbxAll.Size = new System.Drawing.Size(15, 14);
            this.ChkbxAll.TabIndex = 2;
            this.ChkbxAll.UseVisualStyleBackColor = true;
            this.ChkbxAll.Visible = false;
            this.ChkbxAll.CheckedChanged += new System.EventHandler(this.ChkbxAll_CheckedChanged);
            // 
            // DBGrid
            // 
            this.DBGrid.ContextMenuStrip = this.cntMenu;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.DBGrid.DisplayLayout.Appearance = appearance1;
            this.DBGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.DBGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.DBGrid.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DBGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.DBGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.DBGrid.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.DBGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.DBGrid.DisplayLayout.MaxColScrollRegions = 1;
            this.DBGrid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DBGrid.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.DBGrid.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.DBGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.DBGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.DBGrid.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.DBGrid.DisplayLayout.Override.CellAppearance = appearance8;
            this.DBGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.DBGrid.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.DBGrid.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.DBGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.DBGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.DBGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            this.DBGrid.DisplayLayout.Override.MaxSelectedRows = 10000;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.DBGrid.DisplayLayout.Override.RowAppearance = appearance11;
            this.DBGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.DBGrid.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.DBGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DBGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.DBGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.DBGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.DBGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.DBGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 0);
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.Size = new System.Drawing.Size(1898, 902);
            this.DBGrid.TabIndex = 7;
            this.DBGrid.Text = "ultraGrid1";
            this.DBGrid.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
            this.DBGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DBGrid_MouseClick);
            // 
            // dsDistribution
            // 
            this.dsDistribution.DataSetName = "NewDataSet";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1900, 1024);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Distributions";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.FrmMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid_ORG)).EndInit();
            this.cntMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDistribution)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlTop;
        private System.Data.DataSet dsDistribution;
        private System.Windows.Forms.Label lblLookupCondition;
        private System.Windows.Forms.Label lblLookupConditionTit;
        private System.Windows.Forms.CheckBox ChkbxAll;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnLookup;
        private System.Windows.Forms.ContextMenuStrip cntMenu;
        private System.Windows.Forms.ToolStripMenuItem TsmnSelect;
        private System.Windows.Forms.ToolStripMenuItem TsmnRelease;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TsmnAssign;
        private System.Windows.Forms.Button BtnAssign;
        private System.Windows.Forms.Button BtnEvenly;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem distributeEvenlyToolStripMenuItem;
        private System.Windows.Forms.DataGridView DBGrid_ORG;
        private Infragistics.Win.UltraWinGrid.UltraGrid DBGrid;
    }
}

