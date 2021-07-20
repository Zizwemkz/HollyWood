using System;
using System.Collections.Generic;
using System.Text;
using HollywoodAssessment.Data.Models;

namespace HollywoodAssessment.Common.Interfaces
{
  public interface IEventsService
  {
    void CreateEvent(Event Event);
    void UpdateEvent(int id,Event Event);
    void DeleteEvent(int id);
    Event GetEvent(int id);
  }
}
