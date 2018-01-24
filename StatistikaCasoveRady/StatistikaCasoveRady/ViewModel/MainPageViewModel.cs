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
using System.ComponentModel;

namespace StatistikaCasoveRady.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Obed> Obedy { get; set; }

        public string GraphHaderA
        {
            get => _graphHaderA; set
            {
                _graphHaderA = value;
                OnPropertyChanged("GraphHaderA");
            }
        }
        public String NejPolevka
        {
            get => _nejPolevka; set
            {
                _nejPolevka = value;
                OnPropertyChanged("NejPolevka");
            }
        }
        public String NejHlavniJidlo
        {
            get => _nejHlavniJidlo; set
            {
                _nejHlavniJidlo = value;
                OnPropertyChanged("NejHlavniJidlo");
            }
        }
        public String NejSalat
        {
            get => _nejSalat; set
            {
                _nejSalat = value;
                OnPropertyChanged("NejSalat");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsBasicLineChart GrafLine
        {
            get => _grafLine; set
            {
                _grafLine = value;
                OnPropertyChanged("GrafLine");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<Obed> ObedyList
        {
            get => _obedyList; set
            {
                _obedyList = value;
                OnPropertyChanged("ObedyList");
            }
        }

        public ICommand ButtonClickCommand { get; }
        public ICommand NacistValstniDataCommand { get; }
        public ICommand NacistDefaultniDataCommand { get; }

        public ChartValues<double> PocetVsechObeduVMesici
        {
            get => _pocetVsechObeduVMesici; set
            {
                _pocetVsechObeduVMesici = value;
                OnPropertyChanged("PocetVsechObeduVMesici");
            }
        }
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection; set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollectionLineChart
        {
            get => _seriesCollectionLineChart; set
            {
                _seriesCollectionLineChart = value;
                OnPropertyChanged("SeriesCollectionLineChart");
            }
        }
        public Func<double, string> YFormatterLineChart { get; set; }

        public MainPageViewModel()
        {
            _obedService = new ObedService();
            Obedy = new ObservableCollection<Obed>();
            NacistValstniDataCommand = new ZCommand(CanNacistVlastniData, NacistVlastniData);
            NacistDefaultniDataCommand = new ZCommand(CanNacistDefaultniData, NacistDefaultniData);
            ButtonClickCommand = new ZCommand(CanOpenWindow, OpenWindow);
            try
            {
                LoadObedy();
                LoadGraphs();
                ActualizeInfoFiles();
            }
            catch (Exception)
            {

            }
        }

        private void NacistDefaultniData(object obj)
        {
            LoadObedy();
            LoadGraphs();
            ActualizeInfoFiles();
        }

        private bool CanNacistDefaultniData(object obj)
        {
            return true;
        }

        private void NacistVlastniData(object obj)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files|*.xlsx;";

            bool? result = dlg.ShowDialog();
            Obedy.Clear();
            if (result == true)
            {
                string filename = dlg.FileName;
                List<Obed> obedy = _obedService.NactiVlastniObedy(filename);
                foreach (var obed in obedy)
                {
                    obed.Druh = UpravObed(obed);
                    if (!string.IsNullOrEmpty(obed.Druh))
                    {
                        Obedy.Add(obed);
                    }
                }
                ObedyList = new List<Obed>(Obedy);
                LoadGraphs();
                ActualizeInfoFiles();
            }
        }

        private bool CanNacistVlastniData(object obj)
        {
            return true;
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

            GrafLine = new GraphsBasicLineChart(lineSeries);
            GrafLine.AddValues(GetHodonty("HlavniJidlo"), "Hlavní jídla");
            GrafLine.AddValues(GetHodonty("Salat"), "Saláty");
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
                    Title = "Počet jídel",
                    Values = PocetVsechObeduVMesici
                }
            };

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
            Obedy.Clear();
            List<Obed> obedy = _obedService.NactiObedy();
            foreach (var obed in obedy)
            {
                obed.Druh = UpravObed(obed);
                if (!string.IsNullOrEmpty(obed.Druh))
                {
                    Obedy.Add(obed);
                }
            }
        }

        private string UpravObed(Obed obed)
        {

            if (obed.Popis == "Pepsi plech 0,33 l")
                return "Nápoj";

            if (obed.Popis == "Voda neochucená 0,5 l")
                return "Nápoj";

            if (obed.Popis == "Pokuta hotovostní platba")
                return "Pokuta";
            if (obed.Popis == "Pokuta hotovostní platba (částečně)")
                return "Pokuta";

            if (obed.Cena > 99 || obed.Cena < 0)
            {
                return null;
            }
            if (obed.Cena > 16)
            {
                return "HlavniJidlo";
            }
            else if (obed.Cena < 16 && obed.Cena > 9)
            {
                if (obed.Popis == "Pepsi plech 0,33 l")
                    return "Nápoj";
                return "Polevka";
            }
            else if (obed.Cena < 10 && obed.Cena > 3)
            {
                return "Salat";
            }
            else if (obed.Cena < 3 && obed.Cena > 0)
            {
                return "Napoj";
            }

            return null;
        }

        private void ActualizeInfoFiles()
        {
            NejPolevka = ObedyList.GroupBy(item => item.Popis).OrderByDescending(g => g.Count(x => x.Druh == "Polevka")).Select(g => g.Key).First();
            NejHlavniJidlo = ObedyList.GroupBy(item => item.Popis).OrderByDescending(g => g.Count(x => x.Druh == "HlavniJidlo")).Select(g => g.Key).First();
            NejSalat = ObedyList.GroupBy(item => item.Popis).OrderByDescending(g => g.Count(x => x.Druh == "Salat")).Select(g => g.Key).First();
            GraphHaderA = $"Graf odběrů jídel v jednotlivých měsících od: {ObedyList.OrderBy(x => x.Datum).First().Datum.ToShortDateString()} do {ObedyList.OrderByDescending(x => x.Datum).First().Datum.ToShortDateString()}";
        }

        private readonly IObedService _obedService;
        private List<Obed> _obedyList;
        private SeriesCollection _seriesCollectionLineChart;
        private ChartValues<double> _pocetVsechObeduVMesici;
        private SeriesCollection _seriesCollection;
        private GraphsBasicLineChart _grafLine;
        private string _nejPolevka;
        private string _nejHlavniJidlo;
        private string _nejSalat;
        private string _graphHaderA;
    }
}
