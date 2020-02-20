using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Themes;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace ProjectStandard
{
  public abstract class GridManager
  {
    public static int DefaultRowCountOfDropDownList { get; } = 10;
    public static int OneRowHeightOfDropDownList { get; } = 25;
    public static int MinimumDropDownWidth { get; } = 300;
    public static int MaximumDropDownHeight { get; } = 200;

    public static VisualStudio2012LightTheme ThemeForGrid { get; } = new VisualStudio2012LightTheme();

    public static Color ColorRow { get; } = Color.WhiteSmoke;
    public HashSet<string> NonemptyColumns { get; } = new HashSet<string>(); // Список столбцов которые не допускают вставку пустых значений //

    public bool IsColumnSettingsApplied { get; set; } = false;

    public RadGridView Grid { get; set; } = null;

    public MasterGridViewTemplate MGridViewTemplate { get; set; } = null;

    public void SetThemeForGrid() => Grid.ThemeName = ThemeForGrid.ThemeName;

    public virtual void InitializeGrid(RadGridView grid, bool SetAnotherTheme = false)
    {
      Grid = grid;
      if (SetAnotherTheme) Grid.ThemeName = ThemeForGrid.ThemeName;
      MGridViewTemplate = Grid.MasterTemplate;
      SetDataViewControlProperties();
      Grid.CellEditorInitialized += EventCellEditorInitialized;

      //TODO: Раскомментируйте если не нравится цвет выделенной строки и ячейки 
      //Grid.RowFormatting += new RowFormattingEventHandler(EventRowFormatting);
      //Grid.CellFormatting += new CellFormattingEventHandler(EventCellFormatting);
    }

    public string GetStringValue(string FieldName) => Grid.ZzGetStringValueByFieldName(FieldName);

    public void MarkAsNonEmpty(GridViewDataColumn dc) => NonemptyColumns.Add(dc.FieldName);

    public bool ColumnCannotBeEmpty(string ColumnName) => NonemptyColumns.Contains(ColumnName);

    public abstract void SetDataViewControlProperties();

    public int GetSummaryVisibleColumnsWidth()
    {
      int SumWidth = 0;
      foreach (GridViewDataColumn GvDataColumn in Grid.Columns) if (GvDataColumn.IsVisible) SumWidth += GvDataColumn.Width;
      return SumWidth;
    }

    public void TryToIncreaseColumnsWidthProportionally()
    {
      int SumWidth = GetSummaryVisibleColumnsWidth();
      int Dx = Grid.Width - SumWidth - 30;
      if (Dx > 50)
        foreach (GridViewDataColumn GvDataColumn in Grid.Columns)
          if (GvDataColumn.IsVisible) GvDataColumn.Width += (int)(Dx * (1f * (GvDataColumn.Width) / (SumWidth)));
    }

    public void SetGridFont(Font font)
    {
      Font MyFont = font;
      if (MyFont.Size > 16) MyFont = new Font(font.FontFamily, 12);
      if (MyFont.Size < 8) MyFont = new Font(font.FontFamily, 10);
      Grid.Font = MyFont;
      SetRowHeight(25);
    }

    public void SetRowHeight(int height) => Grid.TableElement.RowHeight = CxConvert.ValueInRange(height, 20, 50);

    public void SetSizeOfCombobox(RadDropDownListEditorElement element)
    {
      int RowCount = DefaultRowCountOfDropDownList;

      if (element.DataSource is DataTable)
      {
        RowCount = (element.DataSource as DataTable).Rows.Count;
      }
      else
      if (element.DataSource is IList<Model.SimpleEntity>)
      {
        RowCount = (element.DataSource as IList<Model.SimpleEntity>).Count;
      }

      if (element.DropDownWidth < MinimumDropDownWidth) element.DropDownWidth = MinimumDropDownWidth;
      element.DropDownHeight = System.Math.Min(RowCount * OneRowHeightOfDropDownList, MaximumDropDownHeight);
    }

    public void LoadUserColumnsSettingsFromDictionary(Dictionary<string, int> ColumnWidth)
    {
      if (IsColumnSettingsApplied == false) Grid.ZzLoadColumnWidth(ColumnWidth);
      IsColumnSettingsApplied = true;
    }

    public void EventCellFormatting(object sender, CellFormattingEventArgs e)
    {
      /*
      if 
        ( 
        //(e.CellElement.ColumnInfo.Name == "cciiuser") &&
        ((int)e.CellElement.RowInfo.Cells["cckkadmin"].Value == 3)
        )
      {
        e.CellElement.ForeColor = Color.Blue;
      }
      else
      {
        e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
      }
      */
      //return;

      if (e.CellElement.IsCurrent)
      {
        e.CellElement.DrawFill = false;
      }
      else
      {
        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      }
    }

    public void EventRowFormatting(object sender, RowFormattingEventArgs e)
    {
      // return;
      // Здесь мы избавляемся от закраски выделения строки по умолчанию 
      if (e.RowElement.IsSelected || e.RowElement.IsCurrent)
      {
        e.RowElement.GradientStyle = GradientStyles.Solid;
        e.RowElement.BackColor = ColorRow;
      }
      else
      {
        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        e.RowElement.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
      }
    }

    public void EventCellEditorInitialized(object sender, GridViewCellEventArgs e)
    { // Этот метод нужен для установки шрифта ComboBox = DropDownListEditor - иначе не получится //
      IInputEditor editor = e.ActiveEditor;
      if (editor != null && editor is RadDropDownListEditor)
      {
        RadDropDownListEditor dropDown = (RadDropDownListEditor)editor;
        RadDropDownListEditorElement element = (RadDropDownListEditorElement)dropDown.EditorElement;
        element.Font = Grid.Font;
        element.ListElement.Font = Grid.Font;
        SetSizeOfCombobox(element);
      }
    }








    public T CreateColumn<T>(string fieldName, string headerText, bool readOnly, DataTable table = null, string valueMember = "", string displayMember = "")
      where T : GridViewDataColumn, new()
    {
      T dc = new T()
      {
        FieldName = fieldName,
        HeaderText = headerText,
        Name = Standard.GetGridColumnName(fieldName),
        ReadOnly = readOnly
      };
      if (table != null)
      {
        GridViewComboBoxColumn combobox = (dc as GridViewComboBoxColumn);
        if (combobox == null) MessageBox.Show("Ошибка! Неправильно указан тип столбца грида - должен быть GridViewComboBoxColumn.");
        combobox.DataSource = table;
        combobox.ValueMember = valueMember == "" ? nameof(Model.Classificator.IdObject) : valueMember;
        combobox.DisplayMember = displayMember == "" ? nameof(Model.Classificator.NameObject) : displayMember;
      }
      return dc;
    }


    public T CreateColumnCombobox<T>(string fieldName, string headerText, bool readOnly, BindingList<Model.SimpleEntity> list)
      where T : GridViewDataColumn, new()
    {
      T dc = new T()
      {
        FieldName = fieldName,
        HeaderText = headerText,
        Name = Standard.GetGridColumnName(fieldName),
        ReadOnly = readOnly
      };
      if (list != null)
      {
        GridViewComboBoxColumn combobox = (dc as GridViewComboBoxColumn);
        if (combobox == null) MessageBox.Show("Ошибка! Неправильно указан тип столбца грида - должен быть GridViewComboBoxColumn.");
        combobox.DataSource = list;
        combobox.ValueMember = nameof(Model.Classificator.IdObject);
        combobox.DisplayMember = nameof(Model.Classificator.NameObject);
      }
      return dc;
    }



    public void AddSorting(string fieldName, ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
      SortDescriptor GridSorting1 = new SortDescriptor();
      GridSorting1.PropertyName = Standard.GetGridColumnName(fieldName);
      GridSorting1.Direction = sortDirection;
      Grid.SortDescriptors.Add(GridSorting1);
    }

    public T AddColumn<T>(string fieldName, string headerText, bool readOnly, DataTable table = null, string valueMember = "", string displayMember = "")
      where T : GridViewDataColumn, new()
    {
      T dc = CreateColumn<T>(fieldName, headerText, readOnly, table, valueMember, displayMember);
      MGridViewTemplate.Columns.Add(dc);
      return dc;
    }


    public T AddColumn<T>(string fieldName, string headerText, bool readOnly, System.Type type, int width)
      where T : GridViewDataColumn, new()
    {
      T dc = CreateColumn<T>(fieldName, headerText, readOnly, null, string.Empty, string.Empty);
      MGridViewTemplate.Columns.Add(dc);
      dc.DataType = type;
      dc.Width = width;
      return dc;
    }

    public T AddColumn<T>(BindingList<Model.SimpleEntity> list, string fieldName, string headerText, bool readOnly, System.Type type, int width)
       where T : GridViewDataColumn, new()
    {
      T dc = CreateColumnCombobox<T>(fieldName, headerText, readOnly, list);
      MGridViewTemplate.Columns.Add(dc);
      dc.DataType = type;
      dc.Width = width;
      return dc;
    }


    public string GetColumnNameByFieldName(string fieldName)
    {
      foreach (GridViewDataColumn c in Grid.Columns) if (c.FieldName == fieldName) { return c.Name; }; return string.Empty;
    }

    public bool UpdateCurrentCell(string fieldName, string value)
    {
      bool Success = false; bool ErrorOccured = false;
      if (Grid.CurrentRow is GridViewDataRowInfo)
      {
        int selectedIndex = Grid.Rows.IndexOf((GridViewDataRowInfo)Grid.CurrentRow);
        string ColumnName = GetColumnNameByFieldName(fieldName);
        if (ColumnName.Length < 1) throw new Exception($"Ошибка! Не найден столбец для данных с именем поля = {fieldName}");
        try
        {
          Grid.Rows[selectedIndex].Cells[ColumnName].Value = value;
        }
        catch (Exception)
        {
          ErrorOccured = true;
        }
        Success = !ErrorOccured;
      }
      return Success;
    }

    public bool UpdateCurrentCell(string fieldName, int value)
    {
      bool Success = false; bool ErrorOccured = false;
      if (Grid.CurrentRow is GridViewDataRowInfo)
      {
        int selectedIndex = Grid.Rows.IndexOf((GridViewDataRowInfo)Grid.CurrentRow);
        string ColumnName = GetColumnNameByFieldName(fieldName);
        if (ColumnName.Length < 1) throw new Exception($"Ошибка! Не найден столбец для данных с именем поля = {fieldName}");
        try
        {
          Grid.Rows[selectedIndex].Cells[ColumnName].Value = value;
        }
        catch (Exception)
        {
          ErrorOccured = true;
        }
        Success = !ErrorOccured;
      }
      return Success;
    }
  }
}
