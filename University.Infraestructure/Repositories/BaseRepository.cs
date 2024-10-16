﻿using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Interfaces;
using University.Infraestructure.Data;

namespace University.Infraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _entities;

        public BaseRepository(UniversityStoreContext context)
        {
            _entities = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            T? entity = await GetById(id);
            if (entity != null)
                _entities.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => _entities.Update(entity));
        }
    }
}
