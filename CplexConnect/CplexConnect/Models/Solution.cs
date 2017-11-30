using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class Solution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Room Solution")]
        public int Room { get; set; }
        [DisplayName("Time Solution")]
        public int Time { get; set; }
    }
}