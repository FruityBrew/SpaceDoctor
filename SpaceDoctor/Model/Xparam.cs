using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    public class XParam
    {
        public Int32 Id { get; set; }

        public Double Value { get; set; }

        public XParamsType Type { get; set; }

        public XExam Exam { get; set; }
    }
}
