namespace ReactiveExtensionExamples.Values
{
    using ReactiveExtensionExamples.Extensions;
    using Xamarin.Forms;

    public static class Styles
    {
        public const string
            ReactiveNavigation = "ReactiveNavigation",
            ReactiveButton = "ReactiveButton",
            ReactiveEntry = "ReactiveEntry",
            ReactiveActivityIndicator = "ReactiveActivityIndicator";

        public static Color
            GreenAccent = Color.FromHex("#8af279"),
            BlueAccent = Color.FromHex("#1777e6");


        public static void Initialize()
        {
            if (Application.Current.Resources == null)
                Application.Current.Resources = new ResourceDictionary();

            Application.Current.Resources.Add(CreateReactiveNavigationStyle());
            Application.Current.Resources.Add(CreateReactiveButtonStyle());
            Application.Current.Resources.Add(CreateReactiveEntryStyle());
            Application.Current.Resources.Add(CreateReactiveActivityIndicatorStyle());
        }

        static Style CreateReactiveNavigationStyle()
        {
            return new Style(typeof(NavigationPage))
                .Set(NavigationPage.BarBackgroundColorProperty, GreenAccent)
                .Set(NavigationPage.BarTextColorProperty, Color.White)
                .Set(NavigationPage.IconProperty, "slideout.png");
        }

        static Style CreateReactiveButtonStyle()
        {
            return new Style(typeof(Button))
                .Set(Button.BackgroundColorProperty, BlueAccent)
                .Set(Button.TextColorProperty, Color.White)
                .Set(Button.MarginProperty, new Thickness(8d));
        }

        static Style CreateReactiveEntryStyle()
        {
            return new Style(typeof(Entry))
                .Set(Entry.TextColorProperty, BlueAccent)
                .Set(Entry.MarginProperty, new Thickness(8d));
        }

        static Style CreateReactiveActivityIndicatorStyle()
        {
            return new Style(typeof(ActivityIndicator))
                .Set(ActivityIndicator.ColorProperty, BlueAccent);
        }
    }
}


