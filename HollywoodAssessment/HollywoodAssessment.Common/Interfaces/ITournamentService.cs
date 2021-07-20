using System;
using System.Collections.Generic;
using System.Text;
using HollywoodAssessment.Data.Models;

namespace HollywoodAssessment.Common.Interfaces
{
  public interface ITournamentService
  {
    void CreateTournament(Tournament tournament);
    void UpdateTournament(int id,Tournament tournament);
    void DeleteTournament(int id);
    Tournament GetTournament(int id);
  }
}
