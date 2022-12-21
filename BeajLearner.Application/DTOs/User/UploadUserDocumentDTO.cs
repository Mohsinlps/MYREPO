using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.DTOs.User
{
    public class UploadUserDocumentDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]

        public string FileName { get; set; }
        [Required]

        public string File { get; set; }
    }
}
