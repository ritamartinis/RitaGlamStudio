﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RitaGlamStudio.Application.Common.Interfaces;
using RitaGlamStudio.Infrastructure.Data;

namespace RitaGlamStudio.Infrastructure.Repository
{
	//Cada vez que preciso de adicionar, atualizar ou remover algo da BD, faço através deste repositório genérico.
	public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
	{
		//Ligação à BD
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;

		//Construtor
		public RepositoryGeneric(ApplicationDbContext db)
		{
			_db = db;
			dbSet = db.Set<T>();
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public bool Any(Expression<Func<T, bool>> filter)
		{
			return dbSet.Any(filter);
		}

		public T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		{
			//Avaliar a Query do método que eu estou a usar
			IQueryable<T> query = _db.Set<T>();

			//Estou a preparar o SELECT (na query)
			if (filter is not null)
				query = query.Where(filter);

			if (!string.IsNullOrEmpty(includeProperties))
			{
				//Estou a separar as properties, se existirem, por vírgulas.
				//O include é CASE SENSITIVE!!
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperties);
				}
			}
			return query.FirstOrDefault()!;
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property.Trim());		//para poder colocar duas properties no MakeupProductController, linha 23

				}
			}
			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}
	}
}
