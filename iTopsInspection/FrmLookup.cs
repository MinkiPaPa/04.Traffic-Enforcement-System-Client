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

namespace iTopsInspection
{
    public partial class FrmLookup : Form
    {
        //static int iHForm = -1;
        //static int iHPnlKind = -1;
        ////static int iUserLevel = -1;
        //static bool bIsSupervisor = false;



        public FrmLookup()
        {
            InitializeComponent();

            //
            Fn_SetCodeWithDB();
        }

        // 닫기
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 선택된 조회 종류 반환(미분배, 분배, 전체) - Supervisor
        public int GFn_GetLookupKind_SV(ref String strLookupKind_SV)
        {
            int rv = -1;
            strLookupKind_SV = "";

            if (rdbtnUnChecked_SV.Checked)
            {
                rv = 11;
                strLookupKind_SV = rdbtnUnChecked_SV.Text;
            }
            else if (rdbtnChecked_SV.Checked)
            {
                rv = 12;
                strLookupKind_SV = rdbtnChecked_SV.Text;
            }
            else if (rdbtnAll_SV.Checked)
            {
                rv = 13;
                strLookupKind_SV = rdbtnAll_SV.Text;
            }

            return rv;
        }

        // 선택된 조회 종류 반환(미분배, 분배, 전체)
        public int GFn_GetLookupKind(ref String strLookupKind)
        {
            int rv = -1;
            strLookupKind = "";

            if (rdbtnUnChecked.Checked)
            {
                rv = 1;
                strLookupKind = rdbtnUnChecked.Text;
            }
            else if (rdbtnChecked.Checked)
            {
                rv = 2;
                strLookupKind = rdbtnChecked.Text;
            }
            else if (rdbtnAll.Checked)
            {
                rv = 3;
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

        // 선택된 Officer 반환
        public bool GFn_GetOfficer(ref int index, ref String strId, ref String strNm)
        {
            bool rv = false;

            strId = "";
            strNm = "";
            index = -1;
            try
            {
                index = CbbOfficer.SelectedIndex;
                strId = CbbOfficer.SelectedValue.ToString();
                strNm = CbbOfficer.Text;

                rv = true;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                rv = false;
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
        public void GFn_SetInit( int iKind_SV, int iKind, bool bUseDate, String sStart, String sEnd, int iOfficerindex, String sOfficerId, int iInspectorindex, String sInspectorId)
        {

            // 조회 종류 설정 - Supervisor
            if (iKind_SV == 11) this.rdbtnUnChecked_SV.Checked = true;
            else if (iKind_SV == 12) this.rdbtnChecked_SV.Checked = true;
            else if (iKind_SV == 13) this.rdbtnAll_SV.Checked = true;

            // 조회 종류 설정
            if (iKind == 1) this.rdbtnUnChecked.Checked = true;
            else if (iKind == 2) this.rdbtnChecked.Checked = true;
            else if (iKind == 3) this.rdbtnAll.Checked = true;

            // 기간사용 여부 설정
            ChkbxUseDate.Checked = bUseDate;

            // 
            if (bUseDate)
            {
                dtpStart.Text = sStart;
                dtpEnd.Text = sEnd;
            }

            // Officer
            if (iOfficerindex >= 0)
                CbbOfficer.SelectedIndex = iOfficerindex;

            // Inspector
            if (iInspectorindex >= 0)
                CbbInspector.SelectedIndex = iInspectorindex;
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
            // Officer 선택 콤보
            CbbOfficer.Items.Clear();
            dsOfficer.Tables.Clear();

            if (iTopsLib.Lib.GFn_SelectOfficer(dsOfficer) < 0) return;

            if (dsOfficer.Tables.Count > 0)
            {

                CbbOfficer.DataSource = dsOfficer.Tables[0];

                CbbOfficer.DisplayMember = "user_nm";
                CbbOfficer.ValueMember = "user_id";

            }
            CbbOfficer.SelectedIndex = 0;
            CbbOfficer_SelectedIndexChanged(null, null);

            // Inspector 선택 콤보
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

        // Officer 선택이 변경되면 옆에 Id 도 보여주자
        private void CbbOfficer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbbOfficer.Items.Count <= 0)
            {
                txtOfficerId.Text = "";

                return;
            }
            txtOfficerId.Text = CbbOfficer.SelectedValue.ToString();


        }
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

        // Inspector와 Supervisor에게 보여 주는 화면을 구분
        private void FrmLookup_Shown(object sender, EventArgs e)
        {

            //try
            //{
            //    // 원래의 폼 : supervisor 화면 높이
            //    if (iHForm < 0)
            //    {
            //        iHForm = this.Height;
            //        iHPnlKind = pnlKind.Height;
            //    }

            //    bIsSupervisor = Lib.GFn_IsSupervisor();

            //    if (bIsSupervisor)  // Supervisor and admin
            //    {
            //        this.Height = iHForm;
            //        pnlKind.Height = iHPnlKind;

            //        pnlInspection_kind.Top = lblSupervisor.Top + lblSupervisor.Height + 2;

            //        lblSupervisor.Visible = true;
            //        pnlSupervisor.Visible = true;
            //        pnlInspector.Visible = true;
            //    }
            //    else // Inspector
            //    {
            //        this.Height = iHForm - pnlInspector.Height - lblSupervisor.Height - 10; // 5 * 2 = 여백 
            //        pnlKind.Height = iHPnlKind - lblSupervisor.Height - 5;

            //        pnlInspection_kind.Top = lblSupervisor.Top;

            //        lblSupervisor.Visible = false;
            //        pnlSupervisor.Visible = false;
            //        pnlInspector.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;
            //}
            //finally
            //{
            //    //
            //}

        }

        // Kind Click Event
        private void Fn_KindClick(object sender, EventArgs e)
        {
            rdbtnUnChecked_SV.Enabled = true;
            rdbtnChecked_SV.Enabled = true;
            rdbtnAll_SV.Enabled = true;

            rdbtnUnChecked.Enabled = true;
            rdbtnChecked.Enabled = true;
            rdbtnAll.Enabled = true;


            try
            {
                
                //MessageBox.Show(sender.GetType().ToString());
                RadioButton rbtn = sender as RadioButton;
                int iSel = Convert.ToInt32(rbtn.Tag.ToString());

                if (iSel == 11)                         // Supervisor UnChecked
                {
                }
                else if (iSel == 12)                    // Supervisor Checked
                {
                    rdbtnUnChecked.Enabled = false;
                    //rdbtnChecked.Enabled = true;
                    rdbtnAll.Enabled = false;
                    rdbtnChecked.Checked = true;
                }
                else if (iSel == 13)                    // Supervisor ALL
                {
                }
                else if (iSel == 1)                     // Inspector UnChecked
                {
                    //rdbtnUnChecked_SV.Enabled = true;
                    rdbtnChecked_SV.Enabled = false;
                    rdbtnAll_SV.Enabled = false;
                    rdbtnUnChecked_SV.Checked = true;
                }
                else if (iSel == 2)                     // Inspector Checked
                {
                }
                else if (iSel == 3)                     // Inspector ALL
                {
                }

            }
            catch (Exception ex)
            {
                String tmpStr = ex.Message;
            }
            finally
            {

            }
        }
    }
}
