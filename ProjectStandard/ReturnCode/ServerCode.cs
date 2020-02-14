using System.Net;
using System.Net.Http;

namespace ProjectStandard
{
  public class ServerCode
  {
    //public static string ContentTextPlain { get; } = "text/plain";

    //public static string ContentJson { get; } = "application/json";

    public static int EcApikeyExpired { get; } = 111111;

    public static int EcApikeyWrong { get; } = 111112;

    public static int EcYourIpIsBanned { get; } = 111113;

    public static int EcUserIsNotFound { get; } = 100005;

    public static int EcUserIsLocked { get; } = 100004;

    public static int EcIncorrectLoginOrPassword { get; } = 100003;

    public static int EcStoredProcedureExecution { get; } = 100007;

    public static int EcDataNotFound { get; } = 100010;


    public static HttpStatusCode CodeApikeyIsExpired { get; } = HttpStatusCode.Conflict;

    public static HttpStatusCode CodeApikeyIsWrong { get; } = HttpStatusCode.Unauthorized;

    public static HttpStatusCode CodeClientSentNoData { get; } = HttpStatusCode.BadRequest;

    public static HttpStatusCode CodeErrorTimeout { get; } = HttpStatusCode.ServiceUnavailable;

    public static HttpStatusCode CodeError { get; } = HttpStatusCode.Forbidden;

    public static bool IsApikeyExpired(ReturnCode code) => code.NumericValue == EcApikeyExpired;

    public static bool IsApikeyExpired(HttpResponseMessage response) => response.StatusCode == CodeApikeyIsExpired;

  }
}
