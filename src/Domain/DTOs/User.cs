using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

}
