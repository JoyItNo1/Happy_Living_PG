using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class Rooms 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rooms_Id { get; set; }
        [ForeignKey("PGAdmin_Id")]
        public int? Floor_No { get; set; }

        public int? Room_no { get; set;}
    }
}
