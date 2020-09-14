using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xaminals.Models;
using Xaminals.ViewModels;

namespace Xaminals.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchViewModel searchViewModel;
        public SearchPage()
        {
            InitializeComponent();
            searchViewModel = new SearchViewModel(Navigation);
            BindingContext = searchViewModel;
        }

        private void SwipeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SwipeListView.SelectedItem != null)
            {
                var place = SwipeListView.SelectedItem as string;
                searchViewModel.getPosition(place);
                SwipeListView.SelectedItem = null;
            }
        }
    }
}