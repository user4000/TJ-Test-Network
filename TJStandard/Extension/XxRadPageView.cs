using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace TJStandard
{
  public static class XxRadPageView
  {
    public static void ZzPagesVisibility(this RadPageView pageView, ElementVisibility state)
    {
      foreach (var page in pageView.Pages) page.Item.Visibility = state;
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