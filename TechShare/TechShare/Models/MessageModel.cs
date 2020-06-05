using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{    
    public class MessageModel
    {
        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public string Action { get; set; }        
    }
}
