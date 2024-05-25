using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Application.Common.Interfaces
{
	//Este é a Interface do repositório das Marcas
	public interface IBrandRepository : IRepositoryGeneric<Brand>
	{
		void Update(Brand entity);
	}
}
