using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo de Cubierta")]
        [Required(ErrorMessage = "Debe escribir un nombre")]
        [MaxLength(50, ErrorMessage = "El nombre no puede tener un máximo de 50 carácteres")]
        public string Name { get; set; } = null!;
    }
}
