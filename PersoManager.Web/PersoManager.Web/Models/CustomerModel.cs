using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersoManager.Web.Models
{
    public class CustomerModel
    {
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string AccountNumber { get; set; }
        public long CardProfileID { get; set; }
        public System.DateTime Date { get; set; }
        public long CustomerBranch { get; set; }
        public long[] CustomerIDs { get; set; }
    }
}