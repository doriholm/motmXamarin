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
		object newClub;
		readonly IList<Match> matches = new ObservableCollection<Match>();
		object motmPlayer; 

		public TeamPage(SingleClub clubObject, int teamId )
        {
            InitializeComponent();
            
            //use teamId to find the right team and get team matches
			Team findTeam = new Team();
			if(teamId == 0)
			{
				findTeam = clubObject.teams[0];
			}
			else
			{
				findTeam = clubObject.teams.Find(t => t.teamId == teamId);
			}
			GetTeamMatches(findTeam);

			var theTeamId = teamId;
			newClub = clubObject as SingleClub;
			Club.BindingContext = newClub;

            string umbracoUrl = "http://motmv2.azurewebsites.net/";
            clubLogo.Source = (clubObject.clubPic != "") ? umbracoUrl + clubObject.clubPic : "blogo.png";
			clubStadium.Source = (clubObject.stadiumPic != "") ? umbracoUrl + clubObject.stadiumPic : "stadium.png";

			foreach(Match match in matches)
			{
				match.matchName = clubObject.clubName;
				match.opponentLogo = (match.opponentLogo != "") ? umbracoUrl + match.opponentLogo : "blogo.png";
			}

			foreach(var team in clubObject.teams)
			{
				motmPlayer = team.highestT;
			}


			MotmPlayer.BindingContext = motmPlayer;
			NextMatches.ItemsSource = matches;
			TeamNameListView.Text = clubObject.clubName;

        }

        public async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
			if (((ListView)sender).SelectedItem == null)
                return;

            var matchObj = e.SelectedItem;
			var match = matchObj as Match;
            var club = newClub as SingleClub;

            ((ListView)sender).SelectedItem = null; // de-select the row

			await Navigation.PushAsync(new MatchPage(club, match, matchFromAll:null));
        }

		public void GetTeamMatches(Team team)
		{
			foreach (Match match in team.matches)
            {
                if (matches.All(m => m.matchId != match.matchId))
					 matches.Add(match);
            }
		}
    }
}
