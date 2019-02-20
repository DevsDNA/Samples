namespace XFRxUpdate.Features.Main
{
    using ReactiveUI;
    using System;
    using System.Reactive.Linq;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.WhenActivated(async d =>
            {
                this.BindCommand(ViewModel, vm => vm.RefreshCommand, v => v.PlanesListView.RefreshCommand);
                this.Bind(ViewModel, vm => vm.SearhText, v => v.SearchTextLabel.Text);
                this.OneWayBind(ViewModel, vm => vm.BindingData, v => v.PlanesListView.ItemsSource);

                ViewModel.RefreshCommand.IsExecuting
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(ie => this.PlanesListView.IsRefreshing = ie);

                IObservable<System.Reactive.EventPattern<object>> ascendingButtonObservable = 
                    Observable.FromEventPattern(h => this.AscendingButton.Clicked += h, h => this.AscendingButton.Clicked -= h);
                d(ascendingButtonObservable
                    .Subscribe(e=>ViewModel.SortDescending = false));

                IObservable<System.Reactive.EventPattern<object>> descendingButtonObservable =
                    Observable.FromEventPattern(h => this.DescendingButton.Clicked += h, h => this.DescendingButton.Clicked -= h);
                d(descendingButtonObservable
                    .Subscribe(e => ViewModel.SortDescending = true));

                await ViewModel.RefreshCommand.Execute();
            });
        }
    }
}