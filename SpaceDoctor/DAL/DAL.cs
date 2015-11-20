using System.Collections.Generic;
using SpaceDoctor.Model;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace SpaceDoctor.DAL
{
    internal class DAL
    {
        readonly XDBContext _dbContext;

        IEnumerable<XClient> _clientCollection;
        IEnumerable<XExamType> _examTypesCollection;
        IEnumerable<XDragKit> _dragKitCollection;


        public DAL()
        {

            _dbContext = new XDBContext();
            
            //загрузить сразу все навигац. свойства клиента:
            var v = _dbContext.Clients
                                .Include("ExamsCollection.ParamsCollection")
                                .Include("ExamsCollection.ExamType")
                                .Include("ExamsCollection.ExamType.ParamsCollection")
                                .Include("DragPlanCollection.DragKit.DragCollection");


            var va = _dbContext.ExamsType.Include("ParamsCollection");
            var vd = _dbContext.DragKits.Include("DragCollection");

            _clientCollection = v;
            _examTypesCollection = va;
            _dragKitCollection = vd;
     
        }


        public XDBContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public IEnumerable<XClient> ClientCollection
        {
            get
            {
                return _clientCollection;
            }
        }

        public IEnumerable<XExamType> ExamTypesCollection
        {
            get
            {
                return _examTypesCollection;
            }
        }

        public IEnumerable<XDragKit> DragKitCollection
        {
            get
            {
                return _dragKitCollection;
            }
        }

        public IEnumerable<T> GetEntityCollection<T>(params String [] properties) where T : class
        {
            ObjectContext objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
            objContext.ContextOptions.LazyLoadingEnabled = true;
            var v = objContext.CreateObjectSet<T>();
            

            foreach(var prop in properties)
            {
                LoadProperty<T>(v, prop);
            }

            IEnumerable<T> ienum = v;

            return ienum;
        }

        public void LoadProperty<T> (IEnumerable<T> ienum, String propertyName) 
        {
            foreach(var v in ienum)
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.LoadProperty(v, propertyName);
            }
        }

        internal void RemoveExam(XExam examToRemove)
        {
            DbContext.Parameters.RemoveRange(examToRemove.ParamsCollection);
            DbContext.Exams.Remove(examToRemove);
        }


    }
}
