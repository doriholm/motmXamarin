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
		
		readonly IList<Club> clubs = new ObservableCollection<Club>();
		readonly IList<Club> clubsFromSearch = new ObservableCollection<Club>();
		readonly IList<Club> favClubs = new ObservableCollection<Club>();
		readonly DataManager manager = new DataManager();

        public ChooseFavClubsPage()
        {
            InitializeComponent();
            
        }

		void Search_Clubs(object sender, System.EventArgs e)
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

		void SelectFavClub(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (((ListView)sender).SelectedItem == null)
                return;
            
			var club = (e.SelectedItem as Club);
            ((ListView)sender).SelectedItem = null;

			if (favClubs.All(c => c.clubId != club.clubId))
				favClubs.Add(club);

			//if(!favClubsIdContains(clubId)){
			//	favClubsId.Add(clubId);
			//}
			//SetListViewHeight();
			FavClubsList.ItemsSource = favClubs;
		}

        public void SetListViewHeight()
		{
			int Items = favClubs.Count();
			int RowHeight = FavClubsList.RowHeight;
			FavClubsList.HeightRequest = Items * RowHeight; 
		}

		protected async override void OnAppearing()
        {
            base.OnAppearing();

			List<int> sports = new List<int>();
            try
            {            
				var sportCollection = await manager.GetSports();
				var obj = (Application.Current as App).sportsIds;
                
				foreach (Sport sport in sportCollection)
                {
                    obj.Add(sport);
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
			

			var test = (Application.Current as App).sportsIds;
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
