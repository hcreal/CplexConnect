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
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Room ID")]
        public string RoomID { get; set; }
        [DisplayName("Room Capacity")]
        public int RoomCapacity { get; set; }
        [DisplayName("Room Attribute")]
        public string RoomAttribute { get; set; }
        public string Building { get; set; }
        public virtual List<Buildings> BuildingList { get; set; }

    }
}