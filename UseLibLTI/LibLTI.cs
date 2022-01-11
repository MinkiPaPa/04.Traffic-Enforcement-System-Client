using System;
// 이미지 처리
using System.Drawing;
// 날짜 형식 
using System.Globalization;
//============================================================================================//
// LTI SDK 사용
//============================================================================================//
//
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// LTI Def
//jmx file errors
enum LTI_ERRORS
{
    ERR_FILE_NOT_FOUND = 1,
    ERR_CANNOT_READ_FILE_ID_HEADER = 2,
    ERR_CANNOT_READ_FILE_HEADER = 3,
    ERR_FILE_ID_HEADER_SIZE_DOES_NOT_MATCH = 4,
    ERR_INCORRECT_FILE_HEADER_TYPE = 5,
    ERR_CRC_CHECKSUM_DOES_NOT_MATCH = 6,
    ERR_CANNOT_READ_CLIP = 7,
    ERR_CANNOT_FIND_FIRST_IMAGE_HEADER = 8,
    ERR_CANNOT_READ_FIRST_IMAGE_HEADER = 9,
    ERR_CANNOT_READ_FIRST_IMAGE = 10,
    ERR_CANNOT_FIND_STILL_IMAGE = 11,
    ERR_CANNOT_READ_STILL_IMAGE = 12,
    ERR_BAD_USER_DATA_FORMAT = 13,
    END_OF_FILE = 14,
    ERR_WRONG_ENCRYPTION = 15,
    ERR_BAD_CLIP_FRAME = 16,
    ERR_BAD_STILL_IMAGE = 17,
    ERR_CANNOT_OPEN_FILE = 18,
    ERR_CANNOT_CREATE_FILE = 19,
    ERR_WRONG_ENDIAN = 20,
    ERR_BAD_FILE_FORMAT = 21,
    ERR_CANNOT_ALLOCATE_MEMORY = 22,
    ERR_CANNOT_READ_FILE = 23,
    ERR_TOO_MANY_FRAMES = 24,
    ERR_ZERO_FRAMES = 25,
    ERR_CORRUPTED_FILE = 26,
    ERR_INVALID_FRAME = 27
}

//crosshair types
enum CROSSHAIR_TYPE
{
    CROSSHAIR_BEAM_SIZE = 0,
    CROSSHAIR_CLASSIC = 1,
    NO_CROSSHAIR = 2
}

//video clip types
enum CLIP_TYPE
{
    DBC = 0x01,
    SPEED = 0x02,
    SURVEY = 0x04,
    VIDEO = 0x08
}

//submode
enum SUBMODE
{
    NULL = 0,
    SPEED = 1,
    DBC = 2,
    REAR_PLATE = 5,
    VIDEO = 6,
    UNMANNED = 7,
    MANNED = 8,
    ROADOFFSET = 9
}

enum SPEED_UNITS
{
    MPH = 0,
    KMH = 1
}

enum DISTANCE_UNITS
{
    FEET = 0,
    METERS = 1
}

enum TC_ENCRYPTION
{
    PASSWORDS_NO = 0,
    PASSWORDS_YES = 1
}

enum DATE_FORMAT
{
    DATE_MM_DD_YYYY = 1,
    DATE_DD_MM_YYYY = 2
}

enum VEHICLE_TYPE
{
    SINGLE_SPEED = 0, //lower limit
    VEHICLE_UNKNOWN = 2, //higher limit
    EVAL_TRUCK = 3, //lower limit
    EVAL_CAR = 4, //higher limit
    MINIMUM_LIMITS = 5 //minimum limit
}


[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct textdataW
{
    //movie clip type
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public String ClipType;

    //movie clip data - clip #, frame rate and number of frames
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)] public String ClipNumber;
    public int iNumberOfFrames;
    public int iMeasurementFrame;

    //speed limits and capture speed settings
    //valid only if Dual Speed Mode is not active
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String SpeedLimit;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String CaptureSpeed;

    //speed and distance units
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String SpeedUnits;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String DistanceUnits;

    //violation data
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredSpeed;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredDistance;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredSpeed2;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredDistance2;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredTBC;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredDBC;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredTBM;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String MeasuredRoadOffset;
    public int iCurrentLane;
    //
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public String OperatorName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)] public String OperatorID;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public String StreetName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public String StreetCode;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)] public String ClipDate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)] public String ClipTime;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public String LastAligned;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public String CalExpires;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)] public String PaidData;

    //GPS and Tilt data
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public String Latitude;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public String Longitude;

    //Misc
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public String FirmwareVersion;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)] public String SerialNo;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)] public String Signature;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public String SystemMode;

    //Movie Clip Additional Data
    public int iCrosshairX;
    public int iCrosshairY;
    public int iImageWidth;
    public int iImageHeight;

    //speed limits and capture speed settings
    //valid only if Dual Speed Mode is active
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String LowerSpeedLimit;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String HigherSpeedLimit;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String LowerCaptureSpeed;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String HigherCaptureSpeed;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)] public String LimitsUsed;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)] public String ClipName;
    public int iVehicleType;
};


[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFOHEADER
{
    [MarshalAs(UnmanagedType.I4)] public Int32 biSize;
    [MarshalAs(UnmanagedType.I4)] public Int32 biWidth;
    [MarshalAs(UnmanagedType.I4)] public Int32 biHeight;
    [MarshalAs(UnmanagedType.I2)] public short biPlanes;
    [MarshalAs(UnmanagedType.I2)] public short biBitCount;
    [MarshalAs(UnmanagedType.I4)] public Int32 biCompression;
    [MarshalAs(UnmanagedType.I4)] public Int32 biSizeImage;
    [MarshalAs(UnmanagedType.I4)] public Int32 biXPelsPerMeter;
    [MarshalAs(UnmanagedType.I4)] public Int32 biYPelsPerMeter;
    [MarshalAs(UnmanagedType.I4)] public Int32 biClrUsed;
    [MarshalAs(UnmanagedType.I4)] public Int32 biClrImportant;
}

[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFO
{
    [MarshalAs(UnmanagedType.Struct, SizeConst = 40)] public BITMAPINFOHEADER bmiHeader;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)] public Int32[] bmiColors;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
//public unsafe struct BITMAPFILEHEADER
public struct BITMAPFILEHEADER
{
    public Int16 bfType;
    public Int32 bfSize;
    public Int16 bfReserved1;
    public Int16 bfReserved2;
    public Int32 bfOffBits;
};

namespace UseLibLTI
{

    public class LibLTI
    {
        //-----
        // LIT DEF
        //-----
        const int RGBQUAD_SIZE = 4;
        const int MAX_STILL_IMAGE_WIDTH = 1280;
        const int MAX_STILL_IMAGE_HEIGHT = 960;

        static System.Windows.Forms.PictureBox pbxImage = null;


        //----------------------------------------------------------------------//
        // Lti TextData to String[]
        //----------------------------------------------------------------------//
        public static int ExtractImage(String strSrc, String strDest)
        {

            int CrosshairType;
            int rv = 0;
            String ClipName = strSrc;

            pbxImage = new System.Windows.Forms.PictureBox();
            //pbxImage.Dispose();


            //-----
            // LTI Image 읽기
            //-----
            byte[] imgbuf = new byte[MAX_STILL_IMAGE_WIDTH * MAX_STILL_IMAGE_HEIGHT * 3];
            BITMAPINFO bmiImage = new BITMAPINFO();

            String FrameTimeStamp = new String(' ', 12);
            String FileName = new String(' ', 260);

            //CrosshairType = (int)CROSSHAIR_TYPE.CROSSHAIR_BEAM_SIZE;
            CrosshairType = (int)CROSSHAIR_TYPE.NO_CROSSHAIR;

            pbxImage.Image = null;
            pbxImage.Image = null;

            //get still image with crosshair
            rv = Lti.GetMeasurementFrameWithCrosshairNet(ClipName, CrosshairType, ref bmiImage, imgbuf, FrameTimeStamp);
            if (rv == 0)
            {
                //SaveBmpImage("MeasurementFrame.bmp", bmiImage, imgbuf);
                SaveImage(bmiImage, imgbuf, strDest);
                return 0;
            }
            else
            {
                Console.WriteLine("Could not extract frame");
                return 0;
            }

        }

        //----------------------------------------------------------------------//
        // Lti TextData to String[]
        //----------------------------------------------------------------------//
        private static void SaveImage(BITMAPINFO bmiImage, byte[] imgbuf, String strDest)
        {
            // 파일 저장을 위하여 BMP Image Header 생성
            BITMAPFILEHEADER hdr = new BITMAPFILEHEADER();

            hdr.bfType = 0x4d42;        // 0x42 = "B" 0x4d = "M"
            hdr.bfSize = (Int32)Marshal.SizeOf(hdr) +
                       bmiImage.bmiHeader.biSize + bmiImage.bmiHeader.biClrUsed * RGBQUAD_SIZE + bmiImage.bmiHeader.biSizeImage;
            hdr.bfReserved1 = 0;
            hdr.bfReserved2 = 0;
            hdr.bfOffBits = (Int32)Marshal.SizeOf(hdr) + bmiImage.bmiHeader.biSize + bmiImage.bmiHeader.biClrUsed * RGBQUAD_SIZE;

            // BMP 파일 생성 
            FileStream fs = File.Create("tmpImage.bmp");

            byte[] buffer = new byte[1280 * 960 * 3];
            GCHandle h = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            // copy the file header into int byte[] mem alloc
            Marshal.StructureToPtr(hdr, h.AddrOfPinnedObject(), false);
            fs.Write(buffer, 0, Marshal.SizeOf(hdr));
            // copy the image header into int byte[] mem alloc
            Marshal.StructureToPtr(bmiImage, h.AddrOfPinnedObject(), true);
            fs.Write(buffer, 0, Marshal.SizeOf(bmiImage.bmiHeader) + bmiImage.bmiHeader.biClrUsed * 256);

            fs.Write(imgbuf, 0, bmiImage.bmiHeader.biSizeImage);

            // BMP 파일 읽기 
            Bitmap bmp = new Bitmap(fs);

            pbxImage.Image = bmp;

            // Jpeg 파일로 저장 
            pbxImage.Image.Save(strDest, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            h.Free();
            fs.Dispose();

            // temp file 삭제 
            FileInfo fi = new FileInfo("tmpImage.bmp");
            fi.Delete();


        }


        //----------------------------------------------------------------------//
        // Lti TextData to String[]
        //----------------------------------------------------------------------//
        public static String[] ConvertToStrArr(textdataW userdata)
        {

            try
            {
                //movie clip data - clip #, frame rate and number of frames
                String dbcSpeed2 = "";
                String dbcDistance2 = "";
                String dbcTBC = "";
                String dbcDBC = "";
                String dbcTBM = "";
                String dbcRoadOffset = "";

                if (userdata.ClipType == "DBC")
                {
                    dbcSpeed2 = userdata.MeasuredSpeed2; //string.Format("Measured Speed2 = {0}\n", );
                    dbcDistance2 = userdata.MeasuredDistance2; //string.Format("Measured Distance2 = {0}\n", );
                    dbcTBC = userdata.MeasuredTBC; //string.Format("Measured TBC = {0}\n", ); 
                    dbcDBC = userdata.MeasuredDBC; //string.Format("Measured DBC = {0}\n", );
                    dbcDBC = userdata.MeasuredTBM; //string.Format("Measured TBM = {0}\n", );
                    dbcRoadOffset = userdata.MeasuredRoadOffset; //string.Format("Measured Road Offset = {0}\n", );
                }

                //
                String strClipDt = DateTime.ParseExact(userdata.ClipDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString();
                String strLastAlignedDt = DateTime.ParseExact(userdata.LastAligned.Substring(0, 10), "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString();

                // 2019.11.28 역방향 촬영된속도 위반 절대값으로 변경하여 계산
                String strRealSpeed = userdata.MeasuredSpeed;
                int iRealSpeed = Math.Abs(int.Parse(strRealSpeed));

                String[] rvStr = {
                            // [Movie Clip]
                              userdata.ClipName //string.Format("File = {0}\n", )
                            , userdata.ClipType //string.Format("Clip Type = {0}\n", )
                            , userdata.ClipNumber //string.Format("Clip Number = {0}\n", )
                            , userdata.SystemMode //string.Format("System Mode = {0}\n", )
                            , string.Format("{0}", userdata.iNumberOfFrames) //string.Format("Number Of Frames = {0}\n", )
                            , string.Format("{0}", userdata.iMeasurementFrame) //string.Format("Measurement Frame = {0}\n", )


                            // [Violation]
                            , string.Format("{0}", userdata.iVehicleType) //string.Format("Vehicle Type = {0}\n", )
//                            , userdata.MeasuredSpeed // string.Format("Measured Speed = {0}\n", )
                            , iRealSpeed.ToString()     // 절대값으로
                            , userdata.SpeedLimit //string.Format("Speed Limit = {0}\n", )
                            , userdata.CaptureSpeed //string.Format("Capture Speed = {0}\n", )
                            , userdata.MeasuredDistance //string.Format("Measured Distance = {0}\n", )


                            // [Speed and Distance Units]
                            , userdata.SpeedUnits // string.Format("Speed Units = {0}\n", )
                            , userdata.DistanceUnits //string.Format("Distance Units = {0}\n", )
                            , string.Format("{0}", userdata.iCurrentLane) //string.Format("Lane = {0}\n", )

                            // DBC
                            , dbcSpeed2
                            , dbcDistance2
                            , dbcTBC
                            , dbcDBC
                            , dbcTBM
                            , dbcRoadOffset

                            //
                            // [Operator, Stress ...]
                            , userdata.OperatorName //string.Format("Operator Name = {0}\n", )
                            , userdata.OperatorID //string.Format("Operator ID = {0}\n", )
                            , userdata.StreetName //string.Format("Street Name = {0}\n", )
                            , userdata.StreetCode //string.Format("Street Code = {0}\n", )

                            ////, userdata.ClipDate //string.Format("Clip Date = {0}\n", )
                            //, String.Format("{0:u}", DateTime.ParseExact(userdata.ClipDate, "mm/dd/yyyy", null))
                            , strClipDt.Substring(0, 10)

                            , userdata.ClipTime //string.Format("Clip Time Code = {0}\n", )
                            //, userdata.LastAligned //string.Format("Last Aligned = {0}\n", )
                            , strLastAlignedDt

                            , userdata.Latitude //string.Format("Latitude = {0}\n", )
                            , userdata.Longitude //string.Format("Longitude = {0}\n", )

                            // [Misc]
                            , userdata.FirmwareVersion //string.Format("Firmware Version = {0}\n", )
                            , userdata.SerialNo //string.Format("Serial No = {0}\n", )

                            , userdata.Signature //string.Format("Signature = {0}\n", )

                            // [Movie Clip Additional Data]
                            , string.Format("({0}, {1})", userdata.iCrosshairX, userdata.iCrosshairY) //string.Format("Crosshair Position: (X, Y) = ({0}, {1})\n", )
                            , string.Format("({0}, {1})", userdata.iImageWidth, userdata.iImageHeight) //string.Format("Frame Size: (Width, Height) = ({0}, {1})\n", )

                            // ALPR
                            , ""
                            , ""
                            , ""
                            , ""
                            , ""
                            , ""
                            , ""
                            };
                return rvStr;
            }
            catch (Exception e)
            {
                //
                String tmp = e.Message;
                return null;
            }

        }
        //----------------------------------------------------------------------//
        // LTI Data 분석
        //----------------------------------------------------------------------//
        public static int AnalysisLtiData(String[] arrFiles
                                         , ref ListView lvLtiData
                                         )
        {

            //allocate memory for jmx variables
            //textdataW ClipData = new textdataW();
            //ListView lvData = new ListView();
            try
            {
                for (int i = 0; i < arrFiles.Count(); i++)
                {
                    int rv = 0;
                    var ClipData = new textdataW();

                    //get text data
                    rv = Lti.GetTextDataW(arrFiles[i].ToString(), ref ClipData);
                    if (rv == 0)
                    {
                        //SaveTextData("UserData.txt", ClipData);
                        //ShowText(ClipData);
                        String[] strClip = ConvertToStrArr(ClipData);   // textdataW ---> String []
                        ListViewItem lvi = new ListViewItem(strClip);   // String [] ---> ListViewItem

                        lvLtiData.Items.Add(lvi);

                    }
                    else if (rv == (int)LTI_ERRORS.ERR_WRONG_ENCRYPTION)
                    {
                        //Console.WriteLine("Wrong Encryption");
                        //return 0;
                        continue;
                    }
                    else
                    {
                        //Console.WriteLine("Something wrong");
                        //return 0;
                        continue;
                    }

                } // End for

                return lvLtiData.Items.Count;


            }
            catch (Exception e)
            {
                //
                String tmp = e.Message;
            }
            finally
            {

            }

            return 0;

        }


        //--------------------------------------------------------------------------//
        // LTI Def
        //--------------------------------------------------------------------------//
        public class Win32Api
        {
            [DllImport("user.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr GetDC(IntPtr hWnd);
            [DllImport("user.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr GetWindowDC(IntPtr hWND);
            [DllImport("gdi.dll")]
            public static extern int GetDIBits(IntPtr hdc, IntPtr hbmp, uint uStartScan,
                        uint cScanLines, byte[] lpvBits, ref BITMAPINFO lpbmi, uint uUsage);
        };

        //public unsafe class Lti
        public class Lti
        {
            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int GetTextDataW(String FileName, ref textdataW userdata);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int GetTextDataW2(String FileName, ref textdataW userdata, int dataformat);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int GetMeasurementFrameWithCrosshairNet(String FileName, int CrosshairType, ref BITMAPINFO bmiImage, byte[] imgbuf, String FrameTimeStamp);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int GetMeasurementFrameWithCrosshairAndTextNet(String FileName, int CrosshairType, ref BITMAPINFO bmiImage, byte[] imgbuf, String FrameTimeStamp);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int GetClipFrameNet(String FileName, int FrameCounter, int CrosshairType, ref BITMAPINFO bmiImage, byte[] imgbuf, String FrameTimeStamp);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int ConvertNamesToTrucamFormatNet(String PCFileName, String TrucamFileName, int Encryption);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int ConvertNamesFromTrucamFormatNet(String TrucamFileName, String PCFileName, int Encryption);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int ConvertLocationsToTrucamFormatNet(String PCFileName, String TrucamFileName);

            [DllImport("ltijmx64.dll", CharSet = CharSet.Unicode)]
            public static extern int ConvertLocationsFromTrucamFormatNet(String TrucamFileName, String PCFileName);
        };

    }
}

