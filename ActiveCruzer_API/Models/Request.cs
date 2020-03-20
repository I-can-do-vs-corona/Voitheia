using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ActiveCruzer.Models
{
    public class Request
    {
        public int unique_id { get; set; }

        public User requestor { get; set; }
        public User acceptor { get; set; }

        public string topic { get; set; }
        public string description { get; set; }
        enum status { OPEN = 1, PENDING = 2, CLOSED = 3, TIMEOUT = 4 };
        public int currentStatus { get; set; }
        public DateTime createdOn { get; set; }

        public Request(User requestor, string topic, string description)
        {

        }
    }
}
