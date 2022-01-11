using System;
// DataSet 사용
using System.Data;
// SQL 접속
using System.Data.SqlClient;
// ListViewItem, MessageBox 사용
using System.Windows.Forms;

namespace DBLibMngLocationMap
{
    public class Offence_code
    {
        // Select Location Code, return dataset
        public static int GFn_GetOffenceCodeForMapDS(SqlConnection Conn, ref DataSet ds
                                                   , String strReferenceCd
                                                   , String strLegislationCd
                                                   , String strCategory
                                                   , bool bOnlyUseY = true
                                                    )
        {
            try
            {
                int rv = -1;
                ds.Clear();

                if (Conn == null) return rv;
                if (Conn.State == ConnectionState.Closed) Conn.Open();
                if (Conn.State == ConnectionState.Closed) return rv;

                String strUseYN = bOnlyUseY ? "1" : "";
                String SQLText = String.Format("SELECT CAST(0 AS BIT)               sel_yn                      "
                                             + "     , MM.offence_cd                offence_cd                  "
                                             + "     , MM.reference                 reference_cd                "
                                             + "     , MM.relevant_legislation      legislation_cd              "
                                             + "     , MM.category                  category                    "
                                             + "     , MM.use_yn                    use_yn                      "
                                             + "     , MM.speed_min                 speed_from                  "
                                             + "     , MM.speed_max                 speed_to                    "
                                             + "     , MM.fine                      fine                        "
                                             + "     , CAST(0 AS BIT)               sel_yn_org                  "
                                             + "     , CAST(0 AS INT)               rec_state                   "
                                             + "  FROM OFFENCE_CODE             MM                              "
                                             + " WHERE MM.use_yn                LIKE '{0}%'                     "
                                             + "   AND MM.reference             = '{1}'                         "
                                             + "   AND MM.relevant_legislation  = '{2}'                         "
                                             + "   AND MM.category              = '{3}'                         "
                                             + " ORDER BY reference_cd, legislation_cd, category, speed_from ASC, offence_cd ASC    "
                                             , strUseYN
                                             , strReferenceCd
                                             , strLegislationCd
                                             , strCategory
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

    }
}
