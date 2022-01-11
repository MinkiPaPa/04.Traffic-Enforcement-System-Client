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
    class Contraventions
    {
        //===========================================================//
        // Insert & Update Contraventions - Supervisor가 검수 완료한 경우 
        //===========================================================//
        public static bool InsertContraventions(SqlConnection Conn, String strUserId
                                              , bool bIsSupervisor
                                              , String strArrOffence_id
                                         )
        {
            int rv = 0;

            if (Conn == null)
            {
                return false;
            }
            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (Conn.State == ConnectionState.Closed) return false;

            String SQLText = "";

            // INSERT
            if (strArrOffence_id != "")
            {
                SQLText = String.Format("INSERT INTO CONTRAVENTIONS ( offence_id           "
                                       + "                           , interface            "
                                       + "                           , branch               "
                                       + "                           , officer              "
                                       + "                           , device_type          "
                                       + "                           , device_mdl           "
                                       + "                           , device_sn            "
                                       + "                           , when_dt              "
                                       + "                           , court                "
                                       + "                           , street               "
                                       + "                           , location             "
                                       + "                           , direction            "
                                       + "                           , manual               "
                                       + "                           , lane                 "
                                       + "                           , speed_regal          "
                                       + "                           , speed_is             "
                                       + "                           , distance             "
                                       + "                           , file_directory       "
                                       + "                           , file_original        "
                                       + "                           , file_no              "
                                       + "                           , file_name            "
                                       + "                           , file_plate           "
                                       + "                           , offence_code         "
                                       + "                           , fine                 "
                                       + "                           , edit_who             "
                                       + "                           , edit_time            "
                                       + "                           , edit_is              "
                                       + "                           , optr_who             "
                                       + "                           , optr_time            "
                                       + "                           , optr_is              "
                                       + "                           , carnum               "
                                       + "                           , carcolor             "
                                       + "                           , carmake              "
                                       + "                           , carmake_code         "
                                       + "                           , cartype              "
                                       + "                           , cartype_code         "
                                       + "                           , code_status          "
                                       + "                           , code_status_aux      "
                                       + "                           , status               "
                                       + "                           , cctime               "
                                       + "                           )                      "
                                       + "                      SELECT offence_id           "
                                       + "                           , 'interface'          "
                                       + "                           , branch               "
                                       + "                           , officer              "
                                       + "                           , device_type          "
                                       + "                           , device_mdl           "
                                       + "                           , device_sn            "
                                       + "                           , regulation_time      "
                                       + "                           , court                "
                                       + "                           , street               "
                                       + "                           , location             "
                                       + "                           , direction            "
                                       + "                           , CASE SIGN(manual_yn) WHEN 1 THEN 'Y' ELSE 'N' END "
                                       + "                           , regulation_lane      "
                                       + "                           , regulation_spd_limit "
                                       + "                           , real_speed           "
                                       + "                           , regulation_distance  "
                                       + "                           , file_directory       "
                                       + "                           , file_original        "
                                       + "                           , file_no              "
                                       + "                           , file_name            "
                                       + "                           , file_plate           "
                                       + "                           , offence_code         "
                                       + "                           , fine                 "
                                       + "                           , upload_id            "
                                       + "                           , upload_time          "
                                       + "                           , 1                    "   // edit_is              "
                                       + "                           , inspection_id        "
                                       + "                           , inspection_time      "
                                       + "                           , 1                    "   // optr_is              "
                                       + "                           , vehicle_plate        "
                                       + "                           , vehicle_color        "
                                       + "                           , vehicle_maker        "   // carmake
                                       + "                           , vehicle_maker_cd     "   // carmake_code    
                                       + "                           , vehicle_type         "   // cartype         
                                       + "                           , vehicle_type_cd      "   // cartype_code    
                                       + "                           , 0                    "   // code_status     
                                       + "                           , null                 "   // code_status_aux 
                                       + "                           , ''                   "   // status          
                                       + "                           , GetDate()            "   // cctime          
                                       + "                        FROM OFFENCES             "
                                       + "                       WHERE offence_id   = '{0}' "
                                       , strArrOffence_id);

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
                }
                return true;
            }
            catch (Exception e)
            {
                // Insert 실패시 Update ????
                // 생각해 보고 나중에 Update 넣어야 하면 여기에 ...



                // Build 시 메시지 나오지 말라고 ...
                String tmp = e.Message;

                return false;
            }
            finally
            {
                //

            }


        }
    }
}
