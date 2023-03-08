using OpenCvSharp;
using System;
using System.IO;

namespace MatObjectTutorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Mat mat = new Mat(30, 40, MatType.CV_8UC3, new Scalar(255, 255 ,0));
            //Mat mat1 = new Mat(new Size(30, 40), MatType.CV_8UC3, new Scalar(0, 0, 255));
            //Cv2.ImShow("m", mat);
            //Cv2.ImShow("m1", mat1);

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string fileName = Path.Combine(projectDirectory, "Images/lena.jpg");

            Mat colorImage = Cv2.ImRead(fileName);

            Cv2.ImShow("Original", colorImage);

            //Mat cloneImage = colorImage.Clone();

            //Cv2.ImShow("Clone image", cloneImage);

            //Mat[] channels;

            //Cv2.Split(colorImage, out channels);

            //Cv2.ImShow("Blue", channels[0]);
            //Cv2.ImShow("Green", channels[1]);
            //Cv2.ImShow("Red", channels[2]);

            //Mat roiImage = new Mat(colorImage, new Rect(0, 0, 500, 500));
            //Cv2.ImShow("ROI", roiImage);

            int numRows = colorImage.Rows;
            int numCols = colorImage.Cols;

            Mat cImage = colorImage.Clone();

            //Mat.Indexer<Vec3b> indexer = cImage.GetGenericIndexer<Vec3b>();

            //for (int y = 0; y < numRows; y++)
            //{
            //    for (int x = 0; x < numCols; x++)
            //    {
            //        Vec3b pixel = cImage.Get<Vec3b>(y, x);
            //        byte blue = pixel.Item0;
            //        byte green = pixel.Item1;   
            //        byte red = pixel.Item2; 

            //        byte temp = blue;
            //        pixel.Item1 = temp;
            //        pixel.Item0 = red;

            //        cImage.Set<Vec3b> (y, x, pixel);    
            //    }
            //}

            //for (int y = 0; y < numRows; y++)
            //{
            //    for (int x = 0; x < numCols; x++)
            //    {
            //        Vec3b pixel = indexer[y, x];
            //        byte blue = pixel.Item0;
            //        byte green = pixel.Item1;
            //        byte red = pixel.Item2;

            //        byte temp = blue;
            //        pixel.Item0 = red;
            //        pixel.Item2 = temp;

            //        indexer[y, x ] = pixel; 
            //    }
            //}

            MatOfByte3 mat3 = new MatOfByte3(cImage);
            var indexer = mat3.GetIndexer();

            for (int y = 0; y < numRows; y++)
            {
                for (int x = 100; x < numCols-100; x++)
                {
                    Vec3b pixel = indexer[y, x];
                    byte blue = pixel.Item0;
                    byte green = pixel.Item1;
                    byte red = pixel.Item2;

                    byte temp = blue;
                    pixel.Item0 = red;
                    pixel.Item2 = temp;

                    indexer[y, x] = pixel;
                }
            }

            Cv2.ImShow("swapped", cImage);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
