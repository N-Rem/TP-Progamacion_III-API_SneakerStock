using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public ReservationState State { get; set; }

        //Tabla intermedia.
        //[Required]
        public ICollection<ReservationSneaker> ReservationSneakers { get; set; } = new List<ReservationSneaker>();

        [Required]
        [ForeignKey("IdUser")]
        public int IdUser { get; set; }
        [Required]
        public User User { get; set; }


        public enum ReservationState
        {
            Active,
            Finalized,
        }

    }
}