using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using motmXamarin.Data;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class App : Application
    {
		public static UserRepository UserRepo { get; private set; }
		public IList<Sport> sportsIds = new ObservableCollection<Sport>();

        public App(string dbPath)
        {
            InitializeComponent();

            UserRepo = new UserRepository(dbPath);

            //Delete data for testing
			UserRepo.DeleteTable();           
            
			if(UserRepo.GetUserSettings() == null)
			{
				//Send to Pick fav clubs and create new user table
				UserRepo.CreateUser();
				MainPage = new motmXamarin.Pages.ChooseFavClubsPage();

			}
			else
			{
			    //UserRepo.CreateUser();
				MainPage = new motmXamarin.MotmMasterPage();
			}
        }


        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
