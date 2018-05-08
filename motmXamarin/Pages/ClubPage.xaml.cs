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

namespace motmXamarin
{
	public partial class ClubPage : ContentPage
	{
		//readonly int singleClubId; 
		readonly DataManager manager = new DataManager();
		readonly IList<Team> teams = new ObservableCollection<Team>();
		readonly ObservableCollection<SingleClub> thisClub = new ObservableCollection<SingleClub>();

		object newClub;

		//public ClubPage()
		//{
		//    InitializeComponent();
		//}

		public ClubPage(SingleClub clubObject)
		{
			InitializeComponent();

			newClub = clubObject as SingleClub;
			Teams.ItemsSource = teams;
			//singleClubId = clubId;
			Club.BindingContext = newClub;

			string umbracoUrl = "http://motmv2.azurewebsites.net/";
			clubLogo.Source = (clubObject.clubPic != "") ? umbracoUrl + clubObject.clubPic : "blogo.png";
			clubStadium.Source = (clubObject.stadiumPic != "") ? umbracoUrl + clubObject.stadiumPic : "stadium.png";
                        

		}



		protected override void OnAppearing()
		{
			base.OnAppearing();

			var singleClub = newClub as SingleClub;
			foreach (Team team in singleClub.teams)
			{
				if (teams.All(t => t.teamId != team.teamId))
					teams.Add(team);
			}

		}

		//public void OnSelection(object sender, EventArgs e)
		//{
		//	//var teamObj = e.SelectedItem;
		//	//var team = teamObj as Team;
		//	//int teamId = int.Parse(team.teamId);
		//	//var teamObj = (ListView)sender;
		//	//var team = teamObj.SelectedItem as Team;
		//	//int theTeamId = team.teamId;
		//	//var club = (ListView)sender;
		//	//var myJob = (club.SelectedItem as SingleClub);
		//	var club = newClub as SingleClub;

		//	((ListView)sender).SelectedItem = null; // de-select the row
		//	Navigation.PushAsync(new TeamPage(club));



		//}

		public void GetTeamItem(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
                return;
			
			var teamObj = e.SelectedItem;
			var team = teamObj as Team;
			int teamId = team.teamId;

			var club = newClub as SingleClub;

            ((ListView)sender).SelectedItem = null; // de-select the row
			Navigation.PushAsync(new TeamPage(club, teamId));

		}
	}
}
