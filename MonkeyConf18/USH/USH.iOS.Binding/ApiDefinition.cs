using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace USH.iOS.Binding
{
    // @interface DocScanner : NSObject
    [BaseType(typeof(NSObject))]
    public interface DocScanner
    {
        // -(id)initWithString:(NSString *)dummy;
        [Export("initWithString:")]
        IntPtr Constructor(string dummy);

        // -(NSArray *)detectBorders:(UIImage *)image;
        [Export("detectBorders:")]
        NSObject[] DetectBorders(UIImage image);

        // -(UIImage *)fixPerspective;
        [Export("fixPerspective")]
        UIImage FixPerspective();
    }
}

