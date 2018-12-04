namespace USH.Droid.Binding
{
    using global::Android.Graphics;
    using global::Android.Runtime;
    using OpenCV.Core;
    using OpenCV.ImgProc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class OpenCVWrapper : IDisposable
    {
        private bool disposedValue = false;
        private Bitmap originalImage;
        private Mat originalMat = new Mat();

        private MatOfPoint2f detectedBorder;
        double oldPerimeter = 0;

        public OpenCV.Core.Point[] DetectBorders(Bitmap image)
        {
            originalImage = image;

            var contours = ProcessImage();

            var detectedBorders = FindBiggestContourWithFourBorders(contours);

            return detectedBorders;
        }

        public Bitmap FixPerspective()
        {
            double maxWidth = 0;
            double maxHeight = 0;
            OpenCV.Core.Point pTopLeft, pBottomLeft, pBottomRight, pTopRight;
            GetImageMaxSize(out maxWidth, out maxHeight, out pTopLeft, out pBottomLeft, out pBottomRight, out pTopRight);

            if (maxWidth <= 0 || maxHeight <= 0)
                return null;

            CreateSrcDstArrays(maxWidth, maxHeight, pTopLeft, pBottomLeft, pBottomRight, pTopRight, out MatOfPoint2f src, out MatOfPoint2f dst);

            Mat undistortedMat = new Mat(new OpenCV.Core.Size(maxWidth, maxHeight), OpenCV.Core.CvType.Cv8uc4);
            Mat ptMat = Imgproc.GetPerspectiveTransform(src, dst);
            Imgproc.WarpPerspective(originalMat, undistortedMat, ptMat, new OpenCV.Core.Size(maxWidth, maxHeight));
            Bitmap undistortedBitmap = Bitmap.CreateBitmap((int)maxWidth, (int)maxHeight, Bitmap.Config.Argb8888);
            OpenCV.Android.Utils.MatToBitmap(undistortedMat, undistortedBitmap);

            return undistortedBitmap;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                originalImage.Dispose();
                originalMat.Dispose();
                if (detectedBorder != null)
                    detectedBorder.Dispose();
                oldPerimeter = 0;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }

        private void CreateSrcDstArrays(double maxWidth, double maxHeight, OpenCV.Core.Point pTopLeft, OpenCV.Core.Point pBottomLeft, OpenCV.Core.Point pBottomRight, OpenCV.Core.Point pTopRight, out MatOfPoint2f src, out MatOfPoint2f dst)
        {
            src = new MatOfPoint2f(new OpenCV.Core.Point[4] { pBottomLeft, pTopLeft, pTopRight, pBottomRight });
            dst = new MatOfPoint2f(new OpenCV.Core.Point[4]
            {
                new OpenCV.Core.Point(0,0),
                new OpenCV.Core.Point(maxWidth - 1, 0),
                new OpenCV.Core.Point(maxWidth - 1, maxHeight - 1),
                new OpenCV.Core.Point(0, maxHeight - 1)
            });
        }

        private void GetImageMaxSize(out double maxWidth, out double maxHeight, out OpenCV.Core.Point pTopLeft, out OpenCV.Core.Point pBottomLeft, out OpenCV.Core.Point pBottomRight, out OpenCV.Core.Point pTopRight)
        {
            var pointArray = detectedBorder.ToArray();
            pTopLeft = new OpenCV.Core.Point(pointArray[0].X, pointArray[0].Y);
            pBottomLeft = new OpenCV.Core.Point(pointArray[1].X, pointArray[1].Y);
            pBottomRight = new OpenCV.Core.Point(pointArray[2].X, pointArray[2].Y);
            pTopRight = new OpenCV.Core.Point(pointArray[3].X, pointArray[3].Y);
            var w1 = pTopLeft.X - pBottomRight.X;
            var w2 = pTopRight.X - pBottomLeft.X;
            var h1 = pTopRight.Y - pTopLeft.Y;
            var h2 = pBottomLeft.Y - pBottomRight.Y;

            maxWidth = w1 > w2 ? w1 : w2;
            maxHeight = h1 > h2 ? h1 : h2;
        }

        private OpenCV.Core.Point[] FindBiggestContourWithFourBorders(IList<MatOfPoint> contours)
        {
            OpenCV.Core.Point[] biggestContour = null;
            Parallel.ForEach(contours, (c) =>
            {
                MatOfPoint2f mat2f = new MatOfPoint2f(c.ToArray());
                double perimeter = Imgproc.ArcLength(mat2f, true);
                if (perimeter > 1500)
                {
                    MatOfPoint2f approx = new MatOfPoint2f();
                    Imgproc.ApproxPolyDP(mat2f, approx, 0.02 * perimeter, true);
                    if (approx.Total() == 4)
                    {
                        MatOfPoint approxMat = new MatOfPoint(approx.ToArray());
                        if (Imgproc.IsContourConvex(approxMat))
                        {
                            if (biggestContour == null)
                            {
                                biggestContour = approx.ToArray();
                                detectedBorder = approx;
                                oldPerimeter = perimeter;
                            }
                            else
                            {
                                if (oldPerimeter < perimeter)
                                {
                                    biggestContour = approx.ToArray();
                                    detectedBorder = approx;
                                    oldPerimeter = perimeter;
                                }
                            }
                        }
                    }
                }
                mat2f.Dispose();
            });
            return biggestContour;
        }

        private IList<MatOfPoint> ProcessImage()
        {
            Mat grayMat = new Mat();
            Mat blurMat = new Mat();
            Mat edgesMat = new Mat();
            Mat final = new Mat();
            Mat h = new Mat();

            IList<MatOfPoint> contours = new JavaList<MatOfPoint>();

            OpenCV.Android.Utils.BitmapToMat(originalImage, originalMat);
            originalImage.Dispose();
            Imgproc.CvtColor(originalMat, grayMat, Imgproc.ColorBgr2gray);
            Imgproc.GaussianBlur(grayMat, blurMat, new OpenCV.Core.Size(3, 3), 0);
            Imgproc.Canny(blurMat, edgesMat, 10, 250);


            Mat kernel = Imgproc.GetStructuringElement(Imgproc.MorphRect, new Size(3, 3));
            Imgproc.MorphologyEx(edgesMat, final, Imgproc.MorphClose, kernel);

            Imgproc.FindContours(final, contours, h, Imgproc.RetrExternal, Imgproc.ChainApproxSimple);
            return contours;
        }
    }
}