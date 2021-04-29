
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using MovieAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Data.OleDb;
using System.Data;
using System.IO;
using CsvHelper;


namespace MovieAPI.Business
{
    public class MovieService : IMovieService
    {
        void IMovieService.metadata(Movie movieModel)
        {
            List<Movie> movieData = new List<Movie>();
            if (movieModel != null)
            {
                //Consider it as database for add movie
                movieData.Add(movieModel);
            }
        }

        IEnumerable<Movie> IMovieService.metadata(long movieID)
        {

            List<Movie> movieData = new List<Movie>();
            string pathToExcelFile = Constants.folderPath_MetData;

            var fileInfo = new FileInfo(pathToExcelFile);
            using (TextReader reader = fileInfo.OpenText())
            {
                using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    var records = csvReader.GetRecords<Movie>().ToList();
                    movieData = records.Where(r => r.MovieId == movieID).ToList();
                }
            }
            return movieData;
        }



        IEnumerable<StatsResult> IMovieService.stats()
        {
            List<Movie> movieData = new List<Movie>();
            List<MoviesStats> movieStats = new List<MoviesStats>();
            List<StatsResult> movieResult = new List<StatsResult>();
            int watches = 0;
            long averageWatchDurationS = 0;


            string pathToExcelFile = Constants.folderPath_MetData;
            var fileInfo = new FileInfo(pathToExcelFile);
            using (TextReader reader = fileInfo.OpenText())
            {
                using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    movieData = csvReader.GetRecords<Movie>().ToList();
                }
            }
           

            pathToExcelFile = Constants.folderPath_Stats;
            fileInfo = new FileInfo(pathToExcelFile);
            using (TextReader reader = fileInfo.OpenText())
            {
                using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    movieStats = csvReader.GetRecords<MoviesStats>().ToList();
                }
                
            }
            List<long> idList = movieData.Select(x => x.MovieId).Distinct().ToList();
            for (int j = 0; j < idList.Count(); j++)
            {
                var resltData = movieData.Where(x => x.MovieId == idList[j]).FirstOrDefault();
                StatsResult obj = new StatsResult();
                var ss = (from p in movieStats
                          where p.movieId == idList[j]
                          select p).ToList();
                if(ss.Count()>0)
                {
                    watches = ss.Count();
                    averageWatchDurationS = (ss.Sum(x => x.watchDurationMs) / watches);// Avg watch time in milli second

                }

                obj.Title = resltData.Title;
                obj.ReleaseYear = resltData.ReleaseYear;
                obj.watches = watches;
                obj.averageWatchDurationS = averageWatchDurationS;
                obj.MovieId = idList[j];
                movieResult.Add(obj);

            }
            return movieResult.OrderByDescending(x => x.watches).OrderByDescending(x => x.ReleaseYear);
        }
    }
}
