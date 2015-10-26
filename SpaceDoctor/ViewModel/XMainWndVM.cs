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
        ICollection<XDrag> _dragCollection;
        XExamVM _actualExam;

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

            _client = new XClientVM(Dal.ClientCollection.First(cl => cl.Id == 1));



            _dragCollection = new Collection<XDrag>(Dal.DbContext.Drags.ToList());

            ActualExam = new XExamVM();
            ActualExam.Date = DateTime.Now;


            CreateNewExamCommand = new XCommand(CreateNewExam);
            SaveExamCommand = new XCommand(SaveExam);
            AddNewExamToPlanCommand = new XCommand(AddNewExamToPlan);

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


        #endregion

        #region methods

        public void CreateNewExam()
        {
            ActualExam = new XExamVM();
            ActualExam.ExamType = SelectedExamType; //new XExamTypeVM(this.SelectedExamType.ExType);

            ActualExam.Date = DateTime.Now;
            _client.ExamsObsCollection.Add(ActualExam);

            RaisePropertyChanged("ActualExam");
        }

        public void SaveExam()
        {
            Dal.DbContext.SaveChanges();
        }

        public void AddNewExamToPlan()
        {
            ActualExam = new XExamVM();
            ActualExam.ExamType = SelectedExamType;
            ActualExam.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minutes, 0);

            _client.ExamsObsCollection.Add(ActualExam);
            Dal.DbContext.SaveChanges();
        }

        #endregion


        #region eventHandlers

        private void View_CurrentChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged("SelectedExamType");
        }

        #endregion

        #region commands 

        public XCommand CreateNewExamCommand { get; set; }
        public XCommand SaveExamCommand { get; set; }
        public XCommand AddNewExamToPlanCommand { get; set; }




        #endregion

    }
}
