using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI;
using TJFramework;
using TJFramework.ApplicationSettings;
using static TestNetwork.Program;
using static TJFramework.Logger.Manager;

namespace TestNetwork
{
  public enum TextSearchMode
  {
    StartWith = 0,
    Contains = 1,
    WholeWord = 2
  }


  // Пример использования настроек. 
  // В самом низу текста события:
  // public override void PropertyValueChanged(string PropertyName)
  // public override void EventBeforeSaving()
  // public override void EventAfterSaving()


  [Serializable]
  public class CxApplicationSettings : TJStandardApplicationSettings
  {
    [Category("База данных настроек")]
    [Editor(typeof(PropertyGridBrowseEditor), typeof(BaseInputEditor))] // File name dialog //
    public string SettingsDatabaseLocation { get; set; }

    [Category("База данных настроек")]
    [DisplayName("Режим поиска папки по названию")]
    public TextSearchMode FolderNameSearchMode { get; set; } = TextSearchMode.StartWith;

    [Category("База данных настроек")]
    [DisplayName("Выделять новую папку после создания")]
    public bool SelectNewFolderAfterCreating { get; set; } = false;

    [Category("Внешний вид")]
    [DisplayName("Ориентация вкладок главной формы")]
    public StripViewAlignment MainPageOrientation { get; set; } = StripViewAlignment.Top;


    [Category("Внешний вид")]
    [DisplayName("Шрифт иерархического списка папок")]
    public Font TreeViewFont { get; set; } = new Font("Verdana", 9.75F);


    [Browsable(false)]
    public Size TreeViewSize { get; set; } = new Size(400, 0);

    public override void PropertyValueChanged(string PropertyName)
    {
      //Manager.EventPropertyValueChanged(PropertyName);
      //ms.Message(MessageType.msg_info, property_name, "Changed!", 4,MessagePosition.pos_SC); 
      //Log.Save(MsgType.Debug, "public override void PropertyValueChanged(string PropertyName)", PropertyName);
      if (PropertyName == nameof(MainPageOrientation))
        TJFrameworkManager.Service.SetMainPageViewOrientation(MainPageOrientation);
    }

    public override void EventBeforeSaving()
    {
      //Password = "";
      //Log.Save(MsgType.Debug, "public override void EventBeforeSaving()", "test");
      //MessageBox.Show("EventBefore_Saving");
    }

    public override void EventAfterSaving()
    {
      //Password = "12345";
      //Log.Save(MsgType.Debug, "public override void EventAfterSaving()", "test");
      //MessageBox.Show("EventAfter_Saving");
    }
  }




  /* ===================================================================================================== */

  [Serializable]
  public class ExampleOfApplicationSettings : TJStandardApplicationSettings
  {


    [Category("Внешний вид")]
    [DisplayName("Ориентация вкладок главной формы")]
    public StripViewAlignment MainPageOrientation { get; set; } = StripViewAlignment.Top;


    [Category("Category 1")]
    public string MyString1 { get; set; } = "Privet 1111";

    [Category("Category 1")]
    public DateTime MyDatetime1 { get; set; } = DateTime.Now;

    [Category("Category 2")]
    public Font MyFont1 { get; set; } = new Font("Verdana", 14F, FontStyle.Italic);

    [Category("Category 2")]
    public Color MyColor1 { get; set; } = Color.LightGreen;


    [Category("Range Example")]
    [RadRange(1, 5)]
    public byte DoorsCount { get; set; } = 4;


    [Browsable(false)]
    public int MyHiddenProperty { get; set; } = 890110000;

    [Category("Read Only Example")]
    [ReadOnly(true)]
    public int Count { get; set; } = 18991;

    [Category("Visible Name differs from Property Name")]
    [DisplayName("This is visible name of a property!")]
    public string ExampleOfStringProperty { get; set; }

    [Category("Description of property example")]
    [Description("The manufacturer of the item")]
    public string Manufacturer { get; set; }

    [Category("Login")]
    [RadSortOrder(0)]
    public string ServerName { get; set; } = "MyServer";

    [Category("Login")]
    [RadSortOrder(1)]
    [Description("User account")]
    public string Username { get; set; } = "Vasya";

    [Category("Login")]
    [RadSortOrder(2)]
    [Description("User password")]
    [PasswordPropertyText(true)]
    public string Password { get; set; } = "";

    [Category("Login"), RadSortOrder(3)]
    public bool Connect { get; set; } = false;


    [Category("Login")]
    [RadSortOrder(4)]
    [ReadOnly(true)]
    [DisplayName("Connection state")]
    public bool ConnectionState { get; set; }


    public DateTime inner_Date_Time = TJStandardDateTimeDefaultValue;

    [Category("Login")]
    [RadSortOrder(5)]
    public string MyDateTime
    {
      get { return GetDateTime(inner_Date_Time); }
      set { inner_Date_Time = SetDateTime(value, inner_Date_Time); }
    }

    public override void PropertyValueChanged(string PropertyName)
    {
      //Manager.EventPropertyValueChanged(PropertyName);
      //ms.Message(MessageType.msg_info, property_name, "Changed!", 4,MessagePosition.pos_SC); 
      //Log.Save(MsgType.Debug, "public override void PropertyValueChanged(string PropertyName)", PropertyName);
      if (PropertyName == nameof(MainPageOrientation))
        TJFrameworkManager.Service.SetMainPageViewOrientation(MainPageOrientation);
    }

    public override void EventBeforeSaving()
    {
      //Password = "";
      //Log.Save(MsgType.Debug, "public override void EventBeforeSaving()", "test");
      //MessageBox.Show("EventBefore_Saving");
    }

    public override void EventAfterSaving()
    {
      //Password = "12345";
      //Log.Save(MsgType.Debug, "public override void EventAfterSaving()", "test");
      //MessageBox.Show("EventAfter_Saving");
    }
  }
}



