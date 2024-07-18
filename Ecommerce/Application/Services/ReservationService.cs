using Application.Interfaces;
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
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryReservation _repositoryReservation;
        private readonly IRepositorySneaker _repositorySneaker;
        public ReservationService(IRepositoryReservation repositoryReservation, IRepositorySneaker repositorySneaker, IRepositoryUser repositoryUser)
        {
            _repositoryReservation = repositoryReservation;
            _repositorySneaker = repositorySneaker;
            _repositoryUser = repositoryUser;
        }

        //Crud reservation
        public void Create(int idUser)
        {
            var user = _repositoryUser.GetById(idUser)
                ?? throw new Exception("Usuario no encontrado");
            var newReservation = new Reservation()
            {
                IdUser = idUser,
                User = user,
                State = Reservation.ReservationState.Active,
            };
            _repositoryReservation.Add(newReservation);

        }

        public void Delete(int id)
        {
            var obj = _repositoryReservation.GetById(id);

            if (obj == null)
            {
                throw new Exception("no encontrado");
            }
            _repositoryReservation.Delete(obj);
        }

        public List<ReservationDto> GetAll()
        {
            var list = _repositoryReservation.GetAllReservation();

            return ReservationDto.CreateList(list);
        }
        public List<ReservationDto> GetActiveReservations()
        {
            var list = _repositoryReservation.GetActiveReservations();

            return ReservationDto.CreateList(list);
        }

        public ReservationDto GetById(int id)
        {
            var obj = _repositoryReservation.GetReservationById(id);
            if (obj == null)
            {
                return null;
            }
            //agregar el usuario al obj reservation.
            var user = _repositoryUser.GetById(obj.IdUser)
                ?? throw new Exception("no se encontro el user");
            obj.User = user;
            return ReservationDto.Create(obj);
        }

        //----------------


        public void FinalizedReservation(int id)
        {
            var obj = _repositoryReservation.GetById(id)
              ?? throw new Exception("");

            _repositoryReservation.FinalizedReservation(obj);
        }



        public void AddToReservation(ReservationSneakerRequest rsDto)
        {
            var sneaker = _repositorySneaker.GetById(rsDto.SneakerId)
                ?? throw new Exception("Sneaker no encontrada");

            if (sneaker.Stock > 0 && rsDto.Quantity < sneaker.Stock)
            {
                //Agrega la zapatilla 
                _repositoryReservation.AddToReservation(sneaker, rsDto.ReservationId, rsDto.Quantity);
            } else
            {
                throw new Exception("No hay Stock");
            }

        }

        public void BuyReservation(int idReservation)
        {
            var reservation = _repositoryReservation.GetReservationById(idReservation)
                ?? throw new Exception("no se encontro la reservacion");
            if (reservation.State == Reservation.ReservationState.Finalized)
            {
                throw new Exception("La reservacion ya esta finalizada");
            }


            //var sneakerInReservation = reservation.ReservationSneakers.Select(rs => rs.Sneaker).ToList();
            foreach (var rs in reservation.ReservationSneakers)
            {
                var sneaker = rs.Sneaker;
                var quantity = rs.Quantity;
                if (sneaker.Stock < quantity)
                {
                    throw new Exception($"No hay suficiente stock de la Zapatilla {sneaker.Name}");
                }
                sneaker.Stock -= quantity;
                //Disminuye el stock de la zapatilla
                _repositorySneaker.Update(sneaker);
            }
            _repositoryReservation.FinalizedReservation(reservation);

        }


    }
}
