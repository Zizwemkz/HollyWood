using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HollywoodAssessment.API.Controllers
{
  [Route("api/[controller]")]
  [Authorize(Roles = "TournamentManager")]
  public class EventController : Controller
  {
    private readonly IEventsService _eventsService;

    public EventController(IEventsService eventsService)
    {
      _eventsService = eventsService;
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    public ActionResult<Event> Get(int id)
    {
      var result = _eventsService.GetEvent(id);
      return result;
    }

    // POST api/<controller>
    [HttpPost]
    public ActionResult Post([FromBody]Event Event)
    {
       _eventsService.CreateEvent(Event);
       return Ok();
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]Event Event)
    {
      _eventsService.UpdateEvent(id, Event);
      return Ok();
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      _eventsService.DeleteEvent(id);
      return NoContent();
    }
  }
}
