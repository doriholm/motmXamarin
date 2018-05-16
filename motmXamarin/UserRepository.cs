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
			conn.CreateTable<MatchVotes>();
            
        }
        
		public void AddSports(List<int> sportIds)
		{
			conn.DeleteAll<FavSports>();

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
				result = conn.Table<FavClubs>().Delete(c => c.clubId == clubId);                
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
        
        public int VoteMotm(int matchId)
		{
			int result = 0;
            try
            {
				result = conn.Insert(new MatchVotes { matchId = matchId });                
            }
            catch (Exception ex)
            {
				return result;
            }
            return result;
		}


		public string CreateUser()
        {
			string result;
            try
            {				            
                var insert = conn.Insert(new UserSettings { Name = "User", PhoneToken = "TokenString" });
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
			conn.DeleteAll<MatchVotes>();
        }


        //
        // Get data from the Tables
        //


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

		public List<FavSports> GetSports()
        {

            try
            {
				return conn.Table<FavSports>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
			return new List<FavSports>();
        }

		public List<MatchVotes> GetMatchVotes()
        {

            try
            {
				return conn.Table<MatchVotes>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
			return new List<MatchVotes>();
        }

    }
}