using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using SageCRM.AspNet;

namespace SageCRM.AspNet.Design
{
    class EntryDesigner : ControlDesigner
    {
        private DesignerVerbCollection _designerverbs;
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerverbs == null)
                {
                    _designerverbs = new DesignerVerbCollection();
                    _designerverbs.Add(new DesignerVerb("Sage CRM Screen Properties...",
                        new EventHandler(this.OnSageCRMPropertyBuilder)));
                    _designerverbs.Add(new DesignerVerb("Sage CRM Screen Builder...",
                        new EventHandler(this.OnPropertyBuilder)));
                }
                return _designerverbs;
            }
        }
        private void OnSageCRMPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentEntryPropEditor compEditor = new SageCRMComponentEntryPropEditor();
            compEditor.EditComponent(Component);
        }
        private void OnPropertyBuilder(object sender, EventArgs e)
        {
            SageCRMComponentEntryBlockEditor compEditor = new SageCRMComponentEntryBlockEditor();
            compEditor.EditComponent(Component);
        }
        public override void Initialize(IComponent component)
        {
            if ((!(component is SageCRMBaseEntryBlock)) &&
               (!(component is SageCRMEntryBlock)) &&
               //(!(component is SageCRMEntryItem)) &&
               (!(component is SageCRMPortalEntryBlock)))
            {
                throw new ArgumentException("Component must be a SageCRMEntryBlock or SageCRMPortalEntryBlock", "component");
            }
            base.Initialize(component);
        }
    }
}
