using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PdfViewerPill.Droid
{
    public static class PdfExtensions
    {
        public static Bitmap GetBitmapFromRender(this PdfRenderer renderer, int index = 0)
        {
            var page = renderer.OpenPage(index);                     
            var b = Bitmap.CreateBitmap(page.Width, page.Height, Bitmap.Config.Argb8888);
            page.Render(b, null, null, PdfRenderMode.ForDisplay);
            page.Close();
            return b;
        }
    }
}