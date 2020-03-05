﻿using TJStandard;
using TJSettings;

using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public class ProjectManager: IOutputMessage
  {
    public const string Empty = "";

    public Converter CvManager { get; } = new Converter();

    public UiControlManager UiControl { get; } = new UiControlManager();

    private FormTreeView FxTreeview { get; set; } = null;

    internal void Init(FormTreeView form)
    {
      FxTreeview = form;
      UiControl.Init(form);
    }

    public void OutputMessage(string message, string header = Empty)
    {
      Ms.Message(message, header).NoAlert().Debug();
    }

    public string RemoveSpecialCharacters(string NameFolder)
    {
      return NameFolder.Trim().Replace(' ', '_').RemoveSpecialCharacters();
    }
  }
}