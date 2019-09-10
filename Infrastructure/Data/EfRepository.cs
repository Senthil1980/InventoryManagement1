using InventoryManagement.ApplicationCore.Entities;
using InventoryManagement.ApplicationCore.Interfaces;
using InventoryManagement.ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;

        public EfRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await _appDbContext.Set<T>().FindAsync(Id);
        }
        public async Task<List<T>> ListAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        public async Task<List<T>> ListAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_appDbContext.Set<T>().AsQueryable(), spec);
        }

    }
}
