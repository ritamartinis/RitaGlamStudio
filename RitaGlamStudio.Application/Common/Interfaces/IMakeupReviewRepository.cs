using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Application.Common.Interfaces
{
	//Este é a Interface do repositório das Reviews
	public interface IMakeupReviewRepository : IRepositoryGeneric<MakeupReview>
	{
		void Update(MakeupReview entity);
	}
}
