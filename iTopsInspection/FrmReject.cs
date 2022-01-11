using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTopsInspection
{
    public partial class FrmReject : Form
    {
        // 생성자
        public FrmReject()
        {
            InitializeComponent();

            Fn_SetInspectorWithDB();
            RdbtnOne.Checked = true;
        }

        // Focus를 잃으면 닫는다 ... MainForm에서 다른 프로그램을 선택하면 닫히게 ...
        // 다른 프로그램에서 띄운 DialogForm과 혼돈이 생긴다.
        private void FrmReject_Deactivate(object sender, EventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }

        // 선택 값 반환
        public bool GFn_GetCondition(ref int iKind
                                   , ref String sId
                                   , ref String sNm
                                   , ref String sCm
                                    )
        {
            try
            {
                if (RdbtnOne.Checked)
                {
                    iKind = 0;
                    sId = CbbInspector.SelectedValue.ToString();
                    sNm = CbbInspector.Text;
                }
                else if (RdbtnALL.Checked)
                {
                    iKind = 1;
                    sId = "";
                    sNm = "";
                }
                else
                {
                    iKind = -1;
                    sId = "";
                    sNm = "";
                }

                sCm = txtCommentary.Text;

                return true;
            }
            catch (Exception ex)
            {

                sCm = ex.Message;
                return false;

            }

        }

        // Inspector 정보 가져 오기
        private void Fn_SetInspectorWithDB()
        {
            CbbInspector.DataSource = null;
            CbbInspector.Items.Clear();
            dsInspector.Tables.Clear();
            if (iTopsLib.Lib.GFn_SelectInspector(dsInspector) < 0) return;

            // 전체 선택 삭제 
            for (int i = 0; i < dsInspector.Tables[0].Rows.Count; i++)
            {
                if (dsInspector.Tables[0].Rows[i].ItemArray[0].ToString() == "ALL")
                    dsInspector.Tables[0].Rows[i].Delete();

            }

            if (dsInspector.Tables.Count > 0)
            {

                CbbInspector.DataSource = dsInspector.Tables[0];

                CbbInspector.DisplayMember = "user_nm";
                CbbInspector.ValueMember = "user_id";

            }
            CbbInspector.SelectedIndex = 0;
            CbbInspector_SelectedIndexChanged(null, null);

        }

        // 한 사람의 자료를 반려 할 것인지 모든 자료를 반려 할 것 인지 선택
        private void RdbtnOne_Click(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton rb = sender as RadioButton;

                String strSel = "ALL";
                gbInspector.Enabled = false;
                if (rb.Checked)
                {
                    strSel = rb.Tag.ToString();
                }

                if (strSel == "ONE")
                {
                    gbInspector.Enabled = true;
                }
            }

        }

        // 대상 Inspetor 선택
        private void CbbInspector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbbInspector.Items.Count <= 0)
            {
                txtInspectorId.Text = "";

                return;
            }
            txtInspectorId.Text = CbbInspector.SelectedValue.ToString();

        }

        // Reject 처리
        private void BtnReject_Click(object sender, EventArgs e)
        {
            this.Deactivate -= FrmReject_Deactivate;
            try
            {
                // 입력 값 확인 - Inspector
                if (RdbtnOne.Checked && CbbInspector.SelectedItem == null)
                {
                    MessageBox.Show("Select a Inspector", "Information"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CbbInspector.Focus();
                    return;
                }

                // 입력 값 확인 - Commentary
                if (txtCommentary.Text == "")
                {
                    MessageBox.Show("Please enter commentary of rejection", "Information"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommentary.Focus();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {
                this.Deactivate += FrmReject_Deactivate;

            }

        }
    }
}
