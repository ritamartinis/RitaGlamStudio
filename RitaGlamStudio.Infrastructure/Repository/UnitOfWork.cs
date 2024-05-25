using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	//UnitOfWork - chamamos a isto um wrapper.
	public class UnitOfWork : IUnitOfWork
	{
		//Acesso à BD
		private readonly ApplicationDbContext _db;

		//Construtor
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Brand = new BrandRepository(db);
			Category = new CategoryRepository(db);
			MakeupProduct = new MakeupProductRepository(db);
            MakeupReview = new MakeupReviewRepository(db);
        }

		public IBrandRepository Brand { get; private set; }
		public ICategoryRepository Category { get; private set; }
		public IMakeupProductRepository MakeupProduct { get; private set; }
        public IMakeupReviewRepository MakeupReview { get; private set; }

        public void Save()
		{
			_db.SaveChanges();
		}
	}
}
