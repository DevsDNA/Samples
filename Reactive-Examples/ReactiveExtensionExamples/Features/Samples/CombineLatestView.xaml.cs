namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System.Reactive.Disposables;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CombineLatestView
	{
		public CombineLatestView ()
		{
			InitializeComponent ();
		}

        protected override void CreateBindingsAndSubscribes(CompositeDisposable disposables)
        {
            base.CreateBindingsAndSubscribes(disposables);
            this.Bind(ViewModel, vm => vm.Red, v => v.redColor.Value).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.Green, v => v.greenColor.Value).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.Blue, v => v.blueColor.Value).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Red, v => v.redColor.MinimumTrackColor, cv=> 
            {
                return Color.FromRgb(cv, 0, 0);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Green, v => v.greenColor.MinimumTrackColor, cv =>
            {
                return Color.FromRgb(0, cv, 0);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Blue, v => v.blueColor.MinimumTrackColor, cv =>
            {
                return Color.FromRgb(0, 0, cv);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Red, v => v.redColorLabel.TextColor, cv =>
            {
                return Color.FromRgb(cv, 0, 0);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Green, v => v.greenColorLabel.TextColor, cv =>
            {
                return Color.FromRgb(0, cv, 0);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Blue, v => v.blueColorLabel.TextColor, cv =>
            {
                return Color.FromRgb(0, 0, cv);
            }).DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.Color, v => v.boxViewColor.BackgroundColor).DisposeWith(disposables);
        }
    }
}