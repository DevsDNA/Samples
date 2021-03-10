using Foundation;
using ObjCRuntime;
using System;
using System.Runtime.InteropServices;
using UIKit;
using XamarinAccessibility.iOS.Services;
using XamarinAccessibility.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AccessibilityService))]
namespace XamarinAccessibility.iOS.Services
{
    public class AccessibilityService : IAccessibilityService
    {
        [DllImport(Constants.UIKitLibrary, EntryPoint = "UIAccessibilityPostNotification")]
        public extern static void PostNotification(uint notification, IntPtr id);

        public bool IsVoiceAssistanceActive()
        {
            return UIAccessibility.IsVoiceOverRunning;
        }

        public void PlayAudio(string message)
        {
            if (IsVoiceAssistanceActive())
                PostNotification(1008, new NSString(message).Handle);
        }
    }
}