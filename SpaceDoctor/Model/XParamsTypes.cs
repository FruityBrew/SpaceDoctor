using System;
using System.Collections.Generic;
using System.Linq;

using Attr = System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SpaceDoctor.Model
{
    public class XParamsTypes
    {
        public Int32 Id { get; set; }

        [Attr.DataType(Attr.DataType.Text)]
        [DisplayName("Название")]
        public String Name { get; set; }

        [Attr.DataType(Attr.DataType.Text)]
        [DisplayName("Единица измерения")]
        public String Measure { get; set; }
 
    }
}