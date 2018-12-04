namespace ReactiveExtensionExamples.Features.Main
{
    using ReactiveExtensionExamples.Enums;
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Xamarin.Forms;

    public class MasterPage : ContentPage
    {
        readonly Subject<NavigationPages> selectedNavigation = new Subject<NavigationPages>();
        public IObservable<NavigationPages> SelectedNavigation { get { return this.selectedNavigation.AsObservable(); } }

        public MasterPage()
        {
            Title = "Reactive Examples";

            ScrollView scrollContainer = new ScrollView
            {
                BackgroundColor = Values.Styles.GreenAccent
            };

            StackLayout navigationContainer = new StackLayout
            {
                Spacing = 8d
            };

            scrollContainer.Content = navigationContainer;

            Button delay = new Button
            {
                Text = "Delay",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.Delay)),
            };
            navigationContainer.Children.Add(delay);

            Button throttle = new Button
            {
                Text = "Throttle",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.Throttle)),
            };
            navigationContainer.Children.Add(throttle);

            Button buffer = new Button
            {
                Text = "Buffer",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.Buffer)),
            };
            navigationContainer.Children.Add(buffer);

            Button bufferWithWhere = new Button
            {
                Text = "Buffer with Where",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.BufferWithWhere)),
            };
            navigationContainer.Children.Add(bufferWithWhere);


            Button merge = new Button
            {
                Text = "Merge",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.Merge)),
            };
            navigationContainer.Children.Add(merge);

            Button combineLatest = new Button
            {
                Text = "Combine Latest",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.CombineLatest)),
            };
            navigationContainer.Children.Add(combineLatest);

            Button login = new Button
            {
                Text = "Login",
                Command = new Command(() => this.selectedNavigation.OnNext(NavigationPages.RxUiLogin)),
            };
            navigationContainer.Children.Add(login);

            Content = scrollContainer;
        }
    }
}
