// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace XDMActivityInstagram
{
	[Register ("XDMAIDemoViewController")]
	partial class XDMAIDemoViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem barButtonItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imageView { get; set; }

		[Action ("actionButton:")]
		partial void actionButton (MonoTouch.UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}

			if (barButtonItem != null) {
				barButtonItem.Dispose ();
				barButtonItem = null;
			}
		}
	}
}
