using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class PGsharingType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sharetype_Id { get; set; }

        [ForeignKey("PGAdmin_Id")]
        public int? PGAdminId { get; set; }
        public string? SharingType { get; set; }
        public string? Price { get; set; }

    }
}
