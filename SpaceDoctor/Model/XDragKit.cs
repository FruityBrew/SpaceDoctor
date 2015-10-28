using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaceDoctor.Model
{
    public class XDragKit
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public ICollection<XDrag> DragCollection { get; set; }

        public XDragKit()
        {
            DragCollection = new Collection<XDrag>();
        }
    }
}
