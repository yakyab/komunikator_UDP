using System.Windows;
using komunikatorUDP.ViewModels;

namespace komunikatorUDP.Views
{
    /// <summary>
    /// Główne okno aplikacji komunikatora.
    /// Obsługuje interakcje użytkownika z interfejsem oraz komunikację z ViewModel.
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;

            // Rejestruje zdarzenie dla błędów zgłaszanych przez ViewModel
            _viewModel.ErrorOccurred += (sender, errorMessage) =>
            {
                MessageBox.Show(errorMessage, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku "Wyślij". Wysyła wiadomość przez ViewModel.
        /// </summary>
        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SendMessage(MessageInput.Text);
            MessageInput.Text = string.Empty;
        }

        /// <summary>
        /// Inicjalizuje połączenie UDP na podstawie wprowadzonych portów.
        /// </summary>
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
            _viewModel.InitializeUDPConnection();
        }
    }
}
