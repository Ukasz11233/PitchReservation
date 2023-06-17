using System.ComponentModel.DataAnnotations;

namespace RezerwacjaBoiska.Models
{
    public enum OcenaBoiska
    {
        [Display(Name = "Slabe")]
        Slabe,
        
        [Display(Name = "Ok")]
        Ok,
        
        [Display(Name = "Dobre")]
        Dobre,
        
    }    
    public class Opinie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ocena")]
        public OcenaBoiska Ocena { get; set; }
        [Display(Name = "Komentarz")]
        public String Komentarz { get; set; }
        [Display(Name = "Data dodania")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime DataDodania { get; set; }
        public Gracz? Autor { get; set; }
        public Boiska? Boisko { get; set; }
    }
}