﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sneaker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //se encarga de hacer el id y devolverlo cuando se necestia.
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public SneakerBrand Brand { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "must be a positive value.")] //solo se puede agregar un numero mayor o igual a 0
        public int Price { get; set; }

        [Required]
        public SneakerCategory Category { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "must be a positive value.")]
        public int Stock { get; set; }

        //public ICollection<ReservationSneaker> ReservationSneakers { get; set; } = new List<ReservationSneaker>();
        public enum SneakerCategory
        {
            Sports,
            Casual,
            Running
        }

        public enum SneakerBrand
        {
            Nike,
            Adidas,
            Converse
        }

    }
}