using System.Drawing;

namespace FrontEndServer.Client.ChatBot_UI.Models.Messages
{
    public abstract class Message
    {
        internal string _message, _sender;
        public string  _backgroundColor, _textColor;
        internal DateTime _sentOnDate;

        public Message(string message, string sender, DateTime sentOnDate)
        {
            _message = message;
            _sender = sender;
            _sentOnDate = sentOnDate;
        }

        public void SetMessage(string message)
        {
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }
        public string GetSender() => _sender;

        public void SetSender(string sender)
        {
            _sender = sender;
            return;
        }

    }
}
