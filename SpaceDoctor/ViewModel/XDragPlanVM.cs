using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    public class XDragPlanVM
    {

        #region fields
        readonly XDragPlan _dragPlan;
        readonly XDragKitVM _dragKit;


        #endregion

        #region ctors
        public XDragPlanVM() : this(new XDragPlan())
        {

        }

        public XDragPlanVM(XDragPlan dragPlan)
        {
            _dragPlan = dragPlan;
            _dragKit = new XDragKitVM(dragPlan.DragKit);
      
        }

        public XDragPlanVM(XDragKitVM dragKit)
        {
            
            _dragPlan = new XDragPlan();
            
            _dragKit = dragKit;
            _dragPlan.DragKit = dragKit.DragKit;
            Date = DateTime.Now;

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

        public String TimePlan
        {
            get
            {
                return Date.ToString("H:mm");
            }
        }

        public  XDragKitVM DragKit
        {
            get
            {
                return _dragKit;
            }
        }

        public String NameKit
        {
            get
            {
                return DragKit.Name;
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
