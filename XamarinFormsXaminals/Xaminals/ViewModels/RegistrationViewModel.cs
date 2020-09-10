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

    public class RegistrationViewModel : BaseViewModel
    {
        private INavigation navigation;
        public RegistrationViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Dob = DateTime.Now;
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
        private string confirmPassword
        {
            get;
            set;
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                }
            }
        }
        private DateTime dob
        {
            get;
            set;
        }
        public DateTime Dob
        {
            get { return dob; }
            set
            {
                if (dob != value)
                {
                    dob = value;

                    OnPropertyChanged("Dob");
                }
            }
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
        private Command registerCommand;
        public Command RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand = new Command(async () => await ExecuteRegisterCommand()));
            }
        }


        private async Task ExecuteRegisterCommand()
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
                if (Password != ConfirmPassword)
                {
                    error += "Password not match with confirm password";
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", error, TimeSpan.FromSeconds(2));
                    return;
                }
                if (string.IsNullOrWhiteSpace(Address))
                {
                    error += "Please provide address";
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", error, TimeSpan.FromSeconds(2));
                    return;
                }

                LoggedInUser checkUser = App.Database.CheckLoggedInUser(Email);
                if (checkUser != null)
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Error", "Try with diffrent email", TimeSpan.FromSeconds(2));
                    return;
                }

                LoggedInUser objUser = new LoggedInUser();
                objUser.Email = Email;
                objUser.Password = Password;
                objUser.Address = Address;
                objUser.Dob = Dob;
                App.Database.SaveLoggedInUser(objUser);

                LoggedIn  obj  = new LoggedIn ();
                obj.Email = Email;
                obj.Password = Password;
                App.Database.SaveLoggedIn(obj);


                App.Current.MainPage = new HomePage();


            }
            catch (Exception ex)
            {



            }
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
