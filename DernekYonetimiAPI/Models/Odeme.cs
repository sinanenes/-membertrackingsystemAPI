namespace DernekYonetimiAPI.Models
{
    public class Odeme
    {
        public int OdemeId { get; set; }
        public int? KullaniciId { get; set; }
        public int? KisiId { get; set; }
        public decimal? OdemeTutar { get; set; }
        public DateTime OdemeTarih { get; set; }
        public int? OdemeTurKod { get; set; }
        public int? DonemAyKod { get; set; }
        public int? DonemYilKod { get; set; }
        public int? KasaBankaId { get; set; }
        public string? Aciklama { get; set; }
    }
}
