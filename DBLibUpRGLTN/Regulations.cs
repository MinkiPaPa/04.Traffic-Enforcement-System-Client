using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// Path
using System.IO;
// ListViewItem 사용
using System.Windows.Forms;

namespace DBLibUpRGLTN
{
    // LTI Text Data -iTopsUpRGLTN.FrmMain.cs 와 동일한 것을 갖고 있다. 수정시 같이 수정해 줄것
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

    // LTI Text Data - iTopsUpRGLTN.FrmMain.cs 와 동일한 것을 갖고 있다. 수정시 같이 수정해 줄 것 
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

    public class Regulations
    {

        // 특정 단속 이미지가 존재하는지 확인
        public static String GFn_GetRegulationImageFile(SqlConnection Conn, String strFileName)
        {

            if (Conn == null)
            {
                return null;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return null;

            String SQLText = String.Format("SELECT file_name           file_name       "
                                          + "  FROM REGULATIONS                         "
                                          + " WHERE UPPER(file_name)    = UPPER('{0}')  "
                                          , strFileName
                                          );
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            var Values = sqlComm.ExecuteReader();
            try
            {
                // 0건
                if (!Values.HasRows) return null;
                if (Values.Read())
                {
                    return Values[0].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                return null;

            }
            finally
            {
                if (Values != null) Values.Close();
            }
        }

        // Insert Regulations LTI
        public static int Insert(SqlConnection Conn, ListViewItem Items, String strUserId, String svrImgPath, String strBranch, String strCourt, ref String strRV)
        {
            int rv = 0;
            strRV = "";

            if (Conn == null)
            {
                return -1;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return -1;

            String strOrgFile = Items.SubItems[(int)LvLTI.CLIP_NAME].Text;
            String strMesrmFile = Path.GetFileNameWithoutExtension(strOrgFile) + ".jpg";
            String strPalteFile = "";
            if (Items.SubItems[(int)LvLTI.DECIPHER_PLATE].Text.Length != 0)
            {
                strPalteFile = Path.GetFileNameWithoutExtension(strOrgFile) + "_P.jpg";
            }
            String SQLText = String.Format("INSERT INTO REGULATIONS ( device_type"
                                          + "                        , device_mdl"
                                          + "                        , device_sn"
                                          // Location
                                          + "                        , branch"
                                          + "                        , officer"
                                          + "                        , court"
                                          + "                        , street"
                                          + "                        , location"
                                          + "                        , direction"
                                          // Regulations
                                          + "                        , regulation_type"
                                          + "                        , regulation_lane"
                                          + "                        , regulation_distance"
                                          + "                        , regulation_time"
                                          + "                        , regulation_spd_limit"
                                          // Speed, offence_code, fine
                                          + "                        , real_speed"
                                          + "                        , over_speed"
                                          + "                        , offence_code"
                                          + "                        , fine"
                                          // File
                                          + "                        , file_directory"
                                          + "                        , file_original"
                                          + "                        , file_no"
                                          + "                        , file_name"
                                          + "                        , file_plate"

                                          /* Uploader 정보 */
                                          + "                        , upload_id"
                                          + "                        , upload_time"
                                          /* 2019.06.18 판독정보 추가 */
                                          + "                        , decipher_plate"
                                          + "                        , decipher_maker"
                                          + "                        , decipher_type"
                                          + "                        , decipher_color"
                                          + "                        , decipher_model"
                                          + "                        , decipher_year"
                                          + "                        , decipher_orntt"
                                          // Update Info
                                          + "                        , create_dtm"
                                          + "                        , create_id"
                                          + "                        , chg_dtm"
                                          + "                        , chg_id"
                                          + "                        )"
                                          + "                 VALUES ( '{0}', '{1}', '{2}'"                                     // Device
                                          + "                        , '{3}', '{4}', '{5}', '{6}', '{7}',  '{8}'"               // Location
                                          + "                        , '{9}', {10}, {11}, '{12}', {13}"                         // regulations
                                          + "                        , {14},  {14} - {13}, '{15}', {16}"                               // speed, offence_code, fine
                                          + "                        , '{17}', '{18}', {19}, '{20}', '{21}'"                    // file path, name
                                          + "                        , '{22}', GetDate()"                                       // uploader
                                          + "                        , '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}'"  // decipher
                                          + "                        , GetDate(), '{30}', GetDate(), '{31}' "                   // Update Info
                                          + "                        ) "
                                          // Device
                                          , "DTYP00001"
                                          , "DMDL00001"
                                          , "1"
                                          // Location
                                          //                                          , "NEW CASTLE"
                                          , strBranch
                                          , Items.SubItems[(int)LvLTI.OP_ID].Text
                                          , strCourt
                                          , Items.SubItems[(int)LvLTI.STRT_NAME].Text
                                          , Items.SubItems[(int)LvLTI.STRT_CD].Text
                                          , "Direc"
                                          /* 단속 정보 */
                                          , "RGT00001"
                                          , Items.SubItems[(int)LvLTI.LANE].Text
                                          , Items.SubItems[(int)LvLTI.MESRM_DISTC].Text
                                          , Items.SubItems[(int)LvLTI.CLIP_DATE].Text + " " + Items.SubItems[(int)LvLTI.CLIP_TIME].Text
                                          , Items.SubItems[(int)LvLTI.SPD_LIMIT].Text
                                          // Speed, offence_code, fine
                                          , Items.SubItems[(int)LvLTI.MESRM_SPD].Text
                                          //                                          , "0" 
                                          , "VLT00001"
                                          , "0"
                                          /* 단속 이미지 파일 정보 */
                                          , svrImgPath
                                          , strOrgFile
                                          , Items.SubItems[(int)LvLTI.MESRM_FRAME].Text
                                          , strMesrmFile
                                          , strPalteFile
                                          /* Uploder */
                                          , strUserId

                                          /* 판독 정보 */
                                          , Items.SubItems[(int)LvLTI.DECIPHER_PLATE].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_MAKER].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_TYPE].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_COLOR].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_MODEL].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_YEAR].Text
                                          , Items.SubItems[(int)LvLTI.DECIPHER_ORNTT].Text

                                          , strUserId
                                          , strUserId
                                          );

            // 실행
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            try
            {
                rv = sqlComm.ExecuteNonQuery();

                if (rv == 1) return rv;

            }
            catch (Exception ex)
            {
                // Build 시 메시지 나오지 말라고 ...
                String strTmp = ex.Message;
                strRV = strTmp;
                return -1;
            }
            finally
            {
                //

            }

            return rv;
        }

        // Insert Regulations Com Laser
        public static int InsertCL(SqlConnection Conn, ListViewItem Items, String strUserId, String svrImgPath, ref String strRV)
        {
            int rv = 0;
            strRV = "";

            if (Conn == null)
            {
                return -1;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return -1;

            String strOrgFile = "EV_" + Items.SubItems[(int)LvCL.NAME].Text + ".avi";
            String strMesrmFile = "EI_" + Items.SubItems[(int)LvCL.NAME].Text + ".jpg";
            String strPalteFile = "";
            if (Items.SubItems[(int)LvCL.DECIPHER_PLATE].Text.Length != 0)
            {
                strPalteFile = "EI_" + Items.SubItems[(int)LvCL.NAME].Text + "_P.jpg";
            }
            String SQLText = String.Format("INSERT INTO REGULATIONS ( device_type"
                                          + "                        , device_mdl"
                                          + "                        , device_sn"
                                          // Location
                                          + "                        , branch"
                                          + "                        , officer"
                                          + "                        , court"
                                          + "                        , street"
                                          + "                        , location"
                                          + "                        , direction"
                                          // Regulations
                                          + "                        , regulation_type"
                                          + "                        , regulation_lane"
                                          + "                        , regulation_distance"
                                          + "                        , regulation_time"
                                          + "                        , regulation_spd_limit"
                                          // Speed, offence_code, fine
                                          + "                        , real_speed"
                                          + "                        , over_speed"
                                          + "                        , offence_code"
                                          + "                        , fine"
                                          // File
                                          + "                        , file_directory"
                                          + "                        , file_original"
                                          + "                        , file_no"
                                          + "                        , file_name"
                                          + "                        , file_plate"

                                          /* Uploader 정보 */
                                          + "                        , upload_id"
                                          + "                        , upload_time"
                                          /* 2019.06.18 판독정보 추가 */
                                          + "                        , decipher_plate"
                                          + "                        , decipher_maker"
                                          + "                        , decipher_type"
                                          + "                        , decipher_color"
                                          + "                        , decipher_model"
                                          + "                        , decipher_year"
                                          + "                        , decipher_orntt"
                                          // Update Info
                                          + "                        , create_dtm"
                                          + "                        , create_id"
                                          + "                        , chg_dtm"
                                          + "                        , chg_id"
                                          + "                        )"
                                          + "                 VALUES ( '{0}', '{1}', '{2}'"                                     // Device
                                          + "                        , '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'"                // Location
                                          + "                        , '{9}', {10}, {11}, '{12}', {13}"                         // regulations
                                          + "                        , {14},  {14} - {13}, '{15}', {16}"                               // speed, offence_code, fine
                                          + "                        , '{17}', '{18}', {19}, '{20}', '{21}'"                    // file path, name
                                          + "                        , '{22}', GetDate()"                                       // uploader
                                          + "                        , '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}'"  // decipher
                                          + "                        , GetDate(), '{30}', GetDate(), '{31}' "                   // Update Info
                                          + "                        ) "
                                          // Device
                                          , "DTYP00001"
                                          , "DMDL00002"
                                          , Items.SubItems[(int)LvCL.MODEL].Text
                                          // Location
                                          , "NEW CASTLE"
                                          , Items.SubItems[(int)LvCL.OP_ID].Text
                                          //, "COURT"
                                          , Items.SubItems[(int)LvCL.COURT].Text

                                          , "Street Name"
                                          , Items.SubItems[(int)LvCL.LOCATION].Text
                                          , Items.SubItems[(int)LvCL.DIRECTION].Text

                                          // 단속정보
                                          , "RGT00001"
                                          , "0"
                                          , Items.SubItems[(int)LvCL.DISTANCE].Text
                                          , Items.SubItems[(int)LvCL.DATE].Text + " " + Items.SubItems[(int)LvCL.TIME].Text
                                          , Items.SubItems[(int)LvCL.SPEED_LIMIT].Text
                                          // 위반정보
                                          , Items.SubItems[(int)LvCL.SPEED].Text
                                          //                                          , "0"
                                          , "VLT00001"
                                          , "0"
                                          // 이미지 파일정보
                                          , svrImgPath
                                          , strOrgFile
                                          , "0"
                                          , strMesrmFile
                                          , strPalteFile
                                          /* 2019.06.18 삭제 후 판독 정보 추가 
                                                                                    , Items.SubItems[(int)LvCL.DECIPHER_PLATE].Text
                                          */
                                          /* Uploader 정보 */
                                          , strUserId

                                          /* 2019.06.18 판독 정보 추가 */
                                          , Items.SubItems[(int)LvCL.DECIPHER_PLATE].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_MAKER].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_TYPE].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_COLOR].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_MODEL].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_YEAR].Text
                                          , Items.SubItems[(int)LvCL.DECIPHER_ORNTT].Text

                                          , strUserId
                                          , strUserId
                                          );

            // 실행
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            try
            {
                rv = sqlComm.ExecuteNonQuery();

                if (rv == 1) return rv;

            }
            catch (Exception ex)
            {
                // Build 시 메시지 나오지 말라고 ...
                String strTmp = ex.Message;
                strRV = strTmp;
                //MessageBox.Show(strTmp);
                return -1;
            }
            finally
            {
                //

            }

            return rv;
        }
    }
}
