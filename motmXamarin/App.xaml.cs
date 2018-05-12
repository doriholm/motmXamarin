﻿using System;
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
		public List<int> sportsIds = new List<int>();

        public App(string dbPath)
        {
            InitializeComponent();

            UserRepo = new UserRepository(dbPath);

            //Delete data for testing
			//UserRepo.DeleteTable();

            //insert sports into the global Sports list
			foreach (FavSports sport in UserRepo.GetSports())
				sportsIds.Add(sport.SportId);
            
            MainPage = new motmXamarin.MotmMasterPage();

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
