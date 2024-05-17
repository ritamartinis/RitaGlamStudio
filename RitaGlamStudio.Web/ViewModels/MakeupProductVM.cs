using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Web.ViewModels
{
    public class MakeupProductVM
    {
        public MakeupProduct MakeupProduct { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem>? BrandItems { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryItems { get; set; }
    }
}

