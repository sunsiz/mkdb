/*
 * Creato da SharpDevelop.
 * Utente: michele
 * Data: 10/02/2009
 * Ora: 9.09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing;


namespace mkdb.Widgets
{
	public class wiwApp : wx.Window, IWDBBase
	{
		protected wdbAppProps _props;
		protected bool _is_selected;
			
		public wiwApp(wx.Window _pc, wx.Sizer _ps) : base(null)
		{
			_props = new wdbAppProps();
			SetDefaultProps("Project");
			SetWidgetProps();
		}
		
		#region IWidgetElem Interface implementation
		public wx.SizerItem SizerItem
		{
			get	{	return null;	}
		}
		public WidgetProps Properties	
		{	
			get	{	return _props; }
		}
		public wx.Window ParentContainer	
		{	
			get	{	return this.Parent;	}
		}
		public wx.Sizer	ParentSizer		
		{	
			get	{	return	null;	}
		}
		public bool	IsSizer		
		{	
			get	{	return false;	}
		}
		public bool	IsSelected	
		{	
			get	{	return _is_selected;	}
			set	{	_is_selected = value;	}
		}		
		public int WidgetType
		{
			get	{	return (int)StandardWidgetType.WID_APP; }
		}
		#endregion
		
		
		private void SetDefaultProps(string name)
		{
			_props.EnableNotification = false;
			_props.Name = name;
			_props.EnableNotification = true;
		}
				
		public bool InsertWidget()
		{
			InsertWidgetInText();
			return true;
		}
		public bool DeleteWidget()
		{
			return false;
		}		
		
		public bool InsertWidgetInText()
		{
			/*
			if __name__ == "__main__":
    			app = wx.PySimpleApp(0)
    			wx.InitAllImageHandlers()
    			...
    			app.MainLoop()
			*/
			Python.PyFileEditor ed = Common.Instance().PyEditor;
			ed.InsertSingleLine(-1, Python.PyFileSection.PY_APP_SECTION, "if __name__ == \"__main__\":\n");
			ed.InsertSingleLine(-1, Python.PyFileSection.PY_APP_SECTION, "\tapp = wx.PySimpleApp(0)\n");
			ed.InsertSingleLine(-1, Python.PyFileSection.PY_APP_SECTION, "\twx.InitAllImageHandlers()\n");
			ed.InsertSingleLine(-1, Python.PyFileSection.PY_APP_SECTION, "\tapp.MainLoop()\n");
			return true;			
		}
		
		public bool DeleteWidgetFromText()
		{
			return true;
		}
		
		public long FindBlockInText()
		{
			return -1;
		}
		
		public bool CanAcceptChildren()
		{
			return false;
		}
		
		public void HighlightSelection()
		{			
		}
								
		public void winProps_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
            	case "Name":
            		this.Name = _props.Name;
					Common.Instance().ObjTree.SelectedNode.Text = _props.Name;		
            		break;
            }
            this.UpdateWindowUI();            
        }
				
		public void SetWidgetProps()
		{
			_props.PropertyChanged += new PropertyChangedEventHandler(winProps_PropertyChanged);
			Common.Instance().ObjPropsPanel.SelectedObject = _props;
		}		
	}
}
