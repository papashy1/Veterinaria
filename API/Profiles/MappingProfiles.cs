using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Views;

namespace API.Profiles;
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Laboratorio,LabDto>().ReverseMap();
            CreateMap<Medicamento,MedicamentoDto>().ReverseMap();
            CreateMap<Veterinario,VetDto>().ReverseMap();
            CreateMap<Mascota,PetDto>().ReverseMap();
            CreateMap<Mascota,PetxBreedDto>().ReverseMap();
            CreateMap<Raza,BreedDto>().ReverseMap();
            CreateMap<Especie,SpeciesDto>().ReverseMap();
            CreateMap<Propietario,OwnerDto>().ReverseMap();
            CreateMap<Propietario,OwnerxPetsDto>().ReverseMap();
            CreateMap<Raza,BreedxPetsDto>().ReverseMap();
            CreateMap<Especie,SpeciesxPetDto>().ReverseMap();
            CreateMap<Cita,AppointmentxPetDto>().ReverseMap();
            CreateMap<Proveedor,ProviderDto>().ReverseMap();
            CreateMap<Usuario,UsuarioDto>().ReverseMap();
            CreateMap<MovimientoTotal,TotalMovimientoDto>().ReverseMap();
            CreateMap<Cita,AppointmentDto>().ReverseMap();
            CreateMap<MovimientoMedicamento,MovimientoDto>().ReverseMap();
        }
    }
