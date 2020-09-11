using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xaminals.Controls;
using Xaminals.Models;
using Xaminals.Views;
using XF.Material.Forms.UI.Dialogs;

namespace Xaminals.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        private INavigation navigation;
        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;


            loadLatLng();
        }

        public CustomMap customMap { get; private set; }
        public async void loadLatLng()
        {
            customMap = new CustomMap();
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Please Wait..");
            try
            {

                var locator = CrossGeolocator.Current;
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


        public async void searchLocation()
        {
            if (HomePage.Lat != 0 && HomePage.Lng != 0)
            {
                var start_pin = new CustomPin
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(Convert.ToDouble(HomePage.Lat), Convert.ToDouble(HomePage.Lng)),
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
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(HomePage.Lat), Convert.ToDouble(HomePage.Lng)), Xamarin.Forms.Maps.Distance.FromKilometers(10)));
            }

        }



        public Command LogoutCommand
        {
            get
            {
                return new Command(async (data) =>
                {
                    bool answer = await App.Current.MainPage.DisplayAlert("", "Are you sure?", "Yes", "No");
                    if (answer)
                    {
                        App.Database.ClearLoginDetails();
                        App.Current.MainPage = new LoginPage();
                    }
                });
            }
        }


        public Command SearchCommand
        {
            get
            {
                return new Command((data) =>
                {
                    navigation.PushModalAsync(new SearchhPage());
                });
            }
        }


    }
}
