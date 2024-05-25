using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	public class BrandRepository : RepositoryGeneric<Brand>, IBrandRepository
	{
		private readonly ApplicationDbContext _db;

		//Construtor
		public BrandRepository(ApplicationDbContext db) : base(db)               
		{
			_db = db;
		}
		public void Update(Brand entity)
		{
			_db.Update(entity);
		}
	}
}
