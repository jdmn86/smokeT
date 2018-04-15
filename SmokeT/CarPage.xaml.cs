using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmokeT
{
    public partial class CarPage : CarouselPage
    {
        public CarPage(bool firstTime)
        {
            InitializeComponent();

            if (firstTime == true)
            {
                OpenSettingsFirstTimeAsync();
            }
        }

        public async void OpenSettingsFirstTimeAsync()
        {
            var settingsFirstTime = new SettingsView();
            await Navigation.PushModalAsync(new NavigationPage(settingsFirstTime));
        }
    }
}
