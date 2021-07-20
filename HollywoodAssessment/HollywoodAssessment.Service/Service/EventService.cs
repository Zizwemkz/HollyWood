using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HollywoodAssessment.Common.Exceptions;
using HollywoodAssessment.Common.Interfaces;
using HollywoodAssessment.Data.Models;

namespace HollywoodAssessment.Service.Service
{
  public class EventService : IEventsService
  {
    public HollywoodAssessmentDbContext Db;

    public EventService(HollywoodAssessmentDbContext hollywoodAssessmentDbContext)
    {
      Db = hollywoodAssessmentDbContext;
    }

    public void CreateEvent(Event Event)
    {
      Db.Event.Add(Event);
      Db.SaveChanges();
    }

    public void UpdateEvent(int id, Event Event)
    {
      var result = GetEvent(id);
      result.AutoClose = Event.AutoClose;
      result.EventDateTime = Event.EventDateTime;
      result.EventEndDateTime = Event.EventEndDateTime;
      result.EventName = Event.EventName;
      result.EventNumber = Event.EventNumber;
      result.TournamentId = Event.TournamentId;

      Db.SaveChanges();
    }

    public void DeleteEvent(int id)
    {
      var result = GetEvent(id);
      Db.Event.Remove(result);
    }

    public Event GetEvent(int id)
    {
      var result = Db.Event.FirstOrDefault(x => x.EventId == id);
      if(result is null)
        throw  new HttpStatusCodeException((int)HttpStatusCode.BadRequest, "Invalid Event");
      return result;
    }
  }
}
