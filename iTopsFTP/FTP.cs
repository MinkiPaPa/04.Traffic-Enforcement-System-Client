using System;

//
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace iTopsFTP
{
    public class FTP
    {

        //----------------------------------------------------------------------//
        // File DownLoad
        //----------------------------------------------------------------------//
        public static bool Fn_FileDownLoad(String svrIP
                                          , String svrFile
                                          , String svrID
                                          , String svrPWD
                                          , String cliPath
                                          , String cliFile
                                          , bool showMsg = false
                                          )
        {

            FtpWebRequest ftpRequest;
            try
            {
                // Server 접속 정보
                String hostName = "ftp://" + svrIP + svrFile;

                // FTP 접속
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(hostName));
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials = new NetworkCredential(svrID, svrPWD);

                //  Server File 읽기
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                Stream ftpStream = ftpResponse.GetResponseStream();
                long cl = ftpResponse.ContentLength;

                // Client File 기록 준비 
                FileStream outputStream = new FileStream(cliPath + "\\" + cliFile, FileMode.Create);

                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                // Read Server FIle ---> Write Client File
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                // 정리 
                ftpStream.Close();
                outputStream.Close();
                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                // Download 실패
                // 메시지를 보여주기 원하면 보여준다.
                if (showMsg)
                    MessageBox.Show(ex.Message, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // Download 성공
            // 메시지를 보여주기 원하면 보여준다.
            if (showMsg)
                MessageBox.Show(svrFile + "\n\n" + "Downloaded Successfully", "Download File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        //----------------------------------------------------------------------//
        // Create Directory - 파일을 올리기 위해 폴더를 만들어 준다.
        // 이미 폴더가 존재하여 예외가 발생하면 무시한다.
        //----------------------------------------------------------------------//
        private static bool MakeDir(String svrID, String svrPWD, String svrIP, String path, bool showMsg)
        {

            FtpWebRequest ftpRequest;
            try
            {
                // Server 접속 정보
                String dirName = "ftp://" + svrIP + path;

                // FTP 접속
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(dirName));
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials = new NetworkCredential(svrID, svrPWD);

                // 디렉토리 만들기 
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                // 서버 응답이 느리다 ... 기다린 후 처리 
                //System.Threading.Thread.Sleep(1000);
                try
                {
                    using (var resp = (FtpWebResponse)ftpRequest.GetResponse())
                    {
                        resp.Close();
                    }
                }
                catch (WebException e)
                {
                    // 빌드 메시지 보기 싫어서 ...
                    String s = e.Message;

                    // 이곳의 예외상황은 이미 폴더가 존재하는 경우라고 보고 ... 다음으로 진행 
                    return true;
                }

            }
            catch (WebException e)
            {
                // Make dir 실패 ... 무슨 이유가 있을까???
                MessageBox.Show(e.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 폴더 생성 성공 
            return true;

        }

        //----------------------------------------------------------------------//
        // File Upload
        //----------------------------------------------------------------------//
        public static bool Fn_FileUpLoad(String svrIP
                                        , String svrPath
                                        , String svrFile
                                        , String svrID
                                        , String svrPWD
                                        , String cliPath
                                        , String cliFile
                                        , bool showMsg = false
                                        )
        {

            // 먼저 Upload 할 폴더를 만들어 주자!!!
            // 맨앞과 맨뒤의 '/'는 빼고 한다.
            String[] arrDir = svrPath.Substring(1, svrPath.Length - 1).Split('/');

            String curPath = "";
            for (int i = 0; i < arrDir.Length; i++)
            {
                //if (i < 2)
                //{
                //    curPath += "/" + arrDir[i];
                //    continue;
                //}
                curPath += "/" + arrDir[i];
                bool rv = MakeDir(svrID, svrPWD, svrIP, curPath, true);
                if (rv == false) return false;
            }

            FtpWebRequest ftpRequest;
            try
            {
                // File Upload
                // Server 접속 정보
                //String hostName = "ftp://" + svrIP + svrPath + "/" + svrFile;
                String hostName = "ftp://" + svrIP + svrPath + svrFile;

                // FTP 접속
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(hostName));
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials = new NetworkCredential(svrID, svrPWD);


                // 파일 Upload 
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                // Download 준비
                int length = 4096;
                byte[] buffer = new byte[length];
                int bytesread = 0;

                // 파일 준비 
                Stream ftpStream = ftpRequest.GetRequestStream();
                FileStream file = File.OpenRead(cliPath + cliFile);
                do
                {
                    bytesread = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesread);
                }
                while (bytesread != 0);

                file.Close();
                ftpStream.Close();

            }
            catch (Exception ex)
            {
                // Download 실패
                // 메시지를 보여주기 원하면 보여준다.
                if (showMsg)
                    MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // Download 성공
            // 메시지를 보여주기 원하면 보여준다.
            if (showMsg)
                MessageBox.Show(svrFile + "\n\n" + "Uploaded Successfully", "Upload File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

    }
}
