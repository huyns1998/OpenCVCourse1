using OpenCvSharp;
using System;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.Common;
using OpenCvSharp.Extensions;
using System.Drawing;

namespace BarcodeDotNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string fileName = Path.Combine(projectDirectory, "Images/barcode.jpg");
            //String fileName = Path.Combine(projectDirectory, "Images/barcode4.jpg");
            String fileName = Path.Combine(projectDirectory, "Images/barcode5.PNG");

            Console.WriteLine($"\nProcessing {fileName}");
            bool debug = true;

            Mat image = new Mat(fileName);
            //Cv2.Resize(image, image, new OpenCvSharp.Size(), 0.5, 0.5);
            if (debug)
            {
                Cv2.ImShow("Source", image);
                Cv2.WaitKey(1);
            }

            Mat gray = new Mat();
            int channels = image.Channels();
            if (channels > 1)
            {
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGRA2GRAY);
            }
            else
            {
                image.CopyTo(gray);
            }

            Mat gradX = new Mat();
            Cv2.Sobel(gray, gradX, MatType.CV_32F, xorder: 1, yorder: 0, ksize: -1);
            Mat gradY = new Mat();
            Cv2.Sobel(gray, gradY, MatType.CV_32F, xorder: 0, yorder: 1, ksize: -1);

            Mat gradient = new Mat();
            Cv2.Subtract(gradX, gradY, gradient);
            Cv2.ConvertScaleAbs(gradient, gradient);

            if (debug)
            {
                Cv2.ImShow("Gradient", gradient);
                Cv2.WaitKey(1);
            }

            var blurred = new Mat();
            Cv2.Blur(gradient, blurred, new OpenCvSharp.Size(9, 9));
            double thresh = 150;
            Mat threshImage = new Mat();
            Cv2.Threshold(blurred, threshImage, thresh, 255, ThresholdTypes.Binary);
            if (debug)
            {
                Cv2.ImShow("threshImage", threshImage);
                Cv2.WaitKey(1);
            }

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(21, 7));
            Mat closed = new Mat();
            Cv2.MorphologyEx(threshImage, closed, MorphTypes.Close, kernel);

            if (debug)
            {
                Cv2.ImShow("Closed", closed);
                Cv2.WaitKey(1);
            }

            Cv2.Erode(closed, closed, null, iterations: 4);
            Cv2.Dilate(closed, closed, null, iterations: 4);

            if (debug)
            {
                Cv2.ImShow("Erode and Dilate", closed);
                Cv2.WaitKey(1);
            }

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchyIndexes;
            Cv2.FindContours(closed, out contours, out hierarchyIndexes, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

            if (contours.Length == 0)
            {
                Console.WriteLine("Couldn't find any object in the image");
                return;
            }

            contours = contours.OrderByDescending(x => Cv2.ContourArea(x)).ToArray();

            Rect lcr = Cv2.BoundingRect(contours[0]);

            Mat barcode = new Mat(image, lcr);


            Cv2.CvtColor(barcode, barcode, ColorConversionCodes.BGRA2GRAY);

            //Cv2.ImShow("Baecode", barcode);
            Cv2.WaitKey(1);

            Mat barcodeClone = barcode.Clone();
            //Cv2.Resize(barcodeClone, barcodeClone, new OpenCvSharp.Size(223, 185));
            Mat barcodeFinal = GetBarcodeContainer(barcodeClone);
            Cv2.ImShow("barcodeFinal", barcodeFinal);
            string result = DecodeBarcode(barcodeFinal.ToBitmap());



            //Cv2.CvtColor(barcode, barcode, ColorConversionCodes.BGRA2GRAY);


            //Mat barcodeContainer = GetBarcodeContainer(barcode);

            //Mat barcodeFinal = new Mat();

            ////Cv2.Blur(barcodeContainer, barcodeContainer, new OpenCvSharp.Size(9, 9));

            //Cv2.Threshold(barcodeContainer, barcodeFinal, 127, 255, ThresholdTypes.Binary);

            //Cv2.ImShow("barcodeContainer", barcodeContainer);
            //string result = DecodeBarcode(barcodeContainer.ToBitmap());
            ////string result =  barcodeReader.Decode(barcodeBitmap);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();




        }


        private static Mat GetBarcodeContainer(Mat barcode)
        {
            Mat barcodeContainer = new Mat(new OpenCvSharp.Size(barcode.Width + 30, barcode.Height + 30), MatType.CV_8U, Scalar.White);

            Rect barcodeRect = new Rect(new OpenCvSharp.Point(15, 15), new OpenCvSharp.Size(barcode.Width, barcode.Height));
            Mat roi = barcodeContainer[barcodeRect];
             
            barcode.CopyTo(roi);
            
            //Cv2.WaitKey(0);
            return barcodeContainer;
        }

        private static string DecodeBarcode(System.Drawing.Bitmap barcodeBitmap)
        {
            BitmapLuminanceSource source = new BitmapLuminanceSource(barcodeBitmap);

            var reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
            {
                AutoRotate = true,  
                Options = new DecodingOptions
                {
                    TryInverted = true,
                    TryHarder = true
                }
            };

            var result = reader.Decode(source);

            if (result == null)
            {
                Console.WriteLine("Decode failed");
                return string.Empty;
            }

            Console.WriteLine("Barcode format {0}", result.BarcodeFormat);
            Console.WriteLine("Result {0}", result.Text);

            var writer = new BarcodeWriter
            {
                Format = result.BarcodeFormat,
                Options = {Width = 200, Height = 50, Margin = 4},
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };
            var barcodeImage = writer.Write(result.Text);
            Cv2.ImShow("Barcode writer", barcodeImage.ToMat());

            return result.Text;
        }
    }
}
