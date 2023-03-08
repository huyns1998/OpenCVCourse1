using OpenCvSharp;
using System;
using System.IO;

namespace MorphologicalOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/Morphplogical.PNG");

            Mat image = Cv2.ImRead(fileName);

            Mat kernel = Mat.Ones(new Size(3, 3), MatType.CV_8U);
            Mat erosion3x3 = new Mat();
            Cv2.Erode(image, erosion3x3,kernel, iterations:3);

            Cv2.ImShow("original", image);
            Cv2.ImShow("erosion3x3", erosion3x3);

            Mat dilation3x3 = new Mat();    
            Cv2.Dilate(erosion3x3, dilation3x3, kernel, iterations:3);
            Cv2.ImShow("dilation3x3", dilation3x3);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();    
        }
    }
}
