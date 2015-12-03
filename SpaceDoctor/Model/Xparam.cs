using System;
using Attr = System.ComponentModel.DataAnnotations;

namespace SpaceDoctor.Model
{
    public class XParam
    {
        public Int32 Id { get; set; }
              
        public Double? Value { get; set; }

        [Attr.Required]
        public XParamType Type { get; set; }

        [Attr.Required]
        public XExam Exam { get; set; }
    }
}
