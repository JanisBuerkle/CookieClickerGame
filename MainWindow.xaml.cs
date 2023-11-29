using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPG
{
    public partial class MainWindow : Window
    {
        private double Cookies { get; set; }
        public double Multiplikator { get; set; }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel), typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(default(MainWindowViewModel)));

        public MainWindowViewModel ViewModel
        {
            get => (MainWindowViewModel)GetValue(ViewModelProperty); 
            set => SetValue(ViewModelProperty, value); 
        }
        
        public MainWindow()
        {
            Multiplikator = 1.0;
            ViewModel = new MainWindowViewModel();
            InitializeComponent();
        }

        private void CookieClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.Content = $"Cookies: {Cookies:F1}";
            Cookies += Multiplikator;
        }

        private void Times01(object sender, RoutedEventArgs e)
        {
            Multiplikator += 0.1;
        }
    }
}