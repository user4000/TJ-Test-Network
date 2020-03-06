using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TJStandard
{
  public class ReturnCodeFactory
  {
    public const int ConstError = -1;

    public static readonly int NcSuccess = 0;

    public static readonly int NcError = ConstError;

    public static readonly int NcIdObjectError = -1;

    public const string Empty = "";

    public static ReturnCode Error(int numericValue, string stringValue = Empty, string stringNote = Empty)
    {
      return Create(numericValue, NcIdObjectError, stringValue, stringNote);
    }

    public static ReturnCode Error(string stringValue = Empty, string stringNote = Empty, int numericValue = ConstError)
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
      {/* In this version, the method will return exactly the [ReturnCode] value that the server Stored Procedure returned */
        string json = await response.Content.ReadAsStringAsync();   code = CxConvert.JsonToObject<ReturnCode>(json);
      }
      else
      { /* In this variant, the method replaces the contents of the ReturnCode that the server Stored Procedure returned, returning its own ReturnCode value based on the HttpResponse Code */
        code = response.IsSuccessStatusCode ? Success() : Error(response.ReasonPhrase, response.StatusCode.ToString());
      }
      return code;
    }

    public static async Task<ReturnCode> FromHttpResponse(HttpResponseMessage response)
    {/* In this version, the method will return exactly the [ReturnCode] value that the server Stored Procedure returned */

      //if (response.StatusCode == ServerCode.CodeErrorTimeout)  return Error("The server did not respond to your request.");
      //if (response.StatusCode == ServerCode.CodeError) return Error("An error occurred while executing the request!");

      ReturnCode code;
      try
      {
        string json = await response.Content.ReadAsStringAsync();
        code = CxConvert.JsonToObject<ReturnCode>(json);
      }
      catch (Exception ex)
      {
        code = Error("Error trying to get response from server! " + ex.Message + " " + ex.StackTrace, ex.Source);
      }

      return code;
    } 
  }
}
