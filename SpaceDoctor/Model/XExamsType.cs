using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    public class XExamsType
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public ICollection<XParamsType> ParamsCollection { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public XExamsType()
        {
            ParamsCollection = new Collection<XParamsType>();
        }
    }
}
