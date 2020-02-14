using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  [Serializable]
  public class User
  {
    public int IdUser { get; set; }
    public string UserLogin { get; set; }
    public int IdStatus { get; set; }
    public int IdUnit { get; set; }
    public int IdPosition { get; set; }
    public string SurName { get; set; }
    public string FirstName { get; set; }
    public string UserPatronymic { get; set; }
    public string UserNote { get; set; }
    public string PasswordHash { get; set; }
    public string SaltHash { get; set; }
  }
}
