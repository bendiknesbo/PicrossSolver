using System;
using System.Windows;
using System.Drawing;
using Domain;

namespace PicrossSolver {
    public partial class MainWindow : Window {
        //public Color[,] MyGrid { get; set; }
        public GridPresenter ViewModel;
        private static int _counter = 0;

        public MainWindow() {
            InitializeComponent();
            ViewModel = new GridPresenter();
            DataContext = ViewModel;
        }

        private void LoadDemo_OnClick(object sender, RoutedEventArgs e) {
            ViewModel.LoadDemoData();
        }
    }
}