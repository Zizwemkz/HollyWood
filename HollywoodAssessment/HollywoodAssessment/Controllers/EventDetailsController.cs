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
  public class EventDetailsController : Controller
  {
    private readonly IEventDetailService _eventDetail;

    public EventDetailsController(IEventDetailService eventDetail)
    {
      _eventDetail = eventDetail;
    }


    // GET: api/<controller>
    [HttpGet("{id}")]
    public ActionResult<EventDetail> Get(int id)
    {
      return _eventDetail.GetEventDetail(id);
    }

    // POST api/<controller>
    [HttpPost]
    public ActionResult Post([FromBody]EventDetail eventDetail)
    {
      _eventDetail.CreateEventDetails(eventDetail);
      return Ok();
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody]EventDetail eventDetail)
    {
      _eventDetail.UpdateEventDetails(id,eventDetail);
      return Ok();
    }


    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      _eventDetail.RemoveEventDetails(id);
      return NoContent();
    }
  }
}
