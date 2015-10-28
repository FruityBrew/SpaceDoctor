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

        readonly ICollection<XDragVM> _dragCollection;
        readonly CollectionViewSource _dragsCVS;

        readonly ICollection<XDragKitVM> _dragKitObsCollection;
        readonly CollectionViewSource _dragKitCVS;

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


            //Список препаратов
            _dragCollection = new ObservableCollection<XDragVM>();
            foreach (var v in Dal.DbContext.Drags)
                _dragCollection.Add(new XDragVM(v));

            _dragsCVS = new CollectionViewSource();
            _dragsCVS.Source = _dragCollection;

            //список лекарственныхНаборов:
            _dragKitObsCollection = new ObservableCollection<XDragKitVM>();
            foreach (var v in Dal.DragKitCollection)
                _dragKitObsCollection.Add(new XDragKitVM(v));
            _dragKitCVS = new CollectionViewSource();
            _dragKitCVS.Source = _dragKitObsCollection;
            _dragKitCVS.View.CurrentChanged += DragsKitView_CurrentChanged;
            

            ActualExam = new XExamVM();
            ActualExam.Date = DateTime.Now;


            CreateNewExamCommand = new XCommand(CreateNewExam);
            SaveChangesCommand = new XCommand(SaveExam);
            AddNewExamToPlanCommand = new XCommand(AddNewExamToPlan);
            CreateNewExamTypeCommand = new XCommand(CreateNewExamType);
            SaveNewExamTypeCommand = new XCommand(SaveNewExamType);
            DeleteExamFromPlanCommand = new XCommand(DeleteExamFromPlan);
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

        public XDragKitVM SelectedDragsKit
        {
            get 
            {
                return DragsKitCVSView.CurrentItem as XDragKitVM;
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
                RaisePropertyChanged("ActualExam");
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

        public DateTime Day { get; set; }

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


        public ICollectionView AllDragsCVSView
        {
            get
            {
                return _dragsCVS.View;
            }
        }

        public ICollectionView DragsKitCVSView
        {
            get
            {
                return _dragKitCVS.View;
            }
        }


        #endregion

        #region methods

        private void CreateNewExam()
        {
            ActualExam.ExamType = SelectedExamType; //new XExamTypeVM(this.SelectedExamType.ExType);

            ActualExam.Date = DateTime.Now;
            _client.ExamsObsCollection.Add(ActualExam);

            RaisePropertyChanged("ActualExam");
            ActualExam = new XExamVM(); //менял 
            ActualExam.Date = DateTime.Now;
        }

        private void SaveExam()
        {
            Dal.DbContext.SaveChanges();
        }

        
        private void AddNewExamToPlan()
        {
            ActualExam.ExamType = SelectedExamType;
            //ActualExam.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minutes, 0);
            ActualExam.Date = new DateTime(ActualExam.Date.Year, ActualExam.Date.Month, ActualExam.Date.Day, Hour, Minutes, 0);

            _client.ExamsObsCollection.Add(ActualExam);
            Dal.DbContext.SaveChanges();
            ActualExam = new XExamVM();
            ActualExam.Date = DateTime.Now;

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
                {
                    NewExamType.ParamTypeaObsCollection.Add(v);
                    v.SelectToNewExam = false;
                }
            }

            ParamTypesCVSView.Refresh();

            Dal.DbContext.SaveChanges();

        }


        private void DeleteExamFromPlan()
        {
            Dal.RemoveExam(Client.SelectedExamFromPlan.Exam);

            Client.DeleteExam(Client.SelectedExamFromPlan);
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



        private void DragsKitView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragsKit");
        }


        #endregion

        #region commands 

        public XCommand CreateNewExamCommand { get; set; }
        public XCommand SaveChangesCommand { get; set; }
        public XCommand AddNewExamToPlanCommand { get; set; }
        public XCommand CreateNewExamTypeCommand { get; set; }
        public XCommand SaveNewExamTypeCommand { get; set; }
        public XCommand DeleteExamFromPlanCommand { get; set; }

        #endregion

    }
}
