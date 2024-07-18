using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISneakerService
    {
        List<SneakerDto> GetSneaker();
        SneakerDto GetById(int id);
        SneakerDto Create(SneakerDto sneakerDto);
        void Update(SneakerDto sneakerDto, int id);
        void DeleteById(int id);
        List<SneakerDto> GetByBrand(string brand);
        List<SneakerDto> GetByCategory(string category);
        bool SneakerExists(string name, string brand, string category);
    }
}
