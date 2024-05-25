using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RitaGlamStudio.Domain.Entities
{
    public class MakeupProduct
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        //Chave estrangeira é a Brand. Esta é a ligação ao Brand.cs
        //Para criar uma chave estrangeira, precisamos destes dois: int brand id e brand brand 
        [ForeignKey("Brand")]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        //este é só para navegação entre as duas tabelas, para não nos dar erro quando preenchemos o formulário, precisamos:
        [ValidateNever]
        public Brand Brand { get; set; } = null!;    //esta propriedade serve para ele navegar entre as duas tabelas

        
        [ForeignKey("Category")]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!; //esta também é uma propriedade de navegação


        [Range(1, 5000)]
        public int Price { get; set; }

        public int Stock { get; set; }

        public string? Description { get; set; }

        [NotMapped] //Esta propriedade serve para dizer que esta propriedade NÃO É para criar uma coluna na bd
        public IFormFile? Image { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}
