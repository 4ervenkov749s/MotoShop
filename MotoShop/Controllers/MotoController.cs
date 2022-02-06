using AutoMapper;
using MotoShop.BL.Interfaces;
using MotoShop.DL.Interfaces;
using MotoShop.Models.DTO;
using MotoShop.Models.Requests;
using MotoShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MotoShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;
        private readonly IMapper _mapper;

        public MotoController(IMotoService motoService, IMapper mapper)
        {
            _motoService = motoService;
            _mapper = mapper;
        }



        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _motoService.GetAll();

            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _motoService.GetById(id);
            if (result == null) return NotFound(id);

            var response = _mapper.Map<MotoResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateMoto([FromBody] MotoRequest motoRequest)
        {
            if (motoRequest == null) return BadRequest();

            var moto = _mapper.Map<Moto>(motoRequest);
            var result = _motoService.Create(moto);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var result = _motoService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] MotoUpdateRequest motoRequest)
        {
            if (motoRequest == null) return BadRequest();

            var searchMoto = _motoService.GetById(motoRequest.Id);
            if (searchMoto == null) return NotFound(motoRequest.Id);

            searchMoto.Brand = motoRequest.Brand;
            searchMoto.Model = motoRequest.Model;

            var result = _motoService.Update(searchMoto);

            return Ok(result);
        }



    }
}
