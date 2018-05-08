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

		public TeamPage(SingleClub clubObject, int teamId )
        {
            InitializeComponent();
            
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

            NextMatches.ItemsSource = "nae";
        }

        public async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MatchPage());
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
