using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    // Domain determina la arquitectura fundamental de la aplicación.
    // Por eso los contratos de acceso a datos (intrefaces) son parte del Domain, no de la Infraestructura.
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        ICollection<Reservation> GetAllReservationUser(int idUser);
        User GetByEmail(string email);
    }
}