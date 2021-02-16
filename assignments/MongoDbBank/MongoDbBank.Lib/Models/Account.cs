using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
        public double? CurrentBalance { get; set; }
    }
}
