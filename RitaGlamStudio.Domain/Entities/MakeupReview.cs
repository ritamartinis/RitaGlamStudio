using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RitaGlamStudio.Domain.Entities
{
    public class MakeupReview
    {
        public int Id { get; set; }

        //Chave estrangeira é MakeupProduct. Esta é a ligação ao MakeupProduct.cs
        //Para criar uma chave estrangeira, precisamos destes dois!
        [ForeignKey("MakeupProduct")]
        [Display(Name = "Makeup Product")]
        public int MakeupProductId { get; set; }
        //este é só para navegação entre as duas tabelas, para não nos dar erro quando preenchemos o formulário, precisamos:
        [ValidateNever]
        public MakeupProduct MakeupProduct { get; set; } = null!;    //esta propriedade serve para ele navegar entre as duas tabelas

        [MaxLength(50)]
        [Display(Name = "Client Name")]
        public string? ClientName { get; set; }

        [MaxLength(100)]
        [Required]
        public string Review { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Review Date")]
        public DateTime ReviewDate { get; set; }

    }
}
