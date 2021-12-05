namespace DernekYonetimiAPI.Models
{
    public class Bilgilendirme
    {
        public int BilgilendirmeId { get; set; }
        public string? BilgilendirmeMetni { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public string? Aciklama { get; set; }
    }
}
