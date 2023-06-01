using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class Room_Sharing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Room_Sharing_Id { get; set; }

        [ForeignKey("Rooms_No")]
        public int? Rooms_No { get; set; }

        [ForeignKey("Email")]
        public String? Email { get; set; }
        public int? room_sharing { get; set; }  
    }
}
