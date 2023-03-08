using OpenCvSharp;
using System;

namespace BarcodeCamera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture cap = new VideoCapture(0);

            double scalingFactor = 1.0;

            var lower = new Scalar(80, 40, 40);
            var upper = new Scalar(120, 255, 255);

            while (true)
            {
                var frame = GetFrame(cap, scalingFactor);



                //Mat hsvFrame = new Mat();   
                //Cv2.CvtColor(frame, hsvFrame, ColorConversionCodes.BGR2HSV);

                //Mat mask = new Mat();
                //Cv2.InRange(hsvFrame, lower, upper, mask);  
                //Mat res = new Mat();    
                //Cv2.BitwiseAnd(frame, frame, res, mask);
                //Cv2.MedianBlur(res, res, ksize: 5);



                Cv2.ImShow("image", frame);
                //Cv2.ImShow("Color detector", res);

                var c = Cv2.WaitKey(10);
                if (c == 27)
                {
                    break;
                }
            }

            Cv2.DestroyAllWindows();
        }
        private static Mat GetFrame(VideoCapture cap, double scalingFactor)
        {
            Mat frame = new Mat();
            bool ret = cap.Read(frame);

            Cv2.Resize(frame, frame, new Size(), scalingFactor, scalingFactor, InterpolationFlags.Nearest);

            return frame;
        }
    }
}
