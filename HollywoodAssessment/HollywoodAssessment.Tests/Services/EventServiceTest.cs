using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HollywoodAssessment.Common.Exceptions;
using HollywoodAssessment.Data.Models;
using HollywoodAssessment.Service.Service;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HollywoodAssessment.Tests.Services
{
  [TestFixture]
  public class EventServiceTest : ServiceBase
  {
    private EventService _eventService;

    [SetUp]
    public void SetUp()
    {
      _eventService = new EventService(Db);
    }

    [Test]
    public void CreateEvent_GivenValidEvent_ShouldCreateEvent()
    {
      var eventId = 5;
      var Event = new Event()
      {
        AutoClose = true,
        EventDateTime = DateTime.Today,
        EventId = eventId,
        EventName = "Event",
        EventNumber = 1,
        TournamentId = 1
      };

      _eventService.CreateEvent(Event);
      var result = Db.Event.FirstOrDefault(x => x.EventId == eventId);

      Assert.IsNotNull(result);
    }

    [Test]
    public void GetEvent_GivenValidEventId_ShouldReturnEvent()
    {
      var eventid = 1;
      var result = _eventService.GetEvent(eventid);

      Assert.IsNotNull(result);
    }

    [Test]
    public void GetEvent_GivenInvalidEventID_ShouldThrowException()
    {
      var eventid = -1;

      Assert.Throws<HttpStatusCodeException>(() => _eventService.GetEvent(eventid));
    }

    [Test]
    public void UpdateEvent_GivenValidEvent_ShouldUpdateEvent()
    {
      var eventid = 1;
      var EventName = "UpdatedName";
      short EventNumber = 5;

      var Event = new Event()
      {
        AutoClose = true,
        EventDateTime = DateTime.Today,
        EventId = eventid,
        EventName = EventName,
        EventNumber = EventNumber,
        TournamentId = 1
      };

      _eventService.UpdateEvent(eventid, Event);
      var result = _eventService.GetEvent(eventid);
     
      Assert.AreEqual(EventName,result.EventName);
      Assert.AreEqual(EventNumber, result.EventNumber);
    }

    public void DeleteEvent_GivenValidEventId_ShouldRemoveEvent()
    {
      var eventid = 1;

      var before = Db.Event.ToList().Count;
      _eventService.DeleteEvent(eventid);
      var after = Db.Event.ToList().Count;

      Assert.Less(after, before);
    }
  }
}
