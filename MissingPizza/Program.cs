using OpenCvSharp;
using System;
using System.IO;

namespace MissingPizza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/convex.PNG");

            Mat img = new Mat(fileName);

            Point[][] contours = GetAllContours(img);

            double factor = 0.01;

            foreach (Point[] contour in contours)
            {
                double epsilon = factor * Cv2.ArcLength(contour, true);
                var contourNew = Cv2.ApproxPolyDP(contour, epsilon, true);

                if (Cv2.IsContourConvex(contourNew))
                {
                    continue;
                }
                Cv2.DrawContours(img, new Point[][] { contour }, 0, new Scalar(0, 0, 0), thickness: 2);
            }
            Cv2.ImShow("img", img);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
        static Point[][] GetAllContours(Mat image)
        {
            Mat refGray = new Mat();
            Cv2.CvtColor(image, refGray, ColorConversionCodes.BGR2GRAY);
            Mat thresh = new Mat();
            Cv2.Threshold(refGray, thresh, 127, 255, ThresholdTypes.Binary);

            Point[][] contours;
            HierarchyIndex[] hIndx;
            Cv2.FindContours(thresh, out contours, out hIndx, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
            return contours;
        }
    }
}
