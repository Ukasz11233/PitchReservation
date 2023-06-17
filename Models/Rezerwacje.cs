using System.ComponentModel.DataAnnotations;

namespace RezerwacjaBoiska.Models
{
    public enum StatusRezerwacji
{
    [Display(Name = "Potwierdzona")]
    Potwierdzona,
    
    [Display(Name = "Anulowana")]
    Anulowana,
    
    [Display(Name = "W trakcie")]
    Trwa,
    [Display(Name = "W oczekiwaniu")]
    Oczekuje
}
    public class Rezerwacje
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Data rezerwacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime DataRezerwacji { get; set; }
        [Display(Name = "Godzina rozpoczecia")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime GodzinaRozpoczecia { get; set; }
        [Display(Name = "Godzina zakonczenia")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime GodzinaZakonczenia { get; set; }
         [Display(Name = "Status")]
        public StatusRezerwacji Status { get; set; }
        public Gracz? Gracze { get; set; }
        public Boiska? Boiska { get; set; }

    }
}