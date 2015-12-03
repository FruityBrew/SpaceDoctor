using System;
using System.Collections.Generic;
using Attr = System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SpaceDoctor.Model
{
    [Attr.Schema.Table("Exams")] 
   public class XExam
    {
        [Attr.Key]
        public Int32 Id { get; set; }

        [Attr.Schema.ForeignKey("Client")]
        [Attr.Required]
        public Int32 ClientId { get; set; }

        [Attr.Required]
        public XClient Client { get; set; }

        
        [Attr.DataType(Attr.DataType.Date)]
        [DisplayName("Дата обследования")]
        [Attr.Required]
        public DateTime Date { get; set; }


        //  public String Name { get; set; }

        public ICollection<XParam> ParamsCollection { get; set; }

        [Attr.Required]
        public XExamType ExamType { get; set; }

        public XExam()
        {
            ParamsCollection = new Collection<XParam>();
            ExamType = new XExamType();
        }
    }
}
