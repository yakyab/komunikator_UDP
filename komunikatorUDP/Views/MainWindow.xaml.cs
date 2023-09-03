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
using komunikatorUDP.ViewModels;

namespace komunikatorUDP.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.SendMessage(MessageInput.Text);
            MessageInput.Text = string.Empty; // Czyszczenie pola po wysłaniu
        }

        private void InitializeUDPConnection(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ListeningPortInput.Text) || string.IsNullOrEmpty(TargetPortInput.Text))
            {
                MessageBox.Show("Proszę wypełnić oba pola portów.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(ListeningPortInput.Text, out int listeningPort) || !int.TryParse(TargetPortInput.Text, out int targetPort))
            {
                MessageBox.Show("Proszę wprowadzić prawidłowe numery portów.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.ListeningPort = listeningPort;
            _viewModel.TargetPort = targetPort;
            _viewModel.InitializeUDPConnection(); // Inicjacja połączenia z nowymi portami
        }


    }
}
