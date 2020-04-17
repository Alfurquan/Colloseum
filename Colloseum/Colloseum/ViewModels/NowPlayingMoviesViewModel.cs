using Colloseum.Interfaces;
using Colloseum.Models;
using Colloseum.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Colloseum.ViewModels
{
    public class NowPlayingMoviesViewModel : MoviesViewModel
    {
        private List<Movie> _nowPlayingMovies;
        private bool IsDataLoaded;
        private ApiService apiService;
        public ICommand GetMoviesCommand { get; set; }

        public List<Movie> NowPlayingMovies
        {
            get
            {
                return _nowPlayingMovies;
            }
            set
            {
                SetValue(ref _nowPlayingMovies, value);
            }
        }

        public NowPlayingMoviesViewModel(IPageService pageService) : base(pageService)
        {
            IsDataLoaded = false;
            NowPlayingMovies = new List<Movie>();
            apiService = new ApiService();
            GetMoviesCommand = new Command(async () => await GetMovies());
        }

        public async Task GetMovies()
        {
            if (!IsDataLoaded)
            {
                try
                {
                    IsBusy = true;
                    NowPlayingMovies = await apiService.GetNowPlayingMovies();
                    IsDataLoaded = true;

                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    HandleError(ex);

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

    }
}
