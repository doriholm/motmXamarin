﻿using motmXamarin.Data;
using motmXamarin.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace motmXamarin
{
    public partial class ClubsMainPage : ContentPage
    {
        readonly IList<Club> clubs = new ObservableCollection<Club>();
        readonly DataManager manager = new DataManager();
        //If user has saved sportIds locally get that
        readonly List<int> sportIds = new List<int>();

        //public ClubsMainPage()
        //{
        //    InitializeComponent();
           
        //}

        public ClubsMainPage(List<int> sportIdsList)
        {
            InitializeComponent();
            
            sportIds = sportIdsList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
			string umbracoUrl = "http://motmv2.azurewebsites.net";

            // Turn on network indicator
            this.IsBusy = true;

            try
            {
                
                if(sportIds.Count() == 0)
                {
                    sportIds.Add(1084);
                }

                var sportCollection = await manager.GetClubs(sportIds);


                foreach (Club club in sportCollection)
                {
                    if (clubs.All(c => c.clubId != club.clubId))
                        clubs.Add(club);
                }
				foreach(Club club in clubs)
				{					
					club.clubPic = (club.clubPic != "") ? umbracoUrl + club.clubPic : "blogo.png";
					club.stadiumPic = (club.stadiumPic != "") ? umbracoUrl + club.stadiumPic : "stadium.png";
				}
            }
            finally
            {
                this.IsBusy = false;
            }
			BindingContext = clubs;

        }

        


        //changed method so it checks the Team count before deciding which page to navigate to. In case there are more than 1 team
         async void OnSelection(object sender, ItemTappedEventArgs e)
        {
            var club = (ListView)sender;
            var myJob = (club.SelectedItem as Club);

            //Deselect the element so it goes back to it's orignial state
            ((ListView)sender).SelectedItem = null;

            this.IsBusy = true;

            try
            {

                var sportCollection = await manager.GetSingleClub(myJob.clubId);
                GC.KeepAlive(sportCollection);

                var newClub = sportCollection as SingleClub;
            
                int teamCount = sportCollection.teams.Count();
                if (teamCount == 1)
                {
					await Navigation.PushAsync(new TeamPage(sportCollection, teamId:0));
                }
                else{
                    await Navigation.PushAsync(new ClubPage(sportCollection));
                }


            }
            finally
            {
                this.IsBusy = false;
            }
         
        }


    }


}
