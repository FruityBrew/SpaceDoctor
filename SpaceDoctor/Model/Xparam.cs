using System;

namespace SpaceDoctor.Model
{
    public class XParam
    {
        public Int32 Id { get; set; }
              
        public Double? Value { get; set; }

        public XParamType Type { get; set; }

        public XExam Exam { get; set; }
    }
}
