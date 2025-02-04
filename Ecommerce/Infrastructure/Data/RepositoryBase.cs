﻿using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    //Clase genérica, repositorio base, T es como un comodin que nos permite usarlo en todas las entidades. 
    //Esta clase no existe hasta el momento en que se la llama a travez de otro repositorio. 
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        // Contexto de base de datos de Entity Framework
        private readonly DbContext _dbContext;
        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T? GetById<TId>(TId id)
        {
            return _dbContext.Set<T>().Find(new object[] { id });
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}