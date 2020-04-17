using Colloseum.Models;
using Colloseum.Services;
using Colloseum.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Colloseum.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NowPlayingMoviesPage : ContentPage
    {
              
    
        public NowPlayingMoviesPage()
        {
            InitializeComponent();
            BindingContext = new NowPlayingMoviesViewModel(new PageService());
            lvNowPlaying.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as NowPlayingMoviesViewModel).GetMoviesCommand.Execute(null);
        }

        private void lvNowPlaying_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (BindingContext as MoviesViewModel).SelectMovieCommand.Execute(e.SelectedItem);
        }
    }
}