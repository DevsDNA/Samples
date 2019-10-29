using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PdfViewerPill.Droid
{
    [Activity(Label = "PdfActivity")]
    public class PdfActivity : Activity
    {

        public  static PdfRenderer pdfRenderer;
        public static string file;

        public static PdfRenderer PdfRenderer { get => pdfRenderer; set => pdfRenderer = value; }
        public static string File { get => file; set => file = value; }

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RelativeLayout ll = new RelativeLayout(this);
            ImageView pdfImageView = new ImageView(this);

            ll.AddView(pdfImageView, new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            var pdfBitmap = pdfRenderer.GetBitmapFromRender();
            pdfImageView.SetImageBitmap(pdfBitmap);
            this.AddContentView(ll, new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
            // Create your application here
        }
    }
}