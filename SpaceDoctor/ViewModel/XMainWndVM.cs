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

        readonly DAL _dal;

        ICollection<XExamsType> _examsTypesCollection;

        


        public XMainWndVM()
        {
            using (Dal.DbContext)
            {



                _examsTypesCollection = new Collection<XExamsType>(Dal.DbContext.ExamsType.ToList());

                _client = new XClientVM(Dal.ClientCollection.First(cl => cl.Id == 1));
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

        internal DAL Dal
        {
            get
            {
                return _dal;
            }
        }
    }
}
