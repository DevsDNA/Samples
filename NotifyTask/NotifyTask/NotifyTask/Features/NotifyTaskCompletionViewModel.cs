

namespace NotifyTask.Features
{
    using NotifyTask.Services;
    public class NotifyTaskCompletionViewModel
    {
        public NotifyTaskCompletionViewModel()
        {
            UrlByteCount = new NotifyTaskCompletion<int>(
              MyStaticService.CountBytesInUrlAsync("https://www.google.com"));
        }
        public NotifyTaskCompletion<int> UrlByteCount { get; private set; }
    }
}
