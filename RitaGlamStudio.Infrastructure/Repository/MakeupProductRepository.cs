﻿using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Domain.Entities;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	public class MakeupReviewRepository : RepositoryGeneric<MakeupReview>, IMakeupReviewRepository
	{
		private readonly ApplicationDbContext _db;

		//Construtor
		public MakeupReviewRepository(ApplicationDbContext db) : base(db)               
		{
			_db = db;
		}
		public void Update(MakeupReview entity)
		{
			_db.Update(entity);
		}
	}
}
