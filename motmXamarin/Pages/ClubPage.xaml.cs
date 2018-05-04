using motmXamarin.Data;
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
        readonly int singleClubId; 
        readonly DataManager manager = new DataManager();
        readonly IList<Team> teams = new ObservableCollection<Team>();
        readonly ObservableCollection<SingleClub> thisClub = new ObservableCollection<SingleClub>();
        //object newClub = new { clubName = "John Doe", Company = "Xamarin" };
        object newClub;

        //public ClubPage()
        //{
        //    InitializeComponent();
        //}

        public ClubPage(int clubId)
        {
            InitializeComponent();


            Teams.ItemsSource = teams;
            singleClubId = clubId;

        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Turn on network indicator
            this.IsBusy = true;

            try
            {

                var sportCollection = await manager.GetSingleClub(singleClubId);
                GC.KeepAlive(sportCollection);

                var club = sportCollection as SingleClub;
                newClub = sportCollection;
                    
                thisClub.Add(sportCollection);

                //int teamCount = 0;
                foreach(Team team in sportCollection.teams)
                {
                    //teamCount += 1;
                    teams.Add(team);
                }

                ////if only one team go to TeamPage
                //var totalCount = teamCount;
                //ClubName.Text = club.clubName;

                Club.BindingContext = newClub;

            }
            finally
            {
                this.IsBusy = false;
            }

        }
    }
}
