﻿using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SneakerService : ISneakerService
    {
        private IRepositorySneaker _repositorySneaker;
        private IRepositoryReservation _repositoryReservation;

        public SneakerService(IRepositorySneaker repositorySneaker, IRepositoryReservation respositoryReservation)
        {
            _repositoryReservation = respositoryReservation;
            _repositorySneaker = repositorySneaker;
        }

        //Validar que no se pueda crear una zapatilla con la misma info
        public bool SneakerExists(string name, string brand, string category)
        {
            return _repositorySneaker.GetAll().Any(sneaker => sneaker.Name == name && sneaker.Brand.ToString() == brand && sneaker.Category.ToString() == category);
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

        public SneakerDto Create(SneakerCreateRequest sneakerDto)
        {
            //Valida la marca
            if (!Enum.TryParse<Sneaker.SneakerBrand>(sneakerDto.Brand.ToString(), true, out var brand) || !Enum.IsDefined(typeof(Sneaker.SneakerBrand), brand))
            {
                throw new ArgumentException("Brand no es una opción válida");
            }

            //Valida la categoria
            if (!Enum.TryParse<Sneaker.SneakerCategory>(sneakerDto.Category.ToString(), true, out var category) || !Enum.IsDefined(typeof(Sneaker.SneakerCategory), category))
            {
                throw new ArgumentException("Category no es una opción válida");
            }

            //Valida que ya exista una zapatiila con la misma info
            if (SneakerExists(sneakerDto.Name, sneakerDto.Brand.ToString(), sneakerDto.Category.ToString()))
            {
                throw new Exception("Ya existe una zapatilla con esa informacion");
            }

            var sneaker = new Sneaker()
            {
                Name = sneakerDto.Name,
                Brand = sneakerDto.Brand,
                Category = sneakerDto.Category,
                Price = sneakerDto.Price,
                Stock = sneakerDto.Stock,
            };

            _repositorySneaker.Add(sneaker);
            return SneakerDto.Create(sneaker);
        }

        public void Update(SneakerCreateRequest sneakerDto, int id)
        {
            var obj = _repositorySneaker.GetById(id)
                ?? throw new Exception("No se encontro la zapatilla");

            //Valida la marca
            if (!Enum.TryParse<Sneaker.SneakerBrand>(sneakerDto.Brand.ToString(), true, out var brand) || !Enum.IsDefined(typeof(Sneaker.SneakerBrand), brand))
            {
                throw new ArgumentException("Brand no es una opción válida");
            }

            //Valida la categoria
            if (!Enum.TryParse<Sneaker.SneakerCategory>(sneakerDto.Category.ToString(), true, out var category) || !Enum.IsDefined(typeof(Sneaker.SneakerCategory), category))
            {
                throw new ArgumentException("Category no es una opción válida");
            }

            if (SneakerExists(sneakerDto.Name, sneakerDto.Brand.ToString(), sneakerDto.Category.ToString()))
            {
                throw new Exception("Ya existe una zapatilla con esa informacion");
            }

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
