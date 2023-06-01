using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class SelectedPGUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SelectedPGUser_Id { get; set; }

        [ForeignKey("PGAdmin_Id")]
        public int? PGAdminId { get; set; }
        public string? Select_State { get; set; }
        public string? Select_City { get; set; }
        public string? Select_Area { get; set; }
        public string? Select_PG_Type { get; set; }
        public string? Select_PG { get; set; }
        public string? Location { get; set; }
        public string? Sharing_Type { get; set; }
        public string? Sharing { get; set; }
        public string? Cost { get; set; }

    }
}
