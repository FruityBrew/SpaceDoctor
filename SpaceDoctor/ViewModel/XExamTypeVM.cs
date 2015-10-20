using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel 
{
    public class XExamTypeVM : XViewModelBase
    {
        readonly XExamsType _exType;
        readonly ObservableCollection<XParamTypeVM> _paramTypeObsCollection;
        readonly CollectionViewSource _paramTypesCVS;


        public XExamTypeVM()
        {
            _exType = new XExamsType();
        }

        public XExamTypeVM(XExamsType exType)
        {
            _exType = exType;
            _paramTypeObsCollection = new ObservableCollection<XParamTypeVM>();

            foreach (var v in exType.ParamsCollection)
                _paramTypeObsCollection.Add(new XParamTypeVM(v));

            _paramTypesCVS = new CollectionViewSource();
            _paramTypesCVS.Source = _paramTypeObsCollection;
            
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

        internal ObservableCollection<XParamTypeVM> ParamTypeaObsCollection
        {
            get
            {
                return _paramTypeObsCollection;
            }
        }

        public ICollectionView ParamTypeCVSView
        {
            get
            {
                return _paramTypesCVS.View;
            } 
        }

    }
}
