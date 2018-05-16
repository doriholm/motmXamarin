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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace motmXamarin.Pages
{
    public partial class AllMatchesPage : ContentPage
    {
		readonly DataManager manager = new DataManager();
		IList<allmatches> favMatches = new ObservableCollection<allmatches>();
		IList<allmatches> otherMatches = new ObservableCollection<allmatches>();

		object firstMatch;


        public AllMatchesPage()
        {
			
            InitializeComponent();

           

        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			if(favMatches.Count() == 0 && otherMatches.Count() == 0){
				this.IsBusy = true;

                try
                {
                    var globalSportIds = (Application.Current as App).sportsIds;
                    var favClubsIds = (Application.Current as App).favClubsIds;
                    List<int> favClubsIdsList = new List<int>();

                    foreach (FavClubs club in favClubsIds)
                    {
                        if (!favClubsIdsList.Contains(club.clubId))
                            favClubsIdsList.Add(club.clubId);
                    }

                    var sportCollection = await manager.GetAllMatches(globalSportIds);


                    if (favClubsIds.Count() != 0)
                    {

                        foreach (allmatches match in sportCollection)
                        {
                            foreach (allmatchesinfo info in match.info)
                            {
                                if (favClubsIdsList.Contains(info.clubid))
                                    favMatches.Add(match);
                                else
                                    otherMatches.Add(match);
                            }

                        }
                    }
                    else
                    {
                        foreach (allmatches match in sportCollection)
                        {
                            if (otherMatches.All(c => c.matchId != match.matchId))
                                otherMatches.Add(match);
                        }

                    }

                    favMatches = favMatches.OrderBy(x => x.startDate).Reverse().ToList();
                    otherMatches = otherMatches.OrderBy(x => x.startDate).Reverse().ToList();

                    if (favMatches.Count() != 0)
                    {
                        FixUmbracoUrl(favMatches);
                        FavMatchesText.IsVisible = true;
                        FavMatchesListView.IsVisible = true;
                        firstMatch = favMatches[0];
                        FavMatchesText.Text = favMatches.Count() + FavMatchesText.Text;
                    }
                    else
                    {
                        firstMatch = otherMatches[0];
						FavMatchesText.IsVisible = false;

                    }



                    if (otherMatches.Count() != 0)
                        FixUmbracoUrl(otherMatches);

                    FavMatchesListView.ItemsSource = favMatches;
                    OtherMatchesListView.ItemsSource = otherMatches;
                    HeaderClubMatch.BindingContext = firstMatch;

                    int Items = favMatches.Count();
                    int RowHeight = FavMatchesListView.RowHeight;
                    FavMatchesListView.HeightRequest = Items * RowHeight;
                }
                finally
                {
                    this.IsBusy = false;
                    absoluteDiv.IsVisible = false;
                    KommendeKampe.IsVisible = true;
                    HeaderClubMatch.IsVisible = true;

                }
			}

		}



		public void FixUmbracoUrl(IList<allmatches> matches)
		{
			string umbracoUrl = "http://motmv2.azurewebsites.net";


				foreach (allmatches match in matches)
                {
                    match.clublogo = (match.clublogo != "") ? umbracoUrl + match.clublogo : "";
				    match.stadiumPic = (match.stadiumPic != "") ? umbracoUrl + match.stadiumPic : "";
                }
            
		}

		public async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            var matchObj = e.SelectedItem;
			var match = matchObj as allmatches;
            

            ((ListView)sender).SelectedItem = null; // de-select the row

			await Navigation.PushAsync(new MatchPage(club:null, match:null, matchFromAll:match));
        }

        
        
    }
}
