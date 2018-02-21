using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Google.Maps;
using UIKit;

namespace GoogleMapsUtilsDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            MapServices.ProvideAPIKey("YOUR_API_KEY_HERE");

            LoadApplication(new App());



            return base.FinishedLaunching(app, options);
        }
    }
}
