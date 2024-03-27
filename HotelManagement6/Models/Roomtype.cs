using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Roomtype
    {
        public Roomtype()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public string BedSize { get; set; } = null!;
        public int NumberOfBeds { get; set; }
        public bool Handicap { get; set; }
        public bool? Suite { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
