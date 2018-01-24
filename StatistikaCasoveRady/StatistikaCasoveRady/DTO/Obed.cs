

using System;

namespace StatistikaCasoveRady
{
    public class Obed
    {
        public DateTime Datum { get; set; }
        public DateTime? Cas { get; set; }
        public DateTime? Zuctovano { get; set; }
        public string Typ { get; set; }
        public string Popis { get; set; }
        public decimal? Cena { get; set; }
        public decimal? zustatek { get; set; }
        public string Druh { get; set; }
    }
}
