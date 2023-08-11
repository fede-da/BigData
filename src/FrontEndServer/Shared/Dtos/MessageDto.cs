using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndServer.Shared.Dtos
{
    public class MessageDTO
    {
        public string Guid { get; set; }
        public string Message { get; set; }

        // Override ToString() to print the message and the guid

        public override string ToString()
        {
            return $"Message: {Message} - Guid: {Guid}";
        }
    }

}
