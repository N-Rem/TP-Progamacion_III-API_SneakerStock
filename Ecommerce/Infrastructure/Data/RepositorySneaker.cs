using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositorySneaker : RepositoryBase<Sneaker>, IRepositorySneaker
    {
        private readonly ApplicationContext _context;
        public RepositorySneaker(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<Sneaker>? GetByBrand(string brand)
        {
            var listSneaker = _context.Set<Sneaker>().ToList().Where(sneaker => sneaker.Brand.ToString().Equals(brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if(listSneaker.Count == 0)
            {
                throw new Exception("no se encontro la marca de zapatilla");
            }
            return listSneaker;
        }

        public ICollection<Sneaker>? GetByCategory(string category)
        {
            var listSneaker = _context.Set<Sneaker>().ToList().Where(sneaker => sneaker.Category.ToString().Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (listSneaker.Count == 0)
            {
                throw new Exception("no se encontro la categoria de zapatilla");
            }
            return listSneaker;
        }

    }
}