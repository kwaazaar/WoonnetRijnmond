using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woonnet.Models
{
    public class Advertentie
    {
        public long AdvertentieId { get; set; }
        public long FrontendAdvertentieId { get; set; }
        public bool UrgentieProfielMatch { get; set; }
        public int WoonwensObjectType { get; set; }
        public bool IsVandaagNieuw { get; set; }
        public int Definitievescore { get; set; }
    }
}
