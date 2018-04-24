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
    public partial class motmXamarinPage : ContentPage
    {
        readonly IList<Sport> sports = new ObservableCollection<Sport>();

        readonly DataManager manager = new DataManager();

        public motmXamarinPage()
        {
            BindingContext = sports;
            InitializeComponent();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            // Turn on network indicator
            this.IsBusy = true;

            try
            {
                var sportCollection = await manager.GetSports();

                foreach (Sport sport in sportCollection)
                {
                    if (sports.All(s => s.sportId != sport.sportId))
                        sports.Add(sport);
                }




            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
