using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class MotmMasterPage : MasterDetailPage
    {
        public MotmMasterPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new ClubsMainPage())
            {
                BarBackgroundColor = Color.Green,
                BarTextColor = Color.White
            };

            IsPresented = false;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new ClubsMainPage());

            IsPresented = false;
        }
    }
}

