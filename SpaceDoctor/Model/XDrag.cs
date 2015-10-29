using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    public class XDrag
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Form { get; set; }

        public ICollection<XDragKit> DragKitCollection { get; set; }
    }
}
