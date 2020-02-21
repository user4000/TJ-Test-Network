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

    public string SettingValue { get; set; }

    public int Rank { get; set; }

    public int BooleanValue { get; set; }

    public Setting(int idFolder, string idSetting, int idType, string settingValue, int rank, int booleanValue)
    {
      IdFolder = idFolder;
      IdSetting = idSetting;
      IdType = idType;
      SettingValue = settingValue;
      Rank = rank;
      BooleanValue = booleanValue;
    }
  }

  public static class XxListSetting
  {
    public static void ZzAdd(this IList<Setting> list, int IdFolder, string IdSetting, int IdType, string SettingValue, int Rank, int BooleanValue)
    {
      list.Add(new Setting(IdFolder, IdSetting, IdType, SettingValue, Rank, BooleanValue));
    }
  }
}
