using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class SpeciesxPetDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<BreedxPetsDto> Razas { get; set; }
    }
