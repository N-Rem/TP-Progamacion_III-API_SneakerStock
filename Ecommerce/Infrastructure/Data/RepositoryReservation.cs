using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryReservation : RepositoryBase<Reservation>, IRepositoryReservation
    {
        private readonly ApplicationContext _context;
        public RepositoryReservation(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        /*public Reservation AddReservation(Reservation reservation)
        {
            var user = _context.users.FirstOrDefault(r => r.Id == reservation.IdUser);
            reservation.User = user;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }*/

        public ICollection<Reservation>? GetAllReservation()
        {
            var listReservation = _context.Reservations.Include(r => r.User).Include(r => r.ReservationSneakers).ThenInclude(rs => rs.Sneaker).ToList()
            ?? throw new Exception("no se econtraron Reservas");

            return listReservation;
        }

        public Reservation? GetReservationById(int id)
        {
            return _context.Reservations.Include(r => r.ReservationSneakers).ThenInclude(rs => rs.Sneaker).FirstOrDefault(r => r.Id == id);
        }
        public ICollection<Reservation> GetActiveReservations()
        {
            return _context.Reservations
                .Include(r => r.User)
                .Include(c => c.ReservationSneakers) // Incluir los items del carrito
                .ThenInclude(item => item.Sneaker) // Incluir el producto (Pizza)
                .Where(c => c.State == Reservation.ReservationState.Active) // Filtrar por carritos activos
                .ToList();
        }


        public void AddToReservation(Sneaker sneaker, int reservationId, int quantity)
        {

            var reservation = _context.Reservations.Include(r => r.ReservationSneakers).ThenInclude(r => r.Sneaker).FirstOrDefault(r => r.Id == reservationId)
                ?? throw new Exception("no se encontro la Reservation");

            if (reservation.State == Reservation.ReservationState.Finalized)
            {
                throw new Exception("La reservacion esta finalizada");
            }

            var reservationSneaker = reservation.ReservationSneakers.FirstOrDefault(rs => rs.SneakerId == sneaker.Id);
            if (reservationSneaker == null)
            {
                reservation.ReservationSneakers.Add(new ReservationSneaker()
                {
                    ReservationId = reservationId,
                    SneakerId = sneaker.Id,
                    Quantity = quantity
                });
            }
            else
            {
                reservationSneaker.Quantity += quantity;
            }
            _context.SaveChanges();


        }

        public void FinalizedReservation(Reservation reservation)
        {
            reservation.State = Reservation.ReservationState.Finalized;
            _context.SaveChanges();
        }
        
    }
}
