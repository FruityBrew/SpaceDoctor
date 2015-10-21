using SpaceDoctor.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    public class XMainWndVM : XViewModelBase
    {

        #region fields

        readonly XClientVM _client;
        readonly DAL _dal;
        readonly ObservableCollection<XExamTypeVM> _examTypesObsCollection;
        readonly CollectionViewSource _examTypesCVS;
        ICollection<XDrag> _dragCollection;
        XExamVM _actualExam;
        //XCommand CreateNewExam;

        #endregion 


        #region ctors
        public XMainWndVM()
        {
            _dal = new DAL();
 
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


                CreateNewExamCommand = new XCommand(CreateNewExam);
                SaveExamCommand = new XCommand(SaveExam);


            
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

        #endregion

        #region methods

        public void CreateNewExam()
        {
            ActualExam.ExamType = this.SelectedExamType;
            
            foreach(var v in this.SelectedExamType.ParamTypeaObsCollection) // перенести в конструктор
            {
                ActualExam.AddParams(new XParamVM(v));
            }


            _client.ExamsObsCollection.Add(ActualExam);
            _client.Client.ExamsCollection.Add(ActualExam.Exam); //перенести метод Add ДАЛ

      
        }

        public void SaveExam()
        {
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



        #endregion

    }
}
