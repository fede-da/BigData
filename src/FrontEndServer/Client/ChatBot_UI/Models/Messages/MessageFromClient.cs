namespace FrontEndServer.Client.ChatBot_UI.Models.Messages
{
    // Text color white and background color black
    public class MessageFromClient : Message
    {
        public MessageFromClient(string message, string sender, DateTime sentOnDate) : base(message, sender, sentOnDate)
        {
            this._backgroundColor = "#000000";
            this._textColor = "#FFFFFF";
        }
    }
}
