using StatistikaCasoveRady.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace StatistikaCasoveRady.View
{
    /// <summary>
    /// Interakční logika pro mainPageView.xaml
    /// </summary>
    public partial class MainPageView 
    {
        public MainPageView()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }
    }
}
