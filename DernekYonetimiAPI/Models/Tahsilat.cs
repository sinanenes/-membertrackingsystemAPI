namespace DernekYonetimiAPI.Models
{
    public class Tahsilat
    {
        public int TahsilatId { get; set; }
        public int? KisiId { get; set; }
        public DateTime TahsilatTarih { get; set; }
        public int? DonemAyKod { get; set; }
        public int? DonemYilKod { get; set; }
        public int? TahsilatTurKod { get; set; }
        public decimal? TahsilatTutar { get; set; }
        public int? KasaBankaId { get; set; }
        public string? Aciklama { get; set; }
    }
}
