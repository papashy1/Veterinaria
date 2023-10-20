using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class ProveedorController:BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProviderDto>>>Get1()
    {
        var proveedores=await _unitOfWork.Proveedores.GetAllAsync();
        return _mapper.Map<List<ProviderDto>>(proveedores);
    }
       [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProviderDto>>> Getpag([FromQuery] Params ProveedorParams)
    {
        var Proveedor = await _unitOfWork.Proveedores.GetAllAsync(ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
        var lstProveedorsDto = _mapper.Map<List<ProviderDto>>(Proveedor.registros);
        return new Pager<ProviderDto>(lstProveedorsDto,Proveedor.totalRegistros,ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Proveedor>> Post(ProviderDto ProveedorDto)
    {
        var Proveedor = _mapper.Map<Proveedor>(ProveedorDto);
        this._unitOfWork.Proveedores.Add(Proveedor);
        await _unitOfWork.SaveAsync();
        if (Proveedor == null)
        {
            return BadRequest();
        }
        ProveedorDto.Id = Proveedor.Id;
        return CreatedAtAction(nameof(Post), new { id = ProveedorDto.Id }, ProveedorDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Proveedor>> Put(int id, [FromBody] ProviderDto ProveedorDto)
    {
        var Proveedor = _mapper.Map<Proveedor>(ProveedorDto);
        if (Proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Update(Proveedor);
        await _unitOfWork.SaveAsync();
        return Proveedor;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if (Proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Remove(Proveedor);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetProvidersxMed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<ProviderDto>>>Get2(string medicamento)
    {
        var proveedores=await _unitOfWork.Proveedores.GetProvidersxMed(medicamento);
        return _mapper.Map<List<ProviderDto>>(proveedores);

    }

}
