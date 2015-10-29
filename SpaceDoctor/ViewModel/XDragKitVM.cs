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
    public class XDragKitVM
    {
        #region fields

        readonly XDragKit _dragKit;
        readonly ObservableCollection<XDragVM> _dragsObsCollection;
        readonly CollectionViewSource _dragsCVS;

        #endregion

        #region ctors
        public XDragKitVM() : this(new XDragKit())
        {
            
        }

        public XDragKitVM(XDragKit dragKit)
        {
            _dragKit = dragKit;
            _dragsObsCollection = new ObservableCollection<XDragVM>();
            _dragsCVS = new CollectionViewSource();

            foreach (var v in DragKit.DragCollection)
                _dragsObsCollection.Add(new XDragVM(v));

            _dragsObsCollection.CollectionChanged += _dragsObsCollection_CollectionChanged;

            _dragsCVS.Source = DragsObsCollection;
        }

        #endregion

        #region properties

        public XDragKit DragKit
        {
            get
            {
                return _dragKit;
            }
        }

        private ObservableCollection<XDragVM> DragsObsCollection
        {
            get
            {
                return _dragsObsCollection;
            }
        }

        public ICollectionView DragsCVSView
        {
            get
            {
                return _dragsCVS.View;
            }
        }

        public String Name
        {
            get 
            {
                return DragKit.Name;
            }
            set
            {
                DragKit.Name = value;
            }
        }


        #endregion

        #region commands
        #endregion

        #region methods

        internal void AddDragToKit(XDragVM drag)
        {
            this.DragsObsCollection.Add(drag);
        }

        #endregion

        #region eventHandlers

        private void _dragsObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                DragKit.DragCollection.Add(((XDragVM)e.NewItems[0]).Drag);
            }
        }

        #endregion

    }
}
