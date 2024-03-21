using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using RMS.DAL.IRepository;


namespace RMS.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DSBDBEntities Context = null;
        private ObjectContext ObjectContext = null;
        public Repository()
        {
            Context = new DSBDBEntities();
            try
            {
                ObjectContext = ((IObjectContextAdapter)Context).ObjectContext;
            }
            catch (Exception e)
            {

                string errorMessage = e.Message;
            }
          
            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = true;
        }

        /// <summary>
        /// Returns a DbSet instance for access to entities of the given type in the
        /// context, the ObjectStateManager, and the underlying store.
        /// </summary>
        protected DbSet<TEntity> ObjectSet
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }


        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        public virtual TEntity GetById(int? id)
        {
            return ObjectSet.Find(id);
        }



        public virtual IQueryable<TEntity> All()
        {
            return ObjectSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Where(predicate).AsQueryable<TEntity>();
        }



        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Count(predicate) > 0;
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.FirstOrDefault(predicate);
        }

        public virtual int Count()
        {
            return ObjectSet.Count();
        }

        public TEntity Add(TEntity entity)
        {
            TEntity addResult = ObjectSet.Add(entity);
            SaveChanges();
            return addResult;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Count(predicate);
        }

        public virtual int Save(TEntity entity)
        {
            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                ObjectSet.Add(entity);
                entry.State = EntityState.Added;
            }
            else
            {
                ObjectSet.Attach(entity);
                entry.State = EntityState.Modified;
            }

            return SaveChanges();
        }

        public virtual int Edit(TEntity entity)
        {
            var entry = Context.Entry(entity);

            ObjectSet.Attach(entity);
            entry.State = EntityState.Modified;

            return SaveChanges();
        }
        public virtual int Delete(TEntity entity)
        {
            ObjectSet.Remove(entity);

            return SaveChanges();
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                ObjectSet.Remove(obj);
            }
            SaveChanges();
            return 0;
        }




        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjectSet.Any(predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>
        /// An List<TEntity> that contains elements from the input sequence 
        /// that satisfy the condition specified by predicate.
        /// </returns>
        public virtual IQueryable<TEntity> Filter<T>(Expression<Func<TEntity, bool>> filter, out int total)
        {

            var _resetSet = filter != null ? ObjectSet.Where(filter).AsQueryable() :
                ObjectSet.AsQueryable();
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public virtual TEntity EditEnity(TEntity entity)
        {
            var entry = Context.Entry(entity);
            ObjectSet.Attach(entity);
            entry.State = EntityState.Modified;
            SaveChanges();
            return entity;
        }

        public virtual int SaveList(List<TEntity> entities)
        {
            int save = 0;
            foreach (TEntity entity in entities)
            {
                save += Save(entity);
            }
            return save;
        }
    }
}
