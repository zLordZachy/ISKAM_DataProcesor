using LiveCharts;
using LiveCharts.Wpf;
using System;

namespace StatistikaCasoveRady.Graphs
{

    public class GraphsBasicLineChart 
    {
        private SeriesCollection _seriesCollection;
        private string[] _labels;
        private Func<double, string> _yFormatter;

        public GraphsBasicLineChart(LineSeries lineSeries)
        {
            SeriesCollection = new SeriesCollection();

            Labels = new[] { "Leden", "Unor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
            YFormatter = value => value.ToString("C");

            SeriesCollection.Add(lineSeries);
            SeriesCollection[0].Values.Add(5d);

        }

        public GraphsBasicLineChart()
        {
        }

        public event Action PointChanged;

        protected void OnPointChanged()
        {
            if (PointChanged != null) PointChanged.Invoke();
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

        public virtual SeriesCollection SeriesCollection
        {
            get => _seriesCollection; set
            {
                _seriesCollection = value;
                OnPointChanged();
            }
        }
        public virtual string[] Labels
        {
            get => _labels; set
            {
                _labels = value;
                OnPointChanged();
            }
        }
        public virtual Func<double, string> YFormatter
        {
            get => _yFormatter; set
            {
                _yFormatter = value;
                OnPointChanged();
            }
        }

    }
}
