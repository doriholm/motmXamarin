using System;
using System.Collections.Generic;
using motmXamarin.Pages;
using motmXamarin.Data;
using motmXamarin;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

			App.UserRepo.AddNewPerson(newPerson.Text, newToken.Text);
            statusMessage.Text = App.UserRepo.StatusMessage;
        }

        public void OnGetButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            List<UserSettings> people = App.UserRepo.GetAllPeople();
            peopleList.ItemsSource = people;
        }

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			statusMessage.Text = "";
			App.UserRepo.GetUserSettings();
		}
    }
}
