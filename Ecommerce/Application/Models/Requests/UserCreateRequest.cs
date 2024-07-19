using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.User;

namespace Application.Models.Requests
{
    public class UserCreateRequest
    {

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

    }
}