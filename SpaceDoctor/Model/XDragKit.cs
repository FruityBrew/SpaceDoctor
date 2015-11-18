using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Attr = System.ComponentModel.DataAnnotations;


namespace SpaceDoctor.Model
{
    public class XDragKit
    {
        public Int32 Id { get; set; }

        [Attr.Required]
        public String Name { get; set; }

        public ICollection<XDrag> DragCollection { get; set; }

        public XDragKit()
        {
            DragCollection = new Collection<XDrag>();
        }
    }
}
