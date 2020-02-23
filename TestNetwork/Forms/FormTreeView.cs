using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using static TJFramework.TJFrameworkManager;
using static TestNetwork.Program;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork, IEventEndWork
  {
    private LocalDatabaseOfSettings DbSettings { get; } = new LocalDatabaseOfSettings();

    private TreeviewManager TvManager { get; set; } = null;

    private GridSettings VxGridSettings { get; set; } = null;

    private DataTable TableFolders { get; set; } = null;

    private string NameOfSelectedNode { get; set; } = string.Empty;

    private RadTreeNode[] SearchResult { get; set; } = null;

    private string CurrentIdSetting { get; set; } = string.Empty;

    private int CurrentIdFolder { get; set; } = -1;

    private int SearchIterator { get; set; } = 0;

    public FormTreeView()
    {
      InitializeComponent(); // https://docs.telerik.com/devtools/winforms/controls/treeview/data-binding/binding-to-self-referencing-data //
      TvManager = TreeviewManager.Create(this.TvFolders, this.ImageListFolders, Program.ApplicationSettings.TreeViewFont);
    }

    private void ResetView()
    {
      ResetDataSourceForTreeview();
      VxGridSettings.ResetView();
      CurrentIdFolder = -1;
      NameOfSelectedNode = string.Empty;
      CurrentIdSetting = string.Empty;
      SearchResult = null;
      SearchIterator = 0;
      PvSettings.Visible = false;
      PnEditorTool.Visible = false;
    }

    private void ResetDataSourceForTreeview() => TvFolders.DataSource = null;

    private void SetDatabaseFile(string PathToDatabaseFile)
    {
      ResetView();
      TxDatabaseFile.Text = PathToDatabaseFile;
      DbSettings.SavePathToDatabase(PathToDatabaseFile);
      TxDatabaseFile.SelectionStart = PathToDatabaseFile.Length;
    }

    private void SetPropertiesDateTimePicker()
    {
      TxSettingDatetime.DateTimePickerElement.ShowTimePicker = true;
      TxSettingDatetime.Format = DateTimePickerFormat.Custom;
      TxSettingDatetime.CustomFormat = Manager.DatetimeFormat;
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

      SetPropertiesDateTimePicker();

      TxDatabaseFile.ReadOnly = true;
      TxFolderDelete.ReadOnly = true;

      PvEditor.SelectedPage = PgEmpty;
      PvEditor.ZzPagesVisibility(ElementVisibility.Collapsed);

      PvFolders.Pages.ChangeIndex(PgSearch, 0);
      PvFolders.SelectedPage = PgSearch;

      PvSettings.Pages.ChangeIndex(PgSetting, 0);
      PvSettings.SelectedPage = PgSetting;

      //this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.SizeMode = SplitPanelSizeMode.Absolute;
      //this.ScMain.SplitPanels[nameof(PnTreeview)].SizeInfo.AbsoluteSize = Program.ApplicationSettings.TreeViewSize;

      PnTreeview.SizeInfo.SizeMode = SplitPanelSizeMode.Absolute;
      PnTreeview.SizeInfo.AbsoluteSize = Program.ApplicationSettings.TreeViewSize;

      VxGridSettings = new GridSettings(this);
      VxGridSettings.InitializeGrid(this.GvSettings);

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
      BxSettingChange.Click += EventButtonSettingChange;
      BxSettingChange.Enabled = false;
      //BxSettingsAdd.Click += EventButtonSettingAdd;

      BxSettingsAdd.Click += async (s, e) => await EventButtonSettingSave(s, e);
      BxSettingSave.Click += async (s, e) => await EventButtonSettingSave(s, e);

      DxTypes.SelectedValueChanged += EventSettingTypeChanged;

      TvFolders.SelectedNodeChanged += async (s, e) => await EventTreeviewSelectedNodeChanged(s,e);
      ScMain.SplitterMoved += EventScMainSplitterMoved;
      GvSettings.SelectionChanged += EventGridSelectionChanged;
    }

    private int GetMessageBoxWidth(string message) => Math.Min(message.Length * 9, 500);

    internal TypeSetting GetCurrentType()
    {
      int integerValue = 0;
      try { integerValue = DxTypes.ZzGetIntegerValue(); } catch { integerValue = -1; };
      if (integerValue < 0) return TypeSetting.Unknown;
      return TypeSettingConverter.FromInteger(integerValue);
    }

    public void EventStartWork()
    {
      SetProperties(); SetEvents();
    }

    private void EventScMainSplitterMoved(object sender, SplitterEventArgs e)
    {
      if (PnTreeview.SizeInfo.AbsoluteSize.Width > (2 * PnUpper.Width) / 3)
        PnTreeview.SizeInfo.AbsoluteSize = new Size((39 * PnUpper.Width) / 100, 0);
    }

    private void ButtonChangeSettingDisable()
    {
      CurrentIdSetting = string.Empty;
      BxSettingChange.Enabled = false;
    }

    private async Task RefreshGridSettings() => VxGridSettings.RefreshGrid(await DbSettings.GetSettings(CurrentIdFolder));

    private async Task EventTreeviewSelectedNodeChanged(object sender, RadTreeViewEventArgs e)
    {
      TvFolders.HideSelection = true;

      try
      {
        NameOfSelectedNode = e.Node.Text;
      }
      catch
      {
        NameOfSelectedNode = string.Empty;
      }
      TxFolderRename.Text = NameOfSelectedNode;
      TxFolderDelete.Text = NameOfSelectedNode;

      CurrentIdFolder = DbSettings.GetIdFolder(e.Node);
      //---- Event ---- Get List of Settings of current folder ----//
      await RefreshGridSettings();
      
      if (PvSettings.Visible == false) PvSettings.Visible = true;
      ButtonChangeSettingDisable();
      VxGridSettings.Grid.HideSelection = true;

      TvFolders.HideSelection = false;
    }

    private void EventGridSelectionChanged(object sender, EventArgs e)
    {
      CurrentIdSetting = VxGridSettings.GetIdSetting();
      BxSettingChange.Enabled = CurrentIdSetting.Length > 0 ;
      if (VxGridSettings.Grid.HideSelection) VxGridSettings.Grid.HideSelection = false;
    }

    private async void EventButtonSettingChange(object sender, EventArgs e)
    {
      BxSettingChange.Enabled = false;
      //Ms.Message($"folder={CurrentIdFolder}", $"setting={CurrentIdSetting}").Pos(MsgPos.TopCenter).Debug();
      await Task.Delay(1000);
      BxSettingChange.Enabled = true;    
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

      string NameFolderDraft = TxFolderName.Text.Trim();
      string NameFolder = Manager.RemoveSpecialCharacters(NameFolderDraft);
      TxFolderName.Text = NameFolder;

      if (NameFolder.Length < 1)
        if (NameFolderDraft.Length < 1)
        {
          Ms.ShortMessage(MsgType.Fail, "Не указано название новой папки", 350, TxFolderName).Create();
          return;
        }
        else
        {
          Ms.ShortMessage(MsgType.Fail, "Вы указали символы, которые нельзя использовать в названии", 400, TxFolderName).Create();
          return;
        }

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
      else // Give out SUCCESS MESSAGE //
      {
        if (NameFolderDraft.Length == NameFolder.Length)
        {
          Ms.ShortMessage(MsgType.Debug, code.StringValue, GetMessageBoxWidth(code.StringValue), TxFolderName)
            .Offset(new Point(TxFolderName.Width, -5 * TxFolderName.Height))
            .Create();
        }
        else
        {
          Ms.Message("Некоторые указанные вами символы\nбыли исключены из названия", code.StringValue)
            .Control(TxFolderName).Offset(new Point(200, 0)).Delay(7)
            .Info();
          Ms.Message("В названии папки были указаны запрещённые символы:", NameFolderDraft).NoAlert().Warning();
          Ms.Message("Название было исправлено:", NameFolder).NoAlert().Warning();
        }

        EventRefreshDataFromDatabaseFile();
        parent = TvFolders.GetNodeByPath(ParentFullPath);
        if (parent == null)
        {
          Ms.Message(MsgType.Error, "Ошибка!", $"Метод TvFolders.GetNodeByPath(ParentFullPath) вернул значение null. ParentFullPath={ParentFullPath}", null, MsgPos.Unknown, 0).NoAlert().Create();
          Ms.ShortMessage(MsgType.Warning, $"Ошибка! Подробности в жунале сообщений", 300, TxFolderName).NoTable().Create();
        }
        else
        {
          TvManager.TryToSelectFolderAfterCreating(parent, NameFolder);
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

      string NameFolder = Manager.RemoveSpecialCharacters(TxFolderRename.Text);
      TxFolderRename.Text = NameFolder;

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
        Ms.Error("Не удалось удалить папку", ex).Control(TxFolderDelete).Create();
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
        Ms.Message("Не удалось удалить папку.", code.StringValue, TxFolderDelete).Fail();
      }
    }

    private void ClearArraySearchResult()
    {
      if ((SearchResult != null) && (SearchResult.Length > 0))
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

    private void EventLoadDataFromDatabaseFile(bool LoadDataFirstTimeFromThisFile)
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
          Ms.Message("Не удалось прочитать данные\nиз указанного вами файла.", "Указанный вами файл не является базой данных заданного типа").Control(TxDatabaseFile).Error();
        }
        else
        {
          Ms.Error("Не удалось прочитать данные\nиз указанного вами файла.", ex).Control(TxDatabaseFile).Create();
        }
      }

      if (Error == false)
        try
        {
          if (TableFolders != null)
          {
            TableFolders.Clear(); //TableFolders.Columns.Clear();// TableFolders.Dispose();
          }
          DbSettings.FillTreeView(TvFolders, table);
          TvManager.SetFontAndImageForAllNodes();
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
        if (LoadDataFirstTimeFromThisFile) EventLoadDataFromFileFirstTime();
      }
    }

    private void EventLoadDataFromFileFirstTime()
    {
      DbSettings.InitVariables(Manager);
      DbSettings.FillDropDownListForTableTypes(DxTypes);
      Ms.ShortMessage(MsgType.Debug, "Данные прочитаны.", 190, TxDatabaseFile).Offset(new Point(TxDatabaseFile.Width + 30, -2 * TxDatabaseFile.Height)).Create();
    }

    private void EventButtonSettingAdd(object sender, EventArgs e)
    {
      TypeSetting type = TypeSettingConverter.FromInteger(DxTypes.ZzGetIntegerValue());
      if (type==TypeSetting.Unknown)
      {
        Ms.Message("Ошибка!", "Нельзя добавлять переменную неизвестного типа").Control(TxSettingAdd).Warning();
        return;
      }    
    }

    private async Task EventButtonSettingSave(object sender, EventArgs e)
    {
      ReturnCode code = ReturnCodeFactory.Error("Не удалось создать переменную");
      string IdSetting = Manager.RemoveSpecialCharacters(TxSettingAdd.Text);
      TxSettingAdd.Text = IdSetting;
      if (IdSetting.Length < 1)
      {
        Ms.Message("Ошибка", "Не указано имя переменной").Control(DxTypes).Warning(); return;
      }

      switch (GetCurrentType())
      {
        case TypeSetting.Boolean:
          PvEditor.SelectedPage = PgBoolean;
          code = DbSettings.SaveSettingBoolean(true, CurrentIdFolder, IdSetting, SwBooleanValue.Value);
          SwBooleanValue.Value = false;
          break;
        case TypeSetting.Datetime:
          PvEditor.SelectedPage = PgDatetime;
          code = DbSettings.SaveSettingDatetime(true, CurrentIdFolder, IdSetting, TxSettingDatetime.Value);
          TxSettingDatetime.Value = DateTime.Today;
          break;
        case TypeSetting.Text:
          PvEditor.SelectedPage = PgText;
          break;
        default:
          PvEditor.SelectedPage = PgEmpty;
          break;
      }

      if (code.Success)
      {
        TxSettingAdd.Clear();
        Ms.Message("Данные записаны", code.StringValue).Control(DxTypes).Offset(30, -150).Ok();
        await RefreshGridSettings();
      }
      else
      {
        Ms.Message("Произошла ошибка", code.StringValue).Control(DxTypes).Offset(30, -150).Warning();
      }
    }

    private void EventSettingTypeChanged(object sender, EventArgs e)
    {
      switch (GetCurrentType())
      {
        case TypeSetting.Boolean:
          PvEditor.SelectedPage = PgBoolean;
          break;
        case TypeSetting.Datetime:
          PvEditor.SelectedPage = PgDatetime;
          TxSettingDatetime.Value = DateTime.Today;
          break;
        case TypeSetting.Text:
          PvEditor.SelectedPage = PgText;
          break;
        default:
          PvEditor.SelectedPage = PgEmpty;
          break;
      }

      // Ms.Message("aaa", $"{type.ToString()}").Pos(MsgPos.TopCenter).Debug();
    }

    public void EventEndWork()
    {
      Program.ApplicationSettings.TreeViewSize = PnTreeview.SizeInfo.AbsoluteSize;
    }
  }
}

