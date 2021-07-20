using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HollywoodAssessmen.API.Controllers
{
  [Route("api/[controller]")]
  [Authorize(Roles = "TournamentManager, Admin")]
  public class TournamentController : Controller
  {
    private readonly ITournamentService _tournamentService;

    public TournamentController(ITournamentService tournamentService)
    {
      _tournamentService = tournamentService;
    }

    // GET: api/<controller>
    [HttpGet("{id}")]
    public ActionResult<Tournament> Get(int id)
    {
      var result = _tournamentService.GetTournament(id);
      return result;
    }

    // POST api/<controller>
    [HttpPost]
    public void Post([FromBody]Tournament tournament)
    {
      _tournamentService.CreateTournament(tournament);
      RedirectToAction("Post", "Event", new Event() { TournamentId = tournament.TournamentId });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]Tournament tournament)
    {
      _tournamentService.UpdateTournament(id, tournament);
      return Ok();
    }

    [Authorize(Roles = "Admin")]
    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int id)
    {
      _tournamentService.DeleteTournament(id);
      return NoContent();
    }
  }
}
