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
public class PropietarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PropietarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<OwnerDto>>>Get1()
    {
        var propietarios=await _unitOfWork.Propietarios.GetAllAsync();
        return _mapper.Map<List<OwnerDto>>(propietarios);
    }
       [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<OwnerDto>>> Getpag([FromQuery] Params PropietarioParams)
    {
        var Propietario = await _unitOfWork.Propietarios.GetAllAsync(PropietarioParams.PageIndex,PropietarioParams.PageSize,PropietarioParams.Search);
        var lstPropietariosDto = _mapper.Map<List<OwnerDto>>(Propietario.registros);
        return new Pager<OwnerDto>(lstPropietariosDto,Propietario.totalRegistros,PropietarioParams.PageIndex,PropietarioParams.PageSize,PropietarioParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Propietario>> Post(OwnerDto PropietarioDto)
    {
        var Propietario = _mapper.Map<Propietario>(PropietarioDto);
        this._unitOfWork.Propietarios.Add(Propietario);
        await _unitOfWork.SaveAsync();
        if (Propietario == null)
        {
            return BadRequest();
        }
        PropietarioDto.Id = Propietario.Id;
        return CreatedAtAction(nameof(Post), new { id = PropietarioDto.Id }, PropietarioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Propietario>> Put(int id, [FromBody] OwnerDto PropietarioDto)
    {
        var Propietario = _mapper.Map<Propietario>(PropietarioDto);
        if (Propietario == null)
        {
            return NotFound();
        }
        _unitOfWork.Propietarios.Update(Propietario);
        await _unitOfWork.SaveAsync();
        return Propietario;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Propietario = await _unitOfWork.Propietarios.GetByIdAsync(id);
        if (Propietario == null)
        {
            return NotFound();
        }
        _unitOfWork.Propietarios.Remove(Propietario);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetOwnerxPets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<OwnerxPetsDto>>>Get2()
    {
        var propietarios=await _unitOfWork.Propietarios.GetOwnerxPets();
        return _mapper.Map<List<OwnerxPetsDto>>(propietarios);
    }
    [HttpGet("GetOwnerxPetsBreed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<OwnerxPetsDto>>>Get3(string raza)
    {
        var propietarios=await _unitOfWork.Propietarios.GetOwnerxPetsBreed(raza);
        return _mapper.Map<List<OwnerxPetsDto>>(propietarios);

    }
}
