using System;
using System.Collections.Generic;
using System.Text;

namespace komunikatorUDP.Models
{
    public class Message
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string IsMyMessage { get; set; } 
    }
}
