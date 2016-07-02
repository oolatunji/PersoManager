using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class CardProfileModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string CardType { get; set; }
        public string CardBin { get; set; }
        public long CEDuration { get; set; }
    }
}
