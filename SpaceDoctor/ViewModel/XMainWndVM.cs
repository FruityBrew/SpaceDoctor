using SpaceDoctor.DAL;
using SpaceDoctor.Model;
using SpaceDoctor.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    /***************************************************
    Класс ViewModel для mainWnd приложения.
    Обеспечивает взаимодействие между Моделью и Представлением,
    предоставляя логику оповещения об изменении свойств и коллекций.

    Включает ссылку на контекст БД.
    Содержит обертки для коллекций (не связанных с конкретным клиентом)
    ("Все лекарства", "Наборы лекарств", "Типы параметров", "Типы обследований"),
    а также имеет ссылку на текущего клиента и связанные с ним коллекции.

    ***************************************************/

    /// <summary>
    /// View-model class for the MainWindow
    /// </summary>
    public sealed class XMainWndVM : XViewModelBase
    {

       // readonly XGenericRepository<XClient> _clientRepos;
        #region fields

        readonly XClientVM _client;

        //доступ к контексту БД:
        readonly XDAL _dal;

        //Типы обследований:
        readonly ObservableCollection<XExamTypeVM> _examTypesObsCollection;
        readonly CollectionViewSource _examTypesCVS;

        //Типы параметров обследований:
        readonly ObservableCollection<XParamTypeVM> _paramTypesObsCollection;
        readonly CollectionViewSource _paramTypesCVS;

        //Все лекарства:
        readonly ICollection<XDragVM> _dragCollection;
        readonly CollectionViewSource _dragsCVS;

        //Наборы лекарств:
        readonly ObservableCollection<XDragKitVM> _dragKitObsCollection;
        readonly CollectionViewSource _dragKitCVS;

        //Новый тип обследования для добавления:
        XExamTypeVM _newExam;

        //Новый набор лекарств:
        XDragKitVM _newDragKit;


        //Часы и минуты для комбобокса выбра времени:
        readonly ICollection<Int32> _hoursCollection;
        readonly ICollection<Int32> _minutesCollection;
        Int32 _hour;
        Int32 _minutes;

        //Дата для назначения обследования
        DateTime _actualDate;

        //Даты для фильтрации параметров
        DateTime _dateFrom = new DateTime(2000, 1, 1);
        DateTime _dateTo = DateTime.Now;
        #endregion


        #region ctors
        public XMainWndVM()
        {

            try
            {
                _dal = new XDAL("SpaceDoctorDB");

                IEnumerable<XExam> examEnumerable = Dal.GetEntityCollection<XExam>("ParamsCollection");
                IEnumerable<XClient> clientEnumerable = _dal.GetEntityCollection<XClient>("ExamsCollection", "DragPlanCollection", "RegData");
                IEnumerable<XExamType> exTypeEnumerable = _dal.GetEntityCollection<XExamType>("ParamsCollection");
                IEnumerable<XDragKit> drKitEnumerable = _dal.GetEntityCollection<XDragKit>("DragCollection");
                IEnumerable<XParamType> paramType = _dal.GetEntityCollection<XParamType>();
                IEnumerable<XDrag> dragEnum = _dal.GetEntityCollection<XDrag>();


                //заполнить коллекции с типами обследований:
                _examTypesObsCollection = new ObservableCollection<XExamTypeVM>();
                foreach (var v in exTypeEnumerable)
                    _examTypesObsCollection.Add(new XExamTypeVM(v));

                _examTypesCVS = new CollectionViewSource();
                _examTypesCVS.Source = _examTypesObsCollection;
                _examTypesCVS.View.CurrentChanged += View_CurrentChanged;
                _examTypesObsCollection.CollectionChanged += _examTypesObsCollection_CollectionChanged;


                //заполнить коллекции с типами параметров:
                _paramTypesObsCollection = new ObservableCollection<XParamTypeVM>();
                 foreach (var v in paramType)

                _paramTypesObsCollection.Add(new XParamTypeVM(v));
                _paramTypesCVS = new CollectionViewSource();
                _paramTypesCVS.Source = _paramTypesObsCollection;
                _paramTypesCVS.View.CurrentChanged += ParamTypes_CurrentChanged;

                _client = new XClientVM(clientEnumerable.First(cl => cl.Id == 1));


                //Список препаратов
                _dragCollection = new ObservableCollection<XDragVM>();
                foreach (var v in dragEnum)
                    _dragCollection.Add(new XDragVM(v));

                _dragsCVS = new CollectionViewSource();
                _dragsCVS.Source = _dragCollection;

                //список лекарственныхНаборов:
                _dragKitObsCollection = new ObservableCollection<XDragKitVM>();
                foreach (var v in drKitEnumerable)
                _dragKitObsCollection.Add(new XDragKitVM(v));

                _dragKitCVS = new CollectionViewSource();
                _dragKitCVS.Source = _dragKitObsCollection;
                _dragKitCVS.View.CurrentChanged += DragsKitView_CurrentChanged;
                _dragKitObsCollection.CollectionChanged += _dragKitObsCollection_CollectionChanged;
            }

            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Не могу соединиться с сервером");           
                Environment.FailFast("Не могу соединиться с сервером");
            }


            _hoursCollection = new Collection<Int32>();
            for (int i = 0; i < 24; i++)
                _hoursCollection.Add(i);

            _minutesCollection = new Collection<Int32>();
            for (int i = 0; i < 60; i += 5)
                _minutesCollection.Add(i);


            ActualDate = DateTime.Now;

            CreateNewExamCommand = new XCommand(CreateNewExam);
            SaveChangesCommand = new XCommand(SaveChanges);
            AddNewExamToPlanCommand = new XCommand(AddNewExamToPlan);
            CreateNewExamTypeCommand = new XCommand(CreateNewExamType);
            SaveNewExamTypeCommand = new XCommand(SaveNewExamType);
            DeleteExamFromPlanCommand = new XCommand(DeleteExamFromPlan);
            CreateNewDragKitCommand = new XCommand(CreateNewDragKit);
            SaveNewDragKitCommand = new XCommand(SaveNewDragKit);
            AddNewDragPlanCommand = new XCommand(AddNewDragPlan);
            OpenProfileWndCommand = new XCommand(OpenProfileWnd);
          //  AddWndCommand = new XCommand(AddWnd);
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


        internal XDAL Dal
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
                if (value > 23 || value < 0)
                    throw new ArgumentException("Значение не в пределах допустимого диапозона");
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
                if (value > 59 || value < 0)
                    throw new ArgumentOutOfRangeException("Значение не в пределах допустимого диапозона");
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

        public XParamTypeVM SelectedParamType
        {
            get
            {
                return _paramTypesCVS.View.CurrentItem as XParamTypeVM;
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

        private XDragKitVM NewDragKit
        {
            get
            {
                return _newDragKit;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _newDragKit = value;
            }
        }

        /// <summary>
        /// Возвращает или задает начальную дату для фильтрации параметров
        /// </summary>
        public DateTime DateFrom
        {
            get
            {
                return _dateFrom;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _dateFrom = value;
            }
        }

        /// <summary>
        /// Возвращает или задает конечную дату для фильтрации параметров
        /// </summary>
        public DateTime DateTo
        {
            get
            {
                return _dateTo;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _dateTo = value;
            }
        }

        public DateTime ActualDate
        {
            get
            {
                return _actualDate;
            }

            set
            {
                if(value < DateTime.Today)
                    {
                        MessageBox.Show("Некорректная дата. Введите сегодняшнюю или будующую дату");
                    }
                    else
                        _actualDate = value;
            }
        }

        public object FailFast
        {
            get;
            private set;
        }
        #endregion

        #region methods

        /// <summary>
        /// Создает новое обследование для выполнения его "сейчас"
        /// </summary>
        private void CreateNewExam()
        {

            XExamVM newEx = new XExamVM(this.SelectedExamType);
            newEx.Date = DateTime.Now;
            _client.AddExam(newEx);

            ActualDate = DateTime.Now;
        }


        private void AddNewDragPlan()
        {
            XDragPlanVM newDragPlan = new XDragPlanVM(SelectedDragsKit);
            newDragPlan.Date = new DateTime(ActualDate.Date.Year, ActualDate.Date.Month, ActualDate.Date.Day, Hour, Minutes, 0);
            Client.AddDragPlan(newDragPlan);
            SaveChanges();

            if(Client.IsSynchronizeWithGCalendar)
            {
                XGCalendarAccess.CreateNewEvent(Client.GCalendarAdress, newDragPlan.Date, newDragPlan.Date, newDragPlan.NameKit, true, newDragPlan.DragKit.DragNamesToString());
            }

            ActualDate = DateTime.Now;
            RaisePropertyChanged("ActualDate");
        }

        private void SaveChanges()
        {
            try 
            {
                
                Dal.SaveChanges();
            }

            catch(System.Data.Entity.Validation.DbEntityValidationException validationEx)
            {
                MessageBox.Show("Невозможно сохранить изменения" + validationEx.Message);
            }

            catch(System.Data.Entity.Infrastructure.DbUpdateException dbUpdateEx)
            {
                MessageBox.Show("Невозможно сохранить изменения" + dbUpdateEx.Message);
            }

            catch (System.ObjectDisposedException disposedEx)
            {
                Dal.Dispose();
                MessageBox.Show("Невозможно сохранить изменения. Приложение будет перезапущено" + disposedEx.Message);
                System.Windows.Forms.Application.Restart();
            }

            catch (System.InvalidOperationException invalidOpEx)
            {
                Dal.Dispose();
                MessageBox.Show("Невозможно сохранить изменения. Приложение будет перезапущено" + invalidOpEx.Message);
                System.Windows.Forms.Application.Restart();
            }

        }


        /// <summary>
        /// Добавляет новое обследование выбранного типа в План
        /// </summary>
        private void AddNewExamToPlan()
        {

            XExamVM newEx = new XExamVM(this.SelectedExamType);
            newEx.Date = new DateTime(ActualDate.Date.Year, ActualDate.Date.Month, ActualDate.Date.Day, Hour, Minutes, 0);
            Client.AddExam(newEx);
            SaveChanges();

            if(Client.IsSynchronizeWithGCalendar)
            {
                XGCalendarAccess.CreateNewEvent( Client.GCalendarAdress, newEx.Date, newEx.Date, newEx.Name, true);
            }

            ActualDate = DateTime.Now;
            RaisePropertyChanged("ActualDate");
        }

        /// <summary>
        /// Создает новый тип обследования и добавляет в коллекцию
        /// </summary>
        private void CreateNewExamType()
        {
            NewExamType = new XExamTypeVM();
            ExamTypesObsCollection.Add(NewExamType);
        }

        /// <summary>
        /// Добавляет выбранные в контроле Представления параметры к списку типа обследования
        /// </summary>
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
            SaveChanges();

        }

        /// <summary>
        /// Добавляет выбранные  в контроле Представления препараты нового Набора лекарств
        /// </summary>
        private void SaveNewDragKit()
        {
            foreach (var v in _dragCollection)
            {
                if (v.SelectedToNewKit)
                {
                    NewDragKit.AddDragToKit(v);
                    v.SelectedToNewKit = false;
                }
            }

            DragsKitCVSView.Refresh();
            SaveChanges();
        }

        /// <summary>
        /// Удаляет выбранное в контроле Представления Обследование
        /// </summary>
        private void DeleteExamFromPlan()
        {
            Dal.DeleteObject<XExam>(Client.SelectedExamFromPlan.Exam);
            Client.DeleteExam(Client.SelectedExamFromPlan);
            SaveChanges();
        }

        /// <summary>
        /// Создает новый Набор лекарств и добавляет в коллекцию
        /// </summary>
        private void CreateNewDragKit()
        {
            NewDragKit = new XDragKitVM();
            this._dragKitObsCollection.Add(NewDragKit);
        }

        /// <summary>
        /// Возвращает созданное окно с графиком
        /// </summary>
        /// <returns></returns>
        internal XPlotControl  AddWnd()
        {
             return new XPlotControl(CreatePlot());
        }

        private XPlotVM CreatePlot()
        {
           XPlotVM plot = new XPlotVM();

            plot.CreatePlot(Client.GetParameterValue(SelectedParamType.Name, DateFrom, DateTo), SelectedParamType.Name);
            return plot;
        }

        /// <summary>
        /// Открывает окно с данными клиента
        /// </summary>
        private void OpenProfileWnd()
        {
            if (XProfileWindow.CreateXProfileWnd(Client))
                SaveChanges();

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
                Dal.AddObject<XExamType>(((XExamTypeVM)e.NewItems[0]).ExType);
                ExamTypesCVSView.MoveCurrentTo(e.NewItems[0]);
               
                RaisePropertyChanged("SelectedExamType");
            }
        }


        private void DragsKitView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedDragsKit");
        }

        private void _dragKitObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    Dal.AddObject<XDragKit>(((XDragKitVM)e.NewItems[0]).DragKit);
                }
        }


        private void ParamTypes_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("SelectedParamType");
        }


        #endregion

        #region commands 

        public XCommand CreateNewExamCommand { get; set; }
        public XCommand SaveChangesCommand { get; set; }
        public XCommand AddNewExamToPlanCommand { get; set; }
        public XCommand CreateNewExamTypeCommand { get; set; }
        public XCommand SaveNewExamTypeCommand { get; set; }
        public XCommand DeleteExamFromPlanCommand { get; set; }

        public XCommand CreateNewDragKitCommand { get; set; }
        public XCommand SaveNewDragKitCommand { get; set; }
        public XCommand AddNewDragPlanCommand { get; set; }

        public XCommand OpenProfileWndCommand { get; set; }
        #endregion
    }
}
