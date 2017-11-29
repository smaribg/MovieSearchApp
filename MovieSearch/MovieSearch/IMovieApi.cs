using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearch
{
    public interface IMovieApi
    {
        Task<List<string>> GetMovieTitle(string title);
    }
}
