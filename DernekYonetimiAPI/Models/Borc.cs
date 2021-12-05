namespace DernekYonetimiAPI.Models
{
    public class Borc
    {
        public int BorcId { get; set; }
        public int? KisiId { get; set; }
        public DateTime BorcTarih { get; set; }
        public int? DonemAyKod { get; set; }
        public int? DonemYilKod { get; set; }
        public int? BorcTurKod { get; set; }
        public decimal? BorcTutar { get; set; }
        public string? Aciklama { get; set; }
    }
}
