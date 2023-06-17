using System.ComponentModel.DataAnnotations;

namespace RezerwacjaBoiska.Models
{
    public class Gracz
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public String Imie { get; set; }
        [Display(Name = "Nazwisko")]
        public String Nazwisko { get; set; }
        [Display(Name = "Adres")]
        public String Adres { get; set; }
        [Display(Name = "Numer telefonu")]
        [DisplayFormat(NullDisplayText = "Brak")]
        public String NumerTelefonu { get; set;}
        [Display(Name = "Email")]
        public String Email { get; set; }

    }
}