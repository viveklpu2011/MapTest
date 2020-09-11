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
        HomeViewModel homeViewModel;
        public static double Lat = 0;
        public static double Lng = 0;
        public HomePage()
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel(Navigation);
            BindingContext = homeViewModel;
        }
         

        protected override void OnAppearing()
        {
            homeViewModel.searchLocation();
        }

    }
}