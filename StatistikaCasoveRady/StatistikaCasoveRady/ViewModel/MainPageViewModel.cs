using LiveCharts;
using StatistikaCasoveRady.Services;
using StatistikaCasoveRady.WindowView;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using LiveCharts.Wpf;
using StatistikaCasoveRady.Graphs;

namespace StatistikaCasoveRady.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Obed> Obedy { get; set; }
        public List<Obed> ObedyList { get; set; }
        private readonly IObedService _obedService;
        public ICommand ButtonClickCommand { get; }
        public ChartValues<double> PocetVsechObeduVMesici { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels{ get; set; }
    public Func<double, string> Formatter { get; set; }


        public SeriesCollection SeriesCollectionLineChart { get; set; }
        public Func<double, string> YFormatterLineChart { get; set; }

        public MainPageViewModel()
        {
            _obedService = new ObedService();
            Obedy = new ObservableCollection<Obed>();
            ButtonClickCommand = new ZCommand(CanOpenWindow, OpenWindow);
            LoadObedy();
            LoadGraphs();
        }

        private void LoadGraphs()
        {
            Labels = new[] { "Leden", "Unor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
            LoadValuesPerYear();
            LoadBasicCulomnGraph();
            LoadLineChartGraph();
            
        }

        private void LoadLineChartGraph()
        {
           
            LineSeries lineSeries = new LineSeries
            {
                Title = "Polevky",
                Values = GetHodonty("Polevka")
            };
            
            GraphsBasicLineChart graf = new GraphsBasicLineChart(lineSeries);

            graf.AddValues(GetHodonty("HlavniJidlo"),"Hlavní jídla");

            graf.AddValues(GetHodonty("Salat"), "Saláty");
            
            SeriesCollectionLineChart = graf.SeriesCollection;
        }

        private ChartValues<double> GetHodonty(string DruhJidla)
        {
            ChartValues<double> hodnoty = new ChartValues<double>();
            for (int i = 1; i < 13; i++)
            {
                hodnoty.Add(ObedyList.Where(x => x.Datum.Month == i && x.Druh == DruhJidla).ToList().Count);
            }

            return hodnoty;
        }

        private void LoadBasicCulomnGraph()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = PocetVsechObeduVMesici
                }
            };


            Labels = new[] { "Leden", "Unor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
            Formatter = value => value.ToString("N");
        }

        private bool CanOpenWindow(object parametr)
        {
           return true;
        }

        private void OpenWindow(object parametr)
        {
            LoadValuesPerYear();
            GraphWindow win = new GraphWindow(PocetVsechObeduVMesici);
            win.Show();
        }

        private void LoadValuesPerYear()
        {
            PocetVsechObeduVMesici = new ChartValues<double>();
            ObedyList = new List<Obed>(Obedy);
            for (int i = 1; i < 13; i++)
            {
                PocetVsechObeduVMesici.Add(ObedyList.Where(x => x.Datum.Month == i).ToList().Count);
            }
        }

        private void LoadObedy()
        {
            List<Obed> obedy = _obedService.NactiObedy();
            foreach (var obed in obedy)
            {
                if (obed.Cena > 99 || obed.Cena < 0)
                {
                    continue;
                }
                if (obed.Cena > 16)
                {
                    obed.Druh = "HlavniJidlo";
                }
                else if (obed.Cena < 16 && obed.Cena > 9)
                {
                    obed.Druh = "Polevka";
                } 
                else if(obed.Cena < 10 && obed.Cena > 3)
                {
                    obed.Druh = "Salat";
                }
                else if (obed.Cena < 3 && obed.Cena > 0)
                {
                    obed.Druh = "Napoj";
                }
                Obedy.Add(obed);
            }
        }
    }
}
