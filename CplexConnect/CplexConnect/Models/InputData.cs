using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class InputData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Semester_Id { get; set; }
        public int Course_Id { get; set; }
        public int Instructor_Id { get; set; }
        public string CourseName { get; set; }
        public string SemesterName { get; set; }

    }
}