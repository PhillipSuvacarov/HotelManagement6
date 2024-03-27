using System;
using System.Collections.Generic;

namespace HotelManagement6.Models
{
    public partial class Guest
    {
        public Guest()
        {
            Guestreservationascs = new HashSet<Guestreservationasc>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? GovId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        public virtual ICollection<Guestreservationasc> Guestreservationascs { get; set; }
    }
}
