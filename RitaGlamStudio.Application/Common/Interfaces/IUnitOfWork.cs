namespace RitaGlamStudio.Application.Common.Interfaces
{
	public interface IUnitOfWork
	{
		IBrandRepository Brand { get; }						//Este vai aceder à Interface da Marca
		ICategoryRepository Category { get; }               //Este vai aceder à Interface da Categoria
		IMakeupProductRepository MakeupProduct { get; }		//Este vai aceder à Interface dos Produtos de Maquilhagem
		IMakeupReviewRepository MakeupReview { get; }		//Este vai aceder à Interface das Reviews

		void Save();
	}
}
