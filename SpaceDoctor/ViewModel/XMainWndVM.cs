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
       readonly XClientVM _client;

        readonly DAL _dal;

        readonly ObservableCollection<XExamTypeVM> _examTypesObsCollection;
        readonly CollectionViewSource _examTypesCVS;

        

        ICollection<XDrag> _dragCollection;


        public XMainWndVM()
        {
            _dal = new DAL();
            using (Dal.DbContext)
            {
                _examTypesObsCollection = new ObservableCollection<XExamTypeVM>();
                foreach(var v in Dal.ExamTypesCollection)
                {
                    _examTypesObsCollection.Add(new XExamTypeVM(v));
                }
                _examTypesCVS = new CollectionViewSource();
                _examTypesCVS.Source = _examTypesObsCollection;
                _examTypesCVS.View.CurrentChanged += View_CurrentChanged;


                _client = new XClientVM(Dal.ClientCollection.First(cl => cl.Id == 1));

                _dragCollection = new Collection<XDrag>(Dal.DbContext.Drags.ToList());
            }      
        }

        private void View_CurrentChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged("SelectedExamType");
        }

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

    }
}
