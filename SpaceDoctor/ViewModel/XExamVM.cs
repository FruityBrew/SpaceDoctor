using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    public class XExamVM 
    {
        readonly XExam _exam;
        ObservableCollection<XParam> _paramsObsCollection;
        CollectionViewSource _paramCVS;

        public XExamVM()
        {
            _exam = new XExam();
            _paramsObsCollection = new ObservableCollection<XParam>(this.ParamsCollection);
            _paramCVS = new CollectionViewSource();

            _paramCVS.Source = _paramsObsCollection;


        }

        public XExamVM(XExam exam)
        {
            _exam = exam;

            _paramsObsCollection = new ObservableCollection<XParam>(this.ParamsCollection);
            _paramCVS = new CollectionViewSource();

            _paramCVS.Source = _paramsObsCollection;

        }

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
                return Type.Name;
            }
        }

        public ICollectionView ParamCVSView
        {
            get
            {
                return _paramCVS.View;
            }
        }

        private ICollection<XParam> ParamsCollection 
        {
            get 
            {
                return Exam.ParamsCollection;
            }
        }

        public ObservableCollection<XParam> ParamsObsCollection
        {
            get
            {
                return _paramsObsCollection;
            }
        }

        public XExamsType Type
        {
            get 
            {
                return Exam.ExamType;
             }

             set
             {
                Exam.ExamType = value;
             }
        } 

        public DateTime Date
        {
            get { return Exam.Date; }
        }
    }
}
