using SpaceDoctor.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;


namespace SpaceDoctor.ViewModel
{
    public class XExamTypeVM : XViewModelBase
    {

        #region fields
        readonly XExamsType _exType;
        readonly ObservableCollection<XParamTypeVM> _paramTypeObsCollection;
        readonly CollectionViewSource _paramTypesCVS;
        #endregion


        #region ctors
        public XExamTypeVM() : this (new XExamsType())
        {

        }
        
        public XExamTypeVM(XExamsType exType)
        {
            _exType = exType;
            _paramTypeObsCollection = new ObservableCollection<XParamTypeVM>();

            foreach (var v in exType.ParamsCollection)
                _paramTypeObsCollection.Add(new XParamTypeVM(v));

            _paramTypeObsCollection.CollectionChanged += _paramTypeObsCollection_CollectionChanged;

            _paramTypesCVS = new CollectionViewSource();
            _paramTypesCVS.Source = _paramTypeObsCollection;
            
        }

        #endregion

        #region properties

        public String Name 
        {
            get 
            {
                return ExType.Name; 
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Ребята, название не может быть пустым...");
                else
                {
                    ExType.Name = value;
                    RaisePropertyChanged("Name");
                }
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

        #endregion


        private void _paramTypeObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                this._exType.ParamsCollection.Add(((XParamTypeVM)e.NewItems[0]).ParamType);
        }
    }
}
