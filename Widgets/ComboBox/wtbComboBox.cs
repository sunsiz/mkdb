/*
 * Creato da SharpDevelop.
 * Utente: michele
 * Data: 23/02/2009
 * Ora: 9.08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;

namespace mkdb.Widgets.Combobox
{
	
	/// <summary>
	/// Tool Button wdbFrame
	/// </summary>
	public class wtbCombobox : ToolStripButton
	{
		protected int _img_index;
		
		public wtbCombobox(string name, string image_resource) : base()
		{
			this.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			// this.Image = ((System.Drawing.Image)(resources.GetObject(image_resource)));
			this.Image = (System.Drawing.Image)GetEmbeddedImage();
			this.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Name = name;
			this.Size = new System.Drawing.Size(23, 22);
			this.Text = name;
			this.Click += new System.EventHandler(this.ToolStripButtonClick);	
			Common.Instance().ObjTreeImageList.Images.Add(name, this.Image);						
			_img_index = Common.Instance().ObjTreeImageList.Images.Count - 1;					
			// _img_index = Common.Instance().ObjTreeImageList.Images.IndexOf(this.Image);
		}
		
		// Button
		// Cos� per ogni widget
		void ToolStripButtonClick(object sender, EventArgs e)
		{	
			TreeView objtree = Common.Instance().ObjTree;
			WidgetTreeNode ps = Common.Instance().FindBestParentSizer((WidgetTreeNode)objtree.SelectedNode, false);
			WidgetTreeNode pc = Common.Instance().FindBestParentContainer((WidgetTreeNode)objtree.SelectedNode, false);			
			if (ps == null)
				return;
			
			wx.Window win;
			Common.Instance().CheckParentForWidget(pc, ps, out win);
			wdbCombobox item = new wdbCombobox(win, (wx.Sizer)ps.Widget);
			ps.Nodes.Add(item);
			item.ImageIndex = _img_index;
			item.SelectedImageIndex = _img_index;			
			objtree.SelectedNode = item;
		}

		public Image GetEmbeddedImage()
		{
    		try
    		{
    			System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
        		// myNamespace.resources.images.top_left_corner.gif 
        		System.IO.Stream str = a.GetManifestResourceStream("mkdb.Widgets.ComboBox.choice.png");
        		if(str == null)
            		throw new Exception("Could not locate embedded resource ");
        		return new System.Drawing.Bitmap(str);
    		}
    		catch(Exception e)
    		{
        		throw new Exception("mkdb : " + e.Message);
    		}
		}
	}
}