using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Android;

namespace Xaminals.Droid
{
    [Activity(Label = "Xaminals", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string permissionAF = Manifest.Permission.AccessFineLocation;
        const string permissionAC = Manifest.Permission.AccessCoarseLocation;
 
        const int RequestLocationId = 0;
        readonly string[] permissions =
    {
       Manifest.Permission.AccessFineLocation,
         Manifest.Permission.AccessCoarseLocation,
    };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            XF.Material.Droid.Material.Init(this, savedInstanceState);

            DependencyService.Register<ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(this);



            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(permissionAF) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(permissionAC) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            


            global::Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
