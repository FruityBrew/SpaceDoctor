using System;
using Attr = System.ComponentModel.DataAnnotations;

namespace SpaceDoctor.Model
{
   public  class XDragPlan
    {
        public Int32 Id { get; set; }

        [Attr.Required]
        public DateTime Date { get; set; }

        [Attr.Required]
        public XDragKit DragKit { get; set; }

        public XDragPlan()
        {
            DragKit = new XDragKit();
        }

    }
}
