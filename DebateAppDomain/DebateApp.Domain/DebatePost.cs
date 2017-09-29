﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DebateApp.Domain
{
    public  class DebatePost
    {
        private static int idcounter = 0;//change this to pull from a persistent id counter later
        public int PostID { get; set; }
        public string CommentText { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserID { get; set; }
        public int MaxLength { get; set; }
        public string Team { get; set; }
        public int Astros { get; set; }
        public int Votes { get; set; }
        public Dictionary<String, String> Sources { get; set; }
        public int DebateID { get; set; }
        public DebatePost(string s, User u)
        {
            PostID = idcounter;
            idcounter += 1;
            CommentText = s;
            UserID = u.UserID;
        }
        public virtual bool Validate()
        {
            return CommentText.Length <= MaxLength;
        }
    }
}
