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
  public class EventDetailsService : IEventDetailService
  {
    public HollywoodAssessmentDbContext db;

    public EventDetailsService(HollywoodAssessmentDbContext hollywoodAssessmentDb)
    {
      db = hollywoodAssessmentDb;
    }
    public void CreateEventDetails(EventDetail eventDetail)
    {
      if (eventDetail.EventDetailId.ToString() is null)
      {
        throw new HttpStatusCodeException((int)HttpStatusCode.BadRequest, "You need to create event first");
      }

      db.EventDetail.Add(eventDetail);
      db.SaveChanges();
    }

    public void UpdateEventDetails(int id, EventDetail eventDetail)
    {

      var result = GetEventDetail(id);
      result.EventDetailName = eventDetail.EventDetailName;
      result.EventDetailId = eventDetail.EventDetailId;
      result.EventDetailName = eventDetail.EventDetailName;
      result.EventDetailNumber = eventDetail.EventDetailNumber;
      result.EventDetailOdd = eventDetail.EventDetailOdd;
      result.EventDetailStatus = eventDetail.EventDetailStatus;
      result.EventDetailStatusId = eventDetail.EventDetailStatusId;
      result.EventId = eventDetail.EventId;
      result.FinishingPosition = eventDetail.FinishingPosition;
      result.FirstTimer = eventDetail.FirstTimer;

      db.SaveChanges();

    }

    public void RemoveEventDetails(int id)
    {
      var result = GetEventDetail(id);
      db.EventDetail.Remove(result);
    }

    public EventDetail GetEventDetail(int id)
    {
      var result = db.EventDetail.FirstOrDefault(x => x.EventDetailId == id);
      if (result is null)
        throw  new HttpStatusCodeException((int)HttpStatusCode.BadRequest, "Invalid Event Details");

      return result;
    }
  }
}
