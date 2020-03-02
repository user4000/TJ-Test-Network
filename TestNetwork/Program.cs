using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using TJFramework;
using TJFramework.FrameworkSettings;

namespace TestNetwork // TODO: Создать иерархию DLL-сборок - от стандартов до их потребителей //
{
  static class Program
  {
    public static string ApplicationUniqueName { get; } = "Application settings editor";

    private static Mutex AxMutex = null;

    public static ProjectManager Manager { get; set; } = null;

    public static CxApplicationSettings ApplicationSettings { get => TJFrameworkManager.ApplicationSettings<CxApplicationSettings>(); } // User custom settings in Property Grid //

    public static TJStandardFrameworkSettings FrameworkSettings { get; } = TJFrameworkManager.FrameworkSettings; // Framework embedded settings //

    private static void TestEventPageChanged(string PageName)
    {
      MessageBox.Show("You have changed a page = " + PageName);
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      AxMutex = new Mutex(true, ApplicationUniqueName, out bool createdNew);
      if (!createdNew)
      {
        MessageBox.Show("Another instance of the application is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      TJFrameworkManager.Logger.FileSizeLimitBytes = 1000000;
      TJFrameworkManager.Logger.Create();

      TJFrameworkManager.Service.CreateApplicationSettings<CxApplicationSettings>();
      TJFrameworkManager.Service.AddForm<FormServer>("Server");
      TJFrameworkManager.Service.AddForm<FormClient>("Client");
      TJFrameworkManager.Service.AddForm<FormTreeView>("TreeView");
      TJFrameworkManager.Service.SetMainFormCaption("Application setting editor");
      TJFrameworkManager.Service.StartPage<FormTreeView>();
      //TJFrameworkManager.Service.SetMainPageViewOrientation(StripViewAlignment.Left);

      FrameworkSettings.HeaderFormSettings = "Settings";
      FrameworkSettings.HeaderFormLog = "Message log";
      FrameworkSettings.HeaderFormExit = "Exit";
      //FrameworkSettings.ConfirmExitButtonText = "Confirm exit";

      FrameworkSettings.MainFormMinimizeToTray = false;
      FrameworkSettings.VisualEffectOnStart = true;
      FrameworkSettings.VisualEffectOnExit = true;

      FrameworkSettings.ValueColumnWidthPercent = 60;
      FrameworkSettings.MainPageViewReducePadding = true;
      FrameworkSettings.RememberMainFormLocation = true;
      FrameworkSettings.PageViewFont = new Font("Verdana", 10);

      FrameworkSettings.FontAlertText = new Font("Verdana", 8);
      FrameworkSettings.FontAlertCaption = new Font("Verdana", 8);
      FrameworkSettings.MaxAlertCount = 3;
      FrameworkSettings.LimitNumberOfAlerts = true;
      FrameworkSettings.SecondsAlertAutoClose = 5;
      FrameworkSettings.FontAlertCaption = new Font("Verdana", 9);
      FrameworkSettings.FontAlertText = new Font("Verdana", 9);

      Manager = ProjectManagerFactory.Create();

      Action ExampleOfVisualSettingsAndEvents = () =>
      {
        FrameworkSettings.MainFormMargin = 50;
        FrameworkSettings.PageViewItemSize = new Size(200, 30);
        FrameworkSettings.ItemSizeMode = PageViewItemSizeMode.EqualSize;
        FrameworkSettings.PageViewItemSpacing = 50;
        FrameworkSettings.PropertyGridPadding = new Padding(155, 100, 45, 25);

        // Привязка различных событий фреймворка к методам класса CxManager //
        //TJFrameworkManager.Service.EventPageChanged = Manager.EventPageChanged;
        //TJFrameworkManager.Service.EventBeforeMainFormClose = Manager.EventBeforeMainFormClose;
        //TJFrameworkManager.Service.EventBeforeMainFormCloseAsync = Manager.EventBeforeMainFormCloseAsync();
      };

      TJFrameworkManager.Run();
    }
  }
}