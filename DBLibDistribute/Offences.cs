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
    public class Offences
    {
        //===========================================================//
        // Insert Offences
        //===========================================================//
        public static int InsertOffences(SqlConnection Conn, String strUserId
                                        , ref String[] strArrRgltn_id
                                        , ref String[] strArrOffence_id
                                        , ref String[] strArrInspector_id
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

            for (int i = 0; i < strArrRgltn_id.Length; i++)
            {
                String SQLText = "";

                // INSERT
                if (strArrOffence_id[i] == "")
                {
                    SQLText = String.Format("INSERT INTO OFFENCES (	                    "

                                           + "					  rgltn_id				"   // 단속 정보                                       
                                           + "					, device_type			"
                                           + "					, device_mdl			"
                                           + "					, device_sn			    "

                                           + "					, branch				"
                                           + "					, officer				"
                                           + "					, court				    "
                                           + "					, street	   	        "
                                           + "					, location	 	        "
                                           + "					, direction	            "

                                           + "					, regulation_type		"
                                           + "					, regulation_lane		"
                                           + "					, regulation_distance 	"
                                           + "					, regulation_time		"
                                           + "					, regulation_spd_limit	"

                                           + "					, real_speed		    "
                                           + "					, over_speed		    "
                                           + "					, offence_code			"
                                           + "					, fine		            "

                                           + "					, file_directory		"
                                           + "					, file_original		    "
                                           + "					, file_no				"
                                           + "					, file_name				"
                                           + "					, file_plate			"

                                           + "					, upload_id				"
                                           + "					, upload_time			"

                                           + "					, decipher_plate		"   // 번호판 판독 정보                                  
                                           + "					, decipher_maker_cd		"
                                           + "					, decipher_maker		"
                                           + "					, decipher_color		"
                                           + "					, decipher_model		"
                                           + "					, decipher_type_cd	    "
                                           + "					, decipher_type	 	    "
                                           + "					, decipher_year		    "
                                           + "					, decipher_orntt	    "

                                           + "					, inspection_id			"       // 검증 Inspection 정보    
                                           + "                  , inspection_read_not_cd    "   // 검증 불가 코드
                                           + "                  , inspection_read_not_etc   "   // 검증 불가 기타 사유

                                           + "					, vehicle_plate			"   // 차량정보 정보(판독, 검증, 차적조회 로 수정 될 때 이 컬럼도 같이 써준다. - 최종 차량 정보)
                                           + "					, vehicle_maker_cd			"
                                           + "					, vehicle_maker			"
                                           + "					, vehicle_color			"
                                           + "					, vehicle_model			"
                                           + "					, vehicle_type_cd	    "
                                           + "					, vehicle_type		    "
                                           + "					, vehicle_year		    "

                                           + "					, create_dtm			"
                                           + "					, create_id		    	"
                                           + "					, chg_dtm				"
                                           + "					, chg_id			    "
                                           + "					)                       "
                                           + "		     SELECT   R.rgltn_id				                                    "   // 단속 정보                                       
                                           + "					, R.device_type			                                    "
                                           + "					, R.device_mdl			                                    "
                                           + "					, R.device_sn			                                        "

                                           + "					, R.branch				                                    "
                                           + "					, R.officer				                                    "
                                           + "					, R.court				                                        "
                                           + "					, R.street	   	                                            "
                                           + "					, R.location	 	                                            "
                                           + "					, R.direction	                                                "

                                           + "					, R.regulation_type		                                    "
                                           + "					, R.regulation_lane		                                    "
                                           + "					, R.regulation_distance 	                                    "
                                           + "					, R.regulation_time		                                    "
                                           + "					, R.regulation_spd_limit	                                    "

                                           + "					, R.real_speed		                                        "
                                           + "					, R.over_speed		                                        "
                                           // 2019.11.27 재매핑
                                           ////+ "					, R.offence_code			                                    "
                                           ////+ "					, R.fine		                                                "
                                           //+ "                    , CASE T.vhcl_spc_type     "
                                           //+ "                            WHEN 0          THEN F.offence_cd_0     "
                                           //+ "                            WHEN 1          THEN F.offence_cd_1     "
                                           //+ "                            WHEN 2          THEN F.offence_cd_2     "
                                           //+ "                            ELSE                 R.offence_code     "
                                           //+ "                      END offence_cd     "
                                           //+ "                    , CASE T.vhcl_spc_type     "
                                           //+ "                            WHEN 0            THEN F.fine_0     "
                                           //+ "                            WHEN 1            THEN F.fine_1     "
                                           //+ "                            WHEN 2            THEN F.fine_2     "
                                           //+ "                            ELSE                   R.fine       "
                                           //+ "                      END fine     "
                                           ////+ "                    , T.vhcl_spc_type     "
                                           //                                           + "                  , ISNULL(map_nm_2, ISNULL(map_nm_1, map_nm_0)) map_nm                 "
                                           + "                  , ISNULL(offence_cd_2, ISNULL(offence_cd_1, offence_cd_0)) offence_cd   "
                                           + "                  , ISNULL(CASE ISNULL(offence_cd_2, 'N')                                        "
                                           + "                                WHEN 'N' THEN CASE ISNULL(offence_cd_1, 'N')                     "
                                           + "                                                   WHEN 'N' THEN fine_0                          "
                                           + "                                                            ELSE fine_1                          "
                                           + "                                              END                                                "
                                           + "                                         ELSE fine_2                                                      "
                                           + "                           END, 0.00)  fine                                                             "
                                           + "					, R.file_directory		                                        "
                                           + "					, R.file_original		                                        "
                                           + "					, R.file_no				                                    "
                                           + "					, R.file_name				                                    "
                                           + "					, R.file_plate			                                    "

                                           + "					, R.upload_id				                                    "
                                           + "					, R.upload_time			                                    "

                                           + "					, R.decipher_plate		                                    "   // 번호판 판독 정보                                  
                                           + "					, M.vhcl_make_cd    decipher_maker_cd                       "
                                           + "					, R.decipher_maker		                                    "
                                           + "					, R.decipher_color		                                    "
                                           + "					, R.decipher_model		                                    "
                                           + "					, T.vhcl_type_cd    decipher_type_cd                        "
                                           + "					, R.decipher_type	 	                                        "
                                           + "					, R.decipher_year		                                        "
                                           + "					, R.decipher_orntt	                                        "

                                           + "					, '{0}'	 				                                    "   // 검증 Inspection 정보 // Inspector ID                             
                                           + "					, CASE R.decipher_plate	                                    "
                                           + "                         WHEN ''        THEN '111'                            "
                                           + "                                        ELSE '000'                            "
                                           + "                    END inspection_read_not_cd                                "  // 판독 불가 코드
                                           + "					, CASE R.decipher_plate	                                    "
                                           + "                         WHEN ''        THEN 'Unreadable'                     "
                                           + "                                        ELSE ''                               "
                                           + "                    END inspection_read_not_etc                               "  // 번호판 판독 불가 기타 사유

                                           + "					, R.decipher_plate		                                    "   // 번호판 판독 정보                                  
                                           + "					, M.vhcl_make_cd    decipher_maker_cd                       "
                                           + "					, R.decipher_maker		                                    "
                                           + "					, R.decipher_color		                                    "
                                           + "					, R.decipher_model	                                        "
                                           + "                  , T.vhcl_type_cd    decipher_type_cd                        "
                                           + "					, R.decipher_type		                                        "
                                           + "					, R.decipher_year	                                            "

                                           + "					, GetDate()			                                        "
                                           + "					, '{1}'			    	                                    "
                                           + "					, GetDate()				                                    "
                                           + "					, '{2}'			    	                                    "

                                           + "		         FROM REGULATIONS   R                                           "
                                           + "                    LEFT JOIN                                                 "
                                           + "                    VEHICLE_MAKE  M   ON M.vhcl_make_nm = R.decipher_maker    "
                                           + "                    LEFT JOIN                                                 "
                                           + "                    VEHICLE_TYPE  T   ON T.vhcl_type_nm = R.decipher_type     "
                                           // MAPPING SUB QUERY ----------------
                                           + "                    LEFT JOIN     "
                                           + "                    (     "
                                           + "                      SELECT court     "
                                           + "                           , location_cd     "
                                           + "                           , speed_limit     "
                                           + "                           , reg_min     "
                                           + "                           , reg_max     "
                                           + "                           , speed_min     "
                                           + "                           , speed_max     "
                                           ////  0 : RRS, RUA, RFW    
                                           //                                           + "                           , MAX(CASE map_position WHEN 0 THEN offence_cd ELSE null END)  offence_cd_0     "
                                           //                                           + "                           , MAX(CASE map_position WHEN 0 THEN map_nm     ELSE null END)  map_nm_0     "
                                           //                                           + "                           , MAX(CASE map_position WHEN 0 THEN fine       ELSE 0    END)  fine_0     "
                                           + "                           , MAX(offence_cd)  offence_cd_0     "
                                           + "                           , MAX(map_nm    )  map_nm_0     "
                                           + "                           , MAX(fine      )  fine_0     "
                                           // 1 : W9T, VTB     
                                           + "                           , MAX(CASE map_position WHEN 1 THEN offence_cd ELSE null END)  offence_cd_1     "
                                           + "                           , MAX(CASE map_position WHEN 1 THEN map_nm     ELSE null END)  map_nm_1     "
                                           + "                           , MAX(CASE map_position WHEN 1 THEN fine       ELSE 0    END)  fine_1     "
                                           // 2 : VTM     
                                           + "                           , MAX(CASE map_position WHEN 2 THEN offence_cd ELSE null END)  offence_cd_2     "
                                           + "                           , MAX(CASE map_position WHEN 2 THEN map_nm     ELSE null END)  map_nm_2     "
                                           + "                           , MAX(CASE map_position WHEN 2 THEN fine       ELSE 0    END)  fine_2     "

                                           + "                        FROM (     "
                                           + "                              SELECT M.court                  court     "
                                           + "                                   , M.location_cd            location_cd     "
                                           //                                           + "                      --, M.direction                direction     "
                                           //                                           + "                      --, M.map_seq              map_seq     "
                                           + "                                   , M.reference              reference     "
                                           + "                                   , M.relevant_legislation   relevant_legislation     "
                                           + "                                   , M.category               category     "
                                           //                                           + "                      --, M.map_desc             map_desc     "
                                           //                                           + "                      --, M.offence_list         offence_list     "
                                           //                                           + "                      --, R.cd_desc                reg_road_type     "
                                           + "                                   , L.cd_desc                speed_limit     "
                                           + "                                   , L.cd_v_ext1              reg_min     "
                                           + "                                   , L.cd_v_ext2              reg_max     "
                                           //                                           + "                      --, L.use_yn                 legislation_use_yn     "
                                           + "                                   , O.speed_min              speed_min     "
                                           + "                                   , O.speed_max              speed_max     "
                                           + "                                   , O.fine                   fine     "
                                           + "                                   , O.offence_cd             offence_cd     "
                                           //                                           + "                      --, O.use_yn                 offence_use_yn     "
                                           + "                                   , T.cd                     map_cd     "
                                           + "                                   , T.cd_nm                  map_nm     "
                                           + "                                   , T.cd_v_ext1              map_position     "

                                           + "                                FROM LOCATION_MAP             M     "

                                           + "                                     LEFT JOIN     "

                                           + "                                     CODE_MASTER              R   ON R.cd_grp = 'OFFENCE_RF'     "

                                           + "                                                                    AND R.cd = M.reference     "

                                           + "                                     LEFT JOIN     "

                                           + "                                     CODE_MASTER              L   ON L.cd_grp = 'OFFENCE_RL'     "

                                           + "                                                                    AND L.cd = M.relevant_legislation     "

                                           + "                                     LEFT JOIN     "

                                           + "                                     OFFENCE_CODE             O   ON M.reference = O.reference     "

                                           + "                                                                    AND M.relevant_legislation = O.relevant_legislation     "

                                           + "                                                                    AND M.category = O.category     "

                                           + "                                                                    AND M.offence_list           LIKE '%' + O.offence_cd + '%'     "

                                           + "                                     LEFT JOIN     "

                                           + "                                     CODE_MASTER              T   ON T.cd_grp = 'ROAD_TYPE'     "

                                           + "                                                                 AND M.relevant_legislation   LIKE '%' + T.cd_nm     "
                                           + "                              )       FF     "

                                           + "                       GROUP BY court     "
                                           + "                           , location_cd     "
                                           + "                           , speed_limit     "
                                           + "                           , reg_min     "
                                           + "                           , reg_max     "
                                           + "                           , speed_min     "
                                           + "                           , speed_max     "
                                           + "                    )             F   ON F.court = R.court     "
                                           + "                                     AND F.location_cd = R.location     "
                                           + "                                     AND F.speed_limit = R.regulation_spd_limit     "
                                           + "                                     AND F.speed_min <= R.real_speed     "
                                           + "                                     AND ISNULL(F.speed_max, 999) >= R.real_speed     "
                                           // MAPPING SUB QUERY ----------------

                                           + "		        WHERE rgltn_id = {3}                                            "
                                           , strArrInspector_id[i]
                                           , strUserId
                                           , strUserId
                                           , strArrRgltn_id[i]
                                           );
                }
                // Update
                else
                {
                    SQLText = String.Format("UPDATE OFFENCES			        "
                                           + "   SET inspection_id  = '{0}'	    "
                                           + "     , chg_id         = '{1}'		"
                                           + "     , chg_dtm        = GetDate()	"
                                           //+ " WHERE cntrvt_id      = {2}		"
                                           + " WHERE offence_id      = {2}		"
                                           , strArrInspector_id[i]
                                           , strUserId
                                           , strArrOffence_id[i]
                                           );

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

    }
}
