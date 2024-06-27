using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Domain.Entities.User;

namespace Application.Models
{
    //Las DTO nos sirver para pasar datos a los repositorys de manera mas facil y comoda. 
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters.")]
        //^inicia la cadena, ?=.*[A-Za-z] en algun lugar de la cadena exista una mayuscual o minuscula
        //?=.*\d Verifica que al menos haya un digito del 0 al 9
        //[A-Za-z\d]{6,} tiene que tener al menos 4 caracteres, mayuscuals, minusculas y numero.
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$", ErrorMessage = "Password must contain at least one letter and one number.")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "invalid Email Address")]
        public string EmailAddress { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType Type { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; }

        public UserDto()
        {
            Reservations = new List<ReservationDto>();
        }

        public static UserDto Create(User user)
        {
            var dto = new UserDto();

            dto.Name = user.Name;
            dto.Password = "";
            dto.EmailAddress = user.EmailAddress;
            dto.Id = user.Id;
            dto.Type = user.Type;
            user.Reservations = null;
            //if (user.Reservations != null)
            //{
            //    foreach (var reservation in user.Reservations)
            //    {
            //        dto.Reservations.Add(ReservationDto.Create(reservation));
            //    }
            //}

            return dto;
        }

        public static List<UserDto> CreateList(IEnumerable<User> user)
        {
            var listDto = new List<UserDto>();
            foreach (var u in user)
            {
                listDto.Add(Create(u));
            }
            return listDto;
        }


        //Hacer un metodo que cree listas de dtos (https://github.com/UTN-FRRO-TUP-Programacion3/ConsultaAlumnos/blob/main/src/Application/Models/QuestionDto.cs) linea 46
    }
}