using System.Threading.Tasks;

namespace ProjectStandard
{
  public interface ILoginForm
  {
    bool IsConnected { get; }

    string LastLogin();

    Task<bool> Disconnect();

    Task<bool> Connect(string Login, string Password, System.Windows.Forms.Control control = null);

    Task<ReturnCode> ChangePassword(string Login, string OldPassword, string NewPassword);
  }
}