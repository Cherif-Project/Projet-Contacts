using System.ComponentModel.DataAnnotations;

namespace ContactListe.Models.Domain
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public bool Favorite { get; set; }
    }
}
