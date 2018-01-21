using StatistikaCasoveRady.View;
using System.Windows;

namespace StatistikaCasoveRady
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new MainPageView();
        }
    }
}
