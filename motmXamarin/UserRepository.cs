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

		public void GetUserSettings()
		{
			
			try{
				var result = conn.Table<UserSettings>().Where(u => u.Name == "user2").FirstOrDefault();


			}
			catch (Exception ex){
				StatusMessage = string.Format("Failed to fetch user. Error: {0}", ex.Message);
			}
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