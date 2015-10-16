using SpaceDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    class XExamVM 
    {
        readonly XExam _exam;

        public XExamVM()
        {
            _exam = new XExam();
        }

        public XExamVM(XExam exam)
        {
            _exam = exam;
        }

        public XExam Exam
        {
            get
            {
                return _exam;
            }
        }

        public String Name
        {
            get
            {
                return Exam.
            }
        }

    }
}
