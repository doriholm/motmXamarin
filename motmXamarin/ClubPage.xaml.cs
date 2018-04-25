using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace motmXamarin
{
    public partial class ClubPage : ContentPage
    {
        public ClubPage(int clubId)
        {
            InitializeComponent();
            labelText.Text = clubId.ToString();
        }
    }
}
