using System;

namespace TJStandard
{
  [Serializable]
  public class ReturnCode
  {
    public int NumericValue { get; set; } = -1;

    public int IdObject { get; set; } = -1;

    public string StringValue { get; set; } = string.Empty;

    public string StringNote { get; set; } = string.Empty;

    public ReturnCode() { /* CONSTRUCTOR */ }

    /* CONSTRUCTOR */
    public ReturnCode(int numericValue, int idObject, string stringValue, string stringNote)
    {
      NumericValue = numericValue; IdObject = idObject; StringValue = stringValue; StringNote = stringNote;
    }

    public bool Success { get => NumericValue == 0; }

    public bool Error { get => NumericValue != 0; }
  }
}

