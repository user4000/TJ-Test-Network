using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using TJStandard;

namespace TJSettings
{
  public partial class LocalDatabaseOfSettings
  {
    public string GetRootFolderName() => GetScalarString(DbManager.SqlGetRootFolderName);

    public bool FolderNotFound(int IdFolder) => IdFolder < 0;

    public ReturnCode FolderNotFound() => ReturnCodeFactory.Error((int)Errors.FolderNotFound, "Folder not found");

    public ReturnCode FolderInsert(string Parent, string NameFolder) => FolderInsert(GetIdFolder(Parent), NameFolder);

    public ReturnCode FolderInsert(int IdParent, string NameFolder)
    {
      if (FolderNotFound(IdParent)) return FolderNotFound();
      ReturnCode code = ReturnCodeFactory.Success($"New folder has been created: {NameFolder}");
      int IdNewFolder = -1;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdParent).ZzAdd("@IdParent", IdParent).ZzAdd("@NameFolder", NameFolder);
        int count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderAlreadyExists, "A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderInsert);
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to add a new folder");
        IdNewFolder = command.ZzGetScalarInteger(DbManager.SqlGetIdFolder);
        if (IdNewFolder > 0)
        {
          code.IdObject = IdNewFolder;
        }
        else
        {
          return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to add a new folder");
        }
      }
      return code;
    }

    public ReturnCode FolderRename(string FolderPath, string NameFolder) => FolderRename(GetIdFolder(FolderPath), NameFolder);

    public ReturnCode FolderRename(int IdFolder, string NameFolder)
    {
      if (FolderNotFound(IdFolder)) return FolderNotFound();
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been renamed: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountByIdFolder).ZzAdd("@IdFolder", IdFolder).ZzAdd("@NameFolder", NameFolder);
        count = command.ZzGetScalarInteger();
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderAlreadyExists, "A folder with the same name already exists");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderRename);
        if (count < 1) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to rename a folder");
      }
      if (IdFolder == DbManager.IdFolderRoot) RootFolderName = GetRootFolderName();
      return code;
    }

    public ReturnCode FolderDelete(string FolderPath, string NameFolder) => FolderDelete(GetIdFolder(FolderPath), NameFolder);

    public ReturnCode FolderDelete(int IdFolder, string NameFolder)
    {
      if (FolderNotFound(IdFolder)) return FolderNotFound();
      ReturnCode code = ReturnCodeFactory.Success($"Folder has been deleted: {NameFolder}");
      int count = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection))
      {
        command.ZzOpenConnection().ZzText(DbManager.SqlFolderCountSimple).ZzAdd("@IdFolder", IdFolder);
        count = command.ZzGetScalarInteger();
        if (count != 1) return ReturnCodeFactory.Error((int)Errors.FolderNotFound, "The folder you specified was not found");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildFolder);
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderHasChildFolder, "You cannot delete a folder that has other folders inside");
        count = command.ZzGetScalarInteger(DbManager.SqlCountChildSettings);
        if (count != 0) return ReturnCodeFactory.Error((int)Errors.FolderHasSettings, "You cannot delete a folder that contains settings");
        count = command.ZzExecuteNonQuery(DbManager.SqlFolderDelete);
        if (count == 0) return ReturnCodeFactory.Error((int)Errors.Unknown, "Error trying to delete a folder");
      }
      return code;
    }

    public ReturnCode FolderForceDeleteUsingTreeview(string FolderPath) 
    {
      //Trace.WriteLine($"FolderForceDelete(string FolderPath)  ---> {FolderPath}");
      // This method uses RadTreeView class to perform search operations //
      ReturnCode code = ReturnCodeFactory.Error();

      int IdFolder = GetIdFolder(FolderPath);

      if (FolderNotFound(IdFolder)) return FolderNotFound();

      //Trace.WriteLine($"FolderForceDelete(string FolderPath)  ---> Point 1 --- IdFolder = {IdFolder}");

      bool FlagFound = false;

      void SearchNode(RadTreeNode node)
      {
        if (FlagFound) return;
        if (GetIdFolder(node) == IdFolder)
        {
          FlagFound = true;
          //Trace.WriteLine($"Found !!! ---> {node.FullPath}");
          LocalMethodDeleteAllSettingsOfOneFolder(node);
          LocalMethodDeleteFolderAndAllChildFolders(node);
        }
        if (!FlagFound) foreach (var item in node.Nodes) SearchNode(item);
      }

      void LocalMethodDeleteAllSettingsOfOneFolder(RadTreeNode node)
      {
        DeleteAllSettingsOfOneFolder(node.FullPath);
        //Trace.WriteLine($"Delete settings of folder ======= {node.FullPath}");
        foreach (var item in node.Nodes) LocalMethodDeleteAllSettingsOfOneFolder(item);
      }

      void LocalMethodDeleteFolderAndAllChildFolders(RadTreeNode node)
      {
        foreach (var item in node.Nodes) LocalMethodDeleteFolderAndAllChildFolders(item);
        ReturnCode result = FolderDelete(node.FullPath, string.Empty);
        if (IdFolder == GetIdFolder(node)) code = result;
        //Trace.WriteLine($@"Delete folder /\/\/\/\/\/\/\ === {node.FullPath}");
      }

      DataTable table = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, table);

      foreach (var item in form.TvFolders.Nodes) SearchNode(item);

      form.TvFolders.DataSource = null;
      table.Clear();
      form.TvFolders.Dispose();
      table.Dispose();
      form.Close();

      //Trace.WriteLine($@" >>>>>>>>>>>>>>>>>>>>> {ReturnCodeFormatter.ToString(code)}");

      return code;
    }

    public ReturnCode FolderForceDelete(string FolderPath) 
    {
      ReturnCode code = ReturnCodeFactory.Error();
      int IdFolder = GetIdFolder(FolderPath);
      if (FolderNotFound(IdFolder)) return FolderNotFound();
      List<int> list = GetListOfIdFolders(IdFolder);

      void ProcessOneFolder(SQLiteCommand cmd, int InnerIdFolder)
      {
        List<int> InnerList = GetListOfIdFolders(cmd, InnerIdFolder);
        foreach (int item in InnerList) ProcessOneFolder(cmd, item);
        DeleteSettingsAndFolder(cmd, InnerIdFolder);
      }

      void DeleteSettingsAndFolder(SQLiteCommand cmd, int InnerIdFolder)
      {
        cmd.Parameters.Clear();
        cmd.ZzAdd("@IdFolder", InnerIdFolder).ZzExecuteNonQuery(DbManager.SqlDeleteAllSettingsOfOneFolder);
        cmd.ZzExecuteNonQuery(DbManager.SqlFolderDelete);
      }

      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection).ZzOpenConnection())
      {
        foreach (int item in list) ProcessOneFolder(command, item);
      }

      DeleteAllSettingsOfOneFolder(IdFolder);
      return FolderDelete(IdFolder, FolderPath);
    }

    public string AddRootFolderNameIfNotSpecified(string FullPath)
    {
      FullPath = FullPath.Trim();
      if (FullPath == string.Empty) return RootFolderName;
      FullPath = FullPath.TrimStart(FolderPathSeparator[0]).TrimEnd(FolderPathSeparator[0]);
      if (FullPath.StartsWith(RootFolderName) == false) FullPath = RootFolderName + FolderPathSeparator + FullPath;
      return FullPath;
    }

    public int GetIdFolder(RadTreeNode node)
    {
      int Id = IdFolderNotFound;
      if (node != null)
        try
        {
          DataRowView row = node.DataBoundItem as DataRowView;
          Id = CxConvert.ToInt32(row.Row[DbManager.CnFoldersIdFolder].ToString(), IdFolderNotFound);
        }
        catch { }
      return Id;
    }

    /// <summary>
    /// Find IdFolder by Materialized Full Path (Root Folder Name may be omitted).
    /// </summary>
    public int GetIdFolder(string FullPath) // Find IdFolder by Materialized Full Path - Root Folder Name may be omitted //
    {
      FullPath = AddRootFolderNameIfNotSpecified(FullPath);
      string[] names = FullPath.Split(FolderPathSeparator[0]);
      int IdFolder = 0;
      string sql = string.Empty;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(connection).ZzText(DbManager.SqlGetIdFolder).ZzOpenConnection() )
      {
        for (int i = 0; i < names.Length; i++)
        {
          IdFolder = command.ZzAdd("@IdParent", IdFolder).ZzAdd("@NameFolder", names[i]).ZzGetScalarInteger();
          command.Parameters.Clear();
          if (IdFolder < 0) break;
        }        
      }
      return IdFolder;
    }

    private int GetIdFolderUsingTreeview(string FullPath)
    {
      int x = IdFolderNotFound;

      int ProcessOneNode(RadTreeNode node)
      {
        if (node.FullPath == FullPath) return GetIdFolder(node);
        foreach (var item in node.Nodes)
        {
          x = ProcessOneNode(item);
          if (x != IdFolderNotFound) { return x; }
        }
        return -1;
      }

      DataTable table = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, table);

      int IdFolder = IdFolderNotFound;

      foreach (var item in form.TvFolders.Nodes)
      {
        IdFolder = ProcessOneNode(item);
        if (IdFolder != IdFolderNotFound) break;
      }

      form.TvFolders.DataSource = null;
      table.Clear();
      form.TvFolders.Dispose();
      table.Dispose();
      form.Close();
      return IdFolder;
    }

    /// <summary>
    /// Get all folders of the database.
    /// </summary>
    public List<Folder> GetListOfFolders()
    {
      List<Folder> list = new List<Folder>();
      void ProcessOneNode(RadTreeNode node)
      {
        list.Add(Folder.Create(GetIdFolder(node), node.Text, node.FullPath, node.Level));
        foreach (var item in node.Nodes) ProcessOneNode(item);
      }
      DataTable table = GetTableFolders();
      FxTreeView form = new FxTreeView();
      form.Visible = false;
      FillTreeView(form.TvFolders, table);
      foreach (var item in form.TvFolders.Nodes) ProcessOneNode(item);
      form.TvFolders.DataSource = null;
      table.Clear();
      form.TvFolders.Dispose();
      table.Dispose();
      form.Close();
      return list;
    }

    /// <summary>
    /// Get names of all direct child folders of the specified folder.
    /// </summary>
    public List<string> GetListOfFolders(string ParentFolderPath)
    {
      List<string> list = new List<string>();
      DataTable table = new DataTable();
      int IdFolder = GetIdFolder(ParentFolderPath);
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlFolderGetChildren, connection).ZzAdd("@IdFolder", IdFolder))
      using (SQLiteDataReader reader = command.ZzOpenConnection().ExecuteReader())
      {
        table.Load(reader);
      }
      if (table.Rows.Count > 0) list = (from DataRow row in table.Rows select row[0].ToString()).ToList();
      table.Clear();
      return list;
    }

    public List<int> GetListOfIdFolders(int IdFolder)
    {
      List<int> list = new List<int>();
      int x = 0;
      using (SQLiteConnection connection = GetSqliteConnection())
      using (SQLiteCommand command = new SQLiteCommand(DbManager.SqlFolderGetIdChildren, connection).ZzAdd("@IdFolder", IdFolder))
      using (SQLiteDataReader reader = command.ZzOpenConnection().ExecuteReader())    
        while (reader.Read())
        {
          x = reader.GetInt32(0);
          if (x > 0) list.Add(x);
        }      
      return list;
    }

    public List<int> GetListOfIdFolders(SQLiteCommand command, int IdFolder)
    {
      List<int> list = new List<int>();
      int x = 0;
      command.Parameters.Clear();
      using (SQLiteDataReader reader = command.ZzText(DbManager.SqlFolderGetIdChildren).ZzAdd("@IdFolder", IdFolder).ExecuteReader())
        while (reader.Read())
        {
          x = reader.GetInt32(0);
          if (x > 0) list.Add(x);
        }
      return list;
    }
  }
}

