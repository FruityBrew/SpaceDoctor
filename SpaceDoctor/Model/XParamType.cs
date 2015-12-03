using System;
using System.Collections.Generic;
using Attr = System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SpaceDoctor.Model
{
    public class XParamType
    {
        public Int32 Id { get; set; }

        [Attr.DataType(Attr.DataType.Text)]
        [Attr.Required]
        [DisplayName("Название")]
        public String Name { get; set; }

        [Attr.DataType(Attr.DataType.Text)]
        [Attr.Required]
        [DisplayName("Единица измерения")]
        public String Measure { get; set; }
 
        public ICollection<XExamType> ExamsTypeCollection { get; set; }

        public XParamType()
        {
            ExamsTypeCollection = new Collection<XExamType>();
        }
    }
}