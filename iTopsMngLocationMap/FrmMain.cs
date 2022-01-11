// UltraWinGrid 제어
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
// 
using iTopsLib;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iTopsMngLocationMap
{
    public partial class FrmMain : Form
    {
        // 전역
        bool bIsStart = false;
        //bool bIsStart_D = false;

        // 접속자 정보
        String USER_INFO = "";

        //int chkbxAll_Pos = -1;
        int pnlBdTDetail_Height = -1;
        //int pnlBdTDtlOffence_Width = -1;

        // 생성자
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

            //
            InitializeComponent();



            // 컨트롤 초기 위치, 너비, 높이
            pnlBdTDetail_Height = pnlBdTDetail.Height;
            //pnlBdTDtlOffence_Width = pnlBdTDetail.Width - pnlDtlMenu.Left - pnlDtlMenu.Width;
            ////pnlBdTDtlOffence_Width = this.Width - pnlDtlMenu.Left - pnlDtlMenu.Width;
            //pnlBdTDtlOffence_Width = (pnlBdTDetail.Width - 1130);


            // 시작
            bIsStart = true;
            //bIsStart_D = true;

        }

        // 
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                // 처음 한번만 실행
                if (bIsStart)
                {
                    // 
                    Fn_InitControl();

                    // 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
                    if (!Lib.GFn_Login(USER_INFO)) Close();

                    // 코드 읽기
                    Fn_SetCodeWithDB();

                    //// Data 읽기
                    //Fn_LoadData();

                    bIsStart = false;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                // 혹시 모르니까
                if (bIsStart) bIsStart = false;
            }
        }

        // 종료
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ComboBox 코드 읽기 
        private void Fn_SetCodeWithDB()
        {
            try
            {
                // Court Code
                try
                {
                    int rv = iTopsLib.Lib.GFn_GetCodeDS("COURT", "", ref dsCourt, true, false);
                    if (rv < 0) return;
                    if (rv > 0)
                    {
                        CbbCourt.DataSource = dsCourt.Tables[0];

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


                // Location Code
                //try
                //{
                //    int rv = iTopsLib.Lib.GFn_GetLocationCodeDS(ref dsLocationCd, true);
                //    if (rv < 0) return;
                //    if (rv > 0)
                //    {
                //        CbbLocationCd.DataSource = dsLocationCd.Tables[0];

                //        CbbLocationCd.DisplayMember = "display_value";
                //        CbbLocationCd.ValueMember = "cd";

                //        CbbLocationCd.Text = null;
                //    }

                //}
                //catch (Exception ex)
                //{
                //    String strTmp = ex.Message;
                //}
                //finally
                //{

                //}
                Fn_SetOnlyLocationWithDB("");

                // Direction Code
                try
                {
                    int rv = iTopsLib.Lib.GFn_GetCodeDS("DIRECTION", "", ref dsDirection, true, false);
                    if (rv < 0) return;
                    if (rv > 0)
                    {
                        CbbDirection.DataSource = dsDirection.Tables[0];

                        CbbDirection.DisplayMember = "display_value";
                        CbbDirection.ValueMember = "cd";

                        CbbDirection.Text = null;
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;
                }
                finally
                {

                }

                // OFFENCE Reference
                try
                {
                    int rv = iTopsLib.Lib.GFn_GetCodeDS("OFFENCE_RF", "", ref dsReference, true, false);
                    if (rv < 0) return;
                    if (rv > 0)
                    {
                        CbbReferenceCd.DataSource = dsReference.Tables[0];

                        CbbReferenceCd.DisplayMember = "display_value";
                        CbbReferenceCd.ValueMember = "cd";

                        CbbReferenceCd.Text = null;
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;
                }
                finally
                {

                }

                // OFFENCE Legislation
                //try
                //{
                //    int rv = iTopsLib.Lib.GFn_GetCodeDS("OFFENCE_RL", "", ref dsLegislation, true, false);
                //    if (rv < 0) return;
                //    if (rv > 0)
                //    {
                //        CbbLegislation.DataSource = dsLegislation.Tables[0];

                //        CbbLegislation.DisplayMember = "display_value";
                //        CbbLegislation.ValueMember = "cd";

                //        CbbLegislation.Text = null;
                //    }

                //}
                //catch (Exception ex)
                //{
                //    String strTmp = ex.Message;
                //}
                //finally
                //{

                //}
                Fn_SetOnlyLegislationWithDB("");


            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
            }

        }

        // ComboBox 코드 읽기 - Location
        private void Fn_SetOnlyLocationWithDB(String strCourt)
        {
            try
            {
                int rv = iTopsLib.Lib.GFn_GetLocationCodeDS(ref dsLocationCd, true, strCourt);
                if (rv < 0) return;
                if (rv > 0)
                {
                    CbbLocationCd.DataSource = dsLocationCd.Tables[0];

                    CbbLocationCd.DisplayMember = "display_value";
                    CbbLocationCd.ValueMember = "cd";

                    CbbLocationCd.Text = null;
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

        // ComboBox 코드 읽기 - Legislation
        private void Fn_SetOnlyLegislationWithDB(String strCode)
        {
            try
            {

                // OFFENCE Legislation
                try
                {
                    int rv = iTopsLib.Lib.GFn_GetCodeDS("OFFENCE_RL", strCode, ref dsLegislation, true, false);
                    if (rv < 0) return;
                    if (rv > 0)
                    {
                        CbbLegislationCd.DataSource = dsLegislation.Tables[0];

                        CbbLegislationCd.DisplayMember = "display_value";
                        CbbLegislationCd.ValueMember = "cd";

                        CbbLegislationCd.Text = null;
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
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
            }

        }

        // 초기 컨트롤 상태 설정 
        private void Fn_InitControl()
        {
            if (bIsStart)
            {
                spltDBGrid.Appearance.BackColor = System.Drawing.Color.White;
                spltDBGrid.Appearance.BackColor2 = System.Drawing.Color.White;
                spltDBGrid.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;

            }
        }

        // Main Data Load
        private int Fn_LoadData()
        {

            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {


                // 화면 Clear
                //Fn_ClearScreen();

                // 조회 조건 화면 표시
                lblLookupCondition.Text = String.Format("Loou up kind : ALL");


                // Data 조회
                int rv = iTopsLib.Lib.GFn_GetLocationMapDS(ref dsLocationMap, false);


                if (dsLocationMap.Tables.Count > 0)
                {

                    DBGrid.BeginUpdate();
                    try
                    {

                        //DBGrid.DataSource = dsLocationMap.Tables[0];

                        //// 디자인 등등
                        //if (bIsStart)
                        //{
                        //    Fn_Design();
                        //    Fn_SetColWidth();
                        //}

                        if (DBGrid.DataSource == null)
                        {
                            chkbxSel_All.Visible = true;

                            DBGrid.DataSource = dsLocationMap.Tables[0];

                            Fn_Design();
                            Fn_SetColWidth();
                        }

                        // Filtering - 삭제된 것만 숨기고 다 보여 준다.
                        Fn_SetGridFiter(null, false);


                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;
                        rv = -1;
                    }
                    finally
                    {
                        DBGrid.EndUpdate();

                    }

                    // 자료 없음
                    if (rv == 0)
                    {
                        MessageBox.Show("No data available!!", "Information"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return rv;
                    }

                    if (rv > 0)
                    {
                        //Fn_SetEnable(true);

                        DBGrid.Rows[0].Selected = true;
                        DBGrid.Refresh();
                    }


                }

                return rv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                //ChkbxAll.Visible = true;
                this.Cursor = oldCursor;

            }
        }

        // Grid 꾸미기
        private void Fn_Design()
        {
            try
            {
                // 전체 디자인 ======================================
                //// 일반 Row 열 디자인
                DBGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(238, 239, 249);

                //// ColumnHeader
                DBGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;

                // SelectedRow.backcolor & forecolor
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.SystemColors.Window;
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.ForeColor = System.Drawing.SystemColors.ControlText;

                // SelectedRow.backcolor
                DBGrid.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.Color.FromArgb
                    (((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));


                // 컬럼별 디자인=====================================
                // 보여주기/숨기기
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["court"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["court_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["location"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["location_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["direction"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["direction_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["reference"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["reference_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["category"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["create_id"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["create_nm"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_id"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_nm"].Hidden = false;

                DBGrid.DisplayLayout.Bands[0].Columns["court_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["location_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["direction_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["reference_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["category_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn_org"].Hidden = true;

                DBGrid.DisplayLayout.Bands[0].Columns["rec_state"].Hidden = true;

                // 고정 컬럼
                DBGrid.DisplayLayout.UseFixedHeaders = true;
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Fixed = true;

                //// Currency
                //DBGrid.DisplayLayout.Bands[0].Columns["fine"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;

                // Date & time format
                DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Format = "yyyy-MM-dd HH:mm:ss";

                DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Format = "yyyy-MM-dd HH:mm:ss";


                // Header 가운데 정렬
                for (int iCol = 0; iCol < DBGrid.DisplayLayout.Bands[0].Columns.Count; iCol++)
                {
                    // Header
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextHAlign = HAlign.Center;
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextVAlign = VAlign.Middle;

                    // 좌측 정렬
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Left;
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextVAlign = VAlign.Middle;

                    // 우측정렬
                    if (iCol == DBGrid.DisplayLayout.Bands[0].Columns["map_seq"].Index)
                    {
                        DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Right;
                    }
                    // 중앙정렬
                    else if (iCol == DBGrid.DisplayLayout.Bands[0].Columns["category"].Index)
                    {
                        DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Center;
                    }


                }


                // 선택 컬럼만 수정 가능하게 
                for (int i = 0; i < DBGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (i == DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Index)
                        DBGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    else
                        DBGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
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

        // 그리드 컬럼별 너비 / 제목 
        private void Fn_SetColWidth()
        {
            try
            {
                // 컬럼 너비 조정
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Width = 30;
                DBGrid.DisplayLayout.Bands[0].Columns["court"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["court_desc"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["location"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["location_desc"].Width = 300;
                DBGrid.DisplayLayout.Bands[0].Columns["direction"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["direction_desc"].Width = 70;
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq"].Width = 50;
                DBGrid.DisplayLayout.Bands[0].Columns["reference"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["reference_desc"].Width = 210;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_desc"].Width = 350;
                DBGrid.DisplayLayout.Bands[0].Columns["category"].Width = 60;
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc"].Width = 300;
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list"].Width = 300;
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn"].Width = 30;
                DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Width = 150;
                DBGrid.DisplayLayout.Bands[0].Columns["create_id"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["create_nm"].Width = 150;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Width = 150;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_id"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["chg_nm"].Width = 150;

                DBGrid.DisplayLayout.Bands[0].Columns["court_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["location_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["direction_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["reference_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["category"].Width = 60;
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn_org"].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns["rec_state"].Width = 100;

                // 컬럼 제목
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Caption = " ";
                DBGrid.DisplayLayout.Bands[0].Columns["court"].Header.Caption = "Court";
                DBGrid.DisplayLayout.Bands[0].Columns["court_desc"].Header.Caption = "Court";
                DBGrid.DisplayLayout.Bands[0].Columns["location"].Header.Caption = "Location";
                DBGrid.DisplayLayout.Bands[0].Columns["location_desc"].Header.Caption = "Location";
                DBGrid.DisplayLayout.Bands[0].Columns["direction"].Header.Caption = "Direction";
                DBGrid.DisplayLayout.Bands[0].Columns["direction_desc"].Header.Caption = "Direction";
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq"].Header.Caption = "Seq";
                DBGrid.DisplayLayout.Bands[0].Columns["reference"].Header.Caption = "Reference";
                DBGrid.DisplayLayout.Bands[0].Columns["reference_desc"].Header.Caption = "Reference";
                DBGrid.DisplayLayout.Bands[0].Columns["legislation"].Header.Caption = "Legislation";
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_desc"].Header.Caption = "Legislation";
                DBGrid.DisplayLayout.Bands[0].Columns["category"].Header.Caption = "Category";
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc"].Header.Caption = "Map Desc";
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list"].Header.Caption = "Offence Code";
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn"].Header.Caption = "YN";
                DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Header.Caption = "Create Date";
                DBGrid.DisplayLayout.Bands[0].Columns["create_id"].Header.Caption = "Create Id";
                DBGrid.DisplayLayout.Bands[0].Columns["create_nm"].Header.Caption = "Create Name";
                DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Header.Caption = "Change Date";
                DBGrid.DisplayLayout.Bands[0].Columns["chg_id"].Header.Caption = "Chage Id";
                DBGrid.DisplayLayout.Bands[0].Columns["chg_nm"].Header.Caption = "Change Name";

                DBGrid.DisplayLayout.Bands[0].Columns["court_org"].Header.Caption = "court_org";
                DBGrid.DisplayLayout.Bands[0].Columns["location_org"].Header.Caption = "location_org";
                DBGrid.DisplayLayout.Bands[0].Columns["direction_org"].Header.Caption = "direction_org";
                DBGrid.DisplayLayout.Bands[0].Columns["map_seq_org"].Header.Caption = "map_seq_org";
                DBGrid.DisplayLayout.Bands[0].Columns["reference_org"].Header.Caption = "reference_org";
                DBGrid.DisplayLayout.Bands[0].Columns["legislation_org"].Header.Caption = "legislation_org";
                DBGrid.DisplayLayout.Bands[0].Columns["category"].Header.Caption = "category_org";
                DBGrid.DisplayLayout.Bands[0].Columns["map_desc_org"].Header.Caption = "map_desc_org";
                DBGrid.DisplayLayout.Bands[0].Columns["offence_list_org"].Header.Caption = "offence_list_org";
                DBGrid.DisplayLayout.Bands[0].Columns["use_yn_org"].Header.Caption = "use_yn_org";

                DBGrid.DisplayLayout.Bands[0].Columns["rec_state"].Header.Caption = "rec_state";

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }
        }

        // Detail Offence Data Load
        private int Fn_LoadDataDtl()
        {

            chkbxSel_All_D.CheckedChanged -= chkbxSel_All_D_CheckedChanged;
            try
            {
                dsOffenceCd.Clear();
                chkbxSel_All_D.Checked = false;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                chkbxSel_All_D.CheckedChanged += chkbxSel_All_D_CheckedChanged;
            }

            int rv = 0;

            if (DBGrid.Rows.Count < 0) return rv;
            if (DBGrid.ActiveRow == null) return rv;

            // 
            if (Convert.ToString(CbbReferenceCd.Value) == String.Empty) return rv;
            if (Convert.ToString(CbbLegislationCd.Value) == String.Empty) return rv;
            if (Convert.ToString(txtCategory.Value) == String.Empty) return rv;

            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 조회 조건 
                String strReferenceCd = Convert.ToString(CbbReferenceCd.Value);
                String strLegislationCd = Convert.ToString(CbbLegislationCd.Value);
                String strCategory = Convert.ToString(txtCategory.Value);

                // Data 조회
                rv = iTopsLib.Lib.GFn_GetOffenceCodeForMapDS(ref dsOffenceCd
                                                           , strReferenceCd
                                                           , strLegislationCd
                                                           , strCategory
                                                           , false
                                                            );


                if (dsOffenceCd.Tables.Count > 0)
                {

                    DBGrid_Offence.BeginUpdate();
                    try
                    {

                        if (DBGrid_Offence.DataSource == null)
                        {
                            chkbxSel_All_D.Visible = true;
                            DBGrid_Offence.DataSource = dsOffenceCd.Tables[0];

                            Fn_DesignDtl();
                            Fn_SetColWidthDtl();

                            // 마지막으로 기존 선택항목 표시
                            Fn_SetOffenceList(Convert.ToString(DBGrid.ActiveRow.Cells["offence_list"].Value));
                        }

                        //// Filtering - 삭제된 것만 숨기고 다 보여 준다.
                        //Fn_SetGridFiter(null, false);


                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;
                        rv = -1;
                    }
                    finally
                    {
                        DBGrid_Offence.EndUpdate();

                    }

                    // 자료 없음
                    if (rv == 0)
                    {
                        //MessageBox.Show("No data available!!", "Information"
                        //    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ////return rv;
                    }

                    if (rv > 0)
                    {
                        //Fn_SetEnable(true);

                        DBGrid_Offence.Rows[0].Selected = true;
                        DBGrid_Offence.Refresh();
                    }


                }

                return rv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                this.Cursor = oldCursor;

            }
        }

        // Grid 꾸미기
        private void Fn_DesignDtl()
        {
            try
            {
                // 전체 디자인 ======================================
                //// 일반 Row 열 디자인
                DBGrid_Offence.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(238, 239, 249);

                //// ColumnHeader
                DBGrid_Offence.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;

                // SelectedRow.backcolor & forecolor
                DBGrid_Offence.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.SystemColors.Window;
                DBGrid_Offence.DisplayLayout.Override.SelectedRowAppearance.ForeColor = System.Drawing.SystemColors.ControlText;

                // SelectedRow.backcolor
                DBGrid_Offence.DisplayLayout.Override.SelectedRowAppearance.BackColor = System.Drawing.Color.FromArgb
                    (((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));


                // 컬럼별 디자인=====================================
                // 보여주기/숨기기
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["offence_cd"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["reference_cd"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["legislation_cd"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["category"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["use_yn"].Hidden = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_from"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_to"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Hidden = false;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn_org"].Hidden = true;

                DBGrid_Offence.DisplayLayout.Bands[0].Columns["rec_state"].Hidden = true;

                // 고정 컬럼
                DBGrid_Offence.DisplayLayout.UseFixedHeaders = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["offence_cd"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["reference_cd"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["legislation_cd"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["category"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["use_yn"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_from"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_to"].Header.Fixed = true;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Header.Fixed = true;

                // Currency
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;

                // Date & time format
                //DBGrid_Offence.DisplayLayout.Bands[0].Columns["create_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                //DBGrid_Offence.DisplayLayout.Bands[0].Columns["create_dtm"].Format = "yyyy-MM-dd HH:mm:ss";

                //DBGrid_Offence.DisplayLayout.Bands[0].Columns["chg_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                //DBGrid_Offence.DisplayLayout.Bands[0].Columns["chg_dtm"].Format = "yyyy-MM-dd HH:mm:ss";

                // Header 가운데 정렬
                for (int iCol = 0; iCol < DBGrid_Offence.DisplayLayout.Bands[0].Columns.Count; iCol++)
                {
                    // Header
                    DBGrid_Offence.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextHAlign = HAlign.Center;
                    DBGrid_Offence.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextVAlign = VAlign.Middle;

                    // 중앙 정렬
                    DBGrid_Offence.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Center;
                    DBGrid_Offence.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextVAlign = VAlign.Middle;

                    // 우측정렬
                    if (iCol == DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_from"].Index ||
                        iCol == DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_to"].Index ||
                        iCol == DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Index)
                    {
                        DBGrid_Offence.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Right;
                    }
                    // 좌측정렬
                    else if (iCol == DBGrid_Offence.DisplayLayout.Bands[0].Columns["category"].Index)
                    {
                        //
                    }

                }

                // 우측 정렬


                // 선택 컬럼만 수정 가능하게 
                for (int i = 0; i < DBGrid_Offence.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (i == DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Index)
                        DBGrid_Offence.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    else
                        DBGrid_Offence.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
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

        // 그리드 컬럼별 너비 / 제목 
        private void Fn_SetColWidthDtl()
        {
            try
            {

                // 컬럼 너비 조정
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Width = 30;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["offence_cd"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["reference_cd"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["legislation_cd"].Width = 120;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["category"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["use_yn"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_from"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_to"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn_org"].Width = 100;
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["rec_state"].Width = 100;

                // 컬럼 제목
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Caption = " ";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["offence_cd"].Header.Caption = "Offence";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["reference_cd"].Header.Caption = "Reference";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["legislation_cd"].Header.Caption = "Legislation";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["category"].Header.Caption = "Category";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["use_yn"].Header.Caption = "YN";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_from"].Header.Caption = "From";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["speed_to"].Header.Caption = "To";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["fine"].Header.Caption = "Fine";
                DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn_org"].Header.Caption = "sel_yn_org";

                DBGrid_Offence.DisplayLayout.Bands[0].Columns["rec_state"].Header.Caption = "rec_state";

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }
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
                // Row가 선택되면 무조건 
                CbbCourt.Focus();

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

                // Click한 행을 선택 처리
                //DBGrid.Selected.Rows.Clear();
                //DBGrid.Rows[iRow].Selected = true;

                // 선택 체크박스를 클릭한 경우 선택을 반전 시킨다.
                int iCol = cell.Column.Index;
                if (iCol == DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Index)
                {
                    DBGrid.Rows[iRow].Cells[iCol].Value = !Convert.ToBoolean(DBGrid.Rows[iRow].Cells[iCol].Value);
                }

                // 상세영역에 데이터 보여주기 
                //if (pnlBody.Height != pnlBdTMenu.Height)    // 상세 영역이 펼쳐져 있으면 
                //{
                //    Fn_DisplayData(iRow);
                //}
                DBGrid.Rows[iRow].Selected = true;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }

        }

        // Fn_ClearDetail
        private void Fn_ClearDetail()
        {
            try
            {
                CbbCourt.Value = String.Empty;
                CbbLocationCd.Value = String.Empty;
                CbbDirection.Value = String.Empty;
                CbbReferenceCd.Value = String.Empty;
                CbbLegislationCd.Value = String.Empty;
                txtCategory.Value = String.Empty;
                txtMappingDesc.Value = String.Empty;
                txtSpeedLimit.Value = String.Empty;
                txtMinPenaltySpeed.Value = String.Empty;
                txtMaxPenaltySpeed.Value = String.Empty;
                chkUseYN.Checked = false;

                dsOffenceCd.Clear();
                DBGrid_Offence.DataSource = null;

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
        // DBGrid의 선택이 변경되면 상세 화면에 보여준다.
        private void Fn_DisplayData(int index)
        {
            CbbCourt.ValueChanged -= DetailItem_ValueChanged;
            CbbLocationCd.ValueChanged -= DetailItem_ValueChanged;
            CbbDirection.ValueChanged -= DetailItem_ValueChanged;
            CbbReferenceCd.ValueChanged -= DetailItem_ValueChanged;
            CbbLegislationCd.ValueChanged -= DetailItem_ValueChanged;
            txtCategory.ValueChanged -= DetailItem_ValueChanged;
            txtMappingDesc.ValueChanged -= DetailItem_ValueChanged;
            chkUseYN.CheckedChanged -= DetailItem_ValueChanged;

            try
            {
                // Code Combo
                Fn_SetOnlyLocationWithDB("");
                Fn_SetOnlyLegislationWithDB("");

                // 화면 상단 Detail 부분 Clear
                Fn_ClearDetail();

                if (index < 0) return;

                // 값 넣기
                CbbCourt.Value = Convert.ToString(DBGrid.Rows[index].Cells["court"].Value);
                CbbLocationCd.Value = Convert.ToString(DBGrid.Rows[index].Cells["location"].Value);
                CbbDirection.Value = Convert.ToString(DBGrid.Rows[index].Cells["direction"].Value);
                CbbReferenceCd.Value = Convert.ToString(DBGrid.Rows[index].Cells["reference"].Value);
                CbbLegislationCd.Value = Convert.ToString(DBGrid.Rows[index].Cells["legislation"].Value);
                txtCategory.Value = Convert.ToString(DBGrid.Rows[index].Cells["category"].Value);
                txtMappingDesc.Value = Convert.ToString(DBGrid.Rows[index].Cells["map_desc"].Value);
                chkUseYN.Checked = Convert.ToBoolean(DBGrid.Rows[index].Cells["use_yn"].Value);


                // ValueChanged EventHandler를 같이 사용하기 위함
                CbbCourt.Tag = Convert.ToString(DBGrid.Rows[index].Cells["court"].Column.Index);
                CbbLocationCd.Tag = Convert.ToString(DBGrid.Rows[index].Cells["location"].Column.Index);
                CbbDirection.Tag = Convert.ToString(DBGrid.Rows[index].Cells["direction"].Column.Index);
                CbbReferenceCd.Tag = Convert.ToString(DBGrid.Rows[index].Cells["reference"].Column.Index);
                CbbLegislationCd.Tag = Convert.ToString(DBGrid.Rows[index].Cells["legislation"].Column.Index);
                txtCategory.Tag = Convert.ToString(DBGrid.Rows[index].Cells["category"].Column.Index);
                txtMappingDesc.Tag = Convert.ToString(DBGrid.Rows[index].Cells["map_desc"].Column.Index);
                chkUseYN.Tag = Convert.ToString(DBGrid.Rows[index].Cells["use_yn"].Column.Index);

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                CbbCourt.ValueChanged += DetailItem_ValueChanged;
                CbbLocationCd.ValueChanged += DetailItem_ValueChanged;
                CbbDirection.ValueChanged += DetailItem_ValueChanged;
                CbbReferenceCd.ValueChanged += DetailItem_ValueChanged;
                CbbLegislationCd.ValueChanged += DetailItem_ValueChanged;
                txtCategory.ValueChanged += DetailItem_ValueChanged;
                txtMappingDesc.ValueChanged += DetailItem_ValueChanged;
                chkUseYN.CheckedChanged += DetailItem_ValueChanged;

                // 우측 읽어준다.
                Fn_LoadDataDtl();

                // 작업할 수 있도록 Focus 이동
                CbbCourt.Focus();
            }

        }


        // 마우스가 나가면 원상복구
        private void spltDBGrid_MouseLeave(object sender, EventArgs e)
        {
            //if (sender is Infragistics.Win.Misc.UltraSplitter)
            //{
            //    (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor = System.Drawing.Color.White;
            //    (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor2 = System.Drawing.Color.White;
            //    //(sender as Infragistics.Win.Misc.UltraSplitter).BorderStyle = Infragistics.Win.UIElementBorderStyle.None;

            //}

        }

        // 사이즈 조절 가능한 판넬의 최소 사이즈 - Top
        private void pnlBdTDetail_Resize(object sender, EventArgs e)
        {
            if (sender is Infragistics.Win.Misc.UltraPanel)
            {
                if ((sender as Infragistics.Win.Misc.UltraPanel).Name == "pnlBdTDetail")
                {
                    if ((sender as Infragistics.Win.Misc.UltraPanel).Height < pnlBdTDetail_Height &&
                        (sender as Infragistics.Win.Misc.UltraPanel).Height != 0)
                        (sender as Infragistics.Win.Misc.UltraPanel).Height = pnlBdTDetail_Height;
                }

                //pnlMainMenu.Width = this.Width - pnlBdTDetail.Width - 2;

            }
        }

        // 사이즈 조절 가능한 판넬의 최소 사이즈 - TopLeft
        private void pnlBdTDtlOffence_Resize(object sender, EventArgs e)
        {
            if (sender is Infragistics.Win.Misc.UltraPanel)
            {
                if ((sender as Infragistics.Win.Misc.UltraPanel).Name == "pnlBdTDtlOffence")
                {
                    if ((sender as Infragistics.Win.Misc.UltraPanel).Width > (pnlBdTDetail.Width - 520) &&
                        (sender as Infragistics.Win.Misc.UltraPanel).Width != 0)
                        (sender as Infragistics.Win.Misc.UltraPanel).Width = (pnlBdTDetail.Width - 526);
                }

                pnlBdTDtlMenuR.Width = (sender as Infragistics.Win.Misc.UltraPanel).Width - 3;
            }

        }

        //
        private void pnlBdTDtlLocation_Resize(object sender, EventArgs e)
        {

        }

        // ComboBox 의 Dropdown List 초기화
        private void ComboBox_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

            if (sender == null) return;
            if (sender is UltraCombo)
            {

                int iDDWidth = (sender as UltraCombo).Width >= 500 ? (sender as UltraCombo).Width : 500;

                //DropDown size
                (sender as UltraCombo).DropDownWidth = iDDWidth;

                // 일단 모두 숨긴다. - 나중에 컬럼이 추가 되어도 관련된 것만 수정하기 위함
                for (int i = 0; i < e.Layout.Bands[0].Columns.Count; i++)
                {
                    e.Layout.Bands[0].Columns[i].Hidden = true;
                }

                // 필요한 거만 보여준다.
                e.Layout.Bands[0].Columns[0].Hidden = false;
                e.Layout.Bands[0].Columns[1].Hidden = false;


                //if ((sender as UltraCombo).Name == "CbbReference")
                //{

                //    e.Layout.Bands[0].Columns[0].Width = 100;
                //    e.Layout.Bands[0].Columns[1].Width = 264;

                //}
                //else if ((sender as UltraCombo).Name == "CbbLegislation")
                //{

                //    e.Layout.Bands[0].Columns[0].Width = 110;
                //    e.Layout.Bands[0].Columns[1].Width = 810;

                //}

                e.Layout.Bands[0].Columns[0].Width = iDDWidth / 4;
                e.Layout.Bands[0].Columns[1].Width = iDDWidth - e.Layout.Bands[0].Columns[0].Width - 20;
            }


        }

        // 입력 값이 기존 코드에 없는 경우 처리
        private void ComboBox_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraCombo combo = sender as Infragistics.Win.UltraWinGrid.UltraCombo;

            String strCbbName = combo.Name;
            String strName = strCbbName.Substring(3, strCbbName.Length - 3).ToUpper();

            if (DBGrid.Rows.Count > 0)
            {
                MessageBox.Show("Please enter the exact details of " + strName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                combo.Value = null;
                combo.Focus();
            }

        }

        // Court
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

        // Location KeyPress
        private void CbbLocationCd_KeyPress(object sender, KeyPressEventArgs e)
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

                String strInput = CbbLocationCd.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsReference.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsReference.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
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

        //  Combo keypress
        private void CbbReference_KeyPress(object sender, KeyPressEventArgs e)
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

                String strInput = CbbReferenceCd.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsReference.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsReference.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
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

        // Direction Combo keypress
        private void CbbLegislation_KeyPress(object sender, KeyPressEventArgs e)
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

                String strInput = CbbLegislationCd.Text + e.KeyChar;

                try
                {
                    DataRow[] row = dsLegislation.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                    if (row.Length == 0)
                    {
                        row = dsLegislation.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
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

        // 조회 후 사용하도록 메시지 처리
        private void ComboBox_Click(object sender, EventArgs e)
        {
            if (DBGrid.Rows.Count <= 0)
            {
                MessageBox.Show("Please work after inquiry.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLookup.Focus();
            }
        }

        // ComboBox 클릭시 기존 Text 전체 선택
        private void ComboBox_Enter(object sender, EventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraCombo combo = sender as Infragistics.Win.UltraWinGrid.UltraCombo;
            combo.SelectAll();
        }

        // DBGrid Selection Change ---> Display DetailData 
        private void DBGrid_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (DBGrid.Rows.Count <= 0) return;
            //if (pnlBody.Height != pnlBdTMenu.Height)    // 상세 영역이 펼쳐져 있으면 
            //{
            if (DBGrid.ActiveRow == null && DBGrid.Selected.Rows.Count == 0) return;
            if (DBGrid.ActiveRow == null) DBGrid.ActiveRow = DBGrid.Selected.Rows[0];

            Fn_DisplayData(DBGrid.ActiveRow.Index);

            //}
        }

        // 신규 레코드 생성 후 필요한 값을 모두 입력하지 않은 상태에서 다른 작업을 하려하면 막는다.
        private void DBGrid_BeforeSelectChange(object sender, BeforeSelectChangeEventArgs e)
        {
            if (DBGrid.Rows.Count <= 0) return;
            if (DBGrid.ActiveRow == null) return;
            if (DBGrid.Selected.Rows.Count != 1) return;

            DBGrid.BeforeSelectChange -= DBGrid_BeforeSelectChange;
            try
            {
                if (e.Type == typeof(UltraGridRow))
                {
                    // 입력값이 정확한지 확인 
                    String param = String.Empty;
                    int rv = Fn_RecValidate(DBGrid.Selected.Rows[0], ref param);

                    // 필수 항목에 값이 입력되지 않았다.
                    if (rv > 0)
                    {
                        String strMsg = String.Format("[{0}] item is not entered."
                                                    , param
                                                     );

                        // 신규 입력인 경우 입력 취소 문의 
                        if (Convert.ToInt32(DBGrid.Selected.Rows[0].Cells["rec_state"].Value) == (int)iTopsLib.RecState.NEW)
                        {
                            if (MessageBox.Show(strMsg + "\n\nAre you sure you want to cancel\n"
                                                       + "entering the new record?"
                                              , "Information"
                                              , MessageBoxButtons.OKCancel
                                              , MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                DBGrid.Selected.Rows[0].Delete(false);
                            }
                            else
                            {
                                DBGrid.ActiveRow = DBGrid.Selected.Rows[0];
                                e.Cancel = true;
                            }
                        }
                    }
                    // 입력된 값이 규정에 어긋난다.
                    else if (rv < 0)
                    {
                        MessageBox.Show(param, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBGrid.ActiveRow = DBGrid.Selected.Rows[0];
                        e.Cancel = true;
                        pnlBdTDetail.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                DBGrid.BeforeSelectChange += DBGrid_BeforeSelectChange;
            }
        }

        // 레코드 입력 값 확인
        private int Fn_RecValidate(Infragistics.Win.UltraWinGrid.UltraGridRow Row, ref String param)
        {

            param = String.Empty;
            int rv = 0;

            if (Row == null) return rv;

            //// 신규 레코드가 아니면 통과 
            //if (Convert.ToInt32(Row.Cells["rec_state"].Value) != (int)iTopsLib.RecState.NEW) return rv;

            try
            {
                //-------------------------------------------------------------------------//
                // 필수 항목 값이 입력 되어 있는지 확인 
                //-------------------------------------------------------------------------//

                // CbbLocationCd
                if (Convert.ToString(Row.Cells["location"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["location"].Column.Index));
                    param = Convert.ToString(Row.Cells["location"].Column.Header.Caption).ToUpper();
                    CbbLocationCd.Focus();
                }
                // CbbDirection
                else if (Convert.ToString(Row.Cells["direction"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["direction"].Column.Index));
                    param = Convert.ToString(Row.Cells["direction"].Column.Header.Caption).ToUpper();
                    CbbDirection.Focus();
                }
                // CbbReferenceCd
                else if (Convert.ToString(Row.Cells["reference"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["reference"].Column.Index));
                    param = Convert.ToString(Row.Cells["reference"].Column.Header.Caption).ToUpper();
                    CbbReferenceCd.Focus();
                }
                // CbbLegislationCd
                else if (Convert.ToString(Row.Cells["legislation"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["legislation"].Column.Index));
                    param = Convert.ToString(Row.Cells["legislation"].Column.Header.Caption).ToUpper();
                    CbbLegislationCd.Focus();
                }
                // txtCategory
                else if (Convert.ToString(Row.Cells["category"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["category"].Column.Index));
                    param = Convert.ToString(Row.Cells["category"].Column.Header.Caption).ToUpper();
                    txtCategory.Focus();
                }
                // txtMappingDesc
                else if (Convert.ToString(Row.Cells["map_desc"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["catmap_descegory"].Column.Index));
                    param = Convert.ToString(Row.Cells["map_desc"].Column.Header.Caption).ToUpper();
                    txtMappingDesc.Focus();
                }

                //-------------------------------------------------------------------------//
                // Input Value validate
                //-------------------------------------------------------------------------//
                // Mapping 여부 - offence_list 로 판단
                else if (Convert.ToString(Row.Cells["offence_list"].Value) == String.Empty)
                {
                    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["offence_list"].Column.Index));
                    param = "Offence Code not mapped\n\nPlease select an offense code!!!";
                    DBGrid_Offence.Focus();
                }


                // Offence Code Length 는 무조건 5자리 이상
                //else if (Convert.ToString(Row.Cells["offence_cd"].Value).Length < 5)
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["offence_cd"].Column.Index));
                //    param = String.Format("The value of item [{0}] must be 5 digits OR more!!!"
                //                        , Convert.ToString(Row.Cells["offence_cd"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}
                //// Penalty Speed From not null
                //else if (Row.Cells["speed_from"].Value.ToString() == "")
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["speed_from"].Column.Index));
                //    param = String.Format("The value of item [{0}] is not NULL!!!"
                //                        , Convert.ToString(Row.Cells["speed_from"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}
                //// Penalty Speed To not null - Summon Case 제외
                //else if (Convert.ToInt32(Row.Cells["speed_from"].Value) <= Convert.ToInt32(Row.Cells["penalty_max_speed"].Value)
                //      && Row.Cells["speed_to"].Value.ToString() == ""
                //        )
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["speed_to"].Column.Index));
                //    param = String.Format("The value of item [{0}] is not NULL!!!"
                //                        , Convert.ToString(Row.Cells["speed_to"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}
                //// Penalty Speed To is null - Summon Case
                //else if (Convert.ToInt32(Row.Cells["speed_from"].Value) > Convert.ToInt32(Row.Cells["penalty_max_speed"].Value)
                //      && Row.Cells["speed_to"].Value.ToString() != ""
                //        )
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["speed_to"].Column.Index));
                //    param = String.Format("The value of item [{0}] must be NULL!!!"
                //                        , Convert.ToString(Row.Cells["speed_to"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}
                //// Fine - Summon Case
                //else if (Convert.ToInt32(Row.Cells["speed_from"].Value) > Convert.ToInt32(Row.Cells["penalty_max_speed"].Value)
                //      && Row.Cells["speed_to"].Value.ToString() == ""
                //      && Row.Cells["fine"].Value.ToString() != ""
                //        )
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["fine"].Column.Index));
                //    param = String.Format("The value of item [{0}] must be NULL at Summon Case!!!"
                //                        , Convert.ToString(Row.Cells["fine"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}
                //// Fine - Not Summon Case
                //else if (Convert.ToInt32(Row.Cells["speed_from"].Value) <= Convert.ToInt32(Row.Cells["penalty_max_speed"].Value)
                //      && Row.Cells["speed_to"].Value.ToString() != ""
                //      && Row.Cells["fine"].Value.ToString() == ""
                //        )
                //{
                //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["fine"].Column.Index));
                //    param = String.Format("The value of item [{0}] is not NULL!!!"
                //                        , Convert.ToString(Row.Cells["fine"].Column.Header.Caption).ToUpper()
                //                         );
                //    txtOffenceCd.Focus();
                //}

                // Speed 중복 체크 ... 저장시 하자 !!!!


                return rv;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                return rv;
            }
            finally
            {
                //
            }
        }

        // 행추가
        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (DBGrid.DataSource == null)
            {
                MessageBox.Show("Please work after inquiry !!!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Filter 풀어준다.
            Fn_SetGridFiter(null, false);

            // 추가
            UltraGridRow Row = this.DBGrid.DisplayLayout.Bands[0].AddNew();

            Row.Cells["sel_yn"].Value = false;
            Row.Cells["use_yn"].Value = true;
            Row.Cells["rec_state"].Value = iTopsLib.RecState.NEW;

            DBGrid.Selected.Rows.Clear();
            DBGrid.Selected.Rows.Add(Row);
            DBGrid.ActiveRow = Row;

            CbbCourt.Focus();
        }

        // 상단 Detail 의 항목을 편집하면 Grid에 반영하기 
        private void DetailItem_ValueChanged(object sender, EventArgs e)
        {
            if (DBGrid.Rows.Count <= 0) return;
            if (DBGrid.ActiveRow == null) return;

            try
            {
                if (sender is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraTextEditor);
                    if (obj.Value == null)
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
                    else
                    {
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);

                        // 코드변경에 따라 영향 있는 것들 반영하기
                        if (obj.Name == "txtCategory")
                        {

                            // 해당하는 Locatoin Code 읽기
                            Fn_LoadDataDtl();
                        }

                    }
                }
                else if (sender is Infragistics.Win.UltraWinGrid.UltraCombo)
                {
                    var obj = (sender as Infragistics.Win.UltraWinGrid.UltraCombo);
                    DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);

                    // 그리드에 코드값 보여 주기
                    DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag) + 1].Value = Convert.ToString(obj.SelectedRow.Cells[1].Value as String);

                    // 코드변경에 따라 영향 있는 것들 반영하기
                    if (obj.Name == "CbbCourt")
                    {
                        // Court 에 해당하는 Location Code 리스트 만들기
                        String strCourt = Convert.ToString(obj.Value);
                        //if (strCourt != "")
                        //{
                        Fn_SetOnlyLocationWithDB(strCourt);
                        //}

                    }
                    else if (obj.Name == "CbbReferenceCd")
                    {
                        String strCode = Convert.ToString(obj.SelectedRow.Cells["cd_desc"].Value as String);
                        if (strCode != "")
                        {
                            // Reference 에 해당하는 Legislation 리스트 만들기
                            Fn_SetOnlyLegislationWithDB(strCode);

                            if (CbbLegislationCd.Rows.Count > 0)
                            {
                                CbbLegislationCd.SelectedRow = CbbLegislationCd.Rows[0];
                                //nedtSpeedFrom.Value = nedtSpeedFrom.MinValue;
                                //nedtSpeedTo.Value = nedtSpeedTo.MaxValue;

                                // 해당하는 Offence Code 읽기
                                Fn_LoadDataDtl();
                            }
                            else
                            {
                                txtSpeedLimit.Value = "";
                                txtMinPenaltySpeed.Value = "";
                                txtMaxPenaltySpeed.Value = "";
                                //nedtSpeedFrom.MinValue = 0;
                                //nedtSpeedFrom.MaxValue = 0;
                                //nedtSpeedFrom.Value = null;

                                //nedtSpeedTo.MinValue = 0;
                                //nedtSpeedTo.MaxValue = 0;
                                //nedtSpeedTo.Value = null;
                            }
                        }
                    }
                    else if (obj.Name == "CbbLegislationCd")
                    {
                        txtSpeedLimit.Value = Convert.ToString(obj.SelectedRow.Cells["cd_desc"].Value as String);
                        txtMinPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext1"].Value as String);
                        txtMaxPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext2"].Value as String);

                        // 해당하는 Offence Code 읽기
                        Fn_LoadDataDtl();
                    }


                }
                else if (sender is Infragistics.Win.UltraWinEditors.UltraCheckEditor)
                {
                    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCheckEditor);
                    DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = obj.Checked;
                }
                else if (sender is Infragistics.Win.UltraWinEditors.UltraNumericEditor)
                {
                    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraNumericEditor);
                    if (obj.Value.ToString() == "")
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
                    else
                    {

                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
                    }

                }
                else if (sender is Infragistics.Win.UltraWinEditors.UltraCurrencyEditor)
                {
                    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCurrencyEditor);
                    if (obj.Value == 0)
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = "NO AG";
                    else
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
                }
                else if (sender is Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit)
                {
                    var obj = (sender as Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit);
                    if (obj.Value.ToString() == "")
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
                    else
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);

                }

                // record state 변경 ... 삭제된 것은 ???
                if (Convert.ToInt32(DBGrid.ActiveRow.Cells["rec_state"].Value) != (int)iTopsLib.RecState.NEW)
                {
                    DBGrid.ActiveRow.Cells["rec_state"].Value = iTopsLib.RecState.UPDATED;
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

        // 수정중인 자료 DBGrid 반영 후 검증
        private void pnlBdTDtlLeft_Leave(object sender, EventArgs e)
        {

            if (DBGrid.Rows.Count <= 0) return;
            if (DBGrid.ActiveRow == null) return;
            if (DBGrid.Selected.Rows.Count != 1) return;

            BeforeSelectChangeEventArgs event_param = new BeforeSelectChangeEventArgs(typeof(UltraGridRow), null);
            DBGrid_BeforeSelectChange(null, event_param);

        }

        // 수정중인 자료 DBGrid 반영 후 검증 - 그리드에서 수정 중인 Row 선택시 검증을 피하기 위함
        private void DBGrid_MouseEnter(object sender, EventArgs e)
        {
            pnlBdTDetail.Leave -= pnlBdTDtlLeft_Leave;
        }

        // 수정중인 자료 DBGrid 반영 후 검증 - 그리드에서 수정 중인 Row 선택시 검증을 피하기 위함
        private void DBGrid_MouseLeave(object sender, EventArgs e)
        {
            pnlBdTDetail.Leave += pnlBdTDtlLeft_Leave;
        }

        // 재조회 
        private void BtnLookup_Click(object sender, EventArgs e)
        {
            // 변경되 것이 있는지 확인 
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

            //// 코드 읽기
            //Fn_SetCodeWithDB();

            // Data 읽기
            Fn_LoadData();



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

            // Filtering - 삭제된 것만 숨기고 다 보여 준다.
            Fn_SetGridFiter(null, false);

            // 저장 작업 시작
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // 준비 
                String[] strArrCourt = new string[iCnt];
                String[] strArrLocationCd = new string[iCnt];
                String[] strArrDirection = new string[iCnt];
                String[] strArrMapSeq = new string[iCnt];
                String[] strArrReference = new string[iCnt];
                String[] strArrLegislation = new string[iCnt];
                String[] strArrCategory = new string[iCnt];
                String[] strArrMapDesc = new string[iCnt];
                String[] strArrOffence_List = new string[iCnt];

                String[] strArrUse_yn = new string[iCnt];

                String[] strArrCourt_org = new string[iCnt];
                String[] strArrLocationCd_org = new string[iCnt];
                String[] strArrDirection_org = new string[iCnt];
                String[] strArrMapSeq_org = new string[iCnt];

                String[] strArrRecState = new string[iCnt];

                String[] strArrResult = new string[iCnt];

                int iPos = -1;

                foreach (UltraGridRow row in DBGrid.Rows)
                {
                    // 변경 사항이 없으면 Skip
                    if (!Fn_IsChangedRow(row)) continue;

                    iPos++;

                    strArrCourt[iPos] = Convert.ToString(row.Cells["court"].Value);
                    strArrLocationCd[iPos] = Convert.ToString(row.Cells["location"].Value);
                    strArrDirection[iPos] = Convert.ToString(row.Cells["direction"].Value);
                    strArrMapSeq[iPos] = Convert.ToString(row.Cells["map_seq"].Value);
                    strArrReference[iPos] = Convert.ToString(row.Cells["reference"].Value);
                    strArrLegislation[iPos] = Convert.ToString(row.Cells["legislation"].Value);
                    strArrCategory[iPos] = Convert.ToString(row.Cells["category"].Value);
                    strArrMapDesc[iPos] = Convert.ToString(row.Cells["map_desc"].Value);
                    strArrOffence_List[iPos] = Convert.ToString(row.Cells["offence_list"].Value);

                    strArrUse_yn[iPos] = Convert.ToString(row.Cells["use_yn"].Value);

                    strArrCourt_org[iPos] = Convert.ToString(row.Cells["court_org"].Value);
                    strArrLocationCd_org[iPos] = Convert.ToString(row.Cells["location_org"].Value);
                    strArrDirection_org[iPos] = Convert.ToString(row.Cells["direction_org"].Value);
                    strArrMapSeq_org[iPos] = Convert.ToString(row.Cells["map_seq_org"].Value);

                    if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.NEW) strArrRecState[iPos] = "I";
                    else if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.UPDATED) strArrRecState[iPos] = "U";
                    else if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.DELETED) strArrRecState[iPos] = "D";
                    else strArrRecState[iPos] = "";
                }

                // 저장 
                int rv = iTopsLib.Lib.GFn_InsertLocationMap(strArrCourt
                                                          , strArrLocationCd
                                                          , strArrDirection
                                                          , strArrMapSeq
                                                          , strArrReference
                                                          , strArrLegislation
                                                          , strArrCategory
                                                          , strArrMapDesc
                                                          , strArrOffence_List
                                                          , strArrUse_yn
                                                          , strArrCourt_org
                                                          , strArrLocationCd_org
                                                          , strArrDirection_org
                                                          , strArrMapSeq_org
                                                          , strArrRecState
                                                          , ref strArrResult
                                                           );

                // 실패 내역이 있다.
                if (rv != iCnt)
                {
                    //
                    MessageBox.Show(String.Format("There is an error in the DB operation.\n"
                                                + "{0} Data Processing failed\n\n"
                                                + "Check for duplicate input data!!!"
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

        // 변경여부 확인 - 전체
        private int Fn_IsChanged()
        {
            int iCnt = 0;
            foreach (UltraGridRow row in DBGrid.Rows)
            {
                if (!Fn_IsChangedRow(row)) continue;
                iCnt++;
            }

            return iCnt;
        }

        // 변경여부 확인 - one row
        private bool Fn_IsChangedRow(UltraGridRow row)
        {
            // 상태가 변했으면 수정된 것
            if (Convert.ToChar(row.Cells["rec_state"].Value) != Convert.ToChar(iTopsLib.RecState.NORMAL)) return true;

            // 변경 사항이 없으면 Skip
            if ((Convert.ToString(row.Cells["court"].Value)                        // Offence Code
                == Convert.ToString(row.Cells["court_org"].Value)) &&
                (Convert.ToString(row.Cells["location"].Value)                      // Reference Code
                == Convert.ToString(row.Cells["location_org"].Value)) &&
                (Convert.ToString(row.Cells["direction"].Value)           // Relevant Legislation Code
                == Convert.ToString(row.Cells["direction_org"].Value)) &&
                (Convert.ToString(row.Cells["map_seq"].Value)                          // Category
                == Convert.ToString(row.Cells["map_seq_org"].Value)) &&
                (Convert.ToString(row.Cells["reference"].Value)                            // Use YN
                == Convert.ToString(row.Cells["reference_org"].Value)) &&
                (Convert.ToString(row.Cells["legislation"].Value)                        // 
                == Convert.ToString(row.Cells["legislation_org"].Value)) &&
                (Convert.ToString(row.Cells["category"].Value)                          // 
                == Convert.ToString(row.Cells["category_org"].Value)) &&
                (Convert.ToString(row.Cells["map_desc"].Value)                          // 
                == Convert.ToString(row.Cells["map_desc_org"].Value)) &&
                (Convert.ToString(row.Cells["offence_list"].Value)                          // 
                == Convert.ToString(row.Cells["offence_list_org"].Value)) &&
                (Convert.ToString(row.Cells["use_yn"].Value)                          // 
                == Convert.ToString(row.Cells["use_yn_org"].Value))
                ) return false;
            else return true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (DBGrid.DataSource == null)
            {
                MessageBox.Show("Please work after inquiry !!!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Filter 풀어준다.
            Fn_SetGridFiter(null, false);

            // 수정된 값이 반영되도록 이벤트 발생 

            // 선택( 체크) 된 것이 있는지 확인 
            int iCnt = 0;
            foreach (UltraGridRow Row in DBGrid.Rows)
            {
                if ((bool)Row.Cells["sel_yn"].Value == true)
                {
                    iCnt++;
                }
            }

            if (iCnt == 0)
            {
                MessageBox.Show("Please work after selecting the data to distribute!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DBGrid.BeforeSelectChange -= DBGrid_BeforeSelectChange;
            DBGrid.BeginUpdate();
            iCnt = 0;
            try
            {
                // 기존 ActiveRow 보관
                int iActiveIndex = DBGrid.ActiveRow.Index;

                //foreach (UltraGridRow Row in DBGrid.Rows)
                for (int i = DBGrid.Rows.Count - 1; i >= 0; i--)
                {

                    //UltraGridRow Row = DBGrid.Rows[i];
                    if ((bool)DBGrid.Rows[i].Cells["sel_yn"].Value == true)
                    {
                        // 
                        if (i == iActiveIndex) iActiveIndex--;

                        // 신규 입력은 삭제
                        if (Convert.ToInt32(DBGrid.Rows[i].Cells["rec_state"].Value) == (int)iTopsLib.RecState.NEW)
                        {
                            DBGrid.Rows[i].Delete(false);
                        }
                        // 기존 자료는 레코드 상태 변경 
                        else
                        {
                            DBGrid.Rows[i].Cells["rec_state"].Value = iTopsLib.RecState.DELETED;
                            DBGrid.Rows[i].Cells["sel_yn"].Value = false;
                        }

                        iCnt++;
                    }
                }

                // ActiveRow 지정
                if (iActiveIndex >= 0)
                {
                    DBGrid.ActiveRow = DBGrid.Rows[iActiveIndex];
                    DBGrid.Selected.Rows.Clear();
                    DBGrid.Selected.Rows.Add(DBGrid.Rows[iActiveIndex]);
                }
                else
                {
                    foreach (UltraGridRow Row in DBGrid.Rows)
                    {
                        if ((int)Row.Cells["rec_state"].Value != (int)iTopsLib.RecState.DELETED)
                        {
                            DBGrid.ActiveRow = Row;
                            DBGrid.Selected.Rows.Clear();
                            DBGrid.Selected.Rows.Add(Row);
                            break;
                        }
                    }

                }


                // Filtering 
                Fn_SetGridFiter(null, false);


            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                DBGrid.EndUpdate();
                DBGrid.BeforeSelectChange += DBGrid_BeforeSelectChange;

                MessageBox.Show(String.Format("[{0}] Rows deleted!!", iCnt), "Information"
                              , MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


        // 작업 내용 취소
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // 조호하지 않은 경우
            if (DBGrid.DataSource == null)
            {
                MessageBox.Show("Please work after inquiry !!!", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 입력범위를 벗어나도 됨
            pnlBdTDetail.Leave -= pnlBdTDetail_Leave;
            try
            {
                if (DBGrid.ActiveRow == null) return;

                // 
                // 변한게 없으면 메시지 
                if (Convert.ToInt32(DBGrid.ActiveRow.Cells["rec_state"].Value) == (int)iTopsLib.RecState.NORMAL)
                {
                    MessageBox.Show("This Row is not Changed!!", "Information"
                                  , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                // 신규는 삭제
                else if (Convert.ToInt32(DBGrid.ActiveRow.Cells["rec_state"].Value) == (int)iTopsLib.RecState.NEW)
                {
                    int iRow = DBGrid.ActiveRow.Index;
                    DBGrid.BeforeSelectChange -= DBGrid_BeforeSelectChange;
                    try
                    {
                        DBGrid.ActiveRow.Delete(false);
                        if (iRow > 0)
                        {

                            DBGrid.ActiveRow = DBGrid.Rows[iRow - 1];
                            DBGrid.Selected.Rows.Clear();
                            DBGrid.Selected.Rows.Add(DBGrid.ActiveRow);
                        }
                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;
                    }
                    finally
                    {
                        DBGrid.BeforeSelectChange += DBGrid_BeforeSelectChange;

                    }

                }
                // 아니면 DB 값으로 ....
                else
                {
                    // 상단을 원 복하면 valuechange에 의해 그리드에 반영된다.
                    CbbCourt.Value = DBGrid.ActiveRow.Cells["court_org"].Value;
                    CbbLocationCd.Value = DBGrid.ActiveRow.Cells["location_org"].Value;
                    CbbDirection.Value = DBGrid.ActiveRow.Cells["direction_org"].Value;
                    CbbReferenceCd.Value = DBGrid.ActiveRow.Cells["reference_org"].Value;
                    CbbLegislationCd.Value = DBGrid.ActiveRow.Cells["legislation_org"].Value;
                    txtCategory.Value = DBGrid.ActiveRow.Cells["category_org"].Value;
                    txtMappingDesc.Value = DBGrid.ActiveRow.Cells["map_desc_org"].Value;
                    chkUseYN.Checked = (bool)DBGrid.ActiveRow.Cells["use_yn_org"].Value;

                    // 레코드 상태 원복
                    DBGrid.ActiveRow.Cells["rec_state"].Value = iTopsLib.RecState.NORMAL;


                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {
                // 입력범위를 벗어나지 못하게 함
                pnlBdTDetail.Leave += pnlBdTDetail_Leave;

            }

        }

        // 종료 전에 수정중인 자료가 있는지 확인
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 변경되 것이 있는지 확인 
            int iCnt = Fn_IsChanged();
            if (iCnt > 0)
            {
                if (MessageBox.Show("Changed data exists!!\n" +
                        "Do you ignore the changes?\n\n" +
                        "Yes : Lookup / No : Cancel Lookup", "Question"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        }

        // 수정중인 자료 DBGrid 반영 후 검증
        private void pnlBdTDetail_Leave(object sender, EventArgs e)
        {
            if (DBGrid.Rows.Count <= 0) return;
            if (DBGrid.ActiveRow == null) return;
            if (DBGrid.Selected.Rows.Count != 1) return;

            // 어디로 포커스가 가려는지 찾는다.
            // 입력 검증 없이 가도 되는 곳이면 보내 준다.
            // DBGrid 는 자신의 Event 처리에 맡기고 
            // 나머지컨트롤은 가도 되는지 확인 후 처리한다.
            Control ctrl = iTopsLib.Lib.FindControlAtCursor(this);
            if (ctrl == null) return;

            if (ctrl.Name != "DBGrid")
            {
                bool bCanLeave = false;

                // 입력 취소 가능하게
                if (ctrl.Name == "BtnCancel") bCanLeave = true;

                // 그냥 가면 안되는 컨트롤은 입력중인 데이타 검증 후 보낸다.
                if (!bCanLeave)
                {
                    // 다른 입력중인 값의 입력값을 확인하도록 한다.
                    BeforeSelectChangeEventArgs event_param = new BeforeSelectChangeEventArgs(typeof(UltraGridRow), null);
                    DBGrid_BeforeSelectChange(null, event_param);

                }

            }
        }


        // Grid Filter 설정/해제 ... 삭제된 것은 무조건 숨김
        private void Fn_SetGridFiter(UltraGridRow Row, bool value = false)
        {
            if (DBGrid.Rows.Count <= 0) return;

            // 삭제 숨기기
            UltraGridColumn Col = DBGrid.DisplayLayout.Bands[0].Columns["rec_state"];
            ColumnFilter FilterCol = Col.Band.ColumnFilters[Col];
            FilterCol.ClearFilterConditions();
            FilterCol.FilterConditions.Add(FilterComparisionOperator.NotEquals, Convert.ToChar(iTopsLib.RecState.DELETED));

            //// 선택한 Court
            //UltraGridColumn COURT_Col = DBGrid.DisplayLayout.Bands[0].Columns["court"];
            //ColumnFilter COURT_FilterCol = COURT_Col.Band.ColumnFilters[COURT_Col];
            //COURT_FilterCol.ClearFilterConditions();
            //if (value)
            //    COURT_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["court"].Value));

            ////// RF
            ////UltraGridColumn RF_Col = DBGrid.DisplayLayout.Bands[0].Columns["reference_cd"];
            ////ColumnFilter RF_FilterCol = RF_Col.Band.ColumnFilters[RF_Col];
            ////RF_FilterCol.ClearFilterConditions();
            ////if (value)
            ////    RF_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["reference_cd"].Value));

            ////// RL
            ////UltraGridColumn RL_Col = DBGrid.DisplayLayout.Bands[0].Columns["relevant_legislation_cd"];
            ////ColumnFilter RL_FilterCol = RL_Col.Band.ColumnFilters[RL_Col];
            ////RL_FilterCol.ClearFilterConditions();
            ////if (value)
            ////    RL_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["relevant_legislation_cd"].Value));

            ////// CT
            ////UltraGridColumn CT_Col = DBGrid.DisplayLayout.Bands[0].Columns["category"];
            ////ColumnFilter CT_FilterCol = CT_Col.Band.ColumnFilters[CT_Col];
            ////CT_FilterCol.ClearFilterConditions();
            ////if (value)
            ////    CT_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["category"].Value));

        }

        // 전체 선택/해제
        private void chkbxSel_All_CheckedChanged(object sender, EventArgs e)
        {
            bool bSel = chkbxSel_All.Checked;

            foreach (UltraGridRow Row in DBGrid.Rows)
                Row.Cells["sel_yn"].Value = bSel;

            DBGrid.Refresh();

        }

        private void chkbxSel_All_D_CheckedChanged(object sender, EventArgs e)
        {

            if (DBGrid.ActiveRow == null) return;

            DBGrid_Offence.AfterCellUpdate -= DBGrid_Offence_AfterCellUpdate;

            try
            {

                bool bSel = chkbxSel_All_D.Checked;
                foreach (UltraGridRow Row in DBGrid_Offence.Rows)
                    Row.Cells["sel_yn"].Value = bSel;

                Fn_GetOffenceList(DBGrid.ActiveRow.Index);
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {

                DBGrid_Offence.Refresh();
            }
            DBGrid_Offence.AfterCellUpdate += DBGrid_Offence_AfterCellUpdate;


        }

        private void DBGrid_Offence_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell == null) return;
            if (DBGrid.ActiveRow == null) return;

            // Row 범위 확인 
            int iRow = e.Cell.Row.Index;
            if (iRow < 0 || iRow >= DBGrid_Offence.Rows.Count) return;

            // 
            Fn_GetOffenceList(DBGrid.ActiveRow.Index);

        }

        private void DBGrid_Offence_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //// Row가 선택되면 무조건 
                //CbbLocationCd.Focus();

                // Mouse 좌측 버튼이 눌린 경우만 처리한다.
                if (e.Button != MouseButtons.Left) return;

                // Shift, Ctrl 등 다른 키를 누르고 Mouse Click 한 경우 System에서 처리하도록 맡긴다.
                bool bKeyNone = !Convert.ToBoolean(ModifierKeys);
                if (!bKeyNone) return;


                // 마우스 클릭한 위치( Cell ) 을 찾는다.
                Infragistics.Win.UIElement element;
                Infragistics.Win.UltraWinGrid.UltraGridCell cell;

                element = DBGrid_Offence.DisplayLayout.UIElement.ElementFromPoint(e.Location);
                cell = element.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell)) as Infragistics.Win.UltraWinGrid.UltraGridCell;

                if (cell == null) return;

                // Row 범위 확인 
                int iRow = cell.Row.Index;
                if (iRow < 0 || iRow >= DBGrid_Offence.Rows.Count) return;

                // Click한 행을 선택 처리
                //DBGrid.Selected.Rows.Clear();
                //DBGrid.Rows[iRow].Selected = true;

                // 선택 체크박스를 클릭한 경우 선택을 반전 시킨다.
                int iCol = cell.Column.Index;
                if (iCol == DBGrid_Offence.DisplayLayout.Bands[0].Columns["sel_yn"].Index)
                {
                    DBGrid_Offence.Rows[iRow].Cells[iCol].Value = !Convert.ToBoolean(DBGrid_Offence.Rows[iRow].Cells[iCol].Value);
                }

                DBGrid_Offence.Rows[iRow].Selected = true;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }


        }

        // 상세 우측의 Offence Code List에 기존에 선택했던 것 체크해 준다.
        private void Fn_SetOffenceList(String ValueList)
        {
            if (ValueList.Trim() == String.Empty) return;
            try
            {
                if (DBGrid_Offence.Rows.Count <= 0) return;
                for (int iRow = 0; iRow < DBGrid_Offence.Rows.Count; iRow++)
                {
                    DBGrid_Offence.Rows[iRow].Cells["sel_yn"].Value = ValueList.Contains(Convert.ToString(DBGrid_Offence.Rows[iRow].Cells["offence_cd"].Value));
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

        // 상세 우측의 Offence Code List에서 선택한 것들을 합쳐서 반환한다.
        private String Fn_GetOffenceList(int iMainIndex)
        {
            String rv = "";
            try
            {
                if (DBGrid_Offence.Rows.Count <= 0) return rv;
                for (int iRow = 0; iRow < DBGrid_Offence.Rows.Count; iRow++)
                {
                    if ((bool)DBGrid_Offence.Rows[iRow].Cells["sel_yn"].Value)
                        rv += Convert.ToString(DBGrid_Offence.Rows[iRow].Cells["offence_cd"].Value) + ",";
                }

                return rv;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                return rv;
            }
            finally
            {
                // Main Grid 에 넣어준다.
                DBGrid.Rows[iMainIndex].Cells["offence_list"].Value = rv;
            }

        }

        // 
        private void ComboBox_BeforeDropDown(object sender, CancelEventArgs e)
        {
            if (sender == null) return;
            if (sender is UltraCombo)
            {
                if ((sender as UltraCombo).Name == "CbbLocationCd")
                {
                    // Court 에 해당하는 Location Code 리스트 만들기
                    String strCourt = Convert.ToString(CbbCourt.Value);
                    if (strCourt != "")
                        Fn_SetOnlyLocationWithDB(strCourt);
                }
                else if ((sender as UltraCombo).Name == "CbbLegislationCd")
                {
                    // Reference 에 해당하는 Legislation 리스트 만들기

                    String strCode = Convert.ToString(CbbReferenceCd.SelectedRow.Cells["cd_desc"].Value as String);
                    if (strCode != "")
                        Fn_SetOnlyLegislationWithDB(strCode);

                }


            }
        }
    }
}
