using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SpaceDoctor.Models
{
    public class XBloodParam
    {
        public Int32 ID
        { get; set; }

        public Double Value
        { get; set; }

        public XParams Param
        { get; set; }

        public XExam Exam
        { get; set; }
    }
}