namespace StatistikaCasoveRady.DTO
{
    public class OcisteneObedy
    {
        public int Mesic { get; set; }
        public double PocetDni { get; set; }
        public long PocetObedu { get; set; }
        public long PocetOcistenychObedu { get; set; }


        public OcisteneObedy(int mesic, int pocetDni, long pocetObedu)
        {
            Mesic = mesic;
            PocetDni = pocetDni;
            PocetObedu = pocetObedu;

            double k = 366 / 12;
            PocetOcistenychObedu = (long) (PocetObedu * (k / PocetDni));
        }

    }
}
