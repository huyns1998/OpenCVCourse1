using OpenCvSharp;
using System;
using System.IO;

namespace LoadDisplaySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/lena.jpg");
            Mat image = Cv2.ImRead(fileName, ImreadModes.GrayScale);
            Cv2.ImShow("Lena", image);

            Cv2.ImWrite(Path.Combine(projectDirectory, "Images/lenaSave.jpg") , image);   

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
