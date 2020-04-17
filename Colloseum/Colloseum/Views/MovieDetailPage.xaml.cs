using Colloseum.Models;
using Colloseum.Services;
using Colloseum.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colloseum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {
        private Movie _movie;
        public MovieDetailPage(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException();

            _movie = movie;
            InitializeComponent();
            BindingContext = new MovieDetailsViewModel(new PageService());
        }
        protected override void OnAppearing()
        {
            (BindingContext as MovieDetailsViewModel).GetMovieDetailsCommand.Execute(_movie.Id);
            base.OnAppearing();
        }

    }
}