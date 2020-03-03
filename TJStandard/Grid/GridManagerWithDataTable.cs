using System.Data;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using System.Windows.Forms;

namespace TJStandard
{
  public abstract class GridWithDataTable : GridManager // Actions with an object that is presented in a table form. Contains a [RadGridView] associated with a [DataTable] //
  {
    public BindingSource DataBindingSource { get; } = new BindingSource();

    public DataTable Table { get; set; } = new DataTable();

    public void SetDataSourceForGrid() => Grid.DataSource = DataBindingSource;

    public delegate Task TypeDelegateEventDataColumnChanging(object sender, DataColumnChangeEventArgs e);

    private TypeDelegateEventDataColumnChanging DelegateEventDataColumnChanging { get; set; } = null;

    protected bool DelegateEventDataColumnChangingEnabled { get; set; } = true;

    public override void InitializeGrid(RadGridView grid, bool SetAnotherTheme = false) // Called from User Form Constructor //
    {
      base.InitializeGrid(grid, SetAnotherTheme);
      DataBindingSource.DataSource = Table;
      SetDataSourceForGrid();
      Table.ColumnChanging += async (s, e) => await EventDataColumnChanging(s, e);
    }

    public void SetEventDataColumnChanging(TypeDelegateEventDataColumnChanging method)
    {
      DelegateEventDataColumnChanging = method;
    }

    protected async Task EventDataColumnChanging(object sender, DataColumnChangeEventArgs e)
    { // Event: User finished editing cell contents //
      string CurrentColumnName = e.Column.ColumnName;
      string CurrentCellValue = e.Row[CurrentColumnName].ToString();
      string ProposedCellValue = e.ZzProposedValue();

      if ( // if column cannot be empty we return old value //
          (ProposedCellValue.Trim().Length < 1) &&
          (ColumnCannotBeEmpty(CurrentColumnName))
         )
        e.ProposedValue = CurrentCellValue;

      if ((DelegateEventDataColumnChanging != null) && DelegateEventDataColumnChangingEnabled) await DelegateEventDataColumnChanging.Invoke(sender, e);
    }

    //public DataRow FindRowByKey(string KeyField, string KeyValue) => Table.Select($"{KeyField}={KeyValue}").FirstOrDefault();

    public string GetValue(CurrentRowChangedEventArgs e, string FieldName)
    {
      string s = string.Empty;
      try { s = e.CurrentRow.Cells[Standard.GetGridColumnName(FieldName)].Value.ToString(); } catch { s = string.Empty; }
      return s;
    }

    public void UpdateOneCell(string KeyField, string KeyValue, string FieldName, string FieldValue)
    {
      DataRow row = Table.ZzFindRowByKey(KeyField, KeyValue);
      if (row != null)
      {
        DelegateEventDataColumnChangingEnabled = false;
        row[FieldName] = FieldValue;
        DelegateEventDataColumnChangingEnabled = true;
      }
    }

    public void EventEndWork() // The event that is raised by the client when preparing to exit the program //
    {
      DelegateEventDataColumnChanging = null;
      Grid.ZzDispose();
      Table.Clear();
      Table.Columns.Clear();
      Table.Dispose();
    }
  }
}