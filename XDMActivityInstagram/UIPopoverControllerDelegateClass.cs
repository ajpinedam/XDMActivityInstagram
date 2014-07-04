using System;
using MonoTouch.UIKit;

namespace XDMActivityInstagram
{
    public class UIPopoverControllerDelegateClass : UIPopoverControllerDelegate
    {
        private UIViewController _viewController;

        public UIPopoverControllerDelegateClass (UIViewController viewController)
        {
            _viewController = viewController;
        }
    }
}

