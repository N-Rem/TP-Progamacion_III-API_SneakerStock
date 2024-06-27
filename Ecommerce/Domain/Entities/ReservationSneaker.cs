using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReservationSneaker
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int SneakerId { get; set; }
        public Sneaker Sneaker { get; set; }
        public int Quantity { get; set; }
    }
}