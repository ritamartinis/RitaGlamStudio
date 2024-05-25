using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	public class MakeupProductRepository : RepositoryGeneric<MakeupProduct>, IMakeupProductRepository
	{
		private readonly ApplicationDbContext _db;

		//Construtor
		public MakeupProductRepository(ApplicationDbContext db) : base(db)               
		{
			_db = db;
		}
		public void Update(MakeupProduct entity)
		{
			_db.Update(entity);
		}
	}
}
