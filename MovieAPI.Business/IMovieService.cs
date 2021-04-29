using MovieAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieAPI.Business
{
    public interface IMovieService
    {
        void metadata(Movie movieModel);
        IEnumerable<Movie> metadata(long movieID);
        IEnumerable<StatsResult> stats();
    }
}
