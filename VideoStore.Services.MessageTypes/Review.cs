using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Services.MessageTypes
{
    public class Review : MessageType
    {
        public string Title { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewLocation { get; set; }
        public string ReviewDetails { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int AuthorId { get; set; }
        public int MediaId { get; set; }
    }
}
