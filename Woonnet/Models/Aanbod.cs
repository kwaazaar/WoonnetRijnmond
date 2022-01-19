using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woonnet.Models
{
    public class Aanbod
    {
        public string id { get; set; }
        public string straat { get; set; }
        public string huisnummer { get; set; }
        public string huisletter { get; set; }
        public string huisnummertoevoeging { get; set; }
        public string wijk { get; set; }
        public string plaats { get; set; }
        public string buurt { get; set; }
        public string beschikbaarper { get; set; }
        public string objecttype { get; set; }
        public string totaleoppervlakte { get; set; }
        public string aantalslaapkamers { get; set; }
        public string totalehuur { get; set; }
        public string verdeelmodel { get; set; }
        public string publstart { get; set; }
        public string publstop { get; set; }
        public string minleeftijd { get; set; }
        public string maxleeftijd { get; set; }
        public string aantalreacties { get; set; }

        public DateTime PublStartDateTime
        {
            get
            {
                return DateTime.Parse(publstart);
            }
        }
        public int? AantalReactiesNumeric
        {
            get
            {
                return (int.TryParse(aantalreacties, out var value)) ? value : default;
            }
        }

        public string Url
        {
            get
            {
                return "https://www.woonnetrijnmond.nl/detail/" + id;
            }
        }

    }
}
