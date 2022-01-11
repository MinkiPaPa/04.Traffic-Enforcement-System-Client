using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;

//// ListViewItem 사용
//using System.Windows.Forms;

// Path

namespace DBLibDistribute
{
    public class Regulations
    {
        //===========================================================//
        // Regulations 조회 
        //===========================================================//
        public static int GFn_SelectRegulations(SqlConnection Conn
                                               , String strDistributeYN
                                               , String strDateYN
                                               , String strStart
                                               , String strEnd
                                               , String strUseUserID
                                               , String strUserID
                                               , ref DataSet ds
                                               )
        {
            try
            {
                // 초기화 
                int rv = 0;
                ds.Clear();

                if (Conn == null)
                {
                    return -1;
                }
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return -1;

                String SQLText = String.Format("SELECT CAST(0 AS BIT)              sel_yn                                  "   // 선택 여부
                                              + "     , R.rgltn_id					rgltn_id	  							"   // 단속 고유번호 ID

                                              + "     , R.device_type				device_type								"   // 사용기기 종류 : 이동형, 고정형, 신호위반, 통계용 ... (INTERFACE)
                                              + "	  , R.device_mdl				device_mdl								"   // 사용기기 모델 : Maker or Model No or Serial No ...
                                              + "     , R.device_sn			    	device_sn	 							"   // 사용기기 No : 관리 번호 또는 시리얼 No

                                              + "	  , R.branch				    branch		   							"   // Branch
                                              + "     , BR.cd_nm         			branch_nm							    "   // Branch Name 2019.12.05


                                              + "	  , R.officer				    officer	   								"   // 단속경관 ID
                                              + "     , P.name           			officer_nm  							"   // 단속경관 이름
                                              + "	  , R.court				    	court									"   // 관할법원 (Area)
                                              + "	  , R.street	   		        street						            "   // Hightway Name : M1, N1, N3 ...
                                              + "	  , R.location	 	            location						        "   // 지역코드
                                              + "	  , R.direction		            direction						        "   // 방향코드 : N, S, E, W, ND ...

                                              + "	  , R.regulation_type			regulation_type							"   // 단속 - 종류 : 속도위반 단속, 신호위반 단속, 법규위반단속 ...
                                              + "     , CR.cd_nm         			regulation_nm							"   // 단속 - 종류명칭			
                                              + "	  , R.regulation_lane		   	regulation_lane	   						"   // 단속 - 차선
                                              + "	  , R.regulation_distance 		regulation_distance						"   // 단속 - 촬영 거리 
                                              + "	  , R.regulation_time		    regulation_time	   						"   // 단속 - 시각
                                              + "	  , R.regulation_spd_limit	   	regulation_spd_limit					"   // 단속 - 제한 속도
                                                                                                                                // 
                                              + "	  , R.real_speed		   	    real_speed	   						    "   // 위반 - 주행 속도
                                              + "	  , R.over_speed		   	    over_speed	   						    "   // 위반 - 위반 속도
                                              + "	  , R.offence_code  		    offence_code							"   // 위반 코드
                                              + "     , CV.cd_nm         			offence_nm							    "   // 위반 코드 명
                                              + "	  , R.fine      			    fine        							"   // 범칙금
                                                                                                                                // 
                                              + "	  , R.file_directory			file_directory							"   // 파일 PATH( 파일 서버의 절대경로 )로
                                              + "	  , R.file_original		    	file_original							"   // 원본 파일 - 동영상 원본(*.avi, *.jmx, ...)
                                              + "	  , R.file_no					file_no			    					"   // 추출 이미지 순번(0 ~ )
                                              + "	  , R.file_name					file_name								"   // 판독 이미지 파일명
                                              + "	  , R.file_plate			    file_plate								"   // 번호판 추출 이미지 파일 - 번호판 이미지

                                              // Uploder 
                                              + "	  , R.upload_id					upload_id								"   // upload 담당자 ID
                                              + "     , PU.name          			upload_nm   							"   // upload 담당자 이름
                                              + "	  , R.upload_time				upload_time								"   // upload 시각
                                              /* 2019.06.18 판독 정보 추가 */
                                              + "     , R.decipher_plate            decipher_plate                          "
                                              + "     , R.decipher_maker            decipher_maker                          "
                                              + "     , VM.vhcl_make_nm             decipher_maker_nm                       "
                                              + "     , R.decipher_type             decipher_type                           "
                                              + "     , VT.vhcl_type_nm             decipher_type_nm                        "
                                              + "     , R.decipher_color            decipher_color                          "
                                              + "     , R.decipher_model            decipher_model                          "
                                              + "     , R.decipher_year             decipher_year                           "
                                              + "     , R.decipher_orntt            decipher_orntt                          "

                                              + "     , CAST(CASE SIGN(C.offence_id)                                        "
                                              + "                 WHEN 1     then 1                                         "
                                              + "                 ELSE                                         0            "
                                              + "            END AS BIT)           contravation_yn                          "   // 위반 테이블 반영여부

                                              + "      , C.offence_id               cntrvt_id                               "   // 위반 테이블 ID

                                              // 추가 inspection 담당자 
                                              + "     , C.inspection_id             inspector_id                            "   // 검수 담당자 user_id
                                              + "     , PI.name                     inspector_nm                            "   // 검수 담당자 person_name

                                              + "	  , R.create_dtm			    create_dtm								"   // 등록일시
                                              + "	  , R.create_id		    		create_id	 							"   // 등록자 ID
                                              + "	  , R.chg_dtm				    chg_dtm	   								"   // 최종 수정일시
                                              + "	  , R.chg_id			    	chg_id		   							"   // 최종 작업자
                                                                                                                                // Data 변경 확인용
                                              + "     , C.inspection_id             org_inspector_id                        "   // ORG 검수 담당자 user_id
                                              + "  FROM REGULATIONS			    R                                           "
                                              + "       LEFT JOIN                                                           "
                                              + "       OFFENCES    		    C	ON R.rgltn_id	        = C.rgltn_id    "

                                              + "       LEFT JOIN                                                           "   // Branch 사용 2019.12.05
                                              + "       CODE_MASTER             BR  ON 'BRNCH'              = BR.cd_grp     "
                                              + "                                  AND R.branch             = BR.cd         "

                                              + "       LEFT JOIN                                                           "
                                              + "       PERSONS                 P   ON R.officer            = P.prsn_id     "
                                              + "       LEFT JOIN                                                           "
                                              + "       USERS                   U   ON R.upload_id          = U.user_id     "
                                              + "       LEFT JOIN                                                           "
                                              + "       PERSONS                 PU  ON U.prsn_id            = PU.prsn_id    "
                                              + "       LEFT JOIN                                                           "
                                              + "       CODE_MASTER             CR  ON 'RGLT_TP'            = CR.cd_grp     "
                                              + "                                  AND R.regulation_type    = CR.cd         "
                                              + "       LEFT JOIN                                                           "
                                              + "       CODE_MASTER             CV  ON 'OFFENCE_TP'         = CV.cd_grp     "
                                              + "                                  AND R.offence_code       = CV.cd         "
                                              // VEHICLE Maker
                                              + "       LEFT JOIN                                                           "
                                              + "       VEHICLE_MAKE            VM  ON R.decipher_maker     = VM.vhcl_make_cd   "
                                              // VEHICLE Type
                                              + "       LEFT JOIN                                                           "
                                              + "       VEHICLE_TYPE            VT  ON R.decipher_type      = VT.vhcl_type_cd   "
                                              // Inspection 담당자
                                              + "       LEFT JOIN                                                           "
                                              + "       USERS                   UI  ON C.inspection_id = UI.user_id         "
                                              + "       LEFT JOIN                                                           "
                                              + "       PERSONS                 PI  ON UI.prsn_id = PI.prsn_id              "

                                              + " WHERE 1 = 1                                                               "
                                              //-- Superviser 확인 끝난거 제외
                                              + "   AND (C.supervisor_permit_cd is null or SIGN(C.supervisor_permit_cd) <> 1) "
                                              //-- 검수 완료되지 않은 것
                                              + "   AND CASE SIGN(C.inspection_end_yn)                                      "
                                              + "            WHEN 1 THEN 'Y'                                                "
                                              + "            ELSE        'N'                                                "
                                              + "       END                     = 'N'                                       "
                                              //-- 분배, 미분배, ALL
                                              //+ "   AND CASE SIGN(C.rgltn_id)                                               "
                                              //+ "            WHEN 1 THEN 'Y'                                                "
                                              //+ "            ELSE        'N'                                                "
                                              //+ "       END                     IN ({0})                                    "
                                              + "   AND CASE SIGN(C.offence_id) "
                                              + "            WHEN 1         THEN CASE SIGN(LEN(TRIM(C.inspection_id)))      "
                                              + "                                     WHEN 0          THEN         'N'      "
                                              + "                                     ELSE                         'Y'      "
                                              + "                                END                                        "
                                              + "            ELSE                                                  'N'      "
                                              + "       END                 IN ({0})                                        " // 분배여부

                                              //-- 추가 - 기간 지정 조회
                                              + "   AND ( 'N' = '{1}' OR                                                    "
                                              + "         CONVERT(VARCHAR, R.regulation_time, 112)                          "
                                              + "         BETWEEN CONVERT(VARCHAR, CONVERT(DATE, '{2}'), 112)               "
                                              + "             AND CONVERT(VARCHAR, CONVERT(DATE, '{3}'), 112)               "
                                              + "       )                                                                   "
                                              //-- 추가 - 담당자 지정 조회
                                              //                                          + "   AND C.inspection_id LIKE '{4}' + '%'                                   "
                                              + "   AND (                                                                   "
                                              + "         ( 'Y' = '{4}' AND C.inspection_id = '{5}') OR                     "
                                              + "         ( 'N' = '{4}')                                                     "
                                              + "       )                                                                   "
                                              + " ORDER BY rgltn_id                                                         "
                                              , strDistributeYN
                                              , strDateYN
                                              , strStart
                                              , strEnd
                                              , strUseUserID
                                              , strUserID
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
