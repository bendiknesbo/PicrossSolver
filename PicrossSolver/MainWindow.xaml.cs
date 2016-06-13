using System.Windows;
using Domain.GuiWrappers;

namespace PicrossSolver {
    public partial class MainWindow : Window {
        public GridPresenter ViewModel;

        public MainWindow() {
            InitializeComponent();
            ViewModel = new GridPresenter();
            DataContext = ViewModel;
        }

        private void LoadDemo_OnClick(object sender, RoutedEventArgs e) {
            ViewModel.LoadDemoData();
        }

        private void LoadSpecific_OnClick(object sender, RoutedEventArgs e) {
            ViewModel.LoadSpecificData();
        }

        private void ShowAllClassifiers_OnClick(object sender, RoutedEventArgs e) {
            CellPresenter.ShowAllClassifiers = !CellPresenter.ShowAllClassifiers;
            ViewModel.NotifyClassifiers();
        }
    }
}