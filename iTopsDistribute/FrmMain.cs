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
using iTopsLib;

// DataGridView property doublebuffer 사용하기 위함
using System.Reflection;

// UtraGrid
using Infragistics.Win.UltraWinGrid;

//// Color
//using System.Drawing;

namespace iTopsDistribute
{
    //class CustomDataGridView : DataGridView
    //{
    //    public CustomDataGridView()
    //    {
    //        DoubleBuffered = true;
    //    }
    //}

    public partial class FrmMain : Form
    {

        // 접속자 정보
        String USER_INFO = "";

        int iLookupKind = -1;
        String strLookupKind = "";
        bool bUseDate = false;
        String strStart = "";
        String strEnd = "";
        int InspectorIndex = -1;
        String strInspectorId = "";
        String strInspectorNm = "";

        int chkbxAll_Pos = -1;

        //// CustomDataGridView
        //CustomDataGridView CDbGrid = null;

        public FrmMain(String[] args)
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

            InitializeComponent();

            //// 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            //if (!Lib.GFn_Login(USER_INFO)) Close();


            //// Load
            //// 전체 선택의 초기 위치 기억
            //ChkbxAll.Left = 9;
            //ChkbxAll.Top = 9;
            //if (chkbxAll_Pos < 0)
            //    chkbxAll_Pos = ChkbxAll.Left;

            //// Shown


            ////// Mouse Wheel 로 Grid Scroll 처리 
            ////this.DBGrid.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.DBGrid_MouseWheel);

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //// 전체 선택의 초기 위치 기억
            //ChkbxAll.Left = 9;
            //ChkbxAll.Top = 9;
            //if (chkbxAll_Pos < 0)
            //    chkbxAll_Pos = ChkbxAll.Left;

            // 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            if (!Lib.GFn_Login(USER_INFO)) Close();

            // Load
            // 전체 선택의 초기 위치 기억
            ChkbxAll.Left = 9;
            ChkbxAll.Top = 9;
            if (chkbxAll_Pos < 0)
                chkbxAll_Pos = ChkbxAll.Left;

        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            //// 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            //if (!Lib.GFn_Login(USER_INFO)) Close();

        }

        // MDIchild 로 들어가면 Mouse Wheel로 스크롤이 되지 않는다...Why?????
        //// Mouse Wheel 로 그리드 스크롤
        //private void DBGrid_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    //// Data가 있는 경우만 처리
        //    //if (DBGrid.Rows.Count <= 0) return;

        //}



        // 종료
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

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
                frmLookup.GFn_SetInit(iLookupKind, bUseDate, strStart, strEnd, InspectorIndex, strInspectorId);

                if (frmLookup.ShowDialog() == DialogResult.OK )
                {
                    iLookupKind = frmLookup.GFn_GetLookupKind(ref strLookupKind);
                    bUseDate = frmLookup.GFn_GetUseDate(ref strStart, ref strEnd);

                    InspectorIndex = -1;
                    strInspectorId = "";
                    strInspectorNm = "";
                    // 실패하면 다시 초기화 
                    if (!frmLookup.GFn_GetInspector(ref InspectorIndex, ref strInspectorId, ref strInspectorNm))
                    {
                        InspectorIndex = -1;
                        strInspectorId = "";
                        strInspectorNm = "";
                    }

                    // 조회 조건 화면 표시
                    if (bUseDate)
                    {
                        lblLookupCondition.Text = String.Format( "Loou up kind : [{0}] Use Date : [{1}] Date : [{2} ~ {3}] Inspector : [{4}]"
                                                               , strLookupKind, bUseDate ? "Y" : "N", strStart, strEnd, strInspectorNm + " ( " + strInspectorId + " )" );
                    } else
                    {
                        lblLookupCondition.Text = String.Format("Loou up kind : [{0}] Use Date : [{1}] Inspector : [{2}]"
                                                               , strLookupKind, bUseDate ? "Y" : "N", strInspectorNm + " ( " + strInspectorId + " )");
                    }


                    Cursor oldCursor = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    ChkbxAll.Visible = false;
                    ChkbxAll.Checked = false;
                    try
                    {

                        Fn_LoadData();      // Data 조회
                        Fn_Design();        // Grid Color, Font 꾸미기
                        Fn_SetColWidth();   // 컬럼 너비 설정 

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        ChkbxAll.Visible = true;
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
            // 임시로 사용 - DB, FTP 서버 접속
            //iTopsLib.Lib.Fn_Login("usr1", "usr1");

            String strDistributeYN = "";
            String strDateYN = "";
            //String strStart = "";
            //String strEnd = "";
            String strUseUserID = "";
            String strUserID = "";

            // 분배 여부
            if      (iLookupKind == 0) strDistributeYN = "'N'";
            else if (iLookupKind == 1) strDistributeYN = "'Y'";
            else if (iLookupKind == 2) strDistributeYN = "'Y', 'N'";
            else                       strDistributeYN = "''";

            // 기간조회 여부
            if (bUseDate) strDateYN = "Y";
            else          strDateYN = "N";
            //strStart
            //strEnd


            if (strInspectorId == "ALL") strUseUserID = "N";
            else                         strUseUserID = "Y";
            strUserID = strInspectorId;


            //SetGridInit();          // Grid 초기화 
            dsDistribution.Clear();
            int rv = iTopsLib.Lib.GFn_SelectRegulations( dsDistribution
                                                       , strDistributeYN
                                                       , strDateYN
                                                       , strStart
                                                       , strEnd
                                                       , strUseUserID
                                                       , strUserID
                                                       );
            // 자료 없음
            if (rv == 0)
            {
                MessageBox.Show("No data available!!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dsDistribution.Tables.Count > 0)
            {
                ////DbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                //DBGrid_ORG.DataSource = dsDistribution.Tables[0];

                ////DbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DBGrid.DataSource = dsDistribution.Tables[0];
                DBGrid.DataBind();
                DBGrid.UpdateData();

            }
        }

        private void SetGridInit()
        {
            DBGrid.DataSource = null;

            DBGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

            //그리드를 완전 초기화 한다.
            DBGrid.ResetDisplayLayout();
            //그리드의 캡션을 숨긴다.
            DBGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            //컬럼 넓이를 자동으로 조정한다
            DBGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            //그리드 클릭시 Row전체를 선택한게 한다.
            DBGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            //그리드 헤더와셀의 텍스트를 가운데로 정렬한다.
            DBGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            DBGrid.DisplayLayout.Bands[0].Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            // 모두 텍스트 스타일로 나오도록(이거안하면, 날짜부분에 달력이 생김)
            DBGrid.DisplayLayout.Bands[0].Override.CellDisplayStyle = CellDisplayStyle.FormattedText;

            //UGrid.DisplayLayout.Bands[0].Columns.Add("ROWNUM", "Order");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("NOTICE_NUM", "Notice Number");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("CARNUM", "Card Number");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("WHEN_DT", "Offence Date");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("FINE_S", "Fine");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("PAYED", "Pay");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("PAY_DUEDT", "Pay Date");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("PAYED_RECEIPT", "Receipt Num");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("N016_P_NAME", "Owner");
            //UGrid.DisplayLayout.Bands[0].Columns.Add("CUID", "cuid");
            //UGrid.DisplayLayout.Bands[0].Columns["CUID"].Hidden = true;


        }

        // Grid 꾸미기
        private void Fn_Design()
        {
            try
            {
                //DbGrid_ORG.AutoGenerateColumns = true;

                //// 사이즈 자동 맞춤
                ////dbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ////dbGrid.EditMode = DataGridViewEditMode.EditOnEnter;

                //// 일반 Row 열 디자인
                //DbGrid_ORG.BorderStyle = BorderStyle.None;
                //DbGrid_ORG.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                //DbGrid_ORG.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                //DbGrid_ORG.DefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Regular);

                //// Row 오른쪽 정렬
                //DbGrid_ORG.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DbGrid_ORG.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
                //DbGrid_ORG.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                //DbGrid_ORG.BackgroundColor = Color.White;

                //// ColumnHeader
                //DbGrid_ORG.EnableHeadersVisualStyles = false;
                //DbGrid_ORG.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                //DbGrid_ORG.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                //DbGrid_ORG.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 50, 72);

                ////header Column 오른쪽 정렬
                //DbGrid_ORG.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //DbGrid_ORG.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // 컬럼명 폰트컬러

                //// RowHeader
                //DbGrid_ORG.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(15,50,72);
                //DbGrid_ORG.RowHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                //DbGrid_ORG.RowHeadersDefaultCellStyle.ForeColor = Color.White; // Row명 폰트 컬러
                //DbGrid_ORG.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;





                //DBGrid.AutoGenerateColumns = true;

                //// 사이즈 자동 맞춤
                ////dbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                ////dbGrid.EditMode = DataGridViewEditMode.EditOnEnter;

                //// 일반 Row 열 디자인
                //DBGrid.DisplayLayout.BorderStyle = BorderStyle.None;
                //DBGrid.DisplayLayout.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                //DbGridDBGrid.DisplayLayout_ORG.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                //DBGrid.DisplayLayout.DefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Regular);
                DBGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(238, 239, 249);

                //// Row 오른쪽 정렬
                //DBGrid.DisplayLayout.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //DBGrid.DisplayLayout.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
                //DBGrid.DisplayLayout.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                //DBGrid.DisplayLayout.BackgroundColor = Color.White;

                //// ColumnHeader
                //DBGrid.DisplayLayout.EnableHeadersVisualStyles = false;
                //DBGrid.DisplayLayout.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                //DBGrid.DisplayLayout.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                //DBGrid.DisplayLayout.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 50, 72);
                DBGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;

                ////header Column 오른쪽 정렬
                //DBGrid.DisplayLayout.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //DBGrid.DisplayLayout.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // 컬럼명 폰트컬러

                //// RowHeader
                //DBGrid.DisplayLayout.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 50, 72);
                //DBGrid.DisplayLayout.RowHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                //DBGrid.DisplayLayout.RowHeadersDefaultCellStyle.ForeColor = Color.White; // Row명 폰트 컬러
                //DBGrid.DisplayLayout.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // SelectedRow.backcolor & forecolor
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.SystemColors.Window;
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.ForeColor = System.Drawing.SystemColors.ControlText;

                // SelectedRow.backcolor
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.Color.FromArgb
                    //(((System.Byte)(242)), ((System.Byte)(247)), ((System.Byte)(251)));
                    (((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
                //DBGrid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                //this.Cursor = oldCursor;

            }
        }

        // 컬럼별 너비 조정
        private void Fn_SetColWidth()
        {
            try
            {
                //// 숨기기
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.BRANCH].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.COURT].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.STREET].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.LOCATION].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DIRECT].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FINE].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].Visible = false;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].Visible = false;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Visible = false;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Visible = true;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].Visible = true;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Visible = false;

                //// 컬럼 너비 조정
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Width = 30;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.BRANCH].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.COURT].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.STREET].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.LOCATION].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DIRECT].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FINE].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Width = 30;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Width = 100;
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].Width = 100;

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Width = 100;


                //// 컬럼 제목
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].HeaderText = "";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].HeaderText = "ID";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].HeaderText = "Device";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].HeaderText = "Model";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].HeaderText = "No";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.BRANCH].HeaderText = "Branch";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER].HeaderText = "Officer";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].HeaderText = "Officer";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.COURT].HeaderText = "Court";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.STREET].HeaderText = "Violation Street";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.LOCATION].HeaderText = "Violation Location";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DIRECT].HeaderText = "Violation Direction";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].HeaderText = "Regulation Type";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].HeaderText = "Regulation Type";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].HeaderText = "Lane";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].HeaderText = "Distance";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].HeaderText = "Time";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].HeaderText = "Speed Limit";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].HeaderText = "Real Speed";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].HeaderText = "Over Speed";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].HeaderText = "Offence Code";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].HeaderText = "Offence Name";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FINE].HeaderText = "Fine";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].HeaderText = "File Dir";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].HeaderText = "File Org";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].HeaderText = "File No";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].HeaderText = "File Name";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].HeaderText = "File Plate";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].HeaderText = "Upload ID";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].HeaderText = "Upload Name";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].HeaderText = "Upload Time";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].HeaderText = "Plate No";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].HeaderText = "Maker";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].HeaderText = "Maker Name";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].HeaderText = "Type";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].HeaderText = "Type Name";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].HeaderText = "Color";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].HeaderText = "Model";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].HeaderText = "Year";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].HeaderText = "Orientation";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].HeaderText = "Offence YN";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].HeaderText = "Offence ID";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].HeaderText = "Inspector ID";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].HeaderText = "Inspector Name";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].HeaderText = "Cteate Time";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].HeaderText = "Create ID";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].HeaderText = "Change Time";
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].HeaderText = "Change ID";

                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].HeaderText = "ORG Inspector ID";

                //// 선택 컬럼만 수정 가능하게 
                //for (int i = 0; i < DbGrid_ORG.Columns.Count; i++)
                //{
                //    if (i == (int)iTopsLib.EnGridDistribution.SEL_YN)
                //        DbGrid_ORG.Columns[i].ReadOnly = false;
                //    else
                //        DbGrid_ORG.Columns[i].ReadOnly = true;
                //}

                //// 기타
                //DbGrid_ORG.Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Resizable = DataGridViewTriState.False;  // 선택 영역 사이즈 조정 못하게 ...


                // 숨기기
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH_NM].Hidden = !true;      // 2019.12.05
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.COURT].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.STREET].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.LOCATION].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DIRECT].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FINE].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].Hidden = !false;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].Hidden = !false;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Hidden = !false;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Hidden = !true;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].Hidden = !true;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Hidden = !false;

                // Date & time format
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Format = "dd-MMM-yyyy HH:mm:ss";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Format = "dd-MMM-yyyy HH:mm:ss";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Format = "dd-MMM-yyyy HH:mm:ss";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Format = "dd-MMM-yyyy HH:mm:ss";






                // 컬럼 너비 조정
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Width = 30;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.COURT].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.STREET].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.LOCATION].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DIRECT].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FINE].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Width = 30;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Width = 100;


                // 컬럼 제목
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Header.Caption = "";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Header.Caption = "ID";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_TP].Header.Caption = "Device";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_MDL].Header.Caption = "Model";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DEVICE_SN].Header.Caption = "No";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH].Header.Caption = "Branch";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.BRANCH_NM].Header.Caption = "Branch Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER].Header.Caption = "Officer";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFICER_NM].Header.Caption = "Officer";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.COURT].Header.Caption = "Court";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.STREET].Header.Caption = "Violation Street";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.LOCATION].Header.Caption = "Violation Location";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DIRECT].Header.Caption = "Violation Direction";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TP].Header.Caption = "Regulation Type";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_NM].Header.Caption = "Regulation Type";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_LANE].Header.Caption = "Lane";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_DIST].Header.Caption = "Distance";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_TIME].Header.Caption = "Time";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.RGLT_SPD_LIMIT].Header.Caption = "Speed Limit";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.REAL_SPEED].Header.Caption = "Real Speed";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OVER_SPEED].Header.Caption = "Over Speed";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_CODE].Header.Caption = "Offence Code";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_NM].Header.Caption = "Offence Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FINE].Header.Caption = "Fine";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_DIR].Header.Caption = "File Dir";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_ORG].Header.Caption = "File Org";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NO].Header.Caption = "File No";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_NM].Header.Caption = "File Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.FILE_PLATE].Header.Caption = "File Plate";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_ID].Header.Caption = "Upload ID";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_NM].Header.Caption = "Upload Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.UPLOAD_TIME].Header.Caption = "Upload Time";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_PLATE].Header.Caption = "Plate No";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER].Header.Caption = "Maker";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MAKER_NM].Header.Caption = "Maker Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE].Header.Caption = "Type";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_TYPE_NM].Header.Caption = "Type Name";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_COLOR].Header.Caption = "Color";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_MODEL].Header.Caption = "Model";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_YEAR].Header.Caption = "Year";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.DECIPHER_ORNTT].Header.Caption = "Orientation";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Header.Caption = "Offence YN";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Header.Caption = "Offence ID";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Header.Caption = "Inspector ID";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Header.Caption = "Inspector Name";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_DTM].Header.Caption = "Cteate Time";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CREATE_ID].Header.Caption = "Create ID";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_DTM].Header.Caption = "Change Time";
                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.CHG_ID].Header.Caption = "Change ID";

                DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Header.Caption = "ORG Inspector ID";

                // 선택 컬럼만 수정 가능하게 
                for (int i = 0; i < DBGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (i == (int)iTopsLib.EnGridDistribution.SEL_YN)
                        DBGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    else
                        DBGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }

                // 기타
                //DBGrid.DisplayLayout.Bands[0].Columns[(int)iTopsLib.EnGridDistribution.SEL_YN].Resizable = DataGridViewTriState.False;  // 선택 영역 사이즈 조정 못하게 ...


            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }
        }

        //private void dbGrid_Scroll(object sender, ScrollEventArgs e)
        //{
        //    ScrollOrientation orientation = e.ScrollOrientation;
        //    if (orientation == ScrollOrientation.HorizontalScroll)
        //    {
        //        //int iDiff = e.NewValue - e.OldValue;

        //        //MessageBox.Show(e.NewValue.ToString());

        //        chkbxAll.Left = chkbxAll_Pos - e.NewValue;

        //        if (chkbxAll.Left < chkbxAll.Width) chkbxAll.Visible = false;
        //        else                                chkbxAll.Visible = true;

        //    }

        //}

        // Select All click
        //private void chkbxAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool bSel = ChkbxAll.Checked;

        //    foreach (DataGridViewRow Row in DbGrid.Rows)
        //        ((DataGridViewCheckBoxCell)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN]).Value = bSel;
        //    DbGrid.RefreshEdit();

        //}

        // scroll에 따라 전체 선택 콤보 이동 및 숨기기
        private void DbGrid_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollOrientation orientation = e.ScrollOrientation;
            if (orientation == ScrollOrientation.HorizontalScroll)
            {
                //int iDiff = e.NewValue - e.OldValue;

                //MessageBox.Show(e.NewValue.ToString());

                ChkbxAll.Left = chkbxAll_Pos - e.NewValue;

                if (ChkbxAll.Left < ChkbxAll.Width) ChkbxAll.Visible = false;
                else ChkbxAll.Visible = true;

            }

        }

        private void ChkbxAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bSel = ChkbxAll.Checked;

            //foreach (DataGridViewRow Row in DbGrid_ORG.Rows)
            //    ((DataGridViewCheckBoxCell)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN]).Value = bSel;
            //DbGrid_ORG.RefreshEdit();
            
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Rows)
                ((Infragistics.Win.UltraWinGrid.UltraGridCell)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN]).Value = bSel;
            DBGrid.Refresh();


        }


        private void TsmnSelect_Click(object sender, EventArgs e)
        {
            //// 선택된 것이 없으면 그만 ...
            //if (DbGrid_ORG.SelectedRows.Count <= 0) return;

            //foreach (DataGridViewRow row in DbGrid_ORG.SelectedRows)
            //    row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = true;


            // 선택된 것이 없으면 그만 ...
            if (DBGrid.Selected.Rows.Count <= 0) return;

            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Selected.Rows)
                Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = true;
            //DBGrid.ActiveRow.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = true;

        }

        private void TsmnRelease_Click(object sender, EventArgs e)
        {
            //// 선택된 것이 없으면 그만 ...
            //if (DBGrid_ORG.SelectedRows.Count <= 0) return;

            //foreach (DataGridViewRow row in DBGrid_ORG.SelectedRows)
            //    row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = false;

            // 선택된 것이 없으면 그만 ...
            if (DBGrid.Selected.Rows.Count <= 0) return;

            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Selected.Rows)
                Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = false;

        }

        // Inspector 지정
        private void TsmnAssign_Click(object sender, EventArgs e)
        {
            // 수정된 값이 반영되도록 이벤트 발생 
            BtnAssign.Focus();

            // 선택( 체크) 된 것이 있는지 확인 
            int iSelCnt = 0;
            //foreach (DataGridViewRow row in DbGrid_ORG.Rows)
            //{
            //    if ((bool)row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value == true)
            //    {
            //        iSelCnt++;
            //    }
            //}
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Rows)
            {
                if ((bool)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value == true)
                {
                    iSelCnt++;
                }
            }

            if (iSelCnt == 0)
            {
                MessageBox.Show("Please work after selecting the data to distribute!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 담당자 지정 화면 띄우기
            FrmAssign frmAssign = new FrmAssign();
            try
            {
                // 지정했으면 
                if (frmAssign.ShowDialog() == DialogResult.OK)
                {
                    //InspectorIndex = -1;
                    //strInspectorId = "";
                    //strInspectorNm = "";
                    int index = -1;
                    String strId = "";
                    String strNm = "";
                    // 실패하면 다시 초기화 
                    //if (!frmAssign.GFn_GetInspector(ref InspectorIndex, ref strInspectorId, ref strInspectorNm))
                    if (!frmAssign.GFn_GetInspector(ref index, ref strId, ref strNm))
                    {
                        //InspectorIndex = -1;
                        //strInspectorId = "";
                        //strInspectorNm = "";
                        index = -1;
                        strId = "";
                        strNm = "";

                        return;
                    }

                    // 마지막 확인 메시지 후 작업 
                    if (MessageBox.Show(String.Format("{0} selected materials\n"
                                                    + "Would you like to distribute the selected data to {1}({2}) ?"
                                                //, iSelCnt, strInspectorNm, strInspectorId)
                                                , iSelCnt, strNm, strId)
                                  , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //foreach (DataGridViewRow row in DbGrid_ORG.Rows)
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Rows)
                        {
                            // 선택된 것만 ...
                            if ((bool)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value == false) continue;

                            // Inspector 정보
                            //row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value = strInspectorId;
                            //row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Value = strInspectorNm;
                            Row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value = strId;
                            Row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Value = strNm;

                            // 분배여부
                            Row.Cells[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Value = true;
                            Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = false;


                        }
                        // 전체 선택 해제
                        ChkbxAll.Checked = false;

                        MessageBox.Show("The operation completed successfully.\n\n"
                                      + "Be sure to Save !!!", "Information"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                frmAssign.Dispose();
            }
        }

        // 균등 분배 화면 띄우기
        private void BtnEvenly_Click(object sender, EventArgs e)
        {
            // 수정된 값이 반영되도록 이벤트 발생 
            BtnAssign.Focus();


            // 선택( 체크) 된 것이 있는지 확인 
            int iSelCnt = 0;
            //foreach (DataGridViewRow row in DbGrid_ORG.Rows)
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow Row in DBGrid.Rows)
            {
                if ((bool)Row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value == true)
                {
                    iSelCnt++;
                }
            }

            if (iSelCnt == 0)
            {
                MessageBox.Show("Please work after selecting the data to distribute!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 담당자 지정 화면 띄우기
            FrmEvenly frmEvenly = new FrmEvenly();
            try
            {
                // 지정했으면 
                if (frmEvenly.ShowDialog() == DialogResult.OK)
                {
                    String[] strArrID = null;
                    String[] strArrNM = null;
                    // 실패하면 다시 초기화 
                    if (!frmEvenly.GFn_GetInspector(ref InspectorIndex, ref strArrID, ref strArrNM))
                    {
                        //InspectorIndex = -1;
                        //strInspectorId = "";
                        //strInspectorNm = "";

                        return;
                    }

                    int iPerson = strArrID.Length;
                    if (iPerson == 0) return;       // 혹시 모르니까 

                    int iCurPos = 0;

                    // 마지막 확인 메시지 후 작업 
                    if (MessageBox.Show(String.Format("Could you please distribute {0} pieces of data equally to {1} people?) ?"
                                                , iSelCnt, iPerson)
                                  , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        //foreach (DataGridViewRow row in DbGrid_ORG.Rows)
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in DBGrid.Rows)
                        {
                            // 선택된 것만 ...
                            if ((bool)row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value == false) continue;

                            
                            
                            row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value = strArrID[iCurPos];   // 담당자
                            row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_NM].Value = strArrNM[iCurPos];   // 담당자

                            row.Cells[(int)iTopsLib.EnGridDistribution.OFFENCE_YN].Value = true;      // 분배여부
                            row.Cells[(int)iTopsLib.EnGridDistribution.SEL_YN].Value = false;       // 선택 여부

                            iCurPos++;
                            if (iCurPos >= iPerson) iCurPos = 0;

                        }
                        // 전체 선택 해제
                        ChkbxAll.Checked = false;

                        MessageBox.Show("The operation completed successfully.\n\n"
                                      + "Be sure to Save !!!", "Information"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                frmEvenly.Dispose();
            }

        }

        private int Fn_IsChanged()
        {
            int iCnt = 0;
            //foreach(DataGridViewRow row in DbGrid_ORG.Rows)
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in DBGrid.Rows)
            {
                // 변경 사항이 없으면 Skip
                if (Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value) 
                    == Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Value)) continue;
                iCnt++;
            }

            return iCnt;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int iCnt = Fn_IsChanged();
            if ( iCnt <= 0)
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
                String[] strArrRgltn_id = new String[iCnt];
                String[] strArrOffence_id = new String[iCnt];
                String[] strArrInspector_id = new String[iCnt];
                String[] strArrResult = new String[iCnt];

                int iPos = -1;
                //foreach (DataGridViewRow row in DbGrid_ORG.Rows)
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in DBGrid.Rows)
                {
                    // 변경 사항이 없으면 Skip
                    if (Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value)
                        == Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.ORG_INSPECTOR_ID].Value)) continue;

                    iPos++;
                    strArrRgltn_id[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.RGLTN_ID].Value);

                    strArrOffence_id[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.OFFENCE_ID].Value);
                    strArrInspector_id[iPos] = Convert.ToString(row.Cells[(int)iTopsLib.EnGridDistribution.INSPECTOR_ID].Value);
                }

                // 저장 
                int rv = iTopsLib.Lib.GFn_InsertOffencesInDistribute( ref strArrRgltn_id
                                                                    , ref strArrOffence_id
                                                                    , ref strArrInspector_id
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

            Fn_LoadData();      // Data 다시 조회
        }

        // 종료전에 변경한 자료가 있는지 확인 
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

            //// CustomDataGridView 사용자 그리드 해제
            //if (CDbGrid != null) CDbGrid.Dispose();

        }

        private void DbGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DbGrid.Refresh();
        }

        private void FrmMain_Deactivate(object sender, EventArgs e)
        {
            //if (frmLookup != null)
            //{
            //    TabPage page = iTopsLib.Lib.GFn_GetActivePage();
            //    if ((TabPage)(this.Parent) != page)
            //        frmLookup.GFn_Close();
            //}
        }

        // 우측 클릭시 active row 변경
        private void DBGrid_MouseDown(object sender, MouseEventArgs e)
        {
            
            //Infragistics.Win.UltraWinGrid.UltraGridRow row;
            //Infragistics.Win.UIElement element;

            //element = DBGrid.DisplayLayout.UIElement.ElementFromPoint(e.Location);
            //row = element.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow)) as Infragistics.Win.UltraWinGrid.UltraGridRow;

            //if (row == null || !row.IsDataRow) return;

            //// Only Mouse Click
            //if (ModifierKeys.HasFlag(Keys.None))
            //{
            //    DBGrid.Selected.Rows.Clear();

            //    //DBGrid.ActiveRow = row;
            //    DBGrid.Selected.Rows.Add(row);
            //}
            //// Ctrl + Mouse Click
            //else if (ModifierKeys.HasFlag(Keys.Control))
            //{
            //    //DBGrid.ActiveRow = row;
            //    DBGrid.Selected.Rows.Add(row);

            //}
            //// Shift + Mouse Click
            //else if (ModifierKeys.HasFlag(Keys.Shift))
            //{
            //    if ()
            //}
            //else
            //{

            //}

            ////if (e.Button == MouseButtons.Right)
            ////{
            ////    if (row != null && row.IsDataRow)
            ////    {
            ////        DBGrid.ActiveRow = row;
            ////        DBGrid.Selected.Rows.Add(row);
            ////    }
            ////}
        }

        //================================================================================================================//
        // Grid 자체의 속성을 
        //      DBGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
        // 로 설정하고 
        // CheckBox 선택/해제를 처리해 준다.
        // Mouse Left가 Click 되고 Keyboard의 아무 것도 누르지 않은 것만 처리하고
        // 그 외(Keyboard가 같이 눌린 경우, Mouse의 다른 Event의 경우) System이 처리하도록 한다. <=== Grid Multi 선택을 처리하기 위 함
        //================================================================================================================//
        private void DBGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Mouse 좌측 버튼이 눌린 경우만 처리한다.
                if (e.Button != MouseButtons.Left) return;

                // Shift, Ctrl 등 다른 키를 누르고 Mouse Click 한 경우 System에서 처리하도록 맡긴다.
                bool bKeyNone = !Convert.ToBoolean(ModifierKeys);
                if (!bKeyNone) return;


                // 마우스 클릭한 위치( Cell ) 을 찾는다.
                Infragistics.Win.UIElement element;
                Infragistics.Win.UltraWinGrid.UltraGridCell cell;

                element = DBGrid.DisplayLayout.UIElement.ElementFromPoint(e.Location);
                cell = element.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell)) as Infragistics.Win.UltraWinGrid.UltraGridCell;

                if (cell == null) return;

                // Row 범위 확인 
                int iRow = cell.Row.Index;
                if (iRow < 0 || iRow >= DBGrid.Rows.Count) return;

                // 선택 체크박스를 클릭한 경우 선택을 반전 시킨다.
                int iCol = cell.Column.Index;
                if (iCol == (int)iTopsLib.EnGridDistribution.SEL_YN)
                {
                    DBGrid.Rows[iRow].Cells[iCol].Value = !Convert.ToBoolean(DBGrid.Rows[iRow].Cells[iCol].Value);
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }

        }
    }

    //// DataGridView 속도개선 - Dublebuffer
    //public static class ExtensionMethods
    //{
    //    public static void DoubleBuffered(this DataGridView dbGridView, bool setting)
    //    {
    //        Type dbGridViewType = dbGridView.GetType();
    //        PropertyInfo pi = dbGridViewType.GetProperty("DoubleBuffered",
    //            BindingFlags.Instance | BindingFlags.NonPublic);
    //        pi.SetValue(dbGridView, setting, null);
    //    }

    //}
}


