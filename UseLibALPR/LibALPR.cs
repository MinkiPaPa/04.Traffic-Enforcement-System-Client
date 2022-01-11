// AlprNet 
using AlprNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
// etc...
using System.Reflection;
//
using System.Windows.Forms;
// VehicleClassifierNet 
using VehicleClassifierNet;
using VehicleClassifierNet.Models;

namespace UseLibALPR
{
    public class LibALPR
    {
        public static bool ProcessImage(String fileName, String region_code
                                       , ref String sPlate
                                       , ref PictureBox picPlate
                                       , ref String sMake
                                       , ref String sColor
                                       , ref String sModel
                                       , ref String sType
                                       , ref String sYear
                                       , ref String sOrientation
                                       )
        {
            bool rv = false;

            // 반환값 초기화
            sPlate = "";
            //imgLincese = null;
            sMake = "";
            sColor = "";
            sModel = "";
            sType = "";
            sYear = "";
            sOrientation = "";


            //resetControls();
            //var region = (string)comboRegion.SelectedItem;
            var region = region_code;

            String config_file = Path.Combine(AssemblyDirectory, "openalpr.conf");
            String runtime_data_dir = Path.Combine(AssemblyDirectory, "runtime_data");

            try
            {
                // 번호판 인식 
                using (var alpr = new Alpr(region, config_file, runtime_data_dir))
                {

                    alpr.Initialize();

                    if (!alpr.IsLoaded())
                    {
                        //lbxPlates.Items.Add("Error initializing OpenALPR");
                        return rv; ;
                    }

                    PictureBox picOriginal = new PictureBox();  // 추가
                    picOriginal.ImageLocation = fileName;
                    picOriginal.Load();

                    var results = alpr.Recognize(fileName);

                    var images = new List<Image>(results.results.Count());
                    //var i = 1;
                    foreach (var result in results.results)
                    {
                        List<Point> points = new List<Point>();
                        foreach (AlprNet.Models.Coordinate c in result.coordinates)
                            points.Add(new Point(c.x, c.y));

                        var rect = BoundingRectangle(points);
                        var img = Image.FromFile(fileName);
                        var cropped = CropImage(img, rect);
                        images.Add(cropped);


                        //lbxPlates.Items.Add("\t\t-- Plate #" + i++ + " --");
                        //foreach (var plate in result.candidates)
                        //{

                        //    lbxPlates.Items.Add(string.Format(@"{0} {1}% {2}",
                        //                                      plate.plate.PadRight(12),
                        //                                      plate.confidence.ToString("N1").PadLeft(8),
                        //                                      plate.matches_template.ToString().PadLeft(8)));
                        //}

                        float fConf = 0.0f;
                        String strPlate = "";
                        foreach (var plate in result.candidates)
                        {
                            if (plate.confidence > fConf)
                            {
                                strPlate = plate.plate;   // 가장 높은것으로...
                                fConf = plate.confidence;
                            }
                        }
                        if (strPlate.Length > 0) sPlate = strPlate;
                    }

                    if (images.Any())
                    {
                        //imgLincese.Image = CombineImages(images);
                        picPlate.Image = CombineImages(images);

                    }
                }

                // 차량인식 
                using (VehicleClassifier vehicleClassifier = new VehicleClassifier("", "", ""))
                {
                    if (!vehicleClassifier.IsLoaded())
                    {
                        Console.WriteLine("Vehicle Classifier was not properly initialized.  Exiting");
                        vehicleClassifier.Dispose();
                        Console.ReadKey();
                        return false;
                    }

                    vehicleClassifier.setTopN(1);

                    VehicleResponse response = vehicleClassifier.Classify(fileName, region, 390, 250, 830, 830);

                    // 처리 
                    //lbxVehicle.Items.Clear();
                    // Make
                    float fConf = 0.0f;
                    String strMake = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.make)
                    {
                        ////Console.WriteLine("  - {0} ({1}%)", candidate.name, candidate.confidence);
                        //lbxVehicle.Items.Add("Make : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strMake = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strMake.Length > 0) sMake = strMake;

                    // Color
                    fConf = 0.0f;
                    String strColor = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.color)
                    {
                        //lbxVehicle.Items.Add("Color : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strColor = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strColor.Length > 0) sColor = strColor;

                    // make_model
                    fConf = 0.0f;
                    String strModel = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.make_model)
                    {
                        //lbxVehicle.Items.Add("Model : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strModel = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strModel.Length > 0) sModel = strModel;

                    // Body_Type
                    fConf = 0.0f;
                    String strType = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.body_type)
                    {
                        //lbxVehicle.Items.Add("Body Type : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strType = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strType.Length > 0) sType = strType;

                    // Year
                    fConf = 0.0f;
                    String strYear = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.year)
                    {
                        //lbxVehicle.Items.Add("Body Type : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strYear = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strYear.Length > 0) sYear = strYear;

                    // Orientation
                    fConf = 0.0f;
                    String strOrientation = "";
                    foreach (VehicleClassifierNet.Models.Candidate candidate in response.orientation)
                    {
                        //lbxVehicle.Items.Add("Body Type : " + candidate.name + " - " + candidate.confidence);
                        if (candidate.confidence > fConf)
                        {
                            strOrientation = candidate.name;   // 가장 높은것으로...
                            fConf = candidate.confidence;
                        }
                    }
                    if (strOrientation.Length > 0) sOrientation = strOrientation;


                    // Free the VehicleClassifier instance's memory.
                    vehicleClassifier.Dispose();

                }

                // 여기까지 무사히 오면 true
                rv = true;
            }
            catch (Exception e)
            {
                String strTmp = e.Message;
                rv = false;
            }
            finally
            {
            }

            return rv;

        }

        // ALPR 사용에 필요한 각종 Directory 
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        // ALPR 
        public static Rectangle BoundingRectangle(List<Point> points)
        {
            // Add checks here, if necessary, to make sure that points is not null,
            // and that it contains at least one (or perhaps two?) elements

            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

        // ALPR 이미지 자르기
        private static Image CropImage(Image img, Rectangle cropArea)
        {
            const int MAX_WIDTH = 200;
            var bmpImage = new Bitmap(img);
            Bitmap bmp = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            if (bmp.Size.Width > MAX_WIDTH)
                return new Bitmap(bmp, new Size(MAX_WIDTH, bmp.Size.Height / (bmp.Size.Width / MAX_WIDTH)));

            return bmp;
        }

        // ALPR
        public static Bitmap CombineImages(List<Image> images)
        {
            //read all images into memory
            Bitmap finalImage = null;

            try
            {
                var width = 0;
                var height = 0;

                foreach (var bmp in images)
                {
                    width += bmp.Width;
                    height = bmp.Height > height ? bmp.Height : height;
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (var g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Black);

                    //go through each image and draw it on the final image
                    var offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image,
                                    new Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (var image in images)
                {
                    image.Dispose();
                }
            }
        }


    }


}
