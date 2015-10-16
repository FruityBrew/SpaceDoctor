using SpaceDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    internal class XClientVM : XViewModelBase
    {
       readonly XClient _client;
    

       public XClientVM()
       {
            _client = new XClient();
       }

       public XClientVM(XClient client)
       {
            _client = client;
       }


        public XClient Client
        {
            get
            {
                return _client;
            }
        }

        public String Name
        {
            get  
            {
                return Client.Name;
            }
            set
            {
                Client.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public DateTime DateBirthday
        {
            get
            {
                return Client.DateBirthday;
               
            }
            set
            {
                Client.DateBirthday = value;
                RaisePropertyChanged("DateBirthday");
            }
        }
         
        public ICollection<XExam> ExamsCollection
        {
           get
           {
                return Client.ExamsCollection;
           }
        } 
        
    }
}
