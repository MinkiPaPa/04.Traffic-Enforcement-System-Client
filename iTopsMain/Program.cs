using System;
// Mutex ... 중복 실행 방지
using System.Threading;
using System.Windows.Forms;

namespace iTopsMain
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());

            bool bNew;
            Mutex mutex = new Mutex(true, "iTopsMain", out bNew);
            try
            {
                if (bNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmMain());

                    // Mutex 릴리즈
                    mutex.ReleaseMutex();

                }
                else
                {
                    MessageBox.Show("The application is running", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                //
                String strTmp = ex.Message;
            }
            finally
            {
                if (mutex != null)
                {
                    // Mutex 릴리즈
                    mutex.Dispose();
                }
            }
        }
    }
}
