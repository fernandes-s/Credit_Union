using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class AccInfo
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string accType { get; set; }
        public int accountNumber { get; set; }
        public int sortCode { get; set; }
        public decimal initialBalance { get; set; }
        public decimal overdraftLimit { get; set; }
    }
}
