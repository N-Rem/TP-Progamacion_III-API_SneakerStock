using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entities.Sneaker;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Models
{
    public class SneakerDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SneakerBrand Brand { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "must be a positive value.")] //solo se puede agregar un numero mayor o igual a 0
        public int Price { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SneakerCategory Category { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "must be a positive value.")]
        public int Stock { get; set; }


        public static SneakerDto Create(Sneaker sneaker)
        {
            var dto = new SneakerDto();
            dto.Id = sneaker.Id;
            dto.Name = sneaker.Name;
            dto.Brand = sneaker.Brand;
            dto.Price = sneaker.Price;
            dto.Category = sneaker.Category;
            dto.Stock = sneaker.Stock;

            return dto;
        }

        public static List<SneakerDto> CreateList(IEnumerable<Sneaker> sneakers)
        {
            List<SneakerDto> listDto = [];
            foreach (var s in sneakers)
            {
                listDto.Add(Create(s));
            }
            return listDto;
        }


    }
}