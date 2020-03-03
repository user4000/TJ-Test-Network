using System.Data;
using System.Linq;

namespace TJStandard
{
  public static class XxDataTable
  {
    public static DataRow ZzFindRowByKey(this DataTable Table, string KeyField, string KeyValue) => Table.Select($"{KeyField}={KeyValue}").FirstOrDefault();

    public static DataRow ZzFindRowByKey(this DataTable Table, string KeyField, int KeyValue) => Table.Select($"{KeyField}={KeyValue}").FirstOrDefault();

    public static bool ZzUpdateOneCell(this DataTable Table, string KeyFieldName, string KeyValue, string FieldName, int Value)
    {
      bool Result = false;
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row[FieldName] = Value; Result = true;  }
      return Result;
    }

    public static bool ZzUpdateOneCell(this DataTable Table, string KeyFieldName, string KeyValue, string FieldName, string Value)
    {
      bool Result = false;
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row[FieldName] = Value; Result = true;  }
      return Result;
    }

    public static bool ZzUpdateOneCell(this DataTable Table, string KeyFieldName, int KeyValue, string FieldName, int Value)
    {
      bool Result = false;
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row[FieldName] = Value; Result = true;  }
      return Result;
    }

    public static bool ZzUpdateOneCell(this DataTable Table, string KeyFieldName, int KeyValue, string FieldName, string Value)
    {
      bool Result = false;
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row[FieldName] = Value; Result = true;  }
      return Result;
    }

    public static bool ZzDeleteOneRow(this DataTable Table, string KeyFieldName, int KeyValue)
    {
      bool Result = false;      
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row.Delete(); Table.AcceptChanges(); Result = true;  }
      return Result;
    }

    public static bool ZzDeleteOneRow(this DataTable Table, string KeyFieldName, string KeyValue)
    {
      bool Result = false;      
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null)  { row.Delete(); Table.AcceptChanges(); Result = true;  }
      return Result;
    }

    public static int ZzGetRowIndex(this DataTable Table, string KeyFieldName, string KeyValue)
    {
      int Result = 0;      
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null) Result = Table.Rows.IndexOf(row);
      return Result;
    }

    public static int ZzGetRowIndex(this DataTable Table, string KeyFieldName, int KeyValue)
    {
      int Result = 0;      
      DataRow row = Table.ZzFindRowByKey(KeyFieldName, KeyValue);
      if (row != null) Result = Table.Rows.IndexOf(row);
      return Result;
    }
  }
}
