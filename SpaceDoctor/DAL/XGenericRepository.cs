using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SpaceDoctor.DAL
{
    public class XGenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region fields

        readonly ObjectSet<T> _objectSet;
        readonly static ObjectContext _objectContext;


        #endregion

        #region ctors

        static XGenericRepository()
        {
            XDBContext con = new XDBContext();
            _objectContext = ((IObjectContextAdapter)con).ObjectContext;
        }

        public XGenericRepository()
        {
            _objectSet = _objectContext.CreateObjectSet<T>();
        }

        #endregion

        #region properties

        public ObjectSet<T> ObjectSet
        {
            get
            {
                return _objectSet;
            }
        }

        public ObjectContext ObjectContext
        {
            get
            {
                return _objectContext;
            }
        }
        #endregion

        #region commands
        #endregion

        #region methods

        #region implimentationIGenericRepositoryInterfase

        public void Delete(T entity)
        {
            ObjectSet.DeleteObject(entity);
            ObjectContext.SaveChanges();
        }

        public void Insert(T entity)
        {
            ObjectSet.AddObject(entity);
            ObjectContext.SaveChanges();
        }

        public void Update(T entity)
        {
            ObjectContext.ObjectStateManager.ChangeObjectState(entity, entityState: System.Data.Entity.EntityState.Modified);
            ObjectContext.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return ObjectSet.Where(where);
        }

        #endregion

        #endregion

        #region eventHandlers
        #endregion

    }
}
