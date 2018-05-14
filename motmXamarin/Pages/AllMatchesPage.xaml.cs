using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace motmXamarin.Pages
{
    public partial class AllMatchesPage : ContentPage
    {
        public AllMatchesPage()
        {
            InitializeComponent();

            //Create function to handle Listview height. 
            //Counting the items in the Listview and multiply with the rowheight
            int Items = 2;
            int RowHeight = FavMatches.RowHeight;
            FavMatches.HeightRequest = Items * RowHeight; 

            //Function to swap matches in header?
        }

        //get fav clubs id
        //get fav sports id and save in local in object
        //get allmatches api
        //sort matches 
        // - finished
        // - fav club matches - one match goes to the main object
        // - regular matches
    }
}
