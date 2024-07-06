using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Domain.Entities.Reservation;

namespace Application.Models.Requests
{
    public class ReservationCreateRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationState State { get; set; }

        public int UserId { get; set; }
    }
}
