namespace ReactiveExtensionExamples.Features.Samples
{
    using ReactiveUI;

    public class BufferViewModel : Base.BaseReactiveViewModel   
    {
        private string searchText;
        public BufferViewModel()
        {
        }

        public string SearchText
        {
            get => this.searchText;
            set => this.RaiseAndSetIfChanged(ref this.searchText, value);
        }
    }
}
