using System.Data;
using Telerik.WinControls.UI;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public DatabaseStructureManager DbManager { get; } = new DatabaseStructureManager();

    public string RootFolderName { get; private set; } = string.Empty;

    public RadTreeView VxTreeview { get; private set; } = new RadTreeView();

    public char SingleQuote { get; } = '\'';

    public int IdFolderNotFound { get; } = -1;

    public string FolderPathSeparator { get; } = @"\";

    public string DefaultFolder { get; } = "settings";

    public string DefaultFileName { get; } = "application_settings.db";

    public Converter CvManager { get; } = new Converter();

    internal DataTable TableTypes { get; private set; } = null;

    public string SqliteDatabase { get; private set; } = string.Empty;

    private LocalDatabaseOfSettings() { } // Empty private constructor //

    public static LocalDatabaseOfSettings Create() => new LocalDatabaseOfSettings();
  }
}

