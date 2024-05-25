using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	public class CategoryRepository : RepositoryGeneric<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;

		//Construtor
		public CategoryRepository(ApplicationDbContext db) : base(db) 
		{
			_db = db;
		}

		public void Update(Category entity)
		{
			_db.Update(entity);
		}
	}
}
