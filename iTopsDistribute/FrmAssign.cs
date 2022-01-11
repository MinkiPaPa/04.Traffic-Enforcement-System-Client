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
    public partial class FrmAssign : Form
    {
        public FrmAssign()
        {
            InitializeComponent();

            //
            Fn_SetCodeWithDB();
        }

        private void FrmAssign_Load(object sender, EventArgs e)
        {

        }

        private void Fn_SetCodeWithDB()
        {
            //// 임시로 사용 - DB, FTP 서버 접속
            //iTopsLib.Lib.Fn_Login("admin", "admin");

            CbbInspector.DataSource = null;
            CbbInspector.Items.Clear();
            dsInspector.Tables.Clear();

            if (iTopsLib.Lib.GFn_SelectInspector(dsInspector) < 0) Close();

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

        private void CbbInspector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbbInspector.Items.Count <= 0)
            {
                txtInspectorId.Text = "";

                return;
            }
            txtInspectorId.Text = CbbInspector.SelectedValue.ToString();

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

        //
        private void BtnAssign_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {

        }

        private void FrmAssign_Deactivate(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
