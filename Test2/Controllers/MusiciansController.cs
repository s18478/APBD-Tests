﻿using Microsoft.AspNetCore.Mvc;
using Test2.Exceptions;
using Test2.Models.DTOs.Requests;
using Test2.Services;

namespace Test2.Controllers
{
    [Route("api/musicians")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        private readonly IMusicDbService _service;

        private MusiciansController(IMusicDbService service)
        {
            _service = service;
        }

        [HttpGet("{idMusician}")]
        public IActionResult GetMusician(int idMusician)
        {
            try
            {
                var response = _service.GetMusician(idMusician);
                return Ok(response);
            } catch (MusicianDoesNotExistsException exc)
            {
                return NotFound(exc.Message);
            }
        }

        [HttpPost] 
        public IActionResult AddMusician(AddMusicianRequest request)
        {
            try
            {
                _service.AddMusician(request);
                return Ok();
            }
            catch (MusicianDoesNotAddedException exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }
}
