using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TJStandard;
using Telerik.WinControls.UI;
using static TestNetwork.Program;

namespace TestNetwork
{
  public class GridSettings : GridManager
  {
    internal FormTreeView FxTreeView { get; set; } = null;

    internal GridSettings(FormTreeView form) => FxTreeView = form;

    internal BindingList<Setting> Empty { get; } = new BindingList<Setting>();

    internal BindingList<Setting> ListDataSource { get; set; } = new BindingList<Setting>();

    internal GridViewRowInfo ChangedRow { get; private set; } = null;

    internal string CvAction { get; set; } = string.Empty;

    internal string CvIdFolder { get; set; } = string.Empty;

    internal string CvIdSetting { get; set; } = string.Empty;

    internal bool BxEventCellValueChanged { get; set; } = true;

    internal string[] Password { get; } = { string.Empty, "* * *" };

    public override void SetDataViewControlProperties()
    {
      CreateColumns();
      Grid.DataSource = ListDataSource;
      Grid.ReadOnly = false;
      Grid.AllowEditRow = false;
      Grid.AllowRowResize = false;
      Grid.MultiSelect = false;
      Grid.AllowSearchRow = false;
      Grid.EnableFiltering = false;
      Grid.EnableGrouping = false;
      Grid.EnableSorting = false;
      Grid.HideSelection = true;

      //Grid.CurrentCellChanged += GridCurrentCellChanged;
      Grid.CurrentColumnChanged += GridCurrentColumnChanged;
      Grid.SelectionMode = GridViewSelectionMode.FullRowSelect;
      Grid.RowFormatting += new RowFormattingEventHandler(EventRowFormatting);
      Grid.CurrentRowChanging += new CurrentRowChangingEventHandler(EventCurrentRowChanging);
      //Grid.CellFormatting += new CellFormattingEventHandler(EventCellFormatting);
      //Grid.CellValueChanged += EventCellValueChanged;
      //SetThemeForGrid();
    }

    private void EventCurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
    {
      e.Cancel = !Manager.UiControl.FlagAllowChangeSelectedItem;
    }

    private void GridCurrentColumnChanged(object sender, CurrentColumnChangedEventArgs e)
    {
      try { if (e.NewColumn.Index > 1) Grid.CurrentColumn = Grid.Columns[1]; } catch { }
    }

    private void EventCellValueChanged(object sender, GridViewCellEventArgs e)
    {
      if (BxEventCellValueChanged == false) return;
      ChangedRow = Grid.Rows[e.RowIndex];
      CvAction = ChangedRow.Cells[e.ColumnIndex].Value.ToString();
      CvIdFolder = ChangedRow.Cells[Standard.GetGridColumnName(nameof(Setting.IdFolder))].Value.ToString();
      CvIdSetting = ChangedRow.Cells[Standard.GetGridColumnName(nameof(Setting.IdSetting))].Value.ToString();
    }

    internal void CreateColumns()
    {
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdFolder), "IdFolder hidden", true, typeof(int));
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdSetting), "setting", true, typeof(string), 200);
      //----------------------------------------------------------------------------------------------------------------------------------------    
      var CnSettingIdType = AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdType), "IdType hidden", true, typeof(int));
      //----------------------------------------------------------------------------------------------------------------------------------------    
      var CnSettingTypeName = AddColumn<GridViewTextBoxColumn>(nameof(Setting.NameType), "type", true, typeof(string), 90);
      //----------------------------------------------------------------------------------------------------------------------------------------
      var CnSettingValue = AddColumn<GridViewTextBoxColumn>(nameof(Setting.SettingValue), "value", true, typeof(string), 400);
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.Rank), "rank hidden", true, typeof(int));
      //----------------------------------------------------------------------------------------------------------------------------------------
      var CnBooleanValue = AddColumn<GridViewTextBoxColumn>(nameof(Setting.BooleanValue), "boolean value hidden", true, typeof(string));
      //----------------------------------------------------------------------------------------------------------------------------------------

      ExpressionFormattingObject obj = new ExpressionFormattingObject("Boolean_Value_False", $"{CnBooleanValue.Name} = '0'", false);
      obj.CellBackColor = Color.FromArgb(255, 229, 229);
      CnSettingValue.ConditionalFormattingObjectList.Add(obj);

      obj = new ExpressionFormattingObject("Boolean_Value_True", $"{CnBooleanValue.Name} = '1'", false);
      obj.CellBackColor = Color.FromArgb(209, 255, 209); ;
      CnSettingValue.ConditionalFormattingObjectList.Add(obj);

      obj = new ExpressionFormattingObject("Integer", $"{CnSettingIdType.Name} = {(int)(TypeSetting.Integer64)}", false);
      obj.CellForeColor = Color.Blue;
      CnSettingValue.ConditionalFormattingObjectList.Add(obj);

      obj = new ExpressionFormattingObject("TypeName", "0 = 0", false);
      obj.CellForeColor = Color.DarkGray;
      CnSettingTypeName.ConditionalFormattingObjectList.Add(obj);

      //AddSorting(nameof(Setting.Rank));
    }

    internal void ResetView()
    {
      RefreshGrid(Empty);
    }

    private void RefreshGrid()
    {
      Grid.DataSource = ListDataSource;
      if (Grid.Rows.Count > 0)
      {
        Grid.GridNavigator.ClearSelection(); // Clear selection //
      }
    }

    internal void HidePasswordValues()
    {
      for (int i = 0; i < ListDataSource.Count; i++)
        if (ListDataSource[i].IdType == (int)TypeSetting.Password)
          ListDataSource[i].SettingValue = Password[Math.Sign(ListDataSource[i].SettingValue.Length)];
    }

    internal void RefreshGrid(BindingList<Setting> list)
    {
      Grid.DataSource = null;
      if ((list != null) && (list.Count > 0))
      {
        ListDataSource = list;
        HidePasswordValues();
      }
      else
      {
        ListDataSource = Empty;
      }
      RefreshGrid();
    }

    private void RefreshGrid(List<Setting> list)
    {
      if ((list != null) && (list.Count > 0))
      {
        ListDataSource = new BindingList<Setting>(list);
        HidePasswordValues();
      }
      else
      {
        ListDataSource = Empty;
      }
      RefreshGrid();
    }

    internal string GetIdSetting() => this.GetStringValue(nameof(Setting.IdSetting));

    internal Setting GetSetting(string IdSetting)
    {
      if (ListDataSource != null)
        foreach (var item in ListDataSource)
          if (item.IdSetting == IdSetting) return item;
      return null;
    }

    internal int GetRank(Setting setting) => setting == null ? -1 : setting.Rank;

    internal Setting UpperSibling(Setting setting)
    {
      Setting found = null;
      foreach (var item in ListDataSource)
        if (item.Rank < setting.Rank)
          if (item.Rank > GetRank(found))
            found = item;
      return found;
    }

    internal Setting LowerSibling(Setting setting)
    {
      Setting found = null;
      foreach (var item in ListDataSource)
        if (item.Rank > setting.Rank)
          if (item.Rank < (GetRank(found) < 0 ? int.MaxValue : GetRank(found)))
            found = item;
      return found;
    }

    internal void SwapRank(Setting SelectedSetting, Setting Sibling)
    {
      if ((SelectedSetting == null) || (Sibling == null)) return;

    }

    internal void SelectRow(string IdSetting)
    {
      string columnName = Standard.GetGridColumnName(nameof(Setting.IdSetting));
      for (int i = 0; i < Grid.Rows.Count; i++)
      {
        if (Grid.Rows[i].Cells[columnName].Value.ToString() == IdSetting)
          if (!Grid.Rows[i].IsSelected)
          {
            Grid.Rows[i].IsSelected = true;
            Grid.Rows[i].IsCurrent = true;
            break;
          }
      }
    }
  }
}

