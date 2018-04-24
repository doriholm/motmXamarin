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

        public ClubsMainPage()
        {
            BindingContext = clubs;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Turn on network indicator
            this.IsBusy = true;

            try
            {
                var sportCollection = await manager.GetClubs();

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

        void Label_OnTapped(object sender, EventArgs args)
        {
            var obj = (Label)sender;
            int clubId = int.Parse(obj.ClassId);
            //var obj = ((TappedEventArgs)e).Parameter;

            //var clubId = obj as IList<string>;
            //WorkIt.Text = obj.clubId;
            //WorkIt.Text = obj.;
        }




    }


}
