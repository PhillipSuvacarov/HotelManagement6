using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            Guestreservationascs = new HashSet<Guestreservationasc>();
            Reservationpayments = new HashSet<Reservationpayment>();
            Rooms = new HashSet<Room>();
        }

        public int ReservationId { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Guestreservationasc> Guestreservationascs { get; set; }
        public virtual ICollection<Reservationpayment> Reservationpayments { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
