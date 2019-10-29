[assembly: Xamarin.Forms.ExportRenderer(typeof(JavascriptInjector.CustomWebView), typeof(JavascriptInjector.Droid.CustomWebViewRenderer))]
namespace JavascriptInjector.Droid
{
    using Android.Content;
    using Android.Webkit;
    using Java.Interop;
    using Xamarin.Forms.Platform.Android;

    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context)
        {
        }

        protected override WebView CreateNativeControl()
        {
            WebView webView = base.CreateNativeControl();
            webView.Settings.JavaScriptEnabled = true;
            webView.AddJavascriptInterface(new JavascriptInterface(Element as CustomWebView), "Test");

            return webView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                ((CustomWebView)e.NewElement).SetSearchText = SetSearchText;
                ((CustomWebView)e.NewElement).DoSearch = DoSearch;
                ((CustomWebView)e.NewElement).Observe = Observe;
            }
            if (e.OldElement != null)
            {
                ((CustomWebView)e.OldElement).SetSearchText = null;
                ((CustomWebView)e.OldElement).DoSearch = null;
                ((CustomWebView)e.OldElement).Observe = null;
            }
        }

        private void SetSearchText(string text)
        {
            string javascript = $"javascript: document.getElementById('sb_form_q').value = '{text}';";
            Control.EvaluateJavascript(javascript, null);
        }

        private void DoSearch()
        {
            string javascript = "javascript: document.getElementById('sbBtn').click();";
            Control.EvaluateJavascript(javascript, null);
        }

        private void Observe()
        {
            string javascript = "javascript: document.getElementById('sb_form_go').onclick = function() { Test.ButtonPressed(); };";
            Control.EvaluateJavascript(javascript, null);
        }
    }

    public class JavascriptInterface : Java.Lang.Object
    {
        private CustomWebView customWebView;

        public JavascriptInterface(CustomWebView customWebView) : base()
        {
            this.customWebView = customWebView;
        }

        [JavascriptInterface()]
        [Export("ButtonPressed")]
        public void ButtonPressed()
        {
            customWebView.OnSearchBtnPressed();
        }
    }
}