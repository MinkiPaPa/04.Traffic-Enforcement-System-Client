using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// ListViewItem, MessageBox 사용
using System.Windows.Forms;

// Path

namespace DBLibMngOffenceCd
{
    public class Offence_code
    {


        // Select Location Code, return dataset
        public static int GFn_GetOffenceCodeDS(SqlConnection Conn, ref DataSet ds, bool bOnlyUseY = true)
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;

                String strUseYN = bOnlyUseY ? "1" : "";
                String SQLText = String.Format("SELECT CAST(0 AS BIT)               sel_yn                                              "
                                             + "     , MM.offence_cd                offence_cd                                          "

                                             + "     , MM.reference                 reference_cd                                        "
                                             + "     , CF.cd_nm                     reference_desc                                      "

                                             + "     , CF.cd_desc                   legislation_group                                   "

                                             + "     , MM.relevant_legislation      relevant_legislation_cd                             "
                                             + "     , CL.cd_nm                     relevant_legislation_desc                           "

                                             + "     , MM.category                  category                                            "
                                             + "     , MM.use_yn                    use_yn                                              "
                                             + "     , CAST(CL.cd_desc as INT)      speed_limit                                         "
                                             + "     , CAST(CL.cd_v_ext1 AS INT)    penalty_min_speed                                   "
                                             + "     , CAST(CL.cd_v_ext2 AS INT)    penalty_max_speed                                   "
                                             + "     , MM.speed_min                 speed_from                                          "
                                             + "     , MM.speed_max                 speed_to                                            "
                                             + "     , MM.fine                      fine                                                "
                                             + "     , MM.create_dtm                create_dtm                                          "
                                             + "     , MM.create_id                 create_id                                           "
                                             + "     , CP.name                      create_nm                                           "
                                             + "     , MM.chg_dtm                   chg_dtm                                             "
                                             + "     , MM.chg_id                    chg_id                                              "
                                             + "     , GP.name                      chg_nm                                              "

                                             + "     , MM.offence_cd                offence_cd_org                                      "
                                             + "     , MM.reference                 reference_cd_org                                    "
                                             + "     , MM.relevant_legislation      relevant_legislation_cd_org                         "
                                             + "     , MM.category                  category_org                                        "
                                             + "     , MM.use_yn                    use_yn_org                                          "
                                             + "     , CAST(CL.cd_desc as INT)      speed_limit_org                                     "
                                             + "     , CAST(CL.cd_v_ext1 AS INT)    penalty_min_speed_org                               "
                                             + "     , CAST(CL.cd_v_ext2 AS INT)    penalty_max_speed_org                               "
                                             + "     , MM.speed_min                 speed_from_org                                      "
                                             + "     , MM.speed_max                 speed_to_org                                        "
                                             + "     , MM.fine                      fine_org                                            "

                                             + "     , CAST(0 AS INT)               rec_state                                           "

                                             + "  FROM OFFENCE_CODE             MM                                                      "
                                             + "       LEFT JOIN CODE_MASTER    CF ON CF.cd_grp     = 'OFFENCE_RF'                      "
                                             + "                                  AND CF.cd         = MM.reference                      "
                                             + "       LEFT JOIN CODE_GROUP     GF ON CF.cd_grp     = GF.cd_grp                         "
                                             + "       LEFT JOIN CODE_MASTER    CL ON CL.cd_grp     = 'OFFENCE_RL'                      "
                                             + "                                  AND CL.cd         = MM.relevant_legislation           "
                                             + "       LEFT JOIN CODE_GROUP     GL ON GL.cd_grp     = CL.cd_grp                         "
                                             + "       LEFT JOIN USERS          CU ON CU.user_id    = MM.create_id                      "
                                             + "       LEFT JOIN PERSONS        CP ON CP.prsn_id    = CU.prsn_id                        "
                                             + "       LEFT JOIN USERS          GU ON GU.user_id    = MM.chg_id                         "
                                             + "       LEFT JOIN PERSONS        GP ON GP.prsn_id    = GU.prsn_id                        "
                                             + " WHERE MM.use_yn                LIKE '{0}%'                                             "
                                             + " ORDER BY speed_limit ASC, relevant_legislation DESC, speed_from ASC, offence_cd ASC    "
                                             , strUseYN
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

                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return -1;
            }
            finally
            {

            }
        }


        //    // 특정 Location 코드가 존재하는지 확인
        //    public static bool GFn_IsExistOffenceCode(SqlConnection Conn, String strCode, ref int iSeq)
        //    {
        //        // 결과 값
        //        bool rv = false;
        //        iSeq = -1;

        //        // DB 접속 확인
        //        if (Conn == null) return rv;
        //        if (Conn.State == ConnectionState.Closed) Conn.Open();
        //        if (Conn.State == ConnectionState.Closed) return rv;

        //        // SQL 작성
        //        String SQLText = String.Format("SELECT ISNULL(MAX(location_seq), 0) + 1     seq "
        //                                     + "  FROM LOCATION_CODE                            "
        //                                     + " WHERE UPPER(location)      = UPPER('{0}')      "
        //                                     + "   AND use_yn               = 1                 "
        //                                     , strCode
        //                                      );

        //        // Query 
        //        SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
        //        var Values = sqlComm.ExecuteReader();

        //        try
        //        {
        //            // 0건
        //            if (!Values.HasRows)
        //            {
        //                //
        //            }
        //            else if (Values.Read())
        //            {
        //                iSeq = Convert.ToInt32(Values[0]);
        //                if (iSeq == 1) rv = false;
        //                else rv = true;

        //            }
        //            else
        //            {
        //                //
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            String strTmp = ex.Message;
        //            return rv;
        //        }
        //        finally
        //        {
        //            if (Values != null) Values.Close();
        //        }
        //        return rv;

        //    }

        //===========================================================//
        // Insert Offences
        //===========================================================//
        public static int GFn_InsertOffencesCode(SqlConnection Conn, String strUserId
                                               , String[] strArrOffence_cd
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
            int rv = 0;

            if (Conn == null)
            {
                return -1;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return -1;

            int iSCnt = 0;
            int iFCnt = 0;

            // 먼저 삭제 후 작업
            for (int i = 0; i < strArrOffence_cd.Length; i++)
            {
                if (strArrRecState[i] != "D") continue;

                String SQLText = "";
                SQLText = String.Format("DELETE FROM OFFENCE_CODE 	    "
                                      + "      WHERE offence_cd = '{0}' "
                                      , strArrOffence_cd_org[i]
                                       );

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


            // INSERT OR UPDATE
            for (int i = 0; i < strArrOffence_cd.Length; i++)
            {
                // 삭제는 위에서 별도 처리
                if (strArrRecState[i] == "D") continue;

                String SQLText = "";

                // INSERT
                if (strArrOffence_cd_org[i] == "")
                {
                    SQLText = String.Format("INSERT INTO OFFENCE_CODE (	                "

                                           + "					  offence_cd			"   // 단속 정보                                       
                                           + "					, reference			    "
                                           + "					, relevant_legislation	"
                                           + "					, category			    "

                                           + "					, speed_min				"
                                           + "					, speed_max				"
                                           + "					, fine				    "
                                           + "					, use_yn	   	        "

                                           + "					, create_dtm			"
                                           + "					, create_id		    	"
                                           + "					, chg_dtm				"
                                           + "					, chg_id			    "
                                           + "					)                       "

                                           + "           VALUES (                       "
                                           + "                    '{0}'                 "
                                           + "                  , '{1}'                 "
                                           + "                  , '{2}'                 "
                                           + "                  , '{3}'                 "
                                           + "                  , {4}                   "
                                           + "                  , {5}                   "
                                           + "                  , {6}                   "
                                           + "                  , '{7}'                 "

                                           + "					, GetDate()			    "
                                           + "					, '{8}'			    	"
                                           + "					, GetDate()				"
                                           + "					, '{8}'			    	"
                                           + "                  )                       "

                                           , strArrOffence_cd[i]
                                           , strArrReference_cd[i]
                                           , strArrLegislation_cd[i]
                                           , strArrCategory[i]
                                           , strArrSpeed_from[i]
                                           , strArrSpeed_to[i]
                                           , strArrFine[i]
                                           , strArrUse_yn[i]
                                           , strUserId
                                           );
                }
                // Update
                else
                {
                    SQLText = String.Format("UPDATE OFFENCE_CODE			                    "
                                           + "   SET reference              = '{0}'	            "
                                           + "     , relevant_legislation   = '{1}'             "
                                           + "     , category               = '{2}'             "
                                           + "     , speed_min              = {3}               "
                                           + "     , speed_max              = {4}               "
                                           + "     , fine                   = {5}               "
                                           + "     , use_yn                 = '{6}'             "

                                           + "     , chg_id                 = '{7}'		        "
                                           + "     , chg_dtm                = GetDate()	        "
                                           + " WHERE offence_cd             = '{8}'		        "
                                           , strArrReference_cd[i]
                                           , strArrLegislation_cd[i]
                                           , strArrCategory[i]
                                           , strArrSpeed_from[i]
                                           , strArrSpeed_to[i]
                                           , strArrFine[i]
                                           , strArrUse_yn[i]
                                           , strUserId
                                           , strArrOffence_cd_org[i]
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
                catch (Exception ex)
                {
                    // Build 시 메시지 나오지 말라고 ...
                    String tmp = ex.Message;

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
