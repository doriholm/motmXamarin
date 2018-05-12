﻿using System;
using System.Collections.Generic;

namespace motmXamarin.Data
{
    public class Club
    {
		public string clubName { set; get; }
        public int clubId { set; get; }
        public string clubPic { set; get; }
        public string stadiumPic { set; get; }
        public string Sponsor { set; get; }
        public string Homecity { get; set; }
    }

	public class SingleClub
    {
        public string clubName { set; get; }
        public int clubId { set; get; }
        public string HomeCity { set; get; }
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
        public Player highestT { set; get; }
        public List<Match> matches { set; get; }
        public List<Player> players { set; get; }
    }


    public class allmatches
    {
        public int matchId { set; get; }
        public string matchName { set; get; }
        public DateTime startDate { set; get; }
        public DateTime endDate { set; get; }
        public string[] location { set; get; }
        public string opponent { set; get; }
        public List<allmatchesinfo> info { set; get; }
    }
    public class allmatchesinfo
    {
        public string teamName { set; get; }
        public int teamId { set; get; }
        public int clubid { set; get; }
        public string clublogo { set; get; }
        public string clubname { set; get; }
        public List<Player> players { set; get; }
    }
    public class Match
    {
        public int matchId { set; get; }
        public string matchName { set; get; }
        public DateTime startDate { set; get; }
        public DateTime EndDate { set; get; }
        public string[] location { set; get; }
        public string opponent { set; get; }
        public int winner { set; get; }
        public List<Player> players { set; get; }
        //public List<string> players { set; get; }


    }

    public class Player
    {
        public string playerName { set; get; }
        public int playerId { set; get; }
        public string playerGuid { set; get; }
        public int Trophies { set; get; }
    }
}
