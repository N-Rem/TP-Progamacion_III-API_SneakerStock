using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class ReservationSneakerRequest
    {
        public int SneakerId { get; set; }
        public int ReservationId { get; set; }
        public int Quantity { get; set; }
    }
}
