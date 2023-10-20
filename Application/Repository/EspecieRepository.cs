using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class EspecieRepository : GenericRepository<Especie>, IEspecieRepository
{
    private readonly VeterinariaCampusContext _context;
    public EspecieRepository(VeterinariaCampusContext context) : base(context)
    {
        _context=context;
    }



        public async Task<IEnumerable<Especie>> GetSpeciesxPets()
    {
        return await _context.Especies.Include(p=>p.Razas.Where(p=>p.Mascotas.Count()>0)).ThenInclude(p=>p.Mascotas).Where(p=>p.Razas.Where(p=>p.Mascotas.Count()>0).Count()>0).ToListAsync();
    }
    
    public override async Task<IEnumerable<Especie>> GetAllAsync()
    {
        return await _context.Especies.ToListAsync();
    }
        public override async Task<(int totalRegistros, IEnumerable<Especie> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Especies as IQueryable<Especie>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        return (totalRegistros, registros);
    }
}
