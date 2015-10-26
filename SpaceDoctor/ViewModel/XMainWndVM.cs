using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    public sealed class XMainWndVM : XViewModelBase
    {

        #region fields

        readonly XClientVM _client;
        readonly DAL _dal;
        readonly ObservableCollection<XExamTypeVM> _examTypesObsCollection;
        readonly CollectionViewSource _examTypesCVS;

        readonly ObservableCollection<XParamTypeVM> _paramTypesObsCollection;
        readonly CollectionViewSource _paramTypesCVS;

        ICollection<XDrag> _dragCollection;
        XExamVM _actualExam;
        XExamTypeVM _newExam;

        readonly ICollection<Int32> _hoursCollection;
        readonly ICollection<Int32> _minutesCollection;
        Int32 _hour;
        Int32 _minutes;

        #endregion 


        #region ctors
        public XMainWndVM()
        {
            _dal = new DAL();

            _hoursCollection = new Collection<Int32>();
            for (int i = 0; i <= 24; i++)
                _hoursCollection.Add(i);

            _minutesCollection = new Collection<Int32>();
            for (int i = 0; i < 60; i += 5)
                _minutesCollection.Add(i);

            

            _examTypesObsCollection = new ObservableCollection<XExamTypeVM>();
            foreach (var v in Dal.ExamTypesCollection)
            {
                _examTypesObsCollection.Add(new XExamTypeVM(v));
            }

            
            _examTypesCVS = new CollectionViewSource();
            _examTypesCVS.Source = _examTypesObsCollection;
            _examTypesCVS.View.CurrentChanged += View_CurrentChanged;
            _examTypesObsCollection.CollectionChanged += _examTypesObsCollection_CollectionChanged;


            //заполнить коллекции с типами параметров:
            _paramTypesObsCollection = new ObservableCollection<XParamTypeVM>();
            foreach (var v in Dal.DbContext.ParamsTypes)
                _paramTypesObsCollection.Add(new XParamTypeVM(v));
            _paramTypesCVS = new CollectionViewSource();
            _paramTypesCVS.Source = _paramTypesObsCollection;


            _client = new XClientVM(Dal.ClientCollection.First(cl => cl.Id == 1));



            _dragCollection = new Collection<XDrag>(Dal.DbContext.Drags.ToList());

            ActualExam = new XExamVM();
            ActualExam.Date = DateTime.Now;


            CreateNewExamCommand = new XCommand(CreateNewExam);
            SaveExamCommand = new XCommand(SaveExam);
            AddNewExamToPlanCommand = new XCommand(AddNewExamToPlan);
            CreateNewExamTypeCommand = new XCommand(CreateNewExamType);
            SaveNewExamTypeCommand = new XCommand(SaveNewExamType);

        }



        #endregion

        #region properties

        public XExamTypeVM SelectedExamType
        {
            get
            {
                return ExamTypesCVSView.CurrentItem as XExamTypeVM;
            }
        }

        public XClientVM Client
        {
            get
            {
                return _client;
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

        public ObservableCollection<XExamTypeVM> ExamTypesObsCollection
        {
            get
            {
                return _examTypesObsCollection;
            }
        }

        public ICollectionView ExamTypesCVSView
        {
            get
            {
                return _examTypesCVS.View;
            }
        }

        public XExamVM ActualExam
        {
            get
            {
                return _actualExam;
            }

            set
            {
                _actualExam = value;
            }
        }

        public ICollection<int> HoursCollection
        {
            get
            {
                return _hoursCollection;
            }
        }

        public ICollection<int> MinutesCollection
        {
            get
            {
                return _minutesCollection;
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }

            set
            {
                _hour = value;
            }
        }

        public int Minutes
        {
            get
            {
                return _minutes;
            }

            set
            {
                _minutes = value;
            }
        }

        public ICollectionView ParamTypesCVSView
        {
            get
            {
                return _paramTypesCVS.View;
            }
        }

        private XExamTypeVM NewExamType
        {
            get
            {
                return _newExam;
            }

            set
            {
                _newExam = value;
            }
        }

        #endregion

        #region methods

        private void CreateNewExam()
        {
            ActualExam = new XExamVM();
            ActualExam.ExamType = SelectedExamType; //new XExamTypeVM(this.SelectedExamType.ExType);

            ActualExam.Date = DateTime.Now;
            _client.ExamsObsCollection.Add(ActualExam);

            RaisePropertyChanged("ActualExam");
        }

        private void SaveExam()
        {
            Dal.DbContext.SaveChanges();
        }

        private void AddNewExamToPlan()
        {
            ActualExam = new XExamVM();
            ActualExam.ExamType = SelectedExamType;
            ActualExam.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minutes, 0);

            _client.ExamsObsCollection.Add(ActualExam);
            Dal.DbContext.SaveChanges();
        }

        private void CreateNewExamType()
        {
            NewExamType = new XExamTypeVM();
            ExamTypesObsCollection.Add(NewExamType);

        }

        private void SaveNewExamType()
        {
            foreach (var v in _paramTypesObsCollection)
            {
                if (v.SelectToNewExam)
                    NewExamType.ParamTypeaObsCollection.Add(v);
            }

            Dal.DbContext.SaveChanges();

        }


        
        #endregion


        #region eventHandlers

        private void View_CurrentChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged("SelectedExamType");
        }

        private void _examTypesObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Dal.DbContext.ExamsType.Add(((XExamTypeVM)e.NewItems[0]).ExType);
            }
        }

        #endregion

        #region commands 

        public XCommand CreateNewExamCommand { get; set; }
        public XCommand SaveExamCommand { get; set; }
        public XCommand AddNewExamToPlanCommand { get; set; }
        public XCommand CreateNewExamTypeCommand { get; set; }
        public XCommand SaveNewExamTypeCommand { get; set; }


        #endregion

    }
}
