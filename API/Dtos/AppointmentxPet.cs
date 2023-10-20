using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class AppointmentxPetDto
    {
        public int Id { get; set; }
        public PetDto Mascota { get; set; }
    }
