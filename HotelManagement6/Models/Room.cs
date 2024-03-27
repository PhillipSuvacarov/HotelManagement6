using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }

        public virtual Roomtype RoomType { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
