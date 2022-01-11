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
    public partial class FrmLookup : Form
    {

        public FrmLookup()
        {
            InitializeComponent();

            //
            Fn_SetCodeWithDB();
        }

        public void GFn_Close()
        {
            //this.BtnClose_Click(null, null);    
            this.Close();

        }

        // 닫기
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 선택된 조회 종류 반환(미분배, 분배, 전체)
        public int GFn_GetLookupKind(ref String strLookupKind)
        {
            int rv = -1;
            strLookupKind = "";

            if (rdbtnNoDstribution.Checked)
            {
                rv = 0;
                strLookupKind = rdbtnNoDstribution.Text;
            }
            else if (rdbtnDistribution.Checked)
            {
                rv = 1;
                strLookupKind = rdbtnDistribution.Text;
            }
            else if (rdbtnAll.Checked)
            {
                rv = 2;
                strLookupKind = rdbtnAll.Text;
            }

            return rv;
        }

        // 선택된 기간조회 여부 및 기간 반환
        public bool GFn_GetUseDate(ref String strStart, ref String strEnd)
        {
            bool rv = ChkbxUseDate.Checked;
            strStart = "";
            strEnd = "";

            if (rv)
            {
                strStart = dtpStart.Text;
                strEnd = dtpEnd.Text;
            }

            return rv;
        }

        // 선택된 Inspector 반환
        public bool GFn_GetInspector(ref int index, ref String strId, ref String strNm)
        {
            bool rv = false;

            strId = "";
            strNm = "";
            index = -1;
            try
            {
                index = CbbInspector.SelectedIndex;
                strId = CbbInspector.SelectedValue.ToString();
                strNm = CbbInspector.Text;

                rv = true;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                rv = false;
            }

            return rv;

        }

        // 기존 조회 조건으로 설정 
        public void GFn_SetInit( int iKind, bool bUseDate, String sStart, String sEnd, int index, String sId)
        {

            // 조회 종류 설정
            if (iKind == 0) this.rdbtnNoDstribution.Checked = true;
            else if (iKind == 1) this.rdbtnDistribution.Checked = true;
            else if (iKind == 2) this.rdbtnAll.Checked = true;

            // 기간사용 여부 설정
            ChkbxUseDate.Checked = bUseDate;

            // 
            if (bUseDate)
            {
                dtpStart.Text = sStart;
                dtpEnd.Text = sEnd;
            }

            // Inspector
            if (index >= 0)
                //CbbInspector.SelectedIndex = index;
                CbbInspector.SelectedValue = sId;

        }

        // 조회 버튼 Click
        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmLookup_Load(object sender, EventArgs e)
        {
        }

        private void Fn_SetCodeWithDB()
        {
            // 임시로 사용 - DB, FTP 서버 접속
            //iTopsLib.Lib.Fn_Login("admin", "admin");

            CbbInspector.Items.Clear();
            dsInspector.Tables.Clear();

            if (iTopsLib.Lib.GFn_SelectInspector(dsInspector) < 0) return;

            if (dsInspector.Tables.Count > 0)
            {

                CbbInspector.DataSource = dsInspector.Tables[0];

                CbbInspector.DisplayMember = "user_nm";
                CbbInspector.ValueMember = "user_id";

            }
            CbbInspector.SelectedIndex = 0;
            CbbInspector_SelectedIndexChanged(null, null);

        }

        // 기간 조회 여부 
        private void ChkbxUseDate_CheckedChanged(object sender, EventArgs e)
        {
            bool bUse = ChkbxUseDate.Checked;

            dtpStart.Enabled = bUse;
            dtpEnd.Enabled = bUse;
        }

        //private void cbbInspector_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        // Inspector 선택이 변경되면 옆에 Id 도 보여주자
        private void CbbInspector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbbInspector.Items.Count <= 0)
            {
                txtInspectorId.Text = "";

                return;
            }
            txtInspectorId.Text = CbbInspector.SelectedValue.ToString();

        }

        // 다른 프로그램이 실행 되면서 가려지면 닫는다.
        // 다른 창의 조회 창과 동시에 떠 있으면 어느게 맞는지 모른다.
        private void FrmLookup_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
