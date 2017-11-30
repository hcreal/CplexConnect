using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Course { get; set; }
        [DisplayName("Section Number")]
        public string SectionNumbers { get; set; }
        public string Semester { get; set; }
        [DisplayName("Online?")]
        public bool IsOnline { get; set; }
        [DisplayName("Attribute")]
        public string RoomReq { get; set; }
        [DisplayName("Capacity")]
        public int SectionCapacity { get; set; }
        public string Program { get; set; }
        [DisplayName("Time Range")]
        public string TimeRange { get; set; }


    }
}  