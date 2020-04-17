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
    public partial class CastPage : ContentPage
    {
        public CastPage(MovieDetailsViewModel movieDetailsViewModel)
        {
            InitializeComponent();
            BindingContext = movieDetailsViewModel;
        }
    }
}