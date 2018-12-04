namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;

    public class DelayViewModel : Base.BaseReactiveViewModel
    {
        private string searchText;
        public DelayViewModel()
        {
        }

        public string SearchText
        {
            get => this.searchText;
            set => this.RaiseAndSetIfChanged(ref this.searchText, value);
        }
    }
}
