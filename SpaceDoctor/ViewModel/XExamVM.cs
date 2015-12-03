using SpaceDoctor.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

/***********************************************
    Wrapper for the XExam class.

    It contains a collection of related 
     parameters (XParamVM).
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
    public class XExamVM : XViewModelBase
    {

        #region fields
        readonly XExam _exam;
        XExamTypeVM _examType;
        readonly ObservableCollection<XParamVM> _paramsObsCollection;
        readonly CollectionViewSource _paramCVS;
        #endregion


        #region ctors
        public XExamVM() : this (new XExam()) 
        {

        }

        public XExamVM(XExamTypeVM examType) : this()
        {
            _examType = examType;
            _exam.ExamType = examType.ExType;

            foreach (var v in examType.ParamTypeaObsCollection)
                this.AddParams(new XParamVM(v));
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
                RaisePropertyChanged("Date");
            }
        }

        public String TimeExam
        {
            get 
            { 
                return Date.ToString("H:mm");
            }
        }

        public XExamTypeVM ExamType
        {
            get
            {
                return _examType;
            }
        }

        internal ObservableCollection<XParamVM> ParamsObsCollection
        {
            get
            {
                return _paramsObsCollection;
            }
        }

        internal void AddParams(XParamVM paramVM)
        {
            if (paramVM == null || paramVM.Param == null)
                throw new ArgumentNullException();
            this._paramsObsCollection.Add(paramVM);
            this.Exam.ParamsCollection.Add(paramVM.Param);
        }

        #endregion
    }
}
