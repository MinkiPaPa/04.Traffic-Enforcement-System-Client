/* --------------------------------------------------------------------------
 * Copy Regulation Data in USB to Local Disk
 * and Extract Image using ALPR Algorithm
 * and Upload Image Data to File Server
 * and Insert Regulations Data
 * ------------------------------------------------------------------------*/
//
using iTopsLib;
using System;
// 
using System.IO;
using System.Text;
using System.Windows.Forms;

// Thread

// ALPR

namespace iTopsUpRGLTN
{
    // LTI Text Data - DBLibUpRGLTN.Regulations.cs 와 동일한 것을 갖고 있다. 수정시 같이 수정해 줄 것 
    public enum LvLTI
    {
        CLIP_NAME = 0           // ClipName - textdataW.ClipName
      , CLIP_TYPE            // ClipType - textdataW.ClipType
      , CLIP_NUM             // ClipNumber - textdataW.ClipNumber
      , SYSM_MODE            // SystemMode - textdataW.SystemMode
      , NO_FRAMES            // iNumberOfFrames - textdataW.iNumberOfFrames : int
      , MESRM_FRAME          // iMeasurementFrame - textdataW.iMeasurementFrame : int
      , VHCL_TYPE            // iVehicleType - textdataW.iVehicleType
      , MESRM_SPD            // MeasuredSpeed - textdataW.MeasuredSpeed
      , SPD_LIMIT            // SpeedLimit - textdataW.SpeedLimit
      , CAPTURE_SPD          // CaptureSpeed - textdataW.CaptureSpeed
      , MESRM_DISTC         // MeasuredDistance - textdataW.MeasuredDistance
      , SPD_UNITS           // SpeedUnits - textdataW.SpeedUnits
      , DISTC_UNITS         // DistanceUnits - textdataW.DistanceUnits
      , LANE                // iCurrentLane - textdataW.iCurrentLane : int
      , DBC_SPD2            // dbcSpeed2 - textdataW.MeasuredSpeed2
      , DBC_DISTC2          // dbcDistance2 - textdataW.MeasuredDistance2
      , DBC_TBC             // dbcTBC - textdataW.MeasuredTBC
      , DBC_DBC             // dbcDBC - textdataW.MeasuredDBC
      , DBC_TBM             // dbcTBM - textdataW.MeasuredTBM
      , DBC_RD_OFFS         // dbcRoadOffset - textdataW.MeasuredRoadOffset
      , OP_NAME             // OperatorName - textdataW.OperatorName
      , OP_ID               // OperatorID - textdataW.OperatorID
      , STRT_NAME           // StreetName - textdataW.StreetName
      , STRT_CD             // StreetCode - textdataW.StreetCode
      , CLIP_DATE           // ClipDate - textdataW.ClipDate
      , CLIP_TIME           // ClipTime - textdataW.ClipTime
      , LAST_ALIGN          // LastAligned - textdataW.LastAligned
      , LATITUDE            // Latitude - textdataW.Latitude
      , LONGITUDE           // Longitude - textdataW.Longitude
      , FIRM_VER            // FirmwareVersion - textdataW.FirmwareVersion
      , SERIAL_NO           // SerialNo - textdataW.SerialNo
      , SIGNATURE           // Signature - textdataW.Signature
      , CROSS_XY            // iCrosshairXY - textdataW.iCrosshairX , iCrosshairY : int 
      , IMG_WH 		        // iImageWH - textdataW.iImageWidth , iImageHeight : int

      , DECIPHER_PLATE      // Plate No - ALPR 판독 text
      , DECIPHER_MAKER      // 제조사 - ALPR 판독 text
      , DECIPHER_TYPE       // Type - ALPR 판독 text
      , DECIPHER_COLOR      // Color - ALPR 판독 text
      , DECIPHER_MODEL      // Model - ALPR 판독 text
      , DECIPHER_YEAR       // 년식 - ALPR 판독 text
      , DECIPHER_ORNTT      // 방향 - ALPR 판독 text
    }

    // LTI Text Data - DBLibUpRGLTN.Regulations.cs 와 동일한 것을 갖고 있다. 수정시 같이 수정해 줄 것 
    public enum LvCL
    {
        NAME = 0                //
      , DEVICE               //
      , OP_ID                //
      , DATE                 //
      , TIME                 //
      , MODEL                //
      , UNIT                 //
      , DISTANCE             //
      , SPEED_LIMIT          //
      , SPEED                //
      , LANE                //
      , DIRECTION           //
      , COURT               //
      , LOCATION            //
      , GPS                 //
      , DUMY                //

      , DECIPHER_PLATE      // Plate No - ALPR 판독 text
      , DECIPHER_MAKER      // 제조사 - ALPR 판독 text
      , DECIPHER_TYPE       // Type - ALPR 판독 text
      , DECIPHER_COLOR      // Color - ALPR 판독 text
      , DECIPHER_MODEL      // Model - ALPR 판독 text
      , DECIPHER_YEAR       // 년식 - ALPR 판독 text
      , DECIPHER_ORNTT      // 방향 - ALPR 판독 text
    }

    public partial class FrmMain : Form
    {

        // 접속자 정보
        String USER_INFO = "";

        public FrmMain(String[] args)
        {
            //사용자 정보를 인수로 받는다.
            // 없으면 종료
            if (args.Length < 1)
            {
                Close();
                return;
            }

            //Lib.GFn_Login(args[0]);
            USER_INFO = args[0];

            InitializeComponent();

        }

        //===========================================================//
        // 화면 초기 사이즈 
        //===========================================================//
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 임시로 막음
            //this.Height = this.Height - piPrgbHeight - piResultHeight;

            // 임시로 사용 - DB, FTP 서버 접속
            //iTopsLib.Lib.Fn_init();
            //iTopsLib.Lib.Fn_Login("usr1", "usr1");

            //txtEditorId.Text = iTopsLib.Lib.GetUserId();
            //txtEditorName.Text = iTopsLib.Lib.GetUserName();
            // 접속자 계정으로 DB 접속 - 허가된 사용자 인지 확인 
            if (!Lib.GFn_Login(USER_INFO)) Close();

            txtEditorId.Text = iTopsLib.Lib.GetUserId();
            txtEditorName.Text = iTopsLib.Lib.GetUserName();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {

            Fn_SetCodeCombo();

        }

        //===========================================================//
        //  폴더 선택
        //===========================================================//
        private void BtnSelDir_Click(object sender, EventArgs e)
        {
            lblLookupCondition.Text = "";

            if (FbdDlg.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = FbdDlg.SelectedPath;

                // 선택한 폴더의 파일 List 가져 오기 
                lbFileList.Items.Clear();
                String[] arrFileList = null;
                int file_cnt = iTopsLib.Lib.GetFileList(txtDir.Text, ref arrFileList, false);
                if (file_cnt == 0)
                {
                    MessageBox.Show("The file does not exist.\n\n" +
                                     "Select the folder where the data exists!!!", "Information"
                                   , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                String strFullName = "";
                String strFileName = "";
                String strExt = "";
                for (int i = 0; i < file_cnt; i++)
                {
                    strFullName = arrFileList[i].ToString();
                    strFileName = Path.GetFileName(strFullName);
                    strExt = Path.GetExtension(strFullName);

                    // 장비에 맞는 파일만 갖고 오기 
                    if (RdBtnLti.Checked && strExt.ToUpper() != ".JMX") continue;                        // LTI
                    if (RdBtnCom.Checked && strFileName.ToUpper() != "ENFORCEMENT_DB.TXT") continue;    // Laser COM

                    // 목록 추가
                    lbFileList.Items.Add(arrFileList[i].ToString());
                }

                // 장비에 맞는 Data Folder를 선택했는지 확인
                if (lbFileList.Items.Count <= 0)
                {
                    MessageBox.Show(String.Format("[ {0} ] data file does not exist.\n\n" +
                                                  "Select the folder where [ {0} ] data exists!!!", RdBtnLti.Checked ? RdBtnLti.Text : RdBtnCom.Text)
                                   , "Information"
                                   , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDir.Text = String.Empty;
                    BtnSelDir.Focus();
                }

            }
        }

        //===========================================================//
        // Import Start
        //===========================================================//
        private void BtnImport_Click(object sender, EventArgs e)
        {
            // 작업 조건 보여주기 
            String strDeviceName = RdBtnLti.Checked ? RdBtnLti.Text : (RdBtnCom.Checked ? RdBtnCom.Text : "");

            if (RdBtnLti.Checked)
                lblLookupCondition.Text = String.Format("Device : [{0}] Regulation Image Directory : [{1}] Court : [{2}]"
                                                       , strDeviceName, txtDir.Text, CbbCourt.Text);
            else
                lblLookupCondition.Text = String.Format("Device : [{0}] Regulation Image Directory : [{1}]"
                                                       , strDeviceName, txtDir.Text);

            lblLookupCondition.Refresh();


            lbResult.Items.Clear();

            // Branch 선택여부
            if (Convert.ToString(CbbBranch.Value) == "")
            {
                MessageBox.Show("Select the Branch code!!!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CbbBranch.Focus();

                return;
            }


            // LTI의 경우 Court 입력 여부 확인
            if (RdBtnLti.Checked)
            {
                if (Convert.ToString(CbbCourt.Value) == "")
                {
                    MessageBox.Show("Select the court code!!!", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CbbCourt.Focus();

                    return;
                }
            }

            // 폴더 선택 여부 확인
            int file_cnt = lbFileList.Items.Count;
            if (txtDir.Text.Length == 0 || file_cnt == 0)
            {
                MessageBox.Show("Select the folder where the data exists!!!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BtnSelDir.Focus();

                return;
            }

            //  입력자 확인
            if (txtEditorName.Text.Length == 0)
            {
                MessageBox.Show("Input Worker ID", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtEditorId.Focus();

                return;
            }


            // Import Start
            // 다른 작업 못하게 Button 막기 
            grpbDeviceType.Enabled = false;
            BtnClose.Enabled = false;
            BtnSelDir.Enabled = false;
            BtnImport.Enabled = false;
            grpbDeviceType.Enabled = false;
            CbbBranch.Enabled = false;
            CbbCourt.Enabled = false;

            // 목록 보관할 ListView Clear
            lvLtiText.Items.Clear();
            lvComLaserText.Items.Clear();

            // 커서는 작업중으로 ...
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            //// 작업 상황 보여주기
            //AddStatus("[Start]");
            try
            {
                if (RdBtnLti.Checked) Process_LTI();            // LTI 장비 처리
                else if (RdBtnCom.Checked) Process_ComLaser();  // Com Laser 장비 처리 
                else return;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
            finally
            {
                // Log 기록 ... 할 필요가 있을 듯 !!!



                // 버튼 활성화
                BtnClose.Enabled = true;
                BtnSelDir.Enabled = true;
                BtnImport.Enabled = true;
                grpbDeviceType.Enabled = true;
                CbbBranch.Enabled = true;
                CbbCourt.Enabled = RdBtnLti.Checked;
                grpbDeviceType.Enabled = true;

                this.Cursor = oldCursor;
                //// 작업 상황 보여주기
                //AddStatus("[End]");
                // 종료 메시지
                MessageBox.Show("Successfully Import", "Informaton", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //===========================================================//
        // LTI 장비 작업
        //===========================================================//
        private void Process_LTI()
        {

            // 작업 상황 보여주기
            AddStatus("[Start]");

            try
            {

                // LTI 인 경우 선작업이 필요
                // 파일 분석하여 텍스트 파일과 이미지 추출 파일을 만들어 준다.

                // 작업 상황 보여주기
                AddStatus("    -. LTI Device data !!!");

                // 작업 상황 보여주기
                AddStatus("        -. Data file Read.");

                // 단속 파일 분석 .. 파일명 리스트 만들기 
                String[] arrFiles = new String[lbFileList.Items.Count];
                for (int i = 0; i < lbFileList.Items.Count; i++)
                {
                    arrFiles[i] = lbFileList.Items[i].ToString();
                }

                // 작업 상황 보여주기
                AddStatus("        -. LTI Text Data Analysis Start ...");


                // 단속 장비에서 Text Data 추출 ... 만들어진 파일 리스트 전체 작업 
                int rv = UseLibLTI.LibLTI.AnalysisLtiData(arrFiles
                                                     , ref lvLtiText);
                if (rv == 0)
                {
                    MessageBox.Show("Data does not exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // 작업 상황 보여주기
                AddStatus("        -. LTI Text Data Analysis ... End");


                // 파일 복사 USB ---> .\temp 
                String srcFile = "";
                String destFile = "";
                int rec_cnt = rv;

                // 작업 상황 보여주기
                AddStatus("        -. Start Work.", true);

                //// test
                //rec_cnt = 3;
                for (int i = 0; i < rec_cnt; i++)
                {
                    try
                    {

                        // LTI ListView 와 같이 간다.
                        this.lvLtiText.Items[i].Selected = true;
                        ListViewItem Items = lvLtiText.SelectedItems[i];

                        srcFile = arrFiles[i];
                        destFile = ".\\TEMP\\" + Path.GetFileName(srcFile);

                        //---------------------------------------------//
                        // 기존에 서버에 존재하는 단속 영상인지 확인 ... 파일 이름이 고유하다고 가정 
                        //---------------------------------------------//
                        String strExistFile = Path.GetFileNameWithoutExtension(destFile) + ".jpg";
                        if (iTopsLib.Lib.GFn_GetRegulationImageFile(strExistFile) == strExistFile)
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] {2} already exists.", i + 1, rec_cnt, srcFile), true);
                            continue;
                        }

                        //---------------------------------------------//
                        // 파일 복사 USB ---> .\temp 
                        //---------------------------------------------//
                        FileStream fsSrc = null;
                        FileStream fsDest = null;
                        try
                        {
                            if (File.Exists(srcFile))
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk Copy Start", i + 1, rec_cnt));

                                fsSrc = new FileStream(srcFile, FileMode.Open, FileAccess.Read);         //소스 파일 스트림 개체 생성
                                fsDest = new FileStream(destFile, FileMode.Create, FileAccess.Write);    //목적지 파일 스트림 개체 생성
                                byte[] bts = new byte[4096];
                                int cnt = 0;
                                int mok = (int)(fsSrc.Length / 4096) - 1;
                                int nam = (int)(fsSrc.Length % 4096);
                                while (true)
                                {
                                    //Thread.Sleep(10);
                                    bts = new byte[4096];

                                    if (cnt <= mok)
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, 4096);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, 4096);

                                    }
                                    else // 마지막
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, nam);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, nam);

                                        break;
                                    }
                                    cnt++;

                                }

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk Copy End", i + 1, rec_cnt));

                            }
                            else
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk Copy Fail - Not Exist", i + 1, rec_cnt));

                            }
                        }
                        finally
                        {
                            if (fsDest != null) fsDest.Close();
                            if (fsSrc != null) fsSrc.Close();
                        }

                        //---------------------------------------------//
                        // 번호 판독에 사용할  이미지 추출 Save JPEG
                        //---------------------------------------------//
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Extract Image Start", i + 1, rec_cnt));

                        UseLibLTI.LibLTI.ExtractImage(destFile, ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destFile) + ".jpg");

                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Extract Image End", i + 1, rec_cnt));


                        //// 복사 후 USB File 삭제
                        //FileInfo fi = new FileInfo(srcFile);
                        //fi.Delete();

                        //---------------------------------------------//
                        // 번호판 판독 ALPR Lib
                        //---------------------------------------------//
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR Start", i + 1, rec_cnt));

                        bool bDecipherPlate = false;
                        bool bPlateImage = false;

                        try
                        {

                            //picPlate
                            String strFIle = ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destFile) + ".jpg";
                            String strPlateFile = ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destFile) + "_P.jpg";




                            // 전달 받을 결과 
                            String strPlate = "";
                            picPlate.Image = null;

                            String strMake = "";
                            String strColor = "";
                            String strModel = "";
                            String strType = "";
                            String strYear = "";
                            String strOrientation = "";

                            // 이미지 추출 및 번호 판 판독 
                            bDecipherPlate = UseLibALPR.LibALPR.ProcessImage(strFIle, iTopsLib.Lib.GetRegion()
                                                                            , ref strPlate
                                                                            , ref picPlate
                                                                            , ref strMake
                                                                            , ref strColor
                                                                            , ref strModel
                                                                            , ref strType
                                                                            , ref strYear
                                                                            , ref strOrientation
                                                                            );

                            // 번호판 이미지 추출 성공 여부 
                            bPlateImage = (picPlate.Image != null);

                            if (bDecipherPlate && bPlateImage)
                            {
                                // 추출된 번호판 이미지 저장 
                                picPlate.Image.Save(strPlateFile, System.Drawing.Imaging.ImageFormat.Jpeg);

                                // 추출된 번호판 보관
                                Items.SubItems[(int)LvLTI.DECIPHER_PLATE].Text = strPlate;

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR End - [{2}]", i + 1, rec_cnt, strPlate));
                            }
                            else
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR End - [{2}]", i + 1, rec_cnt, "FAIL"));

                            }
                            picPlate.Refresh();

                            // 차량 정보 보관
                            Items.SubItems[(int)LvLTI.DECIPHER_PLATE].Text = strPlate;
                            Items.SubItems[(int)LvLTI.DECIPHER_MAKER].Text = strMake;
                            Items.SubItems[(int)LvLTI.DECIPHER_COLOR].Text = strColor;
                            Items.SubItems[(int)LvLTI.DECIPHER_MODEL].Text = strModel;
                            Items.SubItems[(int)LvLTI.DECIPHER_TYPE].Text = strType;
                            Items.SubItems[(int)LvLTI.DECIPHER_YEAR].Text = strYear;
                            Items.SubItems[(int)LvLTI.DECIPHER_ORNTT].Text = strOrientation;

                        }
                        // 추출 못해도 DB Insert 까지 갈 수 있게 ...
                        catch (Exception e)
                        {
                            String strTmp = e.Message;
                        }

                        //---------------------------------------------//
                        // FTP 전송
                        //---------------------------------------------//
                        //lvLtiText.sel
                        //this.lvLtiText.Items[i].Selected = true;

                        //// 서버 Upload할 폴더를 결정하기 위해 날짜 형식 변경
                        //String strDateUs = this.lvLtiText.SelectedItems[i].SubItems[24].Text; // 월/일/년
                        //String[] arrDate = strDateUs.Split('/');
                        //String strDateKr = arrDate[2] + "/" + arrDate[0] + "/" + arrDate[1];  // 년/월/일
                        String strDateUs = this.lvLtiText.SelectedItems[i].SubItems[(int)LvLTI.CLIP_DATE].Text;   // 월/일/년
                        String strDateKr = "";                                                  // 년/월/일
                        bool cvtDate = iTopsLib.Lib.GFn_FormatDate(strDateUs, (int)iTopsLib.DateFormat.KR, ref strDateKr, '-');
                        if (!cvtDate) continue; // 날짜 변환 못함 

                        bool ftp_rv = false;
                        // 단속 원본 Upload
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Upload original file Start", i + 1, rec_cnt));
                        if (File.Exists(".\\Temp\\" + Path.GetFileName(srcFile)))
                        {
                            ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                           , Path.GetFileName(srcFile)
                                                           , ".\\Temp\\"
                                                           , Path.GetFileName(srcFile)
                                                           , false
                                                           );
                            // 작업 상황 보여주기
                            if (ftp_rv)
                                AddStatus(String.Format("          -. [{0} / {1}] Upload original file Success", i + 1, rec_cnt));
                            else
                                AddStatus(String.Format("          -. [{0} / {1}] Upload original file Fail", i + 1, rec_cnt));

                        }
                        else
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload original file Fail - Not Exist", i + 1, rec_cnt));

                        }



                        // 판독용 추출 이미지 Upload
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Start", i + 1, rec_cnt));
                        if (File.Exists(".\\Temp\\" + Path.GetFileNameWithoutExtension(srcFile) + ".jpg"))
                        {
                            ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                           , Path.GetFileNameWithoutExtension(srcFile) + ".jpg"
                                                           , ".\\Temp\\"
                                                           , Path.GetFileNameWithoutExtension(srcFile) + ".jpg"
                                                           , false
                                                           );

                            // 작업 상황 보여주기
                            if (ftp_rv)
                                AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Success", i + 1, rec_cnt));
                            else
                                AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Fail", i + 1, rec_cnt));

                        }
                        else
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Fail - Not Exist", i + 1, rec_cnt));

                        }

                        if (bDecipherPlate && bPlateImage)
                        {
                            // 추출된 Plate 이미지 Upload
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Start", i + 1, rec_cnt));

                            if (File.Exists(".\\Temp\\" + Path.GetFileNameWithoutExtension(srcFile) + "_P.jpg"))
                            {
                                ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                               , Path.GetFileNameWithoutExtension(srcFile) + "_P.jpg"
                                                               , ".\\Temp\\"
                                                               , Path.GetFileNameWithoutExtension(srcFile) + "_P.jpg"
                                                               , false
                                                               );

                                // 작업 상황 보여주기
                                if (ftp_rv)
                                    AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Success", i + 1, rec_cnt));
                                else
                                    AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Fail", i + 1, rec_cnt));
                            }
                            else
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Fail - Not Exist", i + 1, rec_cnt));

                            }

                        }

                        //---------------------------------------------//
                        // DB Insert
                        //---------------------------------------------//

                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Insert DB Start", i + 1, rec_cnt));


                        //ListViewItem Items = lvLtiText.SelectedItems[i];

                        String strRV = "";
                        //rv = iTopsLib.Lib.Fn_InsertRegulations(Items, Convert.ToString(this.CbbCourt.Value), ref strRV);
                        rv = iTopsLib.Lib.Fn_InsertRegulations(Items, Convert.ToString(this.CbbBranch.Value), Convert.ToString(this.CbbCourt.Value), ref strRV);

                        // 작업 상황 보여주기
                        if (rv < 0)
                            AddStatus(String.Format("          -. [{0} / {1}] Insert DB Fail [{2}]", i + 1, rec_cnt, strRV), true);
                        else
                            AddStatus(String.Format("          -. [{0} / {1}] Insert DB Success", i + 1, rec_cnt), true);

                    }
                    catch (Exception e)
                    {
                        String StrTmp = e.Message;

                        // 못하면 다음 것 해야지...
                        continue;
                    }


                } // End for

                // 작업 상황 보여주기
                AddStatus("        -. Finished Work.");

            }
            catch (Exception ex)
            {
                String tmp = ex.Message;
            }
            finally
            {
                //
                // 작업 상황 보여주기
                AddStatus("[End]");

                //// 종료 메시지
                //MessageBox.Show("Successfully Import", "Informaton", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //===========================================================//
        // Com Laser 장비
        //===========================================================//
        private void Process_ComLaser()
        {
            // 작업 상황 보여주기
            AddStatus("[Start]");

            try
            {

                // 작업 상황 보여주기
                AddStatus("    -. Com Laser Device data !!!");

                // 작업 상황 보여주기
                AddStatus("        -. Data file Read.");

                // 단속 파일 분석 .. 파일명 리스트 만들기 
                String[] arrFiles = new String[lbFileList.Items.Count];
                //lbFileList.Items.Clear();
                for (int i = 0; i < lbFileList.Items.Count; i++)
                {
                    arrFiles[i] = lbFileList.Items[i].ToString();

                    // 단속 파일 내의 단속 영상 정보를 리스트화 한다.
                    using (StreamReader sr = new StreamReader(arrFiles[i], Encoding.Default))
                    {
                        string line = null;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ////this.txtRView.AppendText(line + "\r\n");
                            //arrFiles[i] = lbFileList.Items[i].ToString();
                            line += ";;;;;;;";       // 판독자료 저장용 6개, Plate No 저장용 추가 
                            String[] arrData = line.Split(';');


                            lbCLFileList.Items.Add(arrData[0] + "\r\n");   // 단속 영상 파일명(프리픽스, 확장자 없음)

                            ListViewItem lvi = new ListViewItem(arrData);   // String [] ---> ListViewItem

                            lvComLaserText.Items.Add(lvi);

                        }
                    }
                }

                int rv = lvComLaserText.Items.Count;

                if (rv == 0)
                {
                    MessageBox.Show("Data does not exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //// 작업 상황 보여주기
                //AddStatus("        -. Com Laser Text Data Analysis Start ...");

                //// 단속 장비에서 Text Data 추출 ... 만들어진 파일 리스트 전체 작업 
                //int rv = UseLibLTI.LibLTI.AnalysisLtiData(arrFiles
                //                                     , ref lvLtiText);
                //if (rv == 0)
                //{
                //    MessageBox.Show("Data does not exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //// 작업 상황 보여주기
                //AddStatus("        -. Com Laser Text Data Analysis ... End");


                // 파일 복사 USB ---> .\temp 
                String srcOrgFile = "";
                String srcJpgFile = "";
                String srcJpgFile_P = "";
                String destOrgFile = "";
                String destJpgFile = "";
                String destJpgFile_P = "";  // Plate Image
                int rec_cnt = rv;

                // 작업 상황 보여주기
                AddStatus("        -. Start Work.", true);

                //// test
                //rec_cnt = 3;

                String strPath = Path.GetDirectoryName(arrFiles[0]) + "\\";
                for (int i = 0; i < rec_cnt; i++)
                {
                    try
                    {

                        // LTI ListView 와 같이 간다.
                        this.lvComLaserText.Items[i].Selected = true;
                        ListViewItem Items = lvComLaserText.SelectedItems[i];


                        //String strPath = Path.GetDirectoryName(arrFiles[i]) + "\\";
                        String strName = Items.SubItems[(int)LvCL.NAME].Text.Trim();
                        srcOrgFile = strPath + "EV_" + strName + ".avi";
                        srcJpgFile = strPath + "EI_" + strName + ".jpg";
                        srcJpgFile_P = strPath + "EI_" + strName + "_P.jpg";
                        destOrgFile = ".\\TEMP\\" + Path.GetFileName(srcOrgFile);
                        destJpgFile = ".\\TEMP\\" + Path.GetFileName(srcJpgFile);
                        destJpgFile_P = ".\\TEMP\\" + Path.GetFileName(srcJpgFile_P);   // Plate Image

                        //---------------------------------------------//
                        // 기존에 서버에 존재하는 단속 영상인지 확인 ... 파일 이름이 고유하다고 가정 
                        //---------------------------------------------//
                        String strExistFile = Path.GetFileNameWithoutExtension(destJpgFile) + ".jpg";
                        if (iTopsLib.Lib.GFn_GetRegulationImageFile(strExistFile) == strExistFile)
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] {2} already exists.", i + 1, rec_cnt, srcOrgFile), true);
                            continue;
                        }


                        //---------------------------------------------//
                        // 파일 복사 USB ---> .\temp 
                        //---------------------------------------------//
                        FileStream fsSrc = null;
                        FileStream fsDest = null;

                        // 단속 동영상 .AVI
                        if (File.Exists(srcOrgFile))
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk : Copy .avi File Start", i + 1, rec_cnt));

                            fsSrc = new FileStream(srcOrgFile, FileMode.Open, FileAccess.Read);         //소스 파일 스트림 개체 생성
                            fsDest = new FileStream(destOrgFile, FileMode.Create, FileAccess.Write);    //목적지 파일 스트림 개체 생성
                            try
                            {
                                byte[] bts = new byte[4096];
                                int cnt = 0;
                                int mok = (int)(fsSrc.Length / 4096) - 1;
                                int nam = (int)(fsSrc.Length % 4096);
                                while (true)
                                {
                                    //Thread.Sleep(10);
                                    bts = new byte[4096];

                                    if (cnt <= mok)
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, 4096);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, 4096);

                                    }
                                    else // 마지막
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, nam);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, nam);

                                        break;
                                    }
                                    cnt++;

                                }

                            }
                            catch (Exception e)
                            {
                                String strTmp = e.Message;

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk : Copy .avi File File", i + 1, rec_cnt));
                                continue;
                            }
                            finally
                            {
                                fsDest.Close();
                                fsSrc.Close();

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk : Copy .avi File End", i + 1, rec_cnt));
                            }

                        }

                        // 단속 파일 .JPG
                        if (File.Exists(srcJpgFile))
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk : Copy .jpg File Start", i + 1, rec_cnt));


                            fsSrc = new FileStream(srcJpgFile, FileMode.Open, FileAccess.Read);         //소스 파일 스트림 개체 생성
                            fsDest = new FileStream(destJpgFile, FileMode.Create, FileAccess.Write);    //목적지 파일 스트림 개체 생성
                            try
                            {
                                byte[] bts = new byte[4096];
                                int cnt = 0;
                                int mok = (int)(fsSrc.Length / 4096) - 1;
                                int nam = (int)(fsSrc.Length % 4096);
                                while (true)
                                {
                                    //Thread.Sleep(10);
                                    bts = new byte[4096];

                                    if (cnt <= mok)
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, 4096);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, 4096);

                                    }
                                    else // 마지막
                                    {
                                        // 읽기
                                        fsSrc.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsSrc.Read(bts, 0, nam);

                                        // 쓰기
                                        fsDest.Seek(4096 * cnt, SeekOrigin.Begin);
                                        fsDest.Write(bts, 0, nam);

                                        break;
                                    }
                                    cnt++;

                                }

                            }
                            finally
                            {
                                fsDest.Close();
                                fsSrc.Close();

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] USB ---> Disk : Copy .jpg File End", i + 1, rec_cnt));
                            }
                        }

                        ////---------------------------------------------//
                        //// 번호 판독에 사용할  이미지 추출 Save JPEG
                        ////---------------------------------------------//
                        //// 작업 상황 보여주기
                        //AddStatus(String.Format("          -. [{0} / {1}] Extract Image Start", i + 1, rec_cnt));

                        //UseLibLTI.LibLTI.ExtractImage(destFile, ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destFile) + ".jpg");

                        //// 작업 상황 보여주기
                        //AddStatus(String.Format("          -. [{0} / {1}] Extract Image End", i + 1, rec_cnt));


                        ////// 복사 후 USB File 삭제
                        ////FileInfo fi = new FileInfo(srcFile);
                        ////fi.Delete();

                        //---------------------------------------------//
                        // 번호판 판독 ALPR Lib
                        //---------------------------------------------//
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR Start", i + 1, rec_cnt));

                        bool bDecipherPlate = false;
                        bool bPlateImage = false;

                        try
                        {

                            //picPlate
                            String strFIle = destJpgFile; // ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destJpgFile) + ".jpg";
                            String strPlateFile = destJpgFile_P; // ".\\TEMP\\" + Path.GetFileNameWithoutExtension(destJpgFile) + "_P.jpg";

                            // 전달 받을 결과 
                            String strPlate = "";
                            picPlate.Image = null;

                            String strMake = "";
                            String strColor = "";
                            String strModel = "";
                            String strType = "";
                            String strYear = "";
                            String strOrientation = "";

                            // 이미지 추출 및 번호 판 판독 
                            bDecipherPlate = UseLibALPR.LibALPR.ProcessImage(strFIle, iTopsLib.Lib.GetRegion()
                                                                            , ref strPlate
                                                                            , ref picPlate
                                                                            , ref strMake
                                                                            , ref strColor
                                                                            , ref strModel
                                                                            , ref strType
                                                                            , ref strYear
                                                                            , ref strOrientation
                                                                            );

                            // 번호판 이미지 추출 성공 여부 
                            bPlateImage = (picPlate.Image != null);

                            if (bDecipherPlate && bPlateImage)
                            {
                                // 추출된 번호판 이미지 저장 
                                picPlate.Image.Save(strPlateFile, System.Drawing.Imaging.ImageFormat.Jpeg);

                                // 추출된 번호판 보관
                                Items.SubItems[(int)LvCL.DECIPHER_PLATE].Text = strPlate;

                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR End - [{2}]", i + 1, rec_cnt, strPlate));
                            }
                            else
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Decipher Plate Image with ALPR End - [{2}]", i + 1, rec_cnt, "FAIL"));

                            }
                            picPlate.Refresh();

                            // 차량 정보 보관
                            Items.SubItems[(int)LvCL.DECIPHER_PLATE].Text = strPlate;
                            Items.SubItems[(int)LvCL.DECIPHER_MAKER].Text = strMake;
                            Items.SubItems[(int)LvCL.DECIPHER_COLOR].Text = strColor;
                            Items.SubItems[(int)LvCL.DECIPHER_MODEL].Text = strModel;
                            Items.SubItems[(int)LvCL.DECIPHER_TYPE].Text = strType;
                            Items.SubItems[(int)LvCL.DECIPHER_YEAR].Text = strYear;
                            Items.SubItems[(int)LvCL.DECIPHER_ORNTT].Text = strOrientation;

                        }
                        // 추출 못해도 DB Insert 까지 갈 수 있게 ...
                        catch (Exception e)
                        {
                            String strTmp = e.Message;
                        }

                        //---------------------------------------------//
                        // FTP 전송
                        //---------------------------------------------//
                        //lvLtiText.sel
                        //this.lvLtiText.Items[i].Selected = true;

                        //// 서버 Upload할 폴더를 결정하기 위해 날짜 형식 변경
                        //String strDateUs = this.lvLtiText.SelectedItems[i].SubItems[24].Text; // 월/일/년
                        //String[] arrDate = strDateUs.Split('/');
                        //String strDateKr = arrDate[2] + "/" + arrDate[0] + "/" + arrDate[1];  // 년/월/일
                        String strDateUs = this.lvComLaserText.SelectedItems[i].SubItems[(int)LvCL.DATE].Text.Trim();   // 월/일/년
                        String strDateKr = "";                                                  // 년/월/일
                        bool cvtDate = iTopsLib.Lib.GFn_FormatDate(strDateUs, (int)iTopsLib.DateFormat.KR, ref strDateKr, '-');
                        if (!cvtDate) continue; // 날짜 변환 못함 

                        bool ftp_rv = false;

                        // 단속 원본 Upload
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Upload original file Start", i + 1, rec_cnt));
                        if (File.Exists(".\\Temp\\" + Path.GetFileName(srcOrgFile)))
                        {
                            ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                           , Path.GetFileName(srcOrgFile)
                                                           , ".\\Temp\\"
                                                           , Path.GetFileName(srcOrgFile)
                                                           , false
                                                           );

                            // 작업 상황 보여주기
                            if (ftp_rv)
                                AddStatus(String.Format("          -. [{0} / {1}] Upload original file Success", i + 1, rec_cnt));
                            else
                                AddStatus(String.Format("          -. [{0} / {1}] Upload original file Fail", i + 1, rec_cnt));
                        }
                        else
                        {
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload original file Fail - Not Exist", i + 1, rec_cnt));

                        }




                        // 판독용 추출 이미지 Upload
                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Start", i + 1, rec_cnt));
                        if (File.Exists(".\\Temp\\" + Path.GetFileName(srcJpgFile)))
                        {
                            ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                           , Path.GetFileName(srcJpgFile)
                                                           , ".\\Temp\\"
                                                           , Path.GetFileName(srcJpgFile)
                                                           , false
                                                           );
                            // 작업 상황 보여주기
                            if (ftp_rv)
                                AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Success", i + 1, rec_cnt));
                            else
                                AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Fail", i + 1, rec_cnt));
                        }
                        else
                        {

                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload measured file Fail - Not Exist", i + 1, rec_cnt));
                        }


                        if (bDecipherPlate && bPlateImage)
                        {
                            // 추출된 Plate 이미지 Upload
                            // 작업 상황 보여주기
                            AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Start", i + 1, rec_cnt));
                            if (File.Exists(".\\Temp\\" + Path.GetFileName(srcJpgFile_P)))
                            {
                                ftp_rv = iTopsLib.Lib.UploadFile(strDateKr
                                                               , Path.GetFileName(srcJpgFile_P)
                                                               , ".\\Temp\\"
                                                               , Path.GetFileName(srcJpgFile_P)
                                                               , false
                                                               );
                                // 작업 상황 보여주기
                                if (ftp_rv)
                                    AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Success", i + 1, rec_cnt));
                                else
                                    AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Fail", i + 1, rec_cnt));

                            }
                            else
                            {
                                // 작업 상황 보여주기
                                AddStatus(String.Format("          -. [{0} / {1}] Upload license plate file Fail - Not Exist", i + 1, rec_cnt));


                            }

                        }

                        //---------------------------------------------//
                        // DB Insert
                        //---------------------------------------------//

                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Insert DB Start", i + 1, rec_cnt));


                        //ListViewItem Items = lvLtiText.SelectedItems[i];
                        String strRV = "";
                        rv = iTopsLib.Lib.Fn_InsertRegulationsCL(Items, ref strRV);

                        // 작업 상황 보여주기
                        if (rv < 0)
                            AddStatus(String.Format("          -. [{0} / {1}] Insert DB Fail [{2}]", i + 1, rec_cnt, strRV), true);
                        else
                            AddStatus(String.Format("          -. [{0} / {1}] Insert DB Success", i + 1, rec_cnt), true);


                    }
                    catch (Exception e)
                    {
                        String strTmp = e.Message;

                        // 작업 상황 보여주기
                        AddStatus(String.Format("          -. [{0} / {1}] Fail", i + 1, rec_cnt), true);
                        continue;
                    }

                } // End for

                // 작업 상황 보여주기
                AddStatus("        -. Finished Work.");

            }
            catch (Exception ex)
            {
                String tmp = ex.Message;
            }
            finally
            {
                //
                // 작업 상황 보여주기
                AddStatus("[End]");

                //// 종료 메시지
                //MessageBox.Show("Successfully Import", "Informaton", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        //===========================================================//
        // 작업 상황 보여주기
        //===========================================================//
        private void AddStatus(String desc, bool isAddLine = false)
        {
            lbResult.Items.Add(String.Format("[{0}]{1}", DateTime.Now.ToLocalTime(), desc));
            if (isAddLine)
                lbResult.Items.Add("");

            // ListBox 맨 밑으로 이동 ... 최근 상황 보여주기
            lbResult.SelectedIndex = lbResult.Items.Count - 1;
            lbResult.ClearSelected();

            // 다시 그리기 
            lbResult.Refresh();

        }

        private void Fn_SetCodeCombo()
        {
            // Branch
            try
            {
                int rv = iTopsLib.Lib.GFn_GetCodeDS("BRNCH", "", ref dsBranch, true, false);
                if (rv > 0)
                {
                    CbbBranch.DataSource = dsBranch.Tables[0];

                    CbbBranch.DisplayMember = "display_value";
                    CbbBranch.ValueMember = "cd";

                    CbbBranch.Text = null;
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

        }

        //===========================================================//
        // 종료
        //===========================================================//
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnFindEditor_Click(object sender, EventArgs e)
        {
            // 임시
            txtEditorName.Text = txtEditorId.Text;
        }

        private void RdBtnLti_Click(object sender, EventArgs e)
        {
            // 선택한 폴더의 파일 List 가져 오기 
            lbFileList.Items.Clear();

            // 목록 보관할 ListView Clear
            lvLtiText.Items.Clear();
            lvComLaserText.Items.Clear();

            CbbCourt.Value = String.Empty;
            txtDir.Text = String.Empty;

            if (RdBtnLti.Checked)
            {
                CbbCourt.Enabled = true;
            }
            else if (RdBtnCom.Checked)
            {
                CbbCourt.Enabled = false;
                CbbCourt.Value = null;
            }
        }

        private void CbbCourt_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraCombo combo = sender as Infragistics.Win.UltraWinGrid.UltraCombo;

            String strCbbName = combo.Name;
            String strName = strCbbName.Substring(3, strCbbName.Length - 3).ToUpper();

            MessageBox.Show("Please enter the exact details of " + strName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            combo.Value = null;
            combo.Focus();

        }

        // 입력 값 검증
        private void CbbCourt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 입력된 값이 dataset에 존재하는 값인지 확인 (알파벳, 숫자, 특수문자)
            // space - 32
            // ~     - 126
            if (e.KeyChar == '\r') // enter
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
            //else if ((e.KeyChar >= ' ' && e.KeyChar <= '~'))
            //{
            //    // ComboBox 에서 온 Event 만 처리 
            //    if (!(sender is Infragistics.Win.UltraWinGrid.UltraCombo)) return;

            //    String strInput = CbbCourt.Text + e.KeyChar;
            //    try
            //    {
            //        DataRow[] row = dsCourt.Tables[0].Select(string.Format("cd like '*{0}*'", strInput));
            //        if (row.Length == 0)
            //        {
            //            row = dsCourt.Tables[0].Select(string.Format("cd_nm like '*{0}*'", strInput));
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

        private void CbbCourt_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 일단 모두 숨긴다. - 나중에 컬럼이 추가 되어도 관련된 것만 수정하기 위함
            for (int i = 0; i < e.Layout.Bands[0].Columns.Count; i++)
            {
                e.Layout.Bands[0].Columns[i].Hidden = true;
            }

            // 필요한 거만 보여준다.
            e.Layout.Bands[0].Columns[0].Hidden = false;
            e.Layout.Bands[0].Columns[1].Hidden = false;

            e.Layout.Bands[0].Columns[0].Width = 140;
            e.Layout.Bands[0].Columns[1].Width = 274;

        }

        // 마우스로 창 이동
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
        }

        // 마우스로 창 이동
        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
        }

        // 마우스로 창 이동
        private void pnlTop_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void lvComLaserText_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
