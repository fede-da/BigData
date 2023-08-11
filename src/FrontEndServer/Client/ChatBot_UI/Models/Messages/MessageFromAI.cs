namespace FrontEndServer.Client.ChatBot_UI.Models.Messages
{
    // Text color black and background color white
    public class MessageFromAI : Message
    {
        public MessageFromAI(string message, string sender, DateTime sentOnDate) : base(message, sender, sentOnDate)
        {
            this._backgroundColor = "#FFFFFF";
            this._textColor = "#000000";
        }
    }
}
