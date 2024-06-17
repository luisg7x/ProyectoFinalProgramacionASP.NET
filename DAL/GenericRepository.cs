using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace DAL
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        //protected readonly DbContext Context;
        protected readonly PrograVEntities Context;

        public GenericRepository(PrograVEntities context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return Context.Set<TEntity>().SingleOrDefault(predicate);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public void Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().AddRange(entities);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Attach(entity);
                Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception)
            {

                throw;
            }

        }



        public void Update(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


