using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearch
{
    public class MovieApi : IMovieApi
    {
        public MovieApi()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
        }
        public async Task<List<string>> GetMovieTitle(string title){
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(title);
            if (response.Results.ToList() != null)
                return response.Results.Select(x => x.Title).ToList();
            else
                return new List<string>();
        }

    }
}
