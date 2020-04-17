using Colloseum.Services;
using Colloseum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colloseum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopularMoviesPage : ContentPage
    {
        public PopularMoviesPage()
        {
            InitializeComponent();
            BindingContext = new PopularMoviesViewModel(new PageService());
            popularMovies.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as PopularMoviesViewModel).GetMoviesCommand.Execute(null);
        }

        private void popularMovies_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (BindingContext as PopularMoviesViewModel).SelectMovieCommand.Execute(e.SelectedItem);
        }
    }
}