using System;

namespace Model
{
  [Serializable]
  public class RequestChangePassword
  {
    public string Login { get; set; } = string.Empty;

    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
  }
}
