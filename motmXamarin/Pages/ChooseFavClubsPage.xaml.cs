using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using motmXamarin.Data;
using Xamarin.Forms;

namespace motmXamarin.Pages
{
    public partial class ChooseFavClubsPage : ContentPage
    {
		
		readonly List<int> sports = new List<int>();
		readonly IList<Club> clubs = new ObservableCollection<Club>();
		readonly IList<Club> clubsFromSearch = new ObservableCollection<Club>();
		readonly IList<Club> favClubs = new ObservableCollection<Club>();

		readonly DataManager manager = new DataManager();

        public ChooseFavClubsPage()
        {
            InitializeComponent();
         
			TapGestureRecognizer tapContinueBtn = new TapGestureRecognizer();
            tapContinueBtn.Tapped += ContinueBtnTapped;
            
			continueBtn.GestureRecognizers.Add(tapContinueBtn);
        
        }



	void ContinueBtnTapped(object sender, System.EventArgs e)
    {
			Navigation.PushAsync(new motmXamarin.ClubsMainPage());
			Navigation.RemovePage(this);

    }

		void Search_Clubs(object sender, System.EventArgs e)
		{
			if(SearchEntry.Text != null)
			{
				clubsFromSearch.Clear();
                string searchStringLower = SearchEntry.Text.ToLower();
                foreach (Club club in clubs)
                {
                    if (club.clubName.ToLower().Contains(searchStringLower))
                        clubsFromSearch.Add(club);
                }
                ClubsList.ItemsSource = clubsFromSearch;
			}

		}

		void SelectFavClub(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
                return;
            
			var club = (e.SelectedItem as Club);
            ((ListView)sender).SelectedItem = null;

			App.UserRepo.AddFavClub(club);
                       

			//SetListViewHeight();
			var GetFavClubs = App.UserRepo.GetFavClubs();
			foreach(FavClubs favClub in GetFavClubs)
			{
				if (favClubs.All(c => c.clubId != favClub.clubId))
				{
					Club newClub = new Club();
					newClub.clubId = favClub.clubId;
                    newClub.clubName = favClub.clubName;
                    favClubs.Add(newClub);
				}

			}

			FavClubsList.ItemsSource = favClubs;
		}

		void RemoveFavClub(object sender, SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
                return;

            var club = (e.SelectedItem as Club);
            ((ListView)sender).SelectedItem = null;

			favClubs.Remove(club);
			App.UserRepo.RemoveFavClub(club.clubId);

		}

  //      public void SetListViewHeight()
		//{
		//	int Items = favClubs.Count();
		//	int RowHeight = FavClubsList.RowHeight;
		//	FavClubsList.HeightRequest = Items * RowHeight; 
		//}

		protected async override void OnAppearing()
        {
            base.OnAppearing();


            try
            {            
				var sportCollection = await manager.GetSports();
				var obj = (Application.Current as App).sportsIds;
                
				foreach (Sport sport in sportCollection)
                {
					obj.Add(sport.sportId);
					sports.Add(sport.sportId);
                }
            }
			catch(Exception ex)
            {
				var result = ex.Message;
            }

			GetClubs(sports);
			SearchEntry.IsEnabled = true;
			App.UserRepo.AddSports(sports);
			


        }

		public async void GetClubs(List<int> sportIds)
		{
			try
            {
				var clubCollection = await manager.GetClubs(sportIds);
                
                foreach (Club club in clubCollection)
                {
                    if (clubs.All(c => c.clubId != club.clubId))
                        clubs.Add(club);
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
		}
    }
}
