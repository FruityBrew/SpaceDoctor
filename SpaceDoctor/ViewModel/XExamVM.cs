using SpaceDoctor.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    public class XExamVM 
    {

        #region fields
        readonly XExam _exam;
        ObservableCollection<XParamVM> _paramsObsCollection;
        CollectionViewSource _paramCVS;
        XExamTypeVM _examType;

        #endregion

        #region ctors
        public XExamVM() 
        {
            _exam = new XExam();

            _paramsObsCollection = new ObservableCollection<XParamVM>();
            _paramCVS = new CollectionViewSource();
            
            _paramCVS.Source = _paramsObsCollection;
            _examType = new XExamTypeVM();

        }

        public XExamVM(XExam exam)
        {
            _exam = exam;
            _paramsObsCollection = new ObservableCollection<XParamVM>();
            
            
            foreach (var v in this.Exam.ParamsCollection)
                _paramsObsCollection.Add(new XParamVM(v));

            _paramCVS = new CollectionViewSource();
            _paramCVS.Source = _paramsObsCollection;

            _examType = new XExamTypeVM(Exam.ExamType);
        }

        #endregion


        #region properties
        public XExam Exam
        {
            get
            {
                return _exam;
            }
        }

        public String Name
        {
            get
            {
                return ExamType.Name;
            }
        }

        public ICollectionView ParamCVSView
        {
            get
            {
                return _paramCVS.View;
            }
        }

        public DateTime Date
        {
            get 
            { 
                return Exam.Date; 
            }
            set 
            {
                Exam.Date = value;
            }
        }

        public XExamTypeVM ExamType
        {
            get
            {
                return _examType;
            }

            internal set
            {
               _examType = value;
                _exam.ExamType = value.ExType;
                foreach (var v in value.ParamTypeaObsCollection)
                    this.AddParams(new XParamVM(v));
            }
        }

        internal void AddParams(XParamVM paramVM)
        {
            this._paramsObsCollection.Add(paramVM);
            this.Exam.ParamsCollection.Add(paramVM.Param);
        }

        #endregion
    }
}
