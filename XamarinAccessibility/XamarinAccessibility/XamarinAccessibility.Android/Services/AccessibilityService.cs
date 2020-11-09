using Android.AccessibilityServices;
using Android.Content;
using Android.Views.Accessibility;
using Android.Widget;
using XamarinAccessibility.Services;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinAccessibility.Droid.Services.AccessibilityService))]
namespace XamarinAccessibility.Droid.Services
{
    public class AccessibilityService : IAccessibilityService
    {
        public bool IsVoiceAssistanceActive()
        {
            AccessibilityManager am = (AccessibilityManager)Android.App.Application.Context.GetSystemService(Context.AccessibilityService);
            if (am != null & am.IsEnabled)
                return am.IsTouchExplorationEnabled;
             
            return false;
        }

        public void PlayAudio(string message)
        {
            if (IsVoiceAssistanceActive())
                Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}