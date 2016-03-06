using SpaceDoctor.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

/***********************************************
    Wrapper for the XDragKit class.
    It contains a collection of related 
    drags (XDragVM).

    ----------------------------------------
    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
************************************************/

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
            if (dragKit == null)
                throw new ArgumentNullException("Параметр dragKit не может быть null");

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

        internal ObservableCollection<XDragVM> DragsObsCollection
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
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Название набора лекарств не может быть пустым");
                else
                DragKit.Name = value;
            }
        }

        public String DragNamesToString()
        {
            StringBuilder names = new StringBuilder();
            foreach (var v in DragsObsCollection)
                names.Append(v.Name);
            return names.ToString();
        }

        #endregion

        #region commands
        #endregion

        #region methods

        internal void AddDragToKit(XDragVM drag)
        {
            if (drag == null)
                throw new ArgumentNullException();       
            else                      
            this.DragsObsCollection.Add(drag);
        }

        internal void DeleteDragFromKit(XDragVM drag)
        {
            if (drag == null)
                throw new ArgumentNullException();
            else
                this.DragsObsCollection.Remove(drag);
        }

        #endregion

        #region eventHandlers

        private void _dragsObsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                DragKit.DragCollection.Add(((XDragVM)e.NewItems[0]).Drag);
            }

            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                DragKit.DragCollection.Remove(((XDragVM)e.OldItems[0]).Drag);
            }
        }

        #endregion

    }
}
