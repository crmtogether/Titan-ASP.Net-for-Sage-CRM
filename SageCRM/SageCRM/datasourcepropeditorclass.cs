using System;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public class SageCRMComponentDataSourcePropEditor : WindowsFormsComponentEditor
    {
        public override bool EditComponent(ITypeDescriptorContext context, object component,
            IWin32Window owner)
        {
            SageCRMDataSource block = component as SageCRMDataSource;
            if (block == null)
            {
                throw new ArgumentException("Component must be a SageCRMDataSource", "component");
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
                    DataSourcePropEditor form = new DataSourcePropEditor(block);
                    form.TableName = block.TableName;
                    form.SelectSQL = block.SelectSQL;
                    form.WhereClause = block.WhereClause;
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