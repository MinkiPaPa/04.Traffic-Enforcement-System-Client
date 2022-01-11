using System;
using System.Drawing;
using System.Windows.Forms;

//
namespace iTopsMain
{
    public partial class FrmLogin : Form
    {
        FrmMain refFrmMain;

        // 마우스로 창 이동
        private bool bMoving = false;
        private int iStartX, iStartY;


        public FrmLogin(FrmMain frmMain)
        {
            // MainForm 접근용
            this.refFrmMain = frmMain;

            InitializeComponent();

            // 폼 라운드 처리
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(iTopsLib.Lib.CreateRoundRectRgn(0, 0, Width, Height, 18, 18));

            // 로고의 배경색을 Form의 배경색으로 지정 
            //Bitmap bmp = picLogo.Image as Bitmap;
            Bitmap bmp = this.BackgroundImage as Bitmap;
            BtnOk.BackColor = bmp.GetPixel(BtnOk.Left + 3, BtnOk.Top + 3);
            BtnCancel.BackColor = bmp.GetPixel(BtnCancel.Left, BtnCancel.Top);
        }

        // 로그인 
        private void BtnOk_Click(object sender, EventArgs e)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 로그인 입력값 검증 
                if (!ChkLoginInfo()) return;

                // 로그인 시도 
                bool isSuccess = iTopsLib.Lib.Fn_Login(txtID.Text, txtPWD.Text);

                // 로그인 결과에 맞게 메뉴 권한 제어 
                refFrmMain.SetMenu(isSuccess);

                this.DialogResult = DialogResult.OK;
                if (isSuccess) Close();

            }
            catch (Exception ex)
            {
                String tmpStr = ex.Message;
            }
            finally
            {
                this.Cursor = oldCursor;
            }
        }

        // 로그인 입력값 검증 
        private bool ChkLoginInfo()
        {
            if (txtID.Text.Length == 0)
            {
                MessageBox.Show("Input Your ID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                return false;
            }

            if (txtPWD.Text.Length == 0)
            {
                MessageBox.Show("Input Your Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Focus();
                return false;
            }
            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // 로그인 결과에 맞게 메뉴 권한 제어 
            //refFrmMain.SetMenu(false);

            Close();

        }

        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BtnOk_Click(null, null);

        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPWD.Focus();

        }

        // 마우스로 창 이동
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            //
            bMoving = true;
            iStartX = e.X;
            iStartY = e.Y;

        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            //
            if (bMoving)
            {
                int iDiffX = e.X - iStartX;
                int iDiffY = e.Y - iStartY;

                this.Location = new System.Drawing.Point(this.Location.X + iDiffX
                                                       , this.Location.Y + iDiffY);
            }
        }

        private void FrmLogin_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FrmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            //
            bMoving = false;
        }

        private void txtID_Enter(object sender, EventArgs e)
        {
            if (lblID.Visible) lblID.Visible = false;
            txtID.Focus();
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            lblID.Visible = (txtID.Text.Length == 0);

        }

        private void txtPWD_Enter(object sender, EventArgs e)
        {
            if (lblPWD.Visible) lblPWD.Visible = false;
            txtPWD.Focus();

        }

        private void txtPWD_Leave(object sender, EventArgs e)
        {
            lblPWD.Visible = (txtPWD.Text.Length == 0);

        }


    }
}
