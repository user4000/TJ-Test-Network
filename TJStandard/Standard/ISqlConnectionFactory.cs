using System.Data.SqlClient;

namespace TJStandard
{
  public interface ISqlConnectionFactory
  {
    SqlConnection GetNew();
  }
}