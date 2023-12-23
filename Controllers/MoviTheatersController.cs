using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MoviTheatersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviTheatersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get()
        {
            var entities = await context.MoviTheaters.OrderBy(x=> x.Name).ToListAsync();
            return mapper.Map<List<MovieTheaterDTO>>(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(int id)
        {
            var moviTheater = await context.MoviTheaters.FirstOrDefaultAsync(x => x.Id == id);
            if (moviTheater == null)
            {
                return NotFound();
            }
            return mapper.Map<MovieTheaterDTO>(moviTheater);
        }
        
        [HttpPost]
        public async Task<ActionResult> post(MovieTheaterCreationDTO moviTheaterCreationDTO)
        {
            var movieTheater = mapper.Map<MoviTheater>(moviTheaterCreationDTO);
            context.Add(movieTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, MovieTheaterCreationDTO moviCreationDTO)
        {
            var moviTheater = await context.MoviTheaters.FirstOrDefaultAsync(x => x.Id == id);
            if (moviTheater == null)
            {
                return NotFound();
            }
            moviTheater = mapper.Map(moviCreationDTO, moviTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var moviTheater = await context.MoviTheaters.FirstOrDefaultAsync(x => x.Id == id);
            if (moviTheater == null)
            {
                return NotFound();
            }

            context.Remove(moviTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
