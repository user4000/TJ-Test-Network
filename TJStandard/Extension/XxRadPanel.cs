using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace TJStandard
{
  public static class XxRadPanel
  {
    public static void ZzAddTestButton(this RadPanel panel, string ButtonText, Point Location, Size Size, Action ButtonClickHandler)
    {
      RadButton btn = new RadButton();
      btn.Text = ButtonText;   
      panel.Controls.Add(btn);
      btn.Location = Location;
      btn.Size = Size;
      btn.BringToFront();
      btn.Click += (s, e) => ButtonClickHandler();
    }
  }
}

/*
public static T ZzAddForm<T>(this RadPanel panel) where T : RadForm, new() // Добавляем форму указанного типа на панель //
{
  T form = new T(); 
  form.TopLevel = false;
  panel.Controls.Add(form);
  form.Dock = DockStyle.Fill;
  form.FormBorderStyle = FormBorderStyle.None;
  form.Visible = true;
  form.BringToFront();
  if (form is IFormStartWorkHandler) (form as IFormStartWorkHandler).FormStartWork();
  return form;
}*/