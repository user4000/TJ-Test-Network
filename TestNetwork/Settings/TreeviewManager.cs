using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using static TestNetwork.Program;

namespace TestNetwork
{
  public class TreeviewManager
  {
    internal RadTreeView Treeview { get; private set; } = null;

    internal Font FontOfNode { get; private set; } = new Font("Verdana", 9);

    private TreeviewManager(RadTreeView treeview, ImageList images, Font font)
    {
      Treeview = treeview;
      Treeview.ImageList = images;
      FontOfNode = font;
      SetEvents();
    }

    private void SetEvents()
    {
      Treeview.SelectedNodeChanging += EventSelectedNodeChanging;
    }

    private void EventSelectedNodeChanging(object sender, RadTreeViewCancelEventArgs e)
    {
      e.Cancel = !Manager.UiControl.FlagAllowChangeSelectedItem;
    }

    internal static TreeviewManager Create(RadTreeView treeview, ImageList images, Font font) => new TreeviewManager(treeview, images, font);

    internal void SetFontAndImageForAllNodes()
    {
      foreach (var item in Treeview.Nodes) SetFontAndImageForOneNode(item);
    }

    private void SetFontAndImageForOneNode(RadTreeNode node)
    {
      node.ImageIndex = node.Nodes.Count == 0 ? 0 : node.Level + 1;
      node.Font = FontOfNode; // node.Text = node.Text + " => " + node.Level;
      //if (node.Level < 1) node.Expand();
      foreach (var item in node.Nodes) SetFontAndImageForOneNode(item);
    }

    internal void TryToSelectFolderAfterCreating(RadTreeNode parent, string NameChildFolder)
    {
      RadTreeNode[] nodes = parent.FindNodes(item => item.Name == NameChildFolder);
      if (nodes.Length < 1) return;
      if (Program.ApplicationSettings.SelectNewFolderAfterCreating)
        try
        {
          nodes[0].Selected = true; nodes[0].EnsureVisible();
        }
        catch { }
      else
      {
        parent.Expanded = true; parent.Selected = true;
        try
        {
          foreach (RadTreeNode item in parent.Nodes) if (item != nodes[0]) item.Collapse();
          nodes[0].EnsureVisible();
        }
        catch { }
      }
    }
  }
}
