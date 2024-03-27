using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Guestreservationasc
    {
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public bool PrimaryContact { get; set; }

        public virtual Guest Guest { get; set; } = null!;
        public virtual Reservation Reservation { get; set; } = null!;
    }
}
