using System;
using System.Collections.Generic;
using System.Linq;
using motmXamarin.Data;
using SQLite;

namespace motmXamarin
{
    public class UserRepository
    {
		SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public UserRepository(string dbPath)
        {
			conn = new SQLiteConnection(dbPath);
			conn.CreateTable<UserSettings>();
			conn.CreateTable<FavSports>();
			conn.CreateTable<FavClubs>();
            
        }
        
		public void AddSports(List<int> sportIds)
		{
			if (sportIds != null)
			{
				try
				{
					foreach (int Id in sportIds)
					{
						conn.Insert(new FavSports { SportId = Id });
					}
					var Test = conn.Table<FavSports>().ToList();
				}
				catch (Exception ex)
				{
					StatusMessage = ex.Message;
				}
			}
		}

		public void AddFavClub(Club club)
        {
            int result = 0;
         
            try
            {               
				result = conn.Insert(new FavClubs {
					clubId = club.clubId,
					clubPic = club.clubPic,
					clubName = club.clubName,
					Homecity = club.Homecity,
					stadiumPic = club.stadiumPic,
					Sponsor = club.Sponsor
				});
                
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }

		public void RemoveFavClub(int clubId)
        {
            int result = 0;

            try
            {
				conn.Table<FavClubs>().Delete(c => c.clubId == clubId);                
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }

		public void AddNewPerson(string name, string token)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                
				result = conn.Insert(new UserSettings { Name = name, PhoneToken = token });
                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

		public string CreateUser()
        {
			string result;
            try
            {				            
                conn.Insert(new UserSettings { Name = "User", PhoneToken = "TokenString" });
				result = "Success";
            }
            catch (Exception ex)
            {
				result = ex.Message;
            }
			return result;
        }

        //Delete data for testing
		public void DeleteTable()
        {
			conn.DeleteAll<UserSettings>();
			conn.DeleteAll<FavSports>();
			conn.DeleteAll<FavClubs>();
        }

		public UserSettings GetUserSettings()
		{
			
			try{
				return conn.Table<UserSettings>().Where(u => u.Name == "User").FirstOrDefault();
                
			}
			catch (Exception ex){
				StatusMessage = string.Format("Failed to fetch user. Error: {0}", ex.Message);
			}
			return new UserSettings();
		}

		public List<FavClubs> GetFavClubs()
        {

            try
            {
				return conn.Table<FavClubs>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
			return new List<FavClubs>();
        }


		public List<UserSettings> GetAllPeople()
        {
            try
            {
				return conn.Table<UserSettings>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

			return new List<UserSettings>();
        }
    }
}