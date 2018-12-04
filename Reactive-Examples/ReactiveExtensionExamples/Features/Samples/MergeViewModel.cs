namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    using System.Reactive;

    public class MergeViewModel : Base.BaseReactiveViewModel
    {
        private ReactiveCommand<string, Unit> showResultCommand;
        private string result;
        public MergeViewModel()
        {
        }

        public ReactiveCommand<string, Unit> ShowResultCommand => this.showResultCommand;

        public string Result
        {
            get => this.result;
            set => this.RaiseAndSetIfChanged(ref this.result, value);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            this.showResultCommand = ReactiveCommand.Create<string>(ExecuteShowResultCommand);
        }

        private void ExecuteShowResultCommand(string name)
        {
            if (name.ToLowerInvariant() != "ciani")
                Result = $"{name} has broken the build!";
            else
                Result = $"He has not broke the build!";
        }
    }
}
