using Colloseum.Services;
using Colloseum.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colloseum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingMoviesPage : ContentPage
    {
        public UpcomingMoviesPage()
        {
            InitializeComponent();
            BindingContext = new UpcomingMoviesViewModel(new PageService());
            upcomingMovies.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as UpcomingMoviesViewModel).GetMoviesCommand.Execute(null);
        }

        private void upNowPlaying_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (BindingContext as UpcomingMoviesViewModel).SelectMovieCommand.Execute(e.SelectedItem);
        }
    }
}