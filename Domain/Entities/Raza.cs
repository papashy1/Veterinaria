using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Raza:BaseEntity
    {
        public string Nombre { get; set; }
        public int IdEspeciefk { get; set; }
        public Especie Especie { get; set; }
        public ICollection<Mascota> Mascotas { get; set; }

    }
