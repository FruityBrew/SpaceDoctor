using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpaceDoctor.ViewModel
{
    internal class XClientVM : XViewModelBase
    {
       readonly XClient _client;

        ObservableCollection<XExamVM> _examsObsCollection;

        CollectionViewSource _examsCVS; 

       public XClientVM()
       {
            _client = new XClient();         

       }

       public XClientVM(XClient client)
       {
            _client = client;

            _examsObsCollection = new ObservableCollection<XExamVM>();

            foreach(var v in this.ExamsCollection)
            {
                ExamsObsCollection.Add(new XExamVM(v));
            }

            _examsCVS = new CollectionViewSource();
            _examsCVS.Source = _examsObsCollection;
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

            set
            {
                _examsObsCollection = value;
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
    }
}
