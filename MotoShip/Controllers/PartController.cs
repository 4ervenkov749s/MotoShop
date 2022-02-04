using AutoMapper;
using MotoShop.BL.Interfaces;
using MotoShop.Models.DTO;
using MotoShop.Models.Requests;
using MotoShop.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MotoShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        private readonly IMapper _mapper;

        public PartController(IPartService partService, IMapper mapper)
        {
            _partService = partService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _partService.GetAll();

            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var result = _partService.GetById(id);
            if (result == null) return NotFound(id);

            var response = _mapper.Map<PartResponse>(result);

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreatePart([FromBody] PartRequest partRequest)
        {
            if (partRequest == null) return BadRequest();

            var part = _mapper.Map<Part>(partRequest);
            var result = _partService.Create(part);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var result = _partService.Delete(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] PartUpdateRequest partRequest)
        {
            if (partRequest == null) return BadRequest();

            var searchPart = _partService.GetById(partRequest.Id);
            if (searchPart == null) return NotFound(partRequest.Id);

            searchPart.Name = partRequest.Name;
            searchPart.PartNumber = partRequest.PartNumber;
            searchPart.Price = partRequest.Price;

            var result = _partService.Update(searchPart);

            return Ok(result);
        }



    }
}
