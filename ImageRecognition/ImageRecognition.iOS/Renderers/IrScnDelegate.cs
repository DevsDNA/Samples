using ARKit;
using Foundation;
using SceneKit;
using System;
using UIKit;

namespace ImageRecognition.iOS.Renderers
{
    public class IrScnDelegate : ARSCNViewDelegate
    {
        [Export("renderer:didAddNode:forAnchor:")]
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            ARImageAnchor imageAnchor = anchor as ARImageAnchor;
            if (imageAnchor == null)
                return;

            ARReferenceImage detectecImage = imageAnchor.ReferenceImage;
            if (detectecImage.Name != "AR_DevsDna_Card")
                return;

            node.AddChildNode(CreatePlane(detectecImage));
        }

        private SCNNode CreatePlane(ARReferenceImage detectecImage)
        {
            nfloat width = detectecImage.PhysicalSize.Width;
            nfloat height = detectecImage.PhysicalSize.Height;

            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = UIColor.Blue;
            material.DoubleSided = false;

            SCNPlane geometry = SCNPlane.Create(width, height);
            geometry.Materials = new[] { material };

            SCNNode planeNode = new SCNNode
            {
                Geometry = geometry,
                Position = new SCNVector3(0, 0, 0)
            };

            float angle = (float)(-Math.PI / 2);
            planeNode.EulerAngles = new SCNVector3(angle, 0, 0);

            return planeNode;
        }
    }
}
