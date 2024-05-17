using System.ComponentModel.DataAnnotations;

namespace RitaGlamStudio.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public required string Name { get; set; }
    }
}
