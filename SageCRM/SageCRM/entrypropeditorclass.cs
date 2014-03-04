using System;
using System.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    public class SageCRMComponentEntryPropEditor : WindowsFormsComponentEditor
    {
        public override bool EditComponent(ITypeDescriptorContext context, object component,
            IWin32Window owner)
        {
            SageCRMBaseEntryBlock block = component as SageCRMBaseEntryBlock;
            if (block == null)
            {
                throw new ArgumentException("Component must be a SageCRMEntryBlock or SageCRMPortalEntryBlock", "component");
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
                    EntryPropEditor form = new EntryPropEditor(block);
                    form.EntityName = block.EntityName;
                    form.BlockTitle = block.BlockTitle;
                    form.CreateMode = block.CreateMode;
                    form.WhereClause = block.EntityWhere;
                    form.EntryBlockName = block.EntryBlockName;
                    form.ListBlock = block.ListBlockName;
                    form.SearchMode = block.SearchMode;
                    form.ShowSearchList = block.ShowSearchList;
                    form.AfterSavePage = block.AfterSavePage;
                    form.AjaxSearchMode = block.AjaxSearchMode;
                    if (block is SageCRMEntryBlock)
                    {
                        form.ShowWorkflowButtons = (block as SageCRMEntryBlock).ShowWorkFlowButtons;
                        form.CheckLocks=(block as SageCRMEntryBlock).CheckLocks;
                        form.WorkFlowName = (block as SageCRMEntryBlock).WorkFlowName;
                        form.WFState = (block as SageCRMEntryBlock).WFState;
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