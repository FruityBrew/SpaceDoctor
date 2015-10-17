using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    class XMainWndVM : XViewModelBase
    {
       readonly XClientVM _client;

        readonly XDBContext _dbContext;


        public XMainWndVM()
        {
            _dbContext = new XDBContext();

            _client = new XClientVM(DbContext.Clients.Where(c => c.Id == 1).Single());            

        }

        public XDBContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        internal XClientVM Client
        {
            get
            {
                return _client;
            }
        }

    }
}
