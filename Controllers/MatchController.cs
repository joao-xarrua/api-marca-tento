using MarcaTento.Data;
using MarcaTento.Extensions;
using MarcaTento.Models;
using MarcaTento.ViewModels;
using MarcaTento.ViewModels.Matches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Match = MarcaTento.Models.Match;

namespace MarcaTento.Controllers
{
    [ApiController]
    public class MatchController : ControllerBase
    {
        [HttpGet("v1/matches")]
        public async Task<IActionResult> GetAsync(
            [FromServices] MarcaTentoDataContext context) 
        {
            try
            {
                var matches = await context
                    .Matches
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Select(x => new ListMatchViewModel
                    {
                        Id = x.Id,
                        NameTeamOne = x.NameTeamOne,
                        NameTeamTwo = x.NameTeamTwo,
                        ImageTeamOne = x.ImageTeamOne,
                        ImageTeamTwo = x.ImageTeamTwo,
                        MatchDate = x.MatchDate,
                        ScoreTotal = x.ScoreTotal,
                        ScoreTeamOne = x.ScoreTeamOne,
                        ScoreTeamTwo = x.ScoreTeamTwo,
                        User = $"{x.User.Username} ({x.User.Email})"
                    })
                    .ToListAsync();
                return Ok(new ResultViewModel<List<ListMatchViewModel>>(matches));

            } catch (Exception ex)
            {
                return StatusCode(500, "M1X01 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/matches/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] MarcaTentoDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var match = await context
                    .Matches
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Select(x => new ListMatchViewModel
                    {
                        Id = x.Id,
                        NameTeamOne = x.NameTeamOne,
                        NameTeamTwo = x.NameTeamTwo,
                        ImageTeamOne = x.ImageTeamOne,
                        ImageTeamTwo = x.ImageTeamTwo,
                        MatchDate = x.MatchDate,
                        ScoreTotal = x.ScoreTotal,
                        ScoreTeamOne = x.ScoreTeamOne,
                        ScoreTeamTwo = x.ScoreTeamTwo,
                        User = $"{x.User.Username} ({x.User.Email})"
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (match == null) 
                    return NotFound(new ResultViewModel<Match>("M1X02 - Partida não encontrada"));

                return Ok(new ResultViewModel<ListMatchViewModel>(match));
            } catch (Exception ex)
            {
                return StatusCode(500, "M1X02 - Falha interna do servidor");
            }
        }

        /*
         * A ideia desse post é gerar automanticamente o Slug
         */
        [HttpPost("v1/matches")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorMatchViewModel model,
            [FromServices] MarcaTentoDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Match>(ModelState.GetErrors()));

            try
            {
                var match = new Match
                {
                    NameTeamOne = model.NameTeamOne,
                    NameTeamTwo = model.NameTeamTwo,
                    ImageTeamOne = model.ImageTeamOne,
                    ImageTeamTwo = model.ImageTeamTwo,
                    ScoreTotal = model.ScoreTotal,
                    ScoreTeamOne = model.ScoreTeamOne,
                    ScoreTeamTwo = model.ScoreTeamTwo,
                    UserId = model.UserId,
                    MatchDate = DateTime.Now.ToString(),
                    Slug = $"{model.UserId}-{DateTime.Now}".Replace("/", "").Replace(" ", "-").Replace(":","")
                };

                await context.Matches.AddAsync(match);
                await context.SaveChangesAsync();

                return Created($"v1/matches/{match.Id}", new ResultViewModel<Match>(match));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Match>("05X09 - Falha ao inserir partida"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Match>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/matches/{id:int}")]
        public async Task<IActionResult> DeleteAsync( // METODO DELETE
            [FromRoute] int id,
            [FromServices] MarcaTentoDataContext context)
        {
            try
            {
                var match = await context
                    .Matches
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (match == null) 
                    return NotFound(new ResultViewModel<Match>("Partida não encontrada"));

                context.Matches.Remove(match);
                await context.SaveChangesAsync();

                return Ok(match);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Match>("Falha interna no servidor"));
            }
        }

    }
}
