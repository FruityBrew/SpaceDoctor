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

        ICollection<XDrag> _dragCollection;


        public XMainWndVM()
        {
            _dal = new DAL();
            using (Dal.DbContext)
            {
                _examsTypesCollection = new Collection<XExamsType>(Dal.DbContext.ExamsType.ToList());

                _client = new XClientVM(Dal.ClientCollection.First(cl => cl.Id == 1));

                _dragCollection = new Collection<XDrag>(Dal.DbContext.Drags.ToList());
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

        public ICollection<XDrag> DragCollection
        {
            get
            {
                return _dragCollection;
            }

        }
    }
}
