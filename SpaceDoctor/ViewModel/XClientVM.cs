﻿using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    /****************************************************
    Класс-обёртка для класса XClent модели.
    Обеспечивает взаимодействие между классом XClient Модели и Представлением,
    предоставляя логику оповещения об изменении свойств.
        
    Содержит связанные с клиентом коллекции обследований (XExamVM) 
    и планов приема препаратов (XDragPlanVM), обёрнутые 
    в ObservableCollection и CollectionViewSource. 

    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
    ****************************************************/
    public sealed class XClientVM : XViewModelBase
    {

        #region fields

        readonly XClient _client;

        //Все обследования:
        readonly ObservableCollection<XExamVM> _examsObsCollection;
        readonly CollectionViewSource _examsCVS;

        //Сегодняшние обследования:
        readonly CollectionViewSource _todayExamsCVS;
        //Запланированные обследования:
        readonly CollectionViewSource _planExamsCVS;
        
        //Все планы приема лекарств:
        readonly ObservableCollection<XDragPlanVM> _dragPlanObsCollection;
        readonly CollectionViewSource _dragPlanCVS;

        //Сегодняшние:
        readonly CollectionViewSource _todayDragPlanCVS;
        //Запланированные:
        readonly CollectionViewSource _planDragPlanCVS;


        #endregion


        #region ctors
        public XClientVM() : this (new XClient())
       {
                 
       }

       public XClientVM(XClient client)
       {
            if (client == null)
                throw new ArgumentNullException("Параметр XClient не может быть null");

            _client = client;

            _examsObsCollection = new ObservableCollection<XExamVM>();


            foreach(var v in this.Client.ExamsCollection)
                ExamsObsCollection.Add(new XExamVM(v));

            _examsObsCollection.CollectionChanged += ExamsObsCollection_CollectionChanged;

            _examsCVS = new CollectionViewSource();
            _examsCVS.Source = _examsObsCollection;
            _examsCVS.View.CurrentChanged += ExamsView_CurrentChanged;

            //Фильтр архивных обследований:
           _examsCVS.Filter += _examsCVS_ArchiveFilter;

            

            _todayExamsCVS = new CollectionViewSource();
            _todayExamsCVS.Source = TodayExamsCollection();
            _todayExamsCVS.View.CurrentChanged += TodayExamsView_CurrentChanged;

            _planExamsCVS = new CollectionViewSource();
            _planExamsCVS.Source = PlanExamsCollection();
            _planExamsCVS.View.CurrentChanged += PlanExamView_CurrentChanged;



            _dragPlanObsCollection = new ObservableCollection<XDragPlanVM>();
            foreach (var v in this.Client.DragPlanCollection)
                _dragPlanObsCollection.Add(new XDragPlanVM(v));

            _dragPlanObsCollection.CollectionChanged += DragPlanObsCollection_CollectionChanged;

            _dragPlanCVS = new CollectionViewSource();
            _dragPlanCVS.Source = this.DragPlanObsCollection;

            _todayDragPlanCVS = new CollectionViewSource();
            _todayDragPlanCVS.Source = TodayDragPlan();
            _todayDragPlanCVS.View.CurrentChanged += TodayDragPlanView_CurrentChanged;

            _planDragPlanCVS = new CollectionViewSource();
            _planDragPlanCVS.Source = PlanDragPlan();
            _planDragPlanCVS.View.CurrentChanged += PlanDragPlanView_CurrentChanged;

            ExamsCVSView.Refresh();
       }



        #endregion

        #region properties
        /// <summary>
        /// Возвращает внутренний XClient класса
        /// </summary>
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
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Имя не может быть пустым");
                Client.Name = value;
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
                if (value >= DateTime.Today)
                    throw new ArgumentException("Дата рождения некорректна");
                Client.DateBirthday = value;
            }
        }

        private ObservableCollection<XExamVM> ExamsObsCollection
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

        public ICollectionView TodayExamsCVSView
        {
            get
            {
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

        /// <summary>
        /// Возвращает ICollectionView коллекции Планов приема препаратов на сегодня
        /// </summary>
        public ICollectionView TodayDragPlanCVSView
        {
            get
            {
                return _todayDragPlanCVS.View;
            }
        }

        /// <summary>
        /// Возвращает элемент коллекции, выбранный в контроле Представления
        /// </summary>
        public XDragPlanVM SelectedDragPlanFromToday
        {
            get
            {
                return TodayDragPlanCVSView.CurrentItem as XDragPlanVM;
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


        public Boolean IsSynchronizeWithGCalendar
        {
            get
            {
                return Client.RegData.Synchronize;
            }
            set
            {
                Client.RegData.Synchronize = value;
            }
        }

        public String GCalendarAdress
        {
            get
            {
                return Client.RegData.CalendarAdress;
            }
            set
            {
                if (IsSynchronizeWithGCalendar)
                {
                    if (String.IsNullOrEmpty(value))
                        throw new ArgumentException("Адрес не может быть пустым");
                    else
                        Client.RegData.CalendarAdress = value;
                }
                else
                    Client.RegData.CalendarAdress = value;
            }
        }


        #endregion

        #region methods

        /// <summary>
        /// Возвращает перечисление пар <дата-значение> (сортированных по дате) показателей клиента
        /// </summary>
        /// <param name="namePram">Название параметра</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<DateTime, Double>> GetParameterValue(String namePram, DateTime from, DateTime to) //поменять на тип
        {
            IDictionary<DateTime, Double> dict = new Dictionary<DateTime, Double>(50);

            try
            {
                var v = this.ExamsObsCollection.SelectMany(exam => exam.ParamsObsCollection
                .Where(par =>
                { if (par.ParamType.Name == namePram && par.Value.HasValue && par.Param.Exam.Date >= from && par.Param.Exam.Date <= to)
                        return true;
                    else
                        return false; }),
                 (e, p) => new { e.Date, p.Value.Value });


                dict = v.ToDictionary((a => a.Date), (va => va.Value));

                return dict.OrderBy(d => d.Key);
            }

            catch (ArgumentException)
            {
                MessageBox.Show("Запрос не может быть выполнен");
                throw;
            }
  
        }

        private IEnumerable<XExamVM> TodayExamsCollection()
        {
            try
            {
                return from f in _examsObsCollection
                       where f.Date <= DateTime.Today.AddDays(1) && f.Date >= DateTime.Today
                       select f;
            }
            catch (ArgumentNullException argNullEx)
            {
                MessageBox.Show("При выполнении запроса произошла ошибка" + argNullEx);
                throw;
            }
        }

        private IEnumerable<XExamVM> PlanExamsCollection()
        {
            try
            {
                return from f in _examsObsCollection
                       where f.Date >= DateTime.Today
                       select f;
            }
            catch (ArgumentNullException argNullEx)
            {
                MessageBox.Show("При выполнении запроса произошла ошибка" + argNullEx);
                throw;
            }
        }

        private IEnumerable<XDragPlanVM> TodayDragPlan()
        {
            try
            {
                return from f in _dragPlanObsCollection
                       where f.Date <= DateTime.Today.AddDays(1) && f.Date >= DateTime.Today
                       select f;
            }
            catch (ArgumentNullException argNullEx)
            {
                MessageBox.Show("При выполнении запроса произошла ошибка" + argNullEx);
                throw;
            }
        }

        private IEnumerable<XDragPlanVM> PlanDragPlan()
        {
            try 
            {
                return from f in _dragPlanObsCollection
                       where f.Date >= DateTime.Today
                       select f;
            }

            catch (ArgumentNullException argNullEx)
            {
                MessageBox.Show("При выполнении запроса произошла ошибка" + argNullEx);
                throw;
            }
        }

        /// <summary>
        /// Добавляет обследование в коллекцию клиента
        /// </summary>
        /// <param name="exam">Обследование для добавления</param>
        internal void AddExam(XExamVM exam)
        {
            if (exam == null)
                throw new ArgumentException("Аргумент exam не может быть null");

            ExamsObsCollection.Add(exam);
        }

        /// <summary>
        /// Удаляет обследование из коллекции клиента
        /// </summary>
        /// <param name="exam"></param>
        internal void DeleteExam(XExamVM exam)
        {
            if (exam == null)
               throw new ArgumentException("Арумент exam не может быть null");
            exam.ParamsObsCollection.Clear();
            this.ExamsObsCollection.Remove(exam);
        }

        internal void AddDragPlan(XDragPlanVM dragPlan)
        {
            if (dragPlan == null)
                throw new ArgumentException("Аргумент dragPlan не может быть null");

            this._dragPlanObsCollection.Add(dragPlan);
        }


        #endregion

        #region eventHandlers

        private void ExamsView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExam");
        }


        private void ExamsObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

        private void TodayExamsView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromToday");
        }


        private void PlanExamView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedExamFromPlan");
        }

        private void DragPlanObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                this.Client.DragPlanCollection.Add(((XDragPlanVM)e.NewItems[0]).DragPlan);
            }
            TodayDragPlanCVSView.Refresh();
            PlanDragPlanCVSView.Refresh();
        }


        private void PlanDragPlanView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragPlanFromPlan");
        }

        private void TodayDragPlanView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragPlanFromToday");
        }

        /// <summary>
        /// Фильтр по дате-времени исследования. Фильтрует архивные обследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _examsCVS_ArchiveFilter(object sender, FilterEventArgs e)
        {
            
            e.Accepted = (((XExamVM)e.Item).Date != null && ((XExamVM)e.Item).Date < DateTime.Now);
        }

        #endregion


    }
}
