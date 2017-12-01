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
        private async Task<List<string>> getActors(int id)
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<MovieCredit> response = await movieApi.GetCreditsAsync(id);
            if(response.Item == null){
                return new List<string>();
            }
            return response.Item.CastMembers.Select(x => x.Name).Take(3).ToList();
        }
        private async Task<int> getRuntime(int id)
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<DM.MovieApi.MovieDb.Movies.Movie> response = await movieApi.FindByIdAsync(id);
            if(response.Item != null){
				return response.Item.Runtime;
			}
            return 0;
                
        }

        public async Task<List<Movie>> GetTopRatedMovies(){
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.GetTopRatedAsync();
            List<Movie> movies;

            if (response.Results != null)
            {
                movies = response.Results.Select(x => new Movie
                {
                    Title = x.Title,
                    Year = x.ReleaseDate.Year,
                    ImageRemote = x.PosterPath,
                    ImageLocal = "",
                    BackdropRemote = x.BackdropPath,
                    BackdropLocal = "",
                    Description = x.Overview,
                    Id = x.Id,
                    Genres = x.Genres.Select(y => y.Name).ToList(),
                    AverageVote = x.VoteAverage
                }).ToList();
                foreach (Movie m in movies)
                {
                    m.Actors = await getActors(m.Id);
                    m.Runtime = await getRuntime(m.Id);
                }

                return movies;
            }
            else
                return new List<Movie>();
            
        }

        public async Task<List<Movie>> GetMovieTitle(string title){

            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(title);
            List<Movie> movies;


            if (response.Results != null)
            {
                movies = response.Results.Select(x => new Movie
                {
                    Title = x.Title,
                    Year = x.ReleaseDate.Year,
                    ImageRemote = x.PosterPath,
                    ImageLocal = "",
                    BackdropRemote = x.BackdropPath,
                    BackdropLocal = "",
                    Description = x.Overview,
                    Id = x.Id,
                    Genres = x.Genres.Select(y => y.Name).ToList(),
                    AverageVote = x.VoteAverage
                 
                }).ToList();
                foreach(Movie m in movies){
                    m.Actors = await getActors(m.Id);
                    m.Runtime = await getRuntime(m.Id);
                }

                return movies;
            }
            else
                return new List<Movie>();
        }


    }
}
