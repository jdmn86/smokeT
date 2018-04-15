using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microcharts;
using Microcharts.Forms;
using ProgressRingControl.Forms.Plugin;
using SkiaSharp;
using SmokeT.Models;
using Xamarin.Forms;

namespace SmokeT
{
    public partial class Stat : ContentPage
    {
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
        List<Cigar> lista = new List<Cigar>();
        List<string> listaCores = new List<string>();
        List<KeyValuePair<string, int>> listMeses = new List<KeyValuePair<string, int>>() {
            new KeyValuePair<string, int>("Janeiro", 1),
            new KeyValuePair<string, int>("Fevereiro", 2),
            new KeyValuePair<string, int>("Março", 3),
            new KeyValuePair<string, int>("Abril", 4),
            new KeyValuePair<string, int>("Maio", 5),
            new KeyValuePair<string, int>("Junho", 6),
            new KeyValuePair<string, int>("Julho", 7),
            new KeyValuePair<string, int>("Agosto", 8),
            new KeyValuePair<string, int>("Setembro", 9),
            new KeyValuePair<string, int>("Outubro", 10),
            new KeyValuePair<string, int>("novembro", 11),
            new KeyValuePair<string, int>("Dezembro", 12),
        };

        public Stat()
        {
            InitializeComponent();

            listaCores.Add("#966842");
            listaCores.Add("#f44747");
            listaCores.Add("#eedc31");
            listaCores.Add("#7fdb6a");
            listaCores.Add("#0e68ce");

           

            StartListStats();

        }


        public  void StartListStats()
        {
            if(lista!=null){
                lista.Clear();
            }
            lista = App.CigarDatabse.GetCigarsByMonth();

            if (lista != null)
            {
                foreach (var item in lista)
                {
                    Debug.WriteLine("list entries" + item.ToString());
                    Debug.WriteLine("list entries year: " + item.year.ToString() + " month: " + item.month.ToString() + " day:" + item.day.ToString() + "cost" + item.cost.ToString());

                }
            }
            //mete numa Entry diferente
            var lst = App.CigarDatabse.GetCigarsByMonth();
            //preciso de list of entry


            int itemCor = 0;

                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                    entries.Add(new Microcharts.Entry((float)item.cost)
                    {
                        Label = listMeses.First(x => x.Value == int.Parse(item.month.ToString())).Key,
                        ValueLabel = item.cost.ToString(),
                        Color = SKColor.Parse(listaCores.ElementAt(itemCor))
                        });
                    itemCor += 1;
                    }

           

                chartMouth.Chart = new PointChart() { Entries = entries };
                chartMouth2.Chart = new BarChart() { Entries = entries };

            }

        }

        public void StartRefreshThread()
        {

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 1000), refreshGraph);

        }

        private bool refreshGraph()
        {
            if(lista.Count!=App.CigarDatabse.GetCigarsByMonth().Count()){
                lista.Clear();
                StartListStats();
                chartMouth.Chart = new PointChart() { Entries = entries };
                chartMouth2.Chart = new BarChart() { Entries = entries };
            }
            return true;
        }

    }
}
