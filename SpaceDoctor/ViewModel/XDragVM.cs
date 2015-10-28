using SpaceDoctor.Model;
using System;

namespace SpaceDoctor.ViewModel
{
    class XDragVM
    {

        #region fields

        readonly XDrag _drag;

        #endregion

        #region ctors
        public XDragVM(XDrag drag = default(XDrag))
        {
            _drag = drag;            
        }
        #endregion

        #region properties

        public XDrag Drag
        {
            get
            {
                return _drag;
            }
        }

        public String Name
        {
            get
            {
                return Drag.Name;
            }
            set
            {
                Drag.Name = value;
            }
        }

        public Boolean SelectedToNewKit { get; set; }

        #endregion

        #region commands
        #endregion

        #region methods
        #endregion

        #region eventHandlers
        #endregion

    }
}
