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
    public class UpcomingMoviesViewModel : MoviesViewModel
    {
        private List<Movie> _upcomingMovies;
        private bool IsDataLoaded;
        private ApiService apiService;
        public ICommand GetMoviesCommand { get; set; }

        public List<Movie> UpcomingMovies
        {
            get
            {
                return _upcomingMovies;
            }
            set
            {
                SetValue(ref _upcomingMovies, value);
            }
        }

        public UpcomingMoviesViewModel(IPageService pageService) : base(pageService)
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
                    UpcomingMovies = await apiService.GetUpcomingMovies();
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
