﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xaminals.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void btnlogin_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }
    }
}
