using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using ReactiveUI;

namespace TestRxUI.Features.Test
{
    public class TestViewModel : ReactiveObject
    {
        private int operatorA;
        private int operatorB;
        private int result;

        private ObservableAsPropertyHelper<bool> isDoingSomethingQuick;
        private ObservableAsPropertyHelper<bool> isDoingSomethingSlow;

        private CompositeDisposable disposables;

        public TestViewModel()
        {
            DoSomethingQuickCommand = ReactiveCommand.CreateFromTask(DoSomethingQuickAsync);
            DoSomethingSlowlyCommand = ReactiveCommand.CreateFromTask(DoSomethingSlowlyAsync);
        }

        public int OperatorA
        {
            get => operatorA;
            set => this.RaiseAndSetIfChanged(ref operatorA, value);
        }

        public int OperatorB
        {
            get => operatorB;
            set => this.RaiseAndSetIfChanged(ref operatorB, value);
        }

        public int Result
        {
            get => result;
            private set => this.RaiseAndSetIfChanged(ref result, value);
        }

        public ReactiveCommand<Unit, Unit> DoSomethingQuickCommand { get; private set; }
        public bool IsDoingSomethingQuick { get { return isDoingSomethingQuick.Value; } }

        public ReactiveCommand<Unit, Unit> DoSomethingSlowlyCommand { get; private set; }
        public bool IsDoingSomethingSlow { get { return isDoingSomethingSlow.Value; } }

        public void OnAppearing()
        {
            disposables = disposables ?? new CompositeDisposable();

            disposables.Add(DoSomethingQuickCommand.IsExecuting.ToProperty(this, x => x.IsDoingSomethingQuick, out isDoingSomethingQuick));
            disposables.Add(DoSomethingSlowlyCommand.IsExecuting.ToProperty(this, x => x.IsDoingSomethingSlow, out isDoingSomethingSlow));

            disposables.Add(DoSomethingQuickCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex?.Message)));
            disposables.Add(DoSomethingSlowlyCommand.ThrownExceptions.Subscribe(ex => Debug.WriteLine(ex?.Message)));

            disposables.Add(this.WhenAnyValue(x => x.OperatorA, x => x.OperatorB)
                .Subscribe(x => Result = x.Item1 + x.Item2));
        }

        public void OnDisappearing()
        {
            disposables?.Dispose();
            disposables = null;
        }

        private async Task DoSomethingSlowlyAsync()
        {
            await Task.Delay(5000);
        }

        private async Task DoSomethingQuickAsync()
        {
            await Task.Delay(2000);
        }
    }
}
