using System;
using System.Collections.Generic;
using System.Diagnostics;
using SmokeT.Helpers;
using Xamarin.Forms;

namespace SmokeT
{
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();

            if(!string.IsNullOrEmpty(Settings.costByPocket) && 
               !string.IsNullOrEmpty(Settings.ciggarsPerDay) && 
               !string.IsNullOrEmpty(Settings.ciggarsPerPocket)){
                cost.Text = Settings.costByPocket;
                perDay.Text = Settings.ciggarsPerDay;
                perPocket.Text = Settings.ciggarsPerPocket;
            }          
        }

        private async void GoBack(object sender, EventArgs e)
        {

            if(Settings.firstTime){
                Settings.Start(cost.Text,perDay.Text,perPocket.Text);    
            }else{
                Settings.ChangeSettings(cost.Text, perDay.Text, perPocket.Text);
            }


            if (!string.IsNullOrEmpty(Settings.costByPocket) && 
                !string.IsNullOrEmpty(Settings.ciggarsPerDay) && 
                !string.IsNullOrEmpty(Settings.ciggarsPerPocket))
            {
               // Application.Current.MainPage = new SmokeTPage();
                await Navigation.PopModalAsync();
            }else{
                await DisplayAlert("Settings Not Define", "Please Insert settings", "OK");
            }
        }


    }
}
