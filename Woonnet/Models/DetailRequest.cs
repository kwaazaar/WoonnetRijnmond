using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woonnet.Models
{
    public class DetailRequest
    {
        public string Id { get; set; }
        public long? VolgendeId { get; set; } = 100079264; // oops
        public string Filters { get; set; } = "gebruik!=Complex";
        public string inschrijfnummerTekst { get; set; } = "-1";
        public string Volgorde { get; set; } = "";
        public string hash { get; set; } = "";
    }
}


//{ "Id":"100079231","VolgendeId":100079264,"Filters":"gebruik!=Complex","inschrijfnummerTekst":"-1","Volgorde":"","hash":""}