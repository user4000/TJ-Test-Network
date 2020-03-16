using System.Windows.Forms;
using TJSettings;
using TJStandard;
using static TestNetwork.Program;

namespace TestNetwork

{
  public class ProjectManagerFactory
  {
    public static ProjectManager Create()
    {
      ProjectManager manager = new ProjectManager();
      manager.DbSettings = LocalDatabaseOfSettings.Create();

      /*   
      No need to instantly connect to the database.

      The user can perform the connection operation later.

      ReturnCode code = manager.DbSettings.ConnectToDatabase(ApplicationSettings.SettingsDatabaseLocation);
      
      if (code.Error)
      {
        MessageBox.Show("Could not connect to the database of settings", code.StringValue + " " + code.StringNote, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      */

      return manager;
    }
  }
}