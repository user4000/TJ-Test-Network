using TJSettings;
using static TestNetwork.Program;

namespace TestNetwork

{
  public class ProjectManagerFactory
  {
    public static ProjectManager Create()
    {
      ProjectManager manager = new ProjectManager();
      manager.DbSettings = LocalDatabaseOfSettings.Create(ApplicationSettings.SettingsDatabaseLocation);
      return manager;
    }
  }
}