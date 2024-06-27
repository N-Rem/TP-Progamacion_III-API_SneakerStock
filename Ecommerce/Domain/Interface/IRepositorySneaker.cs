using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepositorySneaker : IRepositoryBase<Sneaker>
    {
        ICollection<Sneaker>? GetByBrand(string brand);
        ICollection<Sneaker>? GetByCategory(string category);

    }
}