using OpenCvSharp;
using System;
using System.IO;
using System.Threading.Channels;
using static OpenCvSharp.LineIterator;

namespace ThreshodingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string fileName = Path.Combine(projectDirectory, "Images/bookpage.jpg");

            //Mat img = Cv2.ImRead(fileName, ImreadModes.Color);

            //Mat threshold = new Mat(new Size(img.Width, img.Height), MatType.CV_8UC3, new Scalar(0, 0, 0));
            string fileName = Path.Combine(projectDirectory, "Images/leaf.jpg");

            //Cv2.ImShow("org", img);

            //Mat threshold = new Mat();
            //Mat grayScaled = new Mat();
            //Cv2.CvtColor(img, grayScaled, ColorConversionCodes.BGR2GRAY);


            //Cv2.Threshold(grayScaled, threshold, 15, 255, ThresholdTypes.Binary);
            //Cv2.ImShow("thresh", threshold);
            //Cv2.ImShow("grayScaled", grayScaled);
            //Mat adaptive = new Mat();
            //Cv2.AdaptiveThreshold(grayScaled, adaptive, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 5, 1);
            //Cv2.ImShow("adaptive", adaptive);
            //float grayPixelVal = (float)grayScaled.At<char>(50, 25);
            //Console.WriteLine("grayScaleValue: " + grayPixelVal);

            //Vec3b pixel = img.Get<Vec3b>(50, 25);
            //Vec3b pixel1 = grayScaled.Get<Vec3b>(20, 10);

            //int channels = grayScaled.Channels();
            //int channels1 = img.Channels();
            //Console.WriteLine("channel of grayScale: " + channels);
            //Console.WriteLine("channel of Original: " + channels1);

            //Console.WriteLine("img-B: " + pixel.Item0);
            //Console.WriteLine("img-G: " + pixel.Item1);
            //Console.WriteLine("img-R: " + pixel.Item2);
            //Console.WriteLine("----------------------");
            //Console.WriteLine("img-B: " + pixel1.Item0);
            //Console.WriteLine("img-G: " + pixel1.Item1);
            //Console.WriteLine("img-R: " + pixel1.Item2);

            //Cv2.ImShow("grayScaled", grayScaled);


            Mat leaf = Cv2.ImRead(fileName, ImreadModes.Color);
            Mat grayScaleLeaf = new Mat();
            Cv2.ImShow("leaf", leaf);
            Cv2.CvtColor(leaf, grayScaleLeaf, ColorConversionCodes.BGR2GRAY);
            Cv2.ImShow("gray leaf", grayScaleLeaf);
            Mat otsu = new Mat();

            Cv2.Threshold(grayScaleLeaf, otsu, 0, 255, ThresholdTypes.Otsu);

            Cv2.ImShow("otsu", otsu);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
