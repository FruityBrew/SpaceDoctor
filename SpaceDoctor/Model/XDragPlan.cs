using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
   public  class XDragPlan
    {
        public Int32 Id { get; set; }

        public DateTime Date { get; set; }

        public XDragKit DragKit { get; set; }

    }
}
