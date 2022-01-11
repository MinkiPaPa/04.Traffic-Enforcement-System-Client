using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// ListViewItem, MessageBox 사용
using System.Windows.Forms;

// Path

namespace DBCodes
{
    // Code 
    public enum LvCode
    {
        CODE = 0           // ClipName - textdataW.ClipName
      , DESC = 1           // ClipType - textdataW.ClipType
    }

    public class Codes
    {

        // Select Code, return dataset
        public static int GFn_GetCodeDS(SqlConnection Conn, String strCodeGrp, String strCode, ref DataSet ds, bool bOnlyUseY = true, bool bIncludeNull = false)
        {
            try
            {
                int rv = 0;
                ds.Clear();

                if (Conn == null)
                {
                    return -1;
                }
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return -1;

                String strUseYN = bOnlyUseY ? "1" : "";
                String strIncludeNull = bIncludeNull ? "1" : "0";
                String SQLText = String.Format("SELECT cd                                   "
                                              + "     , cd_nm                               "
                                              + "     , CASE cd                             "
                                              + "            WHEN ''    THEN ''             "
                                              + "            ELSE '[' + cd + '] ' + cd_nm   "
                                              + "       END  display_value                  "
                                              // 2019/08/22 추가
                                              + "     , cd_desc                             "
                                              // 2019/08/02 추가
                                              + "     , cd_t_ext1                           "
                                              + "     , cd_v_ext1                           "
                                              + "     , cd_t_ext2                           "
                                              + "     , cd_v_ext2                           "
                                              //+ "     , '[' + cd + '] ' + cd_nm     display_value"
                                              + "  FROM ("
                                              + "         SELECT 1                  seq     "
                                              + "              , C.cd               cd      "
                                              + "              , C.cd_nm            cd_nm   "
                                              + "              , C.cd_desc          cd_desc "
                                              + "              , C.cd_t_ext1        cd_t_ext1"
                                              + "              , C.cd_v_ext1        cd_v_ext1"
                                              + "              , C.cd_t_ext2        cd_t_ext2"
                                              + "              , C.cd_v_ext2        cd_v_ext2"
                                              + "           FROM CODE_GROUP         G"
                                              + "              , CODE_MASTER        C"
                                              + "          WHERE UPPER(G.cd_grp)    = '{0}' "
                                              + "            AND G.use_yn           LIKE '{1}%'"
                                              + "            AND UPPER(G.cd_grp)    = C.cd_grp"
                                              // 2019.08.22 - OFENCE_RF cd 일부를 얻기 위함
                                              //+ "            AND UPPER(C.cd)        LIKE '%{2}%'"
                                              + "            AND (                                  "
                                              + "                 UPPER(C.cd)        LIKE '%{2}%' OR"
                                              + "                 '{2}' LIKE '%'+SUBSTRING(cd, 7, 3) + '%'"     // OFENCE_RF cd 일부를 얻기 위함
                                              + "                )                                  "
                                              + "            AND C.use_yn           LIKE '{1}%'"
                                              // code에 null 넣을 것인지
                                              + "         UNION"
                                              + "         SELECT 0                  seq     "
                                              + "              , ''                 cd      "
                                              + "              , ''                 cd_nm   "
                                              + "              , ''                 cd_desc "
                                              + "              , 0                  cd_t_ext1"
                                              + "              , ''                 cd_v_ext1"
                                              + "              , 0                  cd_t_ext2"
                                              + "              , ''                 cd_v_ext2"
                                              + "          WHERE 1 = {3}"
                                              + "       ) M"
                                              + " ORDER BY seq, cd "
                                              , strCodeGrp.ToUpper(), strUseYN, strCode.ToUpper(), strIncludeNull
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

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
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

        // 특정 코드 조회 
        public static String GFn_GetCodeValue(SqlConnection Conn, String strCodeGrp, String strCode, bool bOnlyUseY = true)
        {

            if (Conn == null)
            {
                return null;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return null;

            String strUseYN = bOnlyUseY ? "1" : "";
            String SQLText = String.Format("SELECT C.cd                cd"
                                          + "     , C.cd_nm             cd_nm"
                                          + "     , C.cd_desc           cd_desc"
                                          + "     , C.cd_t_ext1         cd_t_ext1"
                                          + "     , C.cd_v_ext1         cd_v_ext1"
                                          + "     , C.cd_t_ext2         cd_t_ext2"
                                          + "     , C.cd_v_ext2         cd_v_ext2"
                                          + "  FROM CODE_GROUP          G"
                                          + "     , CODE_MASTER         C"
                                          + " WHERE UPPER(G.cd_grp)     = UPPER('{0}') "
                                          + "   AND G.use_yn            LIKE '{1}%'"
                                          + "   AND UPPER(G.cd_grp)     = UPPER(C.cd_grp)"
                                          + "   AND UPPER(C.cd)         = UPPER('{2}')"
                                          + "   AND C.use_yn            LIKE '{1}%'"
                                          , strCodeGrp, strUseYN, strCode
                                          );
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            var Values = sqlComm.ExecuteReader();
            try
            {
                // 0건
                if (!Values.HasRows) return null;
                if (Values.Read())
                {
                    return Values[1].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return null;

            }
            finally
            {
                if (Values != null) Values.Close();
            }
        }


        // Inspector 조회
        public static int GFn_GetInspector(SqlConnection Conn, ref DataSet ds)
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

                String SQLText = String.Format("SELECT user_id										    "
                                              + "     , user_nm                                         "
                                              + "  FROM (                                               "
                                              + "        SELECT 0            gb                         "
                                              + "             , 'ALL'        user_id                    "
                                              + "             , 'ALL'        user_nm                    "
                                              + "        UNION ALL                                      "
                                              + "        SELECT 1            gb                         "
                                              + "             , U.user_id    user_id                    "
                                              + "             , P.name       user_nm                    "
                                              + "          FROM USERS        U                          "
                                              + "               LEFT JOIN                               "
                                              + "               PERSONS     P ON U.prsn_id = P.prsn_id  "
                                              + "               LEFT JOIN                               "
                                              + "               CODE_MASTER C ON U.role_tp = C.cd       "
                                              + "         WHERE U.allowed       = 1                     "
                                              + "           AND C.cd_grp        = 'ROLE_TP'             "
                                              + "           AND UPPER(C.cd_nm)  = 'INSPECTOR'           "
                                              + "       )   M                                           "
                                              + " ORDER BY gb, user_nm, user_id                         "
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

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
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

        // Officer 조회
        public static int GFn_GetOfficer(SqlConnection Conn, ref DataSet ds)
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

                String SQLText = String.Format("SELECT user_id										    "
                                              + "     , user_nm                                         "
                                              + "  FROM (                                               "
                                              + "        SELECT 0            gb                         "
                                              + "             , 'ALL'        user_id                    "
                                              + "             , 'ALL'        user_nm                    "
                                              + "        UNION ALL                                      "
                                              + "        SELECT 1            gb                         "
                                              + "             , U.user_id    user_id                    "
                                              + "             , P.name       user_nm                    "
                                              + "          FROM USERS        U                          "
                                              + "               LEFT JOIN                               "
                                              + "               PERSONS     P ON U.prsn_id = P.prsn_id  "
                                              + "               LEFT JOIN                               "
                                              + "               CODE_MASTER C ON U.role_tp = C.cd       "
                                              + "         WHERE U.allowed       = 1                     "
                                              + "           AND C.cd_grp        = 'ROLE_TP'             "
                                              + "           AND UPPER(C.cd_nm)  = 'OFFICER'             "
                                              + "       )   M                                           "
                                              + " ORDER BY gb, user_nm, user_id                         "
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

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
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

        // VEHICLE_MAKE
        public static int GFn_GetVehicleMake(SqlConnection Conn, ref DataSet ds, String strSearch)
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

                String SQLText = String.Format("SELECT vhcl_make_cd "
                                             + "     , vhcl_make_nm "
                                             + "     , '[' + vhcl_make_cd + '] ' + vhcl_make_nm     display_value"
                                             + "  FROM VEHICLE_MAKE "
                                             + " WHERE del_yn = 'false' "
                                             + "   AND ( "
                                             + "         UPPER(vhcl_make_cd) LIKE '%{0}%' OR "
                                             + "         UPPER(vhcl_make_nm) LIKE '%{0}%' "
                                             + "       ) "
                                             + " ORDER BY vhcl_make_nm "
                                             , strSearch);
                // 실행
                SqlDataAdapter sda = new SqlDataAdapter();

                sda.SelectCommand = new SqlCommand(SQLText, Conn);
                rv = sda.Fill(ds);

                return rv;
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return -1;
            }
            finally
            {
                //
            }
        }

        // 특정 자동차 제조사 조회 
        public static String GFn_GetVehicleMakeValue(SqlConnection Conn, String strSearch, bool bOnlyUseY = true)
        {

            if (Conn == null)
            {
                return null;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return null;

            //String strUseYN = bOnlyUseY ? "1" : "";
            String SQLText = String.Format("SELECT vhcl_make_cd "
                                         + "     , vhcl_make_nm "
                                         + "  FROM VEHICLE_MAKE "
                                         + " WHERE del_yn = 'false' "
                                         + "   AND UPPER(vhcl_make_cd) = '{0}'"
                                         + " ORDER BY vhcl_make_nm "
                                         , strSearch);
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            var Values = sqlComm.ExecuteReader();
            try
            {
                // 0건
                if (!Values.HasRows) return null;
                if (Values.Read())
                {
                    return Values[1].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return null;

            }
            finally
            {
                if (Values != null) Values.Close();
            }
        }


        // VEHICLE_TYPE
        public static int GFn_GetVehicleType(SqlConnection Conn, ref DataSet ds, String strSearch)
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

                String SQLText = String.Format("SELECT vhcl_type_cd "
                                             + "     , vhcl_type_nm "
                                             + "     , '[' + vhcl_type_cd + '] ' + vhcl_type_nm     display_value"
                                             + "     , vhcl_spc_type "
                                             + "  FROM VEHICLE_TYPE "
                                             + " WHERE del_yn = 'false' "
                                             + "   AND ( "
                                             + "         UPPER(vhcl_type_cd) LIKE '%{0}%' OR "
                                             + "         UPPER(vhcl_type_nm) LIKE '%{0}%' "
                                             + "       ) "
                                             + " ORDER BY vhcl_type_nm "
                                             , strSearch);
                // 실행
                SqlDataAdapter sda = new SqlDataAdapter();

                sda.SelectCommand = new SqlCommand(SQLText, Conn);
                rv = sda.Fill(ds);

                return rv;

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return -1;
            }
            finally
            {
                //
            }
        }

        // 특정 자동차 Type 조회 
        public static String GFn_GetVehicleTypeValue(SqlConnection Conn, String strSearch, bool bOnlyUseY = true)
        {

            if (Conn == null)
            {
                return null;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return null;

            //String strUseYN = bOnlyUseY ? "1" : "";
            String SQLText = String.Format("SELECT vhcl_type_cd "
                                         + "     , vhcl_type_nm "
                                         + "  FROM VEHICLE_TYPE "
                                         + " WHERE del_yn = 'false' "
                                         + "   AND UPPER(vhcl_type_cd) = '{0}' "
                                         + " ORDER BY vhcl_type_nm "
                                         , strSearch);
            SqlCommand sqlComm = new SqlCommand(SQLText, Conn);
            var Values = sqlComm.ExecuteReader();
            try
            {
                // 0건
                if (!Values.HasRows) return null;
                if (Values.Read())
                {
                    return Values[1].ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

                //MessageBox.Show("Unable to connect to database.\n\n"
                //              + "Check the status of the data server or network and try again."
                //              , "Error"
                //              , MessageBoxButtons.OK
                //              , MessageBoxIcon.Error);
                MessageBox.Show(strTmp
                              , "Error"
                              , MessageBoxButtons.OK
                              , MessageBoxIcon.Error);

                return null;

            }
            finally
            {
                if (Values != null) Values.Close();
            }
        }

        // Select Location Code, return dataset
        public static int GFn_GetLocationCodeDS(SqlConnection Conn, ref DataSet ds, bool bOnlyUseY = true, String sCourt = "")
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;

                String SQLText = String.Format("SELECT location_cd          cd                              "
                                             + "     , location_desc        cd_nm                           "
                                             + "     , CASE location_cd                                     "
                                             + "            WHEN ''     THEN ''                             "
                                             + "            ELSE '[' + location_cd + '] ' + location_desc   "
                                             + "       END  display_value                                   "
                                             + "     , court                                                "
                                             + "     , speed_limit                                          "
                                             + "  FROM LOCATION_CODE                                        "
                                             + " WHERE use_yn       = '{0}'                                 "
                                             + "   AND court        LIKE '%{1}%'                            "
                                             , bOnlyUseY
                                             , sCourt
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

        // 사용자 권한 등급 코드 조회 
        public static int GFn_GetPermissionLevelDS(SqlConnection Conn, ref DataSet ds, bool bOnlyUseY = true)
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;

                String strUseYN = bOnlyUseY ? "1" : "";
                String SQLText = String.Format("SELECT R.cd             role_tp                                 "
                                             + "     , R.cd_nm          role_nm                                 "
                                             + "     , L.cd_nm          level                                   "
                                             + "  FROM CODE_MASTER      R                                       "
                                             + "       LEFT JOIN                                                "
                                             + "       CODE_MASTER      L   ON L.cd_grp     = 'USR_LV'          "
                                             + "                           AND L.cd         = UPPER(R.cd_nm)    "
                                             + "                           AND L.use_yn     LIKE '{0}%'         "
                                             + " WHERE R.cd_grp         = 'ROLE_TP'                             "
                                             + "   AND R.use_yn         LIKE '{0}%'                             "
                                             + " ORDER BY level                                                 "
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


        // Remapping Fine ... 2020.02.19
        public static int GFn_GetReMappingFine(SqlConnection Conn, ref DataSet ds
                                              , String strCourtCd
                                              , String LocationCd
                                              , String strLimitSpd
                                              , String strRealSpd
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

                String SQLText = String.Format("SELECT ISNULL(offence_cd_2, ISNULL(offence_cd_1, offence_cd_0)) offence_cd "
                                                        + "         , ISNULL(CASE ISNULL(offence_cd_2, 'N') "
                                                        + "                             WHEN 'N' THEN CASE ISNULL(offence_cd_1, 'N') "
                                                        + "                                                            WHEN 'N' THEN fine_0 "
                                                        + "                                                            ELSE fine_1 "
                                                        + "                                                    END "
                                                        + "                                           ELSE fine_2 "
                                                        + "                      END, 0.00)  fine "
                                                        + "  FROM ( "
                                                        + "             SELECT court "
                                                        + "                      , location_cd "
                                                        + "                      , speed_limit "
                                                        + "                      , reg_min "
                                                        + "                      , reg_max "
                                                        + "                      , speed_min "
                                                        + "                      , speed_max "
                                                        + "                      , MAX(offence_cd)  offence_cd_0 "
                                                        + "                      , MAX(map_nm    )  map_nm_0 "
                                                        + "                      , MAX(fine      )  fine_0 "
                                                        + "                      , MAX(CASE map_position WHEN 1 THEN offence_cd ELSE null END)  offence_cd_1 "
                                                        + "                      , MAX(CASE map_position WHEN 1 THEN map_nm     ELSE null END)  map_nm_1 "
                                                        + "                      , MAX(CASE map_position WHEN 1 THEN fine       ELSE 0    END)  fine_1 "
                                                        + "                      , MAX(CASE map_position WHEN 2 THEN offence_cd ELSE null END)  offence_cd_2 "
                                                        + "                      , MAX(CASE map_position WHEN 2 THEN map_nm     ELSE null END)  map_nm_2 "
                                                        + "                      , MAX(CASE map_position WHEN 2 THEN fine       ELSE 0    END)  fine_2 "
                                                        + "               FROM ( "
                                                        + "                          SELECT M.court                  court "
                                                        + "                                   , M.location_cd            location_cd "
                                                        + "                                   , M.reference              reference "
                                                        + "                                   , M.relevant_legislation   relevant_legislation "
                                                        + "                                   , M.category               category "
                                                        + "                                   , L.cd_desc                speed_limit "
                                                        + "                                   , L.cd_v_ext1              reg_min "
                                                        + "                                   , L.cd_v_ext2              reg_max "
                                                        + "                                   , O.speed_min              speed_min "
                                                        + "                                   , O.speed_max              speed_max "
                                                        + "                                   , O.fine                   fine "
                                                        + "                                   , O.offence_cd             offence_cd "
                                                        + "                                   , T.cd                     map_cd "
                                                        + "                                   , T.cd_nm                  map_nm "
                                                        + "                                   , T.cd_v_ext1              map_position "
                                                        + "                            FROM LOCATION_MAP             M "
                                                        + "                                     LEFT JOIN "
                                                        + "                                     CODE_MASTER              R   ON R.cd_grp = 'OFFENCE_RF' "
                                                        + "                                                                    AND R.cd = M.reference "
                                                        + "                                     LEFT JOIN "
                                                        + "                                     CODE_MASTER              L   ON L.cd_grp = 'OFFENCE_RL' "
                                                        + "                                                                    AND L.cd = M.relevant_legislation "
                                                        + "                                     LEFT JOIN "
                                                        + "                                     OFFENCE_CODE             O   ON M.reference = O.reference "
                                                        + "                                                                    AND M.relevant_legislation = O.relevant_legislation "
                                                        + "                                                                    AND M.category = O.category "
                                                        + "                                                                    AND M.offence_list           LIKE '%' + O.offence_cd + '%' "
                                                        + "                                     LEFT JOIN "
                                                        + "                                     CODE_MASTER              T   ON T.cd_grp = 'ROAD_TYPE' "
                                                        + "                                                                 AND M.relevant_legislation   LIKE '%' + T.cd_nm "
                                                        + "                        )       FF "
                                                        + "              GROUP BY court "
                                                        + "                            , location_cd "
                                                        + "                            , speed_limit "
                                                        + "                            , reg_min "
                                                        + "                            , reg_max "
                                                        + "                            , speed_min "
                                                        + "                            , speed_max "
                                                        + "           )   F "
                                                        + " WHERE F.court                    = '{0}' "
                                                        + "   AND F.location_cd              = '{1}' "
                                                        + "   AND F.speed_limit              = {2} "
                                                        + "   AND F.speed_min                <= {3} "
                                                        + "   AND ISNULL(F.speed_max, 999)   >= {3} "
                                             , strCourtCd
                                             , LocationCd
                                             , strLimitSpd
                                             , strRealSpd
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
                //
            }
        }



    }
}
