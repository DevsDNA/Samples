using System.Reactive.Disposables;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Diagnostics;

namespace TestRxUI.Features.Test
{
    public partial class TestView
    {
        public TestView()
        {
            this.WhenActivated(d => ManageDisposables(d));
            InitializeComponent();
        }

        private CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.Bind(ViewModel, vm => vm.OperatorA, v => v.enOperatorA.Text));
            disposables.Add(this.Bind(ViewModel, vm => vm.OperatorB, v => v.enOperatorB.Text));
            disposables.Add(this.OneWayBind(ViewModel, vm => vm.Result, v => v.lbResult.Text, x => $"Result = {x}"));

            disposables.Add(this.BindCommand(ViewModel, vm => vm.DoSomethingQuickCommand, v => v.btQuick));
            disposables.Add(this.BindCommand(ViewModel, vm => vm.DoSomethingSlowlyCommand, v => v.btSlow));

            disposables.Add(this.WhenAnyValue(x => x.ViewModel.IsDoingSomethingQuick, x => x.ViewModel.IsDoingSomethingSlow)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x =>
                {
                    bool isBusy = x.Item1 || x.Item2;
                    aiBusy.IsVisible = isBusy;
                    aiBusy.IsRunning = isBusy;
                }, ex => Debug.WriteLine(ex?.Message)));

            disposables.Add(Observable.FromEventPattern(x => enOperatorA.Completed += x, x => enOperatorA.Completed -= x)
                .Subscribe(_ => enOperatorB.Focus()));

            return disposables;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }
    }
}
