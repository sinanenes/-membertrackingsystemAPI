namespace DernekYonetimiAPI.Models
{
    public class KursKayitUye
    {
        public int KursKayitUyeId { get; set; }
        public int? KursId { get; set; }
        public int? UyeId { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
    }
}
