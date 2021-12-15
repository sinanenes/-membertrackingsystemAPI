namespace DernekYonetimiAPI.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string? KullaniciAdi { get; set; }
        public int? KullaniciTipiKod { get; set; }
        public int? AktifPasifKod { get; set; }
        public string? Parola { get; set; }

    }
}
