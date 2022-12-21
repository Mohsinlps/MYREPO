using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }
        public string recieverNumber { get; set; }
        public int otp { get; set; }
        public string StartTime { get; set; }
    }
}
