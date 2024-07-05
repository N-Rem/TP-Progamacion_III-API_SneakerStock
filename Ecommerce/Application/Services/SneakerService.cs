using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SneakerServices : ISneakerServices
    {
        private IRepositorySneaker _repositorySneaker;
        private IRepositoryReservation _repositoryReservation;

        public SneakerServices(IRepositorySneaker repositorySneaker, IRepositoryReservation respositoryReservation)
        {
            _repositoryReservation = respositoryReservation;
            _repositorySneaker = repositorySneaker;
        }

        //CRUD ----- Sneaker
        public List<SneakerDto> GetSneaker()
        {
            return SneakerDto.CreateList(_repositorySneaker.GetAll());
        }

        public SneakerDto GetById(int id)
        {
            var obj = _repositorySneaker.GetById(id)
                 ?? throw new Exception("No encontrado");

            var objDto = SneakerDto.Create(obj);

            return objDto;
        }

        public SneakerDto Create(SneakerDto sneakerDto)
        {
            var sneaker = new Sneaker()
            {
                Id = sneakerDto.Id,
                Name = sneakerDto.Name,
                Brand = sneakerDto.Brand,
                Category = sneakerDto.Category,
                Price = sneakerDto.Price,
                Stock = sneakerDto.Stock,
            };

            _repositorySneaker.Add(sneaker);
            return sneakerDto;
        }

        public void Update(SneakerDto sneakerDto, int id)
        {
            var obj = _repositorySneaker.GetById(id)
                ?? throw new Exception("No se encontro la zapatilla");

            obj.Name = sneakerDto.Name;
            obj.Brand = sneakerDto.Brand;
            obj.Category = sneakerDto.Category;
            obj.Price = sneakerDto.Price;
            obj.Stock = sneakerDto.Stock;

            _repositorySneaker.Update(obj);
        }

        public void DeleteById(int id)
        {
            var obj = _repositorySneaker.GetById(id);
            if (obj == null)
            {
                throw new Exception("no encontrado");
            }
            _repositorySneaker.Delete(obj);
        }

        public List<SneakerDto> GetByBrand(string brand)
        {
            var listObj = _repositorySneaker.GetByBrand(brand)
                ?? throw new Exception("Marca no encontrada");

            return SneakerDto.CreateList(listObj);
        }

        public List<SneakerDto> GetByCategory(string category)
        {
            var listObj = _repositorySneaker.GetByCategory(category)
                ?? throw new Exception("Categoria no encontrada");
            return SneakerDto.CreateList(listObj);
        }
    }
}
