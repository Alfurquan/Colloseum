using Colloseum.Interfaces;
using Colloseum.Models;
using Colloseum.Services;
using Colloseum.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Colloseum.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private IApiService _apiService;
        private IPageService _pageService;
        private bool IsDataLoaded;
        public ICommand GetMovieDetailsCommand { get; private set; }
        public ICommand ShowFullCastCommand { get; private set; }
        public ICommand ShowPostersPageCommand { get; private set; }

        public ICommand ShowBackdropsPageCommand { get; private set; }
        public ICommand ShowVideoPageCommand { get; private set; }

        private MovieDetail _movie;
        public MovieDetail SelectedMovie
        {
            get
            {
                return _movie;
            }
            set
            {
                SetValue(ref _movie, value);
            }
        }

        private List<Cast> _casts;
        private List<Cast> _topCasts;
        public List<Cast> Casts
        {
            get
            {
                return _casts;
            }
            set
            {
                SetValue(ref _casts, value);
            }
        }
        public List<Cast> TopCasts
        {
            get
            {
                return _topCasts;
            }
            set
            {
                SetValue(ref _topCasts, value);
            }
        }
        private MovieImage _movieImage;
        public MovieImage MovieImage
        {
            get
            {
                return _movieImage;
            }
            set
            {
                SetValue(ref _movieImage, value);
            }
        }
        private List<Video> _videos;
        public List<Video> Videos
        {
            get
            {
                return _videos;
            }
            set
            {
                SetValue(ref _videos, value);
            }
        }
        private Video _selectedVideo;
        public Video SelectedVideo
        {
            get
            {
                return _selectedVideo;
            }
            set
            {
                SetValue(ref _selectedVideo, value);
            }
        }
        public MovieDetailsViewModel(IPageService pageService)
        {
            _apiService = new ApiService();
            _pageService = pageService;
            IsDataLoaded = false;
            GetMovieDetailsCommand = new Command<int>(async vm => await GetMovieDetails(vm));
            ShowFullCastCommand = new Command(async () => await OpenCastPage());
            ShowPostersPageCommand = new Command(async () => await OpenPostersPage());
            ShowBackdropsPageCommand = new Command(async () => await OpenBackdropsPage());
            ShowVideoPageCommand = new Command(async () => await OpenVideoPage());
        }

       
        private async Task OpenCastPage()
        {
             await _pageService.PushModalAsync(new NavigationPage(new CastPage(this)));
        }

        private async Task OpenBackdropsPage()
        {
            await _pageService.PushModalAsync(new NavigationPage(new BackdropPage(this)));
        }

        private async Task OpenPostersPage()
        {
            await _pageService.PushModalAsync(new NavigationPage(new PostersPage(this)));
        }

        private async Task OpenVideoPage()
        {
            if(SelectedVideo != null)
            {
                await _pageService.PushModalAsync(new NavigationPage(new VideoPage(SelectedVideo)));
                SelectedVideo = null;
            }
        }

        private async Task GetMovieDetails(int movieId)
        {
            if (!IsDataLoaded)
            {
                try
                {
                    IsBusy = true;
                    SelectedMovie = await _apiService.GetMovieDetails(movieId);
                    Casts = await _apiService.GetCastForMovie(movieId);
                    MovieImage = await _apiService.GetMovieImages(movieId);
                    TopCasts = Casts.GetRange(0,4);
                    Videos = await _apiService.GetMovieVideos(movieId);
                    IsDataLoaded = true;
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("Execption movie details", ex);
                    await _pageService.DisplayAlert("Error", "Failed to load movie details", "OK", "Cancel");
                }
                finally
                {
                    IsBusy = false;
                }
            }
           
        }
    }
}
