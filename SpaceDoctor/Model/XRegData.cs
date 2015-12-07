using System;
using Attr = System.ComponentModel.DataAnnotations;


namespace SpaceDoctor.Model
{
    [Attr.Schema.Table("RegData")]
    public class XRegData
    {
        public Int32 Id { get; set; }
        public String Login { get; set; }
        public String Pass { get; set; }
        public String Role { get; set; }
        public String CalendarAdress { get; set; }
        public Boolean Synchronize { get; set; }

        public XRegData()
        {
            
        }
    }
}
