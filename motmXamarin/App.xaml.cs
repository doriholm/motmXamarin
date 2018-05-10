﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class App : Application
    {
		public static UserRepository UserRepo { get; private set; }

        public App(string dbPath)
        {
            InitializeComponent();

            UserRepo = new UserRepository(dbPath);

			MainPage = new motmXamarin.MainPage();
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
