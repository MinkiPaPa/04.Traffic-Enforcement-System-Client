using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// ListViewItem, MessageBox 사용
using System.Windows.Forms;

// Path

namespace DBLibMngUser
{
    public class Persons
    {
        // Select PERSONS & USERS return dataset
        public static int GFn_GetPersonsDS(SqlConnection Conn, ref DataSet ds, bool bOnlyUseY = true)
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;


                //String strUseYN = bOnlyUseY ? "1" : "";
                String SQLText = String.Format("SELECT CAST(0 AS BIT)               sel_yn          "
                                             + "     , P.prsn_id                    prsn_id         "
                                             + "     , P.name                       name            "
                                             + "     , P.mobile                     mobile          "
                                             + "     , P.phone                      phone           "
                                             + "     , P.email                      email           "
                                             + "     , P.addr                       addr            "
                                             + "     , CAST(CASE ISNULL(U.user_id, '')              "
                                             + "                WHEN ''     THEN    'FALSE'         "
                                             + "                            ELSE    'TRUE'          "
                                             + "            END AS BIT)             user_yn         "
                                             + "     , U.user_id                    user_id         "
                                             + "     , U.user_pw                    user_pw         "
                                             + "     , U.level                      level           "
                                             + "     , U.role_tp                    role_tp         "
                                             + "     , ISNULL(U.allowed, 'FALSE')   allowed_yn      "
                                             + "     , ISNULL(U.fail_cnt, 0)        fail_cnt        "

                                             + "     , P.prsn_id                    prsn_id_org     "
                                             + "     , P.name                       name_org        "
                                             + "     , P.mobile                     mobile_org      "
                                             + "     , P.phone                      phone_org       "
                                             + "     , P.email                      email_org       "
                                             + "     , P.addr                       addr_org        "
                                             + "     , CAST(CASE ISNULL(U.user_id, '')              "
                                             + "                WHEN ''     THEN    'FALSE'         "
                                             + "                            ELSE    'TRUE'          "
                                             + "            END AS BIT)             user_yn_org     "
                                             + "     , U.user_id                    user_id_org     "
                                             + "     , U.user_pw                    user_pw_org     "
                                             + "     , U.level                      level_org       "
                                             + "     , U.role_tp                    role_tp_org     "
                                             + "     , ISNULL(U.allowed, 'FALSE')   allowed_yn_org  "
                                             + "     , ISNULL(U.fail_cnt, 0)        fail_cnt_org    "

                                             + "     , CAST(0 AS INT)               rec_state       "

                                             + "  FROM PERSONS  P                                   "
                                             + "       LEFT JOIN                                    "
                                             + "       USERS    U   ON U.prsn_id    = P.prsn_id     "
                                             + " ORDER BY name                                      "
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
        // Insert Persons
        //===========================================================//
        public static int GFn_InsertPersons(SqlConnection Conn, String strUserId
                                          , String[] strArrPrsnId
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
            int rv = 0;
            // DB 연결 확인
            if (Conn == null)
            {
                return -1;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return -1;

            // 작업 결과 Count
            int iSCnt = 0;
            int iFCnt = 0;

            //------------------------------------------------
            // 먼저 삭제 - PERSONS, USERS 모두 삭제
            //------------------------------------------------
            for (int i = 0; i < strArrPrsnId.Length; i++)
            {
                if (strArrRecState[i] != "D") continue;

                String SQLText = "";

                // Person Table 삭제
                SQLText = String.Format("DELETE FROM PERSONS 	        "
                                      + "      WHERE prsn_id = '{0}'    "
                                      , strArrPrsnId_ORG[i]
                                       );

                // 실행
                SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
                try
                {
                    rv = sqlComm.ExecuteNonQuery();

                    if (rv == 1)
                    {
                        // 성공 횟수 카운트 - Persons, Users 둘 다 삭제 성공하면 그때 Count
                        //iSCnt++;
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
                    continue;
                }
                finally
                {
                    //

                }


                //------------------------------------------------
                // 삭제시 Users에 계정이 존재하면 같이 삭제해준다.
                //------------------------------------------------
                if (strArrUserYN_ORG[i] != "Y") continue;

                // Users Table 삭제
                SQLText = String.Format("DELETE FROM USERS              "
                                      + "      WHERE prsn_id = '{0}'    "
                                      , strArrPrsnId_ORG[i]
                                       );

                // 실행
                sqlComm = new SqlCommand(SQLText, Conn);
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

                    // 결과 카운트
                    iFCnt++;
                    strArrResult[i] = "FAIL";
                }
                finally
                {
                    //

                }

            } // END for - 삭제 종료


            // INSERT OR UPDATE
            for (int i = 0; i < strArrPrsnId.Length; i++)
            {
                // 삭제는 위에서 별도 처리
                if (strArrRecState[i] == "D") continue;

                // 중복 검사 - PERSONS
                if (strArrPrsnId[i] != strArrPrsnId_ORG[i])
                {
                    String SQLTextExist = String.Format("SELECT COUNT(*)                cnt         "
                                                      + "  FROM PERSONS                             "
                                                      + " WHERE prsn_id                 = '{0}'     "
                                                      , strArrPrsnId[i]
                                                       );

                    SqlCommand sqlCommExist = new SqlCommand(SQLTextExist, Conn);
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
                        strArrResult[i] = "PERSONS Tables DUPLICATED";
                        continue;
                    }

                }

                // 중복 검사 - USERS
                if (strArrUserID[i] != strArrUserID_ORG[i])
                {
                    String SQLTextExist = String.Format("SELECT COUNT(*)                cnt         "
                                                      + "  FROM USERS                               "
                                                      + " WHERE user_id                 = '{0}'     "
                                                      , strArrUserID[i]
                                                       );

                    SqlCommand sqlCommExist = new SqlCommand(SQLTextExist, Conn);
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
                        strArrResult[i] = "USERS Table DUPLICATED";
                        continue;
                    }

                }



                String SQLText = "";

                // INSERT - PERSONS
                if (strArrPrsnId_ORG[i] == "")
                {
                    SQLText = String.Format("INSERT INTO PERSONS (	                            "
                                           + "					  prsn_id                       "
                                           + "					, name                          "
                                           + "					, email                         "
                                           + "					, phone                         "
                                           + "					, mobile                        "
                                           + "					, addr                          "
                                           //+ "					, dept                          "
                                           + "					, create_dtm                    "
                                           + "					, create_id                     "
                                           + "					, chg_dtm                       "
                                           + "					, chg_id                        "
                                           + "					)                               "

                                           + "           VALUES (                               "
                                           + "                    '{0}'                         "
                                           + "                  , '{1}'                         "
                                           + "                  , '{2}'                         "
                                           + "                  , '{3}'                         "
                                           + "                  , '{4}'                         "
                                           + "                  , '{5}'                         "

                                           + "					, GetDate()			            "
                                           + "					, '{6}'			    	        "
                                           + "					, GetDate()				        "
                                           + "					, '{6}'			    	        "
                                           + "                  )                               "

                                           , strArrPrsnId[i]
                                           , strArrName[i]
                                           , strArrEmail[i]
                                           , strArrPhone[i]
                                           , strArrMobile[i]
                                           , strArrAddr[i]

                                           , strUserId
                                           );
                }
                // Update
                else if (strArrPrsnState[i] == "U")
                {
                    SQLText = String.Format("UPDATE PERSONS			                            "
                                          + "   SET prsn_id                = '{0}'              "
                                          + "     , name                   = '{1}'              "
                                          + "	  , email                  = '{2}'              "
                                          + "	  , phone                  = '{3}'              "
                                          + "	  , mobile                 = '{4}'              "
                                          + "	  , addr                   = '{5}'              "

                                          + "     , chg_id                 = '{6}'		        "
                                          + "     , chg_dtm                = GetDate()	        "
                                          + " WHERE prsn_id                = '{7}'		        "

                                          , strArrPrsnId[i]
                                          , strArrName[i]
                                          , strArrEmail[i]
                                          , strArrPhone[i]
                                          , strArrMobile[i]
                                          , strArrAddr[i]
                                          , strUserId
                                          , strArrPrsnId_ORG[i]
                                           );

                }

                if (SQLText != String.Empty)
                {
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
                        continue;
                    }
                    finally
                    {
                        //

                    }

                }

                SQLText = "";
                // 사용자 계정 잠금 ---> 그냥 삭제
                if (strArrUserYN_ORG[i] == "Y" && strArrUserYN[i] == "N")
                {
                    //SQLText = String.Format("UPDATE USERS			                            "
                    //                      + "   SET allowed                 = 'false'           "

                    //                      + "     , chg_id                  = '{1}'		        "
                    //                      + "     , chg_dtm                 = GetDate()	        "
                    //                      + " WHERE user_id                 = '{2}'		        "

                    //                      , strArrAllowedYN[i]
                    //                      , strUserId
                    //                      , strArrUserID_ORG[i]
                    //                       );
                    SQLText = String.Format("DELETE FROM USERS			                            "
                                          + " WHERE user_id                 = '{0}'		        "

                                          //, strArrAllowedYN[i]
                                          //, strUserId
                                          , strArrUserID_ORG[i]
                                           );

                }
                // INSERT - USERS
                else if (strArrUserState[i] == "I")
                {
                    SQLText = String.Format("INSERT INTO USERS (	                            "
                                          + "				    user_id                         "
                                          + "				  , user_pw                         "
                                          + "				  , level                           "
                                          + "				  , prsn_id                         "
                                          + "				  , role_tp                         "
                                          + "				  , fail_cnt                        "
                                          + "				  , allowed                         "
                                          + "				  , create_dtm                      "
                                          + "				  , create_id                       "
                                          + "				  , chg_dtm                         "
                                          + "				  , chg_id                          "
                                          + "				   )                                "
                                          + "           VALUES (                                "
                                          + "                   '{0}'                           "
                                          + "                 , '{1}'                           "
                                          + "                 , '{2}'                           "
                                          + "                 , '{3}'                           "
                                          + "                 , '{4}'                           "
                                          + "                 ,  {5}                            "
                                          + "                 , CAST('{6}' AS BIT)              "
                                          + "				  , GetDate()			            "
                                          + "                 , '{7}'                           "
                                          + "				  , GetDate()				        "
                                          + "                 , '{7}'                           "
                                          + "                  )                                "

                                          , strArrUserID[i]
                                          , strArrUserPW[i]
                                          , strArrLevel[i]
                                          , strArrPrsnId[i]
                                          , strArrRoleTP[i]
                                          , 0                       // strArrFailCnt[i]
                                          , strArrAllowedYN[i]
                                          , strUserId
                                           );
                }
                // Update
                else if (strArrUserState[i] == "U")
                {
                    SQLText = String.Format("UPDATE USERS			                            "
                                          + "   SET user_id                 = '{0}'             "
                                          + "     , user_pw                 = '{1}'             "
                                          + "	  , level                   = '{2}'             "
                                          + "	  , prsn_id                 = '{3}'             "
                                          + "	  , role_tp                 = '{4}'             "
                                          + "	  , fail_cnt                = '{5}'             "
                                          + "     , allowed                 = '{6}'             "

                                          + "     , chg_id                  = '{7}'		        "
                                          + "     , chg_dtm                 = GetDate()	        "
                                          + " WHERE user_id                 = '{8}'		        "

                                          , strArrUserID[i]
                                          , strArrUserPW[i]
                                          , strArrLevel[i]
                                          , strArrPrsnId[i]
                                          , strArrRoleTP[i]
                                          , 0                       // strArrFailCnt[i]
                                          , strArrAllowedYN[i]
                                          , strUserId
                                          , strArrUserID_ORG[i]
                                           );

                }

                if (SQLText != String.Empty)
                {
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

                }

            } // END for 

            // 결과 Count
            iSCnt = 0;
            iFCnt = 0;
            for (int i = 0; i < strArrPrsnId.Length; i++)
            {
                if (strArrResult[i] == "OK") iSCnt++;
                else iFCnt++;
            }


            return iSCnt;
        }


    }
}
