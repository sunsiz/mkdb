/*
 * Creato da SharpDevelop.
 * Utente: Family Rose
 * Data: 18/02/2009
 * Ora: 18.34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing;
using System.Collections;

namespace mkdb.Widgets
{

	public class wdbTextEditProps : wxWindowProps
	{
		// * wxButton props : name, style, label, default, wxWindow, Align
		protected wxFlags _tstyle;
		protected wxFlags _lstyle;
		protected string _label;
		protected int _height;
		protected int _label_width;
		
		public wdbTextEditProps() : base()
		{
			_name = "TextEdit";
			_label = "TextEdit";
			_height = 20;
			_label_width = 100;
			_lstyle = new wxFlags();
			_lstyle.AddItem("wxALIGN_LEFT", wx.Alignment.wxALIGN_LEFT, true);
			_lstyle.AddItem("wxALIGN_RIGHT", wx.Alignment.wxALIGN_RIGHT, false);
			_lstyle.AddItem("wxALIGN_CENTER", wx.Alignment.wxALIGN_CENTRE, false);
			_lstyle.AddItem("wxST_NO_AUTORESIZE", wx.StaticText.wxST_NO_AUTORESIZE, false);
			_tstyle = new wxFlags();
			_tstyle.AddItem("wxTE_CENTER", wx.TextCtrl.wxTE_CENTER, false);
			_tstyle.AddItem("wxTE_LEFT", wx.TextCtrl.wxTE_LEFT, true);
			_tstyle.AddItem("wxTE_MULTILINE", wx.TextCtrl.wxTE_MULTILINE, false);
			_tstyle.AddItem("wxTE_NO_VSCROLL", wx.TextCtrl.wxTE_NO_VSCROLL, false);
			_tstyle.AddItem("wxTE_PROCESS_ENTER", wx.TextCtrl.wxTE_PROCESS_ENTER, false);
			_tstyle.AddItem("wxTE_PROCESS_TAB", wx.TextCtrl.wxTE_PROCESS_TAB, false);
			_tstyle.AddItem("wxTE_READONLY", wx.TextCtrl.wxTE_READONLY, false);
			_tstyle.AddItem("wxTE_RIGHT", wx.TextCtrl.wxTE_RIGHT, false);
		}
		
		[CategoryAttribute("TextEdit"), DescriptionAttribute("TextEdit Props")]
		public string Label
		{
			get	{	return _label;	}
			set	{	_label = value;	NotifyPropertyChanged("Label");	}
		}
		
		[TypeConverter(typeof(wxFlagsTypeConverter))]
		[Editor(typeof(wxFlagsEditor), typeof(UITypeEditor))]
		[CategoryAttribute("TextEdit"), DescriptionAttribute("TextEdit Props")]
		public wxFlags LabelStyle
		{
			get	{	return _lstyle;	}
			set	{	_lstyle = value;	NotifyPropertyChanged("LabelStyle");	}
		}
		
		[TypeConverter(typeof(wxFlagsTypeConverter))]
		[Editor(typeof(wxFlagsEditor), typeof(UITypeEditor))]
		[CategoryAttribute("TextEdit"), DescriptionAttribute("TextEdit Props")]
		public wxFlags TextEditStyle
		{
			get	{	return _tstyle;	}
			set	{	_tstyle = value;	NotifyPropertyChanged("TextEditStyle");	}
		}
		
		[CategoryAttribute("TextEdit"), DescriptionAttribute("TextEdit Props")]
		public int Height
		{
			get	{	return _height;	}
			set	{	_height = value;	NotifyPropertyChanged("Height");	}
		}
				
		[CategoryAttribute("TextEdit"), DescriptionAttribute("TextEdit Props")]
		public int LabelWidth
		{
			get	{	return _label_width;	}
			set	{	_label_width = value;	NotifyPropertyChanged("LabelWidth");	}
		}		
	}

	public class wiwTextEdit : wx.FlexGridSizer, IWDBBase
	{
		protected static long _text_cur_index=0;
		protected wdbTextEditProps _props;
		protected bool _is_selected;
		protected wx.Window _p_container;
		protected wx.Sizer _p_sizer;
		protected wx.SizerItem _sizer_item;		
		// sub widgets
		protected wx.StaticText _sub_label;
		protected wx.TextCtrl _sub_text;		
		
		public wiwTextEdit(wx.Window _pc, wx.Sizer _ps) : base(1, 2, 0, 0)
		{
			_props = new wdbTextEditProps();
			this.AddGrowableCol(1);
			_sub_label = new wx.StaticText(_pc, -1, "TextEdit");
			_sub_text = new wx.TextCtrl(_pc, -1, "");
			this.Add(_sub_label, 0, wx.Alignment.wxALIGN_CENTER_VERTICAL|wx.Direction.wxLEFT, 5);
			this.Add(_sub_text, 0, wx.Alignment.wxALIGN_CENTER_VERTICAL|wx.Stretch.wxEXPAND|wx.Direction.wxRIGHT, 5);			
			_p_container = _pc;
			_p_sizer = _ps;
			_sizer_item = null;
			_text_cur_index++;			
			string name = "TextEdit" + _text_cur_index.ToString();			
			SetDefaultProps(name);
			SetWidgetProps();
		}
		
		
		#region IWidgetElem Interface implementation
		public wx.SizerItem SizerItem
		{
			get	{	return _sizer_item;	}
		}		
		public WidgetProps Properties	
		{	
			get	{	return _props; }
		}
		public wx.Window ParentContainer	
		{	
			get	{	return _p_container;	}
		}
		public wx.Sizer	ParentSizer		
		{	
			get	{	return	_p_sizer;	}
		}
		public bool	IsSizer		
		{	
			get	{	return true;	}
		}
		public bool	IsSelected	
		{	
			get	{	return _is_selected;	}
			set	{	_is_selected = value;	}
		}		
		public int WidgetType
		{
			get	{	return (int)StandardWidgetType.WID_BOXSIZER; }
		}		
		
		private void SetDefaultProps(string name)
		{
			_props.EnableNotification = false;	
			_props.Label = name;
			_props.Name = name;
			_props.EnableNotification = true;
			this._sub_label.Label = name;
			this._sub_label.StyleFlags = (uint)_props.LabelStyle.ToLong;
			this._sub_text.StyleFlags = (uint)_props.TextEditStyle.ToLong;
			this._sub_label.SetSizeHints(_props.LabelWidth, -1);  
			// !! I set the height of the control : _sub_text
			int wl = _props.Height;
			if (wl < 10)
			{
				_props.EnableNotification = false;
				_props.Height = wl = 10;
				_props.EnableNotification = true;			
			}
			_sub_text.SetSizeHints(_sub_text.Width, wl);
			this.SetMinSize(new Size(-1, wl + _props.BorderWidth * 2));			
			// !!
			_p_container.AutoLayout = true;
			_p_container.Layout();			
		}
				
		public bool InsertWidget()
		{			
			_p_sizer.Add(this, 0, wx.Stretch.wxEXPAND|wx.Direction.wxALL, _props.BorderWidth);
			_sizer_item = (wx.SizerItem)_p_sizer.GetItem(_p_sizer.GetItemCount() - 1);			
			// _p_container.AutoLayout = true;
			_p_container.Layout();
			return true;
		}
		
		public bool DeleteWidget()
		{
			// _p_sizer.Detach(this);
			// _p_sizer.Remove(this);	
			this.Clear(true);
			if (_p_sizer != null)
			{
				_p_sizer.Remove(this);				
			}
			return false;
		}
		
		public long FindBlockInText()
		{
			return -1;
		}
		
		public bool CanAcceptChildren()
		{
			return true;
		}				
				
		public void HighlightSelection()
		{
			Panel pan = Common.Instance().Canvas;
			// Graphics area = Graphics.FromHwnd(this.GetHandle());
			Graphics area = Graphics.FromHwnd(_p_container.GetHandle());
			Color cl = Color.FromArgb(255, _p_container.BackgroundColour.Red,
			                     _p_container.BackgroundColour.Green,
			                     _p_container.BackgroundColour.Blue);
			area.Clear(cl);
			if (IsSelected)
			{
				Pen _pen = new Pen(Color.Red, 1);						
				Point _ps = Point.Subtract(this.Position, new Size(_props.BorderWidth, _props.BorderWidth));
				Size _sz = this.Size;
				if ((_sz.Width == 0) || (_sz.Height == 0))
				{
					return;
				}
				_sz.Width += (_props.BorderWidth*2);
				// _sz.Height += (_props.BorderWidth*2 + 2);
				area.DrawRectangle(_pen, _ps.X, _ps.Y, _sz.Width, _sz.Height);
			}						
		}		
		
		public void winProps_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
			bool baa = false;
            switch (e.PropertyName)
            {
            	case "Name":
					Common.Instance().ObjTree.SelectedNode.Text = _props.Name;	
            		break;            		
            	case "Label":
            		this._sub_label.Label = _props.Label;
            		break;
            	case "LabelWidth":
					this._sub_label.SetSizeHints(_props.LabelWidth, -1);            		
					_p_container.Layout();
            		break;
            	case "Height":
					// !! I set the height of the control : _sub_text
					int wl = _props.Height;
					if (wl < 10)
					{
						_props.EnableNotification = false;
						_props.Height = wl = 10;
						_props.EnableNotification = true;			
					}
					_sub_text.SetSizeHints(_sub_text.Width, wl);
					this.SetMinSize(new Size(-1, wl + _props.BorderWidth * 2));			
					// !!
					_p_container.AutoLayout = true;
					_p_container.Layout();
					this.HighlightSelection();					
            		break;
            	case "Border":
            		baa = true;
            		break;
            	case "BorderWidth":
            		baa = true;
            		break;
            	case "Alignment":
            		baa = true;
            		break;
            }
            if (baa)
            {
           		// Is this the only way???         
           		_sizer_item.Border = _props.BorderWidth;
           		_sizer_item.Flag = (int)(_props.Alignment.ToLong|_props.Border.ToLong);
           		// Recalc minsize
				int wl = _sub_text.Height + _props.BorderWidth * 2;
				this.SetMinSize(new Size(-1, wl));           		
				_p_container.AutoLayout = true;
				_p_container.Layout();  
				this.HighlightSelection();
            }
        }
				
		public void SetWidgetProps()
		{
			_props.PropertyChanged += new PropertyChangedEventHandler(winProps_PropertyChanged);
			Common.Instance().ObjPropsPanel.SelectedObject = _props;
		}		
		#endregion
	}
}
