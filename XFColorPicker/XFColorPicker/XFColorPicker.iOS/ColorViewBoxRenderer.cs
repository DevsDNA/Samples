[assembly: Xamarin.Forms.ExportRenderer(typeof(XFColorPicker.ColorViewBox), typeof(XFColorPicker.iOS.ColorViewBoxRenderer))]
namespace XFColorPicker.iOS
{
    using CoreAnimation;
    using CoreGraphics;
    using Foundation;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class ColorViewBoxRenderer : ViewRenderer
    {
        private static readonly CGColor[] colors = new[]
        {
            new CGColor(1,0,0,1),
            new CGColor(1,0,1,1),
            new CGColor(0,0,1,1),
            new CGColor(0,1,1,1),
            new CGColor(0,1,0,1),
            new CGColor(1,1,0,1),
            new CGColor(1,0,0,1)
        };

        private static readonly CGColor[] illumitation = new[]
{
            new CGColor(1,1,1,1),
            new CGColor(0.5f,0.5f,0.5f,0),
            new CGColor(0.5f,0.5f,0.5f,0),
            new CGColor(0,0,0,1)
        };

        private UIView gradientView;
        private CAGradientLayer gradientLayer;
        private CAGradientLayer illuminationLayer;
        private ColorViewBox cViewBox;

        public ColorViewBoxRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            CreateColorGradient(rect);
            CreateIlluminationGradient(rect);
            gradientView.Layer.AddSublayer(gradientLayer);
            gradientView.Layer.AddSublayer(illuminationLayer);
            SetNativeControl(gradientView);
            cViewBox = (ColorViewBox)Element;
            base.Draw(rect);
        }


        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            ProcessTouchesAndExtractColor(touches);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            ProcessTouchesAndExtractColor(touches);
        }

        private void ProcessTouchesAndExtractColor(NSSet touches)
        {
            UITouch touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                CGPoint point = touch.LocationInView(gradientView);
                cViewBox.SelectedColor = new PickerColorModel(GetColorAtTouchPoint(point), (float)point.X, (float)point.Y, cViewBox.Width, cViewBox.Height);
            }
        }

        private Color GetColorAtTouchPoint(CGPoint point)
        {
            byte[] alphaPixel = { 0, 0, 0, 0 };
            var colorSpace = CGColorSpace.CreateDeviceRGB();
            var bitmapContext = new CGBitmapContext(alphaPixel, 1, 1, 8, 4, colorSpace, CGBitmapFlags.PremultipliedLast);
            bitmapContext.TranslateCTM(-point.X, -point.Y);
            gradientView.Layer.RenderInContext(bitmapContext);
            return UIColor.FromRGBA(alphaPixel[0], alphaPixel[1], alphaPixel[2], alphaPixel[3]).ToColor();
        }

        private void CreateColorGradient(CGRect rect)
        {
            gradientView = new UIView(this.Frame)
            {
                BackgroundColor = UIColor.Clear
            };
            gradientLayer = new CAGradientLayer
            {
                Frame = rect,
                Colors = colors,
                StartPoint = new CGPoint(0, 0.5),
                EndPoint = new CGPoint(1, 0.5)
            };
        }

        private void CreateIlluminationGradient(CGRect rect)
        {
            illuminationLayer = new CAGradientLayer
            {
                Frame = rect,
                Colors = illumitation,
                StartPoint = new CGPoint(0, 0),
                EndPoint = new CGPoint(0, 1),
            };
        }
    }
}