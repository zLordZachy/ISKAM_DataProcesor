

using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace StatistikaCasoveRady.WindowModel
{
    public class GraphWindowModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public GraphWindowModel(ChartValues<double> values)
        {
            // InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = values //; new ChartValues<double> { 5, 10, 2, 4,8,9,3,22,1,1,33 }
                }
            };

           
            Labels = new[] { "Leden", "Unor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen","Listopad" , "Prosinec" };
            Formatter = value => value.ToString("N");

        }

    }
}
