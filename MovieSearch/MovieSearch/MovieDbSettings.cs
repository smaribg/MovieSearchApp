using System;
using DM.MovieApi;




namespace MovieSearch
{
    public class MovieDbSettings : IMovieDbSettings
    {
        public MovieDbSettings()
        {
            
        }

		//c0468af93416b377f503f9796ca5be07
		//http://api.themoviedb.org/3/

        string IMovieDbSettings.ApiKey => "c0468af93416b377f503f9796ca5be07";

        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";
    }
}
