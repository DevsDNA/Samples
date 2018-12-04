namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System.Reactive.Disposables;
    using Xamarin.Forms;

    public class CombineLatestViewModel : Base.BaseReactiveViewModel
    {
        private int red;
        private int green;
        private int blue;
        private ObservableAsPropertyHelper<Color> selectedColor;

        public CombineLatestViewModel()
        {
            this.WhenActivated(
               (CompositeDisposable disposables) =>
               {
                   this.WhenAnyValue(
                           x => x.Red, x => x.Green, x => x.Blue,
                           (red, green, blue) => Color.FromRgb(red, green, blue))
                       .ToProperty(this, x => x.Color, out this.selectedColor)
                       .DisposeWith(disposables);
               });
        }

        public int Red
        {
            get => this.red;
            set => this.RaiseAndSetIfChanged(ref this.red, value);
        }

        public int Green
        {
            get => this.green;
            set => this.RaiseAndSetIfChanged(ref this.green, value);
        }


        public int Blue
        {
            get => this.blue;
            set => this.RaiseAndSetIfChanged(ref this.blue, value);
        }

        public Color Color => this.selectedColor?.Value ?? default(Color);

    }
}
