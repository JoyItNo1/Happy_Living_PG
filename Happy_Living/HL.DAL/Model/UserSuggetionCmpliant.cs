using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class UserSuggetionCmpliant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SC_Id { get; set; }
        public int? User_Id { get; set; }
        public string? User_name { get; set; }
        public string? SuggetionOrCmplet { get; set; }
        public int? Block_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? Created_date { get; set; }
    }
}
