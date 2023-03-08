using OpenCvSharp;
using System;

namespace DrawingShape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat canvas = new Mat(600, 600, MatType.CV_8UC3, new Scalar(0, 0, 0));
            

            Scalar red = new Scalar(0, 0, 255);
            Scalar blue = new Scalar(255, 0, 0);
            Scalar green = new Scalar(0, 255, 0);
            Scalar white = new Scalar(255, 255, 255);

            Cv2.Line(canvas, new Point(0, 0), new Point(200, 200), red, 2);
            Cv2.Circle(canvas, new Point(250, 250), 40, white, 3);
            Cv2.Rectangle(canvas, new Rect(new Point(100, 100), new Size(100, 100)), green, 2);
            Cv2.ImShow("canvas", canvas);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
