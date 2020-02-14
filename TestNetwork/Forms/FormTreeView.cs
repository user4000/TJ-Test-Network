using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TJFramework;

namespace TestNetwork
{
  public partial class FormTreeView : RadForm, IEventStartWork
  {
    public FormTreeView()
    {
      InitializeComponent();
    }

    public void EventStartWork()
    {
      LoadData();
    }

    private void LoadData()
    {
      radTreeView1.NodeAdded += RadTreeView1_NodeAdded;
      RadTreeNode root = this.radTreeView1.Nodes.Add("Programming");
      root.Nodes.Add("Microsoft Research News and Highlights", 1);

      root.Nodes.Add("Joel on Software", 2);
      root.Nodes.Add("Miguel de Icaza", 3);
      root.Nodes.Add("channel 9", 4);

      root = this.radTreeView1.Nodes.Add("News (1)");
      root.Nodes.Add("cnn.com (1)", 5);
      root.Nodes.Add("msnbc.com", 6);
      root.Nodes.Add("reuters.com", 7);
      root.Nodes.Add("bbc.co.uk", 8);

      root = this.radTreeView1.Nodes.Add("Personal (19)");
      root.Nodes.Add("sports (2)", 9);
      RadTreeNode folder = root.Nodes.Add("fun (17)");
      folder.Nodes.Add("Lolcats (2)", 10);
      folder.Nodes.Add("FFFOUND (15)", 11);
      folder.Nodes.Add("axaxaxaxa (2)", 0);
      folder.Nodes.Add("dsfdsfsdf  (15)", 1);

      this.radTreeView1.Nodes.Add("Telerik blogs", 12);
      this.radTreeView1.Nodes.Add("Techcrunch", 13);
      this.radTreeView1.Nodes.Add("Engadget", 14);
      this.radTreeView1.Nodes.Add("Engadget 111", 15);
      this.radTreeView1.Nodes.Add("Engadget 222", 16);
      this.radTreeView1.Nodes.Add("Engadget 333", 15);
    }

    private void RadTreeView1_NodeAdded(object sender, RadTreeViewEventArgs e)
    {
      e.Node.Font = radTreeView1.Font;
    }
  }
}
