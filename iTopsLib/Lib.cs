// 플러그형 프로토콜을 통해 네트워크에 액세스하는 동안 발생하는 오류 처리를 위 함
// Exception    : System - 응용프로그램 오류
// WebException : System.Net - 네트워크 접속시  오류

//// REGULATIONS Table 관련 SQL 사용
//using DBLibRegulations;

//// CONTRAVENTIONS Table 관련 SQL 사용
//using DBLibContraventions;

// Distribute 화면에서 사용하는 쿼리

// Inspection 화면에서 사용하는 쿼리

// UpRGLTN 화면에서 사용하는 쿼리
using DBLibUpRGLTN;
// WIN32 API
using Microsoft.Win32;
using System;
using System.Configuration;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// MDI Child 관련 
// Process
using System.Diagnostics;
// Image 처리
using System.Drawing;
// iTops Library 

//
using System.IO;
using System.Linq;
// Code 관련 SQL

//// Code 관련 SQL
//using DBLibCdMngLocation;

// Offence Code 관련 SQL

// Location Mapping Data 관련 SQL

// Persons & Users 관련


//// 레지스트리 관련
//using Microsoft.Win32;

// 화면 깜박임 속도 개선 - property  Dublebuffer 사용하기 위함
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace iTopsLib
{

    //------------------------------------------------------------------//
    // 문자열 암복호화 - System.Text Encoding Base64
    //------------------------------------------------------------------//
    class ConvertEnDe
    {
        // Encoding
        public String GFn_EncodingBase64(String sSrc)
        {
            String rv = String.Empty;

            if (sSrc == String.Empty || sSrc == null) return rv;

            try
            {
                byte[] bytes = Encoding.Default.GetBytes(sSrc);
                rv = Convert.ToBase64String(bytes);

            }
            catch (Exception Ex)
            {
                rv = "Error : " + Ex.Message.ToString();
            }
            return rv;
        }

        // Decoding
        public String GFn_DecodingBase64(String sSrc)
        {
            String rv = String.Empty;

            if (sSrc == String.Empty || sSrc == null) return rv;

            try
            {
                byte[] bytes = Convert.FromBase64String(sSrc);
                rv = Encoding.Default.GetString(bytes);

            }
            catch (Exception Ex)
            {
                rv = "Error : " + Ex.Message.ToString();
            }
            return rv;

        }


    }

    //------------------------------------------------------------------//
    // 공용 라이브러리
    //------------------------------------------------------------------//
    // Distribute 화면의 그리드 
    public enum EnGridDistribution
    {
        SEL_YN = 0 			        // 선택여부
      , RGLTN_ID                 // 단속 고유번호 ID                                                	
      , DEVICE_TP                // 사용기기 종류 : 이동형, 고정형, 신호위반, 통계용 ... (INTERFACE) 
      , DEVICE_MDL               // 사용기기 모델 : Maker or Model No or Serial No ...               
      , DEVICE_SN                // 사용기기 No : 관리 번호 또는 시리얼 No                           

      , BRANCH                   // Branch
      , BRANCH_NM                // Branch name
      , OFFICER                  // 단속경관 ID                                                      
      , OFFICER_NM               // 단속경관 이름                                                    
      , COURT                    // 관할법원 (Area)                                                  
      , STREET            // 위반 - Hightway Name : M1, N1, N3 ...                            
      , LOCATION          // 위반 - 지역코드                                                  
      , DIRECT            // 위반 - 방향코드 : N, S, E, W, ND ...                             

      , RGLT_TP                  // 단속 - 종류 : 속도위반 단속, 신호위반 단속, 법규위반단속 ...     
      , RGLT_NM                 // 단속 - 종류명칭			                                               
      , RGLT_LANE               // 단속 - 차선                                                      
      , RGLT_DIST               // 단속 - 촬영 거리                                                 
      , RGLT_TIME               // 단속 - 시각                                                      
      , RGLT_SPD_LIMIT          // 단속 - 제한 속도                                                 

      , REAL_SPEED             // 위반 - 주행 속도                                                 
      , OVER_SPEED             // 위반 - 주행 속도                                                 
      , OFFENCE_CODE              // 위반 - 종류 : 속도 위반, 신호위반, 안전벨트 미착용 ...           
      , OFFENCE_NM                // 위반 - 종류명칭
      , FINE                // 위반 - 종류명칭

      , FILE_DIR                // 파일 PATH( 파일 서버의 절대경로 )로                              
      , FILE_ORG                // 원본 파일 - 동영상 원본(*.avi, *.jmx, ...)                       
      , FILE_NO                 // 추출 이미지 순번(0 ~ )                                           
      , FILE_NM                 // 판독 이미지 파일명                                               
      , FILE_PLATE              // 번호판 추출 이미지 파일 - 번호판 이미지  

      , UPLOAD_ID               // upload 담당자 ID                                                 
      , UPLOAD_NM               // upload 담당자 이름                                               
      , UPLOAD_TIME             // upload 시각     

      , DECIPHER_PLATE          // 판독 번호판 번호                                                 
      , DECIPHER_MAKER          // 판독 번호판 번호                                                 
      , DECIPHER_MAKER_NM       // 판독 번호판 번호                                                 
      , DECIPHER_TYPE           // 판독 번호판 번호                                                 
      , DECIPHER_TYPE_NM        // 판독 번호판 번호                                                 
      , DECIPHER_COLOR          // 판독 번호판 번호                                                 
      , DECIPHER_MODEL          // 판독 번호판 번호                                                 
      , DECIPHER_YEAR           // 판독 번호판 번호                                                 
      , DECIPHER_ORNTT          // 판독 - 방향

      , OFFENCE_YN               // 위반 테이블 반영여부   
      , OFFENCE_ID               // 위반 테이블 ID

      , INSPECTOR_ID            // Inspector id
      , INSPECTOR_NM            // Inspector name

      , CREATE_DTM              // 등록일시                                                         
      , CREATE_ID               // 등록자 ID                                                        
      , CHG_DTM                 // 최종 수정일시                                                    
      , CHG_ID                  // 최종 작업자     

      , ORG_INSPECTOR_ID        // Original Inspector 
    }

    // Inspection 화면의 그리드 
    public enum EnGridInspection
    {
        OFFENCE_ID = 0               // 위반 테이블 PK

      //=== 단속 정보                                                                                                                                                                                                                                                                                                                            
      , RGLTN_ID                 // 단속 테이블 Key
      , DEVICE_TYPE              // 사용기기 종류: 이동형, 고정형, 신호위반, 통계용... (INTERFACE)
      , DEVICE_NM
      , DEVICE_MDL               // 사용기기 모델: Maker or Model No or Serial No...
      , DEVICE_MDL_NM
      , DEVICE_SN                // 사용기기 No: 관리 번호 또는 시리얼 No

      , BRANCH                   // 소속: JMPD, CAPE, DBAN...
      , OFFICER                  // 단속경관 ID
      , OFFICER_NM
      , COURT                   // 관할법원()
      , STREET                  // Hightway Name: M1, N1, N3...
      , LOCATION                // 지역코드
      , DIRECTION               // 방향코드 : N, S, E, W, ND...
      , MENUAL_YN

      , REGULATION_TYPE         // 단속 - 종류 : 속도위반 단속, 신호위반 단속, 법규위반단속...
      , REGULATION_NM
      , REGULATION_LANE         // 단속 - 차선
      , REGULATION_DISTANCE     // 단속 - 촬영 거리
      , REGULATION_TIME         // 단속 - 시각
      , REGULATION_SPD_LIMIT    // 단속 - 제한 속도

      , REAL_SPEED         // 위반 - 주행 속도
      , OVER_SPEED         // 위반 - 주행 속도
      , OFFENCE_CODE          // 위반 - 종류 : 속도 위반, 신호위반, 안전벨트 미착용...
      , OFFENCE_NM            // 위반 - 종류 : 속도 위반, 신호위반, 안전벨트 미착용...
      , FINE                    // 위반 - 주행 속도

      , FILE_DIRECTORY          // 파일 경 PATH(파일 서버의 절대경로)로
      , FILE_ORIGINAL           // 원본 파일 -동영상 원본(*.avi, *.jmx, ...)
      , FILE_NO                 // 추출 이미지 순번(0 ~ )
      , FILE_NAME               // 판독 이미지 파일명
      , FILE_PLATE              // 번호판 추출 이미지 파일 -번호판 이미지

      , UPLOAD_ID               // upload 담당자 ID
      , UPLOAD_NM
      , UPLOAD_TIME             // upload 시각

      //===번호판 판독 정보
      , DECIPHER_PLATE          // 판독 - 차량 번호
      , DECIPHER_MAKER_CD       // 판독 - 제조사
      , DECIPHER_MAKER          // 판독 - 제조사
      , DECIPHER_TYPE_CD        // 판독 - 바디 타입? 종류
      , DECIPHER_TYPE           // 판독 - 바디 타입? 종류
      , DECIPHER_COLOR          // 판독 - 차량 색상
      , DECIPHER_MODEL          // 판독 - 차량 모델
      , DECIPHER_YEAR           // 판독 - 생산년도
      , DECIPHER_ORNTT          // 판독 - 방향


        //=== 검증 Inspection 정보
      , INSPECTION_ID           // 검증 담당자
      , INSPECTION_NM
      , INSPECTION_TIME         // 검증 시각
      , INSPECTION_PLATE        // 검증 - 차량 번호
      , INSPECTION_MAKER_CD     // 검증 - 제조사
      , INSPECTION_MAKER        // 검증 - 제조사
      , INSPECTION_COLOR        // 검증 - 차량 색상
      , INSPECTION_MODEL        // 검증 - 차량 모델
      , INSPECTION_TYPE_CD      // 검증 - 바디 타입? 종류
      , INSPECTION_TYPE         // 검증 - 바디 타입? 종류
      , INSPECTION_YEAR         // 검증 - 생산년도

      , INSPECTION_READ_NOT_CD         // 검증 - 검증 불가 코드
      , INSPECTION_READ_NOT_NM         // 검증 - 검증 불가 설명
      , INSPECTION_READ_NOT_ETC         // 검증 - 검증 불가 기타 사유



      , INSPECTION_END_YN       // 검증 - 완료여부

      , SUPERVISOR_ID
      , SUPERVISOR_NM
      , SUPERVISOR_TIME
      , SUPERVISOR_PERMIT_CD
      , SUPERVISOR_REJECT_DESC

      //=== 최종 차량정보
      , VEHICLE_PLATE          // 판독 - 차량 번호
      , VEHICLE_MAKER_CD       // 판독 - 제조사
      , VEHICLE_MAKER          // 판독 - 제조사
      , VEHICLE_TYPE_CD        // 판독 - 바디 타입? 종류
      , VEHICLE_TYPE           // 판독 - 바디 타입? 종류
      , VEHICLE_COLOR          // 판독 - 차량 색상
      , VEHICLE_MODEL          // 판독 - 차량 모델
      , VEHICLE_YEAR           // 판독 - 생산년도

      , STATUS
      , STATUS_NM

      , CREATE_DTM              // 등록일시
      , CREATE_ID               // 등록자 ID
      , CREATE_NM
      , CHG_DTM                 // 최종 수정일시
      , CHG_ID                  // 최종 작업자
      , CHG_NM

      // 수정 여부 판단용
      ////, DECIPHER_PLATE_ORG          // 판독 - 차량 번호
      ////, INSPECTION_PLATE_ORG        // 검증 - 차량 번호
      //, INSPECTION_PLATE_ORG        // 검증 - 차량 번호
      //, INSPECTION_MAKER_ORG        // 검증 - 제조사
      //, INSPECTION_COLOR_ORG        // 검증 - 차량 색상
      //, INSPECTION_MODEL_ORG        // 검증 - 차량 모델
      //, INSPECTION_TYPE_ORG         // 검증 - 바디 타입? 종류
      //, INSPECTION_YEAR_ORG         // 검증 - 생산년도

      , INSPECTION_END_YN_ORG       // 검증 - 완료여부

      , VEHICLE_PLATE_ORG          // 판독 - 차량 번호
      , VEHICLE_MAKER_CD_ORG       // 판독 - 제조사
      , VEHICLE_MAKER_ORG          // 판독 - 제조사
      , VEHICLE_TYPE_CD_ORG        // 판독 - 바디 타입? 종류
      , VEHICLE_TYPE_ORG           // 판독 - 바디 타입? 종류
      , VEHICLE_COLOR_ORG          // 판독 - 차량 색상
      , VEHICLE_MODEL_ORG          // 판독 - 차량 모델
      , VEHICLE_YEAR_ORG           // 판독 - 생산년도

      , INSPECTION_READ_NOT_CD_ORG         // 검증 - 검증 불가 코드
      , INSPECTION_READ_NOT_ETC_ORG         // 검증 - 검증 불가 기타 사유

      // Supervisor용
      , SUPERVISOR_PERMIT_CD_ORG        // Supervisor 작업 상황 코드
      , SUPERVISOR_REJECT_DESC_ORG      // Supervisor 반려 사유 

      , COURT_ORG                   // 관할법원()
      , FINE_ORG                    // 위반 - 주행 속도

    }


    public enum DateFormat
    {
        WRONG = 0
      , KR = 1
      , US = 2
    }

    public enum RecState
    {
        NORMAL = 0
      , NEW = 1
      , UPDATED = 2
      , DELETED = 3
    }

    public class Lib
    {

        //----------------------------------------------------------------------//
        // Def. Global
        //----------------------------------------------------------------------//
        // FTP Connection Info
        static String FtpIP { get; set; }
        static String FtpPort { get; set; }
        static String FtpID { get { return "trapeace"; } }
        static String FtpPWD { get { return "@mbcisno1"; } }
        static String FtpHOME { get { return "/REGULATIONS/Files/"; } }

        // SQL Connection Info
        static String SqlIP { get; set; }
        static String SqlPort { get; set; }
        static String SqlID { get { return "admin"; } }
        static String SqlPWD { get { return "@mbcisno1"; } }

        // DB 변경 - 임시 : code 작업중
        static String SqlDB { get { return "TRAPEACE"; } }
        //static String SqlDB { get { return "TRAPEACE_NEW"; } }


        static String SqlSvrStr { get; set; }
        static SqlConnection SqlConn { get; set; }

        // Connection Info
        static String UserId { get; set; }         // User ID
        static String PassWord { get; set; }       // User PassWord
        static String UserNm { get; set; }         // User Name
        static int Level { get; set; }              // User Level
        static int FailCnt { get; set; }           // Login attempt Cnt
        static bool Connected { get; set; }        // Connection YN

        // Background Image
        static String Background { get; set; }      // MainForm Background Image

        // Child Process
        //static int ChildCnt { get; set; }
        //static Process[] ChildProcesses { get; set; }

        // ALPR
        static String Region { get; set; }

        // 레지스트리
        static String REG_KEY_PATH { get { return @"Software\Trapeace\iTops"; } }
        static String REG_F_IP { get { return @"FtpIp"; } }
        static String REG_F_PORT { get { return @"FtpPort"; } }
        static String REG_S_IP { get { return @"DBServer"; } }
        static String REG_S_PORT { get { return @"DBPort"; } }
        static String REG_ALPR_REGION { get { return @"ALPRRegion"; } }
        static String REG_BACKGROUND { get { return @"BackGround"; } }

        //------------------------------------------------------------------//
        // 문자열 암복호화 - System.Text Encoding Base64
        //------------------------------------------------------------------//
        // 암호화
        public static String GFn_Encoding(String strSrc)
        {
            String rv = String.Empty;

            try
            {
                ConvertEnDe cvtEnDe = new ConvertEnDe();
                rv = cvtEnDe.GFn_EncodingBase64(strSrc);
            }
            catch (Exception ex)
            {
                rv = "Error : " + ex.Message;
            }
            finally
            {
                //
            }

            return rv;
        }

        // 복호화
        private static String Fn_Decoding(String strSrc)
        {
            String rv = String.Empty;

            try
            {
                ConvertEnDe cvtEnDe = new ConvertEnDe();
                rv = cvtEnDe.GFn_DecodingBase64(strSrc);
            }
            catch (Exception ex)
            {
                rv = "Error : " + ex.Message;
            }
            finally
            {
                //
            }

            return rv;
        }

        // 복호화 및 사용자 설정
        // Encoding 된 문자열을 받아 Decoding 후 사용자 정보에 보관
        private static bool Fn_SetUser(String strSrc)
        {
            bool rv = false;
            try
            {
                String strResult = Fn_Decoding(strSrc);
                if (strResult == String.Empty || strResult == null) return rv;

                String[] s = strResult.Split(' ');
                if (s.Length < 2) return rv;

                UserId = s[0];
                PassWord = s[1];

                rv = true;
            }
            catch (Exception ex)
            {
                //
                rv = false;
                String strTmp = ex.Message;
            }
            finally
            {
                //
            }

            return rv;
        }


        //------------------------------------------------------------------//
        // 사용자 관련
        //------------------------------------------------------------------//
        // 사용자 Level 
        public static int GFn_GetUserLevel()
        {
            return Level;
        }

        // Supervisor Level 여부 (admin, supervisor : true else false)
        public static bool GFn_IsSupervisor()
        {
            int iSuperLevel = GFn_GetCodeSupervisorLevel("USR_LV", "SUPERVISOR");   // Supervisor Level
            int iUserLevel = GFn_GetUserLevel();                                    // User Level

            bool rv = iUserLevel >= iSuperLevel;  // Supervisor 등급 여부 
            return rv;
        }

        //------------------------------------------------------------------//
        // 메뉴 설정 관련 - 사용자 사용 등급에 맞게 메뉴를 보여주고 숨기거나 활성/비활성화한다.
        // Menu의 Tag 속성이용
        // Tag = 보여주는 등급,활성화 등급 ( 구분자 ',' ) 
        // ex : Tag = 2,3   ---> 2등급 이상 보여주고 3등급이상 활성화(실행가능) 함.
        //      Tag = null  ---> 누구나 보여지고 누구나 실행 가능 
        //------------------------------------------------------------------//
        public static void GFn_SetMenu(int iLevel, dynamic mn)
        {
            //if (iLevel == null) return;
            if (mn == null) return;

            String strTmp = "";

            try
            {
                // Menu Control 
                if (mn is MenuStrip)
                {
                    MenuStrip MainMenu = mn as MenuStrip;
                    foreach (ToolStripMenuItem mi in MainMenu.Items)
                    {
                        GFn_SetMenu(iLevel, mi);
                    }
                }
                // Menu Item
                else if (mn is ToolStripMenuItem)
                {
                    ToolStripMenuItem mi = mn as ToolStripMenuItem;
                    // 자신에 대한 처리부터 하고
                    // Tag 지정 안한 것은 무조건 보이게
                    if (mi.Tag == null)
                    {
                        mi.Enabled = true;
                        mi.Visible = true;

                    }
                    else
                    {
                        String strTag = mi.Tag.ToString();
                        String[] strArrLevel = strTag.Split(',');
                        if (strArrLevel.Length == 0)
                        {
                            //
                        }
                        else if (strArrLevel.Length == 2)
                        {
                            int iViewLevel = Convert.ToInt32(strArrLevel[0]);
                            int iExecLevel = Convert.ToInt32(strArrLevel[1]);

                            mi.Visible = iViewLevel <= iLevel ? true : false;
                            mi.Enabled = iExecLevel <= iLevel ? true : false;
                        }
                    }

                    // 하위 메뉴(아이템) 처리 
                    foreach (dynamic dynmicItem in mi.DropDownItems)
                    {
                        // Separator
                        if (dynmicItem is ToolStripSeparator)
                        {
                            ToolStripSeparator ddmi = dynmicItem as ToolStripSeparator;

                            // Tag 지정 안한 것은 무조건 보이게
                            if (ddmi.Tag == null)
                            {
                                ddmi.Enabled = true;
                                ddmi.Visible = true;
                                continue;
                            }

                            String strTag = ddmi.Tag.ToString();
                            String[] strArrLevel = strTag.Split(',');
                            if (strArrLevel.Length == 0)
                            {
                                //
                            }
                            else if (strArrLevel.Length == 2)
                            {
                                int iViewLevel = Convert.ToInt32(strArrLevel[0]);
                                int iExecLevel = Convert.ToInt32(strArrLevel[1]);

                                ddmi.Visible = iViewLevel <= iLevel ? true : false;
                                ddmi.Enabled = iExecLevel <= iLevel ? true : false;
                            }

                        }
                        if (dynmicItem is ToolStripMenuItem)
                        {
                            ToolStripDropDownItem ddmi = dynmicItem as ToolStripDropDownItem;

                            // 서브 메뉴가 있으면 들어간다.
                            if (ddmi.HasDropDownItems) GFn_SetMenu(iLevel, ddmi);

                            // Tag 지정 안한 것은 모든 사람을 위한 메뉴
                            if (ddmi.Tag == null)
                            {
                                ddmi.Enabled = true;
                                ddmi.Visible = true;
                                continue;
                            }

                            String strTag = ddmi.Tag.ToString();
                            String[] strArrLevel = strTag.Split(',');
                            if (strArrLevel.Length == 0)
                            {
                                //
                            }
                            else if (strArrLevel.Length == 2)
                            {
                                int iViewLevel = Convert.ToInt32(strArrLevel[0]);
                                int iExecLevel = Convert.ToInt32(strArrLevel[1]);

                                ddmi.Visible = iViewLevel <= iLevel ? true : false;
                                ddmi.Enabled = iExecLevel <= iLevel ? true : false;
                            }
                        }

                    }
                }
                // ...
                else
                {
                    // 
                }

            }
            catch (Exception ex)
            {
                //
                strTmp = ex.Message;
            }
            finally
            {
                //
            }
        }


        //------------------------------------------------------------------//
        // 코드 관련
        //------------------------------------------------------------------//
        // 특정 코드 그룹의 코드 조회 
        public static int GFn_GetCodeDS(String strCodeGrp, String strCode, ref DataSet ds, bool bUseOnly, bool bIncludeNull)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetCodeDS(SqlConn, strCodeGrp, strCode, ref ds, bUseOnly, bIncludeNull);
            return rv;
        }

        // 특정 코드 조회 
        public static String GFn_GetCodeValue(String strCodeGrp, String strCode, bool bUseOnly)
        {
            String strValue = DBCodes.Codes.GFn_GetCodeValue(SqlConn, strCodeGrp, strCode, bUseOnly);
            return strValue;
        }

        // 사용자 업무 Role 의 Level 조회 
        public static int GFn_GetCodeSupervisorLevel(String strCodeGrp, String strCode)
        {
            String strLevel = GFn_GetCodeValue(strCodeGrp, strCode, true);
            if (strLevel != null) return Convert.ToInt32(strLevel);
            return -1;
        }

        // 사용자 권한 등급 코드 조회 : return columns - role_tp, role_nm, level
        public static int GFn_GetPermissionLevelDS(ref DataSet ds)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetPermissionLevelDS(SqlConn, ref ds, true);
            return rv;
        }

        // 자동차 maker 
        public static int GFn_GetVehicleMake(ref DataSet ds, String strSearch)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetVehicleMake(SqlConn, ref ds, strSearch);
            return rv;
        }

        // 특정 자동차 maker 
        public static String GFn_GetVehicleMakeValue(String strSearch)
        {
            String rv = DBCodes.Codes.GFn_GetVehicleMakeValue(SqlConn, strSearch, true);
            return rv;
        }

        // 자동차 type
        public static int GFn_GetVehicleType(ref DataSet ds, String strSearch)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetVehicleType(SqlConn, ref ds, strSearch);
            return rv;
        }

        // 특정 자동차 type
        public static String GFn_GetVehicleTypeValue(String strSearch)
        {
            String rv = DBCodes.Codes.GFn_GetVehicleTypeValue(SqlConn, strSearch, true);
            return rv;
        }
        //------------------------------------------------------------------//
        // Fine 매핑
        //------------------------------------------------------------------//
        // Fine Remapping
        public static int GFn_GetReMappingFine(ref DataSet ds, String strCourtCd, String strLocationCd, String strLimitSpd, String strRealSpd)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetReMappingFine(SqlConn, ref ds, strCourtCd, strLocationCd, strLimitSpd, strRealSpd);
            return rv;
        }


        //------------------------------------------------------------------//
        // Offence Code 관련
        //------------------------------------------------------------------//
        // Offence Code 조회
        public static int GFn_GetOffenceCodeDS(ref DataSet ds, bool bUseOnly)
        {
            ds.Clear();
            int rv = DBLibMngOffenceCd.Offence_code.GFn_GetOffenceCodeDS(SqlConn, ref ds, bUseOnly);
            return rv;
        }

        // Offence Code INSERT OR UPDATE
        public static int GFn_InsertOffencesCode(String[] strArrOffence_cd
                                               , String[] strArrReference_cd
                                               , String[] strArrLegislation_cd
                                               , String[] strArrCategory
                                               , String[] strArrSpeed_from
                                               , String[] strArrSpeed_to
                                               , String[] strArrFine
                                               , String[] strArrUse_yn
                                               , String[] strArrOffence_cd_org
                                               , String[] strArrRecState

                                               , ref String[] strArrResult
                                                )
        {

            int rv = DBLibMngOffenceCd.Offence_code.GFn_InsertOffencesCode(SqlConn, UserId
                                                                         , strArrOffence_cd
                                                                         , strArrReference_cd
                                                                         , strArrLegislation_cd
                                                                         , strArrCategory
                                                                         , strArrSpeed_from
                                                                         , strArrSpeed_to
                                                                         , strArrFine
                                                                         , strArrUse_yn
                                                                         , strArrOffence_cd_org
                                                                         , strArrRecState
                                                                         , ref strArrResult
                                                                          );
            return rv;
        }

        //------------------------------------------------------------------//
        // Location Code 관련
        //------------------------------------------------------------------//
        // Location Code 조회
        public static int GFn_GetLocationCodeDS(ref DataSet ds, bool bUseOnly, String sCourt = "")
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetLocationCodeDS(SqlConn, ref ds, bUseOnly, sCourt);
            return rv;
        }

        //// 특정 Location 코드가 존재하는지 확인
        //public static bool GFn_IsExistLocationCode(String strstrCode, ref int iSeq)
        //{
        //    iSeq = -1;
        //    bool rv = DBLibCdMngLocation.Location_code.GFn_IsExistLocationCode(SqlConn, strstrCode, ref iSeq);
        //    return rv;
        //}

        //// Location Code To Offence Code Mapping Data 조회
        //public static int GFn_GetOffenceMapDS(ref DataSet ds, String court, String location, int location_seq, bool bUseOnly)
        //{
        //    ds.Clear();
        //    int rv = DBLibCdMngLocation.Location_code.GFn_GetOffenceMapDS(SqlConn, ref ds, court, location, location_seq, bUseOnly);
        //    return rv;
        //}

        //------------------------------------------------------------------//
        // Location Mapping data 관련
        //------------------------------------------------------------------//
        // Location Mapping 조회
        public static int GFn_GetLocationMapDS(ref DataSet ds, bool bUseOnly)
        {
            ds.Clear();
            int rv = DBLibMngLocationMap.LocationMap.GFn_GetLocationMapDS(SqlConn, ref ds, bUseOnly);
            return rv;
        }

        // Offence Code 조회
        public static int GFn_GetOffenceCodeForMapDS(ref DataSet ds
                                                   , String strReferenceCd
                                                   , String strLegislationCd
                                                   , String strCategory
                                                   , bool bUseOnly
                                                    )
        {
            ds.Clear();
            int rv = DBLibMngLocationMap.Offence_code.GFn_GetOffenceCodeForMapDS(SqlConn, ref ds
                                                                               , strReferenceCd
                                                                               , strLegislationCd
                                                                               , strCategory
                                                                               , bUseOnly
                                                                                );
            return rv;
        }

        // Location Mapping Data INSERT OR UPDATE
        public static int GFn_InsertLocationMap(String[] strArrCourt
                                              , String[] strArrLocationCd
                                              , String[] strArrDirection
                                              , String[] strArrMapSeq
                                              , String[] strArrReference
                                              , String[] strArrLegislation
                                              , String[] strArrCategory
                                              , String[] strArrMapDesc
                                              , String[] strArrOffence_List

                                              , String[] strArrUse_yn

                                              , String[] strArrCourt_org
                                              , String[] strArrLocationCd_org
                                              , String[] strArrDirection_org
                                              , String[] strArrMapSeq_org

                                              , String[] strArrRecState

                                              , ref String[] strArrResult
                                               )
        {

            int rv = DBLibMngLocationMap.LocationMap.GFn_InsertLocationMap(SqlConn, UserId
                                                                         , strArrCourt
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
            return rv;
        }

        //------------------------------------------------------------------//
        // 사용자 관리 화면(iTopsMMngUser.exe) 관련
        //------------------------------------------------------------------//
        // 인적 정보 조회
        public static int GFn_GetPersonsDS(ref DataSet ds, bool bUseOnly)
        {
            ds.Clear();
            int rv = DBLibMngUser.Persons.GFn_GetPersonsDS(SqlConn, ref ds, bUseOnly);
            return rv;
        }

        // 인적 정보 추가, 수정, 삭제
        public static int GFn_InsertPersons(String[] strArrPrsnId
                                          , String[] strArrName
                                          , String[] strArrMobile
                                          , String[] strArrPhone
                                          , String[] strArrEmail
                                          , String[] strArrAddr

                                          , String[] strArrUserYN
                                          , String[] strArrUserID
                                          , String[] strArrUserPW
                                          , String[] strArrLevel
                                          , String[] strArrRoleTP
                                          , String[] strArrAllowedYN
                                          , String[] strArrFailCnt

                                          , String[] strArrPrsnId_ORG
                                          , String[] strArrUserYN_ORG
                                          , String[] strArrUserID_ORG

                                          , String[] strArrRecState
                                          , String[] strArrPrsnState
                                          , String[] strArrUserState

                                          , ref String[] strArrResult
                                           )
        {

            int rv = DBLibMngUser.Persons.GFn_InsertPersons(SqlConn, UserId
                                                          , strArrPrsnId
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
            return rv;
        }


        //------------------------------------------------------------------//
        // MainMenu 실행 관련
        //------------------------------------------------------------------//
        private static TabControl MainTab = null;


        // Child Process
        const int MAX_PROCESS = 100;
        static int ChildCnt = 0;
        static Process[] processes = new Process[MAX_PROCESS];

        // 닫아야할 TabPage
        static int CloseIndex = -1;
        public static int GFn_GetCloseTabIndex()
        {
            //if (ChildCnt <= 0) return -1;
            return CloseIndex;
        }
        public static void GFn_ClearCloseTabIndex()
        {
            CloseIndex = -1;
        }

        // 종료 이벤트 받기
        public static void ProcessExited(object source, EventArgs e)
        {
            int iPos = -1;
            for (int i = 0; i < ChildCnt; i++)
            {
                try
                {
                    // 종료된 프로세스를 찾는다.
                    if (processes[i].HasExited)
                    {

                        iPos = i;
                        // 이미 종료되서 할 필요 없다.
                        //processes[i].Dispose();
                        processes[iPos] = null;
                        for (int j = 0; j < 10; j++) // 10번만 시도
                        {
                            if (CloseIndex < 0)
                            {
                                CloseIndex = iPos;
                                break;
                            }
                            System.Threading.Thread.Sleep(10);
                        }

                        break;
                    }

                }
                catch (Exception ex)
                {
                    String strTmp = ex.Message;
                    MessageBox.Show(strTmp);
                }
            }

            // process 앞으로 채우기
            if (ChildCnt > 0)
            {
                for (int j = iPos; j < ChildCnt - 1; j++)
                {
                    if (processes[j + 1] != null)
                    {
                        processes[j] = processes[j + 1];
                        processes[j + 1] = null;

                    }
                }
                processes[ChildCnt - 1] = null;
                ChildCnt--;

            }


        }

        //------------------------------------------------------------------//
        // 초기화 및 서버정보 읽기
        //------------------------------------------------------------------//
        public static String GetUserId()
        {
            return UserId;
        }
        public static String GetUserName()
        {
            return UserNm;
        }

        public static String GetRegion()
        {
            return Region;
        }

        public static String GetBackground()
        {
            return Background;
        }
        //------------------------------------------------------------------//
        // 초기화 및 서버정보 읽기
        //------------------------------------------------------------------//
        public static void InitLib()
        {
            // FTP Connection Info
            FtpIP = "";
            FtpPort = "";

            // SQL Connection Info
            SqlIP = "";
            SqlPort = "";
            SqlSvrStr = "";

            SqlConn = null;

            // Connection Info
            UserId = "";
            PassWord = "";
            UserNm = "";
            Level = -1;
            FailCnt = 0;
            Connected = false;

            // Etc ... Background Image
            Background = "";

            // Child Process
            ChildCnt = 0;
            //ChildProcesses 

            Region = "";

            // Read Server Info
            ReadAppConfig();



        }

        //// 임시 - 단위 테스트용
        //public static void Fn_init()
        //{
        //    InitLib();
        //}

        //----------------------------------------------------------------------//
        // 서버 정보 읽기 
        //----------------------------------------------------------------------//
        private static void ReadAppConfig()
        {
            try
            {
                if (Application.ProductName == "iTopsMain") // Main만 App.Config에서 읽는다.
                {
                    // App.config 를 읽는다.
                    // FTP 접속 정보
                    if (FtpIP.Length == 0)
                    {
                        FtpIP = ConfigurationManager.AppSettings["FTP_IP"] ?? String.Empty;
                        FtpPort = ConfigurationManager.AppSettings["FTP_PORT"] ?? String.Empty;

                    }

                    // SQL 접속 정보
                    if (SqlIP.Length == 0)
                    {
                        SqlIP = ConfigurationManager.AppSettings["SQL_IP"] ?? String.Empty;
                        SqlPort = ConfigurationManager.AppSettings["SQL_PORT"] ?? String.Empty;
                        //SqlSvrStr = "server=" + SqlIP + ";uid=admin;pwd=@mbcisno1;database=TRAPEACE";
                        SqlSvrStr = String.Format("server={0};uid={1};pwd={2};database={3}"
                                                 , SqlIP
                                                 , SqlID
                                                 , SqlPWD
                                                 , SqlDB
                                                 );

                    }

                    // ALPR
                    Region = ConfigurationManager.AppSettings["REGION"] ?? String.Empty;

                    // Background Image
                    Background = ConfigurationManager.AppSettings["BACKGROUND"] ?? String.Empty;


                    // 레지스트리에 기록 한다.
                    Fn_SetServerInfoToReg();
                }
                else
                {
                    // 레지스트리에서 읽는다.
                    Fn_GetServerInfoFromReg();
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

        //----------------------------------------------------------------------//
        // 레지스트리 쓰기 ... iTopsMain에서 사용
        //----------------------------------------------------------------------//
        public static void Fn_SetServerInfoToReg()
        {
            try
            {

                // Key 설정 
                RegistryKey reg = Registry.CurrentUser.CreateSubKey(REG_KEY_PATH);
                if (reg == null) return;

                // data 쓰기
                reg.SetValue(REG_F_IP, FtpIP);
                reg.SetValue(REG_F_PORT, FtpPort);

                reg.SetValue(REG_S_IP, SqlIP);
                reg.SetValue(REG_S_PORT, SqlPort);

                reg.SetValue(REG_ALPR_REGION, Region);

                reg.SetValue(REG_BACKGROUND, Background);

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }


        }

        //----------------------------------------------------------------------//
        // 레지스트리 읽기
        //----------------------------------------------------------------------//
        public static void Fn_GetServerInfoFromReg()
        {
            try
            {

                // Key 설정 
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(REG_KEY_PATH);
                if (reg == null) return;

                // data 읽기
                FtpIP = Convert.ToString(reg.GetValue(REG_F_IP));
                FtpPort = Convert.ToString(reg.GetValue(REG_F_PORT));

                SqlIP = Convert.ToString(reg.GetValue(REG_S_IP));
                SqlPort = Convert.ToString(reg.GetValue(REG_S_PORT));
                SqlSvrStr = String.Format("server={0};uid={1};pwd={2};database={3}"
                                         , SqlIP
                                         , SqlID
                                         , SqlPWD
                                         , SqlDB
                                         );

                Region = Convert.ToString(reg.GetValue(REG_ALPR_REGION));

                Background = Convert.ToString(reg.GetValue(REG_BACKGROUND));

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }

        }

        //----------------------------------------------------------------------//
        // Sql Server Connect
        //----------------------------------------------------------------------//
        private static SqlConnection ConnectSql()
        {
            try
            {
                if (SqlSvrStr.Length == 0) return null;

                // 이미 연결되어 있으면 종료 후 재 접속 ???
                if (SqlConn != null) DisConnectSql();

                var Conn = new SqlConnection(SqlSvrStr);
                if (Conn != null)
                {
                    Conn.Open();
                    return Conn;
                }
                else return null;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                MessageBox.Show("Unable to connect to database.\n\n"
                              + "Check the status of the data server or network and try again."
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return null;
            }
            finally
            {

            }
        }

        //----------------------------------------------------------------------//
        // Sql Server DisConnect
        //----------------------------------------------------------------------//
        private static void DisConnectSql()
        {

            if (SqlConn != null) SqlConn.Close();
        }

        //----------------------------------------------------------------------//
        // Login
        //----------------------------------------------------------------------//
        //public static bool GFn_Connect()
        //{
        //    if (UserId.Length == 0) return false;
        //    if (PassWord.Length == 0) return false;

        //    MessageBox.Show(UserId + "/ " + PassWord);
        //    return Fn_Login(UserId, PassWord);
        //}
        public static bool GFn_Login(String Encode)
        {

            if (!Fn_SetUser(Encode)) return false;
            if (UserId.Length == 0) return false;
            if (PassWord.Length == 0) return false;

            return Fn_Login(UserId, PassWord);
        }

        public static bool Fn_Login(String id, String password)
        {
            if (id.Length == 0) return false;
            if (password.Length == 0) return false;

            //// 1. 먼저 자식들을 종료 시킨다.
            //Lib.GFn_CloseChild();

            // 먼저 초기화 및 서버 정보 읽기
            InitLib();

            // DB 접속
            SqlConn = ConnectSql();

            if (SqlConn == null)
            {
                return false;
            }
            if (SqlConn.State == ConnectionState.Closed) SqlConn.Open();


            // SQLText 조립
            String SQLText = String.Format("SELECT U.fail_cnt   fail_cnt   "
                                          + "     , U.allowed    allowed    "
                                          + "     , U.user_pw    pwd        "
                                          + "     , U.level      level      "
                                          + "     , P.name       name       "
                                          + "     , P.dept       dept       "
                                          + "  FROM USERS        U          "
                                          + "     , PERSONS      P          "
                                          + " WHERE U.user_id    = '{0}'    "
                                          + "   AND U.prsn_id    = P.prsn_id"
                                          , id);

            // 조회
            try
            {
                SqlCommand sqlComm = new SqlCommand(SQLText, SqlConn);
                var Users = sqlComm.ExecuteReader();
                try
                {
                    // 0건
                    if (!Users.HasRows)
                    {
                        MessageBox.Show("ID is wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (Users.Read())
                    {
                        // 패스워드 5회 이상 오류 ... 권한 없어짐
                        if (Users[1].ToString().ToUpper() == "FALSE" && Int32.Parse(Users[0].ToString()) >= 5)
                        {
                            MessageBox.Show("You have entered the wrong password more than 5 times.\n" +
                                            "Contact your administrator!!!", "Error"
                                          , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        // 원래 사용 권한을 부여 받지 못했음
                        else if (Users[1].ToString().ToUpper() == "FALSE" && Int32.Parse(Users[0].ToString()) < 5)
                        {
                            MessageBox.Show("You don't have permission.\n" +
                                            "Contact your administrator!!!", "Error"
                                          , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        // 패스워드 불일치 
                        else if (Users[2].ToString() != password)
                        {
                            MessageBox.Show("Invalid password.\n" +
                                            "Please try again!!!", "Error"
                                          , MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // 패스워드 실패 카운트 증가 시키기
                            Users.Close();
                            if (IncreaseFailCnt(id) < 0)
                            {
                                // 실패시 ... ??

                            }
                            return false;
                        }

                        //  성공
                        Level = Int32.Parse(Users[3].ToString());
                        UserId = id;
                        PassWord = password;
                        UserNm = Users[4].ToString();
                        Connected = true;

                    }

                }
                catch (Exception e)
                {
                    // Build 시 메시지 나오지 말라고 ...
                    String strTmp = e.Message;

                    MessageBox.Show(strTmp
                                  , "Error"
                                  , MessageBoxButtons.OK
                                  , MessageBoxIcon.Error);
                    return false;

                }
                finally
                {
                    if (Users != null) Users.Close();
                }


                // 접속 성공시 실패 카운트 초기화
                if (InitFailCnt(id) < 0)
                {
                    // 실패시 ... ??
                }
                //MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                // Build 시 메시지 나오지 말라고 ...
                String strTmp = e.Message;
                return false;

            }
            return true;
        }

        //----------------------------------------------------------------------//
        // 접속 실패 카운트 1증가
        //----------------------------------------------------------------------//
        private static int IncreaseFailCnt(String id)
        {
            if (SqlConn == null)
            {
                return -1;
            }
            if (SqlConn.State == ConnectionState.Closed) SqlConn.Open();

            // SQLText 조립
            String SQLText = String.Format("UPDATE USERS                                        "
                                          + "   SET fail_cnt = fail_cnt + 1                      "
                                          + "     , allowed = CASE allowed                       "
                                          + "                    WHEN 0 THEN 0                   "
                                          + "                    ELSE CASE SIGN(fail_cnt - 4)    "
                                          + "                              WHEN - 1 THEN 1       "
                                          + "                              ELSE 0                "
                                          + "                         END                        "
                                          + "               END                                  "
                                          + " WHERE user_id = '{0}'                              "
                                          , id
                                          );

            // 실행
            int rv = 0;
            SqlCommand sqlComm = new SqlCommand(SQLText, SqlConn);
            try
            {
                rv = sqlComm.ExecuteNonQuery();

                if (rv == 1) return rv;

            }
            catch (Exception e)
            {
                // Build 시 메시지 나오지 말라고 ...
                String strTmp = e.Message;
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                //sqlComm.Clone();
            }

            return rv;

        }

        //----------------------------------------------------------------------//
        // 접속 실패 카운트 초기화 
        //----------------------------------------------------------------------//
        private static int InitFailCnt(String id)
        {
            if (SqlConn == null)
            {
                return -1;
            }
            if (SqlConn.State == ConnectionState.Closed) SqlConn.Open();

            // SQLText 조립
            String SQLText = String.Format("UPDATE USERS            "
                                          + "   SET fail_cnt = 0    "
                                          + " WHERE user_id = '{0}' "
                                          , id
                                          );

            // 실행
            int rv = 0;
            SqlCommand sqlComm = new SqlCommand(SQLText, SqlConn);
            try
            {
                rv = sqlComm.ExecuteNonQuery();

                if (rv == 1) return rv;

            }
            catch (Exception e)
            {
                // Build 시 메시지 나오지 말라고 ...
                String strTmp = e.Message;
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                //sqlComm.Clone();
            }

            return rv;

        }

        //----------------------------------------------------------------------//
        // 접속 종료
        //----------------------------------------------------------------------//
        public static void Fn_DisConnSqlSvr()
        {
            DisConnectSql();
        }

        //----------------------------------------------------------------------//
        // REGULATIONS Table 관련 SQL - 단속영상 파일 존재 확인 
        //----------------------------------------------------------------------//
        public static String GFn_GetRegulationImageFile(String strFileName)
        {
            String strValue = DBLibUpRGLTN.Regulations.GFn_GetRegulationImageFile(SqlConn, strFileName);
            return strValue;
        }

        //----------------------------------------------------------------------//
        // REGULATIONS Table 관련 SQL - INSERT : LTI
        //----------------------------------------------------------------------//
        public static int Fn_InsertRegulations(ListViewItem Items, String strBranch, String strCourt, ref String strRV)
        {
            String svrImgPath = iTopsLib.Lib.GFn_GetSvrImgPath(Items.SubItems[(int)LvLTI.CLIP_DATE].Text, '-');

            int rv = DBLibUpRGLTN.Regulations.Insert(SqlConn, Items, UserId, svrImgPath, strBranch, strCourt, ref strRV);
            return rv;
        }

        //----------------------------------------------------------------------//
        // REGULATIONS Table 관련 SQL - INSERT : Com Laser
        //----------------------------------------------------------------------//
        public static int Fn_InsertRegulationsCL(ListViewItem Items, ref String strRV)
        {
            String svrImgPath = iTopsLib.Lib.GFn_GetSvrImgPath(Items.SubItems[(int)LvCL.DATE].Text.Trim(), '-');

            int rv = DBLibUpRGLTN.Regulations.InsertCL(SqlConn, Items, UserId, svrImgPath, ref strRV);
            return rv;
        }

        //----------------------------------------------------------------------//
        // Inspector 조회 SQL - Read 
        //----------------------------------------------------------------------//
        public static int GFn_SelectInspector(DataSet ds)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetInspector(SqlConn, ref ds);
            return rv;
        }

        //----------------------------------------------------------------------//
        // Officer 조회 SQL - Read 
        //----------------------------------------------------------------------//
        public static int GFn_SelectOfficer(DataSet ds)
        {
            ds.Clear();
            int rv = DBCodes.Codes.GFn_GetOfficer(SqlConn, ref ds);
            return rv;
        }


        //----------------------------------------------------------------------//
        // Regulations 조회 SQL - Read - 분배하기 위한 data
        //----------------------------------------------------------------------//
        public static int GFn_SelectRegulations(DataSet ds
                                               , String strDistributeYN
                                               , String strDateYN
                                               , String strStart
                                               , String strEnd
                                               , String strUseUserID
                                               , String strUserID
                                               )
        {
            ds.Clear();
            int rv = DBLibDistribute.Regulations.GFn_SelectRegulations(SqlConn
                                                                      , strDistributeYN
                                                                      , strDateYN
                                                                      , strStart
                                                                      , strEnd
                                                                      , strUseUserID
                                                                      , strUserID
                                                                      , ref ds);
            return rv;
        }

        //----------------------------------------------------------------------//
        // CONTRAVENTIONS Table 관련 SQL - INSERT 
        //----------------------------------------------------------------------//
        public static int GFn_InsertOffencesInDistribute(ref String[] strArrstrArrRgltn_id
                                                        , ref String[] strArrOffence_id
                                                        , ref String[] strArrInspector_id
                                                        , ref String[] strArrResult
                                                        )
        {

            int rv = DBLibDistribute.Offences.InsertOffences(SqlConn, UserId
                                                            , ref strArrstrArrRgltn_id
                                                            , ref strArrOffence_id
                                                            , ref strArrInspector_id
                                                            , ref strArrResult
                                                            );
            return rv;
        }


        //----------------------------------------------------------------------//
        // CONTRAVENTIONS Table 관련 SQL - Update By Inspector
        //----------------------------------------------------------------------//
        public static int GFn_UpdateContraventionsByInspector(bool bIsSupervisor
                                                             , String[] strFine
                                                             , String[] strArrCourt
                                                             , String[] strArrOffence_id
                                                             , String[] strArrInspection_plate

                                                             , String[] strArrInspection_make_cd
                                                             , String[] strArrInspection_make
                                                             , String[] strArrInspection_type_cd
                                                             , String[] strArrInspection_type
                                                             , String[] strArrInspection_color

                                                             , String[] strArrInspection_rn_cd
                                                             , String[] strArrInspection_rn_etc
                                                             , String[] strArrInspection_end_yn

                                                             , String[] strArrSupervisorPermit_cd
                                                             , String[] strArrSupervisorRejectDesc

                                                             , ref String[] strArrResult
                                                             )
        {

            int rv = DBLibInspection.Offences.UpdateInspector(SqlConn, UserId
                                                             , bIsSupervisor
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
            return rv;
        }


        //----------------------------------------------------------------------//
        // Contraventiona 조회 SQL - Read - Inspection 대상 자료 조회 
        //----------------------------------------------------------------------//
        public static int GFn_SelectInspection(DataSet ds
                                              , String strCheckYN_SV
                                              , String strCheckYN
                                              , String strDateYN
                                              , String strStart
                                              , String strEnd
                                              , String strUseOfficerID
                                              , String strOfficerID
                                              , String strUseInspectorID
                                              , String strInspectorID
                                              )
        {
            ds.Clear();
            int rv = DBLibInspection.Offences.GFn_SelectInspection(SqlConn
                                                                  , strCheckYN_SV
                                                                  , strCheckYN
                                                                  , strDateYN
                                                                  , strStart
                                                                  , strEnd
                                                                  , strUseOfficerID
                                                                  , strOfficerID
                                                                  , GFn_IsSupervisor()
                                                                  , strUseInspectorID
                                                                  , strInspectorID
                                                                  , UserId
                                                                  , ref ds
                                                                  );
            return rv;
        }


        //----------------------------------------------------------------------//
        // 날짜 형식 판단
        // 이렇게 길게 할 필요 있나??
        // DB Query로 한방에 하는게 ....
        // return : true - 한국식 또는 미국식 날짜 형식 맞음 (YYYY/MM/DD or MM/DD/YYYY)
        //                 dateFmt - KR of US
        //          false - 날짜 형식 아님
        //----------------------------------------------------------------------//
        private static bool Fn_IsDateFormat(String date, char delimiter, ref int dateFmt)
        {
            // 준비
            bool rv = false;
            int rvFmt = (int)DateFormat.WRONG;

            dateFmt = rvFmt;

            // 입력값 검증 - 자릿수 확인 
            if (date.Length == 0) return rv;                            // 확인할 것이 없다.
            else if (date.Length != 10) return rv;                      // YYYY/MM/DD or MM/DD/YYYY 가 아니다.

            // 검증 - 분리
            String[] arrDate = date.Split(delimiter);
            if (arrDate.Count() != 3) return rv;                        // 년, 월, 일 로 분리가 안됨

            // 검증 - 준비
            String sYear = "";
            String sMon = "";
            String sDay = "";
            if (arrDate[0].Length == 4)                                 // 한국식(yyyy/mm/dd) 후보
            {
                rvFmt = (int)DateFormat.KR;
                sYear = arrDate[0];
                sMon = arrDate[1];
                sDay = arrDate[2];
            }
            //else if (arrDate[2].Length == 4)                            // 미국식(mm/dd/yyyy) 후보
            //{
            //    rvFmt = (int)DateFormat.US;
            //    sYear = arrDate[2];
            //    sMon = arrDate[0];
            //    sDay = arrDate[1];
            //}
            else if (arrDate[2].Length == 4)                            // 미국식(dd/mm/yyyy) 후보
            {
                rvFmt = (int)DateFormat.US;
                sYear = arrDate[2];
                sMon = arrDate[1];
                sDay = arrDate[0];
            }
            else return rv;                                             // 날짜 형식이 한국식이나 미국식이 아님

            // 검증 - 년, 월, 일
            int iYear, iMon, iDay;
            try
            {
                // 문자가 null 이면 무시 함
                //iYear = Convert.ToInt32(sYear); 

                // 문자가 null 이면 에러 남
                iYear = Int32.Parse(sYear);         // 년도
                iMon = Int32.Parse(sMon);           // 월
                iDay = Int32.Parse(sDay);           // 일

                // 1900 년 이전은 취급 안함
                if (iYear <= 1900) return rv;

                // 1 ~ 12 월 까지만 인정 
                if (iMon < 1 || iMon > 12) return rv;

                // 일자는 1보다 커야지
                if (iDay < 1) return rv;


                // 1, 3, 5, 7, 8, 10, 12 월은 31 까지 
                if (
                     (iMon == 1 || iMon == 3 || iMon == 5
                      || iMon == 7 || iMon == 8 || iMon == 10 || iMon == 12
                     )
                  && iDay > 31
                     ) return rv;
                // 4, 6, 9, 11 월은 30일까지
                else if (
                          (iMon == 4 || iMon == 6 || iMon == 9 || iMon == 11)
                       && iDay > 30
                        ) return rv;
                // 2 은 계산하기 귀찮으니까 29까지로 하자!!!
                else if (iMon == 2 && iDay > 29) return rv;

                rv = true;

            }
            // 문자 -> 숫자 안되거나 범위 밖이면 날짜 아님
            catch (Exception e)
            {

                String sTmp = e.Message;
                return rv;

            }

            // 여기까지 오면 날짜 형식으로 인정
            dateFmt = rvFmt;
            return rv;
        }

        //----------------------------------------------------------------------//
        // 날짜 형식 변경 : date -> KR or US Format( MM/DD/YYYY or YYYY/MM/DD )
        //----------------------------------------------------------------------//
        public static bool GFn_FormatDate(String date, int convertFmt, ref String convert_date, char delimiter = '/')
        {
            // 반환 할 값 초기화
            convert_date = "";

            // 날짜 형식인지 판단
            int dateFmt = 0;
            bool rv = Fn_IsDateFormat(date, delimiter, ref dateFmt);

            // 날짜 형식 아니면 종료
            if (!rv) return rv;

            // 입력값이 원하는 형식이면 종료
            if (convertFmt == dateFmt)
            {
                String[] arrTmp = date.Split(delimiter);
                convert_date = arrTmp[0] + "/" + arrTmp[1] + "/" + arrTmp[2];
                return rv;

            }

            // 형식 변환
            String[] arrDate = date.Split(delimiter);
            //if (convertFmt == (int)DateFormat.KR)  // MM/DD/YYYY ---> YYYY/MM/DD
            //{

            //    convert_date = arrDate[2] + "/" + arrDate[0] + "/" + arrDate[1];  // 년/월/일

            //}
            //else if (convertFmt == (int)DateFormat.US) // YYYY/MM/DD ---> MM/DD/YYYY
            //{

            //    convert_date = arrDate[1] + "/" + arrDate[2] + "/" + arrDate[0];  // 월/일/년

            //}
            if (convertFmt == (int)DateFormat.KR)  // DD/MM/YYYY ---> YYYY/MM/DD
            {

                convert_date = arrDate[2] + "/" + arrDate[1] + "/" + arrDate[0];  // 년/월/일

            }
            else if (convertFmt == (int)DateFormat.US) // YYYY/MM/DD ---> DD/MM/YYYY
            {

                convert_date = arrDate[2] + "/" + arrDate[1] + "/" + arrDate[0];  // 월/일/년

            }
            else
            {   // 여기 올 일 없지만 ...

                convert_date = "";
                return false;

            }

            return rv;

        }

        //----------------------------------------------------------------------//
        // 날짜를 기준으로 서버의 이미지 경로 반환 ( 순수 경로 ) 
        //----------------------------------------------------------------------//
        public static String GFn_GetSvrImgPath(String date, char delimeter = '/')
        {
            String sResult = "";

            if (date.Length == 0) return sResult;                            // 변환할 것이 없다.

            String sToDate = "";
            bool bRv = GFn_FormatDate(date, (int)DateFormat.KR, ref sToDate, delimeter);
            if (!bRv) return sResult;

            sResult = FtpHOME + sToDate + "/";

            return sResult;
        }

        //----------------------------------------------------------------------//
        // 날짜와 파일명을 받아 서버의 FTP 경로 전체 반환 
        //----------------------------------------------------------------------//
        public static String GFn_GetFtpFullName(String date, String filename)
        {
            if (date.Length == 0) return date;
            if (filename.Length == 0) return filename;

            String sPath = GFn_GetSvrImgPath(date);
            if (sPath.Length == 0) return "";

            String ftpFullName = "ftp://" + FtpIP + sPath + filename;

            return ftpFullName;

        }

        //----------------------------------------------------------------------//
        // 지정된 폴더의 파일 목록 반환
        //----------------------------------------------------------------------//
        public static int GetFileList(String strPath
                                     , ref String[] arrFiles
                                     , bool doSubDir
                                     )
        {
            // 입력값 확인 
            if (strPath.Length == 0) return 0;

            try
            {
                String[] strFiles = Directory.GetFiles(strPath);

                if (strFiles.Count() == 0) return 0;
                else arrFiles = strFiles;

                return arrFiles.Count();
            }
            catch (Exception e)
            {
                String tmp = e.Message;
                return 0;
            }

        }

        //----------------------------------------------------------------------//
        // 전체파일 경로에서 Path 와 filename 분리 -- 필요 없을듯 ... 지우자!!!
        //----------------------------------------------------------------------//
        public static bool SplitPathFile(String strFullName
                                        , ref String strPath
                                        , ref String strFileName
                                        , ref String strExt
                                        )
        {

            if (strFullName.Length == 0) return false;

            //
            try
            {
                strPath = Path.GetDirectoryName(strFullName);
                strFileName = Path.GetFileName(strFullName);
                strExt = Path.GetExtension(strFullName);

                return true;
            }
            catch (Exception e)
            {
                // 
                String tmp = e.Message;

                return false;
            }


        }

        //----------------------------------------------------------------------//
        // 파일 다운로드 
        //----------------------------------------------------------------------//
        public static void DownloadFile(String svrFile
                                       , String cliPath
                                       , String cliFile
                                       , bool showMsg = false
                                       )
        {

            // 파일 다운로드 
            if (iTopsFTP.FTP.Fn_FileDownLoad(FtpIP, svrFile, FtpID, FtpPWD, cliPath, cliFile, showMsg))
            {
                // 성공
            }
            else
            {
                // 실패
            }

        }

        //----------------------------------------------------------------------//
        // 파일 업로드 
        //----------------------------------------------------------------------//
        public static bool UploadFile(String strDate
                                     , String svrFile
                                     , String cliPath
                                     , String cliFile
                                     , bool showMsg = false
                                     )
        {

            // 저장할 서버의 경로 및 파일명 조립 
            //String svrPath = FtpHOME + "2019/05/24/";
            //String svrPath = FtpHOME + strDate + "/";
            String svrPath = GFn_GetSvrImgPath(strDate);


            // 파일 업로드 
            //if (iTopsFTP.FTP.Fn_FileUpLoad(FtpIP, svrPath, svrFile, FtpID, FtpPWD, cliPath, cliFile, showMsg))
            //{
            //    // 성공
            //}
            //else
            //{
            //    // 실패
            //}
            return iTopsFTP.FTP.Fn_FileUpLoad(FtpIP, svrPath, svrFile, FtpID, FtpPWD, cliPath, cliFile, showMsg);
        }

        ////----------------------------------------------------------------------//
        //// 문자열 Delimeter 기준으로 자르기
        //// 함수로 만들 필요가 있을까??? : str.Substring(0, str.Length - 1).Split('/');
        ////----------------------------------------------------------------------//
        //public static void SplitStr( String source
        //                           , String delimeter
        //                           , ref String[] arrStr
        //                           , int iStart = 0
        //                           , int iEnd = 9999
        //                           )
        //{
        //    //
        //}

        // MDI Child 관련 ... 나중에 ...
        //----------------------------------------------------------------------//
        // 외부 프로그램 실행하여 MdiChild로 넣기
        //----------------------------------------------------------------------//
        //public static bool Fn_ExecChild( Form MainFrom
        public static bool GFn_ExecChild(ref TabControl parent
                                        //, object sender
                                        //, EventArgs e
                                        , String filename
                                        , String extention
                                        , int win_state
                                        //, int wait_time = 300
                                        , PictureBox picBack
                                        , bool child_yn = true
                                        )
        {
            // parameter check
            if (filename.Length == 0) return false;
            if (extention.Length == 0) return false;
            if (extention.ToUpper() != ".EXE" && extention.ToUpper() != ".DLL") return false;

            // Main의 TabControl 받아 보관하기 
            if (parent == null) return false;
            if (MainTab == null) MainTab = parent;


            //  실행 프로그램 호출인 경우 
            if (extention.ToUpper() == ".EXE")
            {


                // 기존에 실행 되어 있으면 맨앞에 보여주기
                IntPtr handle = IntPtr.Zero;
                foreach (TabPage page in MainTab.TabPages)
                {
                    if (page.Text != filename) continue;

                    handle = (IntPtr)page.Tag;
                    MainTab.SelectedTab = page;
                    //page.get
                    return true;
                }

                // 기존에 없으면 신규로 실행 
                if (handle == IntPtr.Zero)
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    Process ps = new Process();

                    try
                    {
                        int iCnt = MainTab.TabCount;

                        // TabPage  하나 추가 
                        TabPage page = new TabPage();
                        page.DoubleBuffered(true);  // 속도 개선
                        page.Text = filename;


                        //=======================================
                        // 배경 이미지 설정
                        if (picBack.Image != null && picBack.Image != null)
                        {
                            Image img = (picBack.Image.Clone() as Bitmap);
                            try
                            {
                                page.BackgroundImage = new Bitmap(picBack.Width, picBack.Height);
                                using (Graphics g = Graphics.FromImage(page.BackgroundImage))
                                {

                                    if (img != null)
                                    {

                                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                        g.DrawImage(img
                                                  , new Rectangle(0, 0, page.BackgroundImage.Width, page.BackgroundImage.Height)
                                                  , new Rectangle(0, 0, img.Width, img.Height)
                                                  , GraphicsUnit.Pixel
                                                    );
                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                String strTmp = ex.Message;
                            }
                            finally
                            {
                                img.Dispose();
                            }

                        }
                        //=======================================


                        // Process 추가 
                        //psi.FileName = filename + "." + extention;
                        psi.FileName = filename + extention;
                        psi.RedirectStandardOutput = true;
                        psi.UseShellExecute = false;
                        psi.Arguments = GFn_Encoding(UserId + " " + PassWord);


                        ps.StartInfo = psi;
                        ps.EnableRaisingEvents = true;
                        ps.Exited += new EventHandler(ProcessExited);
                        ps.Start();

                        //// TabPage 생성해서 그 위에 올려 주기
                        page.Tag = (IntPtr)ps.Handle;
                        MainTab.TabPages.Add(page);


                        // TabPage가 완전히 생성될때까지 기다린다.
                        while (MainTab.TabCount < (iCnt + 1)) System.Threading.Thread.Sleep(10);

                        MainTab.SelectedTab = page;
                        MainTab.Refresh();


                        //ShowWindow(ps.MainWindowHandle, SW_HIDE);

                        ////
                        ////psWaitForInputIdle(1000);// < ---왜 안먹냐 ?? - 메시지 루프가 있는 프로세스에만 작동한다고 함.
                        //ps.WaitForInputIdle(wait_time);

                        //System.Threading.Thread.Sleep(wait_time);

                        //SetParent(ps.MainWindowHandle, page.Handle);

                        ////ShowWindow(ps.MainWindowHandle, SW_SHOWMAXIMIZED);
                        //ShowWindow(ps.MainWindowHandle, win_state);

                        ////SetForegroundWindow(ps.MainWindowHandle);
                        //SetForegroundWindow(ps.Handle);

                        if (child_yn)
                        {
                            Fn_ChangeParent(filename, (IntPtr)ps.MainWindowHandle, (IntPtr)page.Handle, win_state);
                        }
                        else
                        {
                            ShowWindow(ps.MainWindowHandle, win_state);

                        }

                        processes[ChildCnt] = ps;   // 실행한 프로세스 보관
                        ChildCnt++;

                    }
                    catch (Exception ex)
                    {
                        String tmpStr = ex.Message;
                        return false;
                    }
                    finally
                    {
                        //ps.Close();
                        //ps.Dispose();
                    } // end try

                    return true;
                }
                return true;



                //ProcessStartInfo psi = new ProcessStartInfo();
                //Process ps = new Process();

                //try
                //{

                //    //  먼저 기존에 실행중인 Child 
                //    psi.FileName = filename + "." + extention;
                //    psi.RedirectStandardOutput = true;
                //    psi.UseShellExecute = false;

                //    ps.StartInfo = psi;
                //    ps.EnableRaisingEvents = true;


                //    ////ps.Exited += (sender, e) =>
                //    //ps.Exited += (sender , e) =>
                //    //{
                //    //    MessageBox.Show("프로세스 종료");

                //    //};

                //    Process[] is_run = Process.GetProcessesByName(filename);

                //    IntPtr handle = IntPtr.Zero;

                //    if (is_run.Count() == 0)        // 처음 실행인 경우 
                //    {
                //        ps.Start();
                //        handle = ps.Handle;

                //        //psWaitForInputIdle(1000);// < ---왜 안먹냐 ?? - 메시지 루프가 있는 프로세스에만 작동한다고 함.
                //        System.Threading.Thread.Sleep(wait_time);

                //        SetParent(ps.MainWindowHandle, parent);

                //        ShowWindow(handle, SW_SHOWNORMAL);
                //        SetForegroundWindow(handle);

                //    }
                //    //else                            // 이미 실행 중이면 
                //    //{

                //    //    ShowWindow(is_run[0].Handle, SW_SHOWNORMAL);
                //    //    SetForegroundWindow(is_run[0].Handle);

                //    //}

                //}
                //finally
                //{
                //    ps.Close();
                //    ps.Dispose();

                //}


            }
            else if (extention.ToUpper() == ".DLL")
            {
                // dll 호출인 경우 ... 나중에 필요하면 만들기 
            }

            return true;
        }

        //----------------------------------------------------------------------//
        // 외부 프로그램 실행 후 Main의 MDIChild로 Parent 변경
        //----------------------------------------------------------------------//
        private static void Fn_ChangeParent(String child_name, IntPtr child, IntPtr parent, int win_state)
        {

            try
            {
                int iCnt = -1;
                while (true)
                {
                    iCnt++;

                    //System.Threading.Thread.Sleep(100);

                    Process[] p = Process.GetProcessesByName(child_name);
                    if (p.GetLength(0) == 0) continue;
                    else
                    {

                        using (PerformanceCounter pcP = new PerformanceCounter("Process", "% Processor Time", child_name))
                        {
                            pcP.NextValue();
                            System.Threading.Thread.Sleep(1000);
                            if (pcP.NextValue() > 0) continue;

                            // 전부다 생성 되었다 ... 부모를 지정해 준다.
                            //SetParent(child, parent);
                            SetParent(p[0].MainWindowHandle, parent);
                            ShowWindow(p[0].MainWindowHandle, win_state);
                            SetForegroundWindow(p[0].MainWindowHandle);

                            // 마지막으로 확인 ... ???
                            IntPtr pParent = GetParent(p[0].MainWindowHandle);
                            if (pParent == null)
                            {
                                MessageBox.Show(String.Format("Fail : parent[{0}] child_parent[{1}]", Convert.ToString(parent), Convert.ToString(pParent)));
                                continue;
                            }

                            //MessageBox.Show(String.Format("Sucess : parent[{0}] child_parent[{1}]", Convert.ToString(parent), Convert.ToString(pParent)));


                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 
                String strTmp = ex.Message;

            }
            finally
            {
                //
            }
        }

        //----------------------------------------------------------------------//
        // MdiChild 종료 시키기
        //----------------------------------------------------------------------//
        public static bool GFn_CloseChild()
        {

            if (ChildCnt == 0) return true;
            try
            {
                for (int i = 0; i < ChildCnt; i++)
                {
                    //processes[i].Kill();
                    ////processes[i].Dispose();
                    ///
                    processes[i].Exited -= new EventHandler(ProcessExited);
                    //processes[i].CloseMainWindow();
                    processes[i].Kill();
                    processes[i] = null;
                }
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                return false;
            }
            finally
            {

            }

            return true;
        }

        //--------------------------------------------------------------------------//
        // 마우스 위치의 컨트롤 찾기 및 기타 마우스 관련 
        //--------------------------------------------------------------------------//
        //// 마우스 커서 위치의 컨트롤 찾기
        //public static Control FindControlAtCursor(Form form)
        //{
        //    Point pos = Cursor.Position;
        //    if (form.Bounds.Contains(pos))
        //        return FindControlAtPoint(form, form.PointToClient(Cursor.Position));
        //    return null;
        //}

        // 마우스 커서 위치의 컨트롤 찾기
        public static Control FindControlAtCursor(Form form)
        {
            Point pos = Cursor.Position;
            if (form.Bounds.Contains(pos))
                return FindControlAtPoint(form, form.PointToClient(Cursor.Position));
            return null;
        }

        // 지정된 좌표 위치의 컨트롤 찾기 
        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null) return c;
                    else return child;
                }
            }
            return null;
        }

        //--------------------------------------------------------------------------//
        // Win32 API 사용 - 
        //--------------------------------------------------------------------------//
        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hWndChild);

        [DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern void SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32")]
        public static extern int WaitForInputIdle(IntPtr hProcess, int dwMilliseconds);

        //[DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        //static extern bool FreeLibrary(int hModule);

        // 삐~! 소리나게 
        [DllImport("KERNEL32.DLL")]
        extern public static void Beep(int freq, int dur);

        // 폼 라운드 처리
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

    }

    // 화면 깜박임 속도 개선 - Dublebuffer -- TabPage
    public static class ExtensionMethods
    {
        // TabPage DubleBuffered 속성 지정 
        public static void DoubleBuffered(this TabPage tab, bool setting)
        {
            try
            {
                Type tabType = tab.GetType();
                PropertyInfo pi = tabType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tab, setting, null);
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
            }
        }
    }


}

