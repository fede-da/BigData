using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagApp.DAL.MongoModels
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public int Score { get; set; }
        public string Review { get; set; }
    }
}
