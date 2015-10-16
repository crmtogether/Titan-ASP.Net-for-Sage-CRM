using System;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public class SageCRMComponentListPropEditor : WindowsFormsComponentEditor
    {
        public override bool EditComponent(ITypeDescriptorContext context, object component,
            IWin32Window owner)
        {
            SageCRMBaseListBlock block = component as SageCRMBaseListBlock;
            if (block == null)
            {
                throw new ArgumentException("Component must be a SageCRMListBlock or SageCRMPortalListBlock", "component");
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
                    block.SageCRMConnection.DesignRequest = true;
                    ListPropEditor form = new ListPropEditor(block);
                    form.ListBlock = block.ListBlock;
                    form.EntityName = block.EntityName;
                    form.BlockTitle = block.BlockTitle;
                    form.SelectSQL = block.SelectSQL;
                    form.WhereClause = block.EntityWhere;
                    form.EntryBlockName = block.EntryBlockName;
                    form.FilterBlockName = block.FilterBlockName;
                    if (block is SageCRMListBlock)
                    {
                        form.showWorkFlow();
                        form.WorkFlowTable = (block as SageCRMListBlock).WorkflowTable;
                        form.ShowNewWorkflowButtons = (block as SageCRMListBlock).ShowNewWorkFlowButtons;
                        form.ShowWorkflowButtons = (block as SageCRMListBlock).ShowWorkFlowButtons;
                    }
                    else //i assume its the portal
                    {
                        form.hideWorkFlow();
                    }
                    if (form.ShowDialog(owner) == DialogResult.OK)
                    {
                      changed = true;
                    }
                    block.SageCRMConnection.DesignRequest = false;
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