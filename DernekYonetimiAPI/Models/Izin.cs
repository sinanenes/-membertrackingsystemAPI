namespace DernekYonetimiAPI.Models
{
    public class Izin
    {
        public int IzinId { get; set; }
        public int? KisiId { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public string? Aciklama { get; set; }
    }
}
