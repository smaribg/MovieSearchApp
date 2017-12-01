using System;
using System.Collections.Generic;

namespace MovieSearch
{
    public class Movie
    {
        public int Id
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }
        public string ImageRemote
        {
            get;
            set;
        }
        public string ImageLocal
        {
            get;
            set;
        }
        public string BackdropRemote
        {
            get;
            set;
        }
        public string BackdropLocal
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public List<String> Actors
        {
            get;
            set;
        }
        public int Runtime
        {
            get;
            set;
        }
        public List<String> Genres
        {
            get;
            set;
        }
        public double AverageVote
        {
            get;
            set;
        }
    }
}
