using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace XDMActivityInstagram
{
    public class UIDocumentInteractionControllerDelegateClass : UIDocumentInteractionControllerDelegate
    {
        private UIViewController _viewController;
        private UIActivity _uiActivity;


        public UIDocumentInteractionControllerDelegateClass(UIActivity uiActivity)
        {
            _uiActivity = uiActivity;
            _viewController = _uiActivity.ViewController;
        }

        public override UIViewController ViewControllerForPreview (UIDocumentInteractionController controller)
        {
            return _viewController;
        }

        public override UIView ViewForPreview (UIDocumentInteractionController controller)
        {
            return _viewController.View;
        }

        public override RectangleF RectangleForPreview (UIDocumentInteractionController controller)
        {
            return _viewController.View.Frame;
        }


    }
}

