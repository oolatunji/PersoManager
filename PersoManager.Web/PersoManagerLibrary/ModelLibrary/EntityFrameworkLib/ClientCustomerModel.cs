using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class ClientCustomerModel
    {
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string AccountNumber { get; set; }
        public long CardProfileID { get; set; }
        public Nullable<long> CustomerBranch { get; set; }
        public bool Downloaded { get; set; }
    }
}
