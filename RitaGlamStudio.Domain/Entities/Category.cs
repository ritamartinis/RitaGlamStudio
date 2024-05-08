﻿using System.ComponentModel.DataAnnotations;

namespace RitaGlamStudio.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
