using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public enum Action
    {
        Edit,
        Delete,
        Publish
    }
    public class MessageModel
    {
        public Guid PostId { get; set; }
        public string UserId { get; set; } 
        public Action Action { get; set; }        
    }
}
