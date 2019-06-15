using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReservationApp.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Index]
        [Required]
        public long RoomType_Id { get; set; }
        [ForeignKey("RoomType_Id")]
        public virtual RoomType RoomType { get; set; }
        [Index]
        [Required]
        public long Room_Id { get; set; }
        [ForeignKey("Room_Id")]
        public virtual Room Room { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        
        public DateTime From { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        
        public DateTime To { get; set; }
        
        
        [Display(Name = "Number Of Days")]
        public int NoOfDays { get; set; }
        [Display(Name = "Price")]
        public double TotallPrice { get; set; }
    }
}