using System;
using System.Threading.Tasks;

namespace MovieSearch
{
    public interface IMovieApi
    {
        Task<string> GetMovieTitle(string title);
    }
}
