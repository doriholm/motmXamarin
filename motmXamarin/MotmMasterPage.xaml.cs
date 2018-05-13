using System;
using System.Collections.Generic;
using motmXamarin.Pages;
using motmXamarin.Data;

using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace motmXamarin
{
    public partial class MotmMasterPage : MasterDetailPage
    {
		readonly IList<Sport> sports = new ObservableCollection<Sport>();
		readonly DataManager manager = new DataManager();
		List<int> sportIdsList = new List<int>();
        public MotmMasterPage()
        {
            InitializeComponent();
                                    
			if (App.UserRepo.GetUserSettings() == null)
            {
                //Send to Pick fav clubs and create new user table
                App.UserRepo.CreateUser();
				Detail = new NavigationPage(new motmXamarin.Pages.ChooseFavClubsPage());            
            }
            else
            {
				var getSportsDb = App.UserRepo.GetSports();
				var globalSportIds = (Application.Current as App).sportsIds;

				foreach(FavSports item in getSportsDb){
					if (globalSportIds.All(c => c != item.SportId))
						globalSportIds.Add(item.SportId);
				}

				Detail = new NavigationPage(new ClubsMainPage());
            }

			//getSports and create labels
			FetchAllSports();
         
            IsPresented = false;
            #if __IOS__
			IsGestureEnabled = false;
            #endif
        }

        
        

        public async void FetchAllSports()
		{
			
            try
            {
				var sportCollection = await manager.GetSports();
                
                foreach (Sport sport in sportCollection)
                {
                    if (sports.All(s => s.sportId != sport.sportId))
                        sports.Add(sport);
                }

            }
			catch(Exception ex)
            {
				var error = ex.Message;
            }         
				var globalSportIds = (Application.Current as App).sportsIds;
			    if(globalSportIds.Count() == 0)
				{
					foreach (Sport item in sports)
					{
						item.checkBox = true;
					    sportIdsList.Add(item.sportId);
					}
				}
				else
				{
					foreach (Sport item in sports)
					{
					    if (globalSportIds.Contains(item.sportId))
					    {
					    	sportIdsList.Add(item.sportId);
					    	item.checkBox = true;
					    }
							
					}
				}
         
			//SportList.ItemsSource = sports;
			CheckBoxStatus();
		}

		public void CheckBoxStatus()
		{
			var sportLabelTap = new TapGestureRecognizer();
			sportLabelTap.Tapped += (object s, EventArgs e) =>
            {
				var btn = (Label)s;
				int nr = int.Parse(btn.ClassId);
				if(btn.FontAttributes == FontAttributes.Bold){
					btn.FontAttributes = FontAttributes.None;
					btn.TextColor = Color.FromHex("d2d1d1");
					var findId = sportIdsList.Find(i => i == nr);
					sportIdsList.Remove(findId);
				}
				else
				{
					btn.FontAttributes = FontAttributes.Bold;
					btn.TextColor = Color.White;
					sportIdsList.Add(nr);
				}
                
            };
            
			
			foreach(Sport item in sports)
			{

				var sportLabel = new Label
				{
					Text = item.sportName,
					TextColor = Color.FromHex("d2d1d1"),
					FontSize = 16,
					Margin = 10,
					ClassId = item.sportId.ToString()
				};
				
				if (item.checkBox == true){
					sportLabel.FontAttributes = FontAttributes.Bold;
					sportLabel.TextColor = Color.White;
				}
                sportsLayout.Children.Add(sportLabel);
				sportLabel.GestureRecognizers.Add(sportLabelTap);
            
			}
            
		}

        void ShowAllMatchesPage(object sender, System.EventArgs e)
        {
			App.UserRepo.AddSports(sportIdsList);
            IsPresented = false;
            Detail = new NavigationPage(new AllMatchesPage());
        }

		void ShowAllClubsPage(object sender, System.EventArgs e)
        {
			App.UserRepo.AddSports(sportIdsList);
			(Application.Current as App).sportsIds = sportIdsList;
            IsPresented = false;
            Detail = new NavigationPage(new ClubsMainPage());
        }

        
    }
}

