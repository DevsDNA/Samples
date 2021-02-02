namespace AndroidScreenRecorder.Services
{
    public interface IScreenRecordingService
    {
        void AskForStartRecording();

        void StartRecording();

        void StopRecording();
    }
}
