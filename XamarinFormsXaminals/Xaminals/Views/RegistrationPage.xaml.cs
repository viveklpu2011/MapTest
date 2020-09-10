using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xaminals.ViewModels;

namespace Xaminals.Views
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel(Navigation);
        }
    }
}
