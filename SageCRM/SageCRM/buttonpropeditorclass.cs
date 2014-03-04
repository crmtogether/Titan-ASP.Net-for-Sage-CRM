using System;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public class SageCRMComponentButtonPropEditor : WindowsFormsComponentEditor
    {
        public override bool EditComponent(ITypeDescriptorContext context, object component,
            IWin32Window owner)
        {
            SageCRMButton block = component as SageCRMButton;
            if (block == null)
            {
                throw new ArgumentException("Component must be a SageCRMButton", "component");
            }
            IServiceProvider site = block.Site;
            IComponentChangeService changeservice = null;
            DesignerTransaction transaction = null;
            bool changed = false;

            try
            {
                if (site != null)
                {
                    IDesignerHost designerhost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
                    transaction = designerhost.CreateTransaction("Property Editor");
                    changeservice = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
                    if (changeservice != null)
                    {
                        try
                        {
                            changeservice.OnComponentChanging(block, null);
                        }
                        catch (CheckoutException ex)
                        {
                            if (ex == CheckoutException.Canceled)
                                return false;
                            throw ex;
                        }
                    }
                }
                try
                {
                    ButtonPropEditor form = new ButtonPropEditor(block);
                    form.Caption=block.Caption;
                    form.ImageName = block.ImageName;
                    form.PermissionsEntity = block.PermissionsEntity;
                    form.PermissionsType = block.PermissionsType;
                    form.Target = block.Target;
                    form.Url = block.Url;
                    if (form.ShowDialog(owner) == DialogResult.OK)
                    {
                        changed = true;
                    }
                }
                finally
                {
                    if (changed && changeservice != null)
                    {
                        changeservice.OnComponentChanged(block, null, null, null);
                    }
                }
            }
            finally
            {
                if (transaction != null)
                {
                    if (changed)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Cancel();
                    }
                }
            }
            return changed;
        }

    }

}