using Application.Models;
using Application.Models.Requests;
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
        SneakerDto Create(SneakerCreateRequest sneakerDto);
        void Update(SneakerCreateRequest sneakerDto, int id);
        void DeleteById(int id);
        List<SneakerDto> GetByBrand(string brand);
        List<SneakerDto> GetByCategory(string category);
    }
}
