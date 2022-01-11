// iTops Lib
using iTopsLib;
using System;
// Process

// file 처리
using System.IO;
using System.Windows.Forms;

//// 화면 깜박임 속도 개선 - property  Dublebuffer 사용하기 위함
//using System.Reflection;


namespace iTopsMain
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            // 메인 Form의 Background Image 설정
            iTopsLib.Lib.SetParent((IntPtr)picBack.Handle, (IntPtr)TabClientArea.Handle);

            Lib.InitLib();
            String strBackFile = Lib.GetBackground();
            if (strBackFile != "")
            {

                strBackFile = @".\Img\" + strBackFile;

                // 파일이 존재하는지 확인
                if (File.Exists(strBackFile))
                {
                    picBack.ImageLocation = strBackFile;
                    picBack.Load();

                }
            }
        }

        // Form Load
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 최대화 
            this.WindowState = FormWindowState.Maximized;

            // 사이즈 조정
            // Client 폭의 1/3 보다 작으면 원래 사이즈
            // 그 외 ( 사이즈가 크거나 ... ) Stretch
            if (picBack.Image != null)
            {
                int imgWidth = picBack.Image.Width;
                int cltWidth = TabClientArea.Width;

                if (imgWidth < cltWidth / 3) picBack.SizeMode = PictureBoxSizeMode.CenterImage;
                else picBack.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void MainMn_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MnIt_Test_Click(object sender, EventArgs e)
        {
            //iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "notepad", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED, true);

        }

        // Login
        private void MnIt_Login_Click(object sender, EventArgs e)
        {

            // 처음 시작이 아니면
            if (TabClientArea.TabCount > 0)
            {
                // 확인 메시지 - 기존에 작업중인 모든 내용이 무시된다는 메시지
                if (MessageBox.Show("All work windows are closed.\n"
                                  + "Unsaved content can not be recovered.\n\n"
                                  + "Do you want to continue anyway?"
                                  , "Warning"
                                  , MessageBoxButtons.YesNoCancel
                                  , MessageBoxIcon.Question) != DialogResult.Yes) return;



                Cursor oldCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    // 열어놓은 창들을 새로운 계정으로 로그인하면 다 닫아야한다.
                    // 1. 먼저 자식들을 종료 시킨다.
                    Lib.GFn_CloseChild();

                    // 2. Tab을 닫는다.
                    if (TabClientArea.TabCount != 0)
                    {
                        for (int i = TabClientArea.TabCount - 1; i >= 0; i--)
                        {
                            TabClientArea.TabPages.RemoveAt(i);

                        }
                        picBack.Visible = true;
                        TabClientArea.Refresh();
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;
                }
                finally
                {
                    this.Cursor = oldCursor;
                }

            }

            // 조회 창 띄우기
            FrmLogin frmLogin = new FrmLogin(this);
            try
            {
                // 로그인 정보 보여 주기
                this.Text = "iTops";
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    // 로그인 정보 보여 주기
                    this.Text = String.Format("iTops - ( User : {0} )"
                                            , Lib.GetUserName()
                                             );

                }
                int iLevel = Lib.GFn_GetUserLevel();
                Lib.GFn_SetMenu(iLevel, MainMn);
            }
            finally
            {
                frmLogin.Close();
            }

        }

        public void SetMenu(bool allowed)
        {

            MnIt_File.Enabled = allowed;

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. 먼저 자식들을 종료 시킨다.
            Lib.GFn_CloseChild();

            // 2. DB 접속을 끊는다.
            iTopsLib.Lib.Fn_DisConnSqlSvr();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // 모두 안보이게
            Lib.GFn_SetMenu(-1, MainMn);

            // Login Click() 호출 
            MnIt_Login_Click(this, e);

        }

        // Run Child
        private void MnIt_ExecChild_Click(object sender, EventArgs e)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 배경 숨기기
                if (picBack.Visible) picBack.Visible = false;
                TabClientArea.Refresh();

                // Child 로 실행
                //bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsUpRGLTN", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED, true);

                if (!(sender is System.Windows.Forms.ToolStripMenuItem)) return;

                String strFullName = Path.GetFileName((sender as System.Windows.Forms.ToolStripMenuItem).ToolTipText);
                String strFileName = Path.GetFileNameWithoutExtension(strFullName);
                String strFileExt = Path.GetExtension(strFullName);


                bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, strFileName, strFileExt, iTopsLib.Lib.SW_SHOWMAXIMIZED, picBack, true);

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {
                this.Cursor = oldCursor;
            }

        }

        // Run Import
        private void MnIt_Import_Click(object sender, EventArgs e)
        {
            //Cursor oldCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    // Child 로 실행
            //    //bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsUpRGLTN", "exe", iTopsLib.Lib.SW_SHOWNORMAL, true);
            //    bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsUpRGLTN", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED, true);
            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;

            //}
            //finally
            //{
            //    this.Cursor = oldCursor;
            //}

        }

        // 프로그램 종료
        private void MnIt_Exit_Click(object sender, EventArgs e)
        {
            // 1. 먼저 Child 모두 종료
            // Lib에 함수 만들것 ... FrmMain_FormClosing Event Handler에서 처리
            //Lib.GFn_CloseChild();

            //// 2. 종료
            Close();
        }

        private void MnIt_Inspection_Click(object sender, EventArgs e)
        {
            //Cursor oldCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    // Child 로 실행
            //    bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsInspection", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED,  true);
            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;

            //}
            //finally
            //{
            //    this.Cursor = oldCursor;
            //}


        }

        // 분배 - Distribution
        private void MnIt_Distribution_Click(object sender, EventArgs e)
        {
            //Cursor oldCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    // Child 로 실행
            //    //bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsDistribute", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED, false);
            //    bool rv = iTopsLib.Lib.GFn_ExecChild(ref TabClientArea, "iTopsDistribute", "exe", iTopsLib.Lib.SW_SHOWMAXIMIZED, true);
            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;

            //}
            //finally
            //{
            //    this.Cursor = oldCursor;
            //}

        }

        //private void MnIt_File_Click(object sender, EventArgs e)
        //{

        //}

        private void MainMn_Click(object sender, EventArgs e)
        {
            timerCloseTab.Enabled = true;

        }

        private void MainMn_MouseMove(object sender, MouseEventArgs e)
        {
            MainMn.Focus();
        }

        private void timerCloseTab_Tick(object sender, EventArgs e)
        {
            int index = iTopsLib.Lib.GFn_GetCloseTabIndex();
            if (index < 0) return;

            try
            {
                TabClientArea.TabPages.RemoveAt(index);
                if (TabClientArea.TabPages.Count <= 0)
                    timerCloseTab.Enabled = false;
                if (TabClientArea.TabCount == 0) picBack.Visible = true;
                TabClientArea.Refresh();
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                iTopsLib.Lib.GFn_ClearCloseTabIndex();
            }
        }

        private void TabClientArea_SelectedIndexChanged(object sender, EventArgs e)
        {

            //// 임시 - iTopsDistribute MDIChild 그리드 문제 해결 전까지
            //TabPage page = TabClientArea.SelectedTab;
            //if (page.Text != "iTopsDistribute") return;


            //Process[] p = Process.GetProcessesByName("iTopsDistribute");
            //if (p.GetLength(0) == 0) return;

            //Lib.SetForegroundWindow(p[0].MainWindowHandle);


        }

        // 생성된 TabPage마다 배경 이미지 넣어주기
        private void TabClientArea_ControlAdded(object sender, ControlEventArgs e)
        {
            //if (e.Control is TabPage)
            //{
            //    if (picBack.Image == null) return;
            //    Image img = (picBack.Image.Clone() as Bitmap);
            //    try
            //    {
            //        (e.Control as TabPage).BackgroundImage = new Bitmap(picBack.Width, picBack.Height);
            //        using (Graphics g = Graphics.FromImage((e.Control as TabPage).BackgroundImage))
            //        {

            //            if (img != null)
            //            {

            //                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //                g.DrawImage(img
            //                          , new Rectangle(0, 0, (e.Control as TabPage).BackgroundImage.Width, (e.Control as TabPage).BackgroundImage.Height)
            //                          , new Rectangle(0, 0, img.Width, img.Height)
            //                          , GraphicsUnit.Pixel
            //                            );
            //            }

            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        String strTmp = ex.Message;
            //    }
            //    finally
            //    {
            //        img.Dispose();
            //        (e.Control as TabPage).Refresh();
            //    }
            //}

        }

        // 해상도 변경시
        private void FrmMain_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            this.Refresh();
        }
    }

    //// 화면 깜박임 속도 개선 - Dublebuffer <------------------ iTopsLib로 이동 생성하자 마자 속성 지정 
    //public static class ExtensionMethods
    //{
    //    // TabPage DubleBuffered 속성 지정 
    //    public static void DoubleBuffered(this TabPage tab, bool setting)
    //    {
    //        try
    //        {
    //            Type tabType = tab.GetType();
    //            PropertyInfo pi = tabType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
    //            pi.SetValue(tab, setting, null);
    //        }
    //        catch (Exception ex)
    //        {
    //            String strTmp = ex.Message;
    //        }
    //    }
    //}

}