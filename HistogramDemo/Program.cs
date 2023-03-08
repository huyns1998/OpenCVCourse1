using OpenCvSharp;
using System;
using System.IO;

namespace HistogramDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/lena15.jpg");

            Mat image = Cv2.ImRead(fileName, ImreadModes.GrayScale);
            Mat histogram = ComputeHistogram(image);
            PlotHistogram(histogram);
        }
        static Mat ComputeHistogram(Mat image)
        {
            Mat histogram = new Mat();

            Rangef[] ranges = { new Rangef(0, 256) };
            int[] channels = { 0 };
            int[] histSize = { 256 };

            Cv2.CalcHist(new Mat[] { image }, channels, null, histogram, 1, histSize, ranges);
            return histogram;
        }

        static void PlotHistogram(Mat histogram)
        {
            int plotWidth = 1024, plotHeight = 400;
            int binWidth = (plotWidth / histogram.Rows);
            Mat canvas = new Mat(plotHeight, plotWidth, MatType.CV_8UC3, new Scalar(0, 0, 0));

            Cv2.Normalize(histogram, histogram, 0, plotHeight, NormTypes.MinMax);

            for(int rows = 1; rows <  histogram.Rows; ++rows)
            {
                Cv2.Line(canvas,
                    new Point((binWidth * (rows - 1)), (plotHeight - (float)(histogram.At<float>(rows-1, 0)))),
                    new Point(binWidth * rows, (plotHeight- (float)(histogram.At<float>(rows, 0)))),
                    new Scalar(125, 125, 125), 2);   
            }

            Cv2.ImShow("Histogram", canvas);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
