using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace motmXamarin.Data
{
	[Table("userSettings")]
    public class UserSettings
    {
		public string Name { get; set; }
		public string PhoneToken { get; set; }
    }

	[Table("favSports")]
    public class FavSports
    {        
        public int SportId { get; set; }              
    }

	[Table("favClubs")]
    public class FavClubs
    {
        public int ClubId { get; set; }
    }

    [Table("userToken")]
	public class UserToken
	{
		public string UmbracoUserToken { get; set; }
	}

}

