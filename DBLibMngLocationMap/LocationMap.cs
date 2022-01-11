using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// ListViewItem, MessageBox 사용
using System.Windows.Forms;

namespace DBLibMngLocationMap
{
    public class LocationMap
    {
        // Select Location Code, return dataset
        public static int GFn_GetLocationMapDS(SqlConnection Conn, ref DataSet ds, bool bOnlyUseY = true)
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;

                String strUseYN = bOnlyUseY ? "1" : "";
                String SQLText = String.Format("SELECT CAST(0 AS BIT)               sel_yn                  "
                                             + "     , MM.court                     court                   "
                                             + "     , CC.cd_nm                     court_desc              "
                                             + "     , MM.location_cd               location                "
                                             + "     , LL.location_desc             location_desc           "
                                             + "     , MM.direction                 direction               "
                                             + "     , CD.cd_nm                     direction_desc          "
                                             + "     , MM.map_seq                   map_seq                 "
                                             + "     , MM.reference                 reference               "
                                             + "     , CF.cd_nm                     reference_desc          "
                                             + "     , MM.relevant_legislation      legislation             "
                                             + "     , CL.cd_nm                     legislation_desc        "
                                             + "     , MM.category                  category                "
                                             + "     , MM.map_desc                  map_desc                "
                                             + "     , MM.offence_list              offence_list            "
                                             + "     , MM.use_yn                    use_yn                  "
                                             + "     , MM.create_dtm                create_dtm              "
                                             + "     , MM.create_id                 create_id               "
                                             + "     , PC.name                      create_nm               "
                                             + "     , MM.chg_dtm                   chg_dtm                 "
                                             + "     , MM.chg_id                    chg_id                  "
                                             + "     , PG.name                      chg_nm                  "

                                             + "     , MM.court                     court_org               "
                                             + "     , MM.location_cd               location_org            "
                                             + "     , MM.direction                 direction_org           "
                                             + "     , MM.map_seq                   map_seq_org             "
                                             + "     , MM.reference                 reference_org           "
                                             + "     , MM.relevant_legislation      legislation_org         "
                                             + "     , MM.category                  category_org            "
                                             + "     , MM.map_desc                  map_desc_org            "
                                             + "     , MM.offence_list              offence_list_org        "
                                             + "     , MM.use_yn                    use_yn_org              "

                                             + "     , CAST(0 AS INT)               rec_state               "

                                             + "  FROM LOCATION_MAP                 MM                                                      "
                                             + "       LEFT JOIN    CODE_MASTER     CC      ON CC.cd_grp        = 'COURT'                   "
                                             + "                                           AND CC.cd            = MM.court                  "
                                             + "       LEFT JOIN    LOCATION_CODE   LL      ON LL.court         = MM.court                  "
                                             + "                                           AND LL.location_cd   = MM.location_cd            "
                                             + "       LEFT JOIN    CODE_MASTER     CD      ON CD.cd_grp        = 'DIRECTION'               "
                                             + "                                           AND CD.cd            = MM.direction              "
                                             + "       LEFT JOIN    CODE_MASTER     CF      ON CF.cd_grp        = 'OFFENCE_RF'              "
                                             + "                                           AND CF.cd            = MM.reference              "
                                             + "       LEFT JOIN    CODE_MASTER     CL      ON CL.cd_grp        = 'OFFENCE_RL'              "
                                             + "                                           AND CL.cd            = MM.relevant_legislation   "
                                             + "       LEFT JOIN    USERS           UC      ON UC.user_id       = MM.create_id              "
                                             + "       LEFT JOIN    PERSONS         PC      ON PC.prsn_id       = UC.prsn_id                "
                                             + "       LEFT JOIN    USERS           UG      ON UG.user_id       = MM.chg_id                 "
                                             + "       LEFT JOIN    PERSONS         PG      ON PG.prsn_id       = UG.prsn_id                "
                                             + " WHERE MM.use_yn                    LIKE '{0}%'                                             "
                                             + " ORDER BY court, location, direction, map_seq                                               "
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

        //===========================================================//
        // Insert Location Mapping Data
        //===========================================================//
        public static int GFn_InsertLocationMap(SqlConnection Conn, String strUserId
                                              , String[] strArrCourt
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
            for (int i = 0; i < strArrLocationCd.Length; i++)
            {
                if (strArrRecState[i] != "D") continue;

                String SQLText = "";
                SQLText = String.Format("DELETE FROM LOCATION_MAP	        "
                                      + "      WHERE court          = '{0}' "
                                      + "        AND location_cd    = '{1}' "
                                      + "        AND direction      = '{2}' "
                                      + "        AND map_seq        =  {3}  "
                                      , strArrCourt_org[i]
                                      , strArrLocationCd_org[i]
                                      , strArrDirection_org[i]
                                      , strArrMapSeq_org[i]
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
            for (int i = 0; i < strArrLocationCd.Length; i++)
            {
                // 삭제는 위에서 별도 처리
                if (strArrRecState[i] == "D") continue;

                String SQLText = "";

                // INSERT
                if (strArrLocationCd_org[i] == "")
                {
                    //-----------------------------------------------------------------------
                    // 1. 중복 체크 - 순번제외하고 필수 항목으로 비교
                    //-----------------------------------------------------------------------
                    SQLText = String.Format("SELECT COUNT(*)                cnt         "
                                          + "  FROM LOCATION_MAP                        "
                                          + " WHERE court                   = '{0}'     "
                                          + "   AND location_cd             = '{1}'     "
                                          + "   AND direction               = '{2}'     "
                                          + "   AND reference               = '{3}'     "
                                          + "   AND relevant_legislation    = '{4}'     "
                                          + "   AND category                = '{5}'     "
                                          , strArrCourt[i]
                                          , strArrLocationCd[i]
                                          , strArrDirection[i]
                                          , strArrReference[i]
                                          , strArrLegislation[i]
                                          , strArrCategory[i]
                                           );

                    SqlCommand sqlCommExist = new SqlCommand(SQLText, Conn);
                    SqlDataReader Values = sqlCommExist.ExecuteReader();
                    bool bExist = false;
                    try
                    {
                        // 0건
                        if (!Values.HasRows)
                        {
                            // 실패
                            bExist = true;
                        }
                        else
                        {
                            if (Values.Read())
                            {
                                //if (Convert.ToInt32(Values["cnt"] as String) != 0) 
                                if (Convert.ToInt32(Values["cnt"]) != 0)
                                {
                                    // 실패
                                    bExist = true;
                                }
                            }
                            else
                            {
                                // 실패
                                bExist = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;

                        // 실패
                        bExist = true;
                    }
                    finally
                    {
                        if (Values != null) Values.Close();
                    }

                    // 중복 여부 판단
                    if (bExist)
                    {
                        // 실패 횟수 카운트
                        iFCnt++;
                        strArrResult[i] = "DUPLICATED";
                        continue;
                    }

                    //-----------------------------------------------------------------------
                    // 2. 순번 추출
                    //-----------------------------------------------------------------------
                    SQLText = String.Format("SELECT ISNULL(MAX(map_seq), 0) + 1     map_seq "
                                          + "  FROM LOCATION_MAP                            "
                                          + " WHERE court                   = '{0}'         "
                                          + "   AND location_cd             = '{1}'         "
                                          + "   AND direction               = '{2}'         "
                                          , strArrCourt[i]
                                          , strArrLocationCd[i]
                                          , strArrDirection[i]
                                           );

                    //SQLText = String.Format("SELECT 1    map_seq "
                    //                      , strArrCourt[i]
                    //                      , strArrLocationCd[i]
                    //                      , strArrDirection[i]
                    //                       );


                    SqlCommand sqlCommSeq = new SqlCommand(SQLText, Conn);
                    SqlDataReader ValuesSeq = sqlCommSeq.ExecuteReader();
                    int iSeq = 0;
                    try
                    {
                        // 0건
                        if (!ValuesSeq.HasRows)
                        {
                            // 실패
                            iSeq = -1;
                        }
                        else
                        {
                            if (ValuesSeq.Read())
                            {
                                iSeq = Convert.ToInt32(ValuesSeq["map_seq"]);
                                if (iSeq == 0)
                                {
                                    // 실패
                                    iSeq = -1;
                                }

                            }
                            else
                            {
                                // 실패
                                iSeq = -1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        String strTmp = ex.Message;

                        // 실패
                        iSeq = -1;
                    }
                    finally
                    {
                        if (ValuesSeq != null) ValuesSeq.Close();
                    }

                    // 중복 여부 판단
                    if (iSeq < 1)
                    {
                        iSeq = -1;
                        // 실패 횟수 카운트
                        iFCnt++;
                        strArrResult[i] = "FAIL";
                        continue;
                    }


                    SQLText = String.Format("INSERT INTO LOCATION_MAP           "
                                          + "          ( court                  "
                                          + "          , location_cd            "
                                          + "          , direction              "
                                          + "          , map_seq                "
                                          + "          , reference              "
                                          + "          , relevant_legislation   "
                                          + "          , category               "
                                          + "          , map_desc               "
                                          + "          , offence_list           "
                                          + "          , use_yn                 "
                                          + "          , create_dtm             "
                                          + "          , create_id              "
                                          + "          , chg_dtm                "
                                          + "          , chg_id                 "
                                          + "          )                        "
                                          + "   VALUES ( '{0}'                  "
                                          + "          , '{1}'                  "
                                          + "          , '{2}'                  "
                                          + "          ,  {3}                   "
                                          + "          , '{4}'                  "
                                          + "          , '{5}'                  "
                                          + "          , '{6}'                  "
                                          + "          , '{7}'                  "
                                          + "          , '{8}'                  "
                                          + "          , '{9}'                  "
                                          + "          , GetDate()              "
                                          + "          , '{10}'                 "
                                          + "          , GetDate()              "
                                          + "          , '{10}'                 "
                                          + "          )                        "
                                          , strArrCourt[i]
                                          , strArrLocationCd[i]
                                          , strArrDirection[i]
                                          , iSeq
                                          , strArrReference[i]
                                          , strArrLegislation[i]
                                          , strArrCategory[i]
                                          , strArrMapDesc[i]
                                          , strArrOffence_List[i]
                                          , strArrUse_yn[i]
                                          , strUserId
                                           );
                }
                // Update
                else
                {
                    SQLText = String.Format("UPDATE LOCATION_MAP                        "
                                          + "   SET reference               = '{0}'     "
                                          + "     , relevant_legislation    = '{1}'     "
                                          + "     , category                = '{2}'     "
                                          + "     , map_desc                = '{3}'     "
                                          + "     , offence_list            = '{4}'     "
                                          + "     , use_yn                  = '{5}'     "
                                          + "     , chg_dtm                 = GetDate() "
                                          + "     , chg_id                  = '{6}'     "
                                          + " WHERE court                   = '{7}'     "
                                          + "   AND location_cd             = '{8}'     "
                                          + "   AND direction               = '{9}'     "
                                          + "   AND map_seq                 =  {10}     "
                                          , strArrReference[i]
                                          , strArrLegislation[i]
                                          , strArrCategory[i]
                                          , strArrMapDesc[i]
                                          , strArrOffence_List[i]
                                          , strArrUse_yn[i]
                                          , strUserId
                                          , strArrCourt[i]
                                          , strArrLocationCd[i]
                                          , strArrDirection[i]
                                          , strArrMapSeq[i]
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
