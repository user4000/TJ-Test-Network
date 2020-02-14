namespace ProjectStandard
{
  public class ReturnCodeFormatter
  {
    private static ReturnCode TryToGetReturnCodeFromJson_DEPRECATED_DO_NOT_USE_IT(string json)
    {
      ReturnCode code = null;

      if (MayBeReturnCode(json))
        try
        {
          code = CxConvert.JsonToObject<ReturnCode>(json);
        }
        catch { code = null;  }

      return code;
    }

    public static bool MayBeReturnCode(string json) =>
      json.Contains("\"" + nameof(ReturnCode.NumericValue)    + "\"") &&
      json.Contains("\"" + nameof(ReturnCode.StringValue) + "\"") &&
      json.Contains("\"" + nameof(ReturnCode.IdObject)      + "\"") &&
      json.Contains("\"" + nameof(ReturnCode.StringNote)    + "\"");
    
    public static string ToString(ReturnCode code)
    {
      return $"{code.NumericValue};{code.IdObject};{code.StringValue};{code.StringNote}";
    }
  }
}
