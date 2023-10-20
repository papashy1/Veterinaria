using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface IPropietarioRepository:IGenericRepository<Propietario>
    {
        Task<IEnumerable<Propietario>> GetOwnerxPets();
        Task<IEnumerable<Propietario>> GetOwnerxPetsBreed(string raza);
    }
