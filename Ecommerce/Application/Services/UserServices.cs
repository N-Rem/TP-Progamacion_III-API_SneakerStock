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
    //son las funciones que se van a usar en los controllers con su inyeccion
    public class UserServices : IUserServices

    {
        private readonly IRepositoryUser _repositoryUser;
        public UserServices(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        //metodo para verificar si ya existe un usuario con esa informacion
        public bool UserExists(string email, string name)
        {
            return _repositoryUser.GetAll().Any(user => user.EmailAddress == email || user.Name == name);
        }

        //CRUD ---- ADMIN, CLIENT, USERS
        public List<UserDto> GetAdmins()
        {

            return UserDto.CreateList(_repositoryUser.GetAll().Where(user => user.Type == User.UserType.Admin).ToList());

        }

        public List<UserDto> GetClients()
        {

            return UserDto.CreateList(_repositoryUser.GetAll().Where(user => user.Type == User.UserType.Client).ToList());

        }

        public List<UserDto> GetUsers()
        {
            return UserDto.CreateList(_repositoryUser.GetAll());
        }

        public UserDto GetById(int id)
        {
            var obj = _repositoryUser.GetById(id)
                 ?? throw new Exception("No encontrado");

            var objDto = UserDto.Create(obj);

            return objDto;
        }

        public void CreateAdmin(UserCreateRequest adminDto)
        {
            if(UserExists(adminDto.Name, adminDto.Name))
            {
                throw new Exception("Ya existe un admin con ese nombre o email");
            }

            var Admin = new User()
            {
                Name = adminDto.Name,
                Password = adminDto.Password,

                EmailAddress = adminDto.EmailAddress,
                Type = User.UserType.Admin
            };


            _repositoryUser.Add(Admin);
        }
        public void CreateClient(UserCreateRequest clientDto)
        {
            if (UserExists(clientDto.Name, clientDto.Name))
            {
                throw new Exception("Ya existe un cliente con ese nombre o email");
            }

            var Admin = new User()
            {
                Name = clientDto.Name,
                EmailAddress = clientDto.EmailAddress,
                Password = clientDto.Password,
                Type = User.UserType.Client
            };


            _repositoryUser.Add(Admin);
        }

        public void Update(UserCreateRequest userDto, int idUser)
        {
            var obj = _repositoryUser.GetById(idUser)
                ?? throw new Exception("User no encotrado");
            obj.Id = idUser; //¿Como acceder al id del usuario actual?
            obj.Name = userDto.Name;
            obj.Password = userDto.Password;
            obj.EmailAddress = userDto.EmailAddress;


            _repositoryUser.Update(obj);
        }

        public void DeleteById(int id)
        {
            var obj = _repositoryUser.GetById(id);
            if (obj == null)
            {
                throw new Exception("no encontrado");
            }
            _repositoryUser.Delete(obj);
        }

        public List<ReservationDto> GetAllReservationUser(int idUser)
        {
            //agregue a cada una de las recervaciones el usuario correspondiente para que se muestre.
            var listReservation = _repositoryUser.GetAllReservationUser(idUser);
            return ReservationDto.CreateList(listReservation);
        }


    }
}