using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    internal class DAL
    {
        readonly XDBContext _dbContext;

        IEnumerable<XClient> _clientCollection;
        IEnumerable<XExamsType> _examTypesCollection;

        public DAL()
        {
            _dbContext = new XDBContext();

            // _clientCollection = new Collection<XClient>();
            //загрузить сразу все навигац. свойства клиента:
            var v = _dbContext.Clients
                                .Include("ExamsCollection.ParamsCollection")
                                .Include("ExamsCollection.ExamType")
                                .Include("ExamsCollection.ExamType.ParamsCollection")
                                .Include("DragPlanCollection.DragKit.DragCollection");

            _clientCollection = v;

            var va = _dbContext.ExamsType.Include("ParamsCollection");

            _examTypesCollection = va;
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

        public IEnumerable<XExamsType> ExamTypesCollection
        {
            get
            {
                return _examTypesCollection;
            }

        }

        public void AddExam(XExam exam)
        {
            
           // DbContext.Exams.Add(exam);
        }


        internal void RemoveExam(XExam examToRemove)
        {
            DbContext.Parameters.RemoveRange(examToRemove.ParamsCollection);
            DbContext.Exams.Remove(examToRemove);
        }
    }
}
