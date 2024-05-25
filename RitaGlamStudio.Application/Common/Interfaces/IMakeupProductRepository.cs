using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Application.Common.Interfaces
{
	//Este é a Interface do repositório dos Produtos de Maquilhagem
	public interface IMakeupProductRepository : IRepositoryGeneric<MakeupProduct>
	{
		void Update(MakeupProduct entity);
	}
}
