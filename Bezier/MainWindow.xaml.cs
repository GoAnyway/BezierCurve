using Bezier.Models;
using System.Windows;

namespace Bezier
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
        }
    }
}