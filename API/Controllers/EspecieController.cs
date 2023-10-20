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
    public class EspecieController:BaseApiController
{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

    public EspecieController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<SpeciesDto>>>Get1()
    {
        var especies=await _unitOfWork.Especies.GetAllAsync();
        return _mapper.Map<List<SpeciesDto>>(especies);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SpeciesDto>>> Getpag([FromQuery] Params EspecieParams)
    {
        var Especie = await _unitOfWork.Especies.GetAllAsync(EspecieParams.PageIndex,EspecieParams.PageSize,EspecieParams.Search);
        var lstEspeciesDto = _mapper.Map<List<SpeciesDto>>(Especie.registros);
        return new Pager<SpeciesDto>(lstEspeciesDto,Especie.totalRegistros,EspecieParams.PageIndex,EspecieParams.PageSize,EspecieParams.Search);
    }
        [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Especie>> Post(SpeciesDto EspecieDto)
    {
        var especie = _mapper.Map<Especie>(EspecieDto);
        this._unitOfWork.Especies.Add(especie);
        await _unitOfWork.SaveAsync();
        if (especie == null)
        {
            return BadRequest();
        }
        EspecieDto.Id = especie.Id;
        return CreatedAtAction(nameof(Post), new { id = EspecieDto.Id }, EspecieDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Especie>> Put(int id, [FromBody] SpeciesDto EspecieDto)
    {
        var especie = _mapper.Map<Especie>(EspecieDto);
        if (especie == null)
        {
            return NotFound();
        }
        _unitOfWork.Especies.Update(especie);
        await _unitOfWork.SaveAsync();
        return especie;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var especie = await _unitOfWork.Especies.GetByIdAsync(id);
        if (especie == null)
        {
            return NotFound();
        }
        _unitOfWork.Especies.Remove(especie);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("GetSpeciesxPets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<SpeciesxPetDto>>>Get3()
    {
        var especies=await _unitOfWork.Especies.GetSpeciesxPets();
        return _mapper.Map<List<SpeciesxPetDto>>(especies);

    }

}
