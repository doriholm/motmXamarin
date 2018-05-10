using System;
using System.Collections.Generic;
using motmXamarin.Pages;
using motmXamarin.Data;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class MotmMasterPage : MasterDetailPage
    {
        readonly List<int> sportIdsList = new List<int>();

        public MotmMasterPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new ClubsMainPage(sportIdsList))
            {
                BarBackgroundColor = Color.Green,
                BarTextColor = Color.White
            };

            IsPresented = false;
            #if __IOS__
			IsGestureEnabled = false;
            #endif
        }

        //
        void ShowAllClubsPage(object sender, System.EventArgs e)
        {
            
            IsPresented = false;
            Detail = new NavigationPage(new ClubsMainPage(sportIdsList));
        }

        void ShowAllMatchesPage(object sender, System.EventArgs e)
        {

            IsPresented = false;
            Detail = new NavigationPage(new AllMatchesPage());
        }

        void SportCheckbox(object sender, System.EventArgs e)
        {
            var btn = (Button)sender;
            int nr = int.Parse(btn.ClassId);

            if(!sportIdsList.Contains(nr))
            {
                sportIdsList.Add(nr);
                btn.BackgroundColor = Color.Purple;
            }
               else{
                sportIdsList.Remove(nr);
                btn.BackgroundColor = Color.Gray;            
            }   
        }
    }
}

