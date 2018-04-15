using System.Collections.Generic;
using SkiaSharp;
using Xamarin.Forms;
using Microcharts;
using ProgressRingControl.Forms.Plugin;
using System;
using System.Diagnostics;
using SmokeT.Helpers;
using System.Threading.Tasks;
using SmokeT.Models;
using System.Globalization;

namespace SmokeT
{
    public partial class AddPage : ContentPage
    {
        public bool stopTimer { get; private set; }
        public bool timer { get; private set; }
        public int hora { get; private set; }
        public int minutes { get; private set; }

        public AddPage()
        {
            InitializeComponent();
            var progressRing = new ProgressRing { RingThickness = 15, Progress = 0.5f};
            //dia mudou
            if(Settings.firstTime==false && !DateTime.Now.Day.Equals(DateTime.Parse(Settings.dateToday).Day) && !DateTime.Now.Month.Equals(DateTime.Parse(Settings.dateToday).Month)){
                ChangeDay();
            }
            timer = false;
            if(!string.IsNullOrEmpty(Settings.timerStop)){
                RefreshTimerOnResume();
            }
            ActualizarLayout();
        }

        private void RefreshTimerOnResume()
        {
            //if mesmo dia
            //ir buscar o tempo entre
            int diffDays= (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Days;
            int diffhours = (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Hours;
            int diffminutes = (DateTime.Now - DateTime.Parse(Settings.timerStop.ToString())).Minutes;

            if(diffDays>=1){
                diffDays =diffDays * 24;
            }

            Settings.hours=((diffDays+diffhours)+int.Parse(Settings.hours)).ToString();
            Settings.minutes=(int.Parse(Settings.minutes)+diffminutes).ToString();

            Settings.timerStop = "";

            ActualizarTodayCiggars();

            StartTimer(false);
        }

        private void ChangeDay()
        {
            Settings.ChangeToday();
            Debug.WriteLine("fez change day");
        }

        private async void clickSettings(object sender, EventArgs e)
        {         
            var settingsDetail = new SettingsView();
            await Navigation.PushModalAsync(new NavigationPage(settingsDetail));

        }

        public void ActualizarLayout()
        {
            
            ActualizarProgressoGraph();
            ActualizarTodayCiggars();

   /*         if(Settings.firstTime==false) {
                Cigar cigar = new Cigar();
                cigar.year = DateTime.Now.Year;
                cigar.month = DateTime.Now.Month;
                cigar.day = 14;
                cigar.cost = (double.Parse(Settings.costByPocket) / double.Parse(Settings.ciggarsPerPocket));

                App.CigarDatabse.SaveCigar(cigar);

                App.CigarDatabse.SaveCigar(cigar);

                App.CigarDatabse.SaveCigar(cigar);
            }
           */

        }

        private void ActualizarTodayCiggars(){
            myToday.Text = Settings.ciggarsToday.ToString();
         //   Debug.WriteLine("Today cigars: " + Settings.ciggarsToday);
        }

        private void ActualizarProgressoGraph(){
            // actualizar grafico
            double pro = 0;
            if (!string.IsNullOrEmpty(Settings.ciggarsPerDay) &&
               !string.IsNullOrEmpty(Settings.ciggarsToday))
            {
                if (Convert.ToInt32(Settings.ciggarsToday) <= Convert.ToInt32(Settings.ciggarsPerDay))
                {
                    pro = Convert.ToDouble(Settings.ciggarsToday) / Convert.ToDouble(Settings.ciggarsPerDay);

                }
                else
                {
                    pro = 1f;
                }
            }
            myGraph.Progress = pro;

            if (pro < 0.4)
            {
                myGraph.RingProgressColor = Color.Green;
            }
            else if (pro < 0.8)
            {
                myGraph.RingProgressColor = Color.Yellow;
            }
            else
            {
                myGraph.RingProgressColor = Color.Red;
            }
        }

        private async void AddCigarAsync(object sender, EventArgs e)
        {

            Settings.ciggarsToday = Convert.ToString(Convert.ToInt32(Settings.ciggarsToday) + 1);
                      //  Debug.WriteLine("1");
            //save database
            Cigar cigar = new Cigar();
            cigar.year = DateTime.Now.Year;
            cigar.month = DateTime.Now.Month;
            cigar.day = DateTime.Now.Day;
            cigar.cost =  (double.Parse(Settings.costByPocket, CultureInfo.InvariantCulture)  / double.Parse(Settings.ciggarsPerPocket, CultureInfo.InvariantCulture));
                      //  Debug.WriteLine("2");
            App.CigarDatabse.SaveCigar(cigar);
                      //    Debug.WriteLine("3");
            ActualizarLayout();
                    //        Debug.WriteLine("4");
            if(timer==true){
                hora = 0;
                minutes = 0;
               // stopTimer = false;
                //timer = false;
               // StartTimer();
            }else{
                StartTimer();
            }
        //    Debug.WriteLine("after add cigar number of cigars:" + Settings.ciggarsToday);
        }

        public void StartTimer(){
            timer = true;
            myChrono.Text="00:00";
            hora = 0;
            minutes = 0;
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 1000), UpdateTimer);

        }

        public void StartTimer(bool fromStop)
        {
            timer = true;
            hora = int.Parse(Settings.hours);
            minutes = int.Parse(Settings.minutes);
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 1000), UpdateTimer);

        }

        private bool UpdateTimer()
        {
            if(stopTimer==true){
                return false;
            }

            if(minutes==60){
                hora = hora + 1;
                minutes = 0;
            }else{
                minutes = minutes + 1;
            }

            Handletimer(hora,minutes);

            return true;
        }

        public void Handletimer(int hora,int minutes){
            if (hora < 10)
            {

                if (minutes < 10)
                {
                    myChrono.Text = "0" + hora.ToString() + ":0" + minutes.ToString();
                }
                else
                {
                    myChrono.Text = "0" + hora.ToString() + ":" + minutes.ToString();
                }

            }
            else
            {
                if (minutes < 10)
                {
                    myChrono.Text = hora.ToString() + ":0" + minutes.ToString();
                }
                else
                {
                    myChrono.Text = hora.ToString() + ":" + minutes.ToString();
                }
            }

            Settings.hours = hora.ToString();
            Settings.minutes = minutes.ToString();
        }
    }
}
