using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HollywoodAssessment.Common.Exceptions;
using HollywoodAssessment.Data.Models;
using HollywoodAssessment.Service.Service;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

namespace HollywoodAssessment.Tests.Services
{
  [TestFixture]
  public class TournamentServiceTest :ServiceBase
  {
    private TournamentService _tournamentService;

    [SetUp]
    public void SetUp()
    {
      _tournamentService = new TournamentService(Db);
    }

    [Test]
    public void CreateTournament_GivenValidTournament_ShouldAddTournament()
    {
      var tournamentid = 5;
      var tournament = new Tournament {TournamentId = tournamentid, TournamentName = "NewTournament"};

      _tournamentService.CreateTournament(tournament);
      var result = Db.Tournament.FirstOrDefault(x => x.TournamentId == tournamentid);

      Assert.IsNotNull(result);
    }
    //TODO More tests for create tournament
    [Test]
    public void GetTournament_GivenValidID_ShouldReturnTournament()
    {
      var tournamentid = 1;

      var result = _tournamentService.GetTournament(tournamentid);

      Assert.IsNotNull(result);
    }

    [Test]
    public void GetTournament_GivenInvalidID_ShouldThrowException()
    {
      var tournament = -1;

      Assert.Throws<HttpStatusCodeException>(() => _tournamentService.GetTournament(tournament));
    }

    [Test]
    public void UpdateTournament_GivenValidTournamentandID_ShouldUpdateTournament()
    {
      var tournamentID = 1;
      var tournamentName = "UpdatedTournament";

      _tournamentService.UpdateTournament(tournamentID, new Tournament(){  TournamentName = tournamentName});
      var result = Db.Tournament.FirstOrDefault(x => x.TournamentId == tournamentID);

      Assert.AreEqual(tournamentName, result.TournamentName);
    }

    [Test]
    public void DeleteTournament_GivenValidID_ShouldRemoveTournament()
    {
      var tournamentID = 1;

      var before = Db.Tournament.ToList().Count;
      _tournamentService.DeleteTournament(tournamentID);
      var after = Db.Tournament.ToList().Count;

     Assert.Less(after, before);
    }



    

  }
}
