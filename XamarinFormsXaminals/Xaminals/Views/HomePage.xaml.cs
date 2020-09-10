using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xaminals.Controls;
using Xaminals.ViewModels;
using XF.Material.Forms.UI.Dialogs;

namespace Xaminals.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public static double Lat = 0;
        public static double Lng = 0;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
            loadLatLng();
        }
        public async void loadLatLng()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Please Wait..");
            try
            {
                
                var locator = CrossGeolocator.Current;
                locator.AllowsBackgroundUpdates = true;
                var position = await locator.GetPositionAsync(10000);


                var start_pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)),
                        Label = "",
                        Address = ""
                    },
                    Id = "Xamarin",
                    startPin = true
                };
                customMap.CustomPins = new List<CustomPin> { start_pin };
                if (Device.OS == TargetPlatform.iOS)
                {
                    customMap.Pins.Add(start_pin.Pin);

                }
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)), Xamarin.Forms.Maps.Distance.FromKilometers(10)));

                await loadingDialog.DismissAsync();
            }
            catch (Exception ex)
            {
                await loadingDialog.DismissAsync();
            }
        }

        protected override void OnAppearing()
        {
            if (Lat != 0 && Lng != 0)
            {
                var start_pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(Convert.ToDouble(Lat), Convert.ToDouble(Lng)),
                        Label = "",
                        Address = ""
                    },
                    Id = "Xamarin",
                    startPin = true
                };
                customMap.CustomPins = new List<CustomPin> { start_pin };
                if (Device.OS == TargetPlatform.iOS)
                {
                    customMap.Pins.Add(start_pin.Pin);

                }
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(Lat), Convert.ToDouble(Lng)), Xamarin.Forms.Maps.Distance.FromKilometers(10)));
            }
        }

    }
}