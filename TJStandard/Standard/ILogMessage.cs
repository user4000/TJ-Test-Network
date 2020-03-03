using System;

namespace TJStandard
{
  public interface ILogMessage 
  {
    void SaveLog(string message);

    void SaveLog(string header, string message);

    void SaveLog(Exception ex, string message);
  }
}