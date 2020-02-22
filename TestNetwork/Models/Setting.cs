using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetwork
{
  [Serializable]
  public class Setting
  {
    public int IdFolder { get; set; }

    public string IdSetting { get; set; }

    public int IdType { get; set; }

    public string NameType { get; set; }

    public string SettingValue { get; set; }

    public int Rank { get; set; }

    public string BooleanValue { get; set; }

    public Setting(int idFolder, string idSetting, int idType, string nameType, string settingValue, int rank, string booleanValue)
    {
      IdFolder = idFolder;
      IdSetting = idSetting;
      IdType = idType;
      NameType = nameType;
      SettingValue = settingValue;
      Rank = rank;
      BooleanValue = booleanValue;
    }
  }

  public static class XxListSetting
  {
    public static void ZzAdd
      (
      this IList<Setting> list, 
      int IdFolder, 
      string IdSetting, 
      int IdType, 
      string NameType,
      string SettingValue, 
      int Rank,
      string BooleanValue
      )
    {
      list.Add(new Setting(IdFolder, IdSetting, IdType, NameType, SettingValue, Rank, BooleanValue));
    }
  }
}
