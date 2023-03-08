using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdgeDetection
{
    public class CannyTrackbarDemo
    {
        private Mat src;
        int maxBinaryValue = 500;
        int minThreshValue;
        CvTrackbar CvTrackBarHTValue;
        CvTrackbar CvTrackBarGradientType;
        Mat srcGray = new Mat();
        Mat dst = new Mat();
        Window MyWindow;

        public CannyTrackbarDemo(string fileName, int minValue)
        {
            src = Cv2.ImRead(fileName, ImreadModes.Color);
            minThreshValue = minValue;
        }

        public void TrackBar()
        {
            string trackbarGradientType = "Type: \n 0: L1 \n 1: L2";
            Cv2.CvtColor(src, srcGray, ColorConversionCodes.BGR2GRAY);

            MyWindow = new Window("Canny track", WindowMode.AutoSize);

            CvTrackBarGradientType = MyWindow.CreateTrackbar(trackbarGradientType, 0, 1, CannyEdge);
            CvTrackBarHTValue = MyWindow.CreateTrackbar("HighThreshold", 0, 500, CannyEdge);

            CannyEdge(0);

            while (true)
            {
                int c;
                c = Cv2.WaitKey(20);

                if ((char)c == 27)
                {
                    break;
                }
            }
        }

        public void CannyEdge(int x)
        {
            bool L2 = true;
            if(CvTrackBarGradientType.Pos == 0)
            {
                L2 = false; 
            }
            Cv2.Canny(srcGray, dst, minThreshValue, CvTrackBarHTValue.Pos, 3, L2);

            MyWindow.Image = dst;
        }
    }
}
