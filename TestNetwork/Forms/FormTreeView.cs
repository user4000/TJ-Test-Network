using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TJFramework;
using static TJFramework.TJFrameworkManager;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork
  {
    public FormTreeView()
    {
      InitializeComponent(); // https://docs.telerik.com/devtools/winforms/controls/treeview/data-binding/binding-to-self-referencing-data //
    }

    public void EventStartWork()
    {
      BtnLoadData.Click += EventButtonLoadData;

      TvFolders.ImageList = this.imageList1;
      //TvFolders.NodeAdded += RadTreeView1_NodeAdded;

    }

    private void EventButtonLoadData(object sender, EventArgs e)
    {
      string file = Program.ApplicationSettings.SettingsDatabaseLocation;
      DataTable table = MsAccessDataReader.GetDataTable(file, "FOLDERS");

      IEnumerable<DataRow> BadRows = table.Rows.Cast<DataRow>().Where(r => r["IdFolder"].ToString() == r["IdParent"].ToString());
      BadRows.ToList().ForEach(r => r.SetField("IdParent", DBNull.Value));

      this.TvFolders.DisplayMember = "NameFolder";
      this.TvFolders.ParentMember = "IdParent";
      this.TvFolders.ChildMember = "IdFolder";
      try
      {
        this.TvFolders.DataSource = table;
      }
      catch (Exception ex)
      {
        Ms.Error("Ошибка при попытке присвоения свойствв DataSource.", ex);
      }

      foreach (var item in TvFolders.Nodes)
      {
        ProcessOneNode(item);
      }

      Ms.Message("", "Data loaded").Control(BtnLoadData).Ok();
    }



    private void ProcessOneNode(RadTreeNode node)
    {   
      if (node.Parent == null)
        node.ImageIndex = 0;
      else
        node.ImageIndex = 3 + node.Nodes.Count;
      node.Font = TvFolders.Font;
      foreach (var item in node.Nodes)
      {
        ProcessOneNode(item);
      }
    }



    private void LoadTestData()
    {

      RadTreeNode root = this.TvFolders.Nodes.Add("Programming", 17);
      root.Nodes.Add("Microsoft Research News and Highlights", 1);

      root.Nodes.Add("Joel on Software", 2);
      root.Nodes.Add("Miguel de Icaza", 3);
      root.Nodes.Add("channel 9", 4);

      root = this.TvFolders.Nodes.Add("News (1)", 18);
      root.Nodes.Add("cnn.com (1)", 5);
      root.Nodes.Add("msnbc.com", 6);
      root.Nodes.Add("reuters.com", 7);
      root.Nodes.Add("bbc.co.uk", 8);

      root = this.TvFolders.Nodes.Add("Personal (19)", 3);
      root.Nodes.Add("sports (2)", 9);
      RadTreeNode folder = root.Nodes.Add("fun (17)", 7);
      folder.Nodes.Add("Lolcats (2)", 10);
      folder.Nodes.Add("FFFOUND (15)", 11);
      folder.Nodes.Add("axaxaxaxa (2)", 0);
      folder.Nodes.Add("dsfdsfsdf  (15)", 1);

      this.TvFolders.Nodes.Add("Telerik blogs", 12);
      this.TvFolders.Nodes.Add("Techcrunch", 13);
      this.TvFolders.Nodes.Add("Engadget", 14);
      this.TvFolders.Nodes.Add("Engadget 111", 15);
      this.TvFolders.Nodes.Add("Engadget 222", 16);
      this.TvFolders.Nodes.Add("Engadget 333", 15);
    }

    private void RadTreeView1_NodeAdded(object sender, RadTreeViewEventArgs e)
    {
      //e.Node.Font = TvFolders.Font;
      //ProcessOneNode(e.Node);
    }


  }
}
