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
  public class TournamentService : ITournamentService
  {
    public HollywoodAssessmentDbContext Db;

    public TournamentService(HollywoodAssessmentDbContext hollywoodAssessmentDb)
    {
      Db = hollywoodAssessmentDb;
    }
    public void CreateTournament(Tournament tournament)
    {
      Db.Tournament.Add(tournament);
      Db.SaveChanges();
    }

    public void UpdateTournament(int id, Tournament tournament)
    {
      var result = GetTournament(id);
      result.TournamentName = tournament.TournamentName;
      Db.SaveChanges();
    }

    public void DeleteTournament(int id)
    {
      var result = GetTournament(id);
      Db.Remove(result);
      Db.SaveChanges();
    }

    public Tournament GetTournament(int id)
    {
      var result = Db.Tournament.FirstOrDefault(x => x.TournamentId == id);
      if(result is null)
        throw new HttpStatusCodeException((int)HttpStatusCode.BadRequest, "Invalid tournament");

      return result;
    }
  }
}
