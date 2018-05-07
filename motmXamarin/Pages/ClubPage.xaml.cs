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

        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var singleClub = newClub as SingleClub;
            foreach (Team team in singleClub.teams)
                {
                    teams.Add(team);
                }

            //// Turn on network indicator
            //this.IsBusy = true;

            //try
            //{

            //    var sportCollection = await manager.GetSingleClub(singleClubId);
            //    GC.KeepAlive(sportCollection);

            //    var club = sportCollection as SingleClub;
            //    newClub = sportCollection;
                    
            //    thisClub.Add(sportCollection);


            //    foreach(Team team in sportCollection.teams)
            //    {
            //        teams.Add(team);
            //    }


               
            //    //ClubName.Text = club.clubName;

            //    Club.BindingContext = newClub;

            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}

        }


    }
}
