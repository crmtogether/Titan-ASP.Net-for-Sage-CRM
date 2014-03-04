using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public class listEditor : UITypeEditor
    {
        /*
        protected SageCRMConnection getConnection(ITypeDescriptorContext context)
        {
            if (context == null)
                return null;
            foreach (Control c in context.Container.Components)
            {
                if (c.GetType().ToString().Equals("SageCRM.AspNet.SageCRMConnection"))
                {
                    return (c as SageCRMConnection);
                }
                if (c.Controls.Count > 0)
                {
                    c2 = IterateThroughChildren(c);
                    if (c2 is SageCRMConnection)
                        return (c2 as SageCRMConnection);
                }
            }
            return null;
        }
        */
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (provider != null))
            {
                context.OnComponentChanging();
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    
                    listBlockPicker form = new listBlockPicker();
                    //set properties here
                    form.ListValue = (string)value;
                    //form.SageCRMConnectionObject=this.getConnection(context);
                    DialogResult result = edSvc.ShowDialog(form);
                    if (result == DialogResult.OK)
                    {
                        //set the value
                        value = form.ListValue;
                    }
                }
                context.OnComponentChanged();
            }
            return value;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }
    }
}