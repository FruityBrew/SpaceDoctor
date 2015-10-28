using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    class XDragPlanVM
    {

        #region fields
        readonly XDragPlan _dragPlan;
        readonly XDragKitVM _dragKit;


        #endregion

        #region ctors
        public XDragPlanVM(XDragPlan dragPlan = default(XDragPlan))
        {
            _dragPlan = dragPlan;
            _dragKit = new XDragKitVM(dragPlan.DragKit);
        }

        #endregion

        #region properties
        public XDragPlan DragPlan
        {
            get
            {
                return _dragPlan;
            }
        }


        public DateTime Date
        {
            get
            {
                return DragPlan.Date;
            }
            set
            {

                DragPlan.Date = value;
                //RaisePropertyChanged("Date");
            }
        }

        public String TimeExam
        {
            get
            {
                return Date.ToString("H:mm");
            }
        }

        internal XDragKitVM DragKit
        {
            get
            {
                return _dragKit;
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
