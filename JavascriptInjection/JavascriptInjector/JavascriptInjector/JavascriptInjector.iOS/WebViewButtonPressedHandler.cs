namespace JavascriptInjector.iOS
{
    using Foundation;
    using WebKit;

    public class WebViewButtonPressedHandler : NSObject, IWKScriptMessageHandler
    {
        private CustomWebView customWebView;

        public WebViewButtonPressedHandler(CustomWebView customWebView)
        {
            this.customWebView = customWebView;
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            customWebView.OnSearchBtnPressed();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                customWebView = null;
            }
        }
    }
}