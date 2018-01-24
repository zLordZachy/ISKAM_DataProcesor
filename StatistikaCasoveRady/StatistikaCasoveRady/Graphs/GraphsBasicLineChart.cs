using LiveCharts;
using LiveCharts.Wpf;
using System;

namespace StatistikaCasoveRady.Graphs
{
    public class GraphsBasicLineChart 
    {
        public GraphsBasicLineChart(LineSeries lineSeries)
        {
            SeriesCollection = new SeriesCollection();

            Labels = new[] { "Leden", "Unor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
            YFormatter = value => value.ToString();

            SeriesCollection.Add(lineSeries);
            SeriesCollection[0].Values.Add(5d);
        }

        public void AddValues(ChartValues<double> hodnoty, string Title)
        {
            SeriesCollection.Add(new LineSeries
            {
                Title = Title,
                Values = hodnoty,

            });
            SeriesCollection[SeriesCollection.Count - 1].Values.Add(5d);
        }

        public virtual SeriesCollection SeriesCollection { get; set; }
        public virtual string[] Labels { get; set; }
        public virtual Func<double, string> YFormatter { get; set; }
    }
}
