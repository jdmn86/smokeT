using System;
using System.Diagnostics;
using SmokeT.Helpers;
using Xamarin.Forms;

namespace SmokeT
{
    public partial class App : Application
    {
        static CigarDatabseController cigarDatabase;

        public App()
        {
            InitializeComponent();

            //date
            //verifica settings se exitem

            if (Settings.firstTime==true)
            {
                MainPage = new CarPage(true);
            }
            else
            {   //se não abre settings
                MainPage = new CarPage(false);

            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            Settings.timerStop = DateTime.Now.ToString();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            Settings.timerStop = DateTime.Now.ToString();
                //if mesmo dia
                //ir buscar o tempo entre
            /*    int diffDays = (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Days;
                int diffhours = (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Hours;
                int diffminutes = (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Minutes;

                if (diffDays >= 1)
                {
                    diffDays = diffDays * 24;
                }

                Settings.hours = ((diffDays + diffhours) + int.Parse(Settings.hours)).ToString();
                Settings.minutes = (int.Parse(Settings.minutes) + diffminutes).ToString();

                Settings.timerStop = "";*/

        }

        public static CigarDatabseController CigarDatabse
        {
            get{
                if(cigarDatabase==null){
                    cigarDatabase = new CigarDatabseController();
                }
                return cigarDatabase;
            }
        }
    }
}
