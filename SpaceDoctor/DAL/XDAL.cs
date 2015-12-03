using System.Collections.Generic;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;


namespace SpaceDoctor.DAL 
{
    internal class XDAL : IDisposable
    {
        XDBContext _dbContext;
        readonly ObjectContext _objContext;
        readonly String _connection;

        public XDAL(String connectionName)
        {
            _connection = connectionName;
            _dbContext = new XDBContext(connectionName);

            _objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
        }


        private XDBContext DbContext
        {
            get
            {
                return _dbContext;
            }
            set
            {
                _dbContext = value;
            }
        }


        private  ObjectContext ObjContext
        {
            get
            {
                return _objContext;
            }
        }

        /// <summary>
        /// Возвращает коллекцию объектов типа <T> контекста БД  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties">Имена навигационных свойств</param>
        /// <returns></returns>
        public IEnumerable<T> GetEntityCollection<T>(params String [] properties) where T : class
        {
            var v = ObjContext.CreateObjectSet<T>();
            
            foreach(var prop in properties)
            {
                LoadProperty<T>(v, prop);
            }

            return v;
        }

        /// <summary>
        /// Загружает навигационные свойства объекта
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienum">коллекция объектов</param>
        /// <param name="propertyName">Имя навигационного свойства</param>
        private void LoadProperty<T> (IEnumerable<T> ienum, String propertyName) 
        {
            foreach(var v in ienum)
            {
                 ObjContext.LoadProperty(v, propertyName);
            }
        }


        internal void DeleteObject<T>(T entity) 
        {
            ObjContext.DeleteObject(entity);        
        }


        internal void SaveChanges()
        {
          //  Dispose();
             DbContext.SaveChanges();
        }

        internal void AddObject<T>(T entity)  where T : class
        {
            ObjContext.CreateObjectSet<T>().AddObject(entity);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
