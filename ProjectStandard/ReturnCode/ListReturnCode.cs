using System.Collections.Generic;
using System.Linq;

namespace ProjectStandard
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
        code = ReturnCodeFactory.Error("Error! Procedure did not return any data.", "Ошибка! Процедура не вернула результатов.");
      }
      return code;
    }
  }
}
