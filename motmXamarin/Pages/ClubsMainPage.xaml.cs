using motmXamarin.Data;
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
            BindingContext = clubs;
            sportIds = sportIdsList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

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
            }
            finally
            {
                this.IsBusy = false;
            }

        }

        public void Label_OnTapped(object sender, EventArgs e)
        {
            var obj = (Grid)sender;
            int theClubId = int.Parse(obj.ClassId);

            //var obj = ((TappedEventArgs)e).Parameter;
            //WorkIt.Text = obj.ClassId;
            Navigation.PushAsync(new ClubPage(theClubId));

        }




    }


}
