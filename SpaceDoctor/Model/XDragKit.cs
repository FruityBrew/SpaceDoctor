using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    public class XDragKit
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public ICollection<XDrag> DragCollection { get; set; }
    }
}
