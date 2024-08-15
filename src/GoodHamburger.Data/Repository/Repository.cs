using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly GHContext _ghcontext;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(GHContext ghcontext)
        {
            _ghcontext = ghcontext;
            DbSet = ghcontext.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Delete(int id)
        {
            DbSet.Remove(new TEntity { Id = id});
            await SaveChanges();
        }
        
        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await _ghcontext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _ghcontext?.Dispose();
        }
    }

}
