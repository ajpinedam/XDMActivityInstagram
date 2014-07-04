using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.CoreGraphics;
using System.IO;

namespace XDMActivityInstagram
{
    public class XDMActivityInstagram
    {
        private readonly string InstagramUrl = "instagram://app";

        public XDMActivityInstagram ()
        {
            ActivityType = @"UIActivityTypePostToInstagram";
            ActivityTitle = @"Instagram";

            ActivityImage = new UIImage ("instagram.png");

        }

        public string ActivityType {get; private set;}

        public string ActivityTitle {get; private set;}

        public UIImage ActivityImage {get; private set;}


        public UIImage ShareImage {get; set;}
        public string ShareString {get; set;}
        public string BackgroundColors {get; set;}
        public bool IncludeURL {get; set;}

        public UIDocumentInteractionController DocumentController {get; set;}


        public UIBarButtonItem PresentFromButton {get; set;}


        public bool CanPerformWithActivityItems(List<object> activyItems)
        {
            var instagramUrl = new NSUrl (InstagramUrl);

            if (!UIApplication.SharedApplication.CanOpenUrl (instagramUrl)) 
                return false;

            foreach (var item in activyItems) 
            {
                var image = item as UIImage; 
                if (image != null && IsImageLargeEnough (image)) {
                    return true;
                } else {
                    //TODO: Log message
                }
            }

            return false;
        }

        public void PrepareWithActivityItems(List<object> activyItems)
        {
            foreach (var item in activyItems) 
            {
                if (item.GetType () == typeof(UIImage)) 
                {
                    this.ShareImage = (UIImage)item;
                } else if (item.GetType () == typeof(string)) 
                {
                    this.ShareString = 
                            !string.IsNullOrEmpty (this.ShareString) 
                                    ? string.Format ("{0} {1}", this.ShareString, item.ToString ()) 
                                    : item.ToString ();
                } 
                else if (item.GetType () == typeof(NSUrl)) 
                {
                    if (IncludeURL)
                        this.ShareString += !string.IsNullOrEmpty (this.ShareString) ? " " + ((NSUrl)item).AbsoluteString : ((NSUrl)item).AbsoluteString;
                } else {
                    //TODO: Log message: "Unknown item type %@", item"
                }

            }
        }

        public void PerformActivity()
        {
            var cropVal = this.ShareImage.Size.Height > this.ShareImage.Size.Width ? this.ShareImage.Size.Width : this.ShareImage.Size.Height;

            cropVal *= this.ShareImage.CurrentScale;

            var cropRect = new RectangleF {
                Height = cropVal,
                Width = cropVal
            };

            var imageRef = this.ShareImage.CGImage.WithImageInRect (cropRect);

            var image =  UIImage.FromImage (imageRef);

            var imageData = image.AsJPEG (1.0f);

            //TODO: Find way to release image
            imageRef.Dispose ();
            //CGImageRelease (imageRef);

            var path =  Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            var fullUrl = string.Format ("{0}{1}", path, "instagram.igo"); 

            Console.WriteLine ("Full Url: " + fullUrl);

            NSError error;

            if (!imageData.Save(fullUrl, NSDataWritingOptions.Atomic, out error)) 
            {
                Console.WriteLine ("Error saving the image: " + fullUrl);
                return;
            }

            NSUrl fileUrl = new NSUrl (fullUrl);

            this.DocumentController = new UIDocumentInteractionController
            {
                Url = fileUrl
            };

            //this.DocumentController.Delegate = this;

            DocumentController.Uti = "com.instagram.exclusivegram";

            if (!string.IsNullOrEmpty (this.ShareString)) 
            {
                var dict = new NSMutableDictionary ();
                dict.Add ((NSString)"InstagramCaption", (NSString)this.ShareString);
                this.DocumentController.Annotation = dict;
            }

            this.DocumentController.PresentOpenInMenu (this.PresentFromButton, true);

        }

        private bool IsImageLargeEnough(UIImage image)
        {
            var imageSize = image.Size;
            return ((imageSize.Height * image.CurrentScale) >= 612 && (imageSize.Width * image.CurrentScale) >= 612);
        }

        private bool IsImageSquare(UIImage image)
        {
            var imageSize = image.Size;
            return (imageSize.Height == imageSize.Width);
        }



    }
}

