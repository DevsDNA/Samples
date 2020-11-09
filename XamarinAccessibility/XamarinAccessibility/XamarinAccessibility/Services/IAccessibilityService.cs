namespace XamarinAccessibility.Services
{
    public interface IAccessibilityService
    {
        bool IsVoiceAssistanceActive();
        void PlayAudio(string message);
    }
}
