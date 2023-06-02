using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class PaymentImage
    {
        public int Id { get; set; }
        [ForeignKey("block_no")]
        public string UserId { get; set; }
        public string? PaymentsImage { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Created_date { get; set; }
    }
}
