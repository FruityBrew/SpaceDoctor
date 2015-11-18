using System;
using System.Collections.Generic;
using Attr = System.ComponentModel.DataAnnotations;


namespace SpaceDoctor.Model
{
    public class XDrag
    {
        public Int32 Id { get; set; }

        [Attr.Required]
        public String Name { get; set; }

        public String Form { get; set; }

        public ICollection<XDragKit> DragKitCollection { get; set; }
    }
}
