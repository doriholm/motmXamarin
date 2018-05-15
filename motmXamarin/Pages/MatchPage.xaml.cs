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
		readonly DataManager manager = new DataManager();

		int thisMatchId;



		public MatchPage(SingleClub club, Match match, allmatches matchFromAll)
        {
            InitializeComponent();
            
			string umbracoUrl = "http://motmv2.azurewebsites.net/";

            if(club == null)
			{
				SingleClub newClub = new SingleClub();
				newClub.stadiumPic = matchFromAll.stadiumPic;
				newClub.clubPic = matchFromAll.clublogo;
				newClub.clubName = matchFromAll.clubname;
				newClub.HomeCity = matchFromAll.HomeCity;


				theClub = newClub;
				thisMatchId = matchFromAll.matchId;
				OpponentName.Text = matchFromAll.opponent;

				foreach(var item in matchFromAll.info)
				{
					foreach (Player player in item.players)
                    {
                        if (players.All(m => m.playerId != player.playerId))
                            players.Add(player);
                    }
				}

				clubLogo.Source = (theClub.clubPic != "") ?  theClub.clubPic : "blogo.png";
                clubStadium.Source = (theClub.stadiumPic != "") ? theClub.stadiumPic : "stadium.png";

			}
			else
			{
				theClub = club;
				thisMatchId = match.matchId;
				OpponentName.Text = match.opponent;
				GetMatchPlayers(match);

				clubLogo.Source = (theClub.clubPic != "") ? umbracoUrl + theClub.clubPic : "blogo.png";
                clubStadium.Source = (theClub.stadiumPic != "") ? umbracoUrl + theClub.stadiumPic : "stadium.png";

			}





			Club.BindingContext = theClub;




			PlayerList.ItemsSource = players;


                     
        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var matchVotesLIst = (Application.Current as App).MatchVotes;

			if(matchVotesLIst.Contains(thisMatchId))
			{
				ChoosePlayer.IsVisible = false;
                SponsorAd.IsVisible = false;
                ThxForVoting.IsVisible = true;
			}
			else
			{
				ChoosePlayer.IsVisible = true;
                SponsorAd.IsVisible = true;
                
			}
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

			var answer = await DisplayAlert("Bekræfte?", "Vælge " + player.playerName + " som MOTM", "Yes", "No");
            if(answer == true)
			{
				var voteResult = manager.VoteMotm(player.playerId);
				App.UserRepo.VoteMotm(thisMatchId);
				(Application.Current as App).MatchVotes.Add(thisMatchId);
				ChoosePlayer.IsVisible = false;
				SponsorAd.IsVisible = false;
				ThxForVoting.IsVisible = true;

			}
   
            
		}
    }
}
