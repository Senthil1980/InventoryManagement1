using InventoryManagement.ApplicationCore.Entities;
using InventoryManagement.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int Id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAllAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);     
        Task DeleteAsync(T entity);
      
    }
}
