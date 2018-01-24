using LiveCharts;
using StatistikaCasoveRady.WindowModel;
using System.Windows;

namespace StatistikaCasoveRady.WindowView
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(ChartValues<double> values)
        {
            InitializeComponent();
            DataContext = new GraphWindowModel(values);
        }
    }
}
