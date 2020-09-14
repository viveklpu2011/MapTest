using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xaminals.Models;
using Xaminals.Views;
using XF.Material.Forms.UI.Dialogs;

namespace Xaminals.ViewModels
{
    
    public class SearchViewModel : BaseViewModel
    {
        private INavigation navigation;
        public SearchViewModel(INavigation navigation)
        {
            this.navigation = navigation;

        }
        private string address
        {
            get;
            set;
        }
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public ObservableCollection<string> _items = new ObservableCollection<string>();

        public ObservableCollection<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        
       public async void getPosition(string address)
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
                   await navigation.PopModalAsync();
                    break;
                }
            }
            catch (Exception ex)
            {
            }

        }
       
        public Command AddressChanged
        {
            get
            {
                return new Command((data) =>
                {
                    if (!string.IsNullOrEmpty(Address))
                    {
                        GetPlaces(Address);
                    }
                    else
                    {
                        Items.Clear();
                    }
                });
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
                    Items.Clear();
                    Items = new ObservableCollection<string>();
                    foreach (var item in _objUserData.predictions)
                    {
                        Items.Add(item.description);
                    }
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




        public Command BackCommand
        {
            get
            {
                return new Command((data) =>
                {
                    navigation.PopModalAsync();
                });
            }
        }


    }
}
