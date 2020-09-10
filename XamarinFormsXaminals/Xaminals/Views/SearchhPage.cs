using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xaminals.Models;
using XF.Material.Forms.UI.Dialogs;

namespace Xaminals.Views
{

    public class SearchhPage : ContentPage
    {
        public static int check = 0;
        ListView lstViewPlaces;
        Entry sbSearch;
        public SearchhPage()
        {
            Title = "Search Address";

            sbSearch = new Entry() { Placeholder = "Search Location" };
            lstViewPlaces = new ListView()
            {
                IsPullToRefreshEnabled = true,
            };
            lstViewPlaces.SeparatorColor = Color.FromHex("#8A9C94");
            lstViewPlaces.SeparatorVisibility = SeparatorVisibility.Default;
            lstViewPlaces.ItemTemplate = new DataTemplate(typeof(CustomPlaceCell));
            lstViewPlaces.ItemSelected += lstViewPlaces_ItemSelected;
            sbSearch.TextChanged += SbSearch_TextChanged;
            var _layoutMain = new StackLayout
            {
                Orientation = StackOrientation.Vertical,   BackgroundColor=Color.White,  Padding=5,
                Children =
                {
                sbSearch,
                lstViewPlaces
                }
            };

            ImageButton closeImg = new ImageButton()
            {
                Source = "close.png",BackgroundColor=Color.Transparent,
                HeightRequest = 40,
                WidthRequest = 40
            };
            closeImg.Clicked += Image_Clicked;  
            var _layoutn = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(15, 60, 15, 60),
                BackgroundColor = Color.Gray,
                Children =
                {
               closeImg,_layoutMain
                }
            };
            Content = _layoutn;
           
        }

        private void Image_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

        private void lstViewPlaces_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lstViewPlaces.SelectedItem != null)
            {
                var place = (PredictionL)lstViewPlaces.SelectedItem;


                // AddressPage.Address1 = place.description;




                getPosition(place.description);
                lstViewPlaces.SelectedItem = null;
                

            }
        }
        async void getPosition(string address)
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Please Wait..");
            try
            {

                Geocoder gc = new Geocoder();
                IEnumerable<Position> possibleAddresses =
                    await gc.GetPositionsForAddressAsync(address);

                foreach (var result in possibleAddresses)
                {
                    await loadingDialog.DismissAsync();
                    HomePage.Lat = result.Latitude;
                    HomePage.Lng = result.Longitude;
                    this.Navigation.PopModalAsync();
                    break;
                }
            }
            catch
            {
            }
          
        }
        private void SbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(sbSearch.Text))
            {
                GetPlaces(sbSearch.Text);
            }
            else
            {
                lstViewPlaces.ItemsSource = null;
            }
        }


        public async void GetPlaces(string searchText)
        {

            try
            {
                List<string> dictCities = new List<string>();

                string url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" + searchText +
                    "&types=geocode&key=AIzaSyCbuKat-iBg90bfM8G0bytzVTdIThmgv94";
                wsGooglePlaces _objUserData = await getGooglePlaces(url);
                if (_objUserData != null && _objUserData.predictions.Count > 0)
                {
                    lstViewPlaces.ItemsSource = null;
                    lstViewPlaces.ItemsSource = _objUserData.predictions;
                }
                else if (_objUserData == null)
                {
                    await DisplayAlert("", "Internet seems to be down.", "OK");
                }
            }
            catch
            {
            }
            finally
            {
            }
        }


        public static async Task<wsGooglePlaces> getGooglePlaces(string url)
        {
            wsGooglePlaces objData = new wsGooglePlaces();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsGooglePlaces>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public class CustomPlaceCell : ViewCell
        {
            int fontSize = 14;
            public CustomPlaceCell()
            {
                Label lblName = new Label
                {
                    FontSize = fontSize,
                    HorizontalTextAlignment = TextAlignment.Start,
                    BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.Start,
                };
                lblName.SetBinding(Label.TextProperty, new Binding("description"));
                StackLayout _layoutRow11 = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                    BackgroundColor = Color.Transparent,
                    Children =
                {
                    lblName ,
                }
                };
                var sl_projectinfo = new StackLayout
                {
                    Padding = new Thickness(20),
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = Color.Transparent,
                    Children =
                    {
                       _layoutRow11,
                    }
                };
                View = sl_projectinfo;
            }
        }
    }
}
