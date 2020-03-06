using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI;
using TJFramework;
using TJFramework.ApplicationSettings;
using static TestSettingsConsumer.Program;
using static TJFramework.Logger.Manager;

namespace TestSettingsConsumer
{
  public enum TextSearchMode
  {
    StartWith = 0,
    Contains = 1,
    WholeWord = 2
  }

  [Serializable]
  public class CxApplicationSettings : TJStandardApplicationSettings
  {
    [Category("Settings Database")]
    [Editor(typeof(PropertyGridBrowseEditor), typeof(BaseInputEditor))] // File name dialog //
    public string SettingsDatabaseLocation { get; set; }

    [Category("Settings Database")]
    [DisplayName("Folder search mode")]
    public TextSearchMode FolderNameSearchMode { get; set; } = TextSearchMode.StartWith;

    [Category("Settings Database")]
    [DisplayName("Select a new folder after creation")]
    public bool SelectNewFolderAfterCreating { get; set; } = false;

    [Category("Settings Database")]
    [DisplayName("Default new database file name")]
    public string NewFileName { get; set; } = "settings.db";

    [Category("User interface")]
    [DisplayName("Orientation tabs of the main form")]
    public StripViewAlignment MainPageOrientation { get; set; } = StripViewAlignment.Top;

    [Category("User interface")]
    [DisplayName("Font of a hierarchy folder list")]
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
}



