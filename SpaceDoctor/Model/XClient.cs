using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Attr = System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SpaceDoctor.Model
{

    [Attr.Schema.Table("Clients")]
   public class XClient
    {   
        [Attr.Key]
        [Attr.Required]
        [Attr.Schema.Column("Id", TypeName = "int")]
        public Int32 Id { get; set; }

        [Attr.Schema.Column("Name", TypeName = "nvarchar")]
        [Attr.DataType(Attr.DataType.Text)]
        [DisplayName("Имя")]
        public String Name {get; set;}

        [Attr.Schema.Column("DateBirthday", TypeName = "datetime2")]
        [Attr.DataType(Attr.DataType.Date)]
        [DisplayName("Дата рождения")]
        public DateTime DateBirthday { get; set; }


        [DisplayName("Обследования")]
        public ICollection<XExam> ExamsCollection { get; set; }


        public ICollection<XDragPlan> DragPlanCollection { get; set; }


       
        public XClient()
        {
            ExamsCollection = new Collection<XExam>();            
        }
    }
}
