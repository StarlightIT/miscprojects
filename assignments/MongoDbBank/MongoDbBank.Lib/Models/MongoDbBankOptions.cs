using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib.Models
{
    public class MongoDbBankOptions
    {
        public string ConnectionHost { get; set; }
        public string ConnectionPort { get; set; } = "27017";
        public string Username { get; set; }
        public string Passsword { get; set; }
        public string AuthDatabase { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
