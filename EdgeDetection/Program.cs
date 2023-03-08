using OpenCvSharp;
using System;
using System.IO;

namespace EdgeDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int kSize = 5;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/road.jpg");

            Mat image = Cv2.ImRead(fileName, ImreadModes.Color);
            Cv2.CvtColor(image, image, ColorConversionCodes.BGR2GRAY);
            //Cv2.Resize(image, image, new Size(), 0.5, 0.5, InterpolationFlags.Area);


            //Mat sobelX = new Mat(image.Rows, image.Cols, MatType.CV_8U);
            //Mat sobelY = new Mat(image.Rows, image.Cols, MatType.CV_8U);


            Mat sobelX64 = new Mat(image.Rows, image.Cols, MatType.CV_64F);
            Mat sobelY64 = new Mat(image.Rows, image.Cols, MatType.CV_64F);
            Mat sobelXY64 = new Mat();

            Cv2.Sobel(image, sobelX64, MatType.CV_64F, 1, 0, kSize);
            Cv2.Sobel(image, sobelY64, MatType.CV_64F, 0, 1, kSize);

            Cv2.ConvertScaleAbs(sobelX64, sobelX64);
            Cv2.ConvertScaleAbs(sobelY64, sobelY64);

            Cv2.Add(sobelX64, sobelY64, sobelXY64);

            Cv2.ImShow("sobelXY64", sobelXY64);

            //Cv2.Sobel(image, sobelX, MatType.CV_8U, 1, 0, kSize);
            //Cv2.Sobel(image, sobelY, MatType.CV_8U, 0, 1, kSize);

            //Cv2.Add(sobelX, sobelY, sobelXY);

            //Cv2.ImShow("image", image);
            //Cv2.ImShow("sobelX", sobelX);
            //Cv2.ImShow("sobelY", sobelY);
            //Cv2.ImShow("sobelXY", sobelXY);


            //Cv2.CvtColor(image, image, ColorConversionCodes.BGR2GRAY);
            //Mat edgesL2 = new Mat();
            //Mat edgesL1 = new Mat();

            //Cv2.Canny(image, edgesL2, 100, 350, 3, true);
            //Cv2.Canny(image, edgesL1, 100, 350, 3, false);

            //Cv2.ImShow("image", image);
            //Cv2.ImShow("edgesL1", edgesL1);
            //Cv2.ImShow("edgesL2", edgesL2);



            //CannyTrackbarDemo t = new CannyTrackbarDemo(fileName, 100);
            //t.TrackBar();


            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
