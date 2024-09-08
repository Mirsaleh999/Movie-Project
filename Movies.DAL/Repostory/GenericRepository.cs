using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Repostory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repostory
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity item)
        {
            item.InsertDate = DateTime.Now;
            await _entities.AddAsync(item);
            _dbContext.SaveChanges();
           
            return item;
        }

        public void Delete(int id)
        {
            var dbItem = _entities.Find(id);
            _entities.Remove(dbItem);
            dbItem.DeletedDate = DateTime.Now;  
            _dbContext.SaveChanges();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var dbItem = await _entities.FindAsync(id);
            return dbItem;
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            var dbItem = await _entities.ToListAsync();
            return dbItem;
        }

        public TEntity Update(TEntity item)
        {
            var dbEntity = _entities.Find(item.Id);
            item.InsertDate = dbEntity.InsertDate;
            item.UpdateDate = DateTime.Now;
            _entities.Update(item);
            _dbContext.SaveChanges();
            return item;
        }
    }
}
