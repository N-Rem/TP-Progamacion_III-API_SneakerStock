using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ReservationSneakerDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int SneakerId { get; set; }
        public SneakerDto Sneaker { get; set; }
        public int Quantity { get; set; }

        public static ReservationSneakerDto Create(ReservationSneaker sneaker)
        {
            var dto = new ReservationSneakerDto();
            dto.Id = sneaker.Id;
            dto.ReservationId = sneaker.ReservationId;
            dto.SneakerId = sneaker.SneakerId;
            dto.Quantity = sneaker.Quantity;
            dto.Sneaker = SneakerDto.Create(sneaker.Sneaker);

            return dto;
        }

        public static List<ReservationSneakerDto> CreateList(IEnumerable<ReservationSneaker> sneakers)
        {
            List<ReservationSneakerDto> listDto = [];
            foreach (var s in sneakers)
            {
                listDto.Add(Create(s));
            }
            return listDto;
        }
    }
}
