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
                     
        }

		public void GetMatchPlayers(Match match)
        {
			foreach (Player player in match.players)
            {
				if (players.All(m => m.playerId != player.playerId))
					players.Add(player);
            }
        }
    }
}
