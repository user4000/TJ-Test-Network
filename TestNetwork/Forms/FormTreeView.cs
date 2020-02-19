using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectStandard;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TJFramework;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork
  {
    private LocalDatabaseOfSettings DbSettings { get; } = new LocalDatabaseOfSettings();

    private DataTable TableFolders { get; set; } = null;

    private string NameOfSelectedNode { get; set; } = string.Empty;

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
    }

    private void SetProperties()
    {
      BxOpenFile.ShowBorder = false;
      BxSelectFile.ShowBorder = false;
      BxAddFolder.ShowBorder = false;
      BxRenameFolder.ShowBorder = false;
      BxDeleteFolder.ShowBorder = false;
      TvFolders.ImageList = this.ImageListFolders;
      DbSettings.SetFontOfNode(TvFolders.Font);
      TxDatabaseFile.ReadOnly = true;
      TxFolderDelete.ReadOnly = true;
      SetDatabaseFile(Program.ApplicationSettings.SettingsDatabaseLocation);
    }

    private void SetEvents()
    {
      BxOpenFile.Click += EventButtonLoadData;
      BxSelectFile.Click += EventButtonChooseFile;
      BxAddFolder.Click += EventButtonAddFolder;
      BxRenameFolder.Click += EventButtonRenameFolder;
      TvFolders.SelectedNodeChanged += EventTreeviewSelectedNodeChanged;
    }

    public void EventStartWork()
    {
      SetProperties(); SetEvents();
    }

    private void EventTreeviewSelectedNodeChanged(object sender, RadTreeViewEventArgs e)
    {
      try { NameOfSelectedNode = e.Node.Text; } catch { NameOfSelectedNode = string.Empty; }
      TxFolderRename.Text = NameOfSelectedNode;
      TxFolderDelete.Text = NameOfSelectedNode;
    }

    private void EventButtonAddFolder(object sender, EventArgs e)
    {
      RadTreeNode parent = TvFolders.SelectedNode;

      if (parent == null) return;

      int IdFolder = DbSettings.GetIdFolder(parent);
      string ParentFullPath = parent.FullPath;
      bool Error = false;

      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указана папка, в которую добавляется новая.", 380, TxFolderName).Create();
        return;
      }

      if (TxFolderName.Text.Trim().Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указано название новой папки.", 350, TxFolderName).Create();
        return;
      }

      string NameFolder = /* TxFolderName.Text.Trim().Length < 1 ? "Folder" : */ TxFolderName.Text.Trim();

      if (IdFolder >= 0)
      {
        int IdNewFolder = -1;
        try
        {
          IdNewFolder = DbSettings.FolderInsert(IdFolder, NameFolder);
        }
        catch (SQLiteException ex)
        {
          Error = true;
          if (ex.Message.Contains("UNIQUE"))
            Ms.Message("Не удалось добавить новую папку.", "Папка с таким именем уже существует", TxFolderName).Error();
          else
            Ms.Error("Не удалось добавить новую папку", ex).Control(TxFolderName).Create();
        }
        catch (Exception ex)
        {
          Error = true;
          Ms.Error("Не удалось добавить новую папку", ex).Control(TxFolderName).Create();
          IdNewFolder = -1;
        }

        if (IdNewFolder <= 0)
        {
          if (Error==false) Ms.Message("Не удалось добавить новую папку.", "Папка с таким именем уже существует", TxFolderName).Fail();
        }
        else
        {
          Ms.ShortMessage(MsgType.Debug, $"Папка добавлена: {NameFolder}", 250, TxFolderName).Create();
          EventButtonLoadData(sender, e);
          parent = TvFolders.GetNodeByPath(ParentFullPath);
          parent.Expanded = true;
          parent.Selected = true;
        }
      }
      TxFolderName.Clear();
    }

    private void EventButtonRenameFolder(object sender, EventArgs e)
    {
      RadTreeNode node = TvFolders.SelectedNode;

      if (node == null) return;

      int IdFolder = DbSettings.GetIdFolder(node);
      string NodeFullPath = node.FullPath;
   
      if (IdFolder < 0)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указана папка, которую нужно переименовать.", 380, TxFolderRename).Create();
        return;
      }

      string NameFolder = TxFolderRename.Text.Trim();

      bool FolderHasBeenRenamed = false;

      if (NameFolder.Length < 1)
      {
        Ms.ShortMessage(MsgType.Fail, "Ошибка! Не указано новое название папки.", 350, TxFolderRename).Create();
        return;
      }

      try
      {
        FolderHasBeenRenamed = DbSettings.FolderRename(IdFolder, NameFolder);
      }
      catch (Exception ex)
      {
        Ms.Error("Не удалось переименовать папку", ex).Control(TxFolderRename).Create();
        return;
      }

      if (FolderHasBeenRenamed)
      {
        Ms.ShortMessage(MsgType.Debug, $"Папка переименована: {NameFolder}", 250, TxFolderRename).Create();
        node.Text = NameFolder;
      }
      else
      {
        Ms.Message("Не удалось переименовать папку.", "Папка с таким именем уже существует", TxFolderRename).Fail();
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
      DataTable table = null; bool Error = false;
      try
      {
        table = DbSettings.GetSqliteDataTable(DbSettings.TableFolders);
      }
      catch (Exception ex)
      {
        Error = true;
        Ms.Error("Не удалось прочитать данные из указанного вами файла.", ex).Control(TxDatabaseFile).Create();
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
        if (sender is RadImageButtonElement)
        {
          RadImageButtonElement item = sender as RadImageButtonElement;
          if (item.Name==BxOpenFile.Name) Ms.ShortMessage(MsgType.Debug, "Данные прочитаны.", 150, TxDatabaseFile).Create();
        }        
      }
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
