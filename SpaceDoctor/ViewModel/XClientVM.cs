using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    public class XClientVM : XViewModelBase
    {

        #region fields
        readonly XClient _client;

        ObservableCollection<XExamVM> _examsObsCollection;

        CollectionViewSource _examsCVS;

        readonly CollectionViewSource _todayExamsCVS;
        readonly CollectionViewSource _planExamsCVS;

        readonly ObservableCollection<XDragPlanVM> _dragPlanObsCollection;

        readonly CollectionViewSource _dragPlanCVS;

        readonly CollectionViewSource _todayDragPlanCVS;

        readonly CollectionViewSource _planDragPlanCVS;



        #endregion


        #region ctors
        public XClientVM() : this (new XClient())
       {
                 
       }

       public XClientVM(XClient client)
       {
            _client = client;

            _examsObsCollection = new ObservableCollection<XExamVM>();


            foreach(var v in this.Client.ExamsCollection)
            {
                ExamsObsCollection.Add(new XExamVM(v));
            }

            _examsObsCollection.CollectionChanged += _examsObsCollection_CollectionChanged;

            _examsCVS = new CollectionViewSource();
            _examsCVS.Source = _examsObsCollection;
            _examsCVS.View.CurrentChanged += ViewExams_CurrentChanged;
         //   _examsCVS.Filter += _examsCVS_Filter;


            _todayExamsCVS = new CollectionViewSource();
            _todayExamsCVS.Source = TodayExamsCollection();
            _todayExamsCVS.View.CurrentChanged += ViewTodayExams_CurrentChanged1;

            _planExamsCVS = new CollectionViewSource();
            _planExamsCVS.Source = PlanExamsCollection();
            _planExamsCVS.View.CurrentChanged += ViewPlanExam_CurrentChanged;


            _dragPlanObsCollection = new ObservableCollection<XDragPlanVM>();
            foreach (var v in this.Client.DragPlanCollection)
                _dragPlanObsCollection.Add(new XDragPlanVM(v));

            _dragPlanObsCollection.CollectionChanged += _dragPlanObsCollection_CollectionChanged;

            _dragPlanCVS = new CollectionViewSource();
            _dragPlanCVS.Source = this.DragPlanObsCollection;

            _todayDragPlanCVS = new CollectionViewSource();
            _todayDragPlanCVS.Source = TodayDragPlan();
            _todayDragPlanCVS.View.CurrentChanged += _TodayDragPlanView_CurrentChanged;

            _planDragPlanCVS = new CollectionViewSource();
            _planDragPlanCVS.Source = PlanDragPlan();
            _planDragPlanCVS.View.CurrentChanged += View_CurrentChanged;

          //  _examsCVS.View.Refresh();

            //_dragPlanCVS.View.Refresh();
       }



        #endregion

        #region properties

        public ICollectionView TodayDragPlanCVSView
        {
            get
            {
                return _todayDragPlanCVS.View;
            }
        }

        public XDragPlanVM SelectedDragPlan
        {
            get
            {
                return TodayDragPlanCVSView.CurrentItem as XDragPlanVM;
            }
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


        internal ObservableCollection<XExamVM> ExamsObsCollection
        {
            get
            {
                return _examsObsCollection;
            }

        }

        public ICollectionView ExamsCVSView
        {
            get
            {
                return _examsCVS.View;
            }
        }
     

        public XExamVM SelectedExam
        {
            get
            {
                return ExamsCVSView.CurrentItem as XExamVM;
            }
        }

        private ObservableCollection<XDragPlanVM> DragPlanObsCollection
        {
            get
            {
                return _dragPlanObsCollection;
            }
        }


        public ICollectionView DragPlanCVSView
        {
            get
            {
                return _dragPlanCVS.View;
            }
        }

        public ICollectionView TodayExamsCVSView
        {
            get
            {
               // _examsCVS.Source = TodayExamsCollection;
                return _todayExamsCVS.View;
            }
        }

        public XExamVM SelectedExamFromToday
        {
            get
            {
                return TodayExamsCVSView.CurrentItem as XExamVM;
            }
        }

        public ICollectionView PlanExamsCVSView
        {
            get
            {
                return _planExamsCVS.View;
            }
        }

        public XExamVM SelectedExamFromPlan
        {
            get
            {
                return PlanExamsCVSView.CurrentItem as XExamVM;
            }
        }

        public ICollectionView PlanDragPlanCVSView
        {
            get
            {
                return _planDragPlanCVS.View;
            }
        }

        public XDragPlanVM SelectedDragPlanFromPlan
        {
            get
            {
                return PlanDragPlanCVSView.CurrentItem as XDragPlanVM;
            }
        }


        #endregion

        #region methods

        private IEnumerable<XExamVM> TodayExamsCollection()
        {
            return from f in _examsObsCollection
                   where f.Date.Day == DateTime.Now.Day
                   select f;
        }

        private IEnumerable<XExamVM> PlanExamsCollection()
        {
            return from f in _examsObsCollection
                   where f.Date.Day >= DateTime.Now.Day
                   select f;
        }

        private IEnumerable<XDragPlanVM> TodayDragPlan()
        {
            return from f in _dragPlanObsCollection
                   where f.Date.Day == DateTime.Now.Day
                   select f;
        }

        internal void DeleteExam(XExamVM exam)
        {
            exam.ParamsObsCollection.Clear();
            this.ExamsObsCollection.Remove(exam);
        }

        internal void AddDragPlan(XDragPlanVM dragPlan)
        {
            this._dragPlanObsCollection.Add(dragPlan);
        }

        public IEnumerable<XDragPlanVM> PlanDragPlan()
        {
            return from f in _dragPlanObsCollection
                   where f.Date.Day >= DateTime.Now.Day
                   select f;
        }

        #endregion

        #region eventHandlers

        private void ViewExams_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExam");
        }


        private void _examsObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.Client.ExamsCollection.Add(((XExamVM)e.NewItems[0]).Exam);

            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                this.Client.ExamsCollection.Remove(((XExamVM)e.OldItems[0]).Exam);

            }
            TodayExamsCVSView.Refresh();
            PlanExamsCVSView.Refresh();
        }

        private void ViewTodayExams_CurrentChanged1(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromToday");
        }


        private void ViewPlanExam_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromPlan");
        }

        private void _dragPlanObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.Client.DragPlanCollection.Add(((XDragPlanVM)e.NewItems[0]).DragPlan);
            }
            TodayDragPlanCVSView.Refresh();
        }


        private void View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragPlanFromPlan");
        }

        private void _TodayDragPlanView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragPlan");
        }

        /// <summary>
        /// Фильтр по дате-времени исследования. Фильтрует архивные обследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _examsCVS_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = (((XExamVM)e.Item).Date < DateTime.Now);
        }

        #endregion

        #region commands



        #endregion

    }
}
