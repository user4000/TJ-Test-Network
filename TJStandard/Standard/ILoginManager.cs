using System.Threading.Tasks;

namespace TJStandard
{
  public interface ILoginManager : IOutputMessage
  {
    string LastLogin();

    Task<ReturnCode> ChangePassword(string Login, string OldPassword, string NewPassword);

    Task<ReturnCode> Connect(string Login, string Password);

    Task<ReturnCode> Disconnect();

    Task EventConnectButtonClick(ReturnCode code);

    void SaveLastLogin(string login, bool SuccessConnection);

    Task<ReturnCode> DeleteApiKey();

    void ClearApiKey();

  }
}