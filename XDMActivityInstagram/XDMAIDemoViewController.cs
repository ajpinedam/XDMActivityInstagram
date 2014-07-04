
using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XDMActivityInstagram
{
    public partial class XDMAIDemoViewController : UIViewController
    {
        private UIPopoverController _popOver;

        static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public XDMAIDemoViewController ()
            : base (UserInterfaceIdiomIsPhone ? "XDMAIDemoViewController_iPhone" : "XDMAIDemoViewController_iPad", null)
        {

        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
			
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }

        partial void actionButton (UIBarButtonItem sender)
        {
            if(_popOver != null)
            {
                if(_popOver.PopoverVisible)
                {
                    return;
                }else
                {
                    _popOver.Dismiss(true);
                    _popOver = null;
                }
            }


            var instagramActivity = new XDMActivityInstagram();

            //instagramActivity.PresentFromButton = new UIBarButtonItem();

            var shareText = @"CatPaint #catpaint";

            NSUrl shareUrl = new NSUrl(@"http://catpaint.info");

            var activityItems = new NSObject[]{(NSString) shareText, shareUrl, this.imageView.Image};

            var applicationActivities = new UIActivity[]{instagramActivity};

            var activityViewController = new UIActivityViewController(activityItems, applicationActivities);

            //switch for iPhone and iPad

            if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                _popOver = new UIPopoverController (activityViewController);
                //_popOver.Delegate = this;
                _popOver.PresentFromBarButtonItem(sender, UIPopoverArrowDirection.Any, true);
            }
            else
            {
                this.PresentViewController(activityViewController, true, ()=>{
                    Console.WriteLine("Action Completed");
                });
            }

        }
    }
}

