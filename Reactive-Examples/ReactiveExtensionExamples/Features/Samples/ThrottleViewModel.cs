namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;

    public class ThrottleViewModel : Base.BaseReactiveViewModel
    {
        private string searchText;
        public ThrottleViewModel()
        {
        }

        public string SearchText
        {
            get => this.searchText;
            set => this.RaiseAndSetIfChanged(ref this.searchText, value);
        }
    }
}
