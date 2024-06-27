
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.Reservation;
using System.Text.Json.Serialization;
using Domain.Interface;

namespace Application.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int IdUser { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationState State { get; set; }

        public UserDto? User { get; set; }

        public ICollection<SneakerDto>? Sneakers { get; set; }



        public static ReservationDto Create(Reservation reservation)
        {
            var dto = new ReservationDto();

            dto.Id = reservation.Id;
            dto.IdUser = reservation.IdUser;
            dto.State = reservation.State;
            dto.User = UserDto.Create(reservation.User);


            var listSneaker = new List<Sneaker>();
            foreach (var rs in reservation.ReservationSneakers)
            {
                for (int i = 1; i <= rs.Quantity; i++)
                {
                    listSneaker.Add(rs.Sneaker);

                }
            }


            dto.Sneakers = SneakerDto.CreateList(listSneaker);
            return dto;
        }

        public static List<ReservationDto> CreateList(IEnumerable<Reservation> reservations)
        {
            List<ReservationDto> listDto = [];
            foreach (var r in reservations)
            {
                listDto.Add(Create(r));
            }
            return listDto;
        }
    }
}