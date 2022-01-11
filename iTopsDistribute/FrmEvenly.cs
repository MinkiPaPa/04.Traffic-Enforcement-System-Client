using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// DB 사용
using iTopsLib;

namespace iTopsDistribute
{
    public partial class FrmEvenly : Form
    {
        int chkbxAll_Pos = -1;

        public FrmEvenly()
        {
            InitializeComponent();

            //
            Fn_SetCodeWithDB();
        }

        private void Fn_SetCodeWithDB()
        {
            // 임시로 사용 - DB, FTP 서버 접속
            //iTopsLib.Lib.Fn_Login("admin", "admin");

            LvInspector.Items.Clear();
            dsInspector.Tables.Clear();

            if (iTopsLib.Lib.GFn_SelectInspector(dsInspector) < 0) Close();

            // 전체 선택 삭제 
            for (int i = 0; i < dsInspector.Tables[0].Rows.Count; i++)
            {

                if (dsInspector.Tables[0].Rows[i].ItemArray[0].ToString() == "ALL") continue;
                String[] arrInspector = { ""
                                        , dsInspector.Tables[0].Rows[i].ItemArray[1].ToString()
                                        , dsInspector.Tables[0].Rows[i].ItemArray[0].ToString()
                                        };

                ListViewItem lvi = new ListViewItem(arrInspector);   // String [] ---> ListViewItem

                LvInspector.Items.Add(lvi);

            }

        }

        // 선택된 Inspector 반환
        public bool GFn_GetInspector(ref int index, ref String [] strArrID, ref String[] strArrNM)
        {
            bool rv = false;

            // 선택한 갯수 파악
            int iCnt = 0;
            for (int i = 0; i < LvInspector.Items.Count; i++)
            {
                if (LvInspector.Items[i].Checked) iCnt++;
            }

            // 선택 안했으면 퇴짜~!
            if (iCnt == 0) return false;

            // 반환 
            String[] arrTmpID = new String[iCnt];
            String[] arrTmpNM = new String[iCnt];
            int iPos = -1;
            for (int i = 0; i < LvInspector.Items.Count; i++)
            {
                if (!LvInspector.Items[i].Checked) continue;

                iPos++;
                arrTmpID[iPos] = LvInspector.Items[i].SubItems[2].Text;
                arrTmpNM[iPos] = LvInspector.Items[i].SubItems[1].Text;
            }

            if (iPos < 0) return false;

            strArrID = arrTmpID;
            strArrNM = arrTmpNM;

            rv = true;
            return rv;

        }

        private void BtnDistribute_Click(object sender, EventArgs e)
        {
            // 선택된 것이 있는지 확인
            int iCnt = 0;
            for (int i = 0; i < LvInspector.Items.Count; i++)
            {
                if (LvInspector.Items[i].Checked) iCnt++;
            }
            // 선택한 대상이 없으면 메시지 후 선택할 수 있도록...
            if (iCnt <= 0)
            {
                MessageBox.Show("Please choose who you want to distribute and then work on it.", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        //private void lvInspector_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        //{
            //if ((e.ColumnIndex == 0))
            //{
            //    CheckBox chSel = new CheckBox();
            //    Text = "";
            //    LvInspector.SuspendLayout();  // 컨트롤의 레이아웃 논리를 임시로 일시 중단
            //    e.DrawBackground();  // 열 머리글의 배경색을 그리기
            //    chSel.BackColor = Color.Transparent;
            //    chSel.UseVisualStyleBackColor = true;  // 비주얼 스타일을 사용하여 배경을 그리면 true
            //                                           // 컨트롤의 범위를 지정된 위치와 크기로 설정 (Left x, Top y, width, height)
            //    chSel.SetBounds(e.Bounds.X, e.Bounds.Y, chSel.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width, chSel.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width);
            //    // 컨트롤의 높이와 너비를 가져오거나 설정
            //    chSel.Size = new Size((chSel.GetPreferredSize(new Size((e.Bounds.Width - 1), e.Bounds.Height)).Width + 1), e.Bounds.Height);
            //    chSel.Location = new Point(4, 0); // 왼쪽 위를 기준으로 컨트롤의 왼쪽 위의 좌표를 가져오거나 설정
            //    LvInspector.Controls.Add(chSel);
            //    chSel.Show();
            //    //cck.BringToFront();
            //    Visible = true;  // 컨트롤과 모든 해당 자식 컨트롤이 표시되면 true
            //    e.DrawText((TextFormatFlags.VerticalCenter | TextFormatFlags.Left));
            //    //chSel.Click += new EventHandler(Bink);  // 컨트롤을 클릭하면 발생
            //    LvInspector.ResumeLayout(true);  // 일반 레이아웃 논리를 다시 시작

            //}
            //else
            //{
            //    e.DrawDefault = true;
            //}

        //}

        //private void lvInspector_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
            //CheckBox cck = sender as CheckBox;
            //for (int i = 0; i < LvInspector.Items.Count; i++)
            //{
            //    LvInspector.Items[i].Checked = cck.Checked;
            //}
        //}

        //private void lvInspector_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        //{
        //    //e.DrawDefault = true;
        //}

        // 분배
        private void BtnDistribute_Click_1(object sender, EventArgs e)
        {

        }

        private void LvInspector_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if ((e.ColumnIndex == 0))
            {
                CheckBox chSel = new CheckBox();
                Text = "";
                LvInspector.SuspendLayout();  // 컨트롤의 레이아웃 논리를 임시로 일시 중단
                e.DrawBackground();  // 열 머리글의 배경색을 그리기
                chSel.BackColor = Color.Transparent;
                chSel.UseVisualStyleBackColor = true;  // 비주얼 스타일을 사용하여 배경을 그리면 true
                                                       // 컨트롤의 범위를 지정된 위치와 크기로 설정 (Left x, Top y, width, height)
                chSel.SetBounds(e.Bounds.X, e.Bounds.Y, chSel.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width, chSel.GetPreferredSize(new Size(e.Bounds.Width, e.Bounds.Height)).Width);
                // 컨트롤의 높이와 너비를 가져오거나 설정
                chSel.Size = new Size((chSel.GetPreferredSize(new Size((e.Bounds.Width - 1), e.Bounds.Height)).Width + 1), e.Bounds.Height);
                chSel.Location = new Point(4, 0); // 왼쪽 위를 기준으로 컨트롤의 왼쪽 위의 좌표를 가져오거나 설정
                LvInspector.Controls.Add(chSel);
                chSel.Show();
                //cck.BringToFront();
                Visible = true;  // 컨트롤과 모든 해당 자식 컨트롤이 표시되면 true
                e.DrawText((TextFormatFlags.VerticalCenter | TextFormatFlags.Left));
                //chSel.Click += new EventHandler(Bink);  // 컨트롤을 클릭하면 발생
                LvInspector.ResumeLayout(true);  // 일반 레이아웃 논리를 다시 시작

            }
            else
            {
                e.DrawDefault = true;
            }

        }

        private void LvInspector_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            CheckBox cck = sender as CheckBox;
            for (int i = 0; i < LvInspector.Items.Count; i++)
            {
                LvInspector.Items[i].Checked = cck.Checked;
            }

        }

        private void LvInspector_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;

        }

        private void ChkbxAll_Click(object sender, EventArgs e)
        {

        }

        private void FrmEvenly_Load(object sender, EventArgs e)
        {
            // 전체 선택의 초기 위치 기억
            ChkbxAll.Left = 9;
            ChkbxAll.Top = 39;
            if (chkbxAll_Pos < 0)
                chkbxAll_Pos = ChkbxAll.Left;

        }

        private void ChkbxAll_CheckedChanged(object sender, EventArgs e)
        {

            bool bSel = ChkbxAll.Checked;
            foreach (ListViewItem Item in LvInspector.Items)
            {
                Item.Checked = bSel;
            }

        }

        private void FrmEvenly_Deactivate(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
