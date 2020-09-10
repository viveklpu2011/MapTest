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
    public class LoginViewModel : BaseViewModel
    {
        private INavigation navigation;
        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            
        }


      
        

        private string email
        {
            get;
            set;
        }
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string password
        {
            get;
            set;
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private Command loginCommand;
        public Command LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }


        private async Task ExecuteLoginCommand()
        {
            try
            {

                string error = string.Empty;

                if (string.IsNullOrWhiteSpace(Email))
                {
                    error += "Please provide email";
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", error, TimeSpan.FromSeconds(2));
                    return;
                }
                if (!string.IsNullOrEmpty(Email))
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(Email);
                    if (!match.Success)
                    {
                        error += "Please enter valid email";
                    }
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    error += "Please provide password";
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", error, TimeSpan.FromSeconds(2));
                    return;
                }


                LoggedInUser checkUser = App.Database.GetLoggedInUser(Email, Password);
                if (checkUser == null)
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Invalid credential", TimeSpan.FromSeconds(2));
                    return;
                }
                else
                {
                    App.Current.MainPage = new HomePage();
                }
            }
            catch (Exception ex)
            {



            }
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command((data) =>
                {
                     navigation.PushModalAsync(new RegistrationPage());
                });
            }
        }



       

    }
}
