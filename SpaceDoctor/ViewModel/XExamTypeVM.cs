using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel 
{
    public class XExamTypeVM : XViewModelBase
    {
        readonly XExamsType _exType;


        public XExamTypeVM()
        {
            _exType = new XExamsType();
        }

        public XExamTypeVM(XExamsType exType)
        {
            _exType = exType;
        }

        public String Name 
        {
            get 
            {
                return ExType.Name; 
            }
            set
            {
                ExType.Name = value;
                RaisePropertyChanged("Name");
            }
         }

        public XExamsType ExType
        {
            get
            {
                return _exType;
            }
        }
    }
}
