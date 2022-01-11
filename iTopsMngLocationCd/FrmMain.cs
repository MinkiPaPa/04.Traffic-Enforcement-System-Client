// UltraWinGrid 제어
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
// 
using iTopsLib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iTopsMngLocationCd
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

            InitializeComponent();

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
                    // 컨트롤 초기 위치, 너비, 높이
                    pnlBdTDetail_Height = pnlBdTDetail.Height;

                    // 
                    Fn_InitControl();

                    // 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
                    if (!Lib.GFn_Login(USER_INFO)) Close();

                    // 코드 읽기
                    Fn_SetCodeWithDB();

                    // Data 읽기
                    Fn_LoadData();

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


                ////
                //Fn_SetUserAccountWithDB("");


            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
            }

        }

        // ComboBox 코드 읽기 
        private void Fn_SetOnlyLegislationWithDB(String strCode)
        {
            //try
            //{

            //    // OFFENCE Legislation
            //    try
            //    {
            //        int rv = iTopsLib.Lib.GFn_GetCodeDS("OFFENCE_RL", strCode, ref dsLevel, true, false);
            //        if (rv < 0) return;
            //        if (rv > 0)
            //        {
            //            CbbLegislation.DataSource = dsLevel.Tables[0];

            //            CbbLegislation.DisplayMember = "display_value";
            //            CbbLegislation.ValueMember = "cd";

            //            CbbLegislation.Text = null;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        String strTmp = ex.Message;
            //    }
            //    finally
            //    {

            //    }

            //}
            //catch (Exception ex)
            //{
            //    String strTmp = ex.Message;
            //}
            //finally
            //{
            //}

        }

        //// 사용자 계정 읽기
        //private void Fn_SetUserAccountWithDB(String strPrsnId)
        //{
        //    try
        //    {

        //        // User Account
        //        try
        //        {
        //            //int rv = iTopsLib.Lib.GFn_GetLoginAccountDS(ref dsUsers, strPrsnId);
        //            int rv = 0;
        //            if (strPrsnId == "")
        //            {
        //                iTopsLib.Lib.GFn_GetLoginAccountDS(ref dsUser_All, strPrsnId);

        //                if (dsUser_All.Tables.Count > 0)
        //                {

        //                    //DBGridUser.BeginUpdate();
        //                    try
        //                    {
        //                        DBGridUser.DataSource = dsUser_All.Tables[0];

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        String strTmp = ex.Message;
        //                        rv = -1;
        //                    }
        //                    finally
        //                    {
        //                        //DBGridUser.EndUpdate();

        //                    }


        //                    //DBGridUser.Rows[0].Selected = true;
        //                    //DBGridUser.Refresh();

        //                }

        //            }
        //            else
        //            {
        //                iTopsLib.Lib.GFn_GetLoginAccountDS(ref dsUsers, strPrsnId);

        //                if (rv < 0) return;
        //                if (rv > 0)
        //                {
        //                    CbbAccountId.DataSource = dsUsers.Tables[0];

        //                    CbbAccountId.DisplayMember = "user_id";
        //                    CbbAccountId.ValueMember = "user_id";

        //                    CbbAccountId.Text = null;
        //                }

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            String strTmp = ex.Message;
        //        }
        //        finally
        //        {

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        String strTmp = ex.Message;
        //    }
        //    finally
        //    {
        //    }

        //}

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
                int rv = iTopsLib.Lib.GFn_GetPersonsDS(ref dsPersons, false);

                // 자료 없음
                if (rv == 0)
                {
                    MessageBox.Show("No data available!!", "Information"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return rv;
                }

                if (rv > 0)
                {
                    //Fn_SetEnable(true);

                }

                if (dsPersons.Tables.Count > 0)
                {

                    DBGrid.BeginUpdate();
                    try
                    {
                        DBGrid.DataSource = dsPersons.Tables[0];

                        // 디자인 등등
                        if (bIsStart)
                        {
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


                    DBGrid.Rows[0].Selected = true;
                    DBGrid.Refresh();

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
                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["name"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["mobile"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["phone"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["email"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["addr"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["user_id"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw"].Hidden = false;
                //DBGrid.DisplayLayout.Bands[0].Columns["seq"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["level"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn"].Hidden = false;
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt"].Hidden = false;

                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["name_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["mobile_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["phone_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["email_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["addr_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["user_id_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["level_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn_org"].Hidden = true;
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt_org"].Hidden = true;

                DBGrid.DisplayLayout.Bands[0].Columns["rec_state"].Hidden = true;

                // 고정 컬럼
                DBGrid.DisplayLayout.UseFixedHeaders = true;
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Fixed = true;

                //// Currency
                //DBGrid.DisplayLayout.Bands[0].Columns[""].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;

                //// Date & time format
                //DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                //DBGrid.DisplayLayout.Bands[0].Columns["create_dtm"].Format = "yyyy-MM-dd HH:mm:ss";

                //DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                //DBGrid.DisplayLayout.Bands[0].Columns["chg_dtm"].Format = "yyyy-MM-dd HH:mm:ss";


                // 정렬
                for (int iCol = 0; iCol < DBGrid.DisplayLayout.Bands[0].Columns.Count; iCol++)
                {
                    // Header 가운데 정렬
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextHAlign = HAlign.Center;
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].Header.Appearance.TextVAlign = VAlign.Middle;

                    // 좌측정렬
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Left;
                    DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextVAlign = VAlign.Middle;

                    // 우측정렬
                    if (iCol == DBGrid.DisplayLayout.Bands[0].Columns["level"].Index ||
                        iCol == DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt"].Index)
                    {
                        DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Right;
                    }
                    // 중앙정렬
                    else if (iCol == DBGrid.DisplayLayout.Bands[0].Columns["user_yn"].Index ||
                             iCol == DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn"].Index ||
                             iCol == DBGrid.DisplayLayout.Bands[0].Columns["role_tp"].Index
                            )
                    {
                        //
                        DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextHAlign = HAlign.Center;
                        //DBGrid.DisplayLayout.Bands[0].Columns[iCol].CellAppearance.TextVAlign = VAlign.Middle;
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
                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id"].Width = 140;
                DBGrid.DisplayLayout.Bands[0].Columns["name"].Width = 200;
                DBGrid.DisplayLayout.Bands[0].Columns["mobile"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["phone"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["email"].Width = 150;
                DBGrid.DisplayLayout.Bands[0].Columns["addr"].Width = 200;
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn"].Width = 90;
                DBGrid.DisplayLayout.Bands[0].Columns["user_id"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["level"].Width = 70;
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn"].Width = 90;
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt"].Width = 100;

                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["name_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["mobile_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["phone_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["email_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["addr_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["user_id_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["level_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt_org"].Width = 100;
                DBGrid.DisplayLayout.Bands[0].Columns["rec_state"].Width = 100;


                // 컬럼 제목
                DBGrid.DisplayLayout.Bands[0].Columns["sel_yn"].Header.Caption = "";
                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id"].Header.Caption = "Resident Number";
                DBGrid.DisplayLayout.Bands[0].Columns["name"].Header.Caption = "Name";
                DBGrid.DisplayLayout.Bands[0].Columns["mobile"].Header.Caption = "Mobile";
                DBGrid.DisplayLayout.Bands[0].Columns["phone"].Header.Caption = "Phone";
                DBGrid.DisplayLayout.Bands[0].Columns["email"].Header.Caption = "eMail";
                DBGrid.DisplayLayout.Bands[0].Columns["addr"].Header.Caption = "Address";
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn"].Header.Caption = "User YN";
                DBGrid.DisplayLayout.Bands[0].Columns["user_id"].Header.Caption = "Account";
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw"].Header.Caption = "Pass Word";
                DBGrid.DisplayLayout.Bands[0].Columns["level"].Header.Caption = "Level";
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp"].Header.Caption = "Role Type";
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn"].Header.Caption = "Allowed";
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt"].Header.Caption = "Fail Count";

                DBGrid.DisplayLayout.Bands[0].Columns["prsn_id_org"].Header.Caption = "prsn_id_org";
                DBGrid.DisplayLayout.Bands[0].Columns["name_org"].Header.Caption = "name_org";
                DBGrid.DisplayLayout.Bands[0].Columns["mobile_org"].Header.Caption = "mobile_org";
                DBGrid.DisplayLayout.Bands[0].Columns["phone_org"].Header.Caption = "phone_org";
                DBGrid.DisplayLayout.Bands[0].Columns["email_org"].Header.Caption = "email_org";
                DBGrid.DisplayLayout.Bands[0].Columns["addr_org"].Header.Caption = "addr_org";
                DBGrid.DisplayLayout.Bands[0].Columns["user_yn_org"].Header.Caption = "user_yn_org";
                DBGrid.DisplayLayout.Bands[0].Columns["user_id_org"].Header.Caption = "user_id_org";
                DBGrid.DisplayLayout.Bands[0].Columns["user_pw_org"].Header.Caption = "user_pw_org";
                DBGrid.DisplayLayout.Bands[0].Columns["level_org"].Header.Caption = "level_org";
                DBGrid.DisplayLayout.Bands[0].Columns["role_tp_org"].Header.Caption = "role_tp_org";
                DBGrid.DisplayLayout.Bands[0].Columns["allowed_yn_org"].Header.Caption = "allowed_yn_org";
                DBGrid.DisplayLayout.Bands[0].Columns["fail_cnt_org"].Header.Caption = "fail_cnt_org";

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
                txtLocationCd.Focus();

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

        // DBGrid의 선택이 변경되면 상세 화면에 보여준다.
        private void Fn_DisplayData(int index)
        {
            txtLocationCd.ValueChanged -= DetailItem_ValueChanged;
            txtLocationDesc.ValueChanged -= DetailItem_ValueChanged;
            txtSpeedLimit.ValueChanged -= DetailItem_ValueChanged;

            chkUse.CheckedChanged -= DetailItem_ValueChanged;


            try
            {
                //// 먼저 사용자의 계정 읽기
                //Fn_SetUserAccountWithDB(txtPrsnId.Text);

                // 값 넣기
                txtLocationCd.Value = Convert.ToString(DBGrid.Rows[index].Cells["prsn_id"].Value);
                txtLocationDesc.Value = Convert.ToString(DBGrid.Rows[index].Cells["name"].Value);
                txtSpeedLimit.Value = Convert.ToString(DBGrid.Rows[index].Cells["mobile"].Value);

                chkUse.Checked = Convert.ToBoolean(DBGrid.Rows[index].Cells["user_yn"].Value);

                CbbCourt.Value = Convert.ToString(DBGrid.Rows[index].Cells["level"].Value);


                // ValueChanged EventHandler를 같이 사용하기 위함
                txtLocationCd.Tag = Convert.ToString(DBGrid.Rows[index].Cells["prsn_id"].Column.Index);
                txtLocationDesc.Tag = Convert.ToString(DBGrid.Rows[index].Cells["name"].Column.Index);
                txtSpeedLimit.Tag = Convert.ToString(DBGrid.Rows[index].Cells["mobile"].Column.Index);

                chkUse.Tag = Convert.ToString(DBGrid.Rows[index].Cells["user_yn"].Column.Index);



                // 수정인 경우 Resident Number, Account 수정 못하게
                txtLocationCd.Enabled = (Convert.ToString(DBGrid.Rows[index].Cells["prsn_id_org"].Value) == String.Empty);

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                txtLocationCd.ValueChanged += DetailItem_ValueChanged;
                txtLocationDesc.ValueChanged += DetailItem_ValueChanged;
                txtSpeedLimit.ValueChanged += DetailItem_ValueChanged;

                chkUse.CheckedChanged += DetailItem_ValueChanged;


            }

        }


        // 마우스가 들어오면 잘 보이도록 색상을 변경
        private void spltDBGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Infragistics.Win.Misc.UltraSplitter)
            {
                (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor = System.Drawing.SystemColors.HotTrack;
                (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor2 = System.Drawing.SystemColors.HotTrack;
                //(sender as Infragistics.Win.Misc.UltraSplitter).BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;

            }
        }

        // 마우스가 나가면 원상복구
        private void spltDBGrid_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Infragistics.Win.Misc.UltraSplitter)
            {
                (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor = System.Drawing.Color.White;
                (sender as Infragistics.Win.Misc.UltraSplitter).Appearance.BackColor2 = System.Drawing.Color.White;
                //(sender as Infragistics.Win.Misc.UltraSplitter).BorderStyle = Infragistics.Win.UIElementBorderStyle.None;

            }

        }

        // 사이즈 조절 가능한 판넬의 최소 사이즈
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

                pnlMainMenu.Width = this.Width - pnlBdTDetail.Width - 2;

            }
        }

        // ComboBox 의 Dropdown List 초기화
        //private void CbbReference_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        //{

        //}
        private void ComboBox_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

            if (sender is UltraCombo)
            {
                // 일단 모두 숨긴다. - 나중에 컬럼이 추가 되어도 관련된 것만 수정하기 위함
                for (int i = 0; i < e.Layout.Bands[0].Columns.Count; i++)
                {
                    e.Layout.Bands[0].Columns[i].Hidden = true;
                }

                // 필요한 거만 보여준다.
                e.Layout.Bands[0].Columns[0].Hidden = false;
                e.Layout.Bands[0].Columns[1].Hidden = false;

                if ((sender as UltraCombo).Name == "CbbReference")
                {

                    e.Layout.Bands[0].Columns[0].Width = 100;
                    e.Layout.Bands[0].Columns[1].Width = 264;

                }
                else if ((sender as UltraCombo).Name == "CbbLegislation")
                {

                    e.Layout.Bands[0].Columns[0].Width = 110;
                    e.Layout.Bands[0].Columns[1].Width = 810;

                }

            }


        }

        // 입력 값이 기존 코드에 없는 경우 처리
        //private void CbbReference_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        //{

        //}
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

        //  Combo keypress
        //private void CbbReference_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}
        private void CbbAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        // Direction Combo keypress
        private void CbbLegislation_KeyPress(object sender, KeyPressEventArgs e)
        {
            //// 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            //// space - 32
            //// ~     - 126
            //if (e.KeyChar == '\r') // enter
            //{
            //    this.SelectNextControl(sender as Control, true, true, true, true);
            //}
            //else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            //{

            //    String strInput = CbbLegislation.Text + e.KeyChar;

            //    try
            //    {
            //        DataRow[] row = dsLevel.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
            //        if (row.Length == 0)
            //        {
            //            row = dsLevel.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
            //            if (row.Length == 0)
            //            {
            //                e.KeyChar = (char)0;    // 입력 취소
            //                Lib.Beep(1000, 20);     // Beep
            //                return;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        String strTmp = ex.Message;

            //        e.KeyChar = (char)0;    // 입력 취소
            //        Lib.Beep(1000, 20);     // Beep
            //    }
            //}
            //else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            //{
            //    e.KeyChar = (char)0;    // 입력 취소
            //    Lib.Beep(1000, 20);     // Beep
            //}

        }

        // 조회 후 사용하도록 메시지 처리
        //private void CbbReference_Click(object sender, EventArgs e)
        //{

        //}
        private void ComboBox_Click(object sender, EventArgs e)
        {
            if (DBGrid.Rows.Count <= 0)
            {
                MessageBox.Show("Please work after inquiry.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLookup.Focus();
            }

        }

        // ComboBox 클릭시 기존 Text 전체 선택
        //private void CbbReference_Enter(object sender, EventArgs e)
        //{

        //}
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
            if (DBGrid.ActiveRow == null) Fn_DisplayData(DBGrid.Selected.Rows[0].Index);
            else Fn_DisplayData(DBGrid.ActiveRow.Index);

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

            // 신규 레코드가 아니면 통과 
            if (Convert.ToInt32(Row.Cells["rec_state"].Value) != (int)iTopsLib.RecState.NEW) return rv;

            try
            {
                //-------------------------------------------------------------------------//
                // 필수 항목 값이 입력 되어 있는지 확인 
                //-------------------------------------------------------------------------//

                // Resident Number 
                if (Convert.ToString(Row.Cells["prsn_id"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["prsn_id"].Column.Index));
                    param = Convert.ToString(Row.Cells["prsn_id"].Column.Header.Caption).ToUpper();
                    txtLocationCd.Focus();
                }
                // Name
                else if (Convert.ToString(Row.Cells["name"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["name"].Column.Index));
                    param = Convert.ToString(Row.Cells["name"].Column.Header.Caption).ToUpper();
                    txtLocationDesc.Focus();
                }
                // Mobile
                else if (Convert.ToString(Row.Cells["mobile"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["mobile"].Column.Index));
                    param = Convert.ToString(Row.Cells["mobile"].Column.Header.Caption).ToUpper();
                    txtSpeedLimit.Focus();
                }
                // eMail
                else if (Convert.ToString(Row.Cells["email"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["eamil"].Column.Index));
                    param = Convert.ToString(Row.Cells["email"].Column.Header.Caption).ToUpper();
                    //txtEMail.Focus();
                }
                // Address
                else if (Convert.ToString(Row.Cells["addr"].Value) == String.Empty)
                {
                    rv = Convert.ToInt32(Convert.ToString(Row.Cells["addr"].Column.Index));
                    param = Convert.ToString(Row.Cells["addr"].Column.Header.Caption).ToUpper();
                    //txtAddr.Focus();
                }

                //-------------------------------------------------------------------------//
                // Input Value validate
                //-------------------------------------------------------------------------//
                // Resident Number Length 는 무조건 5자리 이상
                else if (Convert.ToString(Row.Cells["prsn_id"].Value).Length < 13)
                {
                    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["prsn_id"].Column.Index));
                    param = String.Format("The value of item [{0}] must be 13 digits OR more!!!"
                                        , Convert.ToString(Row.Cells["prsn_id"].Column.Header.Caption).ToUpper()
                                         );
                    txtLocationCd.Focus();
                }
                // iTops 사용자가 체크된 경우 계정 정보 확인 
                else if (Convert.ToBoolean(Row.Cells["user_yn"].Value))
                {
                    //-------------------------------------------------------------------------//
                    // 입력 여부 확인 
                    //-------------------------------------------------------------------------//
                    // ID
                    if (Convert.ToString(Row.Cells["user_id"].Value) == String.Empty)
                    {
                        rv = Convert.ToInt32(Convert.ToString(Row.Cells["user_id"].Column.Index));
                        param = Convert.ToString(Row.Cells["user_id"].Column.Header.Caption).ToUpper();
                        //txtAccountId.Focus();
                    }
                    // Pass word
                    else if (Convert.ToString(Row.Cells["user_pw"].Value) == String.Empty)
                    {
                        rv = Convert.ToInt32(Convert.ToString(Row.Cells["user_pw"].Column.Index));
                        param = Convert.ToString(Row.Cells["user_pw"].Column.Header.Caption).ToUpper();
                        //txtPassWord.Focus();
                    }
                    // Pass word
                    else if (Convert.ToString(Row.Cells["level"].Value) == String.Empty)
                    {
                        rv = Convert.ToInt32(Convert.ToString(Row.Cells["level"].Column.Index));
                        param = Convert.ToString(Row.Cells["level"].Column.Header.Caption).ToUpper();
                        CbbCourt.Focus();
                    }
                    //-------------------------------------------------------------------------//
                    // 입력 값 검증
                    //-------------------------------------------------------------------------//
                    // Account - ID
                    else if (Convert.ToString(Row.Cells["user_id"].Value).Length < 7)
                    {
                        rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["user_id"].Column.Index));
                        param = String.Format("The value of item [{0}] must be 7 digits OR more!!!"
                                            , Convert.ToString(Row.Cells["user_id"].Column.Header.Caption).ToUpper()
                                             );
                        //txtAccountId.Focus();
                    }
                    // Account - Password
                    else if (Convert.ToString(Row.Cells["user_pw"].Value).Length < 7)
                    {
                        rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["user_pw"].Column.Index));
                        param = String.Format("The value of item [{0}] must be 8 digits OR more!!!"
                                            , Convert.ToString(Row.Cells["user_pw"].Column.Header.Caption).ToUpper()
                                             );
                        //txtPassWord.Focus();
                    }
                    //// Account - Level
                    //else if (Convert.ToString(Row.Cells["level"].Value) == "")
                    //{
                    //    rv = -1 * Convert.ToInt32(Convert.ToString(Row.Cells["level"].Column.Index));
                    //    param = String.Format("The value of item [{0}] is not selected!!!"
                    //                        , Convert.ToString(Row.Cells["level"].Column.Header.Caption).ToUpper()
                    //                         );
                    //    CbbUserLevel.Focus();
                    //}
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
                //
            }
        }

        // 행추가
        private void BtnNew_Click(object sender, EventArgs e)
        {
            // Filter 풀어준다.
            Fn_SetGridFiter(null, false);

            // 추가
            UltraGridRow Row = this.DBGrid.DisplayLayout.Bands[0].AddNew();

            Row.Cells["sel_yn"].Value = false;
            Row.Cells["user_yn"].Value = false;
            Row.Cells["user_yn_org"].Value = false;
            Row.Cells["allowed_yn"].Value = false;
            Row.Cells["allowed_yn_org"].Value = false;
            Row.Cells["rec_state"].Value = iTopsLib.RecState.NEW;

            DBGrid.Selected.Rows.Clear();
            DBGrid.Selected.Rows.Add(Row);
            DBGrid.ActiveRow = Row;

            txtLocationCd.Focus();
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
                        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);
                }
                else if (sender is Infragistics.Win.UltraWinGrid.UltraCombo)
                {
                    var obj = (sender as Infragistics.Win.UltraWinGrid.UltraCombo);
                    DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToString(obj.Value);

                    //// 그리드에 코드값 보여 주기
                    //DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag) + 1].Value = Convert.ToString(obj.SelectedRow.Cells[1].Value as String);

                    // 코드변경에 따라 영향 있는 것들 반영하기
                    //if (obj.Name == "CbbAccountId")
                    //{
                    //    //String strCode = Convert.ToString(obj.SelectedRow.Cells["user_id"].Value as String);
                    //    //if (strCode != "")
                    //    //{
                    //    //    //// Reference 에 해당하는 Legislation 리스트 만들기
                    //    //    //Fn_SetOnlyLegislationWithDB(strCode);

                    //    //    //if (CbbLegislation.Rows.Count > 0)
                    //    //    //{
                    //    //    //    CbbLegislation.SelectedRow = CbbLegislation.Rows[0];
                    //    //    //    nedtSpeedFrom.Value = nedtSpeedFrom.MinValue;
                    //    //    //    nedtSpeedTo.Value = nedtSpeedTo.MaxValue;
                    //    //    //}
                    //    //    //else
                    //    //    //{
                    //    //    //    txtSpeedLimit.Value = "";
                    //    //    //    txtMinPenaltySpeed.Value = "";
                    //    //    //    txtMaxPenaltySpeed.Value = "";
                    //    //    //    nedtSpeedFrom.MinValue = 0;
                    //    //    //    nedtSpeedFrom.MaxValue = 0;
                    //    //    //    nedtSpeedFrom.Value = null;

                    //    //    //    nedtSpeedTo.MinValue = 0;
                    //    //    //    nedtSpeedTo.MaxValue = 0;
                    //    //    //    nedtSpeedTo.Value = null;
                    //    //    //}

                    //    //}

                    //    txtAccountId.Value = Convert.ToString(obj.SelectedRow.Cells["user_id"].Value as String);
                    //    txtPassWord.Value = Convert.ToString(obj.SelectedRow.Cells["user_pw"].Value as String);
                    //    CbbUserLevel.Value = Convert.ToString(obj.SelectedRow.Cells["level"].Value as String);
                    //    chkAllowed.Checked = Convert.ToBoolean(obj.SelectedRow.Cells["allowed"].Value as String);
                    //    txtFailCnt.Value = Convert.ToString(obj.SelectedRow.Cells["fail_cnt"].Value as String);

                    //}
                    ////else if (obj.Name == "CbbLegislation")
                    ////{
                    ////    txtSpeedLimit.Value = Convert.ToString(obj.SelectedRow.Cells["cd_desc"].Value as String);
                    ////    txtMinPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext1"].Value as String);
                    ////    txtMaxPenaltySpeed.Value = Convert.ToString(obj.SelectedRow.Cells["cd_v_ext2"].Value as String);

                    ////    if (txtMinPenaltySpeed.Text == "")
                    ////    {
                    ////        nedtSpeedFrom.MinValue = 0;
                    ////        nedtSpeedTo.MinValue = 0;
                    ////    }
                    ////    else
                    ////    {
                    ////        nedtSpeedFrom.MinValue = Convert.ToInt32(txtMinPenaltySpeed.Text);
                    ////        nedtSpeedTo.MinValue = Convert.ToInt32(txtMinPenaltySpeed.Text);
                    ////    }

                    ////    if (txtMaxPenaltySpeed.Text == "")
                    ////    {
                    ////        nedtSpeedFrom.MaxValue = 0;
                    ////        nedtSpeedTo.MaxValue = 0;
                    ////    }
                    ////    else
                    ////    {
                    ////        nedtSpeedFrom.MaxValue = Convert.ToInt32(txtMaxPenaltySpeed.Text) + 1;
                    ////        nedtSpeedTo.MaxValue = Convert.ToInt32(txtMaxPenaltySpeed.Text);
                    ////    }
                    ////    nedtSpeedFrom.Value = nedtSpeedFrom.MinValue;
                    ////    nedtSpeedTo.Value = nedtSpeedTo.MaxValue;
                    ////}



                }
                else if (sender is Infragistics.Win.UltraWinEditors.UltraCheckEditor)
                {
                    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCheckEditor);
                    DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = obj.Checked;

                    // 계정 입력 판넬 보여 주기 / 숨기기
                    if (obj.Name == "chkUser")
                    {
                        //grpbxAccount.Visible = obj.Checked;
                    }

                }
                //else if (sender is Infragistics.Win.UltraWinEditors.UltraNumericEditor)
                //{
                //    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraNumericEditor);
                //    if (obj.Value.ToString() == "")
                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
                //    else
                //    {

                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
                //    }

                //}
                //else if (sender is Infragistics.Win.UltraWinEditors.UltraCurrencyEditor)
                //{
                //    var obj = (sender as Infragistics.Win.UltraWinEditors.UltraCurrencyEditor);
                //    if (obj.Value == 0)
                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = "NO AG";
                //    else
                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);
                //}
                //else if (sender is Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit)
                //{
                //    var obj = (sender as Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit);
                //    if (obj.Value.ToString() == "")
                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = DBNull.Value;
                //    else
                //        DBGrid.ActiveRow.Cells[Convert.ToInt32(obj.Tag)].Value = Convert.ToInt32(obj.Value);

                //}

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

            // 코드 읽기
            Fn_SetCodeWithDB();

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
                String[] strArrPrsnId = new String[iCnt];
                String[] strArrName = new String[iCnt];
                String[] strArrMobile = new String[iCnt];
                String[] strArrPhone = new String[iCnt];
                String[] strArrEmail = new String[iCnt];
                String[] strArrAddr = new String[iCnt];

                String[] strArrUserYN = new String[iCnt];
                String[] strArrUserID = new String[iCnt];
                String[] strArrUserPW = new String[iCnt];
                String[] strArrLevel = new String[iCnt];
                String[] strArrRoleTP = new String[iCnt];
                String[] strArrAllowedYN = new String[iCnt];
                String[] strArrFailCnt = new String[iCnt];

                String[] strArrPrsnId_ORG = new String[iCnt];
                String[] strArrUserYN_ORG = new String[iCnt];
                String[] strArrUserID_ORG = new String[iCnt];

                String[] strArrResult = new String[iCnt];

                String[] strArrRecState = new String[iCnt];
                String[] strArrPrsnState = new String[iCnt];
                String[] strArrUserState = new String[iCnt];


                int iPos = -1;

                foreach (UltraGridRow row in DBGrid.Rows)
                {
                    // 변경 사항이 없으면 Skip
                    if (!Fn_IsChangedRow(row)) continue;

                    iPos++;

                    strArrPrsnId[iPos] = Convert.ToString(row.Cells["prsn_id"].Value);
                    strArrName[iPos] = Convert.ToString(row.Cells["name"].Value);
                    strArrMobile[iPos] = Convert.ToString(row.Cells["mobile"].Value);
                    strArrPhone[iPos] = Convert.ToString(row.Cells["phone"].Value);
                    strArrEmail[iPos] = Convert.ToString(row.Cells["email"].Value);
                    strArrAddr[iPos] = Convert.ToString(row.Cells["addr"].Value);

                    strArrUserYN[iPos] = Convert.ToBoolean(row.Cells["user_yn"].Value) == true ? "Y" : "N";
                    strArrUserID[iPos] = Convert.ToString(row.Cells["user_id"].Value);
                    strArrUserPW[iPos] = Convert.ToString(row.Cells["user_pw"].Value);
                    strArrLevel[iPos] = Convert.ToString(row.Cells["level"].Value);
                    strArrRoleTP[iPos] = Convert.ToString(row.Cells["role_tp"].Value);
                    strArrAllowedYN[iPos] = Convert.ToString(row.Cells["allowed_yn"].Value);
                    strArrFailCnt[iPos] = Convert.ToString(row.Cells["fail_cnt"].Value);

                    strArrPrsnId_ORG[iPos] = Convert.ToString(row.Cells["prsn_id_org"].Value);
                    strArrUserYN_ORG[iPos] = Convert.ToBoolean(row.Cells["user_yn_org"].Value) == true ? "Y" : "N";
                    strArrUserID_ORG[iPos] = Convert.ToString(row.Cells["user_id_org"].Value);

                    // 레코드의 변경 사항
                    if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.NEW) strArrRecState[iPos] = "I";
                    else if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.UPDATED) strArrRecState[iPos] = "U";
                    else if (Convert.ToInt32(row.Cells["rec_state"].Value) == (int)iTopsLib.RecState.DELETED) strArrRecState[iPos] = "D";
                    else strArrRecState[iPos] = "";

                    // PERSONS Table 변경 내용
                    if (strArrRecState[iPos] == "U")
                    {
                        if (
                            Convert.ToString(row.Cells["prsn_id"].Value) != Convert.ToString(row.Cells["prsn_id_org"].Value) ||
                            Convert.ToString(row.Cells["name"].Value) != Convert.ToString(row.Cells["name_org"].Value) ||
                            Convert.ToString(row.Cells["mobile"].Value) != Convert.ToString(row.Cells["mobile_org"].Value) ||
                            Convert.ToString(row.Cells["phone"].Value) != Convert.ToString(row.Cells["phone_org"].Value) ||
                            Convert.ToString(row.Cells["email"].Value) != Convert.ToString(row.Cells["email_org"].Value) ||
                            Convert.ToString(row.Cells["addr"].Value) != Convert.ToString(row.Cells["addr_org"].Value)
                           )
                        {
                            strArrPrsnState[iPos] = "U";
                        }
                        else
                            strArrPrsnState[iPos] = "";
                    }
                    else
                    {
                        strArrPrsnState[iPos] = strArrRecState[iPos];
                    }

                    // USERS Tables 변경 내용
                    if (
                        Convert.ToBoolean(row.Cells["user_yn_org"].Value) == false &&
                        Convert.ToBoolean(row.Cells["user_yn"].Value) == true
                       )
                    {
                        strArrUserState[iPos] = "I";
                    }
                    else if (
                             Convert.ToBoolean(row.Cells["user_yn_org"].Value) == true &&
                             Convert.ToBoolean(row.Cells["user_yn"].Value) == false
                            )
                    {
                        strArrUserState[iPos] = "D";
                    }
                    else if (
                             Convert.ToString(row.Cells["user_id"].Value) != Convert.ToString(row.Cells["user_id_org"].Value) ||
                             Convert.ToString(row.Cells["user_pw"].Value) != Convert.ToString(row.Cells["user_pw_org"].Value) ||
                             Convert.ToString(row.Cells["level"].Value) != Convert.ToString(row.Cells["level_org"].Value) ||
                             Convert.ToString(row.Cells["prsn_id"].Value) != Convert.ToString(row.Cells["prsn_id_org"].Value) ||
                             Convert.ToString(row.Cells["role_tp"].Value) != Convert.ToString(row.Cells["role_tp_org"].Value) ||
                             Convert.ToString(row.Cells["allowed_yn"].Value) != Convert.ToString(row.Cells["allowed_yn_org"].Value) ||
                             Convert.ToString(row.Cells["fail_cnt"].Value) != Convert.ToString(row.Cells["fail_cnt_org"].Value)
                            )
                    {
                        strArrUserState[iPos] = "U";
                    }
                    else
                    {
                        strArrUserState[iPos] = "";

                    }


                }

                // 저장
                int rv = iTopsLib.Lib.GFn_InsertPersons(strArrPrsnId
                                                      , strArrName
                                                      , strArrMobile
                                                      , strArrPhone
                                                      , strArrEmail
                                                      , strArrAddr

                                                      , strArrUserYN
                                                      , strArrUserID
                                                      , strArrUserPW
                                                      , strArrLevel
                                                      , strArrRoleTP
                                                      , strArrAllowedYN
                                                      , strArrFailCnt

                                                      , strArrPrsnId_ORG
                                                      , strArrUserYN_ORG
                                                      , strArrUserID_ORG

                                                      , strArrRecState
                                                      , strArrPrsnState
                                                      , strArrUserState

                                                      , ref strArrResult
                                                       );

                // 실패 내역이 있다.
                if (rv != iCnt)
                {
                    // 
                    String strDetail = "";
                    for (int i = 0; i < strArrPrsnId.Length; i++)
                    {
                        if (strArrResult[i] != "OK" && strArrResult[i] != String.Empty)
                            strDetail += String.Format("Person ID [{0}] Result[{1}]\n"
                                                     , strArrPrsnId[i]
                                                     , strArrResult[i]
                                                      );
                    }
                    //
                    MessageBox.Show(String.Format("There is an error in the DB operation.\n"
                                                + "{0} Data Processing failed\n\n"
                                                + "Check your Network or DB Server!!!"
                                                + "\n\n[Detail List]\n{1}\n"
                                                , iCnt - rv
                                                , strDetail)
                                                , "Error"
                                                , MessageBoxButtons.OK
                                                , MessageBoxIcon.Error);

                    Fn_LoadData();      // Data 다시 조회

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
            if ((Convert.ToString(row.Cells["prsn_id"].Value)                        // Offence Code
                == Convert.ToString(row.Cells["prsn_id_org"].Value)) &&
                (Convert.ToString(row.Cells["name"].Value)                      // Reference Code
                == Convert.ToString(row.Cells["name_org"].Value)) &&
                (Convert.ToString(row.Cells["mobile"].Value)           // Relevant Legislation Code
                == Convert.ToString(row.Cells["mobile_org"].Value)) &&
                (Convert.ToString(row.Cells["phone"].Value)                          // Category
                == Convert.ToString(row.Cells["phone_org"].Value)) &&
                (Convert.ToString(row.Cells["user_yn"].Value)                            // Use YN
                == Convert.ToString(row.Cells["user_yn_org"].Value)) &&
                (Convert.ToString(row.Cells["email"].Value)                        // 
                == Convert.ToString(row.Cells["email_org"].Value)) &&
                (Convert.ToString(row.Cells["addr"].Value)                          // 
                == Convert.ToString(row.Cells["addr_org"].Value)) &&
                // 계정 정보
                (Convert.ToString(row.Cells["user_id"].Value)                          // 
                == Convert.ToString(row.Cells["user_id_org"].Value)) &&
                (Convert.ToString(row.Cells["user_pw"].Value)                          // 
                == Convert.ToString(row.Cells["user_pw_org"].Value)) &&
                (Convert.ToString(row.Cells["level"].Value)                          // 
                == Convert.ToString(row.Cells["level_org"].Value)) &&
                (Convert.ToString(row.Cells["allowed_yn"].Value)                          // 
                == Convert.ToString(row.Cells["allowed_yn_org"].Value)) &&
                (Convert.ToString(row.Cells["fail_cnt"].Value)                          // 
                == Convert.ToString(row.Cells["fail_cnt_org"].Value))

                ) return false;
            else return true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
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
                    txtLocationCd.Value = Convert.ToString(DBGrid.ActiveRow.Cells["prsn_id_org"].Value);
                    txtLocationDesc.Value = Convert.ToString(DBGrid.ActiveRow.Cells["name_org"].Value);
                    txtSpeedLimit.Value = Convert.ToString(DBGrid.ActiveRow.Cells["mobile_org"].Value);

                    chkUse.Checked = Convert.ToBoolean(DBGrid.ActiveRow.Cells["user_yn_org"].Value);

                    CbbCourt.Value = Convert.ToString(DBGrid.ActiveRow.Cells["level_org"].Value);


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

            UltraGridColumn Col = DBGrid.DisplayLayout.Bands[0].Columns["rec_state"];
            ColumnFilter FilterCol = Col.Band.ColumnFilters[Col];
            FilterCol.ClearFilterConditions();
            FilterCol.FilterConditions.Add(FilterComparisionOperator.NotEquals, Convert.ToChar(iTopsLib.RecState.DELETED));

            //UltraGridColumn RF_Col = DBGrid.DisplayLayout.Bands[0].Columns["reference_cd"];
            //ColumnFilter RF_FilterCol = RF_Col.Band.ColumnFilters[RF_Col];
            //RF_FilterCol.ClearFilterConditions();
            //if (value)
            //    RF_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["reference_cd"].Value));

            //UltraGridColumn RL_Col = DBGrid.DisplayLayout.Bands[0].Columns["relevant_legislation_cd"];
            //ColumnFilter RL_FilterCol = RL_Col.Band.ColumnFilters[RL_Col];
            //RL_FilterCol.ClearFilterConditions();
            //if (value)
            //    RL_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["relevant_legislation_cd"].Value));

            //UltraGridColumn CT_Col = DBGrid.DisplayLayout.Bands[0].Columns["category"];
            //ColumnFilter CT_FilterCol = CT_Col.Band.ColumnFilters[CT_Col];
            //CT_FilterCol.ClearFilterConditions();
            //if (value)
            //    CT_FilterCol.FilterConditions.Add(FilterComparisionOperator.Equals, Convert.ToString(Row.Cells["category"].Value));

        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // UltraTextEditor 의 Key 입력만 처리
            if (!(sender is Infragistics.Win.UltraWinEditors.UltraTextEditor)) return;

            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {


                String strInput = (sender as Infragistics.Win.UltraWinEditors.UltraTextEditor).Text + e.KeyChar;
                //try
                //{
                //    DataRow[] row = dsLevel.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
                //    if (row.Length == 0)
                //    {
                //        row = dsLevel.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
                //        if (row.Length == 0)
                //        {
                //            e.KeyChar = (char)0;    // 입력 취소
                //            Lib.Beep(1000, 20);     // Beep
                //            return;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    String strTmp = ex.Message;

                //    e.KeyChar = (char)0;    // 입력 취소
                //    Lib.Beep(1000, 20);     // Beep
                //}
            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }

        }

        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // UltraCombo 의 Key 입력만 처리
            if (!(sender is UltraCombo)) return;

            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            {

                String strInput = (sender as UltraCombo).Text + e.KeyChar;

                try
                {
                    // 사용자 등급
                    if ((sender as UltraCombo).Name == "CbbUserLevel")
                    {
                        DataRow[] row = dsCourt.Tables[0].Select(string.Format("level like '*{0}*'", strInput));
                        if (row.Length == 0)
                        {
                            row = dsCourt.Tables[0].Select(string.Format("role_nm like '*{0}*'", strInput));
                            if (row.Length == 0)
                            {
                                e.KeyChar = (char)0;    // 입력 취소
                                Lib.Beep(1000, 20);     // Beep
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;

                    e.KeyChar = (char)0;    // 입력 취소
                    Lib.Beep(1000, 20);     // Beep
                }
                finally
                {
                    //ds.Dispose();
                }
            }
            else if (e.KeyChar != 0x7F && e.KeyChar != 0x08 && e.KeyChar != 0x02 && e.KeyChar != 0x16 && e.KeyChar != 0x18)   // 기타 삭제 
            {
                e.KeyChar = (char)0;    // 입력 취소
                Lib.Beep(1000, 20);     // Beep
            }
        }
    }
}
