using ARKit;
using AudioUnit;
using ImageRecognition;
using ImageRecognition.iOS.Renderers;
using System;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(IrControl), typeof(IrControlRenderer))]
namespace ImageRecognition.iOS.Renderers
{
    public class IrControlRenderer : ViewRenderer<IrControl, ARSCNView>
    {
        private ARSCNView sceneView;
        private ARWorldTrackingConfiguration config;

        protected override void OnElementChanged(ElementChangedEventArgs<IrControl> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.RunSession = null;
                e.OldElement.PauseSession = null;
            }

            if (e.NewElement != null)
            {
                sceneView = new ARSCNView
                {
                    AutoenablesDefaultLighting = true,
                    DebugOptions = ARSCNDebugOptions.ShowFeaturePoints,
                };

                sceneView.Frame = Bounds;

                e.NewElement.RunSession = RunSession;
                e.NewElement.PauseSession = PauseSession;

                SetNativeControl(sceneView);
            }
        }

        private void PauseSession()
        {
            sceneView.Session.Pause();
        }

        private void RunSession()
        {
            config?.Dispose();
            sceneView?.Delegate?.Dispose();

            config = new ARWorldTrackingConfiguration();
            config.AutoFocusEnabled = true;
            config.PlaneDetection = ARPlaneDetection.Horizontal | ARPlaneDetection.Vertical;
            config.LightEstimationEnabled = true;
            config.WorldAlignment = ARWorldAlignment.GravityAndHeading;
            config.DetectionImages = ARReferenceImage.GetReferenceImagesInGroup("AR Resources", null);
            config.MaximumNumberOfTrackedImages = 1;

            sceneView.Session.Run(config, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);
            sceneView.Delegate = new IrScnDelegate();
        }
    }
}
