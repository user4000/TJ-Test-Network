using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectStandard
{
  public static class XxIDataReader // https://stackoverflow.com/questions/1464883/how-can-i-easily-convert-datareader-to-listt //
  {
    public static IEnumerable<T> Select<T>(this IDataReader reader,  Func<IDataReader, T> projection)
    {
      while (reader.Read())
      {
        yield return projection(reader);
      }
    }
  }
}
