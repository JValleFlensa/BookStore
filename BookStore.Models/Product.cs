using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Es necesario que escriba un {0}")]
        [DisplayName("Título")]
        public string Tittle { get; set; } = null!;
        [DisplayName("Descripción")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Es necesario que escriba un {0}")]
        public string ISBN { get; set; } = null!;
        [Required(ErrorMessage = "Es necesario que escriba un {0}")]
        [DisplayName("Autor")]
        public string Author { get; set; } = null!;
        [Required]
        [Range(0, 1000)]
        [DisplayName("Precio de Lista")]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, 1000)]
        [DisplayName("Precio 50 o más")]
        public double Price50 { get; set; }
        [Required]
        [Range(0, 1000)]
        [DisplayName("Precio 100 o más")]
        public double Price100 { get; set; }
        [DisplayName("Portada")]
        public string? ImageURL { get; set; }

        [Required(ErrorMessage = "Es necesario que seleccione un {0}")]
        [DisplayName("Tipo de Cubierta")]
        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }

        [Required(ErrorMessage = "Es necesario que seleccione una {0}")]
        [ForeignKey("Category")]
        [DisplayName("Categoría")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
