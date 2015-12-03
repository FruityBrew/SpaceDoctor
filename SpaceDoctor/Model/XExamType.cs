using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Attr = System.ComponentModel.DataAnnotations;

namespace SpaceDoctor.Model
{
    public class XExamType
    {
        public Int32 Id { get; set; }

        [Attr.Required]
        public String Name { get; set; }

        public ICollection<XParamType> ParamsCollection { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public XExamType()
        {
            ParamsCollection = new Collection<XParamType>();
        }
    }
}
