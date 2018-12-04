namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;
    public class BufferWithWhereViewModel : Base.BaseReactiveViewModel
    {
        private string searchText;
        public BufferWithWhereViewModel()
        {
        }

        public string SearchText
        {
            get => this.searchText;
            set => this.RaiseAndSetIfChanged(ref this.searchText, value);
        }
    }
}
