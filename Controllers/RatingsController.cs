using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [ApiController]
    [Route("api/ratings")]
    public class RatingsController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RatingsController( ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
        {
            try
            {
                var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var user = await userManager.FindByEmailAsync(email);
                var userId = user.Id;

                var currentRate = await context.Ratings.FirstOrDefaultAsync(x => x.MovieId == ratingDTO.MovieId &&
                x.UserId == userId.ToString());

                if (currentRate == null)
                {
                    var rating = new Rating();
                    rating.MovieId = ratingDTO.MovieId;
                    rating.Rate = ratingDTO.Rating;
                    rating.UserId = userId.ToString();
                    context.Add(rating);
                }
                else { currentRate.Rate = ratingDTO.Rating; }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e.InnerException;
                _ = e.Message.ToString();
                _ = e.StackTrace;
            }

            return NoContent();
        }
    }
}
