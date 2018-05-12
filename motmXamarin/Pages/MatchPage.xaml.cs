using motmXamarin.Data;
using motmXamarin.Pages;
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
    public partial class MatchPage : ContentPage
    {
		SingleClub theClub = new SingleClub();
		readonly IList<Player> players = new ObservableCollection<Player>();




		public MatchPage(SingleClub club, Match match)
        {
            InitializeComponent();

			theClub = club;
			GetMatchPlayers(match);

			Club.BindingContext = theClub;

            string umbracoUrl = "http://motmv2.azurewebsites.net/";
			clubLogo.Source = (theClub.clubPic != "") ? umbracoUrl + theClub.clubPic : "blogo.png";
			clubStadium.Source = (theClub.stadiumPic != "") ? umbracoUrl + theClub.stadiumPic : "stadium.png";

			PlayerList.ItemsSource = players;

			if (theClub.Sponsor.ToString() == "")
				sponsorName.Text = "NONE";
			else
				sponsorName.Text = theClub.Sponsor.ToString();
                     
        }

		public void GetMatchPlayers(Match match)
        {
			foreach (Player player in match.players)
            {
				if (players.All(m => m.playerId != player.playerId))
					players.Add(player);
            }
        }
        
		async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
                return;
			
			var player = e.SelectedItem as Player;
			((ListView)sender).SelectedItem = null; // de-select the row

			var answer = await DisplayAlert("Confirm?", "Vælge " + player.playerName + " som MOTM", "Yes", "No");
            if(answer == true)
			{
				ChoosePlayer.IsVisible = false;
				SponsorAd.IsVisible = false;
				ThxForVoting.IsVisible = true;
			}
   
            
		}
    }
}
