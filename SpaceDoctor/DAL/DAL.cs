using System.Collections.Generic;
using SpaceDoctor.Model;

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



        internal void RemoveExam(XExam examToRemove)
        {
            DbContext.Parameters.RemoveRange(examToRemove.ParamsCollection);
            DbContext.Exams.Remove(examToRemove);
        }


    }
}
