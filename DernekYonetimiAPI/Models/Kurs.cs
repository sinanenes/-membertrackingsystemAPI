namespace DernekYonetimiAPI.Models
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string? KursAdi { get; set; }
        public int? KursTurKod { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public int? Kapasite { get; set; }
        public decimal? KursUcret { get; set; }
        public int? EgitmenId { get; set; }
    }
}
