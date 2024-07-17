using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IRepositoryReservation : IRepositoryBase<Reservation>
    {
        void AddToReservation(Sneaker sneaker, int reservationId, int quantity);
        void FinalizedReservation(Reservation reservation);
        ICollection<Reservation>? GetAllReservation();
        Reservation? GetReservationById(int id);
        public ICollection<Reservation> GetActiveReservations();
    }
}