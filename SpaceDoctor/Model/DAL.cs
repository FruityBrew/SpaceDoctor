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
        IEnumerable<XDragKit> _dragKitCollection;


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


            var vd = _dbContext.DragKits.Include("DragCollection");


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

        public IEnumerable<XExamsType> ExamTypesCollection
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
