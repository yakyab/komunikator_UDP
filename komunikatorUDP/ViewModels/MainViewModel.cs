using komunikatorUDP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace komunikatorUDP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Message> _messages;
        private UdpClient _udpClient;
        public int ListeningPort { get; set; }
        public int TargetPort { get; set; }

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

        public void InitializeUDPConnection()
        {
            if (_udpClient != null)
            {
                _udpClient.Close();
                _udpClient = null; 
            }

            _udpClient = new UdpClient(ListeningPort);
            StartListening();
        }

        public void SendMessage(string messageContent)
        {
            if (string.IsNullOrEmpty(messageContent)) return;

            if (_udpClient == null)
            {
                MessageBox.Show("Najpierw zatwierdź porty i nawiąż połączenie.", "Brak połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(messageContent);
            _udpClient.Send(data, data.Length, "127.0.0.1", TargetPort);

            Messages.Add(new Message
            {
                Content = messageContent,
                Timestamp = DateTime.Now,
                IsMyMessage = "Ja:"
            });
        }

        public async void StartListening()
        {
            while (true)
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
        }

        // Implementacja interfejsu INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}






