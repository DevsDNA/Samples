using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Pdf;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using PdfViewerPill.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(PdfViewer))]
namespace PdfViewerPill.Droid
{
    public class PdfViewer : IPdfViewer
    {
        public async Task Open(string filePath)
        {
            var client = new HttpClient();
            var rawPdf = await client.GetByteArrayAsync(filePath);

            var f = new Java.IO.File(MainActivity.CurrentActivity.CacheDir, "sample.pdf");

            FileOutputStream output = new FileOutputStream(f);
            await output.WriteAsync(rawPdf);
            output.Close();

            var pdfFileDescriptor = ParcelFileDescriptor.Open(f, ParcelFileMode.ReadOnly);
            var renderer = new PdfRenderer(pdfFileDescriptor);

            PdfActivity.PdfRenderer = renderer;
            PdfActivity.File = filePath;
            MainActivity.CurrentActivity.StartActivity(new Intent(MainActivity.CurrentActivity, typeof(PdfActivity)));
        }
    }
}