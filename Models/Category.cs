using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Web_API.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}