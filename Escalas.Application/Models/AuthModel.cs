using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escalas.Application.Models
{
    public class AuthModel
    {
        public string Hash { get; set; } = string.Empty;
        public byte[] Salt { get; set; }
    }
}
