using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// File 삭제
using System.IO;

namespace iTopsDistribute
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            try
            {
                // 사용자 정보를 Main으로 부터 받는다.
                if (args.Length < 1)
                {
                    // 단독 실행의 경우 Main을 삭제하여 접근을 못하게 하자!!!
                    String strMain = @".\iTopsMain";
                    if (File.Exists(strMain + ".exe"))
                    {
                        File.Delete(strMain + ".exe");
                        File.Delete(strMain + ".exe.config");
                        File.Delete(strMain + ".pdb");
                    }
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain(args));
            }
            catch (Exception ex)
            {
                String strTmp = ex.Message;

            }
            finally
            {

            }
        }
    }
}
