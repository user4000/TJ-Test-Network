using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ProjectStandard;
using Telerik.WinControls.UI;

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
      Grid.SelectionMode = GridViewSelectionMode.CellSelect;
      //Grid.RowFormatting += new RowFormattingEventHandler(EventRowFormatting);
      Grid.CellFormatting += new CellFormattingEventHandler(EventCellFormatting);
      Grid.CellValueChanged += EventCellValueChanged;
    }

    private void EventCellValueChanged(object sender, GridViewCellEventArgs e)
    {
      if (BxEventCellValueChanged == false) return;
      ChangedRow = Grid.Rows[e.RowIndex];
      CvAction = ChangedRow.Cells[e.ColumnIndex].Value.ToString();
      CvIdFolder = ChangedRow.Cells[Standard.GetGridColumnName(nameof(Setting.IdFolder))].Value.ToString();
      CvIdSetting = ChangedRow.Cells[Standard.GetGridColumnName(nameof(Setting.IdSetting))].Value.ToString();
      //Ms.Message(MsgType.Debug, CvAction, CvIdResult, null, MsgPos.TopCenter);
      //ChangeList();
    }

    internal void CreateColumns()
    {
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdFolder), "IdFolder", true, typeof(int), -1);
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdSetting), "Имя переменной", true, typeof(string), 200).ZzPin();
      //----------------------------------------------------------------------------------------------------------------------------------------    
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.IdType), "IdType", true, typeof(int), -1);
      //----------------------------------------------------------------------------------------------------------------------------------------    
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.NameType), "Тип", true, typeof(string), 100);
      //----------------------------------------------------------------------------------------------------------------------------------------
      //GridViewComboBoxColumn ColumnAction = AddColumn<GridViewComboBoxColumn>(ListAction, nameof(TTSearchClientResult.IdAction), "Действие", false, typeof(int), 200);
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.SettingValue), "Значение переменной", true, typeof(string), 300);
      //----------------------------------------------------------------------------------------------------------------------------------------
      AddColumn<GridViewTextBoxColumn>(nameof(Setting.Rank), "Порядковый номер", true, typeof(int), 100);
      //----------------------------------------------------------------------------------------------------------------------------------------
      GridViewTextBoxColumn ColumnBooleanValue = AddColumn<GridViewTextBoxColumn>(nameof(Setting.BooleanValue), "hidden", true, typeof(string), -1) ;
      //----------------------------------------------------------------------------------------------------------------------------------------

      /*ConditionalFormattingObject format = new ConditionalFormattingObject("Boolean_Value_False", ConditionTypes.Equal, "0", "", true);
      format.RowBackColor = Color.LightPink;
      format.RowForeColor = Color.Blue;
      ColumnBooleanValue.ConditionalFormattingObjectList.Add(format);

      format = new ConditionalFormattingObject("Boolean_Value_True", ConditionTypes.Equal, "1", "", true);
      format.RowBackColor = Color.LightGreen;
      format.RowForeColor = Color.Blue;
      ColumnBooleanValue.ConditionalFormattingObjectList.Add(format);*/

      AddSorting(nameof(Setting.Rank));
    }

    internal void ResetView()
    {
      RefreshGrid(Empty);
    }

    internal void RefreshGrid() => Grid.DataSource = ListDataSource;

    internal void RefreshGrid(BindingList<Setting> list)
    {
      if ((list != null) && (list.Count > 0))
        ListDataSource = list;
      else
        ListDataSource = Empty;
      RefreshGrid();
      //Grid.ClearSelection();
    }

    internal void RefreshGrid(List<Setting> list)
    {
      if ((list != null) && (list.Count > 0))
        ListDataSource = new BindingList<Setting>(list);
      else
        ListDataSource = Empty;
      RefreshGrid();
    }
  }
}

