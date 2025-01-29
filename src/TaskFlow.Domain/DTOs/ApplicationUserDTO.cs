using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
