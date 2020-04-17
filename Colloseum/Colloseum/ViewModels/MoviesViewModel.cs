using Colloseum.Interfaces;
using Colloseum.Models;
using Colloseum.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Colloseum.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {

        private readonly IPageService _pageService;

        public ICommand SelectMovieCommand { get; set; }

        public MoviesViewModel(IPageService pageService)
        {
            _pageService = pageService;
            SelectMovieCommand = new Command<Movie>(async vm => await SelectMovie(vm));
        }

        public async Task SelectMovie(Movie movie)
        {
            if (movie != null)
            {
                await _pageService.PushAsync(new MovieDetailPage(movie));
            }
        }

       

        protected async void HandleError(Exception ex)
        {
            Debug.WriteLine("Exception", ex);
            await _pageService.DisplayAlert("Error", "Failed to retreive movies!", "OK", "Cancel");
        }
    }
}
