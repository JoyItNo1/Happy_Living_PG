using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class PGTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PGTypes_Id { get; set; }

        [ForeignKey("PGAdmin_Id")]
        public int? PGAdminId { get; set; }

        public string ? PGtype { get; set; }
    }
}
