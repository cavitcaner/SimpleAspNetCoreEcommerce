namespace Kafein.Model
{
    public class SiparisDetay
    {
        public int Id { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Urun Urun { get; set; }
        public int Adet { get; set; }
        public double ToplamFiyat { get; set; }
    }

}
