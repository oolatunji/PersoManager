using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class StatusUtil
    {
        public enum OrderStatus
        {
            Requested,
            Approved,
            Delivered,
            Acknowledged,
            Cancelled,
            Declined
        }

        public enum Status
        {
            Active,
            InActive,
        }
    }
}
