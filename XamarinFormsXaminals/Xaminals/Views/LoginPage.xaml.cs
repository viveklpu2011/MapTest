using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xaminals.ViewModels;

namespace Xaminals.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

    }
}
