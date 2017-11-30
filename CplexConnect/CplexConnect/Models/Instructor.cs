using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace CplexConnect.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [DisplayName("Instructor ID")]
        
        public string InstructorID { get; set; }
        [Required]
        [DisplayName("First Name")]
       
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        

        [DisplayName("Phone Number")]
   
        public string PhoneNum { get; set; }
        [DisplayName("Is Clinical?")]
        public bool IsClinical { get; set; }
        [DisplayName("Max Course Load")]
        public int MaxCourseLoad { get; set; }
       
        [DisplayName("Primary Program")]
        public string PrimaryProgram { get; set; }
        [Required]
 
        [DisplayName("Assigned Course Load")]  
        [Editable(false)]  
        public int AssignCourseLoad { get; set; }
        [DisplayName("MW(F)")]
        public int CourseDist { get; set; }
        [DisplayName("TR")]
        public int CourseDist2 { get; set; }

    }
}