using OpenCvSharp;
using System;
using System.IO;

namespace ShapeMatching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName1 = Path.Combine(projectDirectory, "Images/oval.PNG");
            string fileName2 = Path.Combine(projectDirectory, "Images/shapes.PNG");

            Mat img1 = Cv2.ImRead(fileName1);
            Mat img2 = Cv2.ImRead(fileName2);

            Point[] refContour = GetRefContour(img1);
            Point[][] inputContour = GetAllContours(img2);

            Point[] closestContour = null;

            double minDist = 0.0;
            Mat contourImage = img2.Clone();

            Cv2.ImShow("Contours", img2);
            Cv2.ImShow("Ref", img1);

            foreach (Point[] contour in inputContour)
            {
                double ret = Cv2.MatchShapes(refContour, contour, ShapeMatchModes.I1);

                if (minDist == 0 || ret < minDist)
                {
                    minDist = ret;
                    closestContour = contour;
                }
            }
            Cv2.DrawContours(img2, new Point[][] { closestContour }, 0, new Scalar(0, 0, 0), thickness: 3);
            Cv2.ImShow("Best Matching", img2);
            Cv2.WaitKey();
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
        static Point[] GetRefContour(Mat img)
        {
            Point[][] contours = GetAllContours(img);

            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                var imgArea = img.Width * img.Height;
                var ratio = area / (float)imgArea;
                if (ratio > 0.1 && ratio < 0.8)
                {
                    return contour;
                }
            }
            return null;
        }
    }
}
