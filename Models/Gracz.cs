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
        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone Number must contain exactly 9 digits.")]
        [DataType(DataType.PhoneNumber)]
        public String NumerTelefonu { get; set;}
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public String Email { get; set; }

    }
}