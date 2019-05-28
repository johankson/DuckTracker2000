using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuckTracker2000
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NotifyButton.Clicked += NotifyButton_Clicked;

            // Add a swipe gesture recognizer for the Notification UI
            var swipeGestureRecognizer = new SwipeGestureRecognizer()
            {
                Direction = SwipeDirection.Down
            };

            swipeGestureRecognizer.Swiped += SwipeGestureRecognizer_Swiped;
            NotificationUI.GestureRecognizers.Add(swipeGestureRecognizer);

            // Add a pan gesture recognizer for the Image
            var panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += (s, e) =>
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        DuckImage.TranslationX = e.TotalX;
                        DuckImage.TranslationY = e.TotalY;
                        break;

                    case GestureStatus.Completed:
                        DuckImage.TranslateTo(0, 0, 250, Easing.CubicOut);
                        break;
                }
            };
           

            DuckImage.GestureRecognizers.Add(panGestureRecognizer);
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await HideNotification();
        }

        private async void NotifyButton_Clicked(object sender, EventArgs e)
        {
            if (NotificationUI.IsVisible == false)
            {
                await ShowNotification("Ducks found!");
            }
            else
            {
                await HideNotification();
            }
        }

        private async Task ShowNotification(string text)
        {
            NotificationText.Text = text;
            NotificationUI.TranslationY = this.Height;
            NotificationUI.IsVisible = true;
            await NotificationUI.TranslateTo(0, 400, 250, Easing.SpringIn);
        }

        private async Task HideNotification()
        {
            NotificationUI.IsVisible = true;
            await NotificationUI.TranslateTo(0, this.Height, 250, Easing.SpringOut);
            NotificationUI.IsVisible = false;
        }
    }
}
