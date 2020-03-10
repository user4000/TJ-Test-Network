using System;

namespace TJSettings
{
  public class DatabaseStructureCheckException : Exception
  {
    public DatabaseStructureCheckException()
    {
    }

    public DatabaseStructureCheckException(string message) : base(message)
    {
    }

    public DatabaseStructureCheckException(string message, Exception inner) : base(message, inner)
    {
    }
  }
}

