using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HollywoodAssessment.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HollywoodAssessment.Tests.Services
{
  [TestFixture]
  public abstract class ServiceBase
  {
    protected HollywoodAssessmentDbContext Db;

    [SetUp]
    public async Task Setup()
    {
      var options = new DbContextOptionsBuilder<HollywoodAssessmentDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
      Db = new HollywoodAssessmentDbContext(options);
      await AddData();
    }

    private async Task AddData()
    {
      var tournament = new List<Tournament>
      {
        new Tournament() {TournamentName = "Tournament1", TournamentId = 1},
        new Tournament() {TournamentName = "Tournament2", TournamentId = 2}
      };

      await Db.Tournament.AddRangeAsync(tournament);
      await Db.SaveChangesAsync();

      var Event = new List<Event>
      {
        new Event() {EventId = 1, AutoClose = false, EventDateTime = DateTime.Today, EventName = "TournamentEvent", EventNumber = 1,TournamentId = 1 },
        new Event() {EventId = 2, AutoClose = true, EventDateTime = Convert.ToDateTime("1/1/2019") , EventName = "TournamentEvent2", EventNumber = 2,TournamentId = 1, EventEndDateTime = Convert.ToDateTime("2/04/2019")},
        new Event() {EventId = 3, AutoClose = true, EventDateTime = Convert.ToDateTime("03/23/2019"), EventName = "TournamentEvent3", EventNumber = 4,TournamentId = 2 }
      };

      await Db.Event.AddRangeAsync(Event);
      await Db.SaveChangesAsync();


      var eventDetailsStatus = new List<EventDetailStatus>()
      {
        new EventDetailStatus() {EventDetailStatusId = 1, EventDetailStatusName = "Active"},
        new EventDetailStatus() {EventDetailStatusId = 2, EventDetailStatusName = "Scratched"},
        new EventDetailStatus() {EventDetailStatusId = 3, EventDetailStatusName = "Closed"},
      };

      await Db.EventDetailStatus.AddRangeAsync(eventDetailsStatus);
      await Db.SaveChangesAsync();

      var eventDetails = new List<EventDetail>
      {
         new EventDetail() {EventDetailId = 1, EventDetailName = "EventDetailsForT1", EventDetailNumber = 2, EventDetailOdd = 50, EventDetailStatusId = 1,FirstTimer = true},
         new EventDetail() {EventDetailId = 2, EventDetailName = "EventDetailsForT2", EventDetailNumber = 3, EventDetailOdd = 20, EventDetailStatusId = 2,FirstTimer = true},
      };

      await Db.EventDetail.AddRangeAsync(eventDetails);
      await Db.SaveChangesAsync();
    }
  }
}
