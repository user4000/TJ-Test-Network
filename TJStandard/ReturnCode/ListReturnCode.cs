using System.Linq;
using System.Collections.Generic;

namespace TJStandard
{
  public class ListReturnCode
  {
    public static ReturnCode First(IList<ReturnCode> list)
    {
      ReturnCode code = default(ReturnCode);
      try
      {
        code = list.First();
      }
      catch
      {
        code = ReturnCodeFactory.Error("Procedure did not return any data.", "Процедура не вернула результатов.");
      }
      return code;
    }
  }
}
