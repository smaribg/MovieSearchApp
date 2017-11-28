using System;
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
        public async Task<string> GetMovieTitle(string title){
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(title);
            if (response.Results.FirstOrDefault() != null)
                return response.Results.FirstOrDefault().Title;
            else
                return "No movie found";
        }

    }
}
