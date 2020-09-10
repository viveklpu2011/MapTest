using Newtonsoft.Json;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xaminals.Models;
using Xaminals.Views;

namespace Xaminals.ViewModels
{
     
    public class HomeViewModel : BaseViewModel
    {
        private INavigation navigation;
        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;

        }





       
      
        public Command LogoutCommand
        {
            get
            {
                return new Command((data) =>
                {
                    App.Database.ClearLoginDetails();
                    App.Current.MainPage =new LoginPage();
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
