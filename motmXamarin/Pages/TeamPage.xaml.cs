using motmXamarin.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace motmXamarin.Pages
{
    public partial class TeamPage : ContentPage
    {
        public TeamPage()
        {
            InitializeComponent();
            NextMatches.ItemsSource = "nae";
        }

        public async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MatchPage());
        }
    }
}
