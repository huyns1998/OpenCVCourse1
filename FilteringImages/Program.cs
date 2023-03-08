using OpenCvSharp;
using System;
using System.IO;

namespace FilteringImages
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/castle.jpg");
            //Mat image = Cv2.ImRead(fileName, ImreadModes.Color);
            //var kernel3x3 = Mat.Ones(new Size(3, 3), MatType.CV_32F) / 9;
            //var kernel5x5 = Mat.Ones(new Size(5, 5), MatType.CV_32F) / 25;
            //Mat result3x3 = new Mat();
            //Mat result5x5 = new Mat();
            //Mat container = new Mat(image.Height, image.Width * 3 + 20 * 2, MatType.CV_8UC3);

            //Cv2.Filter2D(image, result3x3, -1, kernel3x3);
            ////Cv2.Filter2D(image, result5x5, -1, kernel5x5);
            //Cv2.Blur(image, result5x5, new Size(5, 5));
            //container[new Rect(new Point(0, 0), new Size(image.Width, image.Height))] = image;
            //container[new Rect(new Point(image.Width + 20, 0), new Size(image.Width, image.Height))] = result3x3;
            //container[new Rect(new Point(2*image.Width + 40, 0), new Size(image.Width, image.Height))] = result5x5;
            //Cv2.ImShow("Side by side", container);

            //Console.WriteLine("Image {0}", image.Total());    
            //Console.WriteLine("result5x5 {0}", result5x5.Total());    



            Mat image = new Mat(fileName, ImreadModes.Color);
            Mat result1 = new Mat();
            Mat result2 = new Mat();
            Mat grayScale = new Mat();
            Cv2.CvtColor(image, grayScale, ColorConversionCodes.BGR2GRAY);

            //Cv2.Blur(grayScale, result5x5, new Size(5, 5));

            Cv2.GaussianBlur(grayScale, result1, new Size(3, 3), 1.5, 1.5);
            Cv2.GaussianBlur(grayScale, result2, new Size(3, 3), 7, 7);

            Cv2.ImShow("gray image", grayScale);
            Cv2.ImShow("result1", result1);
            Cv2.ImShow("result2", result2);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();    

        }
    }
}
