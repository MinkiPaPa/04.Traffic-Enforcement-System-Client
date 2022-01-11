using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;

//// ListViewItem 사용
//using System.Windows.Forms;

// Path


namespace DBLibInspection
{
    public class Offences
    {
        //===========================================================//
        // Update Offences - Inspector
        //===========================================================//
        public static int UpdateInspector(SqlConnection Conn, String strUserId
                                         , bool bIsSupervisor

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
            int rv = 0;

            if (Conn == null)
            {
                return -1;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return -1;

            int iSCnt = 0;
            int iFCnt = 0;

            for (int i = 0; i < strArrOffence_id.Length; i++)
            {
                String SQLText = "";

                // UPDATE
                if (strArrOffence_id[i] != "")
                {
                    String sv_id = "";
                    String sv_cd = "0";
                    String sv_rj = "";

                    if (bIsSupervisor)
                    {
                        sv_id = strUserId;
                        sv_cd = strArrSupervisorPermit_cd[i];
                        sv_rj = strArrSupervisorRejectDesc[i];
                    }
                    else
                    {
                        //
                    }

                    SQLText = String.Format("UPDATE OFFENCES 		                    "
                                           + "   SET court                  = '{0}'  	"
                                           + "     , inspection_time        = GetDate()	"
                                           + "     , inspection_plate       = '{1}'     "
                                           + "     , inspection_maker_cd    = '{2}'     "
                                           + "     , inspection_maker       = '{3}'     "
                                           + "     , inspection_color       = '{4}'     "
                                           //+ "     , inspection_model       = ''        "
                                           + "     , inspection_type_cd     = '{5}'     "
                                           + "     , inspection_type        = '{6}'     "
                                           //+ "     , inspection_year        = ''        "
                                           + "     , inspection_read_not_cd = '{7}'     "
                                           + "     , inspection_read_not_etc= '{8}'     "
                                           + "     , inspection_end_yn      = {9}       "

                                           + "     , supervisor_id          = '{10}'     "
                                           + "     , supervisor_time        = GetDate() "
                                           + "     , supervisor_permit_cd   = {11}       "
                                           + "     , supervisor_reject_desc = '{12}'     "

                                           + "     , vehicle_plate          = '{1}'     "
                                           + "     , vehicle_maker_cd       = '{2}'     "
                                           + "     , vehicle_maker          = '{3}'     "
                                           + "     , vehicle_color          = '{4}'     "
                                           //+ "     , vehicle_model          = ''        "
                                           + "     , vehicle_type_cd        = '{5}'     "
                                           + "     , vehicle_type           = '{6}'     "
                                           //+ "     , vehicle_year           = ''        "

                                           + "     , fine                   = {13}          "

                                           + "     , chg_id                 = '{14}'		"
                                           + "     , chg_dtm                = GetDate()	"
                                           + " WHERE offence_id             = {15}		"
                                           , strArrCourt[i]
                                           , strArrInspection_plate[i]

                                           , strArrInspection_make_cd[i]
                                           , strArrInspection_make[i]
                                           , strArrInspection_color[i]
                                           , strArrInspection_type_cd[i]
                                           , strArrInspection_type[i]

                                           , strArrInspection_rn_cd[i]
                                           , strArrInspection_rn_etc[i]
                                           , strArrInspection_end_yn[i]
                                           , sv_id
                                           , sv_cd
                                           , sv_rj
                                           , strFine[i]
                                           , strUserId
                                           , strArrOffence_id[i]
                                           );

                }
                else
                {
                    //

                }

                // 실행
                SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
                try
                {
                    rv = sqlComm.ExecuteNonQuery();

                    if (rv == 1)
                    {
                        // 성공 횟수 카운트
                        iSCnt++;
                        strArrResult[i] = "OK";

                        // test ... Contraventions 로 넘어 가지 않도록 임시로 막음
                        //// Supervisor가 검증을 마치면 Contraventions 테이블에 넣어준다.
                        if (bIsSupervisor && strArrSupervisorPermit_cd[i] == "1")
                        {

                            String strOffence_id = strArrOffence_id[i];
                            if (Contraventions.InsertContraventions(Conn, strUserId
                                                                   , bIsSupervisor
                                                                   , strOffence_id
                                                                   )) continue;
                            else
                            {
                                strArrResult[i] = "FAIL";
                                iSCnt--;
                                iFCnt++;
                            }
                            continue;
                        }
                    }

                }
                catch (Exception e)
                {
                    // Build 시 메시지 나오지 말라고 ...
                    String tmp = e.Message;

                    // 실패 횟수 카운트
                    iFCnt++;
                    strArrResult[i] = "FAIL";
                }
                finally
                {
                    //

                }

            } // END for 

            return iSCnt;
        }

        //===========================================================//
        // Offences 조회 
        //===========================================================//
        public static int GFn_SelectInspection(SqlConnection Conn
                                              , String strCheckedYN_SV
                                              , String strCheckedYN
                                              , String strDateYN
                                              , String strStart
                                              , String strEnd
                                              , String strUseOfficerID
                                              , String strOfficerID
                                              , bool bIsSupervisor
                                              , String strUseInspectorID
                                              , String strInspectorID
                                              , String strUserId
                                              , ref DataSet ds
                                              )
        {
            try
            {

                // 초기화 
                int rv = 0;
                ds.Clear();

                if (Conn == null || Conn.State == ConnectionState.Closed)
                {
                    return -1;
                }
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return -1;

                // 특정 Inspector 조회 조건 
                String Inspector = "";
                if (bIsSupervisor)
                {
                    if (strUseInspectorID == "Y")
                        Inspector = strInspectorID;

                }
                else
                {
                    Inspector = strUserId;
                }

                String SQLText = String.Format("SELECT C.offence_id                offence_id                  "           // 위반 테이블 PK
                                                                                                                           // 단속 정보                                                                                                                                                                                                                                                                                                                            
                                              + "     , C.rgltn_id                  rgltn_id                    "           // 단속 테이블 Key

                                              + "     , C.device_type               device_type                 "           // 사용기기 종류: 이동형, 고정형, 신호위반, 통계용... (INTERFACE)
                                              + "     , DT.cd_nm                    device_nm                   "
                                              + "     , C.device_mdl                device_mdl                  "           // 사용기기 모델: Maker or Model No or Serial No...
                                              + "     , DM.cd_nm                    device_mdl_nm               "
                                              + "     , C.device_sn                 device_sn                   "           // 사용기기 No: 관리 번호 또는 시리얼 No

                                              + "     , C.branch                    branch                      "           // 소속: JMPD, CAPE, DBAN...
                                              + "     , C.officer                   officer                     "           // 단속경관 ID
                                              + "     , PO.name                     officer_nm                  "
                                              + "     , C.court                     court                       "           // 관할법원(Area)
                                              + "     , C.street                    street                      "           // Hightway Name: M1, N1, N3...
                                              + "     , C.location                  location                    "           // 지역코드
                                              + "     , C.direction                 direction                   "           // 방향코드 : N, S, E, W, ND...
                                              + "     , C.manual_yn                 manual_yn                   "           // 수기입력 여부 

                                              + "     , C.regulation_type           regulation_type             "           // 단속 - 종류 : 속도위반 단속, 신호위반 단속, 법규위반단속...
                                              + "     , RT.cd_nm                    regulation_nm               "
                                              + "     , C.regulation_lane           regulation_lane             "           // 단속 - 차선
                                              + "     , C.regulation_distance       regulation_distance         "           // 단속 - 촬영 거리
                                              + "     , C.regulation_time           regulation_time             "           // 단속 - 시각
                                              + "     , C.regulation_spd_limit      regulation_spd_limit        "           // 단속 - 제한 속도

                                              + "     , C.real_speed                real_speed                  "           // 주행 속도
                                              + "     , C.over_speed                over_speed                  "           // 위반 속도
                                              + "     , C.offence_code              offence_code                "           // 위반코드
                                              + "     , VT.cd_nm                    offence_nm                  "           // 위반 코드명
                                              + "     , CAST(C.fine AS INT)         fine                        "           // 범칙금

                                              + "     , C.file_directory            file_directory              "           // 파일 경 PATH(파일 서버의 절대경로)로
                                              + "     , C.file_original             file_original               "           // 원본 파일 -동영상 원본(*.avi, *.jmx, ...)
                                              + "     , C.file_no                   file_no                     "           // 추출 이미지 순번(0 ~ )
                                              + "     , C.file_name                 file_name                   "           // 판독 이미지 파일명
                                              + "     , C.file_plate                file_plate                  "           // 번호판 추출 이미지 파일 -번호판 이미지

                                              + "     , C.upload_id                 upload_id                   "           // upload 담당자 ID
                                              + "     , PU.name                     upload_nm                   "
                                              + "     , C.upload_time               upload_time                 "           // upload 시각

                                              //번호판 판독 정보
                                              + "     , C.decipher_plate            decipher_plate              "           // 판독 - 차량 번호
                                              + "     , C.decipher_maker_cd         decipher_maker_cd           "           // 판독 - 제조사
                                              + "     , C.decipher_maker            decipher_maker              "           // 판독 - 제조사
                                              + "     , C.decipher_type_cd          decipher_type_cd            "           // 판독 - 바디 타입? 종류
                                              + "     , C.decipher_type             decipher_type               "           // 판독 - 바디 타입? 종류
                                              + "     , C.decipher_color            decipher_color              "           // 판독 - 차량 색상
                                              + "     , C.decipher_model            decipher_model              "           // 판독 - 차량 모델
                                              + "     , C.decipher_year             decipher_year               "           // 판독 - 생산년도
                                              + "     , C.decipher_orntt            decipher_orntt              "           // 판독 - Orientation

                                              // 검증 Inspection 정보
                                              + "	  , C.inspection_id             inspection_id               "           // 검증 담당자
                                              + "     , PI.name                     inspection_nm               "
                                              + "     , C.inspection_time           inspection_time             "           // 검증 시각
                                              + "     , C.inspection_plate          inspection_plate            "           // 검증 - 차량 번호
                                              + "     , C.inspection_maker_cd       inspection_maker_cd         "           // 검증 - 제조사
                                              + "     , C.inspection_maker          inspection_maker            "           // 검증 - 제조사
                                              + "     , C.inspection_color          inspection_color            "           // 검증 - 차량 색상
                                              + "     , C.inspection_model          inspection_model            "           // 검증 - 차량 모델
                                              + "     , C.inspection_type_cd        inspection_type_cd          "           // 검증 - 바디 타입? 종류
                                              + "     , C.inspection_type           inspection_type             "           // 검증 - 바디 타입? 종류
                                              + "     , C.inspection_year           inspection_year             "           // 검증 - 생산년도
                                              + "     , C.inspection_read_not_cd    inspection_read_not_cd      "           // 검증 - 검증 불능 코드
                                              + "     , RN.cd_nm                    inspection_read_not_nm      "           // 검증 - 검증 불능 설명
                                              + "     , C.inspection_read_not_etc   inspection_read_not_etc     "           // 검증 - 검증 불능 기타 사유
                                              + "     , C.inspection_end_yn         inspection_end_yn           "           // 검증 - 완료여부

                                              + "     , C.supervisor_id             supervisor_id               "           // 관리자 ID
                                              + "     , PS.name                     supervisor_nm               "           // 관리자 성명
                                              + "     , C.supervisor_time           supervisor_time             "           // 관리자 처리시각
                                              + "     , C.supervisor_permit_cd      supervisor_permit_cd        "           // 관리자 허가 여부 - 0:미처리, 1:허가, 2:반려
                                              + "     , C.supervisor_reject_desc    supervisor_reject_desc      "           // 관리자 반려 사유

                                              // 최종 차량 정보
                                              + "     , C.vehicle_plate             vehicle_plate               "           // 최종 - 차량 번호
                                              + "     , C.vehicle_maker_cd          vehicle_maker_cd            "           // 최종 - 제조사
                                              + "     , C.vehicle_maker             vehicle_maker               "           // 최종 - 제조사
                                              + "     , C.vehicle_type_cd           vehicle_type_cd             "           // 최종 - 바디 타입? 종류
                                              + "     , C.vehicle_type              vehicle_type                "           // 최종 - 바디 타입? 종류
                                              + "     , C.vehicle_color             vehicle_color               "           // 최종 - 차량 색상
                                              + "     , C.vehicle_model             vehicle_model               "           // 최종 - 차량 모델
                                              + "     , C.vehicle_year              vehicle_year                "           // 최종 - 생산년도

                                              + "     , C.status_cd                 status                      "           // 진행 상태 코드
                                              + "     , SS.cd_nm                    status_nm                   "           // 진행 상태 설명

                                              + "     , C.create_dtm                create_dtm                  "           // 등록일시
                                              + "     , C.create_id                 create_id                   "           // 등록자 ID
                                              + "     , PW.name                     create_nm                   "
                                              + "     , C.chg_dtm                   chg_dtm                     "           // 최종 수정일시
                                              + "     , C.chg_id                    chg_id                      "           // 최종 작업자
                                              + "     , PC.name                     chg_nm                      "

                                              // 수정 여부 판단용
                                              ////+ "     , C.decipher_plate            decipher_plate_org      "           // 판독 - 차량 번호 
                                              ////+ "     , C.inspection_plate          inspection_plate_org    "           // 검증 - 차량 번호
                                              ////+ "     , C.inspection_end_yn         inspection_end_yn_org   "           // 검증 - 완료여부
                                              //+ "     , C.inspection_plate          inspection_plate_org    "           // 검증 - 차량 번호
                                              //+ "     , C.inspection_maker          inspection_maker_org    "           // 검증 - 제조사
                                              //+ "     , C.inspection_color          inspection_color_org    "           // 검증 - 차량 색상
                                              //+ "     , C.inspection_model          inspection_model_org    "           // 검증 - 차량 모델
                                              //+ "     , C.inspection_type           inspection_type_org     "           // 검증 - 바디 타입? 종류
                                              //+ "     , C.inspection_year           inspection_year_org     "           // 검증 - 생산년도
                                              + "     , C.inspection_end_yn         inspection_end_yn_org       "           // 검증 - 완료여부

                                              + "     , C.vehicle_plate             vehicle_plate_org           "           // 최종 - 차량 번호
                                              + "     , C.vehicle_maker_cd          vehicle_maker_cd_org        "           // 최종 - 제조사
                                              + "     , C.vehicle_maker             vehicle_maker_org           "           // 최종 - 제조사
                                              + "     , C.vehicle_type_cd           vehicle_type_cd_org         "           // 최종 - 바디 타입? 종류
                                              + "     , C.vehicle_type              vehicle_type_org            "           // 최종 - 바디 타입? 종류
                                              + "     , C.vehicle_color             vehicle_color_org           "           // 최종 - 차량 색상
                                              + "     , C.vehicle_model             vehicle_model_org           "           // 최종 - 차량 모델
                                              + "     , C.vehicle_year              vehicle_year_org            "           // 최종 - 생산년도
                                              + "     , C.inspection_read_not_cd    inspection_read_not_cd_org  "           // 검증 - 검증 불능 코드
                                              + "     , C.inspection_read_not_etc   inspection_read_not_etc_org "           // 검증 - 검증 불능 기타 사유
                                              + "     , C.supervisor_permit_cd      supervisor_permit_cd_org    "           // 관리자 허가 여부 - 0:미처리, 1:허가, 2:반려
                                              + "     , C.supervisor_reject_desc    supervisor_reject_desc_org  "           // 관리자 반려 사유
                                              + "     , C.court                     court_org                   "           // 관할법원(Area)
                                              + "     , CAST(C.fine AS INT)         fine_org                    "           // 범칙금


                                              + "  FROM OFFENCES                 C                                      "
                                              // Device Type
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     DT      ON 'DVC_TP'             = DT.cd_grp     "
                                              + "                              AND C.device_type        = DT.cd         "
                                              // Device Model
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     DM      ON 'DVC_MDL'            = DM.cd_grp     "
                                              + "                              AND C.device_mdl         = DM.cd         "
                                              // Regulation Type
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     RT      ON 'RGLT_TP'            = RT.cd_grp     "
                                              + "                              AND C.regulation_type    = RT.cd         "
                                              // Offence Code
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     VT      ON 'VIOLT_TP'           = VT.cd_grp     "
                                              + "                              AND C.offence_code       = VT.cd         "
                                              // Officer                                                            
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           UO      ON C.officer            = UO.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PO      ON UO.prsn_id           = PO.prsn_id    "
                                              // Upload                                                                 
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           UU      ON C.upload_id          = UU.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PU      ON UU.prsn_id           = PU.prsn_id    "
                                              // Inspector                                                              
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           UI      ON C.inspection_id      = UI.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PI      ON UI.prsn_id           = PI.prsn_id    "
                                              // Read Not
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     RN      ON 'READ_NOT'               = RN.cd_grp "
                                              + "                              AND C.inspection_read_not_cd = RN.cd     "
                                              // Supervisor
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           US      ON C.supervisor_id      = US.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PS      ON US.prsn_id           = PS.prsn_id    "
                                              // Status
                                              + "       LEFT JOIN                                                       "
                                              + "       CODE_MASTER     SS      ON 'STATUS'             = SS.cd_grp     "
                                              + "                              AND C.status_cd          = SS.cd         "
                                              // Creater                                                                
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           UW      ON C.create_id          = UW.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PW      ON UW.prsn_id           = PW.prsn_id    "
                                              // Changer                                                                
                                              + "       LEFT JOIN                                                       "
                                              + "       USERS           UC      ON C.chg_id             = UC.user_id    "
                                              + "       LEFT JOIN                                                       "
                                              + "       PERSONS         PC      ON UC.prsn_id           = PC.prsn_id    "
                                              + " WHERE 1 = 1                                                               "
                                              // 관리자가 승인 안한 것 
                                              + "   AND C.supervisor_permit_cd <> 1                                         "
                                              // 로그인한 사람이 담당자 것만 조회
                                              + "   AND C.inspection_id         LIKE '{0}%'                                 "
                                              //-- 확인, 미확인, ALL - Supervisor
                                              + "   AND CASE supervisor_permit_cd                                           "
                                              + "            WHEN 1 THEN 'Y'                                                "   // permission
                                                                                                                                //+ "            WHEN 2 THEN 'X'                                                "   // reject
                                              + "            ELSE        'N'                                                "
                                              + "       END                     IN ({1})                                    "
                                              //-- 확인, 미확인, ALL
                                              + "   AND CASE SIGN(C.inspection_end_yn)                                      "
                                              + "            WHEN 1 THEN 'Y'                                                "
                                              + "            ELSE        'N'                                                "
                                              + "       END                     IN ({2})                                    "
                                              //-- 추가 - 기간 지정 조회
                                              + "   AND ( 'N' = '{3}' OR                                                    "
                                              + "         CONVERT(VARCHAR, C.regulation_time, 112)                          "
                                              + "         BETWEEN CONVERT(VARCHAR, CONVERT(DATE, '{4}'), 112)               "
                                              + "             AND CONVERT(VARCHAR, CONVERT(DATE, '{5}'), 112)               "
                                              + "       )                                                                   "
                                              //-- 추가 - 단속 경찰 지정 조회
                                              + "   AND (                                                                   "
                                              + "         ( 'Y' = '{6}' AND C.officer = '{7}') OR                           "
                                              + "         ( 'N' = '{6}')                                                    "
                                              + "       )                                                                   "
                                              + " ORDER BY rgltn_id                                                         "
                                              //, strUserId
                                              , Inspector
                                              , strCheckedYN_SV
                                              , strCheckedYN
                                              , strDateYN
                                              , strStart
                                              , strEnd
                                              , strUseOfficerID
                                              , strOfficerID
                                              );

                // 실행
                SqlDataAdapter sda = new SqlDataAdapter();

                sda.SelectCommand = new SqlCommand(SQLText, Conn);
                rv = sda.Fill(ds);

                return rv;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;
                return -1;
            }
            finally
            {
                //
            }
        }

    }
}
