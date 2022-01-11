using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
using System.IO;

// 
using iTopsLib;

// DataGridView property doublebuffer 사용하기 위함
using System.Reflection;

namespace iTopsInspection
{
    public partial class FrmMain : Form
    {
        // 접속자 정보
        String USER_INFO = "";

        // 조회조건
        int iLookupKind_SV = -1;
        String strLookupKind_SV = "";
        int iLookupKind = -1;
        String strLookupKind = "";
        bool bUseDate = false;
        String strStart = "";
        String strEnd = "";
        int OfficerIndex = -1;
        String strOfficerId = "";
        String strOfficerNm = "";
        int InspectorIndex = -1;
        String strInspectorId = "";
        String strInspectorNm = "";

        // 이미지 확대 축소
        private Image img;
        private float ratio = 1.0F;
        private Point startPoint = Point.Empty;
        private Point movePoint = Point.Empty;
        bool isClick = false;

        int imgX = 0;
        int imgY = 0;
        //bool mousepressed = false;
        //float zoom = 1;




        // 전체 상황
        private int iLookup = 0;
        private int iConfirmed = 0;
        private int iReadNot = 0;
        private int iRemain = 0;

        private int iConfirmed_SV = 0;
        private int iRejected_SV = 0;
        private int iRemain_SV = 0;

        // Supervisor 여부
        private bool bIsSupervisor = false;

        //// supervisor, inspector용 사이즈 조절 용
        //private int iHCapturing = 0;
        //private int iHReadNotEtc = 0;
        //private int iHInspector = 0;
        //private int iHSupervisor = 0;
        //private int iHMargin = 5;

        //// 초기 좌표
        //private int iTSupervisor, iTInspector, iTVerification, iTPermission, iTReject;


        // 생성자  
        public FrmMain(String [] args)
        {
            // 사용자 정보를 인수로 받는다.
            // 없으면 종료
            if (args.Length < 1)
            {
                Close();
                return;
            }

            //Lib.GFn_Login(args[0]);
            USER_INFO = args[0];

            // 
            InitializeComponent();


            //// Image 확대 축소 이벤트 등록
            //this.picRegulation.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picRegulation1_MouseWheel);

            //// 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            //if (!Lib.GFn_Login(USER_INFO)) Close();

            //// Form_Load
            //// Supervisor 여부
            //bIsSupervisor = iTopsLib.Lib.GFn_IsSupervisor();

            //// Supervisor만 볼 수 있는거
            //lblConfirmedTit_SV.Visible = bIsSupervisor;
            //lblConfirmed_SV.Visible = bIsSupervisor;
            //lblRejectedTit_SV.Visible = bIsSupervisor;
            //lblRejected_SV.Visible = bIsSupervisor;
            //lblRemainTit_SV.Visible = bIsSupervisor;
            //lblRemain_SV.Visible = bIsSupervisor;

            //chkPermission.Visible = bIsSupervisor;
            //txtSupervisor.Visible = bIsSupervisor;
            //BtnReject.Visible = bIsSupervisor;
            //pnlSVStatus.Visible = bIsSupervisor;


            // Form_Shown

            // 화면 Clear
            Fn_ClearScreen();

            //iLookup = 0;
            //iConfirmed = 0;
            //iReadNot = 0;
            //iRemain = 0;

            //iConfirmed_SV = 0;
            //iRejected_SV = 0;
            //iRemain_SV = 0;

            //// Login 등급에 맞게 사이즈, 위치 변경
            //Fn_ChangeCapturingPosition();
        }

        // Form Load
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Image 확대 축소 이벤트 등록
            this.picRegulation.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picRegulation1_MouseWheel);
            this.pnlDetail.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picRegulation1_MouseWheel);

            // 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            if (!Lib.GFn_Login(USER_INFO)) Close();

            // Form_Load
            // Supervisor 여부
            bIsSupervisor = iTopsLib.Lib.GFn_IsSupervisor();

            // Supervisor만 볼 수 있는거
            lblConfirmedTit_SV.Visible = bIsSupervisor;
            lblConfirmed_SV.Visible = bIsSupervisor;
            lblRejectedTit_SV.Visible = bIsSupervisor;
            lblRejected_SV.Visible = bIsSupervisor;
            lblRemainTit_SV.Visible = bIsSupervisor;
            lblRemain_SV.Visible = bIsSupervisor;

            chkPermission.Visible = bIsSupervisor;
            txtSupervisor.Visible = bIsSupervisor;
            BtnReject.Visible = bIsSupervisor;
            pnlSVStatus.Visible = bIsSupervisor;

        }


        // Form Shown
        private void FrmMain_Shown(object sender, EventArgs e)
        {

            //// 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            //if (!Lib.GFn_Login(USER_INFO)) Close();

            //// 화면 Clear
            //Fn_ClearScreen();

            ////iLookup = 0;
            ////iConfirmed = 0;
            ////iReadNot = 0;
            ////iRemain = 0;

            ////iConfirmed_SV = 0;
            ////iRejected_SV = 0;
            ////iRemain_SV = 0;

            //// Login 등급에 맞게 사이즈, 위치 변경
            //Fn_ChangeCapturingPosition();

            // 화면 Clear
            Fn_ClearScreen();

        }


        //
        // 조회 조건 창
        private void BtnLookup_Click(object sender, EventArgs e)
        {
            // 먼저 변경된 자료가 있는지 확인
            int iCnt = Fn_IsChanged();
            if (iCnt > 0)
            {
                if (MessageBox.Show("Changed data exists!!\n" +
                        "Do you ignore the changes?\n\n" +
                        "Yes : Lookup / No : Cancel Lookup", "Question"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            FrmLookup frmLookup = new FrmLookup();
            try
            {
                // 재 조회시 기존 조건으로 설정
                frmLookup.GFn_SetInit(iLookupKind_SV, iLookupKind, bUseDate, strStart, strEnd, OfficerIndex, strOfficerId, InspectorIndex, strInspectorId);
                frmLookup.Owner = this;

                //if (frmLookup.ShowDialog() == DialogResult.OK)
                if (frmLookup.ShowDialog() == DialogResult.OK)

                {
                    // 
                    Fn_SetCodeCombo();

                    // 컨트롤 초기화
                    DbGrid.DataSource = null;
                    DsOffence.Clear();

                    if (picRegulation.Image != null) picRegulation.Image = null;
                    if (picPlate.Image != null) picPlate.Image = null;

                    lblRejectCommTit.Visible = false;
                    lblRejectCommDesc.Visible = false;
                    txtCommentary.Visible = false;

                    PgbPos.Minimum = 0;
                    PgbPos.Maximum = 0;
                    PgbPos.Value = 0;

                    LblPosition.Text = "";
                    lblConfirmed.Text = "";
                    lblRemain.Text = "";

                    OfficerIndex = -1;
                    strOfficerId = "";
                    strOfficerNm = "";

                    InspectorIndex = -1;
                    strInspectorId = "";
                    strInspectorNm = "";

                    iLookupKind_SV = frmLookup.GFn_GetLookupKind_SV(ref strLookupKind_SV);
                    iLookupKind = frmLookup.GFn_GetLookupKind(ref strLookupKind);
                    bUseDate = frmLookup.GFn_GetUseDate(ref strStart, ref strEnd);

                    // Officer 실패하면 다시 초기화 
                    if (!frmLookup.GFn_GetOfficer(ref OfficerIndex, ref strOfficerId, ref strOfficerNm))
                    {
                        OfficerIndex = -1;
                        strOfficerId = "";
                        strOfficerNm = "";
                    }

                    // Inspector 실패하면 다시 초기화 
                    //bool bIsSupervisor = bIsSupervisor; // Supervisor Level 여부 
                    if (bIsSupervisor)
                    {
                        if (!frmLookup.GFn_GetInspector(ref InspectorIndex, ref strInspectorId, ref strInspectorNm))
                        {
                            InspectorIndex = -1;
                            strInspectorId = "";
                            strInspectorNm = "";
                        }
                    }

                    // Fn_LoadData() 속으로 이전
                    //// 조회 조건 화면 표시
                    //if (bIsSupervisor)  lblLookupCondition.Text = String.Format("Supervisor Lookup Conditions - [{0} / {1}] "
                    //                                                          , strInspectorNm + " ( " + strInspectorId + " )"
                    //                                                          , strLookupKind_SV
                    //                                                          );
                    //else                lblLookupCondition.Text = "";

                    //if (bUseDate)
                    //{
                    //    lblLookupCondition.Text += String.Format("Loou up kind : [{0}] Use Date : [{1}] Date : [{2} ~ {3}] Officer : [{4}]"
                    //                                           , strLookupKind, bUseDate ? "Y" : "N", strStart, strEnd, strOfficerNm + " ( " + strOfficerId + " )");
                    //}
                    //else
                    //{
                    //    lblLookupCondition.Text += String.Format("Loou up kind : [{0}] Use Date : [{1}] Officer : [{2}]"
                    //                                           , strLookupKind, bUseDate ? "Y" : "N", strOfficerNm + " ( " + strOfficerId + " )");
                    //}

                    Cursor oldCursor = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {

                        Fn_LoadData();      // Data 조회

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = oldCursor;

                    }


                }
            }
            catch (Exception ex)
            {
                String StrEx = ex.Message;
            }
            finally
            {
                frmLookup.Dispose();
            }

        }

        // Data 조회 
        private void Fn_LoadData()
        {
            // 화면 Clear
            Fn_ClearScreen();

            // 조회 조건 화면 표시
            if (bIsSupervisor) lblLookupCondition.Text = String.Format("Supervisor Lookup Conditions - [{0} / {1}] "
                                                                     , strInspectorNm + " ( " + strInspectorId + " )"
                                                                     , strLookupKind_SV
                                                                     );
            else lblLookupCondition.Text = "";

            if (bUseDate)
            {
                lblLookupCondition.Text += String.Format("Loou up kind : [{0}] Use Date : [{1}] Date : [{2} ~ {3}] Officer : [{4}]"
                                                       , strLookupKind, bUseDate ? "Y" : "N", strStart, strEnd, strOfficerNm + " ( " + strOfficerId + " )");
            }
            else
            {
                lblLookupCondition.Text += String.Format("Loou up kind : [{0}] Use Date : [{1}] Officer : [{2}]"
                                                       , strLookupKind, bUseDate ? "Y" : "N", strOfficerNm + " ( " + strOfficerId + " )");
            }


            // 조회에 사용할 변수 설정 
            String strCheckYN_SV = "";
            String strCheckYN = "";
            String strDateYN = "";
            String strUseOfficerID = "";
            String strUseInspectorID = "";

            // 검수 완료 여부 - Supervisor
            if (iLookupKind_SV == 11) strCheckYN_SV = "'N'";
            else if (iLookupKind_SV == 12) strCheckYN_SV = "'Y'";
            else if (iLookupKind_SV == 13) strCheckYN_SV = "'Y', 'N'";
            else strCheckYN_SV = "''";

            // 검수 완료 여부
            if (iLookupKind == 1) strCheckYN = "'N'";
            else if (iLookupKind == 2) strCheckYN = "'Y'";
            else if (iLookupKind == 3) strCheckYN = "'Y', 'N'";
            else strCheckYN = "''";

            // 기간조회 여부
            if (bUseDate) strDateYN = "Y";
            else strDateYN = "N";

            if (strOfficerId == "ALL") strUseOfficerID = "N";
            else strUseOfficerID = "Y";
            //strOfficerID = strOfficerId;

            if (strInspectorId == "ALL") strUseInspectorID = "N";
            else strUseInspectorID = "Y";

            //PgbPos.Minimum = 0;
            //PgbPos.Maximum = 0;
            //PgbPos.Value = 0;

            //BtnFirst.Enabled = false;
            //BtnBeforeTen.Enabled = false;
            //BtnBeforeOne.Enabled = false;
            //BtnNextOne.Enabled = false;
            //BtnNextTen.Enabled = false;
            //BtnLast.Enabled = false;

            //iLookup = 0;
            //iConfirmed = 0;
            //iReadNot = 0;
            //iRemain = 0;

            //iConfirmed_SV = 0;
            //iRejected_SV = 0;
            //iRemain_SV = 0;

            // Data 조회
            int rv = iTopsLib.Lib.GFn_SelectInspection(DsOffence
                                                      , strCheckYN_SV
                                                      , strCheckYN
                                                      , strDateYN
                                                      , strStart
                                                      , strEnd
                                                      , strUseOfficerID
                                                      , strOfficerId
                                                      , strUseInspectorID
                                                      , strInspectorId
                                                      );
            // 자료 없음
            if (rv == 0)
            {
                MessageBox.Show("No data available!!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (rv > 0)
            {


                PgbPos.Minimum = 0;
                PgbPos.Maximum = rv;
                PgbPos.Value = 1;

                //BtnFirst.Enabled = true;
                //BtnBeforeTen.Enabled = true;
                //BtnBeforeOne.Enabled = true;
                //BtnNextOne.Enabled = true;
                //BtnNextTen.Enabled = true;
                //BtnLast.Enabled = true;
                Fn_SetEnable(true);


            }

            if (DsOffence.Tables.Count > 0)
            {

                //DbGrid.SelectionChanged += null;
                DbGrid.SelectionChanged -= DbGrid_SelectionChanged;
                //chkVerification.CheckedChanged -= chkVerification_CheckedChanged;
                try
                {
                    DbGrid.DataSource = DsOffence.Tables[0];

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;
                }
                finally
                {

                    DbGrid.SelectionChanged += DbGrid_SelectionChanged;
                    //chkVerification.CheckedChanged += chkVerification_CheckedChanged;

                }


                DbGrid.Rows[0].Selected = true;
                DbGrid.Refresh();
                DbGrid_SelectionChanged(null, null);        // 

                Fn_ShowStatus();
            }
        }

        private int Fn_ShowStatus()
        {
            int rv = 0;
            iLookup = DbGrid.Rows.Count;
            iConfirmed = 0;
            iReadNot = 0;
            iRemain = 0;

            iConfirmed_SV = 0;
            iRejected_SV = 0;
            iRemain_SV = 0;

            if (DbGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DbGrid.Rows)
                {
                    if ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value) iConfirmed++;
                    else iRemain++;

                    // Supervisor
                    if (bIsSupervisor)
                    {
                        if ((int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value == 1) iConfirmed_SV++;
                        else if ((int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value == 2) iRejected_SV++;
                        else iRemain_SV++;

                    }
                }

            }
            lblLookup.Text = String.Format("{0}", iLookup);
            lblConfirmed.Text = String.Format("{0}", iConfirmed);
            lblReadNot.Text = String.Format("{0}", iReadNot);
            lblRemain.Text = String.Format("{0}", iRemain);

            // Supervisor
            if (bIsSupervisor)
            {

                lblConfirmed_SV.Text = String.Format("{0}", iConfirmed_SV);
                lblRejected_SV.Text = String.Format("{0}", iRejected_SV);
                lblRemain_SV.Text = String.Format("{0}", iRemain_SV);
            }

            return rv;
        }

        // 사용자 등급, ReadNot 선택에 따른 사이즈, 위치 변경
        private void Fn_ChangeCapturingPosition()
        {
            //// supervisor, inspector용 사이즈 조절 용 보관
            //if (iHCapturing == 0)
            //{

            //    iHReadNotEtc = txtReadNotEtc.Height;
            //    iHInspector = txtInspector.Height;
            //    iHSupervisor = txtSupervisor.Height;
            //    iHMargin = 5;
            //    iHCapturing = pnlCapturing.Height - iHReadNotEtc - iHSupervisor - iHMargin;

            //    // 초기 좌표
            //    iTVerification = chkVerification.Top - iHReadNotEtc;
            //    iTInspector = txtInspector.Top - iHReadNotEtc;

            //    iTPermission = chkPermission.Top - iHReadNotEtc;
            //    iTSupervisor = txtSupervisor.Top - iHReadNotEtc;
            //    iTReject = BtnReject.Top - iHReadNotEtc;

            //}

            //// 사이즈, 위치 조정
            ////bool bIsSupervisor = bIsSupervisor;    // Supervisor Level 여부 
            //bool bIsReadNotEtc = txtReadNotEtc.Visible;     // txtReadNotEtc Visible

            //int iSuper = Convert.ToInt32(bIsSupervisor) * iHSupervisor;
            //if (iSuper > 0) iSuper += iHMargin;

            //int iEtc = Convert.ToInt32(bIsReadNotEtc) * iHReadNotEtc;

            //pnlCapturing.Height = iHCapturing + iSuper + iEtc;

            //// 안움직이는게 나을듯 한데 ... 2019.06.26 주이사님 : 위치 고정으로 결정 
            //// 정신사납고 클릭할 컨트롤이 움직이니까 마우스를 움직여야 함...!!!
            ////pnlMoveCtrl.Top = pnlCapturing.Top + pnlCapturing.Height + iHMargin;
            ////picPlate.Top = pnlMoveCtrl.Top + pnlMoveCtrl.Height + iHMargin;

            //chkVerification.Top = iTVerification + iEtc;
            //txtInspector.Top = iTInspector + iEtc;

            //chkPermission.Top = iTPermission + iEtc;
            //txtSupervisor.Top = iTSupervisor + iEtc;
            //BtnReject.Top = iTReject + iEtc;

            //chkPermission.Visible = bIsSupervisor;
            //txtSupervisor.Visible = bIsSupervisor;
            //BtnReject.Visible = bIsSupervisor;

            ////// reject 사유 - super : textbox, inspector : Label
            ////txtRejectComm.Enabled = bIsSupervisor;

            //pnlSVStatus.Visible = bIsSupervisor;






            //chkPermission.Visible = bIsSupervisor;
            //txtSupervisor.Visible = bIsSupervisor;
            //BtnReject.Visible = bIsSupervisor;
            ////// reject 사유 - super : textbox, inspector : Label
            ////txtRejectComm.Enabled = bIsSupervisor;

            //pnlSVStatus.Visible = bIsSupervisor;

        }

        // Close
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();

        }

        // 변경여부 확인 - one row
        private bool Fn_IsChangedRow(DataGridViewRow row)
        {

            // 반려 사유 Clear 되지 않은게 있으면 ... 놓친거 같다.
            if ((int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value != 2)
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";

            // 변경 사항이 없으면 Skip
            if ((Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value)                        // 번호판
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.FINE].Value)                                 // Fine
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.FINE_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD].Value)                        // Make
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value)                        // Make
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD].Value)                         // Type
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value)                         // Type
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR].Value)                        // Color
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MODEL].Value)                        // Model
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MODEL_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_YEAR].Value)                         // Year
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_YEAR_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value)               // read not code
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC].Value)              // read not etc
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC_ORG].Value)) &&
                ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value
                == (bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value) &&
                ((int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value                              // Supervisor 작업현황
                == (int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD_ORG].Value) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value)               // Supervisor 반려사유 
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC_ORG].Value)) &&
                (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.COURT].Value)               // Supervisor 반려사유 
                == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.COURT_ORG].Value))
                ) return false;
            else return true;
        }

        // 변경여부 확인 - 전체
        private int Fn_IsChanged()
        {
            int iCnt = 0;
            foreach (DataGridViewRow row in DbGrid.Rows)
            {
                // 변경 사항이 없으면 Skip
                //if ((Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE_ORG].Value)) &&
                //    (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_ORG].Value)) &&
                //    (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_ORG].Value)) &&
                //    (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR_ORG].Value)) &&
                //    (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MODEL].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MODEL_ORG].Value)) &&
                //    (Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_YEAR].Value)
                //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_YEAR_ORG].Value)) &&
                //    ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value
                //    == (bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value)
                //    ) continue;
                if (!Fn_IsChangedRow(row)) continue;
                iCnt++;
            }

            return iCnt;
        }

        // 저장 
        private void BtnSave_Click(object sender, EventArgs e)
        {
            int iCnt = Fn_IsChanged();
            if (iCnt <= 0)
            {
                MessageBox.Show("No changes have been made.", "Information"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 작업 시작
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 준비 
                //bool bIsSupervisor = bIsSupervisor;
                String[] strFine = new string[iCnt];
                String[] strArrCourt = new String[iCnt];
                String[] strArrOffence_id = new String[iCnt];
                String[] strArrInspection_plate = new String[iCnt];

                String[] strArrInspection_make_cd = new String[iCnt];
                String[] strArrInspection_make = new String[iCnt];
                String[] strArrInspection_type_cd = new String[iCnt];
                String[] strArrInspection_type = new String[iCnt];
                String[] strArrInspection_color = new String[iCnt];

                String[] strArrInspection_rn_cd = new String[iCnt];
                String[] strArrInspection_rn_etc = new String[iCnt];
                String[] strArrInspection_end_yn = new String[iCnt];
                String[] strArrSupervisorPermit_cd = new String[iCnt];
                String[] strArrSupervisorRejectDesc = new String[iCnt];
                String[] strArrResult = new String[iCnt];

                int iPos = -1;
                foreach (DataGridViewRow row in DbGrid.Rows)
                {
                    // 변경 사항이 없으면 Skip
                    //if ((Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value)
                    //    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE_ORG].Value)) &&
                    //    ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value
                    //    == (bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value)
                    //    ) continue;
                    if (!Fn_IsChangedRow(row)) continue;

                    iPos++;

                    strFine[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.FINE].Value);
                    strArrCourt[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.COURT].Value);
                    strArrOffence_id[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.OFFENCE_ID].Value);
                    //strArrInspector_plate[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_PLATE].Value);
                    strArrInspection_plate[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value);

                    strArrInspection_make_cd[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD].Value);
                    strArrInspection_make[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value);
                    strArrInspection_type_cd[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD].Value);
                    strArrInspection_type[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value);
                    strArrInspection_color[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR].Value);

                    strArrInspection_rn_cd[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value);
                    strArrInspection_rn_etc[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC].Value);

                    strArrInspection_end_yn[iPos] = (bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value ? "1" : "0";

                    strArrSupervisorPermit_cd[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value);
                    strArrSupervisorRejectDesc[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value);
                }

                // 저장 
                int rv = iTopsLib.Lib.GFn_UpdateContraventionsByInspector( bIsSupervisor
                                                                         , strFine
                                                                         , strArrCourt
                                                                         , strArrOffence_id
                                                                         , strArrInspection_plate

                                                                         , strArrInspection_make_cd
                                                                         , strArrInspection_make
                                                                         , strArrInspection_type_cd
                                                                         , strArrInspection_type
                                                                         , strArrInspection_color

                                                                         , strArrInspection_rn_cd
                                                                         , strArrInspection_rn_etc
                                                                         , strArrInspection_end_yn
                                                                         , strArrSupervisorPermit_cd
                                                                         , strArrSupervisorRejectDesc
                                                                         , ref strArrResult
                                                                         );

                // 실패 내역이 있다.
                if (rv != iCnt)
                {
                    //
                    MessageBox.Show(String.Format("There is an error in the DB operation.\n"
                                                + "{0} Data Processing failed\n\n"
                                                + "Check your Network or DB Server!!!"
                                                , iCnt - rv)
                                                , "Error"
                                                , MessageBoxButtons.OK
                                                , MessageBoxIcon.Error);

                    return;

                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                return;
            }
            finally
            {
                this.Cursor = oldCursor;

            }

            MessageBox.Show("Save completed successfully.", "Information"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);



            //// 컨트롤 초기화
            //DbGrid.DataSource = null;
            //DsOffence.Clear();

            //if (picRegulation.Image != null) picRegulation.Image = null;
            //if (picPlate.Image != null) picPlate.Image = null;

            //PgbPos.Minimum = 0;
            //PgbPos.Maximum = 0;
            //PgbPos.Value = 0;

            //LblPosition.Text = "";
            //lblConfirmed.Text = "";
            //lblRemain.Text = "";
            //Fn_ClearScreen();

            Fn_LoadData();      // Data 다시 조회

        }

        // 화면 초기화
        private void Fn_ClearScreen()
        {
            // 컨트롤 초기화
            DbGrid.DataSource = null;
            DsOffence.Clear();

            if (img != null) img = null;
            if (picRegulation.Image != null) picRegulation.Image = null;
            if (picPlate.Image != null) picPlate.Image = null;

            chkShowDetail.Checked = false;
            pnlDetail.Visible = false;

            PgbPos.Minimum = 0;
            PgbPos.Maximum = 0;
            PgbPos.Value = 0;

            iLookup = 0;
            iConfirmed = 0;
            iReadNot = 0;
            iRemain = 0;

            iConfirmed_SV = 0;
            iRejected_SV = 0;
            iRemain_SV = 0;

            // 레코드 이동
            //BtnFirst.Enabled = false;
            //BtnBeforeTen.Enabled = false;
            //BtnBeforeOne.Enabled = false;
            //BtnNextOne.Enabled = false;
            //BtnNextTen.Enabled = false;
            //BtnLast.Enabled = false;

            LblPosition.Text = "";
            lblLookup.Text = "";
            lblConfirmed.Text = "";
            lblReadNot.Text = "";
            lblRemain.Text = "";

            txtRegulationDate.Text = "";
            //txtRegulationFine.Text = "";
            medtFine.Text = "";
            txtRegulationOffenceCd.Text = "";
            txtRegulationStatus.Text = "";

            //CbbCourt_Old.Text = "";
            CbbCourt.Text = "";
            txtLocation.Text = "";
            txtOfficer.Text = "";
            txtCamera.Text = "";

            txtRegNum.Text = "";
            //CbbMake_Old.Text = "";
            //CbbType_Old.Text = "";
            //CbbReadNot_Old.Text = "";
            CbbMake.Text = "";
            CbbType.Text = "";
            CbbReadNot.Text = "";
            txtReadNotEtc.Text = "";

            chkVerification.Checked = false;
            chkPermission.Checked = false;
            txtInspector.Text = "";
            txtSupervisor.Text = "";

            lblRejectCommTit.Visible = false;
            lblRejectCommDesc.Text = "";
            lblRejectCommDesc.Visible = false;

            // Supervisor
            lblConfirmed_SV.Text = "";
            lblRejected_SV.Text = "";
            lblRemain_SV.Text = "";

            txtCommentary.Text = "";

            // 수정 가능 필드 
            Fn_SetEnable(false);
        }

        // 수정 가능 필드 활성 / 비활성
        private void Fn_SetEnable(bool bValue)
        {
            medtFine.Enabled = bValue;
            CbbCourt.Enabled = bValue;
            txtRegNum.Enabled = bValue;
            CbbMake.Enabled = bValue;
            CbbType.Enabled = bValue;
            txtColor.Enabled = bValue;
            CbbReadNot.Enabled = bValue;
            txtReadNotEtc.Visible = bValue;

            // 레코드 이동 버튼
            BtnFirst.Enabled = bValue;
            BtnBeforeTen.Enabled = bValue;
            BtnBeforeOne.Enabled = bValue;
            BtnNextOne.Enabled = bValue;
            BtnNextTen.Enabled = bValue;
            BtnLast.Enabled = bValue;
            //pnlMoveCtrl.Enabled = bValue;

            chkVerification.Enabled = bValue;
            chkPermission.Enabled = bValue;


        }

        // 화면에 보여주기
        private void Fn_DisplayData(int index)
        {
            //------------------
            // 초기화
            //------------------
            // 
            //chkVerification.Checked = false;

            // 확대/ 축소에 사용할 img
            img = null;

            // 단속 이미지 Clear
            if (picRegulation.Image != null) picRegulation.Image.Dispose();
            picRegulation.Image = null;

            // Plate 이미지 Clear
            if (picPlate.Image != null) picPlate.Image.Dispose();
            picPlate.Image = null;



            //------------------
            // 이미지 보여주기
            //------------------
            // 범위를 벗어 나면 화면 지우고 나가기 
            if (index < 0 || index >= DbGrid.RowCount) return;


            // 파일명 만들기 
            String strSvtPath = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FILE_DIRECTORY].Value);
            String strFileName = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FILE_NAME].Value);
            String strPlateName = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FILE_PLATE].Value);
            String strSvrPath = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FILE_DIRECTORY].Value);

            // 파일이 내 PC에 있는지 확인
            bool bExist = false;
            if (strFileName.Length != 0)
            {
                bExist = File.Exists(".\\Temp\\" + strFileName);
                if (!bExist)
                {
                    iTopsLib.Lib.DownloadFile(strSvrPath + strFileName, ".\\Temp\\", strFileName);
                }
                // 잘 받았는지 다시 확인 후 
                bExist = File.Exists(".\\Temp\\" + strFileName);
                if (bExist)
                {
                    picRegulation.ImageLocation = ".\\Temp\\" + strFileName;
                    picRegulation.Load();

                }

            }

            bExist = false;
            if (strPlateName.Length != 0)
            {
                bExist = File.Exists(".\\Temp\\" + strPlateName);
                if (!bExist)
                {
                    iTopsLib.Lib.DownloadFile(strSvrPath + strPlateName, ".\\Temp\\", strPlateName);
                }
                // 잘 받았는지 다시 확인 후 
                bExist = File.Exists(".\\Temp\\" + strFileName);
                if (bExist)
                {
                    picPlate.ImageLocation = ".\\Temp\\" + strPlateName;
                    picPlate.Load();
                }

            }
            if (picPlate.Image == null)
            {
                picPlate.Height = 0;
            }

            // 이미지 확대 축소 관련 
            if (picRegulation.Image != null)
            {
                //picRegulation.SizeMode = PictureBoxSizeMode.StretchImage;
                if (img == null)
                    img = new Bitmap(picRegulation.Image);
                else
                    img = picRegulation.Image;
                //picRegulation.SizeMode = PictureBoxSizeMode.Normal;


                ratio = 1;
                this.movePoint = new Point(0, 0);
                //picRegulation.Invalidate();



                // 확대 된것 원복 시키면서 초기 위치에 그려주기 
                //ratio = 1;
                using (Graphics g = this.CreateGraphics())
                {
                    //// 수평에 맞추기
                    //ratio = ((float)picRegulation.Width / (float)img.Width) * (img.HorizontalResolution / g.DpiX);

                    // 수직에 맞추기
                    ratio = ((float)picRegulation.Height / (float)img.Height) * (img.VerticalResolution / g.DpiY);
                }

                ////속도 개선 
                //picRegulation.DoubleBuffered(true);
                //img.DoubleBuffered(true);

                picRegulation.Invalidate();

            }

            //------------------
            // 데이타 보여주기
            //------------------
            //bool bIsSupervisor = Lib.GFn_IsSupervisor();    // 사용자 등급

            txtSpeedReal.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.REAL_SPEED].Value);
            txtSpeedLimit.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.REGULATION_SPD_LIMIT].Value);
            //if (txtSpeedReal.Text != "" && txtSpeedLimit.Text != "")
            //{
            //    txtSpeedOver.Text = Convert.ToString(Convert.ToInt32(txtSpeedReal.Text) - Convert.ToInt32(txtSpeedLimit.Text));
            //}
            txtSpeedOver.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.OVER_SPEED].Value);

            txtRegulationDate.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.REGULATION_TIME].Value);
            //medtFine.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FINE].Value);
            if (DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FINE].Value == DBNull.Value)
                medtFine.Value = 0;
            else
                medtFine.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.FINE].Value);


            txtRegulationOffenceCd.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.OFFENCE_CODE].Value);

            ////CbbCourt_Old.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.COURT].Value);
            //// AllowNull = true 설정해도 정상, 오류를 불규칙적으로 발생
            //try
            //{
            //    String strCourt = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.COURT].Value);
            //    // 존재하는 코드인지 확인 
            //    if (Lib.GFn_GetCodeValue("COURT", strCourt, true) == null)
            //        CbbCourt.Value = "";
            //    else
            //        CbbCourt.Value = strCourt;
            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;
            //}
            CbbCourt.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.COURT].Value);


            txtLocation.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.LOCATION].Value);
            txtOfficer.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.OFFICER].Value);
            txtOfficerNm.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.OFFICER_NM].Value);
            txtCamera.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.DEVICE_MDL_NM].Value);

            //// Inspection한 정보가 있으면 Inspection 정보 우선 
            ////txtRegNum.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.LICENSE_PLATE_NO].Value);
            //String decipher_plate = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.DECIPHER_PLATE].Value);
            //String inspection_plate = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_PLATE].Value);
            //if (inspection_plate != "") txtRegNum.Text = inspection_plate;
            //else txtRegNum.Text = decipher_plate;
            txtRegNum.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value);

            //////cbxMake.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.DECIPHER_MAKER].Value);
            //////cbxType.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.DECIPHER_TYPE].Value);
            //////chkVerification.Checked = (bool)DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value;
            //////CbbMake_Old.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value);
            //////CbbType_Old.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value);
            ////// AllowNull = true 설정해도 정상, 오류를 불규칙적으로 발생
            ////try
            ////{
            ////    String strMake = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value);
            ////    // 존재하는 코드인지 확인 
            ////    if (Lib.GFn_GetVehicleMakeValue(strMake) == null)
            ////        CbbMake.Value = "";
            ////    else
            ////        CbbMake.Value = strMake;
            ////}
            ////catch (Exception ex)
            ////{
            ////    String strTmp = ex.Message;
            ////}
            //CbbMake.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value);
            if (Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD].Value) == String.Empty)
                CbbMake.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value);
            else
                CbbMake.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD].Value);


            ////try
            ////{
            ////    String strType = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value);
            ////    // 존재하는 코드인지 확인 
            ////    if (Lib.GFn_GetVehicleTypeValue(strType) == null)
            ////        CbbType.Value = "";
            ////    else
            ////        CbbType.Value = strType;
            ////}
            ////catch (Exception ex)
            ////{
            ////    String strTmp = ex.Message;
            ////}
            //CbbType.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value);
            if (Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD].Value) == String.Empty)
                CbbType.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value);
            else
                CbbType.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD].Value);



            txtColor.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.VEHICLE_COLOR].Value);

            // Read Not
            try
            {
                //String strReadNotCd = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value);
                //if (strReadNotCd == "")
                //    CbbReadNot.SelectedItem = null;
                //else
                //    CbbReadNot.SelectedValue = strReadNotCd;
                //CbbReadNot_Old.SelectedValue = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value);
                CbbReadNot.Value = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value);
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }

            // read not etc
            txtReadNotEtc.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC].Value);
            //if (CbbReadNot.Text == Lib.GFn_GetCodeValue("READ_NOT", "RNOT0009", true))    // Etc
            //{
            //    txtReadNotEtc.Visible = true;
            //}
            //else
            //{
            //    txtReadNotEtc.Visible = false;
            //}

            // Inspector Verification
            chkVerification.Checked = (bool)DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value;
            txtInspector.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_NM].Value);

            // Supervisor Permission
            chkPermission.Checked = (int)DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value == 1 ? true : false;
            txtSupervisor.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value);

            // reject 사유
            lblRejectCommTit.Visible = (int)DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value == 2 ? true : false;
            lblRejectCommDesc.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value);
            txtCommentary.Text = Convert.ToString(DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value);

            // Inspection 상황 보여 주기 
            //iChecked = 0;
            //iUnChecked = 0;
            //Fn_ShowStatus();
            //txtCheckedCnt.Text = String.Format("{0}", iChecked);
            //txtCheckedCnt.Text = String.Format("{0}", iUnChecked);

            // 레코드 상태에 따라 사용 가능 컨트롤 제어
            if (bIsSupervisor)     // Supervisor
            {
                chkPermission.Enabled = true;
                BtnReject.Enabled = true;
                //txtCommentary.Visible = lblRejectCommTit.Visible;
                Fn_ResizeRejectComm();

                if (!(bool)DbGrid.Rows[index].Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value)
                {
                    chkPermission.Enabled = false;
                }
            }
            else                            // Inspector
            {
                chkPermission.Enabled = false;
                BtnReject.Enabled = false;

            }
        }

        //===========================================================//
        // 데이타 선택 이동 관련
        //===========================================================//
        // 그리드에서 현재 선택된 RowIndex를 반환 
        private int Fn_GetSelectedIndex()
        {
            int rv = -1;
            if (DbGrid.RowCount <= 0) return rv;

            int iSelIndex = -1;
            foreach (DataGridViewRow row in DbGrid.SelectedRows)
            {
                iSelIndex = row.Index;
                if (iSelIndex >= 0) break;
            }


            if (iSelIndex >= 0) rv = iSelIndex;

            return rv;

        }

        // 그리드 상의 선택 위치 변경시 
        private void DbGrid_SelectionChanged(object sender, EventArgs e)
        {
            //int iSelIndex = -1;
            //foreach (DataGridViewRow row in DbGrid.SelectedRows)
            //{
            //    iSelIndex = row.Index;
            //    if (iSelIndex >= 0) break;
            //}

            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int iSelIndex = Fn_GetSelectedIndex();
                if (iSelIndex >= 0)
                {
                    PgbPos.Value = iSelIndex + 1;

                    Fn_ShowCurPosition();


                    // Grid Row가 변경되서 Data 뿌려줄때 이벤트 끊고 뿌려준다.
                    //chkVerification.CheckedChanged -= chkVerification_CheckedChanged;
                    //chkPermission.CheckedChanged -= chkPermission_CheckedChanged;
                    chkVerification.Click -= chkVerification_Click;
                    chkPermission.Click -= chkPermission_Click;
                    try
                    {
                        Fn_DisplayData(iSelIndex);
                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;
                    }
                    finally
                    {
                        //chkVerification.CheckedChanged += chkVerification_CheckedChanged;
                        //chkPermission.CheckedChanged += chkPermission_CheckedChanged;
                        chkVerification.Click += chkVerification_Click;
                        chkPermission.Click += chkPermission_Click;

                    }
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

        // 첫번째로 이동
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            if (DbGrid.RowCount <= 0) return;
            DbGrid.Rows[0].Selected = true;

        }

        // 마지막으로 이동
        private void BtnLast_Click(object sender, EventArgs e)
        {
            if (DbGrid.RowCount <= 0) return;
            DbGrid.Rows[DbGrid.RowCount - 1].Selected = true;

        }

        // 하나 앞으로 이동
        private void BtnBeforeOne_Click(object sender, EventArgs e)
        {
            //if (DbGrid.RowCount <= 0) return;

            //int iSelIndex = -1;
            //foreach (DataGridViewRow row in DbGrid.SelectedRows)
            //{
            //    iSelIndex = row.Index;
            //    if (iSelIndex >= 0) break;
            //}

            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex > 0)
            {
                DbGrid.Rows[iSelIndex - 1].Selected = true;
            }

        }

        // 하나뒤로 이동
        private void BtnNextOne_Click(object sender, EventArgs e)
        {
            //if (DbGrid.RowCount <= 0) return;

            //int iSelIndex = -1;
            //foreach (DataGridViewRow row in DbGrid.SelectedRows)
            //{
            //    iSelIndex = row.Index;
            //    if (iSelIndex >= 0) break;
            //}


            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < DbGrid.RowCount - 1)
            {
                DbGrid.Rows[iSelIndex + 1].Selected = true;

            }


        }
        // 10번 앞으로 이동
        private void BtnBeforeTen_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    BtnBeforeOne_Click(null, null);
            //}

            int iSelIndex = Fn_GetSelectedIndex() - 10;
            if (iSelIndex < 0) iSelIndex = 0;
            DbGrid.Rows[iSelIndex].Selected = true;

        }

        // 10번 뒤로 이동
        private void BtnNextTen_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    BtnNextOne_Click(null, null);
            //}

            int iSelIndex = Fn_GetSelectedIndex() + 10;
            if (iSelIndex >= DbGrid.RowCount) iSelIndex = DbGrid.RowCount - 1;
            DbGrid.Rows[iSelIndex].Selected = true;

        }

        // LblPosition 에 현재 위치 보여 주기( 작업중인 위치 / 총 대상 수 )
        private void Fn_ShowCurPosition()
        {
            LblPosition.Text = "";
            if (DbGrid.RowCount <= 0) return;

            int iTotal = DbGrid.RowCount;
            //int iSelIndex = -1;
            //foreach (DataGridViewRow row in DbGrid.SelectedRows)
            //{
            //    iSelIndex = row.Index;
            //    if (iSelIndex >= 0) break;
            //}
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < 0) return;

            LblPosition.Text = String.Format("{0} / {1}", iSelIndex + 1, iTotal);
        }

        //===========================================================//
        // 이미지 확대 축소 관련 
        //===========================================================//
        //Image Zoom(Image imgSrc, Size size)
        //{
        //    Bitmap bmp = new Bitmap(imgSrc, imgSrc.Width + (imgSrc.Width * size.Width / 100), imgSrc.Height + (imgSrc.Height * size.Height / 100));
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    return bmp;
        //}

        // Mouse Wheel 로 이미지 확대/축소 
        private void picRegulation1_MouseWheel(object sender, MouseEventArgs e)
        {
            //// 이미지가 할당되지 않았으면 그만
            //if (img == null) return;

            ////
            ////int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            ////if (lines > 0.0)
            ////{
            ////    ratio *= 1.0F + 0.2F * lines;
            ////    if (ratio > 100.0) ratio = 100.0;

            ////}
            ////else if (lines < 0.0)
            ////{
            ////    ratio *= 1.0F + 0.2F * lines;
            ////    if (ratio < 0.5)
            ////    {
            ////        ratio = 0.5;
            ////        this.movePoint = new Point(0, 0);
            ////        //picRegulation.Invalidate();
            ////    }
            ////}
            ////picRegulation.Image = Zoom(img, new Size((int)ratio, (int)ratio));

            //int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            //if (lines == 0) return;
            //for (int i=0; i<Math.Abs(lines);i++)
            //{
            //    if (lines > 0)
            //    {
            //        //ratio *= 1.0F + 0.2F * lines;
            //        ratio *= 1.0F + 0.1F;
            //        if (ratio > 100.0) ratio = 100.0;

            //    }
            //    else if (lines < 0)
            //    {
            //        //ratio *= 1.0F + 0.2F * lines;
            //        ratio *= 1.0F - 0.1F;
            //        if (ratio < 0.5)
            //        {
            //            ratio = 0.5;
            //            this.movePoint = new Point(0, 0);
            //            //picRegulation.Invalidate();
            //        }
            //    }
            //    picRegulation.Image = Zoom(img, new Size((int)ratio, (int)ratio));
            //    picRegulation.Refresh();
            //    //this.picRegulation.Invalidate();

            //}


            // 이미지가 할당되지 않았으면 그만
            if (img == null) return;
            if (e.Delta == 0) return;

            //for (int i = 0; i < Math.Abs(e.Delta) / 5; i++)
            for (int i = 0; i < Math.Abs(e.Delta); i++)
            {
                float oldratio = ratio;
                ratio += Math.Sign(e.Delta) * 0.00065F;  // 테스트 결과 0.00065이 가장 무난한 듯 ...

                // 최대 배율
                if (ratio > 5.0F) ratio = 5.0F;

                // 최소 배율
                if (ratio < 0.3F) ratio = 0.3F;

                MouseEventArgs mouse = e as MouseEventArgs;
                Point mousePosNow = mouse.Location;

                int x = mousePosNow.X - picRegulation.Location.X;
                int y = mousePosNow.Y - picRegulation.Location.Y;

                int oldimagex = (int)(x / oldratio);
                int oldimagey = (int)(y / oldratio);

                int newimagex = (int)(x / ratio);
                int newimagey = (int)(y / ratio);

                //imgx = newimagex - oldimagex + imgx;
                //imgy = newimagey - oldimagey + imgy;
                movePoint.X = newimagex - oldimagex + movePoint.X;
                movePoint.Y = newimagey - oldimagey + movePoint.Y;

                //pictureBox.Refresh();
                picRegulation.Invalidate();

            }

        }

        private void picRegulation_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (this.picRegulation.Image != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.ScaleTransform(ratio, ratio);
                //e.Graphics.DrawImage(img, imgx, imgy);
                e.Graphics.DrawImage(this.picRegulation.Image, movePoint);

            }

        }


        private void picRegulation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isClick = true;
                this.Cursor = Cursors.Cross;
                //this.startPoint = new Point(e.Location.X - movePoint.X, e.Location.Y - movePoint.Y);
                this.startPoint = new Point(e.Location.X, e.Location.Y);
                imgX = movePoint.X;
                imgY = movePoint.Y;
            }

        }

        private void picRegulation_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClick)
            {
                    //this.movePoint = new Point(e.Location.X  - startPoint.X, e.Location.Y - startPoint.Y);
                    int distX = e.Location.X - startPoint.X;
                    int distY = e.Location.Y - startPoint.Y;
                    this.movePoint = new Point((int)(imgX + (distX / ratio)), (int)(imgY + (distY / ratio)));
                    this.picRegulation.Invalidate();

            }

        }

        private void picRegulation_MouseUp(object sender, MouseEventArgs e)
        {
            if (isClick) isClick = false;
            this.Cursor = Cursors.Default;


        }

        // 이미지 원상 복구
        private void picRegulation_DoubleClick(object sender, EventArgs e)
        {
            if (picRegulation.Image == null) return;

            if (img == null)
                img = new Bitmap(picRegulation.Image);
            else
                img = picRegulation.Image;

            ratio = 1;
            this.movePoint = new Point(0, 0);
            //picRegulation.Invalidate();

            // 확대 된것 원복 시키면서 초기 위치에 그려주기 
            //ratio = 1;
            using (Graphics g = this.CreateGraphics())
            {
                //// 수평에 맞추기
                //ratio = ((float)picRegulation.Width / (float)img.Width) * (img.HorizontalResolution / g.DpiX);

                // 수직에 맞추기
                ratio = ((float)picRegulation.Height / (float)img.Height) * (img.VerticalResolution / g.DpiY);
            }

            //속도 개선 
            picRegulation.DoubleBuffered(true);
            img.DoubleBuffered(true);

            picRegulation.Invalidate();


        }


        // 수정 완료여부 
        private void chkVerification_CheckedChanged(object sender, EventArgs e)
        {
            ////int iSelIndex = -1;
            ////foreach (DataGridViewRow row in DbGrid.SelectedRows)
            ////{
            ////    iSelIndex = row.Index;
            ////    if (iSelIndex >= 0)
            ////    {
            ////        row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = chkVerification.Checked;
            ////        Fn_ShowStatus();
            ////        break;
            ////    }
            ////}
            //int iSelIndex = Fn_GetSelectedIndex();
            //DataGridViewRow row = DbGrid.Rows[iSelIndex];

            //// Supervisor가 
            //if (Lib.GFn_IsSupervisor())
            //{
            //    // Inspector의 검수 완료를 한 경우 
            //    if (chkVerification.Checked)
            //    {
            //        // Supervisor의 검수가 막혀 있는 경우 해제
            //        //chkPermission.Enabled = true;
            //        // 완료처리 
            //        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 1;
            //        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";

            //    }
            //    // Inspector의 검수 완료를 해제한 경우 - 기존에 Inspector 검수 완료된 것
            //    else if ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value)
            //    {
            //        // 반려할 것인지 확인 질문
            //        if (MessageBox.Show("Do you really want to reject?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            // 반려 처리 
            //            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 2;
            //            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected";

            //        }
            //        // 반려 취소
            //        else
            //        {
            //            row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value;

            //            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD_ORG].Value;
            //            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC_ORG].Value;

            //            // 다시 보여준다
            //            DbGrid_SelectionChanged(null, null);
            //            Fn_ShowStatus();

            //            return;
            //        }


            //    }
            //    // Inspector의 검수 완료를 해제한 경우 - 기존에 Inspector 검수 안한 것
            //    else
            //    {
            //        // Supervisor의 검수 막기
            //        //chkPermission.Checked = false;
            //        //chkPermission.Enabled = false;
            //        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 0;
            //        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";

            //    }
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = Lib.GetUserId();
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();

            //}
            //row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = chkVerification.Checked;

            //// 다시 보여준다
            //DbGrid_SelectionChanged(null, null);
            //Fn_ShowStatus();

        }

        private void txtRegNum_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtRegNum_KeyUp(object sender, KeyEventArgs e)
        {
            ////int iSelIndex = -1;
            ////foreach (DataGridViewRow row in DbGrid.SelectedRows)
            ////{
            ////    iSelIndex = row.Index;
            ////    if (iSelIndex >= 0)
            ////    {
            ////        row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_PLATE].Value = txtRegNum.Text;
            ////        Fn_Verification_Check(true);
            ////        break;
            ////    }
            ////}
            //int iSelIndex = Fn_GetSelectedIndex();
            //if (iSelIndex >= 0)
            //{
            //    DataGridViewRow row = DbGrid.Rows[iSelIndex];
            //    row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value = txtRegNum.Text;
            //    Fn_Verification_Check(true);
            //}

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 변경된 자료가 있는지 확인 후 닫기 
            int iCnt = Fn_IsChanged();
            if (iCnt > 0)
            {
                if (MessageBox.Show("Changed data exists!!\n" +
                        "Do you ignore the changes and exit?\n\n" +
                        "Yes : Close / No : Cancel Close", "Question"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        }

        // Verification Check - 수정시 자동 체크되게 하자!!!
        private void Fn_Verification_Check(bool check = true)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < 0) return;
            DataGridViewRow row = DbGrid.Rows[iSelIndex];

            if (Fn_IsChangedRow(row))
            {
                chkVerification.Checked = true;
                chkVerification_Click(null, null);
            }
        }

        // 자동차 메이커 변경
        private void cbxMake_TextChanged(object sender, EventArgs e)
        {
            //Fn_Verification_Check(true);

        }

        // 자동차 Type 변경
        private void cbxType_TextChanged(object sender, EventArgs e)
        {
            //Fn_Verification_Check(true);

        }

        private void chkPermission_CheckedChanged(object sender, EventArgs e)
        {
            //int iSelIndex = Fn_GetSelectedIndex();
            //DataGridViewRow row = DbGrid.Rows[iSelIndex];

            ////// Supervisor가 Inspector의 검수 완료를 해제한 경우 
            ////if (Lib.GFn_IsSupervisor() && !chkVerification.Checked)
            ////{
            ////    // 반려할 것인지 확인 질문
            ////    if (MessageBox.Show("Do you really want to reject?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
            ////    {
            ////        chkVerification.Checked = true;
            ////        return;
            ////    }

            ////    // 반려 처리 
            ////    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = "2";
            ////    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = "2";
            ////    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = "2";
            ////    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected";

            ////}

            //row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = !chkPermission.Checked ? 0 : (chkVerification.Checked ? 1 : 2);

            //// reject
            //if (chkPermission.Checked && !chkVerification.Checked)  
            //{
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = Lib.GetUserId();
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected"; 

            //}
            //else // 반려가 아니면 ... 반려 사유 삭제 
            //{
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";
            //}

            //// 다시 보여준다
            //DbGrid_SelectionChanged(null, null);
            //Fn_ShowStatus();


        }

        // Reject
        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (DbGrid.RowCount <= 0) return;

            FrmReject fReject = new FrmReject();
            try
            {

                if (fReject.ShowDialog() == DialogResult.OK)
                {
                    int iKind = -1;
                    String strID = "";
                    String strNM = "";
                    String strComment = "";

                    // 선택 값 가져 오기
                    if (!fReject.GFn_GetCondition(ref iKind
                                                , ref strID
                                                , ref strNM
                                                , ref strComment
                                                 ))
                    {
                        MessageBox.Show(strComment, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Reject 처리 전 마지막 확인 메시지
                    String strMsg = "";
                    if (iKind == 0)
                        strMsg = String.Format("Do you really want to reject {0}'s inspections?", strNM);
                    else
                        strMsg = String.Format("Do you really want to reject all inspections?");

                    if (MessageBox.Show(strMsg
                                      , "Question"
                                      , MessageBoxButtons.YesNoCancel
                                      , MessageBoxIcon.Question
                                       ) != DialogResult.Yes) return;

                    // 반려 처리
                    int iCnt = 0;
                    foreach (DataGridViewRow row in DbGrid.Rows)
                    {
                        // Inspector가 검사 안한 것 제외
                        if (!(bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value) continue;

                        // supervisor가 완료 한 것 제외
                        if ((int)row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD_ORG].Value != 0) continue;
                       
                        // 선택한 Inspector 아닌 것 제외
                        if (iKind == 0 && Convert.ToString(row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_ID].Value) != strID) continue;

                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = Lib.GetUserId();
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 2;
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = strComment;
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();
                        row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = false;

                        iCnt++;
                    }

                    MessageBox.Show(String.Format("Rejected {0} Inspections!!!", iCnt));

                    // 현재 화면의 data 다시 보여주기
                    DbGrid_SelectionChanged(null, null);
                    Fn_ShowStatus();

                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {
                fReject.Dispose();
            }

        }

        // Rejected 
        private void lblRejectCommTit_MouseEnter(object sender, EventArgs e)
        {
            Fn_ShowRejectDesc(true);
        }

        // Rejected 
        private void lblRejectCommTit_MouseMove(object sender, MouseEventArgs e)
        {
            Fn_ShowRejectDesc(true);

        }

        // Rejected 
        private void lblRejectCommTit_DragLeave(object sender, EventArgs e)
        {
        }

        private void lblRejectCommTit_MouseLeave(object sender, EventArgs e)
        {
            Fn_ShowRejectDesc(false);

        }

        // Rejected 
        private void Fn_ShowRejectDesc(bool bShow)
        {
            int iRowIndex = Fn_GetSelectedIndex();
            if (iRowIndex < 0) return;
            bool bRejected = (int)DbGrid.Rows[iRowIndex].Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value == 2;
            lblRejectCommDesc.Visible = bShow;
        }


        // Form 사이즈에 맞게 위치, 사이즈 조정 
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            Fn_ResizeRejectComm();
        }

        // Reject comm 사이즈 조정
        private void Fn_ResizeRejectComm()
        {
            //if (!Lib.GFn_IsSupervisor()) return;
            if (!bIsSupervisor) return;

            // Supervisor reject 사유
            if (pnlMainBtn.Left < pnlLeft.Width + lblRejectCommTit.Left)    txtCommentary.Visible = false;
            else                                                            txtCommentary.Visible = lblRejectCommTit.Visible;

            if (!txtCommentary.Visible) return;

            txtCommentary.Width = this.Width - pnlLeft.Width - txtCommentary.Left - 34;
        }

        private void chkVerification_Click(object sender, EventArgs e)
        {
            //int iSelIndex = -1;
            //foreach (DataGridViewRow row in DbGrid.SelectedRows)
            //{
            //    iSelIndex = row.Index;
            //    if (iSelIndex >= 0)
            //    {
            //        row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = chkVerification.Checked;
            //        Fn_ShowStatus();
            //        break;
            //    }
            //}
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < 0) return;
            DataGridViewRow row = DbGrid.Rows[iSelIndex];

            // Supervisor가 
            //if (Lib.GFn_IsSupervisor())
            if (bIsSupervisor)
            {
                // Inspector의 검수 완료를 한 경우 
                if (chkVerification.Checked)
                {
                    // Supervisor의 검수가 막혀 있는 경우 해제
                    //chkPermission.Enabled = true;
                    // 완료처리 
                    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 1;
                    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";

                }
                // Inspector의 검수 완료를 해제한 경우 - 기존에 Inspector 검수 완료된 것
                else if ((bool)row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value)
                {
                    // 반려할 것인지 확인 질문
                    if (MessageBox.Show("Do you really want to reject?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // 반려 처리 
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 2;
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected";

                    }
                    // 반려 취소
                    else
                    {
                        row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN_ORG].Value;

                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD_ORG].Value;
                        row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC_ORG].Value;

                        // 다시 보여준다
                        DbGrid_SelectionChanged(null, null);
                        Fn_ShowStatus();

                        return;
                    }


                }
                // Inspector의 검수 완료를 해제한 경우 - 기존에 Inspector 검수 안한 것
                else
                {
                    // Supervisor의 검수 막기
                    //chkPermission.Checked = false;
                    //chkPermission.Enabled = false;
                    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = 0;
                    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";

                }
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = Lib.GetUserId();
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();

            }
            row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_END_YN].Value = chkVerification.Checked;

            // 다시 보여준다
            DbGrid_SelectionChanged(null, null);
            Fn_ShowStatus();

        }

        private void chkPermission_Click(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < 0) return;
            DataGridViewRow row = DbGrid.Rows[iSelIndex];

            //// Supervisor가 Inspector의 검수 완료를 해제한 경우 
            //if (Lib.GFn_IsSupervisor() && !chkVerification.Checked)
            //{
            //    // 반려할 것인지 확인 질문
            //    if (MessageBox.Show("Do you really want to reject?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
            //    {
            //        chkVerification.Checked = true;
            //        return;
            //    }

            //    // 반려 처리 
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = "2";
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = "2";
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = "2";
            //    row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected";

            //}

            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_PERMIT_CD].Value = !chkPermission.Checked ? 0 : (chkVerification.Checked ? 1 : 2);

            // reject
            if (chkPermission.Checked && !chkVerification.Checked)
            {
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_ID].Value = Lib.GetUserId();
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_NM].Value = Lib.GetUserName();
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "Supervisor rejected";

            }
            else // 반려가 아니면 ... 반려 사유 삭제 
            {
                row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = "";
            }

            // 다시 보여준다
            DbGrid_SelectionChanged(null, null);
            Fn_ShowStatus();


        }

        // reject 사유를 수정한것 반영
        private void txtCommentary_TextChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex < 0) return;
            DataGridViewRow row = DbGrid.Rows[iSelIndex];

            lblRejectCommDesc.Text = txtCommentary.Text;
            row.Cells[(int)iTopsLib.EnGridInspection.SUPERVISOR_REJECT_DESC].Value = txtCommentary.Text;
        }

        // Read Not 변경 
        private void cbxReadNot_TextChanged(object sender, EventArgs e)
        {

        }

        private void Fn_SetCodeCombo()
        {
            // 각종 코드

            // Offence Code ... 2019.12.18
            //try
            //{
            //    int rv = iTopsLib.Lib.GFn_GetVehicleMake(ref dsOffenceCd, "");
            //    if (rv < 0) return;
            //    if (rv > 0)
            //    {
            //        CbbOffenceCd.DataSource = dsOffenceCd.Tables[0];

            //        CbbOffenceCd.DisplayMember = "display_value";
            //        //CbbMake.DisplayMember = "vhcl_make_nm";
            //        CbbOffenceCd.ValueMember = "vhcl_make_cd";

            //        CbbOffenceCd.Value = null;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;
            //}
            //finally
            //{

            //}

            // Location Code ... 2019.12.18
            try
            {
                //String sCourt = CbbCourt.Value.ToString();
                int rv = iTopsLib.Lib.GFn_GetLocationCodeDS(ref dsLocationCd, true, "");
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbLocationCd.DataSource = dsLocationCd.Tables[0];

                    CbbLocationCd.DisplayMember = "display_value";
                    CbbLocationCd.ValueMember = "cd";

                    CbbLocationCd.Value = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }

            // Officer Code ... 2019.12.18
            try
            {
                int rv = iTopsLib.Lib.GFn_SelectOfficer(dsOfficerCd);
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbOfficerCd.DataSource = dsOfficerCd.Tables[0];

                    CbbOfficerCd.DisplayMember = "user_nm";
                    CbbOfficerCd.ValueMember = "user_id";

                    CbbOfficerCd.Value = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }



            // Court
            try
            {
                int rv = iTopsLib.Lib.GFn_GetCodeDS("COURT", "", ref dsCourt, true, false);
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbCourt.DataSource = dsCourt.Tables[0];

                    //CbbCourt.DisplayMember = "cd_nm";
                    CbbCourt.DisplayMember = "display_value";
                    CbbCourt.ValueMember = "cd";

                    CbbCourt.Text = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }

            // Make
            try
            {
                int rv = iTopsLib.Lib.GFn_GetVehicleMake(ref dsMake, "");
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbMake.DataSource = dsMake.Tables[0];

                    CbbMake.DisplayMember = "display_value";
                    //CbbMake.DisplayMember = "vhcl_make_nm";
                    CbbMake.ValueMember = "vhcl_make_cd";

                    CbbMake.Value = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }

            // Type
            try
            {
                int rv = iTopsLib.Lib.GFn_GetVehicleType(ref dsType, "");
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbType.DataSource = dsType.Tables[0];

                    CbbType.DisplayMember = "display_value";
                    //CbbType.DisplayMember = "vhcl_type_nm";
                    CbbType.ValueMember = "vhcl_type_cd";

                    CbbType.Value = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }


            // Read Not 코드
            try
            {
                int rv = iTopsLib.Lib.GFn_GetCodeDS("READ_NOT", "", ref dsReadNot, true, true);
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbReadNot.DataSource = dsReadNot.Tables[0];

                    CbbReadNot.DisplayMember = "display_value";
                    //CbbReadNot.DisplayMember = "cd_nm";
                    CbbReadNot.ValueMember = "cd";

                    CbbReadNot.Value = null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }

        }

        //private void CbbReadNot_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //// 선택된 내용에 맞게 기타 사유 입력 항목 보여주고 숨기기 
            //if (CbbReadNot_Old.Items.Count <= 0)
            //{
            //    txtReadNotEtc.Visible = false;
            //    return;
            //}
            //else if (CbbReadNot_Old.Text == Lib.GFn_GetCodeValue("READ_NOT", "RNOT0009", true))    // Etc
            //{
            //    txtReadNotEtc.Visible = true;
            //    txtReadNotEtc.Focus();
            //}
            //else
            //{
            //    txtReadNotEtc.Visible = false;
            //}
            ////txtReadNotEtc.Text = CbbReadNot.SelectedValue.ToString();

            //// 내용에 맞게 사이즈, 위치 변경 
            //Fn_ChangeCapturingPosition();

            //// 
            //int iSelIndex = Fn_GetSelectedIndex();
            //if (iSelIndex >= 0)
            //{
            //    DataGridViewRow row = DbGrid.Rows[iSelIndex];
            //    row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value = CbbReadNot_Old.SelectedValue.ToString();
            //    row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_NM].Value = CbbReadNot_Old.Text;
            //}


        //}

        private void txtReadNotEtc_KeyUp(object sender, KeyEventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC].Value = txtReadNotEtc.Text;
                //Fn_Verification_Check(true);
            }


        }


        // 코드 콤보 박스 모양 초기화
        private void CbbCourt_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //e.Layout.Bands[0].Columns[0].Hidden = false;
            //e.Layout.Bands[0].Columns[1].Hidden = false;
            //e.Layout.Bands[0].Columns[2].Hidden = true;

            //e.Layout.Bands[0].Columns[0].Width = 130;
            //e.Layout.Bands[0].Columns[1].Width = 200;
            // 일단 모두 숨긴다. - 나중에 컬럼이 추가 되어도 관련된 것만 수정하기 위함
            for (int i = 0; i < e.Layout.Bands[0].Columns.Count; i++)
            {
                e.Layout.Bands[0].Columns[i].Hidden = true;
            }

            // 필요한 거만 보여준다.
            e.Layout.Bands[0].Columns[0].Hidden = false;
            e.Layout.Bands[0].Columns[1].Hidden = false;

            e.Layout.Bands[0].Columns[0].Width = 130;
            e.Layout.Bands[0].Columns[1].Width = 200;

        }

        // 코드 콤보박스 선택  입력 값이 코드에 없는 경우
        private void CbbCourt_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraCombo combo = sender as Infragistics.Win.UltraWinGrid.UltraCombo;

            String strCbbName = combo.Name;
            String strName = strCbbName.Substring(3, strCbbName.Length - 3).ToUpper();

            if (DbGrid.RowCount > 0)
            {
                MessageBox.Show("Please enter the exact details of " + strName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                combo.Value = null;
                combo.Focus();
            }


        }

        // 수정하기 전에 조회된 상태 인지 확인 
        private void CbbCourt_Click(object sender, EventArgs e)
        {
            if (DbGrid.RowCount <= 0)
            {
                MessageBox.Show("Please work after inquiry.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLookup.Focus();
            }

        }

        // Court Input Validation
        private void CbbCourt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {

                String strInput = CbbCourt.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsCourt.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsCourt.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
                        if (row.Length == 0)
                        {
                            e.KeyChar = (char)0;    // 입력 취소
                            Lib.Beep(1000, 20);     // Beep
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;

                    e.KeyChar = (char)0;    // 입력 취소
                    Lib.Beep(1000, 20);     // Beep
                }
            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }

        }

        // Make Input Validation
        private void CbbMake_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {

                String strInput = CbbMake.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsMake.Tables[0].Select(string.Format("vhcl_make_cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsMake.Tables[0].Select(string.Format("vhcl_make_nm like '*{0}*'", strInput));
                        if (row.Length == 0)
                        {
                            e.KeyChar = (char)0;    // 입력 취소
                            Lib.Beep(1000, 20);     // Beep
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;

                    e.KeyChar = (char)0;    // 입력 취소
                    Lib.Beep(1000, 20);     // Beep
                }

            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }

        }

        // Type Input Validation
        private void CbbType_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {

                String strInput = CbbType.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsType.Tables[0].Select(string.Format("vhcl_type_cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsType.Tables[0].Select(string.Format("vhcl_type_nm like '*{0}*'", strInput));
                        if (row.Length == 0)
                        {
                            e.KeyChar = (char)0;    // 입력 취소
                            Lib.Beep(1000, 20);     // Beep
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;

                    e.KeyChar = (char)0;    // 입력 취소
                    Lib.Beep(1000, 20);     // Beep
                }
            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }

        }

        // Read Not Input Validation
        private void CbbReadNot_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                if (txtReadNotEtc.Visible) txtReadNotEtc.Focus();
                else chkVerification.Focus();
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {

                String strInput = CbbReadNot.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsReadNot.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsReadNot.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
                        if (row.Length == 0)
                        {
                            e.KeyChar = (char)0;    // 입력 취소
                            Lib.Beep(1000, 20);     // Beep
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;

                    e.KeyChar = (char)0;    // 입력 취소
                    Lib.Beep(1000, 20);     // Beep
                }

            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }


        }

        private void CbbReadNot_ValueChanged(object sender, EventArgs e)
        {
            // 선택된 내용에 맞게 기타 사유 입력 항목 보여주고 숨기기 
            //if (CbbMake.Rows.Count <= 0)
            if (DbGrid.RowCount <= 0)   // 조회된 것이 없으면 
            {
                txtReadNotEtc.Visible = false;
                return;
            }
            ////else if (CbbReadNot_Old.Text == Lib.GFn_GetCodeValue("READ_NOT", "RNOT0009", true))    // Etc
            //else if (Convert.ToString(CbbReadNot.Value) == "RNOT0009")    // Etc
            //{
            //    txtReadNotEtc.Visible = true;
            //    txtReadNotEtc.Focus();
            //}
            //else if (Convert.ToString(CbbReadNot.Value) == "111")    // Other
            //{
            //    txtReadNotEtc.Visible = true;
            //    txtReadNotEtc.Focus();
            //}
            //else if (Convert.ToBoolean(CbbReadNot.ActiveRow.Cells[3].Value))    // code.cd_t_ext1 : read_not_etc 사용 여부 
            else if (CbbReadNot.ActiveRow == null)
            {
                txtReadNotEtc.Visible = false;
            }
            else if (Convert.ToBoolean(CbbReadNot.ActiveRow.Cells["cd_t_ext1"].Value))    // code.cd_t_ext1 : read_not_etc 사용 여부 
            {
                txtReadNotEtc.Visible = true;
                txtReadNotEtc.Focus();
            }
            else
            {
                txtReadNotEtc.Visible = false;
                //chkVerification.Focus();
            }
            //txtReadNotEtc.Text = CbbReadNot.SelectedValue.ToString();
            
            //// 내용에 맞게 사이즈, 위치 변경 
            //Fn_ChangeCapturingPosition();

            // 
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_CD].Value = Convert.ToString(CbbReadNot.Value);
                row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_NM].Value = CbbReadNot.Text;
            }
        }

        private void CbbCourt_ValueChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.COURT].Value = Convert.ToString(CbbCourt.Value);
            }

        }

        private void CbbMake_ValueChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER_CD].Value = Convert.ToString(CbbMake.Value);
                //row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value = Convert.ToString(CbbMake.Value);
                if (CbbMake.SelectedRow == null)
                    row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value = String.Empty;
                else
                    row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_MAKER].Value = Convert.ToString(CbbMake.SelectedRow.Cells["vhcl_make_nm"].Value as String);
            }

        }

        private void CbbType_ValueChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                //row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value = Convert.ToString(CbbType.Value);
                row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE_CD].Value = Convert.ToString(CbbType.Value);
                if (CbbType.SelectedRow == null)
                    row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value = String.Empty;
                else
                    row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_TYPE].Value = Convert.ToString(CbbType.SelectedRow.Cells["vhcl_type_nm"].Value as String);

            }

        }

        private void CbbCourt_Enter(object sender, EventArgs e)
        {

            Infragistics.Win.UltraWinGrid.UltraCombo combo = sender as Infragistics.Win.UltraWinGrid.UltraCombo;

            combo.SelectAll();
        }

        private void txtRegNum_ValueChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.VEHICLE_PLATE].Value = txtRegNum.Text;
                Fn_Verification_Check(true);
            }
        }

        private void txtRegNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }

        }

        //
        private void txtReadNotEtc_ValueChanged(object sender, EventArgs e)
        {
            int iSelIndex = Fn_GetSelectedIndex();
            if (iSelIndex >= 0)
            {
                DataGridViewRow row = DbGrid.Rows[iSelIndex];
                row.Cells[(int)iTopsLib.EnGridInspection.INSPECTION_READ_NOT_ETC].Value = txtReadNotEtc.Text;
            }
        }

        //// 상단 Detail 의 항목을 편집하면 Grid에 반영하기 
        //private void DetailItem_ValueChanged(object sender, EventArgs e)
        //{
            //    //if (DBGrid.Rows.Count <= 0) return;
            //    //if (DBGrid.ActiveRow == null) return;

            //    try
            //    {
            //        if (sender is Infragistics.Win.UltraWinEditors.UltraTextEditor)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinEditors.UltraTextEditor);
            //            if (obj.Value == null)
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
            //            else
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);
            //        }
            //        else if (sender is Infragistics.Win.UltraWinGrid.UltraCombo)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinGrid.UltraCombo);
            //            DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);

            //            // 그리드에 코드값 보여 주기
            //            DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag) + 1].Value = Convert.ToString(obj.SelectedRow.Cells[1].Value as String);

            //            // 코드변경에 따라 영향 있는 것들 반영하기
            //            if (obj.Name == "CbbReference")
            //            {
            //                String strCode = Convert.ToString(obj.SelectedRow.Cells["cd_desc"].Value as String);
            //                if (strCode != "")
            //                {
            //                    // Reference 에 해당하는 Legislation 리스트 만들기
            //                    Fn_SetOnlyLegislationWithDB(strCode);

            //                    if (CbbLegislation.Rows.Count > 0)
            //                    {
            //                        CbbLegislation.SelectedRow = CbbLegislation.Rows[0];
            //                        nedtSpeedFrom.Value = nedtSpeedFrom.MinValue;
            //                        nedtSpeedTo.Value = nedtSpeedTo.MaxValue;
            //                    }
            //                    else
            //                    {
            //                        txtSpeedLimit.Value = "";
            //                        txtMinPenaltySpeed.Value = "";
            //                        txtMaxPenaltySpeed.Value = "";
            //                        nedtSpeedFrom.MinValue = 0;
            //                        nedtSpeedFrom.MaxValue = 0;
            //                        nedtSpeedFrom.Value = null;

            //                        nedtSpeedTo.MinValue = 0;
            //                        nedtSpeedTo.MaxValue = 0;
            //                        nedtSpeedTo.Value = null;
            //                    }
            //                }
            //            }
            //            else if (obj.Name == "CbbLegislation")
            //            {
            //                txtSpeedLimit.Value = Convert.ToString(obj.SelectedRow.Cells["cd_desc"].Value as String);
            //                txtMinPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext1"].Value as String);
            //                txtMaxPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext2"].Value as String);

            //                if (txtMinPenaltySpeed.Text == "")
            //                {
            //                    nedtSpeedFrom.MinValue = 0;
            //                    nedtSpeedTo.MinValue = 0;
            //                }
            //                else
            //                {
            //                    nedtSpeedFrom.MinValue = Convert.ToInt32(txtMinPenaltySpeed.Text);
            //                    nedtSpeedTo.MinValue = Convert.ToInt32(txtMinPenaltySpeed.Text);
            //                }

            //                if (txtMaxPenaltySpeed.Text == "")
            //                {
            //                    nedtSpeedFrom.MaxValue = 0;
            //                    nedtSpeedTo.MaxValue = 0;
            //                }
            //                else
            //                {
            //                    nedtSpeedFrom.MaxValue = Convert.ToInt32(txtMaxPenaltySpeed.Text) + 1;
            //                    nedtSpeedTo.MaxValue = Convert.ToInt32(txtMaxPenaltySpeed.Text);
            //                }
            //                nedtSpeedFrom.Value = nedtSpeedFrom.MinValue;
            //                nedtSpeedTo.Value = nedtSpeedTo.MaxValue;
            //            }



            //        }
            //        else if (sender is Infragistics.Win.UltraWinEditors.UltraCheckEditor)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCheckEditor);
            //            DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = obj.Checked;
            //        }
            //        else if (sender is Infragistics.Win.UltraWinEditors.UltraNumericEditor)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinEditors.UltraNumericEditor);
            //            if (obj.Value.ToString() == "")
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
            //            else
            //            {

            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
            //            }

            //        }
            //        else if (sender is Infragistics.Win.UltraWinEditors.UltraCurrencyEditor)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCurrencyEditor);
            //            if (obj.Value == 0)
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = "NO AG";
            //            else
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
            //        }
            //        else if (sender is Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit)
            //        {
            //            var obj = (sender as Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit);
            //            if (obj.Value.ToString() == "")
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
            //            else
            //                DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);

            //        }

            //        // record state 변경 ... 삭제된 것은 ???
            //        if (Convert.ToInt32(DBGrid.ActiveRow.Cells["rec_state"].Value) != (int)iTopsLib.RecState.NEW)
            //        {
            //            DBGrid.ActiveRow.Cells["rec_state"].Value = iTopsLib.RecState.UPDATED;
            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        String strTmp = ex.Message;

            //    }
            //    finally
            //    {
            //        //
            //    }


        //}

        private void medtFine_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r') // enter
                {
                    this.SelectNextControl(sender as Control, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {
                //
            }

        }

        private void medtFine_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iSelIndex = Fn_GetSelectedIndex();
                if (iSelIndex >= 0)
                {
                    DataGridViewRow row = DbGrid.Rows[iSelIndex];
                    row.Cells[(int)iTopsLib.EnGridInspection.FINE].Value = medtFine.Value;
                    Fn_Verification_Check(true);
                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                //
            }

        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            if (this.Width < 1900)
                pnlLeft.Width = 420;
            else
                pnlLeft.Width = 400;

            if (this.Height <= 768)
                pnlImageTop.Height = 80;
            else
                pnlImageTop.Height = 60;

        }

        private void chkShowDetail_CheckedChanged(object sender, EventArgs e)
        {
            pnlDetail.Visible = chkShowDetail.Checked;
        }


        // 상세 판넬 마우스로 끌고 다니기 - 위치 이동
        private Boolean bMoving;
        private int iStX, iStY;

        private void pnlDetail_MouseDown(object sender, MouseEventArgs e)
        {
            //
            bMoving = true;
            iStX = e.X;
            iStY = e.Y;
        }

        private void pnlDetail_MouseUp(object sender, MouseEventArgs e)
        {
            //
            bMoving = false;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {

        }

        // Offence Code 상세 정보 보여주기
        private void lblOffenceCd_MouseEnter(object sender, EventArgs e)
        {
//            pnlOffenceDesc.Visible = true;

        }

        // Offence Code 상세 정보 숨기기
        private void lblOffenceCd_MouseLeave(object sender, EventArgs e)
        {
//            pnlOffenceDesc.Visible = false;

        }

        // Location Code 상세 정보 보여주기
        private void lblLocationCd_MouseEnter(object sender, EventArgs e)
        {
//            pnlLocationCd.Visible = true;
        }

        // Location Code 상세 정보 숨기기
        private void lblLocationCd_MouseLeave(object sender, EventArgs e)
        {
//            pnlLocationCd.Visible = false;
        }

        // Remapping Fine
        private void btnReMap_Click(object sender, EventArgs e)
        {
            // Fine
            try
            {
                // 범칙금 재계산 할 것인지 마지막으로 확인
                if (MessageBox.Show("Do you Remapping to fine?\n\n" +
                        "Yes : Remapping / No : Cancel", "Question"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }


                String strCourtCd = CbbCourt.Value.ToString();
                String strLocationCd = txtLocation.Text;
                String strLimitCd = txtSpeedLimit.Text;
                String strRealSpd = txtSpeedReal.Text;

                int rv = iTopsLib.Lib.GFn_GetReMappingFine(ref dsFine, strCourtCd, strLocationCd, strLimitCd, strRealSpd);
                if (rv > 0)
                {
                    if (MessageBox.Show(String.Format("Do you Change fine ? [ {0} ---> {1} ]" +
                                                      "Yes : Remapping / No : Cancel"
                                                    , medtFine.Text, dsFine.Tables[0].Rows[0].ItemArray[1].ToString())
                                      , "Question"
                                      , MessageBoxButtons.YesNo, MessageBoxIcon.Question
                                      ) == DialogResult.No)
                    {
                        return;
                    }

                    medtFine.Value = dsFine.Tables[0].Rows[0].ItemArray[1].ToString();

                }
                else
                {
                    MessageBox.Show("Can't Remapping!!!");
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

            }


        }

        private void pnlDetail_MouseMove(object sender, MouseEventArgs e)
        {
            //
            if (bMoving)
            {
                int iMoveX = e.X - iStX;
                int iMoveY = e.Y - iStY;

                pnlDetail.Location = new System.Drawing.Point(pnlDetail.Location.X + iMoveX, pnlDetail.Location.Y + iMoveY);
            }
        }
    } // end class


    // DataGridView 속도개선 - Dublebuffer
    public static class ExtensionMethods
    {
        //    public static void DoubleBuffered(this DataGridView dbGridView, bool setting)
        //    {
        //        Type dbGridViewType = dbGridView.GetType();
        //        PropertyInfo pi = dbGridViewType.GetProperty("DoubleBuffered",
        //            BindingFlags.Instance | BindingFlags.NonPublic);
        //        pi.SetValue(dbGridView, setting, null);
        //    }

        // PictureBox DubleBuffered 속성 지정 
        public static void DoubleBuffered(this PictureBox pic, bool setting)
        {
            try
            {
                Type picType = pic.GetType();
                PropertyInfo pi = picType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(pic, setting, null);
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
        }

        // Image DubleBuffered 속성 지정 
        public static void DoubleBuffered(this Image imgZoom, bool setting)
        {
            try
            {
                Type imgZoomType = imgZoom.GetType();
                PropertyInfo pi = imgZoomType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(imgZoom, setting, null);
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
        }

        //// TextBox의 Padding 지정
        //public static void SetPadding(this TextBox textbox, Size size)
        //{
        //    try
        //    {
        //        Type textboxType = textbox.GetType();
        //        PropertyInfo pi = textboxType.GetProperty("Padding", BindingFlags.Instance | BindingFlags.NonPublic);
        //        pi.SetValue(textbox, size, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        String strTmp = ex.Message;
        //    }
        //}


    } // end class

}
