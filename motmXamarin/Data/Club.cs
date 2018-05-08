using System;
using System.Collections.Generic;

namespace motmXamarin.Data
{
    public class Club
    {
        public int clubId { get; set; }
        public string clubName { get; set; }
    }

    public class SingleClub
    {
        public string clubName { set; get; }
        public int clubId { set; get; }
        public string clubPic { set; get; }
        public string stadiumPic { set; get; }
        public object Sponsor { set; get; }
        public string clubabout { set; get; }
        public List<Team> teams { set; get; }
    }

    public class Team
    {
        public string teamName { set; get; }
        public int teamId { set; get; }
        public List<Match> matches { set; get; }
        public List<Player> players { set; get; }
    }

    public class Match
    {
        public int matchId { set; get; }
        public string matchName { set; get; }
        public DateTime startDate { set; get; }
        public string[] location { set; get; }
        //public string opponent { set; get; }
        public List<Player> players { set; get; }
        //public List<string> players { set; get; }
    }

    public class Player
    {
        public string playerName { set; get; }
        public int playerId { set; get; }
        public string playerGuid { set; get; }
    }
}
