using System;

namespace komunikatorUDP.Models
{
    /// <summary>
    /// Reprezentuje pojedynczą wiadomość w komunikatorze.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Zawartość wiadomości w postaci tekstu.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Data i czas, kiedy wiadomość została wysłana lub odebrana.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Określa, kto wysłał wiadomość - użytkownik aplikacji ("Ja:") lub zdalny nadawca ("Nadawca:").
        /// </summary>
        public string IsMyMessage { get; set; }
    }
}
