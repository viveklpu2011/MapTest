using Foundation;
using Plugin.Toasts;
using UIKit;
using Xamarin.Forms;

namespace Xaminals.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            XF.Material.iOS.Material.Init();
            global::Xamarin.Forms.FormsMaterial.Init();

            DependencyService.Register<ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
