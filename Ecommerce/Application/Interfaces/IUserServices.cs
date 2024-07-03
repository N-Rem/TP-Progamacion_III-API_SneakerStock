using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserServices
    {
        List<UserDto> GetAdmins();
        List<UserDto> GetClients();
        List<UserDto> GetUsers();
        UserDto GetById(int id);
        List<ReservationDto> GetAllReservationUser(int idUser);

        void CreateAdmin(UserCreateRequest adminDto);
        void CreateClient(UserCreateRequest clientDto);



        void Update(UserCreateRequest userDto, int idUser);

        void DeleteById(int id);



    }
}