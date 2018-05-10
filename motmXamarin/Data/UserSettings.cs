using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace motmXamarin.Data
{
	[Table("userSettings")]
    public class UserSettings
    {
        //public int Id { get; set; }
		public string Name { get; set; }
		public string PhoneToken { get; set; }
		public string FavSports { get; set; }
        //public int[] FavTeams { get; set; }        
    }


}

