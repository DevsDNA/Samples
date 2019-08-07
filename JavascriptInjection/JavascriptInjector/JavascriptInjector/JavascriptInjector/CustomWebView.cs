namespace JavascriptInjector
{
    using System;
    using Xamarin.Forms;

    public class CustomWebView : WebView
    {
        public Action<string> SetSearchText;
        public Action DoSearch;
        public Action Observe;

        public event EventHandler SearchPreseed;

        public void OnSearchBtnPressed()
        {
            SearchPreseed?.Invoke(this, null);
        }
    }
}
