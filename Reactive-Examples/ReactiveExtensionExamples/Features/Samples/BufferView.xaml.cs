namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BufferView
    {
        public BufferView()
        {
            InitializeComponent();
        }

        protected override void CreateBindingsAndSubscribes(CompositeDisposable disposables)
        {
            base.CreateBindingsAndSubscribes(disposables);
            this.disposables.Add(this.Bind(ViewModel, vm => vm.SearchText, v => v.textEntry.Text));

            Observable
               .FromEventPattern<EventHandler<TextChangedEventArgs>, TextChangedEventArgs>(
                   x => this.textEntry.TextChanged += x,
                   x => this.textEntry.TextChanged -= x)
               //We want to wait 3 seconds
               .Buffer(TimeSpan.FromSeconds(3))
               .Select(argsList => 
                    string.Join(Environment.NewLine
                              , argsList.Select(args => args.EventArgs.NewTextValue).Reverse().ToList()))
               .ObserveOn(RxApp.MainThreadScheduler)
               .Subscribe(text =>
               {
                   this.lastEntries.Children
                           .Insert(
                               0,
                               new Label { Text = text });

                   this.lastEntries.Children
                           .Insert(
                               1,
                               new Label
                               {
                                   Text = string.Format("Received at {0:H:mm:ss}", DateTime.Now),
                                   FontAttributes = FontAttributes.Italic,
                                   FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                                   TextColor = Color.Gray
                               });

                   this.lastEntries.Children
                           .Insert(
                               2,
                               new BoxView { BackgroundColor = Values.Styles.GreenAccent, HeightRequest = 1d });
               })
               .DisposeWith(this.disposables);
        }
    }
}