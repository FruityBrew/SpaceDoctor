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
        


        ObservableCollection<XDragPlan> _dragPlanObsCollection;

        CollectionViewSource _dragPlanCVS;

        #endregion


        #region ctors
        public XClientVM()
       {
            _client = new XClient();         
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
            _examsCVS.Filter += _examsCVS_Filter;


            _todayExamsCVS = new CollectionViewSource();
            _todayExamsCVS.Source = TodayExamsCollection();
            _todayExamsCVS.View.CurrentChanged += ViewTodayExams_CurrentChanged1;

            _planExamsCVS = new CollectionViewSource();
            _planExamsCVS.Source = PlanExamsCollection();
            _planExamsCVS.View.CurrentChanged += ViewPlanExam_CurrentChanged;
            

            DragPlanObsCollection = new ObservableCollection<XDragPlan>(this.Client.DragPlanCollection);
            _dragPlanCVS = new CollectionViewSource();
            _dragPlanCVS.Source = this.DragPlanObsCollection;

         //   DeleteExamFromPlanCommand = new XCommand(DeleteExamFromPlan);

            _examsCVS.View.Refresh();
       }






        #endregion

        #region properties
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

        private ObservableCollection<XDragPlan> DragPlanObsCollection
        {
            get
            {
                return _dragPlanObsCollection;
            }

            set
            {
                _dragPlanObsCollection = value;
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

        internal void DeleteExam(XExamVM exam)
        {
            exam.ParamsObsCollection.Clear();
            this.ExamsObsCollection.Remove(exam);
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
                TodayExamsCVSView.Refresh();
                PlanExamsCVSView.Refresh();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                this.Client.ExamsCollection.Remove(((XExamVM)e.OldItems[0]).Exam);
                TodayExamsCVSView.Refresh();
                PlanExamsCVSView.Refresh();
            }
        }

        private void ViewTodayExams_CurrentChanged1(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromToday");
        }


        private void ViewPlanExam_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromPlan");
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
