using komunikatorUDP.Models;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Net.Sockets;

namespace komunikatorUDP.ViewModels
{
    /// <summary>
    /// ViewModel dla głównego okna komunikatora.
    /// Zarządza logiką komunikacji UDP i operacjami na wiadomościach.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Message> _messages;
        private UdpClient _udpClient;
        private bool _continueListening = true;

        /// <summary>
        /// Adres IP odbiorcy.
        /// </summary>
        public string TargetIP { get; set; }

        /// <summary>
        /// Port, na którym aplikacja będzie nasłuchiwać wiadomości.
        /// </summary>
        public int ListeningPort { get; set; }

        /// <summary>
        /// Port, na który będą wysyłane wiadomości.
        /// </summary>
        public int TargetPort { get; set; }

        /// <summary>
        /// Zdarzenie informujące o błędzie.
        /// </summary>
        public event EventHandler<string> ErrorOccurred;

        /// <summary>
        /// Kolekcja wiadomości wyświetlanych w interfejsie użytkownika.
        /// </summary>
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<Message>();
        }

        /// <summary>
        /// Inicjuje połączenie UDP na podanym porcie nasłuchiwania.
        /// </summary>
        public void InitializeUDPConnection()
        {
            if (_udpClient != null)
            {
                _continueListening = false;
                _udpClient.Close();
                _udpClient = null;
            }

            try
            {
                _udpClient = new UdpClient(ListeningPort);
                _continueListening = true;
                StartListening();
            }
            catch (SocketException)
            {
                ErrorOccurred?.Invoke(this, "Nie można nasłuchiwać na wskazanym porcie, ponieważ jest on już w użyciu.");
            }
        }

        /// <summary>
        /// Wysyła wiadomość na podany port docelowy.
        /// </summary>
        /// <param name="messageContent">Treść wiadomości do wysłania.</param>
        public void SendMessage(string messageContent)
        {
            if (string.IsNullOrEmpty(messageContent)) return;

            if (_udpClient == null)
            {
                ErrorOccurred?.Invoke(this, "Najpierw zatwierdź porty i nawiąż połączenie.");
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(messageContent);
            try
            {
                _udpClient.Send(data, data.Length, TargetIP, TargetPort);
            }
            catch (SocketException ex)
            {
                ErrorOccurred?.Invoke(this, $"Błąd przy wysyłaniu wiadomości: {ex.Message}");
                return;
            }

            Messages.Add(new Message
            {
                Content = messageContent,
                Timestamp = DateTime.Now,
                IsMyMessage = "Ja:"
            });
        }

        /// <summary>
        /// Nasłuchiwanie na wiadomości przychodzące na wskazanym porcie.
        /// </summary>
        public async void StartListening()
        {
            while (_continueListening)
            {
                try
                {
                    var result = await _udpClient.ReceiveAsync();
                    string receivedMessage = Encoding.UTF8.GetString(result.Buffer);

                    Messages.Add(new Message
                    {
                        Content = receivedMessage,
                        Timestamp = DateTime.Now,
                        IsMyMessage = "Nadawca:"
                    });
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        // Implementacja interfejsu INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}





