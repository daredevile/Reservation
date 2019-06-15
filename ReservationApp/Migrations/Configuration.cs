namespace ReservationApp.Migrations
{
    using ReservationApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReservationApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReservationApp.Models.ApplicationDbContext context)
        {
            IList<RoomType> defaultRoomTypes = new List<RoomType>();

            defaultRoomTypes.Add(new RoomType() { Name = "Superior", price =100 });
            defaultRoomTypes.Add(new RoomType() { Name = "Suite", price =200  });
            defaultRoomTypes.Add(new RoomType() { Name = "Family", price =300 });
            defaultRoomTypes.Add(new RoomType() { Name = "Villas", price =400 });

            context.RoomTypes.AddRange(defaultRoomTypes);

            IList<Room> defaultRooms = new List<Room>();

            defaultRooms.Add(new Room() { Name = "Room 1" });
            defaultRooms.Add(new Room() { Name = "Room 2"});
            defaultRooms.Add(new Room() { Name = "Room 3" });
            defaultRooms.Add(new Room() { Name = "Room 4", });
            defaultRooms.Add(new Room() { Name = "Room 5", });
                           
            context.Rooms.AddRange(defaultRooms);

            base.Seed(context);
        }
    }
}
