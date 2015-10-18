using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    public class XMainWndVM : XViewModelBase
    {
       readonly XClientVM _client;

        readonly XDBContext _dbContext;

        ICollection<XExamsType> _examsTypesCollection;

        


        public XMainWndVM()
        {
            _dbContext = new XDBContext();

            var v = _dbContext.Clients

                        //          .i
                        .Include("ExamsCollection.ParamsCollection")

            // .Select(cl => cl.ExamsCollection.Select(e => e.ParamsCollection));
            //Select(x => x.ExamsCollection.Select(e => e.ExamType));
            //.Include("ExamsCollection.ParamsCollection");

            .Include("ExamsCollection.ExamType")
            .Include("ExamsCollection.ExamType.ParamsCollection");
            _client = new XClientVM(v.Where(c => c.Id == 1).Single());


           
            _examsTypesCollection = new Collection<XExamsType>(_dbContext.ExamsType.ToList());



            List<XClient> list = v.ToList<XClient>();          

           
        }

        public XDBContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public XClientVM Client
        {
            get
            {
                return _client;
            }
        }

        public ICollection<XExamsType> ExamsTypesCollection
        {
            get
            {
                return _examsTypesCollection;
            }


        }
    }
}
