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
            _viewModel.ListeningPort = int.Parse(ListeningPortInput.Text);
            _viewModel.TargetPort = int.Parse(TargetPortInput.Text);
            _viewModel.InitializeUDPConnection(); // Inicjacja połączenia z nowymi portami
        }


    }
}
