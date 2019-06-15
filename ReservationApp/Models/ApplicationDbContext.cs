using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReservationApp.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<RoomType> RoomTypes { get; set; }

        public System.Data.Entity.DbSet<Reservation> Reservations { get; set; }

       
    }
}
