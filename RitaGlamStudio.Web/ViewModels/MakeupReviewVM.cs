using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Web.ViewModels
{
    public class MakeupReviewVM
    {
        public MakeupReview MakeupReview { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem>? MakeupProductItems { get; set; }
    }
}

