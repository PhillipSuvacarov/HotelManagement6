using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Reservationpayment
    {
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentDetails { get; set; } = null!;
        
        public int Id { get; set; }

        public virtual Reservation Reservation { get; set; } = null!;
    }
}
