using System;
using System.Collections.Generic;
using Attr = System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SpaceDoctor.Models
{
    [Attr.Schema.Table("Exams")] 
   public class XExam
    {
        [Attr.Key]
        public Int32 Id { get; set; }

        [Attr.Schema.ForeignKey("Client")]
        [Attr.Required]
        public Int32 ClientId { get; set; }
        public XClient Client { get; set; }

        [Attr.DataType(Attr.DataType.Date)]
        [DisplayName("Дата обследования")]
        public DateTime Date { get; set; }

        public ICollection<XObjParam> ObjParamsCollection { get; set; }
        public ICollection<XBloodParam> BloodParamsCollection { get; set; }


        public XExam()
        {
            ObjParamsCollection = new Collection<XObjParam>();
            BloodParamsCollection = new Collection<XBloodParam>();
        }
    }
}
