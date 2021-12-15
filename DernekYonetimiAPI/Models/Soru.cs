namespace DernekYonetimiAPI.Models
{
    public class Soru
    {
        public int SoruId { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime? SoruTarih { get; set; }
        public DateTime? CevapTarih { get; set; }
        public string? SoruMetni { get; set; }
        public string? CevapMetni { get; set; }
        public int? CevaplayanId { get; set; }
        
        
    }
}
