using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Application.Common.Interfaces
{
	//Este é a Interface do repositório das Categorias
	public interface ICategoryRepository : IRepositoryGeneric<Category>
	{
		void Update(Category entity);
	}
}
