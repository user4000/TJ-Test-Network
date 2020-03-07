using System.Text;

namespace TJSettings
{
 public static class ItemNameValidation
  {
    public static bool IsValidCharForItemName(char c) // Только цифры, латинские буквы и несколько символов //
    {
      return
        (
        (c >= '0' && c <= '9') ||
        (c >= 'A' && c <= 'Z') ||
        (c >= 'a' && c <= 'z') ||
        c == '-' ||
        c == '+' ||
        c == '=' ||
        c == '(' ||
        c == ')' ||
        c == '^' ||
        c == '_');
    }

    public static string RemoveSpecialCharacters(this string str)
    {
      StringBuilder sb = new StringBuilder();
      string temp = str.Trim().Replace(' ', '_');
      foreach (char c in temp) if (IsValidCharForItemName(c)) sb.Append(c);
      return sb.ToString();
    }
  }
}

