using SpaceDoctor.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

/***********************************************
   c
    It contains a collection of related 
    types of parameters (XParamTypesVM).

    ----------------------------------------
    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
************************************************/

namespace SpaceDoctor.ViewModel
{
    /// <summary>
    /// Wrapper for the XExam class
    /// </summary>
    public class XExamTypeVM : XViewModelBase
    {

        #region fields
        readonly XExamType _exType;
        readonly ObservableCollection<XParamTypeVM> _paramTypeObsCollection;
        readonly CollectionViewSource _paramTypesCVS;
        #endregion


        #region ctors
        public XExamTypeVM() : this (new XExamType())
        {
        }
        
        public XExamTypeVM(XExamType exType)
        {
            if (exType == null)
                throw new ArgumentNullException("Ссылка на объект XExamType не может быть null");

            _exType = exType;
            _paramTypeObsCollection = new ObservableCollection<XParamTypeVM>();

            if (exType.ParamsCollection == null)
                exType.ParamsCollection = new Collection<XParamType>();

            foreach (var v in exType.ParamsCollection)
                _paramTypeObsCollection.Add(new XParamTypeVM(v));

            _paramTypeObsCollection.CollectionChanged += ParamTypeObsCollection_CollectionChanged;

            _paramTypesCVS = new CollectionViewSource();
            _paramTypesCVS.Source = _paramTypeObsCollection;
            
        }

        #endregion

        #region properties

        public XExamType ExType
        {
            get
            {
                return _exType;
            }
        }

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
                    ExType.Name = value;
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


        private void ParamTypeObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                this._exType.ParamsCollection.Add(((XParamTypeVM)e.NewItems[0]).ParamType);
        }
    }
}
