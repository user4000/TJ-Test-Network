using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using TJFramework;
using TJSettings;
using TJStandard;
using static TestNetwork.Program;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork, IEventEndWork
  {
    private int HeightCollapsed { get; } = 82;
    private int HeightExpanded { get; } = 126;
    private int HeightForLongText { get; } = 200;

    private LocalDatabaseOfSettings DbSettings { get => Manager.DbSettings; }

    private TreeviewManager TvManager { get; set; } = null;
    private GridSettings VxGridSettings { get; set; } = null;
    private DataTable TableFolders { get; set; } = null;
    private string NameOfSelectedNode { get; set; } = string.Empty;
    private RadTreeNode[] SearchResult { get; set; } = null;
    private Setting CurrentSetting { get; set; } = null;
    private string CurrentIdSetting { get; set; } = string.Empty;
    private int CurrentIdFolder { get; set; } = -1;
    private int SearchIterator { get; set; } = 0;

    public FormTreeView()
    {
      InitializeComponent();
      // https://docs.telerik.com/devtools/winforms/controls/treeview/data-binding/binding-to-self-referencing-data //
      TvManager = TreeviewManager.Create(this.TvFolders, this.ImageListFolders, Program.ApplicationSettings.TreeViewFont);
    }

    public void EventStartWork()
    {
      SetProperties(); SetEvents();
    }

    private void SetProperties()
    {
      Padding NoPadding = new Padding(0, 0, 0, 0);
      BxOpenFile.ShowBorder = false;
      BxSelectFile.ShowBorder = false;
      BxFolderAdd.ShowBorder = false;
      BxFolderRename.ShowBorder = false;
      BxFolderDelete.ShowBorder = false;
      BxFolderSearch.ShowBorder = false;
      BxFolderSearchGotoNext.ShowBorder = false;
      BxFolderSearchGotoNext.Visibility = ElementVisibility.Collapsed;
      BxSettingAddNew.ShowBorder = false;
      BxSettingRename.ShowBorder = false;
      BxSettingFileSelect.ShowBorder = false;
      BxSettingColorSelect.ShowBorder = false;
      BxSettingFontSelect.ShowBorder = false;
      BxSettingFolderSelect.ShowBorder = false;
      BxSettingDelete.ShowBorder = false;
      BxSettingChange.Enabled = false;
      BxSettingSave.Enabled = false;
      BxSettingCancel.Enabled = false;
      BxNewFileName.ShowBorder = false;
      BxCreateNewDatabase.ShowBorder = false;

      SetPropertiesDateTimePicker();

      TxDatabaseFile.ReadOnly = true;
      TxFolderDelete.ReadOnly = true;

      PvEditor.SelectedPage = PgEmpty;
      PvEditor.ZzPagesVisibility(ElementVisibility.Collapsed);

      PvFolders.Pages.ChangeIndex(PgFolderDelete, 4);
      PvFolders.Pages.ChangeIndex(PgFolderRename, 3);
      PvFolders.Pages.ChangeIndex(PgFolderAdd, 2);
      PvFolders.Pages.ChangeIndex(PgFolderSearch, 1);
      PvFolders.Pages.ChangeIndex(PgDatabase, 0);
      PvFolders.SelectedPage = PgDatabase;

      BxSettingUp.Left = BxSettingCancel.Location.X + BxSettingCancel.Size.Width + 2 * BxSettingUp.Size.Width;
      BxSettingDown.Left = BxSettingCancel.Location.X + BxSettingCancel.Size.Width + 4 * BxSettingUp.Size.Width;

      PvSettings.Pages.ChangeIndex(PgSettingChange, 0);
      PvSettings.SelectedPage = PgSettingChange;

      PnTreeview.SizeInfo.SizeMode = SplitPanelSizeMode.Absolute;
      PnTreeview.SizeInfo.AbsoluteSize = Program.ApplicationSettings.TreeViewSize;

      PnSettingAddTool.PanelElement.PanelBorder.Visibility = ElementVisibility.Hidden;
      PnSettingChangeTool.PanelElement.PanelBorder.Visibility = ElementVisibility.Hidden;
      PnSettingAddTop.PanelElement.PanelBorder.Visibility = ElementVisibility.Hidden;
      PnSettingChangeTop.PanelElement.PanelBorder.Visibility = ElementVisibility.Hidden;

      StxLongInteger.ZzSetIntegerNumberOnly();
      StxDatetime.CalendarSize = new Size(400, 350);

      VxGridSettings = new GridSettings(this);
      VxGridSettings.InitializeGrid(this.GvSettings);

      PgSettingEmpty.Item.Visibility = ElementVisibility.Collapsed;
      PgSettingMessage.Item.Visibility = ElementVisibility.Collapsed;

      PvEditor.Padding = new Padding(0, 1, 0, 0);
      PvEditor.Margin = NoPadding;

      PnSettingAddTool.Padding = NoPadding;
      PnSettingAddTool.Margin = NoPadding;

      PnSettingChangeTool.Padding = NoPadding;
      PnSettingChangeTool.Margin = NoPadding;

      StxDatetime.Value = DateTime.Today;

      SetDatabaseFile(Program.ApplicationSettings.SettingsDatabaseLocation);
      Manager.InitVariables(this);
    }

    private void SetEvents()
    {
      BxOpenFile.Click += async (s, e) => await EventButtonLoadData(s, e);
      BxSelectFile.Click += EventButtonChooseFile;
      BxFolderAdd.Click += EventButtonAddFolder;
      BxFolderRename.Click += EventButtonRenameFolder;
      BxFolderDelete.Click += EventButtonDeleteFolder;
      BxFolderSearch.Click += EventButtonSearchFolder;
      BxFolderSearchGotoNext.Click += EventButtonSearchFolderGotoNext;

      this.BxSettingAddNew.Click += async (s, e) => await EventButtonSettingAddNew(s, e); // SAVE setting (INSERT) //
      this.BxSettingSave.Click += async (s, e) => await EventButtonSettingUpdateExisting(s, e); // SAVE setting (UPDATE) //
      this.BxSettingDelete.Click += async (s, e) => await EventButtonSettingDelete(s, e); // Delete setting //
      this.BxSettingRename.Click += async (s, e) => await EventButtonSettingRename(s, e); // Rename setting //
      this.BxSettingChange.Click += EventButtonSettingChange; // Change value of setting // it is async method and await works inside it //
      this.BxSettingCancel.Click += EventButtonSettingCancel; // Cancel changing setting //
      this.BxSettingUp.Click += async (s, e) => await EventButtonSettingUp(s, e); // Change Rank - go up //
      this.BxSettingDown.Click += async (s, e) => await EventButtonSettingDown(s, e); // Change Rank - go down //

      BxNewFileName.Click += EventButtonNewFileName;
      BxCreateNewDatabase.Click += EventButtonCreateNewDatabase;

      PvSettings.SelectedPageChanging += EventForAllPageViewSelectedPageChanging;
      PvFolders.SelectedPageChanging += EventForAllPageViewSelectedPageChanging;
      PvSettings.SelectedPageChanging += EventForAllPageViewSelectedPageChanging;

      BxSettingFileSelect.Click += EventButtonSettingFileSelect;
      BxSettingFolderSelect.Click += EventButtonSettingFolderSelect;
      BxSettingColorSelect.Click += EventButtonSettingColorSelect;
      BxSettingFontSelect.Click += EventButtonSettingFontSelect;

      DxTypes.SelectedValueChanged += EventSettingTypeChanged; // Select TYPE of a NEW variable //

      PvSettings.SelectedPageChanged += EventSettingsSelectedPageChanged; // Settings Change, Add, Rename, Delete //

      TvFolders.SelectedNodeChanged += async (s, e) => await EventTreeviewSelectedNodeChanged(s, e); // SELECTED NODE CHANGED //
      ScMain.SplitterMoved += EventScMainSplitterMoved;
      GvSettings.SelectionChanged += EventGridSelectionChanged; // User has selected a new row //
    }

    private void ResetView()
    {
      ResetDataSourceForTreeview();
      VxGridSettings.ResetView();
      CurrentIdFolder = -1;
      NameOfSelectedNode = string.Empty;
      CurrentIdSetting = string.Empty;
      CurrentSetting = null;
      SearchResult = null;
      SearchIterator = 0;
      PvSettings.Visible = false;
    }

    private void ResetDataSourceForTreeview() => TvFolders.DataSource = null;

    private void SetDatabaseFile(string PathToDatabaseFile)
    {
      ResetView();
      TxDatabaseFile.Text = PathToDatabaseFile;
      DbSettings.SetPathToDatabase(PathToDatabaseFile);
      TxDatabaseFile.SelectionStart = PathToDatabaseFile.Length;
    }

    private void SetPropertiesDateTimePicker()
    {
      StxDatetime.DateTimePickerElement.ShowTimePicker = true;
      StxDatetime.Format = DateTimePickerFormat.Custom;
      StxDatetime.CustomFormat = Manager.CvManager.CvDatetime.DatetimeFormat;
    }

    private void EventForAllPageViewSelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
    {
      e.Cancel = !Manager.UiControl.FlagAllowChangeSelectedItem;
    }

    private void ShowNotification(bool Success, string Message)
    {
      if (Message.Length < 1)
      {
        PicSettingMessage.Image = null; LxSettingMessage.Text = string.Empty;
      }
      else
      {
        if (Success)
        {
          PicSettingMessage.Image = PicOk.Image; LxSettingMessage.ForeColor = Color.DarkGreen;
        }
        else
        {
          PicSettingMessage.Image = PicError.Image; LxSettingMessage.ForeColor = Color.DarkViolet;
        }
        LxSettingMessage.Text = Message;
      }
      PvSettings.SelectedPage = PgSettingMessage;
      PgSettingMessage.Select(); // <-- If we do not do that the row of the grid remains selected //
    }

    private async Task EventButtonSettingRename(object sender, EventArgs e)
    {
      ReturnCode code = ReturnCodeFactory.Error("An error occurred while trying to change the setting name");
      string NameSettingDraft = TxSettingRename.Text.Trim();
      string NameSetting = NameSettingDraft.RemoveSpecialCharacters();
      TxSettingRename.Text = NameSetting;

      if (NameSetting.Length < 1)
        if (NameSettingDraft.Length < 1)
        {
          Ms.ShortMessage(MsgType.Fail, "Missing new setting name", 350, TxSettingRename).Create();
          return;
        }
        else
        {
          Ms.ShortMessage(MsgType.Fail, "You have specified characters that cannot be used in the name of setting", 400, TxSettingRename).Create();
          return;
        }

      const string ErrorHeader = "Failed to change setting name";
      try
      {
        code = DbSettings.SettingRename(CurrentIdFolder, CurrentIdSetting, NameSetting);
      }
      catch (Exception ex)
      {
        Ms.Error(ErrorHeader, ex).Control(TxSettingRename).Create();
        return;
      }

      if (code.Success)
      {
        await RefreshGridSettingsAndClearSelection(); // Setting was renamed //
        //Ms.Message($"{NameSetting}", code.StringValue).Wire(TxSettingRename).Offset(TxSettingRename.Width, -2 * TxSettingRename.Height).Ok();
        //SettingsToolbarSetEmptyPage();
      }
      else
      {
        Ms.Message(ErrorHeader, code.StringValue).Wire(TxSettingRename).Warning();
      }
      ShowNotification(code.Success, code.StringValue);
    }

    private async Task EventButtonSettingDelete(object sender, EventArgs e)
    {
      const string ErrorHeader = "Error trying to delete a setting";
      ReturnCode code = ReturnCodeFactory.Error(ErrorHeader);
      string NameSetting = CurrentIdSetting;
      try
      {
        code = DbSettings.SettingDelete(CurrentIdFolder, CurrentIdSetting);
      }
      catch (Exception ex)
      {
        Ms.Error(ErrorHeader, ex).Control(TxSettingRename).Create();
        return;
      }

      if (code.Success)
      {
        await RefreshGridSettingsAndClearSelection(); // Setting was deleted //
      }
      else
      {
        Ms.Message(ErrorHeader, code.StringValue).Wire(TxSettingDelete).Warning();
      }
      ShowNotification(code.Success, code.StringValue);
    }

    private void EventButtonSettingFontSelect(object sender, EventArgs e)
    {
      FontDialog dialog = new FontDialog();
      DialogResult result = dialog.ShowDialog();
      if (result == DialogResult.OK) StxFont.Text = Manager.CvManager.CvFont.ToString(dialog.Font);
    }

    private void EventButtonSettingColorSelect(object sender, EventArgs e)
    {
      RadColorDialog dialog = new RadColorDialog();
      if ((StxColor.Text.Length > 0) && (PvSettings.SelectedPage == PgSettingChange)) dialog.SelectedColor = Manager.CvManager.CvColor.FromString(StxColor.Text).Value;
      DialogResult result = dialog.ShowDialog();
      if (result == DialogResult.OK) StxColor.Text = Manager.CvManager.CvColor.ToString(dialog.SelectedColor);
    }

    private void EventButtonSettingFolderSelect(object sender, EventArgs e)
    {
      RadOpenFolderDialog dialog = new RadOpenFolderDialog();
      DialogResult result = dialog.ShowDialog();
      if (result == DialogResult.OK) StxFolder.Text = dialog.FileName;
    }

    private void EventButtonSettingFileSelect(object sender, EventArgs e)
    {
      RadOpenFileDialog dialog = new RadOpenFileDialog();
      DialogResult result = dialog.ShowDialog();
      if (result == DialogResult.OK) StxFile.Text = dialog.FileName;
    }

    private void EventSettingsSelectedPageChanged(object sender, EventArgs e)
    {
      if ((PvSettings.SelectedPage == PgSettingAdd) && (PvEditor.Parent != PnSettingAddTool))
      {
        PanelSettingsChangeSizeBySettingType(TypeSetting.Unknown);
        PvEditor.Parent = PnSettingAddTool;
      }
      DxTypes.SelectedIndex = (int)(TypeSetting.Unknown); // Unknown //
      SettingEditorResetAllInputControls();
    }

    private void SettingEditorResetAllInputControls()
    {
      StxBoolean.Value = false;
      StxDatetime.Value = DateTime.Today;
      StxFile.Clear();
      StxFolder.Clear();
      StxLongInteger.Clear();
      StxPassword.Clear();
      StxText.Clear();
      StxFont.Clear();
      StxColor.Clear();
    }

    private int GetMessageBoxWidth(string message) => Math.Min(message.Length * 9, 500);

    internal TypeSetting GetTypeFromDropDownList()
    {
      int integerValue = 0;
      try { integerValue = DxTypes.ZzGetIntegerValue(); } catch { integerValue = -1; };
      if (integerValue < 0) return TypeSetting.Unknown;
      return TypeSettingConverter.FromInteger(integerValue);
    }

    private void EventScMainSplitterMoved(object sender, SplitterEventArgs e)
    {
      if (PnTreeview.SizeInfo.AbsoluteSize.Width > (2 * PnUpper.Width) / 3)
        PnTreeview.SizeInfo.AbsoluteSize = new Size((39 * PnUpper.Width) / 100, 0);
    }


    private void EventSettingOneRowSelected() // Event - user selected a setting in the grid //
    {
      bool OneRowSelected = CurrentIdSetting.Length > 0;

      BxSettingChange.Enabled = OneRowSelected;
      BxSettingUp.Enabled = OneRowSelected;
      BxSettingDown.Enabled = OneRowSelected;

      TxSettingRename.Text = CurrentIdSetting;
      TxSettingDelete.Text = CurrentIdSetting;
      if (PgSettingDelete.Enabled != OneRowSelected) PgSettingDelete.Enabled = OneRowSelected;
      if (PgSettingRename.Enabled != OneRowSelected) PgSettingRename.Enabled = OneRowSelected;

      if (OneRowSelected && (PvSettings.SelectedPage == PgSettingMessage))
      {
        PvSettings.SelectedPage = PgSettingEmpty;
      }
    }

    private async Task RefreshGridSettings()
    {
      var list = await DbSettings.GetSettings(CurrentIdFolder);
      VxGridSettings.RefreshGrid(list);
    }

    private void EventSettingClearSelection()
    {
      CurrentIdSetting = string.Empty;
      CurrentSetting = null;
      BxSettingChange.Enabled = false;
      PgSettingDelete.Enabled = false;
      PgSettingRename.Enabled = false;
      BxSettingUp.Enabled = false;
      BxSettingDown.Enabled = false;
    }

    private async Task RefreshGridSettingsAndClearSelection()
    {
      await RefreshGridSettings();
      if (PvSettings.Visible == false) PvSettings.Visible = true;
      EventSettingClearSelection();
      VxGridSettings.Grid.HideSelection = true;
      VxGridSettings.Grid.GridNavigator.ClearSelection();
    }

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
      await RefreshGridSettingsAndClearSelection(); // current treeview node was changed //

      if ((PvSettings.SelectedPage == PgSettingRename) || (PvSettings.SelectedPage == PgSettingDelete)) PvSettings.SelectedPage = PgSettingEmpty;

      TvFolders.HideSelection = false;
    }

    private void EventGridSelectionChanged(object sender, EventArgs e)
    {
      CurrentIdSetting = VxGridSettings.GetIdSetting();
      if (VxGridSettings.Grid.HideSelection) VxGridSettings.Grid.HideSelection = false;
      CurrentSetting = VxGridSettings.GetSetting(CurrentIdSetting);
      EventSettingOneRowSelected();
    }

    private void PutValueOfCurrentSettingToInputControl()
    {
      switch ((TypeSetting)CurrentSetting.IdType)
      {
        case TypeSetting.Boolean:
          StxBoolean.Value = Manager.CvManager.CvBoolean.FromString(CurrentSetting.SettingValue).Value;
          break;
        case TypeSetting.Datetime:
          StxDatetime.Value = Manager.CvManager.CvDatetime.FromString(CurrentSetting.SettingValue).Value;
          break;
        case TypeSetting.Integer64:
          StxLongInteger.Text = Manager.CvManager.CvInt64.FromString(CurrentSetting.SettingValue).Value.ToString();
          break;
        case TypeSetting.Text:
          StxText.Text = CurrentSetting.SettingValue;
          break;
        case TypeSetting.Password:
          StxPassword.Text = string.Empty;
          break;
        case TypeSetting.File:
          StxFile.Text = string.Empty;
          break;
        case TypeSetting.Folder:
          StxFolder.Text = string.Empty;
          break;
        case TypeSetting.Font:
          StxFont.Text = CurrentSetting.SettingValue;
          break;
        case TypeSetting.Color:
          StxColor.Text = CurrentSetting.SettingValue;
          break;
        default:
          break;
      }
    }

    private async void EventButtonSettingChange(object sender, EventArgs e)
    {
      PutValueOfCurrentSettingToInputControl();

      BxSettingChange.Enabled = false;
      BxSettingSave.Enabled = true;
      BxSettingCancel.Enabled = true;
      Manager.UiControl.AllowChangeSelectedItem(false);
      TxDatabaseFile.Enabled = false;
      TxFolderDelete.Enabled = false;
      TxFolderSearch.Enabled = false;
      TxFolderRename.Enabled = false;
      TxFolderAdd.Enabled = false;
      BxSettingDown.Enabled = false;
      BxSettingUp.Enabled = false;

      if (PvEditor.Parent != PnSettingChangeTool) PvEditor.Parent = PnSettingChangeTool;
      await Task.Delay(100);
      PanelSettingsChangeSizeBySettingType((TypeSetting)CurrentSetting.IdType);
    }

    private void EventButtonSettingCancel(object sender, EventArgs e)
    {
      EventButtonSettingCancel();
      BxSettingDown.Enabled = true;
      BxSettingUp.Enabled = true;
    }

    private void EventButtonSettingCancel()
    {
      BxSettingChange.Enabled = true;
      BxSettingSave.Enabled = false;
      BxSettingCancel.Enabled = false;
      Manager.UiControl.AllowChangeSelectedItem(true);
      TxDatabaseFile.Enabled = true;
      TxFolderDelete.Enabled = true;
      TxFolderSearch.Enabled = true;
      TxFolderRename.Enabled = true;
      TxFolderAdd.Enabled = true;
      PanelSettingsChangeSizeBySettingType(TypeSetting.Unknown);
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
      const string ErrorHeader = "Error trying to add a new folder";
      RadTreeNode parent = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error(ErrorHeader);

      if (parent == null)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder where the new one is added is not specified", 400, TxFolderAdd).Create();
        return;
      }

      int IdFolder = DbSettings.GetIdFolder(parent);
      string ParentFullPath = parent.FullPath;
      bool Error = false;

      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder where the new one is added is not specified", 400, TxFolderAdd).Create();
        return;
      }

      if (parent.Level > 14)
      {
        Ms.ShortMessage(MsgType.Fail, "Hierarchical nesting too deep", 280, TxFolderAdd).Create();
        return;
      }

      string NameFolderDraft = TxFolderAdd.Text.Trim();
      string NameFolder = NameFolderDraft.RemoveSpecialCharacters();
      TxFolderAdd.Text = NameFolder;

      if (NameFolder.Length < 1)
        if (NameFolderDraft.Length < 1)
        {
          Ms.ShortMessage(MsgType.Fail, "New folder name not specified", 230, TxFolderAdd).Offset(-100 + TxFolderAdd.Size.Width, -70).Create();
          return;
        }
        else
        {
          Ms.ShortMessage(MsgType.Fail, "You have specified characters that cannot be used in the folder name", 470, TxFolderAdd).Create();
          return;
        }

      int IdNewFolder = -1;

      try
      {
        code = DbSettings.FolderInsert(IdFolder, NameFolder);
      }
      catch (Exception ex)
      {
        Error = true;
        Ms.Error(ErrorHeader, ex).Control(TxFolderAdd).Create();
        IdNewFolder = -1;
      }

      IdNewFolder = code.IdObject;

      if (code.Error)
      {
        if (Error == false) Ms.Message(ErrorHeader, code.StringValue, TxFolderAdd).Fail();
      }

      if (IdNewFolder <= 0)
      {
        if ((Error == false) && (code.Success)) Ms.Message(ErrorHeader, code.StringValue, TxFolderAdd).Fail();
      }
      else // Give out SUCCESS MESSAGE //
      {
        if (NameFolderDraft.Length == NameFolder.Length)
        {
          Ms.ShortMessage(MsgType.Debug, code.StringValue, GetMessageBoxWidth(code.StringValue), TxFolderAdd)
            .Offset(TxFolderAdd.Width, -5 * TxFolderAdd.Height).Create();
        }
        else
        {
          Ms.Message("Some characters you specify\nhave been excluded from the name", code.StringValue)
            .Control(TxFolderAdd).Offset(200, 0).Delay(7).Info();
          Ms.Message("The folder name contained forbidden characters:", NameFolderDraft).NoAlert().Warning();
          Ms.Message("The name has been corrected:", NameFolder).NoAlert().Warning();
        }

        EventRefreshDataFromDatabaseFile();

        parent = TvFolders.GetNodeByPath(ParentFullPath);
        if (parent == null)
        {
          Ms.Message(MsgType.Error, "Error!", $"Method TvFolders.GetNodeByPath(ParentFullPath) has returned [null]. ParentFullPath={ParentFullPath}", null, MsgPos.Unknown, 0).NoAlert().Create();
          Ms.ShortMessage(MsgType.Warning, $"Error! Details in the message log", 300, TxFolderAdd).NoTable().Create();
        }
        else
        {
          TvManager.TryToSelectFolderAfterCreating(parent, NameFolder);
        }
      }
      TxFolderAdd.Clear();
    }

    private void EventButtonRenameFolder(object sender, EventArgs e)
    {
      const string ErrorHeader = "Failed to rename folder";
      RadTreeNode node = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error(ErrorHeader);

      if (node == null)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder you want to rename is not specified", 380, TxFolderRename).Create();
        return;
      }

      int IdFolder = DbSettings.GetIdFolder(node);
      string NodeFullPath = node.FullPath;

      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder you want to rename is not specified", 350, TxFolderRename).Create();
        return;
      }

      string NameFolder = TxFolderRename.Text.RemoveSpecialCharacters();
      TxFolderRename.Text = NameFolder;

      if (NameFolder.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "No new folder name specified", 340, TxFolderRename).Create();
        return;
      }

      if (node.Text == NameFolder)
      {
        Ms.ShortMessage(MsgType.Fail, "The new name is no different from the previous one", 400, TxFolderRename).Create();
        return;
      }

      try
      {
        code = DbSettings.FolderRename(IdFolder, NameFolder);
      }
      catch (Exception ex)
      {
        Ms.Error(ErrorHeader, ex).Control(TxFolderRename).Create();
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
        Ms.Message(ErrorHeader, code.StringValue, TxFolderRename).Fail();
      }
    }

    private void EventButtonDeleteFolder(object sender, EventArgs e)
    {
      const string ErrorHeader = "Failed to delete folder";
      RadTreeNode node = TvFolders.SelectedNode;
      ReturnCode code = ReturnCodeFactory.Error(ErrorHeader);

      if (node == null)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder you want to delete is not specified", 350, TxFolderDelete).Create();
        return;
      }

      if (node.Level == 0)
      {
        Ms.ShortMessage(MsgType.Warning, "It is not allowed to delete a root folder", 350, TxFolderDelete).Create();
        return;
      }

      RadTreeNode parent = node.Parent;
      if (parent == null)
      {
        Ms.ShortMessage(MsgType.Fail, "The folder containing the folder to be deleted was not found", 450, TxFolderDelete).Create();
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
        Ms.Error(ErrorHeader, ex).Control(TxFolderDelete).Create();
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
        Ms.Message(ErrorHeader, code.StringValue, TxFolderDelete).Fail();
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

      Point offset = new Point(PvFolders.Width, -1 * PvFolders.Height);

      if (NameFolder.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "No characters to search", 250, PvFolders).Offset(offset).NoTable().Create(); return;
      }

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.StartWith)
        SearchResult = TvFolders.FindNodes(x => x.Name.StartsWith(NameFolder));

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.Contains)
        SearchResult = TvFolders.FindNodes(x => x.Name.Contains(NameFolder));

      if (Program.ApplicationSettings.FolderNameSearchMode == TextSearchMode.WholeWord)
        SearchResult = TvFolders.FindNodes(x => x.Name == NameFolder);

      SearchIterator = 0;
      if (SearchResult.Length > 0) SelectOneNode(SearchResult[0]);

      BxFolderSearchGotoNext.Visibility = ElementVisibility.Collapsed;

      if (SearchResult.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "The search has not given any results", 300, PvFolders).Offset(offset).NoTable().Create();
      }

      if (SearchResult.Length == 1)
      {
        Ms.ShortMessage(MsgType.Debug, "One folder has been found", 220, PvFolders).Offset(offset).NoTable().Create();
      }

      if (SearchResult.Length > 1)
      {
        Ms.ShortMessage(MsgType.Info, $"Number of folders found: {SearchResult.Length}", 220, PvFolders).Offset(offset).NoTable().Create();
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
          Ms.ShortMessage(MsgType.Ok, "Search completed", 200, TxFolderSearch).Offset(new Point(TxFolderSearch.Width, 0)).NoTable().Create();
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
      if (DialogOpenFile.ShowDialog() == DialogResult.OK) SetDatabaseFile(DialogOpenFile.FileName);
    }

    private async Task EventButtonLoadData(object sender, EventArgs e)
    {
      BxOpenFile.Enabled = false; BxOpenFile.Visibility = ElementVisibility.Collapsed;
      EventLoadDataFromDatabaseFile(true);
      await Task.Delay(3000);
      BxOpenFile.Enabled = true; BxOpenFile.Visibility = ElementVisibility.Visible;
    }

    private void EventRefreshDataFromDatabaseFile() => EventLoadDataFromDatabaseFile(false);

    private void EventLoadDataFromDatabaseFile(bool LoadDataFirstTimeFromThisFile)
    {
      DataTable table = null; bool Error = false;
      ReturnCode CheckDbStructure = ReturnCodeFactory.Success();

      try
      {
        CheckDbStructure = DbSettings.CheckDatabaseStructure();
        if (CheckDbStructure.Error) throw new DatabaseStructureCheckException(CheckDbStructure.StringValue);
        table = DbSettings.GetTableFolders();
      }
      catch (DatabaseStructureCheckException ex)
      {
        Error = true;
        Ms.Message("Checking database structure", ex.Message).Control(TxDatabaseFile).Error();
      }
      catch (Exception ex)
      {
        Error = true;
        if (ex.Message.Contains("not a database"))
        {
          Ms.Message("Failed to read data from the file", "This file is not a database of the specified type").Control(TxDatabaseFile).Error();
        }
        else
        {
          Ms.Error("Failed to read data from the file you specified", ex).Control(TxDatabaseFile).Create();
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
          Ms.Error("An error occurred while trying to read the settings structure from the file", ex).Control(TxDatabaseFile).Create();
        }

      if (Error == false)
      {
        Program.ApplicationSettings.SettingsDatabaseLocation = TxDatabaseFile.Text;
        if (LoadDataFirstTimeFromThisFile) EventLoadDataFromFileFirstTime();
      }
    }

    private void EventLoadDataFromFileFirstTime()
    {
      DbSettings.InitVariables();
      DbSettings.FillDropDownListForTableTypes(DxTypes);
      Ms.ShortMessage(MsgType.Debug, "Data has been loaded from file", 220, TxDatabaseFile).Offset(TxDatabaseFile.Width + 30, -2 * TxDatabaseFile.Height).Create();
    }

    private async Task EventButtonSettingAddNew(object sender, EventArgs e)
    {
      await EventSettingSave(true);
    }

    private async Task EventButtonSettingUpdateExisting(object sender, EventArgs e)
    {
      await EventSettingSave(false);
      EventButtonSettingCancel();
      if (VxGridSettings.Grid.SelectedRows.Count < 1) BxSettingChange.Enabled = false;
    }

    private async Task EventSettingSave(bool AddNewSetting)
    {
      string ErrorHeader = AddNewSetting ? "Failed to create a new setting" : "Failed to change setting value";
      ReturnCode code = ReturnCodeFactory.Error(ErrorHeader);
      string IdSetting = AddNewSetting ? TxSettingAdd.Text.RemoveSpecialCharacters() : CurrentIdSetting;
      TxSettingAdd.Text = AddNewSetting ? IdSetting : string.Empty;

      if (IdSetting.Length < 1)
      {
        Ms.Message("Error", "Setting name not specified").Control(DxTypes).Warning(); return;
      }

      TypeSetting type = AddNewSetting ? GetTypeFromDropDownList() : (TypeSetting)CurrentSetting.IdType;

      switch (type)
      {
        case TypeSetting.Unknown:
          PvEditor.SelectedPage = PgEmpty;
          code = ReturnCodeFactory.Error("Setting type not specified");
          break;
        case TypeSetting.Boolean:
          PvEditor.SelectedPage = PgBoolean;
          code = DbSettings.SaveSettingBoolean(AddNewSetting, CurrentIdFolder, IdSetting, StxBoolean.Value);
          break;
        case TypeSetting.Datetime:
          PvEditor.SelectedPage = PgDatetime;
          code = DbSettings.SaveSettingDatetime(AddNewSetting, CurrentIdFolder, IdSetting, StxDatetime.Value);
          break;
        case TypeSetting.Integer64:
          PvEditor.SelectedPage = PgInteger;
          StxLongInteger.Text = StxLongInteger.Text.Trim();
          if (Manager.CvManager.CvInt64.IsValid(StxLongInteger.Text) == false)
          {
            Ms.Message("Error", "Value is not an integer").Control(PnUpper).Offset(TvFolders.Width, -100).Warning(); return;
          }
          code = DbSettings.SaveSettingLong(AddNewSetting, CurrentIdFolder, IdSetting, Manager.CvManager.CvInt64.FromString(StxLongInteger.Text).Value);
          break;
        case TypeSetting.Text:
          PvEditor.SelectedPage = PgText;
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.Text, StxText.Text);
          break;
        case TypeSetting.Password:
          PvEditor.SelectedPage = PgPassword;
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.Password, StxPassword.Text);
          break;
        case TypeSetting.File:
          PvEditor.SelectedPage = PgFile;
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.File, StxFile.Text);
          break;
        case TypeSetting.Folder:
          PvEditor.SelectedPage = PgFolder;
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.Folder, StxFolder.Text);
          break;
        case TypeSetting.Font:
          PvEditor.SelectedPage = PgFont;
          if (StxFont.Text.Length < 1)
          {
            Ms.Message("Error", "You did not select a font").Control(DxTypes).Warning(); return;
          }
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.Font, StxFont.Text);
          break;
        case TypeSetting.Color:
          PvEditor.SelectedPage = PgColor;
          if (StxColor.Text.Length < 1)
          {
            Ms.Message("Error", "You did not select a color").Control(DxTypes).Warning(); return;
          }
          code = DbSettings.SaveSettingText(AddNewSetting, CurrentIdFolder, IdSetting, TypeSetting.Color, StxColor.Text);
          break;
        default:
          PvEditor.SelectedPage = PgEmpty;
          break;
      }

      Manager.UiControl.AllowChangeSelectedItem(true);

      if (code.Success)
      {
        TxSettingAdd.Clear();
        await RefreshGridSettingsAndClearSelection(); // Setting: INSERT or UPDATE //
      }
      else
      {
        Ms.Message("Error", code.StringValue).Control(DxTypes).Offset(30, -100).Warning();
      }
      ShowNotification(code.Success, code.StringValue);
    }

    private void PanelSettingsChangeSizeBySettingType(TypeSetting type)
    {
      int height = HeightExpanded;
      switch (type)
      {
        case TypeSetting.Boolean:
          PvEditor.SelectedPage = PgBoolean;
          break;
        case TypeSetting.Datetime:
          PvEditor.SelectedPage = PgDatetime;
          break;
        case TypeSetting.Integer64:
          PvEditor.SelectedPage = PgInteger;
          break;
        case TypeSetting.Text:
          PvEditor.SelectedPage = PgText;
          height = HeightForLongText;
          break;
        case TypeSetting.Password:
          PvEditor.SelectedPage = PgPassword;
          break;
        case TypeSetting.File:
          PvEditor.SelectedPage = PgFile;
          break;
        case TypeSetting.Folder:
          PvEditor.SelectedPage = PgFolder;
          break;
        case TypeSetting.Font:
          PvEditor.SelectedPage = PgFont;
          break;
        case TypeSetting.Color:
          PvEditor.SelectedPage = PgColor;
          break;
        default:
          PvEditor.SelectedPage = PgEmpty;
          height = HeightCollapsed;
          break;
      }
      PvSettings.Height = height;
    }

    private void EventSettingTypeChanged(object sender, EventArgs e)
    {
      PanelSettingsChangeSizeBySettingType(GetTypeFromDropDownList());
    }

    private async Task SettingChangeRank(bool GotoUp)
    {
      if (GotoUp) { BxSettingUp.Enabled = false; } else { BxSettingDown.Enabled = false; }
      string IdSetting = CurrentIdSetting;
      Setting sibling = GotoUp ? VxGridSettings.UpperSibling(CurrentSetting) : VxGridSettings.LowerSibling(CurrentSetting);
      if (sibling != null)
      {
        ReturnCode code = DbSettings.SwapRank(CurrentIdFolder, CurrentSetting, sibling);
        await this.RefreshGridSettingsAndClearSelection(); // Setting: rank changed //
        PnUpper.Select();
      }
    }

    private async Task EventButtonSettingDown(object sender, EventArgs e) => await SettingChangeRank(false);

    private async Task EventButtonSettingUp(object sender, EventArgs e) => await SettingChangeRank(true);

    private void EventButtonNewFileName(object sender, EventArgs e)
    {
      RadSaveFileDialog DxNewFile = new RadSaveFileDialog();
      DxNewFile.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
      DxNewFile.FileName = Program.ApplicationSettings.NewFileName;
      DialogResult result = DxNewFile.ShowDialog();
      if (result == DialogResult.OK)
      {
        TxCreateDatabase.Text = DxNewFile.FileName;
      }
    }

    private void EventButtonCreateNewDatabase(object sender, EventArgs e)
    {
      ReturnCode code = DbSettings.CreateNewDatabase(TxCreateDatabase.Text);
      if (code.Success)
      {
        PvFolders.SelectedPage = PgDatabase;
        SetDatabaseFile(TxCreateDatabase.Text);
        TxCreateDatabase.Clear();
        Ms.Message("New database created", "Click the button on the right to open a new database").Control(TxDatabaseFile).Ok();
      }
      else
      {
        Ms.Message("Failed to create a new database", code.StringValue + " " + code.StringNote).Control(TxDatabaseFile).Error();
      }
    }

    public void EventEndWork()
    {
      Program.ApplicationSettings.TreeViewSize = PnTreeview.SizeInfo.AbsoluteSize;
    }
  }
}

