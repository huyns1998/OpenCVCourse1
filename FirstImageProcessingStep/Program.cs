using OpenCvSharp;
using System;
using System.IO;

namespace FirstImageProcessingStep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //string fileName = Path.Combine(projectDirectory, "Images/lena.jpg");
            //Mat image = Cv2.ImRead(fileName);
            ////Translation Matrix
            ////[1, 0, tx]
            ////[0, 1, ty]
            //float[] data = { 1, 0, 50, 0, 1, 50 };
            //Mat M = new Mat(2, 3, MatType.CV_32FC1, data);

            //Mat dest = new Mat();

            //Cv2.WarpAffine(image, dest, M, new Size(image.Width + 60, image.Height + 60));

            //Cv2.ImShow("dest", dest);



            //Point2f center = new Point2f(image.Width / 2, image.Height / 2);
            //double angle = -75;
            //Mat RM = Cv2.GetRotationMatrix2D(center, 0, 0.5);

            //Mat dest = new Mat();

            //Cv2.WarpAffine(image, dest, RM, new Size(image.Width, image.Height));

            //Cv2.ImShow("image", image);

            //Mat dest = new Mat();

            //Cv2.Resize(image, dest, new Size(image.Width/2, image.Height/2));    

            //Cv2.ImShow("dest", dest);

            //Mat dest1 = new Mat();

            //Cv2.Resize(image, dest1, new Size(0, 0), 0.75, 0.75);

            //Cv2.ImShow("dest1", dest1);


            //Mat dst1 = new Mat();
            //Mat dst2 = new Mat();
            //Mat dst3 = new Mat();
            //Cv2.ImShow("image", image);
            //Cv2.Flip(image, dst1, FlipMode.X);
            //Cv2.Flip(image, dst2, FlipMode.Y);
            //Cv2.Flip(image, dst3, FlipMode.XY);


            //Cv2.ImShow("dst1", dst1);
            //Cv2.ImShow("dst2", dst2);
            //Cv2.ImShow("dst3", dst3);


            Mat image1 = Mat.Zeros(new Size(400, 200), MatType.CV_8UC1);
            Mat image2 = Mat.Zeros(new Size(400, 200), MatType.CV_8UC1);

            Cv2.Rectangle(image1, new Rect(new Point(0, 0), new Size(image1.Cols / 2, image1.Rows)), new Scalar(255, 255, 255), -1);
            Cv2.ImShow("image1", image1);

            Cv2.Rectangle(image2, new Rect(new Point(100, 50), new Size(200, 100)), new Scalar(255, 255, 255), -1);
            Cv2.ImShow("image2", image2);

            Mat andOp = new Mat();
            Cv2.BitwiseAnd(image1, image2, andOp);
            Cv2.ImShow("andOp", andOp);

            Mat orOp = new Mat();   
            Cv2.BitwiseOr(image1, image2, orOp);
            Cv2.ImShow("orOp", orOp);

            Mat xorOp = new Mat();
            Cv2.BitwiseXor(image1, image2, xorOp);
            Cv2.ImShow("xorOp", xorOp);

            Mat notOp = new Mat();
            Cv2.BitwiseNot(image1, notOp);
            Cv2.ImShow("notOp", notOp);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
