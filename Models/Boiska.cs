using System.ComponentModel.DataAnnotations;

namespace RezerwacjaBoiska.Models
{
    public class Boiska
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public String Nazwa { get; set; }
        [Display(Name = "Lokalizacja")]
        public String Lokalizacja { get; set; }
        [Display(Name = "Rozmiar")]
        public String Rozmiar { get; set; }

    }
}