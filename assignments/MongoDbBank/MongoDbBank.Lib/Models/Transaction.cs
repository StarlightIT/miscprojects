using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib.Models
{
    public class Transaction
    {
        public DateTime Timestamp { get; set; }
        public double Amount { get; set; }
    }
}
