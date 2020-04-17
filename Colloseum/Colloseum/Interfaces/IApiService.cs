using Colloseum.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Colloseum.Interfaces
{
    public interface IApiService
    {
        Task<MovieDetail> GetMovieDetails(int id);
        Task<List<Movie>> GetNowPlayingMovies();
        Task<MovieImage> GetMovieImages(int id);
        
        Task<List<Video>> GetMovieVideos(int id);
        Task<List<Genre>> GetGenres();
        Task<List<Movie>> GetUpcomingMovies();
        Task<List<Movie>> GetPopularMovies();
        Task<List<Cast>> GetCastForMovie(int id);
    }
}
