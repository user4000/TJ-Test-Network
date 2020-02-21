using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace TestNetwork
{
  public class TreeviewManager
  {
    internal RadTreeView Treeview { get; private set; } = null;

    internal Font FontOfNode { get; private set; } = new Font("Verdana", 9);

    private TreeviewManager(RadTreeView treeview, ImageList images, Font font)
    {
      Treeview = treeview; Treeview.ImageList = images; FontOfNode = font;
    }

    public static TreeviewManager Create(RadTreeView treeview, ImageList images, Font font) => new TreeviewManager(treeview, images, font);

    public void SetFontAndImageForAllNodes()
    {
      foreach (var item in Treeview.Nodes) SetFontAndImageForOneNode(item);
    }

    private void SetFontAndImageForOneNode(RadTreeNode node)
    {
      node.ImageIndex = node.Nodes.Count == 0 ? 0 : node.Level + 1;
      node.Font = FontOfNode; // node.Text = node.Text + " => " + node.Level;
      if (node.Level < 5) node.Expand();
      foreach (var item in node.Nodes) SetFontAndImageForOneNode(item);
    }
  }
}


/*
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
*/
