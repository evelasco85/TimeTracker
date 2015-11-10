using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.Entity.Migrations;

namespace Domain.Infrastructure
{
    public interface IEFRepository
    {
        WorklogDBEntities Entities { get; }
        IQueryable<TEntity> GetEntityQuery<TEntity>() where TEntity : class;
        TEntity Delete<TEntity>(TEntity deleteCriteria) where TEntity : class;
        void Delete<TEntity>(Func<TEntity, bool> criteria) where TEntity : class;
        void Save<TEntity>(Expression<Func<TEntity, object>> primaryKeyExpression, params TEntity[] entitie) where TEntity : class;
        void DeleteAll<TEntity>() where TEntity : class;
        void DeleteRange<TEntity>(params TEntity[] entities) where TEntity : class;
        TransactionScope BeginTransactionScope();
        void CommitTransactionScope(TransactionScope currentTransaction);
    }
    public class EFRepository : IEFRepository
    {
        WorklogDBEntities _entities;

        public EFRepository()
        {
            this._entities = new WorklogDBEntities();

            //Hook UTC resolver
            //((IObjectContextAdapter)this._entities).ObjectContext.ObjectMaterialized +=
            //    (sender, e) => UTCDateTimeKindAttribute.Apply(e.Entity);
        }

        [Obsolete]
        public WorklogDBEntities Entities { get { return this._entities; } }

        public TransactionScope BeginTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Snapshot });
        }

        public void CommitTransactionScope(TransactionScope transactionScope)
        {
            try
            {
                transactionScope.Complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<TEntity> GetEntityQuery<TEntity>() where TEntity : class
        {
            IQueryable<TEntity> selectQuery;

            selectQuery = this.GetDbSet<TEntity>();

            return selectQuery;
        }

        public TEntity Delete<TEntity>(TEntity deleteCriteria) where TEntity : class
        {
            DbSet<TEntity> dbSet = this.GetDbSet<TEntity>();

            dbSet.Remove(deleteCriteria);
            this._entities.SaveChanges();

            return deleteCriteria;
        }

        public void Delete<TEntity>(Func<TEntity, bool> criteria) where TEntity : class
        {
            IEnumerable<TEntity> items = this.GetEntityQuery<TEntity>()
                .Where(criteria);

            this.DeleteRange(items.ToArray());
        }

        public void DeleteRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            DbSet<TEntity> dbSet = this.GetDbSet<TEntity>();

            dbSet.RemoveRange(entities);
            this._entities.SaveChanges();
        }

        public void DeleteAll<TEntity>() where TEntity : class
        {
            DbSet<TEntity> dbSet = this.GetDbSet<TEntity>();

            dbSet.RemoveRange(dbSet.Select(x => x).AsEnumerable());
            this._entities.SaveChanges();
        }
        public void Save<TEntity>(Expression<Func<TEntity, object>> primaryKeyExpression, params TEntity[] entities) where TEntity : class
        {
            WorklogDBEntities context = this._entities;
            DbSet<TEntity> dbSet = this.GetDbSet<TEntity>();
            DbContextTransaction transaction = context.Database.BeginTransaction();

            try
            {
                for (int index = 0; index < entities.Count(); index++)
                {
                    TEntity entity = entities[index];

                    //UTCDateTimeKindAttribute.Apply(entity);

                    dbSet.AddOrUpdate(primaryKeyExpression, entity);
                }

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw ex;
            }
        }

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            DbSet<TEntity> dbSet;

            dbSet = this._entities.Set<TEntity>();

            return dbSet;
        }
    }
}
