using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectStandard
{
  public class ReturnCodeFactory
  {
    public const int ConstError = -1;

    public static readonly int NcSuccess = 0;

    public static readonly int NcError = ConstError;

    public static readonly int NcIdObjectError = -1;

    public const string Empty = "";

    public static ReturnCode Error(string stringValue = "", string stringNote = "", int numericValue = ConstError)
    {
      return Create(numericValue, NcIdObjectError, stringValue, stringNote);
    }

    public static ReturnCode Create(int numericValue, int idObject, string stringValue, string stringNote)
    {
      return new ReturnCode(numericValue, idObject, stringValue, stringNote);
    }

    public static ReturnCode Success(string stringValue = Empty, string stringNote = Empty, int idObject = 0)
    {
      return new ReturnCode(NcSuccess, idObject, stringValue, stringNote);
    }
    
    public static async Task<ReturnCode> FromHttpResponse(HttpResponseMessage response, bool KeepReturnCodeGivenByStoredProcedure)
    {
      ReturnCode code;
      if (KeepReturnCodeGivenByStoredProcedure)
      {/* В данном варианте метод вернёт именно то значение ReturnCode которое вернула ХП сервера */
        string json = await response.Content.ReadAsStringAsync();   code = CxConvert.JsonToObject<ReturnCode>(json);
      }
      else
      { /* В данном варианте метод подменяет собой содержимое ReturnCode которое вернула ХП сервера, возвращая своё собственное значение ReturnCode на основе HttpResponse Code */
        code = response.IsSuccessStatusCode ? Success() : Error(response.ReasonPhrase, response.StatusCode.ToString());
      }
      return code;
    }

    public static async Task<ReturnCode> FromHttpResponse(HttpResponseMessage response)
    {/* В данном варианте метод вернёт именно то значение ReturnCode которое вернула ХП сервера */   

      if (response.StatusCode == ServerCode.CodeErrorTimeout)
        return Error("Ошибка! Сервер не ответил на ваш запрос.");

      if (response.StatusCode == ServerCode.CodeError)
        return Error("Произошла ошибка при выполнении запроса!");

      ReturnCode code;
      try
      {
        string json = await response.Content.ReadAsStringAsync();
        code = CxConvert.JsonToObject<ReturnCode>(json);
      }
      catch (Exception ex)
      {
        code = Error("Ошибка при попытке получения ответа от сервера! " + ex.Message + " " + ex.StackTrace, ex.Source);
      }

      return code;
    } 
  }
}
