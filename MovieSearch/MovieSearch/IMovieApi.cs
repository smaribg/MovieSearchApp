using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearch
{
    public interface IMovieApi
    {
        Task<List<Movie>> GetMoviesByTitle(string title);
        Task<Movie> GetMovieByTitle(string title);
        Task<List<Movie>> GetTopRatedMovies();
    }
}
