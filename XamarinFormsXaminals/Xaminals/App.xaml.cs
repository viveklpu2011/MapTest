using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xaminals.Database;
using Xaminals.Models;
using Xaminals.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xaminals
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           
            LoggedInUser objUser = App.Database.GetUser();
            if (objUser != null)
            {
                MainPage = new NavigationPage(new HomePage()) { BarBackgroundColor = Color.Green, BarTextColor = Color.White };
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.Green, BarTextColor = Color.White };
            }
        }
        static DbCls database;
        public static DbCls Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DbCls(DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("DbCls.db"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
