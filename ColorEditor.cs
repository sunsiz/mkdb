/*
 * Creato da SharpDevelop.
 * Utente: Family Rose
 * Data: 01/02/2009
 * Ora: 21.01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace mkdb
{
	
	public class wxColor : wx.Colour
	{
		public wxColor()
		{			
		}
		
		public wxColor(byte red, byte green, byte blue)
		{			
			Set(red, green, blue);
		}		
		
		public override string ToString()
		{
			return this.Red + "; " + this.Green + "; " + this.Blue;
		}
	}
	

	public class wxColorEditor : UITypeEditor
	{
		
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			// We will use a window for property editing.
			return UITypeEditorEditStyle.Modal;
		} 		

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || context.Instance == null || provider == null)
                return value;

            wxColor color = (wxColor)value;
            System.Windows.Forms.ColorDialog colDialog = new System.Windows.Forms.ColorDialog();
            colDialog.Color = new System.Drawing.Color();
            colDialog.Color = System.Drawing.Color.FromArgb(255, color.Red, color.Green, color.Blue);
            if (colDialog.ShowDialog() != DialogResult.Cancel)
			{
   				wxColor retcolor = new wxColor(colDialog.Color.R, 
   				                               colDialog.Color.G, 
   				                               colDialog.Color.B);
   				return retcolor;
			}            
			return value;
		}
		
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		
		public override void PaintValue(PaintValueEventArgs e)
        {
			if (e.Value != null)
			{			
 				if (e.Value is wxColor) 
   				{
 					System.Drawing.Color col = new System.Drawing.Color();
 					col = System.Drawing.Color.FromArgb(255, ((wxColor)e.Value).Red,
 					                                    ((wxColor)e.Value).Green,
 					                                    ((wxColor)e.Value).Blue);
 					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(col);
         			e.Graphics.FillRectangle(brush, e.Bounds); 					
 				}
 			}
   			base.PaintValue(e);
		}		
	}	
	
}
