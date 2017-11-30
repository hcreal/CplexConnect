using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class ScheduleViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual List<Section> SectionList { get; set; }
        public virtual List<Instructor> InstructorList { get; set; }
        public virtual List<InputData> ModelToList { get; set; }
        public virtual List<Tuple<int,int>> CourseAndInstr {get; set;}
        public List<Tuple<Section,Instructor>> CombinedList { get; set; }
        public virtual int SemesterNumber { get; set; }

    }
}