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
        public XDragKitVM(XDragKit dragKit = default(XDragKit))
        {
            _dragKit = dragKit;
            _dragsObsCollection = new ObservableCollection<XDragVM>();
            _dragsCVS = new CollectionViewSource();

            foreach (var v in DragKit.DragCollection)
                _dragsObsCollection.Add(new XDragVM(v));

            _dragsCVS.Source = DragsObsCollection;
        }
        #endregion

        #region properties

        private XDragKit DragKit
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
        #endregion

        #region eventHandlers
        #endregion

    }
}
