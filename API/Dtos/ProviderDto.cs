using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class ProviderDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
         public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
