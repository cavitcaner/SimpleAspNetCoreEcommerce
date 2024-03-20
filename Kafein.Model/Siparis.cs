using System;

namespace Kafein.Model
{
    public class Siparis
    {
        public int Id { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public double ToplamFiyat { get; set; }
        public SiparisDurum SiparisDurumu { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }

}
