using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using ZXing;
using ZXing.Common;

namespace Camera1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture cap = new VideoCapture(0);

            double scalingFactor = 1.0;

            var lower = new Scalar(80, 40, 40);
            var upper = new Scalar(120, 255, 255);
            string result = null;
            while (string.IsNullOrEmpty(result))
            {
                Mat image = GetFrame(cap, scalingFactor);
                Cv2.ImShow("image", image);

                Mat gray = new Mat();
                int channels = image.Channels();

                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                Mat gradX = new Mat();
                Cv2.Sobel(gray, gradX, MatType.CV_32F, xorder: 1, yorder: 0, ksize: -1);
                Mat gradY = new Mat();
                Cv2.Sobel(gray, gradY, MatType.CV_32F, xorder: 0, yorder: 1, ksize: -1);

                Mat gradient = new Mat();
                Cv2.Subtract(gradX, gradY, gradient);
                Cv2.ConvertScaleAbs(gradient, gradient);

                var blurred = new Mat();
                Cv2.Blur(gradient, blurred, new OpenCvSharp.Size(5, 5));
                double thresh = 100;
                Mat threshImage = new Mat();
                Cv2.Threshold(blurred, threshImage, thresh, 255, ThresholdTypes.Binary);

                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(21, 7));
                Mat closed = new Mat();
                Cv2.MorphologyEx(threshImage, closed, MorphTypes.Close, kernel);

                Cv2.Erode(closed, closed, null, iterations: 4);
                Cv2.Dilate(closed, closed, null, iterations: 4);
                Cv2.ImShow("closed", closed);
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchyIndexes;
                Cv2.FindContours(closed, out contours, out hierarchyIndexes, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

                contours = contours.OrderByDescending(x => Cv2.ContourArea(x)).ToArray();

                if (contours.Length >= 1)
                {
                    Rect lcr = Cv2.BoundingRect(contours[0]);

                    Mat barcode = new Mat(image, lcr);
                    Cv2.CvtColor(barcode, barcode, ColorConversionCodes.BGR2GRAY);

                    Mat barcodeClone = barcode.Clone();
                    Mat barcodeFinal = GetBarcodeContainer(barcodeClone);
                    Cv2.ImShow("barcodeFinal", barcodeFinal);
                    result = DecodeBarcode(barcodeFinal.ToBitmap());

                }
                var c = Cv2.WaitKey(1000);
                if (c == 27)
                {
                    break;
                }
            }

            Cv2.DestroyAllWindows();
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
                Options = { Width = 200, Height = 50, Margin = 4 },
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };
            var barcodeImage = writer.Write(result.Text);
            Cv2.ImShow("Barcode writer", barcodeImage.ToMat());

            return result.Text;
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
        private static Mat GetFrame(VideoCapture cap, double scalingFactor)
        {
            Mat frame = new Mat();
            bool ret = cap.Read(frame);

            Cv2.Resize(frame, frame, new Size(), scalingFactor, scalingFactor, InterpolationFlags.Nearest);

            return frame;
        }

    }
}
