using Colloseum.Interfaces;
using Colloseum.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Colloseum.Services
{
    public class ApiService : IApiService
    {
        private HttpClient client;
        private List<Movie> movies;

        public ApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
            movies = new List<Movie>();
        }

        public async Task<List<Movie>> GetNowPlayingMovies()
        {
            try
            {
                var genres = await GetGenres();
                var nowPlayingMoviesUrl = Constants.BASE_URL + "movie/now_playing?api_key=" + Constants.API_KEY + "&language=en-US&page=1";
                var responseMessage = await client.GetAsync(nowPlayingMoviesUrl);
                return await ExtractMoviesFromResponse(responseMessage, genres);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public async Task<List<Movie>> GetUpcomingMovies()
        {
            var genres = await GetGenres();
            var upcomingMoviesUrl = Constants.BASE_URL + "movie/upcoming?api_key=" + Constants.API_KEY + "&language=en-US&page=1";
            try
            {
                var responseMessage = await client.GetAsync(upcomingMoviesUrl);
                return await ExtractMoviesFromResponse(responseMessage, genres);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<Movie>> GetPopularMovies()
        {
            var genres = await GetGenres();
            var popularMoviesUrl = Constants.BASE_URL + "movie/popular?api_key=" + Constants.API_KEY + "&language=en-US&page=1";
            try
            {
                var responseMessage = await client.GetAsync(popularMoviesUrl);
                return await ExtractMoviesFromResponse(responseMessage, genres);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private async Task<List<Movie>> ExtractMoviesFromResponse(HttpResponseMessage responseMessage, List<Genre> genres)
        {

            if (responseMessage.IsSuccessStatusCode)
            {
                var nowPlayingMovies = await responseMessage.Content.ReadAsStringAsync();
                var moviesResponse = JObject.Parse(nowPlayingMovies)["results"].ToObject<List<Movie>>();
                foreach (var movie in moviesResponse)
                {
                    if (movie.GenreIds != null)
                    {
                        movie.Genres = genres.Where(genre => movie.GenreIds.Any(genreId => genreId == genre.Id)).ToList();
                    }
                    movies.Add(movie);
                }
            }

            return movies;
        }

        public async Task<List<Genre>> GetGenres()
        {
            var genresUrl = Constants.BASE_URL + "genre/movie/list?api_key=" + Constants.API_KEY + "&language=en-US";

            try
            {
                var responseMessage = await client.GetAsync(genresUrl);
                var genresResponse = await responseMessage.Content.ReadAsStringAsync();
                return JObject.Parse(genresResponse)["genres"].ToObject<List<Genre>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MovieDetail> GetMovieDetails(int movieId)
        {
            var movieDetailUrl = Constants.BASE_URL + "movie/" + movieId + "?api_key=" + Constants.API_KEY + "&language=en-US";

            try
            {
                var responseMessage = await client.GetAsync(movieDetailUrl);
                var movieResponse = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieDetail>(movieResponse);

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Cast>> GetCastForMovie(int id)
        {
            var movieCastUrl = Constants.BASE_URL + "movie/" + id + "/credits?api_key=" + Constants.API_KEY;

            try
            {
                var responseMessage = await client.GetAsync(movieCastUrl);
                var castResponse = await responseMessage.Content.ReadAsStringAsync();
                return JObject.Parse(castResponse)["cast"].ToObject<List<Cast>>();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MovieImage> GetMovieImages(int id)
        {
            var movieImageUrl = Constants.BASE_URL + "movie/" + id + "/images?api_key=" + Constants.API_KEY + "&language=en-US&include_image_language=en%2Cnull";

            try
            {
                var responseMessage = await client.GetAsync(movieImageUrl);
                var imageResponse = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MovieImage>(imageResponse);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Video>> GetMovieVideos(int id)
        {
            var movieVideoUrl = Constants.BASE_URL + "movie/" + id + "/videos?api_key=" + Constants.API_KEY;

            try
            {
                var responseMessage = await client.GetAsync(movieVideoUrl);
                var videoResponse = await responseMessage.Content.ReadAsStringAsync();
                return JObject.Parse(videoResponse)["results"].ToObject<List<Video>>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
