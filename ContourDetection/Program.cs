using Microsoft.VisualBasic;
using OpenCvSharp;
using System;
using System.IO;

namespace ContourDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/Capture.PNG");

            Mat image = Cv2.ImRead(fileName);
            Point[][] contours = GetAllContours(image);
            Mat imageClone = image.Clone();
            Cv2.DrawContours(imageClone, contours, -1, new Scalar(0, 0, 0), thickness: 3);
            Cv2.ImShow("image", image);
            Cv2.ImShow("contours", imageClone);
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
