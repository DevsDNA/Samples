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
    public partial class MergeView
    {
        public MergeView()
        {
            InitializeComponent();
        }

        protected override void CreateBindingsAndSubscribes(CompositeDisposable disposables)
        {
            base.CreateBindingsAndSubscribes(disposables);
            this.Bind(ViewModel, vm => vm.Result, v => v.resultLabel.Text).DisposeWith(disposables);

            Observable
                .Merge(
                    Observable
                        .FromEventPattern(
                            x => this.yerayButton.Clicked += x,
                            x => this.yerayButton.Clicked -= x),
                    Observable
                        .FromEventPattern(
                            x => this.chemaButton.Clicked += x,
                            x => this.chemaButton.Clicked -= x),
                    Observable
                        .FromEventPattern(
                            x => this.marcoAntionioButton.Clicked += x,
                            x => this.marcoAntionioButton.Clicked -= x),
                    Observable
                        .FromEventPattern(
                            x => this.cianiButton.Clicked += x,
                            x => this.cianiButton.Clicked -= x))
                .Select(args => ((Button)args.Sender).Text)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(async text => await ViewModel.ShowResultCommand.Execute(text))
                .DisposeWith(disposables);
        }
    }
}