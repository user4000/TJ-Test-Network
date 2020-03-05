using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TJSettings
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
}

