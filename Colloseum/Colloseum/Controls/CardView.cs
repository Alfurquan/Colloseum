
using Xamarin.Forms;

namespace Colloseum.Controls
{
    public class CardView : Frame
    {
        public CardView()
        {
            Padding = 0;
            BorderColor = Color.LightGray;
            if (Device.RuntimePlatform == Device.iOS)
            {
                HasShadow = false;
                BorderColor = Color.Transparent;
                BackgroundColor = Color.White;
            }
        }
    }
}
