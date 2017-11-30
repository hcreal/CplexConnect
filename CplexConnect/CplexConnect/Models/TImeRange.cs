using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CplexConnect.Models
{
    public class TimeRange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Range { get; set;}
        public string Periods { get; set; }
        public virtual List<Periods> PeriodsList { get; set; }
    }
}
