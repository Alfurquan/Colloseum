using Colloseum.Interfaces;
using Colloseum.Models;
using Colloseum.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Colloseum.ViewModels
{
    public class PopularMoviesViewModel : MoviesViewModel
    {
        private List<Movie> _popularMovies;
        private bool IsDataLoaded;
        private ApiService apiService;
        public ICommand GetMoviesCommand { get; set; }

        public List<Movie> PopularMovies
        {
            get
            {
                return _popularMovies;
            }
            set
            {
                SetValue(ref _popularMovies, value);
            }
        }

        public PopularMoviesViewModel(IPageService pageService) : base(pageService)
        {
            IsDataLoaded = false;
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
                    PopularMovies = await apiService.GetPopularMovies();
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
