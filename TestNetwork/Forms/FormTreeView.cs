using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork, IEventEndWork
  {
    private LocalDatabaseOfSettings DbSettings { get; } = new LocalDatabaseOfSettings();

    private DataTable TableFolders { get; set; } = null;

    private string NameOfSelectedNode { get; set; } = string.Empty;

    private RadTreeNode[] SearchResult { get; set; } = null;

    private int SearchIterator { get; set; } = 0;

    public FormTreeView()
    {
      InitializeComponent(); // https://docs.telerik.com/devtools/winforms/controls/treeview/data-binding/binding-to-self-referencing-data //
    }

    private void ResetDataSourceForTreeview() => TvFolders.DataSource = null;

    private void SetDatabaseFile(string PathToDatabaseFile)
    {
      TxDatabaseFile.Text = PathToDatabaseFile;
      ResetDataSourceForTreeview();
      DbSettings.SavePathToDatabase(PathToDatabaseFile);
      TxDatabaseFile.SelectionStart = PathToDatabaseFile.Length;
    }

    private void SetProperties()
    {
      BxOpenFile.ShowBorder = false;
      BxSelectFile.ShowBorder = false;
      BxFolderAdd.ShowBorder = false;
      BxFolderRename.ShowBorder = false;
      BxFolderDelete.ShowBorder = false;
      BxFolderSearch.ShowBorder = false;
      BxFolderSearchGotoNext.ShowBorder = false;
      BxFolderSearchGotoNext.Visibility = ElementVisibility.Collapsed;

      PvFolders.Pages.ChangeIndex(PgSearch, 0);
      PvFolders.Pages.ChangeIndex(PgAdd, 1);
      PvFolders.Pages.ChangeIndex(PgRename, 2);
      PvFolders.Pages.ChangeIndex(PgDelete, 3);
      PvFolders.SelectedPage = PgSearch;

      /*if (Program.ApplicationSettings.TreeViewSize.Width < 200)
        Program.ApplicationSettings.TreeViewSize = new Size(this.Width * (37/100), 0);*/
      this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.SizeMode = SplitPanelSizeMode.Absolute;
      this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.AbsoluteSize = Program.ApplicationSettings.TreeViewSize;

      TvFolders.ImageList = this.ImageListFolders;
      TvFolders.Font = Program.ApplicationSettings.TreeViewFont;
      DbSettings.SetFontOfNode(TvFolders.Font);

      TxDatabaseFile.ReadOnly = true;
      TxFolderDelete.ReadOnly = true;

      SetDatabaseFile(Program.ApplicationSettings.SettingsDatabaseLocation);
    }

    private void SetEvents()
    {
      BxOpenFile.Click += EventButtonLoadData;
      BxSelectFile.Click += EventButtonChooseFile;
      BxFolderAdd.Click += EventButtonAddFolder;
      BxFolderRename.Click += EventButtonRenameFolder;
      BxFolderDelete.Click += EventButtonDeleteFolder;
      BxFolderSearch.Click += EventButtonSearchFolder;
      BxFolderSearchGotoNext.Click += EventButtonSearchFolderGotoNext;
      TvFolders.SelectedNodeChanged += EventTreeviewSelectedNodeChanged;
      ScMain.SplitterMoved += EventScMainSplitterMoved;
    }

    private int GetMessageBoxWidth(string message) => Math.Min(message.Length * 9, 500);

    public void EventStartWork()
    {
      SetProperties(); SetEvents();
    }

    private void EventScMainSplitterMoved(object sender, SplitterEventArgs e)
    {
      if (this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.AbsoluteSize.Width > (2 * PnUpper.Width) / 3)
        this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.AbsoluteSize = new Size((39 * PnUpper.Width) / 100, 0);
    }

    private void EventTreeviewSelectedNodeChanged(object sender, RadTreeViewEventArgs e)
    {
      try { NameOfSelectedNode = e.Node.Text; } catch { NameOfSelectedNode = string.Empty; }
      TxFolderRename.Text = NameOfSelectedNode;
      TxFolderDelete.Text = NameOfSelectedNode;
    }

    private void SelectOneNode(RadTreeNode node)
    {
      if (node != null)
        try
        {
          node.Selected = true;
          node.EnsureVisible();
        }
        catch
        {
          ClearArraySearchResult();
        }
    }

    private void EventButtonAddFolder(object sender, EventArgs e)
    {
      RadTreeNode parent = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error("Ошибка при добавлении папки");

      if (parent == null)
      {
        Ms.ShortMessage(MsgType.Fail, "Не указана папка, в которую добавляется новая", 380, TxFolderName).Create();
        return;
      }

      int IdFolder = DbSettings.GetIdFolder(parent);
      string ParentFullPath = parent.FullPath;
      bool Error = false;

      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "Не указана папка, в которую добавляется новая", 380, TxFolderName).Create();
        return;
      }

      if (TxFolderName.Text.Trim().Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "Не указано название новой папки", 350, TxFolderName).Create();
        return;
      }

      string NameFolder = TxFolderName.Text.Trim();

      int IdNewFolder = -1;
      const string ErrorHeader = "Не удалось добавить новую папку";
      try
      {
        code = DbSettings.FolderInsert(IdFolder, NameFolder);
      }
      catch (Exception ex)
      {
        Error = true;
        Ms.Error(ErrorHeader, ex).Control(TxFolderName).Create();
        IdNewFolder = -1;
      }

      IdNewFolder = code.IdObject;

      if (code.Error)
      {
        if (Error == false) Ms.Message(ErrorHeader, code.StringValue, TxFolderName).Fail();
      }

      if (IdNewFolder <= 0)
      {
        if ((Error == false) && (code.Success)) Ms.Message(ErrorHeader, code.StringValue, TxFolderName).Fail();
      }
      else
      {
        Ms.ShortMessage(MsgType.Debug, code.StringValue, GetMessageBoxWidth(code.StringValue), TxFolderName).Create();
        EventRefreshDataFromDatabaseFile();
        parent = TvFolders.GetNodeByPath(ParentFullPath);
        if (parent == null)
        {
          Ms.Message(MsgType.Error, "Ошибка!", $"Метод TvFolders.GetNodeByPath(ParentFullPath) вернул значение null. ParentFullPath={ParentFullPath}", null, MsgPos.Unknown, 0).NoAlert().Create();
          Ms.ShortMessage(MsgType.Warning, $"Ошибка! Подробности в жунале сообщений", 300, TxFolderName).NoTable().Create();
        }
        else
        {
          parent.Expanded = true;
          parent.Selected = true;
        }
      }

      TxFolderName.Clear();
    }

    private void EventButtonRenameFolder(object sender, EventArgs e)
    {
      RadTreeNode node = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error("Ошибка!");

      if (node == null)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указана папка, которую нужно переименовать.", 380, TxFolderRename).Create();
        return;
      }

      int IdFolder = DbSettings.GetIdFolder(node);
      string NodeFullPath = node.FullPath;

      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указана папка, которую нужно переименовать.", 380, TxFolderRename).Create();
        return;
      }

      string NameFolder = TxFolderRename.Text.Trim();

      if (NameFolder.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указано новое название папки.", 350, TxFolderRename).Create();
        return;
      }

      try
      {
        code = DbSettings.FolderRename(IdFolder, NameFolder);
      }
      catch (Exception ex)
      {
        Ms.Error("Не удалось переименовать папку", ex).Control(TxFolderRename).Create();
        return;
      }

      if (code.Success)
      {
        Ms.ShortMessage(MsgType.Debug, code.StringValue, GetMessageBoxWidth(code.StringValue), TxFolderRename).Create();
        ClearArraySearchResult();
        node.Text = NameFolder;
        node.Selected = false;
        node.Selected = true;
      }
      else
      {
        Ms.Message("Не удалось переименовать папку.", code.StringValue, TxFolderRename).Fail();
      }
    }

    private void EventButtonDeleteFolder(object sender, EventArgs e)
    {
      RadTreeNode node = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error("Ошибка!");

      if (node == null)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указана папка, которую нужно удалить.", 380, TxFolderDelete).Create();
        return;
      }

      RadTreeNode parent = node.Parent;
      if (parent == null)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не найдена папка, содержащая удаляемую папку", 380, TxFolderDelete).Create();
        return;
      }

      string NodeFullPath = parent.FullPath;
      int IdFolder = DbSettings.GetIdFolder(node);

      try
      {
        code = DbSettings.FolderDelete(IdFolder, node.Text);
      }
      catch (Exception ex)
      {
        Ms.Error("Не удалось удалить папку", ex).Control(TxFolderRename).Create();
        return;
      }

      if (code.Success)
      {
        Ms.ShortMessage(MsgType.Debug, code.StringValue, GetMessageBoxWidth(code.StringValue), TxFolderDelete).Create();
        parent.Selected = true;
        ClearArraySearchResult();
        node.Remove();
        if (parent.Nodes.Count < 1) parent.ImageIndex = 0;
      }
      else
      {
        Ms.Message("Не удалось удалить папку.", code.StringValue, TxFolderRename).Fail();
      }
    }

    private void ClearArraySearchResult()
    {
      if (SearchResult.Length > 0)
      {
        Array.Clear(SearchResult, 0, SearchResult.Length);
        SearchIterator = 0;
      }
      if (BxFolderSearchGotoNext.Visibility == ElementVisibility.Visible) BxFolderSearchGotoNext.Visibility = ElementVisibility.Collapsed;
    }

    private void EventButtonSearchFolder(object sender, EventArgs e)
    {
      string NameFolder = TxFolderSearch.Text;
      if (SearchResult?.Length > 0) ClearArraySearchResult();

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.StartWith)
        SearchResult = TvFolders.FindNodes(x => x.Name.StartsWith(NameFolder));

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.Contains)
        SearchResult = TvFolders.FindNodes(x => x.Name.Contains(NameFolder));

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.WholeWord)
        SearchResult = TvFolders.FindNodes(x => x.Name == NameFolder);

      Point offset = new Point(PvFolders.Width, -1 * PvFolders.Height);

      SearchIterator = 0;
      if (SearchResult.Length > 0) SelectOneNode(SearchResult[0]);

      BxFolderSearchGotoNext.Visibility = ElementVisibility.Collapsed;

      if (SearchResult.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "Поиск не дал результатов", 200, PvFolders).Offset(offset).NoTable().Create();
      }

      if (SearchResult.Length == 1)
      {
        Ms.ShortMessage(MsgType.Debug, "Найдена 1 папка", 200, PvFolders).Offset(offset).NoTable().Create();
      }

      if (SearchResult.Length > 1)
      {
        Ms.ShortMessage(MsgType.Info, $"Найдено элементов: {SearchResult.Length}", 200, PvFolders).Offset(offset).NoTable().Create();
        SearchIterator = 1;
        BxFolderSearchGotoNext.Visibility = ElementVisibility.Visible;
      }
    }

    private void EventButtonSearchFolderGotoNext(object sender, EventArgs e)
    {
      if (SearchResult.Length < 1) return;
      if (SearchResult.Length > SearchIterator)
      {
        SelectOneNode(SearchResult[SearchIterator]);
        SearchIterator++;
        if (SearchResult.Length == SearchIterator)
        {
          Ms.ShortMessage(MsgType.Ok, "Поиск завершён", 200, TxFolderSearch).Offset(new Point(TxFolderSearch.Width, 0)).NoTable().Create();
        }
      }
      else
      {
        SearchIterator = 0;
        SelectOneNode(SearchResult[SearchIterator]);
        SearchIterator++;
      }
    }

    private void EventButtonChooseFile(object sender, EventArgs e)
    {
      DialogResult result = DialogOpenFile.ShowDialog();
      if (result == DialogResult.OK)
      {
        SetDatabaseFile(DialogOpenFile.FileName);
      }
    }

    private void EventButtonLoadData(object sender, EventArgs e)
    {
      EventLoadDataFromDatabaseFile(true);
    }

    private void EventRefreshDataFromDatabaseFile() => EventLoadDataFromDatabaseFile(false);

    private void EventLoadDataFromDatabaseFile(bool LoadDataFirstTime)
    {
      DataTable table = null; bool Error = false;
      try
      {
        table = DbSettings.GetTableFolders();
      }
      catch (Exception ex)
      {
        Error = true;
        if (ex.Message.Contains("not a database"))
        {
          Ms.Message("Не удалось прочитать данные \nиз указанного вами файла.", "Указанный вами файл не является базой данных заданного типа").Control(TxDatabaseFile).Error();
        }
        else
        {
          Ms.Error("Не удалось прочитать данные \nиз указанного вами файла.", ex).Control(TxDatabaseFile).Create();
        }
      }

      if (Error == false)
        try
        {
          if (TableFolders != null)
          {
            TableFolders.Clear();
            //TableFolders.Columns.Clear();
            //TableFolders.Dispose();
          }
          DbSettings.FillTreeView(TvFolders, table);
          TableFolders = table;
        }
        catch (Exception ex)
        {
          Error = true;
          Ms.Error("Ошибка при попытке чтения структуры настроек из файла.", ex).Control(TxDatabaseFile).Create();
        }

      if (Error == false)
      {
        Program.ApplicationSettings.SettingsDatabaseLocation = TxDatabaseFile.Text;
        if (LoadDataFirstTime) EventLoadDataFromFileFirstTime();
      }
    }

    public void EventLoadDataFromFileFirstTime()
    {
      DbSettings.InitVariables();
      DbSettings.FillDropDownListForTableTypes(DxTypes);
      Ms.ShortMessage(MsgType.Debug, "Данные прочитаны.", 190, TxDatabaseFile).Offset(new Point(TxDatabaseFile.Width + 30, -2 * TxDatabaseFile.Height)).Create();
    }

    public void EventEndWork()
    {
      Program.ApplicationSettings.TreeViewSize = this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.AbsoluteSize;
    }
  }
}


/*
    private void LoadTestData()
    {
      RadTreeNode root = this.TvFolders.Nodes.Add("Programming", 17);
      root.Nodes.Add("Microsoft Research News and Highlights", 1);

      root.Nodes.Add("Joel on Software", 2);
      root.Nodes.Add("Miguel de Icaza", 3);
      root.Nodes.Add("channel 9", 4);

      root = this.TvFolders.Nodes.Add("News (1)", 18);
      root.Nodes.Add("cnn.com (1)", 5);
      root.Nodes.Add("msnbc.com", 6);
      root.Nodes.Add("reuters.com", 7);
      root.Nodes.Add("bbc.co.uk", 8);

      root = this.TvFolders.Nodes.Add("Personal (19)", 3);
      root.Nodes.Add("sports (2)", 9);
      RadTreeNode folder = root.Nodes.Add("fun (17)", 7);
      folder.Nodes.Add("Lolcats (2)", 10);
      folder.Nodes.Add("FFFOUND (15)", 11);
      folder.Nodes.Add("axaxaxaxa (2)", 0);
      folder.Nodes.Add("dsfdsfsdf  (15)", 1);

      this.TvFolders.Nodes.Add("Telerik blogs", 12);
      this.TvFolders.Nodes.Add("Techcrunch", 13);
      this.TvFolders.Nodes.Add("Engadget", 14);
      this.TvFolders.Nodes.Add("Engadget 111", 15);
      this.TvFolders.Nodes.Add("Engadget 222", 16);
      this.TvFolders.Nodes.Add("Engadget 333", 15);
    }
*/
