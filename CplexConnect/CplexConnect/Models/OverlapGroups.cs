using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class OverlapGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string OverlapGroup { get; set; }
        public string Sections { get; set; }
        public virtual List<Section> SectionList { get; set; }
    }
}