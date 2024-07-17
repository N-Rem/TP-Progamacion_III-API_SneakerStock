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
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly ApplicationContext _context;
        public RepositoryUser(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<Reservation>? GetAllReservationUser(int idUser)
        {
            //se cambia la manerea de traer la lista de todas las reservaciones del usuario. 
            var reservations = _context.Reservations.Include(r => r.User).Include(r => r.ReservationSneakers).ThenInclude(rs => rs.Sneaker).Where(r => r.IdUser == idUser && r.State == Reservation.ReservationState.Active).ToList()
                 ?? throw new Exception("No se encontro reservas del usuario");
            return reservations;
        }

        public User GetByEmail(string email)
        {
            var user = _context.users.FirstOrDefault(u => u.EmailAddress == email);
            return user;
        }

    }
}